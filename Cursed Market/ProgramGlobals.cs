using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;






namespace Cursed_Market
{
    public static class ProgramGlobals
    {
        public static bool isInitialized = false;
        public static bool isOfflineMode = false;




        public static readonly long startupTimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        public static readonly CultureInfo culture = ProgramStartupArguments.GetCulture();
        public static string version = FileVersionInfo.GetVersionInfo(ProgramPaths.executablePath)
            .FileVersion
            .Replace(".", string.Empty);




        public static void CheckForFirstLaunch()
        {
            if (WinReg.GetData_DWORD(WinReg.Common.firstLaunch) != 1)
            {
                Messaging.ShowMessage(Properties.Localization.MESSAGE_FirstLaunch, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button1);
                WinReg.SetData_DWORD(WinReg.Common.firstLaunch, 1);
            }
        }




        public static bool GetIsFontInstalled()
        {
            using (Font font = new Font("Roboto", 12, FontStyle.Regular, GraphicsUnit.Pixel))
            {
                return font.Name == "Roboto";
            }
        }
        



        public static void Close()
        {
            if (FiddlerCore.IsRunning() == true)
            {
                FiddlerCore.Stop();
            }

            Process.GetCurrentProcess().Kill();
        }
        public static void Restart()
        {
            Process.Start(ProgramPaths.executablePath);
            Close();
        }




        public static void SetGameChangerStatus(CursedAPI.E_GameChangers gameChanger, CursedAPI.E_GameChangerStatus newStatus)
        {
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