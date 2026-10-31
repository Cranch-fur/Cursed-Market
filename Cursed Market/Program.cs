//#define FORCED_OFFLINE
#define PROCESS_EXIT_HANDLER
#define EXCEPTION_HANDLER

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using Avalonia.Threading;
using Cursed_Market.UI;
using System;
using System.Diagnostics;
using System.IO;






namespace Cursed_Market
{
    static class Program
    {
        public static void AttemptDisablingProxy()
        {
            /* Only Cursed Market proxy is removed (e.g. left over after a crash), any other proxy stays as it is. */
            if (!WinReg.GetIsCursedProxyRunning())
                return;

            /* Check boolean return value instead of catching exceptions that WinReg already swallows. */
            if (!WinReg.DisableProxy())
            {
                MessageBox.Show("Cursed Market failed to disable proxy!", "Cursed Market Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                Process.Start(new ProcessStartInfo("ms-settings:network-proxy") { UseShellExecute = true }); // Windows Settings network proxy settings tab.
            }
        }

        private static void ProcessExitHandler(object sender, EventArgs e)
        {
            AttemptDisablingProxy();
        }

        private static void ExceptionHandler(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            string exceptionData = e.Exception.ToString();

            try
            {
                string tempFolder = Path.GetTempPath();
                string logFile = Path.Combine(tempFolder, $"[{DateTime.UtcNow:yyyy-MM-dd HH-mm-ss}] Cursed Market Fatal Error.txt");

                File.WriteAllText(logFile, exceptionData);
                Process.Start(new ProcessStartInfo(logFile) { UseShellExecute = true })?.Dispose();
            }
            catch
            {
                MessageBox.Show(exceptionData, "Cursed Market Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

            ProgramGlobals.Close();
        }

        /* Avalonia setup, also used by IDE previewer. Roboto stays the default font of every window. */
        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .With(new FontManagerOptions { DefaultFamilyName = "Roboto" })
                .LogToTrace();
        }

        [STAThread]
        private static void Main(string[] args)
        {
            Properties.Localization.Culture = ProgramGlobals.culture; // We need to clarify what localization culture we're looking to use.

            if (ProgramPaths.GetDataFolderPath() == null) // We're getting a data folder path & verifying its existence at the same time.
            {
                Messaging.ShowMessage(Properties.Localization.MESSAGE_DataFolderCreationFailed, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                ProgramGlobals.Close();
                return;
            }

            if (!ProgramGlobals.GetIsFontInstalled())
            {
                if (File.Exists(ProgramPaths.fontFilePath))
                {
                    Messaging.ShowMessage(string.Format(Properties.Localization.MESSAGE_RobotoFontMissing, ProgramPaths.fontFilePath), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    Process.Start(new ProcessStartInfo(ProgramPaths.fontFilePath) { UseShellExecute = true });
                }
                else
                {
                    Messaging.ShowMessage(string.Format(Properties.Localization.MESSAGE_RobotoFontMissing, CursedAPI.SE_CommonEndpoints.font), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    Process.Start(new ProcessStartInfo(CursedAPI.SE_CommonEndpoints.font) { UseShellExecute = true });
                }

                ProgramGlobals.Close();
                return;
            }

#if FORCED_OFFLINE
            ProgramStartupArguments.GetStartupArguments().Add(ProgramStartupArguments.Common.offlineMode);
#endif

#if PROCESS_EXIT_HANDLER
            AppDomain.CurrentDomain.ProcessExit += ProcessExitHandler;
#endif

            /* Proxy left over after crash or forced kill keeps user offline, unless another running instance still owns it. */
            Process[] runningInstances = Process.GetProcessesByName(ProgramPaths.executableFriendlyName);
            if (runningInstances.Length <= 1)
                AttemptDisablingProxy();

            foreach (Process instance in runningInstances)
                instance.Dispose();

            /* Same as Application.Run(): process lives until Main form gets closed. Windows created from here on are Avalonia ones. */
            ClassicDesktopStyleApplicationLifetime lifetime = new ClassicDesktopStyleApplicationLifetime { Args = args, ShutdownMode = ShutdownMode.OnMainWindowClose };
            BuildAvaloniaApp().SetupWithLifetime(lifetime);

#if EXCEPTION_HANDLER
            Dispatcher.UIThread.UnhandledException += ExceptionHandler;
            try
            {
                Form_Wait formWait = new Form_Wait();
                formWait.Show();
                Dispatcher.UIThread.RunJobs(); // Application.DoEvents() equivalent.

                Form_Main formMain = new Form_Main();
                lifetime.MainWindow = formMain;
                lifetime.Startup += (sender, e) => formMain.Show(); // Form.Show() raises Load before window becomes visible, lifetime's own Show() wouldn't.
                lifetime.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Cursed Market Application.Run() Failed", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
#else
            Form_Main formMain = new Form_Main();
            lifetime.MainWindow = formMain;
            lifetime.Startup += (sender, e) => formMain.Show();
            lifetime.Start();
#endif
        }
    }
}