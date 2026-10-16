using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;






namespace Cursed_Market
{
    public static class ProgramGlobals
    {
        public static bool isInitialized = false;
        public static bool isOfflineMode = false;

        public static readonly long startupTimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        public static readonly CultureInfo culture = ProgramStartupArguments.GetCulture();

        /* Keep raw version without stripping dots, or fall back to "0.0.0.0" if missing. */
        private static string ObtainRawFileVersion()
        {
            string fileVer = FileVersionInfo.GetVersionInfo(ProgramPaths.executablePath).FileVersion;
            return string.IsNullOrWhiteSpace(fileVer) ? "0.0.0.0" : fileVer;
        }

        public static readonly string rawFileVersion = ObtainRawFileVersion();

        public static void CheckForFirstLaunch()
        {
            if (WinReg.GetData_DWORD(WinReg.Common.firstLaunch) != 1)
            {
                Messaging.ShowMessage(Properties.Localization.MESSAGE_FirstLaunch, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                WinReg.SetData_DWORD(WinReg.Common.firstLaunch, 1);
            }
        }

        public static bool GetIsFontInstalled()
        {
            using (Font font = new Font("Roboto", 12, FontStyle.Regular, GraphicsUnit.Pixel))
            {
                return font.Name.Equals("Roboto", StringComparison.OrdinalIgnoreCase);
            }
        }

        public static void Close()
        {
            if (FiddlerCore.IsRunning())
            {
                FiddlerCore.Stop();
            }

            // Ensure proxy is disabled before exiting the process.
            Program.AttemptDisablingProxy();

            Environment.Exit(0);
        }

        public static void Restart()
        {
            if (FiddlerCore.IsRunning())
            {
                FiddlerCore.Stop();
            }

            Program.AttemptDisablingProxy();

            Process.Start(ProgramPaths.executablePath);
            Environment.Exit(0);
        }

        public static void SetGameChangerStatus(CursedAPI.E_GameChangers gameChanger, CursedAPI.E_GameChangerStatus newStatus)
        {
            if (ProgramCache.Forms.Main == null || ProgramCache.Forms.Main.IsDisposed) return;

            if (ProgramCache.Forms.Main.InvokeRequired)
            {
                ProgramCache.Forms.Main.Invoke(new Action(() =>
                    ProgramCache.Forms.Main.SetGameChangerStatus(gameChanger, newStatus)));
            }
            else
            {
                ProgramCache.Forms.Main.SetGameChangerStatus(gameChanger, newStatus);
            }
        }

        public static void SetMainFunctionEnabled(CursedAPI.E_MainFunctions mainFunction, bool newStatus)
        {
            if (ProgramCache.Forms.Main == null || ProgramCache.Forms.Main.IsDisposed) return;

            if (ProgramCache.Forms.Main.InvokeRequired)
            {
                ProgramCache.Forms.Main.Invoke(new Action(() =>
                    ProgramCache.Forms.Main.SetMainFunctionEnabled(mainFunction, newStatus)));
            }
            else
            {
                ProgramCache.Forms.Main.SetMainFunctionEnabled(mainFunction, newStatus);
            }
        }
    }
}