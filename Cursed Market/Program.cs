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
        private static void AttemptDisablingProxy()
        {
            try
            {
                WinReg.DisableProxy();
            }
            catch
            {
                MessageBox.Show("Cursed Market failed to disable proxy!", "Cursed Market Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                Process.Start("ms-settings:network-proxy"); // Windows Settings network proxy settings tab.
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
                string logFile = Path.Combine(tempFolder, $"[{DateTime.UtcNow.ToString("yyyy-MM-dd HH-mm-ss")}] Cursed Market Fatal Error.txt");

                File.WriteAllText(logFile, exceptionData);

                using (Process textviewer = Process.Start(new ProcessStartInfo(logFile)))
                {
                    textviewer.Dispose();
                }
            }
            catch
            {
                MessageBox.Show(exceptionData, "Cursed Market Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

            Globals.Application.Close();
        }




        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Properties.Localization.Culture = Globals.Application.culture; // We need to clarify what localization culture we're looking to use.


            if (Globals.Application.GetDataFolderPath() == null) // We're getting a data folder path & verifying it's existence at the same time
            {
                Messaging.ShowMessage(Properties.Localization.MESSAGE_DataFolderCreationFailed, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                Globals.Application.Close();
            }


            if (Globals.Application.Requirements.GetIsFontInstalled() == false)
            {
                if (File.Exists(Globals.Application.Requirements.robotoFontPath) == true)
                {
                    Messaging.ShowMessage(string.Format(Properties.Localization.MESSAGE_RobotoFontMissing, Globals.Application.Requirements.robotoFontPath), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    Process.Start(Globals.Application.Requirements.robotoFontPath);
                }
                else
                {
                    Messaging.ShowMessage(string.Format(Properties.Localization.MESSAGE_RobotoFontMissing, CursedAPI.SE_CommonEndpoints.robotoFont), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    Process.Start(CursedAPI.SE_CommonEndpoints.robotoFont);
                }

                Globals.Application.Close();
            }


#if FORCED_OFFLINE
            Globals.Application.startupArguments.Add(Globals.Application.SE_CommonStartupArguments.offlineMode);
#endif


#if PROCESS_EXIT_HANDLER
            AppDomain.CurrentDomain.ProcessExit += ProcessExitHandler;
#endif


#if EXCEPTION_HANDLER
            Application.ThreadException += new ThreadExceptionEventHandler(ExceptionHandler);
            try
            {
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
