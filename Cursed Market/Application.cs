using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursed_Market
{
    public static class Application
    {
        private static string GetApplicationVersion()
        {
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(executablePath);
            return versionInfo.FileVersion.Replace(".", string.Empty);
        }
        public static readonly string version = GetApplicationVersion();


        public static readonly List<string> startupArguments = new List<string>(Environment.GetCommandLineArgs()); // As soon as we obtain command line arguments, we immediately convert them into convenient to work with List<string>.
        public static long startupTimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();


        public static readonly CultureInfo culture = startupArguments.FirstOrDefault(arg => arg.StartsWith("-lang=")) != null && startupArguments.FirstOrDefault(arg => arg.StartsWith("-lang=")).Length == 8 // Lookup if application was started with "-lang=" argument, use current Windows UI language if it wasn't
        ? new CultureInfo(startupArguments.First(arg => arg.StartsWith("-lang=")).Substring(6))
        : CultureInfo.InstalledUICulture;


        public static readonly List<string> defaultWebHeaders = new List<string>() // Default set of headers to use in networking requests.
            {
                "User-Agent: Cursed Market Reborn",
                $"Application Version: {version}"
            };


        public static readonly string executableName = AppDomain.CurrentDomain.FriendlyName;            // "MyApplication.exe"
        public static readonly string executableFriendlyName = Process.GetCurrentProcess().ProcessName; // "MyApplication"


        public static readonly string executableDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;        // "C:\Program Files\MyFolder"
        public static readonly string executablePath = Path.Combine(executableDirectoryPath, executableName); // "C:\Program Files\MyFolder\MyApplication.exe"


        public static readonly string requirementsDirectoryPath = Path.Combine(executableDirectoryPath, "Requirements");
        public static readonly string customDirectoryPath = Path.Combine(executableDirectoryPath, "Custom");


        private static string dataFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cursed Market Reborn");
        public static string GetDataFolderPath()
        {
            try
            {
                if (!Directory.Exists(dataFolderPath))
                {
                    Directory.CreateDirectory(dataFolderPath);
                }

                return Directory.Exists(dataFolderPath) ? dataFolderPath : null;
            }
            catch
            {
                return null;
            }
        }


        public static bool GetIsFontInstalled(string fontName = "Roboto")
        {
            using (var font = new Font(fontName, 12, FontStyle.Regular, GraphicsUnit.Pixel))
            {
                return font.Name == fontName;
            }
        }


        public static class Local
        {
            public static string marketFilePath = Path.Combine(customDirectoryPath, "Market.json");
            public static string charactersPrestigeFilePath = Path.Combine(customDirectoryPath, "CharactersPrestige.json");
        }


        public static bool initialized = false;
        public static bool offlineMode = false;


        public static class Theme
        {
            public static class SE_Themes
            {
                public static string defaultTheme = "Default";
                public static string legacy = "Legacy";
                public static string darkMemories = "Dark Memories";
                public static string saintsRow = "Saints Row";
                public static string dracula = "Dracula";
                public static string christmas = "Christmas";
            }

            public static string selectedTheme = WinReg.GetData_SZ(WinReg.SE_CommonEntries.applicationTheme) ?? SE_Themes.darkMemories;
            public static int themeIndex = -1;
        }
    }
}
