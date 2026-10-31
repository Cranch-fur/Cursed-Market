using Cursed_Market.UI;
using Fiddler;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;






namespace Cursed_Market
{
    /* Members of this class are available in Extra Logic scripts as globals. */
    public class ExtraLogicHost
    {
        /* Built once: IsGameSession is called on every proxied request. */
        private static readonly HashSet<string> GAME_HOST_NAMES = new HashSet<string>(ProgramSession.Game.Platform.GetPlatformHostNames(), StringComparer.OrdinalIgnoreCase);

        private readonly ExtraLogic.ModuleState state;
        private readonly object storageLock = new object();
        private JObject storage;


        internal ExtraLogicHost(ExtraLogic.ModuleState moduleState)
        {
            state = moduleState;
        }


        public string ModuleName => state.info.name;
        public string ModuleFilePath => state.info.filePath;
        public string ModuleDirectory => ProgramPaths.extraLogicDirectoryPath;

        public string UserId => ProgramSession.Game.userId;
        public string MatchId => ProgramSession.Game.matchId;
        public bool IsInQueue => ProgramSession.Game.isInQueue;
        public bool IsInMatch => ProgramSession.Game.isInMatch;
        public string PlayerRole => ProgramSession.Game.playerRole.ToString();
        public string GamePlatform => ProgramSession.Game.Platform.currentPlatform.ToString();


        /* Handlers are wrapped in try/catch and removed automatically when proxy stops. */
        public void OnBeforeRequest(Action<Session> handler) => state.Subscribe(ExtraLogic.E_Hook.BeforeRequest, handler);
        public void OnBeforeResponse(Action<Session> handler) => state.Subscribe(ExtraLogic.E_Hook.BeforeResponse, handler);
        public void OnAfterSessionComplete(Action<Session> handler) => state.Subscribe(ExtraLogic.E_Hook.AfterSessionComplete, handler);


        public void OnStop(Action handler)
        {
            if (handler != null)
                state.stopHandlers.Add(handler);
        }


        /* True if session targets one of the known game hosts. */
        public bool IsGameSession(Session session)
        {
            return session != null && session.hostname != null && GAME_HOST_NAMES.Contains(session.hostname);
        }


        public void Log(string message) => state.WriteLog(message);


        public void Notify(string message) => ExtraLogic.ShowNotify(ModuleName, message);


        /* Non-blocking message box, safe to call from proxy threads. */
        public void ShowMessage(string message) => ExtraLogic.ShowMessageAsync(ModuleName, message);


        /* Persistent per-module JSON storage ("Extra Logic\Data\<module>.json").
           Proxy handlers run in parallel, so modify it from handlers through UpdateStorage or lock (StorageLock). */
        public JObject Storage
        {
            get
            {
                lock (storageLock)
                {
                    if (storage == null)
                        storage = ExtraLogic.ReadStorage(state.info);

                    return storage;
                }
            }
        }

        public object StorageLock => storageLock;


        public void SaveStorage()
        {
            lock (storageLock)
            {
                ExtraLogic.WriteStorage(state.info, Storage);
            }
        }


        /* Thread-safe change of storage, optionally saved to disk right away. */
        public void UpdateStorage(Action<JObject> update, bool saveToDisk = true)
        {
            if (update == null)
                return;

            lock (storageLock)
            {
                update(Storage);

                if (saveToDisk)
                    ExtraLogic.WriteStorage(state.info, storage);
            }
        }
    }




    public static class ExtraLogic
    {
        public enum E_ModuleState
        {
            New,
            Changed,
            Enabled,
            Disabled
        }


        public enum E_Hook
        {
            BeforeRequest,
            BeforeResponse,
            AfterSessionComplete
        }


        public class ModuleInfo
        {
            public string filePath;
            public string fileName;
            public string name;
            public string author;
            public string version;
            public List<string> description = new List<string>();
            public List<string> includes = new List<string>();
            public List<string> capabilities = new List<string>();
            public bool hasRiskyCapabilities;
            public bool hasMetadata;
            public string code;
            public string sha256;
            public E_ModuleState state;
            public bool isSelected;
        }


        internal class ModuleState
        {
            public readonly ModuleInfo info;
            public readonly ExtraLogicHost host;
            public readonly List<Action> stopHandlers = new List<Action>();

            private readonly List<KeyValuePair<E_Hook, SessionStateHandler>> subscriptions = new List<KeyValuePair<E_Hook, SessionStateHandler>>();
            private readonly object logLock = new object();
            private int runtimeErrorCount;


            public ModuleState(ModuleInfo moduleInfo)
            {
                info = moduleInfo;
                host = new ExtraLogicHost(this);
            }


            public void Subscribe(E_Hook hook, Action<Session> handler)
            {
                if (handler == null)
                    return;

                SessionStateHandler wrappedHandler = session =>
                {
                    try
                    {
                        handler(session);
                    }
                    catch (Exception ex)
                    {
                        ReportRuntimeError(ex);
                    }
                };

                switch (hook)
                {
                    case E_Hook.BeforeRequest:
                        FiddlerApplication.BeforeRequest += wrappedHandler;
                        break;

                    case E_Hook.BeforeResponse:
                        FiddlerApplication.BeforeResponse += wrappedHandler;
                        break;

                    case E_Hook.AfterSessionComplete:
                        FiddlerApplication.AfterSessionComplete += wrappedHandler;
                        break;
                }

                subscriptions.Add(new KeyValuePair<E_Hook, SessionStateHandler>(hook, wrappedHandler));
            }


            public void Unload()
            {
                foreach (var subscription in subscriptions)
                {
                    switch (subscription.Key)
                    {
                        case E_Hook.BeforeRequest:
                            FiddlerApplication.BeforeRequest -= subscription.Value;
                            break;

                        case E_Hook.BeforeResponse:
                            FiddlerApplication.BeforeResponse -= subscription.Value;
                            break;

                        case E_Hook.AfterSessionComplete:
                            FiddlerApplication.AfterSessionComplete -= subscription.Value;
                            break;
                    }
                }
                subscriptions.Clear();

                foreach (Action stopHandler in stopHandlers)
                {
                    try
                    {
                        stopHandler();
                    }
                    catch (Exception ex)
                    {
                        WriteLog($"OnStop handler failed: {ex}");
                    }
                }
                stopHandlers.Clear();
            }


            public void WriteLog(string message)
            {
                try
                {
                    Directory.CreateDirectory(LOGS_DIRECTORY_PATH);
                    string logFilePath = Path.Combine(LOGS_DIRECTORY_PATH, Path.GetFileNameWithoutExtension(info.fileName) + ".log");

                    lock (logLock)
                    {
                        File.AppendAllText(logFilePath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}");
                    }
                }
                catch
                { }
            }


            private void ReportRuntimeError(Exception ex)
            {
                WriteLog($"Runtime error: {ex}");

                /* Notify only once per module to avoid spam on every request. */
                if (Interlocked.Increment(ref runtimeErrorCount) == 1)
                    ShowNotify(info.name, "Module has thrown an exception, details are written to Extra Logic\\Logs.");
            }
        }




        private static readonly string LOGS_DIRECTORY_PATH = Path.Combine(ProgramPaths.extraLogicDirectoryPath, "Logs");
        private static readonly string DATA_DIRECTORY_PATH = Path.Combine(ProgramPaths.extraLogicDirectoryPath, "Data");
        private const string DECISION_ENTRY_PREFIX = "Extra Logic: ";
        private const int MAX_INCLUDE_DEPTH = 4;

        private static readonly string[] SCRIPT_IMPORTS =
        {
            "System",
            "System.IO",
            "System.Linq",
            "System.Text",
            "System.Text.RegularExpressions",
            "System.Collections.Generic",
            "System.Windows.Forms",
            "Fiddler",
            "Newtonsoft.Json",
            "Newtonsoft.Json.Linq",
            "Cursed_Market"
        };

        /* Regex -> human readable description, risky flag. Heuristic scan, not a security guarantee. */
        private static readonly (Regex pattern, string description, bool isRisky)[] CAPABILITY_RULES =
        {
            (new Regex(@"\bOnBeforeRequest\b|\bBeforeRequest\s*\+="), "Intercepts outgoing requests (game -> server)", false),
            (new Regex(@"\bOnBeforeResponse\b|\bBeforeResponse\s*\+="), "Intercepts incoming responses (server -> game)", false),
            (new Regex(@"\bOnAfterSessionComplete\b|\bAfterSessionComplete\s*\+="), "Inspects finished requests", false),
            (new Regex(@"utilSet(Request|Response)Body|utilReplaceIn(Request|Response)|\.(oRequest|oResponse)\s*\[[^\]]+\]\s*="), "Changes request / response content", false),
            (new Regex(@"\b(HttpClient|WebClient|WebRequest|HttpWebRequest|TcpClient|UdpClient|Socket)\b|\bNetworking\.(Get|Post|Download)"), "Makes its own network connections", false),
            (new Regex(@"\b(File|Directory)\.\w+|\bFileStream\b|\bStream(Reader|Writer)\b"), "Reads or writes files on disk", false),
            (new Regex(@"\bStorage\b|\bSaveStorage\s*\("), "Keeps its own saved data (Extra Logic\\Data)", false),
            (new Regex(@"\b(MessageBox|ShowMessage|Notify)\s*[\.(]"), "Shows messages / notifications", false),
            (new Regex(@"\bProcess\.Start\b|\bProcessStartInfo\b"), "Starts other programs", true),
            (new Regex(@"\bRegistry(Key)?\b|\bWinReg\."), "Reads or changes Windows registry", true),
            (new Regex(@"\bDllImport\b|\bMarshal\.|\bAssembly\.Load|\bActivator\.CreateInstance|\bGetMethod\s*\("), "Uses native code or reflection", true),
            (new Regex(@"^\s*#r\s", RegexOptions.Multiline), "Loads external assemblies (#r)", true)
        };

        private static readonly Regex METADATA_REGEX = new Regex(@"^\s*//\s*@(?<key>name|author|version|description)\b[ \t]*(?<value>.*)$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        private static readonly Regex LOAD_DIRECTIVE_REGEX = new Regex("^\\s*#load\\s+\"(?<path>[^\"]+)\"", RegexOptions.Multiline);
        private static readonly Regex COMMENTS_REGEX = new Regex(@"/\*.*?\*/|//[^\r\n]*", RegexOptions.Singleline);

        private static readonly List<ModuleState> loadedModules = new List<ModuleState>();
        private static ImmutableArray<MetadataReference> cachedReferences;




        /* True if user closed module list with Cancel, proxy start must be aborted. */
        public static bool WasStartCancelled { get; private set; }


        /* Returns names of running modules or null if start was cancelled in module list. */
        public static List<string> LoadAll()
        {
            UnloadAll();
            WasStartCancelled = false;

            List<string> loadedNames = new List<string>();
            bool isShiftHeld = (System.Windows.Forms.Control.ModifierKeys & System.Windows.Forms.Keys.Shift) != 0; // Reads live keyboard state, works without WinForms message loop.

            if (!isShiftHeld)
                return loadedNames;

            List<ModuleInfo> modules = ScanModules();
            modules = ShowManager(modules);
            if (modules == null)
            {
                WasStartCancelled = true;
                return null;
            }

            foreach (ModuleInfo module in modules.Where(m => m.isSelected))
            {
                if (RunModule(module))
                    loadedNames.Add(module.name);
            }

            if (loadedNames.Count > 0)
                ShowNotify("Extra Logic", $"Running modules: {string.Join(", ", loadedNames)}");

            return loadedNames;
        }


        /* Every *.csx in "Extra Logic" folder is a module (sub-folders are reserved for #load includes). */
        public static List<ModuleInfo> ScanModules()
        {
            List<ModuleInfo> modules = new List<ModuleInfo>();
            if (!Directory.Exists(ProgramPaths.extraLogicDirectoryPath))
                return modules;

            string[] moduleFiles = Directory.GetFiles(ProgramPaths.extraLogicDirectoryPath, "*.csx", SearchOption.TopDirectoryOnly);
            Array.Sort(moduleFiles, StringComparer.OrdinalIgnoreCase); // Load order = alphabetical order.

            foreach (string moduleFile in moduleFiles)
            {
                ModuleInfo info = ReadModuleInfo(moduleFile);
                if (info == null)
                    continue;

                info.state = GetSavedState(info);
                info.isSelected = info.state == E_ModuleState.Enabled;
                modules.Add(info);
            }

            return modules;
        }


        public static void UnloadAll()
        {
            foreach (ModuleState moduleState in loadedModules)
                moduleState.Unload();

            loadedModules.Clear();
        }


        public static ModuleInfo ReadModuleInfo(string filePath)
        {
            string code;
            try
            {
                code = File.ReadAllText(filePath);
            }
            catch
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(code))
                return null;

            ModuleInfo info = new ModuleInfo
            {
                filePath = filePath,
                fileName = Path.GetFileName(filePath),
                name = Path.GetFileNameWithoutExtension(filePath),
                code = code
            };

            foreach (Match match in METADATA_REGEX.Matches(code))
            {
                string value = match.Groups["value"].Value.Trim();
                if (value.Length == 0)
                    continue;

                info.hasMetadata = true;
                switch (match.Groups["key"].Value.ToLowerInvariant())
                {
                    case "name": info.name = value; break;
                    case "author": info.author = value; break;
                    case "version": info.version = value; break;
                    case "description": info.description.Add(value); break;
                }
            }

            /* Included files take part in scan and hash, so editing them asks for permission again. */
            StringBuilder combinedCode = new StringBuilder(code);
            CollectIncludes(filePath, code, combinedCode, info.includes, new HashSet<string>(StringComparer.OrdinalIgnoreCase) { Path.GetFullPath(filePath) }, 0);

            string scanSource = COMMENTS_REGEX.Replace(combinedCode.ToString(), string.Empty);
            foreach (var rule in CAPABILITY_RULES)
            {
                if (rule.pattern.IsMatch(scanSource))
                {
                    info.capabilities.Add(rule.isRisky ? $"[!] {rule.description}" : rule.description);
                    info.hasRiskyCapabilities |= rule.isRisky;
                }
            }

            info.sha256 = Encoding.UTF8.GetBytes(combinedCode.ToString()).ComputeHash(Extensions.HashType.SHA256);
            return info;
        }


        private static void CollectIncludes(string ownerFilePath, string ownerCode, StringBuilder combinedCode, List<string> includes, HashSet<string> visited, int depth)
        {
            if (depth >= MAX_INCLUDE_DEPTH)
                return;

            foreach (Match match in LOAD_DIRECTIVE_REGEX.Matches(ownerCode))
            {
                string includePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(ownerFilePath), match.Groups["path"].Value));
                if (!visited.Add(includePath) || !File.Exists(includePath))
                    continue;

                string includeCode = File.ReadAllText(includePath);
                includes.Add(GetRelativePath(includePath));
                combinedCode.Append('\n').Append(includeCode);

                CollectIncludes(includePath, includeCode, combinedCode, includes, visited, depth + 1);
            }
        }


        private static string GetRelativePath(string fullPath)
        {
            string baseDirectory = Path.GetFullPath(ProgramPaths.extraLogicDirectoryPath).TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
            return fullPath.StartsWith(baseDirectory, StringComparison.OrdinalIgnoreCase) ? fullPath.Substring(baseDirectory.Length) : fullPath;
        }


        public static string BuildSummary(ModuleInfo info)
        {
            StringBuilder summary = new StringBuilder();

            summary.AppendLine("WHAT IT DOES (from module author):");
            if (info.description.Count > 0)
            {
                foreach (string line in info.description)
                    summary.AppendLine("  " + line);
            }
            else
            {
                summary.AppendLine("  No description provided. Add \"// @description ...\" lines to the top of the file.");
            }

            summary.AppendLine();
            summary.AppendLine("DETECTED BEHAVIOUR (automatic code scan, may be incomplete):");
            if (info.capabilities.Count > 0)
            {
                foreach (string capability in info.capabilities)
                    summary.AppendLine("  - " + capability);
            }
            else
            {
                summary.AppendLine("  - Nothing notable detected");
            }

            if (info.hasRiskyCapabilities)
            {
                summary.AppendLine();
                summary.AppendLine("[!] Module uses potentially dangerous APIs. Enable it only if you trust its source.");
            }

            summary.AppendLine();
            summary.AppendLine($"File: {info.fileName} ({info.code.Length / 1024.0:0.0} KB)");
            if (info.includes.Count > 0)
                summary.AppendLine($"Includes: {string.Join(", ", info.includes)}");
            summary.AppendLine($"SHA-256: {info.sha256}");

            return summary.ToString();
        }


        /* Saved choice is valid only for exact same content (module + its #load includes). */
        private static E_ModuleState GetSavedState(ModuleInfo info)
        {
            string storedDecision = WinReg.GetData_SZ(DECISION_ENTRY_PREFIX + info.fileName);
            if (string.IsNullOrEmpty(storedDecision))
                return E_ModuleState.New;

            string[] decisionParts = storedDecision.Split('|');
            if (decisionParts.Length != 2 || decisionParts[0] != info.sha256)
                return E_ModuleState.Changed;

            return decisionParts[1] == "1" ? E_ModuleState.Enabled : E_ModuleState.Disabled;
        }


        /* Returns modules with user's selection or null if cancelled. Selection is saved on Start. */
        private static List<ModuleInfo> ShowManager(List<ModuleInfo> modules)
        {
            List<ModuleInfo> result = null;

            void ShowDialog()
            {
                using (Form_ExtraLogicManager manager = new Form_ExtraLogicManager(modules, ScanModules))
                {
                    if (manager.ShowDialog() != DialogResult.OK)
                        return;

                    result = manager.GetModules();
                }
            }

            Form mainForm = ProgramCache.Forms.Main;
            if (mainForm != null && !mainForm.IsDisposed && mainForm.InvokeRequired)
                mainForm.Invoke((Action)ShowDialog);
            else
                ShowDialog();

            if (result != null)
            {
                foreach (ModuleInfo module in result)
                {
                    WinReg.SetData_SZ(DECISION_ENTRY_PREFIX + module.fileName, $"{module.sha256}|{(module.isSelected ? "1" : "0")}");
                    module.state = module.isSelected ? E_ModuleState.Enabled : E_ModuleState.Disabled;
                }
            }

            return result;
        }


        private static bool RunModule(ModuleInfo info)
        {
            ModuleState moduleState = new ModuleState(info);

            try
            {
                Script<object> script = CSharpScript.Create(info.code, BuildScriptOptions(info.filePath), typeof(ExtraLogicHost));

                List<Diagnostic> compileErrors = script.Compile().Where(d => d.Severity == DiagnosticSeverity.Error).ToList();
                if (compileErrors.Count > 0)
                {
                    string errorsText = string.Join(Environment.NewLine, compileErrors.Take(10).Select(d => d.ToString()));
                    Messaging.ShowMessage($"Failed to compile Extra Logic module \"{info.name}\"!\n\n{errorsText}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                script.RunAsync(moduleState.host).GetAwaiter().GetResult();
                loadedModules.Add(moduleState);
                moduleState.WriteLog("Module loaded.");
                return true;
            }
            catch (Exception ex)
            {
                moduleState.Unload();
                moduleState.WriteLog($"Load failed: {ex}");
                Messaging.ShowMessage($"Failed to run Extra Logic module \"{info.name}\"!\n\n{ex.Message}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        private static ScriptOptions BuildScriptOptions(string filePath)
        {
            string baseDirectory = ProgramPaths.extraLogicDirectoryPath;

            return ScriptOptions.Default
                .WithFilePath(filePath)
                .WithReferences(GetMetadataReferences())
                .WithImports(SCRIPT_IMPORTS)
                .WithSourceResolver(new SourceFileResolver(ImmutableArray.Create(baseDirectory), baseDirectory))
                .WithMetadataResolver(ScriptMetadataResolver.Default.WithBaseDirectory(baseDirectory).WithSearchPaths(baseDirectory));
        }


        private static ImmutableArray<MetadataReference> GetMetadataReferences()
        {
            if (!cachedReferences.IsDefault)
                return cachedReferences;

            Assembly[] assemblies =
            {
                typeof(object).Assembly,
                typeof(Uri).Assembly,
                typeof(Enumerable).Assembly,
                typeof(System.Windows.Forms.Form).Assembly, // Scripts keep WinForms (MessageBox etc.) even though UI itself is Avalonia now.
                typeof(System.Drawing.Bitmap).Assembly,
                typeof(System.Net.Http.HttpClient).Assembly,
                typeof(FiddlerApplication).Assembly,
                typeof(JObject).Assembly,
                typeof(ExtraLogic).Assembly
            };

            cachedReferences = assemblies
                .Distinct()
                .Select(CreateReference)
                .Where(reference => reference != null)
                .ToImmutableArray();

            return cachedReferences;
        }


        /* Costura loads embedded assemblies from memory (empty Location), so image is read from exe resources. */
        private static MetadataReference CreateReference(Assembly assembly)
        {
            if (!string.IsNullOrEmpty(assembly.Location) && File.Exists(assembly.Location))
                return MetadataReference.CreateFromFile(assembly.Location);

            byte[] image = ReadCosturaImage(assembly.GetName().Name);
            return image != null ? MetadataReference.CreateFromImage(ImmutableArray.Create(image)) : null;
        }


        private static byte[] ReadCosturaImage(string assemblyName)
        {
            Assembly executingAssembly = typeof(ExtraLogic).Assembly;
            string baseName = $"costura.{assemblyName.ToLowerInvariant()}.dll";
            string resourceName = executingAssembly.GetManifestResourceNames().FirstOrDefault(r =>
                r.Equals(baseName, StringComparison.OrdinalIgnoreCase) ||
                r.Equals(baseName + ".compressed", StringComparison.OrdinalIgnoreCase));

            if (resourceName == null)
                return null;

            using (Stream resourceStream = executingAssembly.GetManifestResourceStream(resourceName))
            using (MemoryStream resultStream = new MemoryStream())
            {
                if (resourceName.EndsWith(".compressed", StringComparison.OrdinalIgnoreCase))
                {
                    using (DeflateStream deflateStream = new DeflateStream(resourceStream, CompressionMode.Decompress))
                        deflateStream.CopyTo(resultStream);
                }
                else
                {
                    resourceStream.CopyTo(resultStream);
                }

                return resultStream.ToArray();
            }
        }


        internal static JObject ReadStorage(ModuleInfo info)
        {
            try
            {
                string storagePath = GetStoragePath(info);
                if (File.Exists(storagePath))
                    return JObject.Parse(File.ReadAllText(storagePath));
            }
            catch
            { }

            return new JObject();
        }


        internal static void WriteStorage(ModuleInfo info, JObject storage)
        {
            Directory.CreateDirectory(DATA_DIRECTORY_PATH);
            File.WriteAllText(GetStoragePath(info), storage.ToString());
        }


        private static string GetStoragePath(ModuleInfo info)
        {
            return Path.Combine(DATA_DIRECTORY_PATH, Path.GetFileNameWithoutExtension(info.fileName) + ".json");
        }


        /* NotifyIcon needs a message loop, so balloon is shown through Main form thread. */
        internal static void ShowNotify(string title, string message)
        {
            Form mainForm = ProgramCache.Forms.Main;
            if (mainForm == null || mainForm.IsDisposed || !mainForm.IsHandleCreated)
                return;

            mainForm.BeginInvoke((Action)(() => Messaging.ShowNotify(title, message, Properties.Resources.Icon, ToolTipIcon.Info)));
        }


        internal static void ShowMessageAsync(string title, string message)
        {
            Thread messageThread = new Thread(() => MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information));
            messageThread.SetApartmentState(ApartmentState.STA);
            messageThread.IsBackground = true;
            messageThread.Start();
        }
    }
}
