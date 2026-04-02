using System;
using System.Diagnostics;
using System.IO;






namespace Cursed_Market
{
    public static class ProgramPaths
    {
        public static readonly string executableName = AppDomain.CurrentDomain.FriendlyName;            // "MyApplication.exe"
        public static readonly string executableFriendlyName = Process.GetCurrentProcess().ProcessName; // "MyApplication"

        public static readonly string executableDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;        // "C:\Program Files\MyFolder"
        public static readonly string executablePath = Path.Combine(executableDirectoryPath, executableName); // "C:\Program Files\MyFolder\MyApplication.exe"

        public static readonly string requirementsDirectoryPath = Path.Combine(executableDirectoryPath, "Requirements");
        public static readonly string fontFilePath = Path.Combine(requirementsDirectoryPath, "Roboto-Regular.ttf");

        public static readonly string charactersPresetDirectoryPath = Path.Combine(executableDirectoryPath, "Characters Preset");
        public static readonly string charactersMapFilePath = Path.Combine(charactersPresetDirectoryPath, "CharactersMap.json");
        public static readonly string charactersPresetFilePath = Path.Combine(charactersPresetDirectoryPath, "CharactersPreset.json");
        public static readonly string charactersPortraitsDirectory = Path.Combine(charactersPresetDirectoryPath, "CharPortraits");

        public static readonly string crosshairsDirectoryPath = Path.Combine(executableDirectoryPath, "Crosshairs");
        public static readonly string customCrosshair01FilePath = Path.Combine(crosshairsDirectoryPath, "Crosshair 01.png");
        public static readonly string customCrosshair02FilePath = Path.Combine(crosshairsDirectoryPath, "Crosshair 02.png");
        public static readonly string customCrosshair03FilePath = Path.Combine(crosshairsDirectoryPath, "Crosshair 03.png");
        public static readonly string customCrosshair04FilePath = Path.Combine(crosshairsDirectoryPath, "Crosshair 04.png");
        public static readonly string customCrosshair05FilePath = Path.Combine(crosshairsDirectoryPath, "Crosshair 05.png");
        public static readonly string customCrosshair06FilePath = Path.Combine(crosshairsDirectoryPath, "Crosshair 06.png");
        public static readonly string customCrosshair07FilePath = Path.Combine(crosshairsDirectoryPath, "Crosshair 07.png");
        public static readonly string customCrosshair08FilePath = Path.Combine(crosshairsDirectoryPath, "Crosshair 08.png");
        public static readonly string customCrosshair09FilePath = Path.Combine(crosshairsDirectoryPath, "Crosshair 09.png");
        public static readonly string customCrosshair10FilePath = Path.Combine(crosshairsDirectoryPath, "Crosshair 10.png");
        public static readonly string customCrosshair11FilePath = Path.Combine(crosshairsDirectoryPath, "Crosshair 11.png");
        public static readonly string customCrosshair12FilePath = Path.Combine(crosshairsDirectoryPath, "Crosshair 12.png");
        public static readonly string customCrosshair13FilePath = Path.Combine(crosshairsDirectoryPath, "Crosshair 13.png");
        public static readonly string customCrosshair14FilePath = Path.Combine(crosshairsDirectoryPath, "Crosshair 14.png");
        public static readonly string customCrosshair15FilePath = Path.Combine(crosshairsDirectoryPath, "Crosshair 15.png");
        public static readonly string customCrosshair16FilePath = Path.Combine(crosshairsDirectoryPath, "Crosshair 16.png");

        public static readonly string extraLogicDirectoryPath = Path.Combine(executableDirectoryPath, "Extra Logic");
        public static readonly string extraLogicBeforeRequestFilePath = Path.Combine(extraLogicDirectoryPath, "BeforeRequest.csx");
        public static readonly string extraLogicBeforeResponseFilePath = Path.Combine(extraLogicDirectoryPath, "BeforeResponse.csx");
        public static readonly string extraLogicAfterSessionCompleteFilePath = Path.Combine(extraLogicDirectoryPath, "AfterSessionComplete.csx");


        private static readonly string dataFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cursed Market");
        public static string GetDataFolderPath()
        {
            try
            {
                if (Directory.Exists(dataFolderPath) == false)
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
        public static readonly string networkingCertificateFilePath = $"{GetDataFolderPath()}\\Cursed Market Root Certificate.p12";
        public static readonly string networkingPasswordFilePath = $"{GetDataFolderPath()}\\Cursed Market Root Certificate Password.txt";

        public static readonly string contentCacheDirectoryPath = Path.Combine(ProgramPaths.Windows.localAppDataDirectoryPath, @"DeadByDaylight\Saved\PersistentDownloadDir\RemoteContentCache");




        public static class Windows
        {
            public static readonly string downloadsDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            public static readonly string localAppDataDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }
    }
}
