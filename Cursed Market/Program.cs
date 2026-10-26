//#define FORCED_OFFLINE
#define PROCESS_EXIT_HANDLER
#define EXCEPTION_HANDLER

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;






namespace Cursed_Market
{
    static class Program
    {
        public static void AttemptDisablingProxy()
        {
            /* Leave user's own proxy alone, only our leftover one has to be removed. */
            if (!WinReg.GetIsOwnProxyEnabled())
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

        private static void ExceptionHandler(object sender, ThreadExceptionEventArgs e)
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

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
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

#if EXCEPTION_HANDLER
            Application.ThreadException += new ThreadExceptionEventHandler(ExceptionHandler);
            try
            {
                Form_Wait formWait = new Form_Wait();
                formWait.Show();
                Application.DoEvents();

                Application.Run(new Form_Main());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Cursed Market Application.Run() Failed", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
#else
            Application.Run(new Form_Main());
#endif
        }
    }
}