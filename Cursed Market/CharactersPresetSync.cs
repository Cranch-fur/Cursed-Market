using Avalonia.Controls;
using Avalonia.Input;
using Cursed_Market.UI;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;






namespace Cursed_Market
{
    /* Downloads / updates "Characters Preset" folder from API manifest (only changed files are downloaded). */
    public static class CharactersPresetSync
    {
        public struct S_SyncResult
        {
            public bool success;
            public string errorMessage;
            public int downloadedFiles;
            public int upToDateFiles;
            public int skippedFiles;
            public int addedPresetEntries;
        }

        public struct S_UpdateCheckResult
        {
            public bool success;
            public int changedFiles;
            public int newCharacters;

            public bool HasUpdates => changedFiles > 0 || newCharacters > 0;
        }

        private const int REQUEST_TIMEOUT_SECONDS = 60;
        private const long MAX_FILE_SIZE = 16 * 1024 * 1024;
        private const string PRESET_FILE_NAME = "CharactersPreset.json";
        private static readonly string[] ALLOWED_EXTENSIONS = { ".json", ".png" };

        private static bool isRunning = false;
        private static bool isCheckedThisSession = false;




        /* UI entry point: asks for confirmation, runs sync in background and reports result. */
        public static async Task<bool> RunWithConfirmationAsync(Control busyControl, bool askConfirmation = true)
        {
            if (isRunning)
                return false;

            DialogResult confirmation = !askConfirmation ? DialogResult.Yes : Messaging.ShowDialog(
                "Download the latest Characters Preset from Cursed Market API?\n\n" +
                "Characters map and portraits will be updated, your own values in \"CharactersPreset.json\" are kept (only new characters are added).",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmation != DialogResult.Yes)
                return false;

            isRunning = true;
            if (busyControl != null)
                busyControl.IsEnabled = false;

            TopLevel busyWindow = busyControl != null ? TopLevel.GetTopLevel(busyControl) : null; // Avalonia has no global cursor, wait cursor is shown over the busy window instead.
            Cursor previousCursor = busyWindow?.Cursor;
            if (busyWindow != null)
                busyWindow.Cursor = new Cursor(StandardCursorType.Wait);

            S_SyncResult result;
            try
            {
                result = await Task.Run(() => Sync());
            }
            finally
            {
                if (busyWindow != null)
                    busyWindow.Cursor = previousCursor;
                if (busyControl != null)
                    busyControl.IsEnabled = true;

                isRunning = false;
            }

            if (!result.success)
            {
                Messaging.ShowMessage($"Failed to update Characters Preset!\n\n{result.errorMessage}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            /* Re-apply preset in memory if the feature is currently active. */
            if (ProgramFeatures.CharactersPreset.enabled && ProgramFeatures.CharactersPreset.Obtain())
                ProgramFeatures.CharactersPreset.SetEnabled(true);

            Media.PlaySoundFromStream(Properties.Resources.SFX_Activate);
            Messaging.ShowMessage(
                "Characters Preset is up to date!\n\n" +
                $"Downloaded files: {result.downloadedFiles}\n" +
                $"Already up to date: {result.upToDateFiles}\n" +
                $"New characters added to your preset: {result.addedPresetEntries}" +
                (result.skippedFiles > 0 ? $"\nSkipped (invalid entries): {result.skippedFiles}" : string.Empty),
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            return true;
        }


        public static S_SyncResult Sync()
        {
            S_SyncResult result = new S_SyncResult();

            try
            {
                using (HttpClient httpClient = CreateHttpClient())
                {
                    JArray manifestFiles = FetchManifest(httpClient, out string baseUrl);

                    Directory.CreateDirectory(ProgramPaths.charactersPresetDirectoryPath);

                    foreach (JToken manifestFile in manifestFiles)
                    {
                        string relativePath = (string)manifestFile["path"];
                        string expectedHash = ((string)manifestFile["sha256"])?.ToLowerInvariant();
                        long expectedSize = (long?)manifestFile["size"] ?? -1;

                        string localPath = ResolveLocalPath(relativePath);
                        if (localPath == null || string.IsNullOrEmpty(expectedHash) || expectedSize < 0 || expectedSize > MAX_FILE_SIZE)
                        {
                            result.skippedFiles++;
                            continue;
                        }

                        bool isPresetFile = string.Equals(relativePath, PRESET_FILE_NAME, StringComparison.OrdinalIgnoreCase);
                        if (File.Exists(localPath) && !isPresetFile && GetFileHash(localPath) == expectedHash)
                        {
                            result.upToDateFiles++;
                            continue;
                        }

                        byte[] fileBytes = httpClient.GetByteArrayAsync(BuildFileUrl(baseUrl, relativePath)).GetAwaiter().GetResult();
                        if (fileBytes.ComputeHash(Extensions.HashType.SHA256) != expectedHash)
                            throw new InvalidDataException($"Checksum mismatch for \"{relativePath}\".");

                        if (isPresetFile)
                        {
                            int addedEntries = MergePresetFile(localPath, fileBytes);
                            result.addedPresetEntries += addedEntries;

                            if (addedEntries > 0)
                                result.downloadedFiles++;
                            else
                                result.upToDateFiles++;

                            continue;
                        }

                        WriteFileSafely(localPath, fileBytes);
                        result.downloadedFiles++;
                    }
                }

                result.success = true;
            }
            catch (Exception ex)
            {
                Exception rootException = ex is AggregateException aggregate ? aggregate.GetBaseException() : ex;
                result.errorMessage = rootException.Message;
            }

            return result;
        }


        /* Compares manifest with local files without writing anything. */
        public static S_UpdateCheckResult CheckForUpdates()
        {
            S_UpdateCheckResult result = new S_UpdateCheckResult();

            try
            {
                using (HttpClient httpClient = CreateHttpClient())
                {
                    JArray manifestFiles = FetchManifest(httpClient, out string baseUrl);

                    foreach (JToken manifestFile in manifestFiles)
                    {
                        string relativePath = (string)manifestFile["path"];
                        string expectedHash = ((string)manifestFile["sha256"])?.ToLowerInvariant();
                        long expectedSize = (long?)manifestFile["size"] ?? -1;

                        string localPath = ResolveLocalPath(relativePath);
                        if (localPath == null || string.IsNullOrEmpty(expectedHash) || expectedSize < 0 || expectedSize > MAX_FILE_SIZE)
                            continue;

                        if (!File.Exists(localPath))
                        {
                            result.changedFiles++;
                            continue;
                        }

                        if (GetFileHash(localPath) == expectedHash)
                            continue;

                        /* User's preset always differs from server one, only characters missing locally count as update. */
                        if (string.Equals(relativePath, PRESET_FILE_NAME, StringComparison.OrdinalIgnoreCase))
                        {
                            byte[] serverBytes = httpClient.GetByteArrayAsync(BuildFileUrl(baseUrl, relativePath)).GetAwaiter().GetResult();
                            result.newCharacters += CountMissingPresetEntries(localPath, serverBytes);
                            continue;
                        }

                        result.changedFiles++;
                    }
                }

                result.success = true;
            }
            catch
            {
                result.success = false;
            }

            return result;
        }


        /* Checks for updates once per session (first Characters Preset window opening). Returns true if update was offered. */
        public static async Task<bool> CheckOnFirstOpenAsync(Control busyControl)
        {
            if (isCheckedThisSession || isRunning || ProgramGlobals.isOfflineMode)
                return false;

            isCheckedThisSession = true;

            TopLevel busyWindow = busyControl != null ? TopLevel.GetTopLevel(busyControl) : null;
            Cursor previousCursor = busyWindow?.Cursor;
            if (busyWindow != null)
                busyWindow.Cursor = new Cursor(StandardCursorType.Wait);

            S_UpdateCheckResult checkResult;
            try
            {
                checkResult = await Task.Run(() => CheckForUpdates());
            }
            finally
            {
                if (busyWindow != null)
                    busyWindow.Cursor = previousCursor;
            }

            /* Network errors are ignored here, local files are used as is. */
            if (!checkResult.success || !checkResult.HasUpdates)
                return false;

            string updateDetails = string.Empty;
            if (checkResult.changedFiles > 0)
                updateDetails += $"New or changed files: {checkResult.changedFiles}\n";
            if (checkResult.newCharacters > 0)
                updateDetails += $"New characters for your preset: {checkResult.newCharacters}\n";

            if (Messaging.ShowDialog($"Characters Preset update is available!\n\n{updateDetails}\nDownload it now? Your own values in \"CharactersPreset.json\" are kept.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                await RunWithConfirmationAsync(busyControl, false);

            return true;
        }


        private static JArray FetchManifest(HttpClient httpClient, out string baseUrl)
        {
            string manifestText = httpClient.GetStringAsync(CursedAPI.SE_CommonEndpoints.charactersPreset).GetAwaiter().GetResult();
            JObject manifest = JObject.Parse(manifestText);

            baseUrl = (string)manifest["baseUrl"];
            if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out Uri baseUri) || baseUri.Scheme != Uri.UriSchemeHttps)
                throw new InvalidDataException("Manifest contains invalid base URL.");

            if (!(manifest["files"] is JArray manifestFiles))
                throw new InvalidDataException("Manifest doesn't contain files list.");

            return manifestFiles;
        }


        private static int CountMissingPresetEntries(string localPath, byte[] serverBytes)
        {
            try
            {
                JObject localPreset = JObject.Parse(File.ReadAllText(localPath));
                JObject serverPreset = JObject.Parse(System.Text.Encoding.UTF8.GetString(serverBytes));

                return serverPreset.Properties().Count(p => localPreset[p.Name] == null);
            }
            catch
            {
                return 1; // Broken local preset, merge will repair it.
            }
        }


        private static HttpClient CreateHttpClient()
        {
            HttpClient httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(REQUEST_TIMEOUT_SECONDS),
                MaxResponseContentBufferSize = MAX_FILE_SIZE
            };

            /* Reuse default "Name: value" headers (User-Agent, Application-Version, ...). */
            foreach (string header in ProgramNetworking.defaultHeaders)
            {
                int separatorIndex = header.IndexOf(':');
                if (separatorIndex > 0)
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Substring(0, separatorIndex).Trim(), header.Substring(separatorIndex + 1).Trim());
            }

            return httpClient;
        }


        /* Returns absolute path inside "Characters Preset" folder or null if entry is unsafe. */
        private static string ResolveLocalPath(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath) || relativePath.Contains("..") || relativePath.Contains(":") || relativePath.StartsWith("/") || relativePath.StartsWith("\\"))
                return null;

            if (relativePath.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
                return null;

            if (!ALLOWED_EXTENSIONS.Contains(Path.GetExtension(relativePath).ToLowerInvariant()))
                return null;

            string baseDirectory = Path.GetFullPath(ProgramPaths.charactersPresetDirectoryPath).TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
            string fullPath = Path.GetFullPath(Path.Combine(baseDirectory, relativePath.Replace('/', Path.DirectorySeparatorChar)));

            return fullPath.StartsWith(baseDirectory, StringComparison.OrdinalIgnoreCase) ? fullPath : null;
        }


        private static string BuildFileUrl(string baseUrl, string relativePath)
        {
            string encodedPath = string.Join("/", relativePath.Split('/').Select(Uri.EscapeDataString));
            return baseUrl.TrimEnd('/') + "/" + encodedPath;
        }


        private static string GetFileHash(string filePath)
        {
            try
            {
                return File.ReadAllBytes(filePath).ComputeHash(Extensions.HashType.SHA256);
            }
            catch
            {
                return string.Empty;
            }
        }


        /* Writes to temp file first, so interrupted download never leaves broken file. */
        private static void WriteFileSafely(string filePath, byte[] fileBytes)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            string tempFilePath = filePath + ".download";
            File.WriteAllBytes(tempFilePath, fileBytes);

            if (File.Exists(filePath))
                File.Delete(filePath);

            File.Move(tempFilePath, filePath);
        }


        /* Keeps user values, adds only characters missing locally. Returns amount of added entries. */
        private static int MergePresetFile(string localPath, byte[] serverBytes)
        {
            string serverText = System.Text.Encoding.UTF8.GetString(serverBytes);

            JObject localPreset = null;
            if (File.Exists(localPath))
            {
                try
                {
                    localPreset = JObject.Parse(File.ReadAllText(localPath));
                }
                catch
                {
                    /* Broken local file is kept as backup and replaced. */
                    File.Copy(localPath, localPath + ".bak", true);
                }
            }

            JObject serverPreset = JObject.Parse(serverText);
            if (localPreset == null)
            {
                WriteFileSafely(localPath, serverBytes);
                return serverPreset.Count;
            }

            int addedEntries = 0;
            foreach (KeyValuePair<string, JToken> serverEntry in serverPreset)
            {
                if (localPreset[serverEntry.Key] == null)
                {
                    localPreset[serverEntry.Key] = serverEntry.Value.DeepClone();
                    addedEntries++;
                }
            }

            if (addedEntries > 0)
                WriteFileSafely(localPath, System.Text.Encoding.UTF8.GetBytes(localPreset.ToString()));

            return addedEntries;
        }
    }
}
