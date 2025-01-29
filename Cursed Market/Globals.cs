using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Cursed_Market
{
    public static class Globals
    {
        public static class Directories
        {
            public static class Windows
            {
                public static readonly string downloadsDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            }
        }
        public static class Application
        {
            public static long startupTimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();




            public static class SE_CommonStartupArguments
            {
                public static readonly string languageOverride     = "-lang=";
                public static readonly string offlineMode          = "-offline";
                public static readonly string noCustomizationsKing = "-noCustomizationsKing";
                public static readonly string noAntiKillSwitch     = "-noAntiKillSwitch";
                public static readonly string noCharacterData      = "-noCharacterData";

            }
            public static readonly List<string> startupArguments = new List<string>(Environment.GetCommandLineArgs()); // As soon as we obtain command line arguments, we immediately convert them into convenient to work with List<string>.
            public static bool HasStartupArgument(string startupArgument)
            {
                return startupArguments.Contains(startupArgument);
            }




            public static readonly CultureInfo culture = startupArguments.FirstOrDefault(arg => arg.StartsWith(SE_CommonStartupArguments.languageOverride)) != null && startupArguments.FirstOrDefault(arg => arg.StartsWith(SE_CommonStartupArguments.languageOverride)).Length == 8 // Lookup if application was started with "-lang=" argument, use current Windows UI language if it wasn't
            ? new CultureInfo(startupArguments.First(arg => arg.StartsWith(SE_CommonStartupArguments.languageOverride)).Substring(6))
            : CultureInfo.InstalledUICulture;




            public static readonly string executableName = AppDomain.CurrentDomain.FriendlyName;            // "MyApplication.exe"
            public static readonly string executableFriendlyName = Process.GetCurrentProcess().ProcessName; // "MyApplication"


            public static readonly string executableDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;        // "C:\Program Files\MyFolder"
            public static readonly string executablePath = Path.Combine(executableDirectoryPath, executableName); // "C:\Program Files\MyFolder\MyApplication.exe"


            public static readonly string requirementsDirectoryPath = Path.Combine(executableDirectoryPath, "Requirements");
            public static readonly string charactersPresetDirectoryPath = Path.Combine(executableDirectoryPath, "Characters Preset");
            public static readonly string crosshairsDirectoryPath = Path.Combine(executableDirectoryPath, "Crosshairs");




            private static string GetApplicationVersion()
            {
                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(executablePath);
                return versionInfo.FileVersion.Replace(".", string.Empty);
            }
            public static readonly string version = GetApplicationVersion();




            private static string dataFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cursed Market");
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


            

            public static class Requirements
            {
                public static bool GetIsFontInstalled()
                {
                    using (Font font = new Font("Roboto", 12, FontStyle.Regular, GraphicsUnit.Pixel))
                    {
                        return font.Name == "Roboto";
                    }
                }
                public static string robotoFontPath = Path.Combine(requirementsDirectoryPath, "Roboto-Regular.ttf");
            }
            



            public static class Networking
            {
                public static readonly List<string> defaultHeaders = new List<string>() // Default set of headers to use in networking requests.
                {
                    $"User-Agent: {executableFriendlyName}",
                    $"Application-Version: {version}",
                    $"Startup-Time: {startupTimeStamp}"
                };
            }




            public static void ReloadThemes()
            {
                if (Globals_Cache.Forms.Main.InvokeRequired)
                {
                    Globals_Cache.Forms.Main.Invoke(new Action(() =>
                        Globals_Cache.Forms.Main.ReloadTheme()));
                }
                else
                {
                    Globals_Cache.Forms.Main.ReloadTheme();
                }


                if (Globals_Cache.Forms.Settings.InvokeRequired)
                {
                    Globals_Cache.Forms.Settings.Invoke(new Action(() =>
                        Globals_Cache.Forms.Settings.ReloadTheme()));
                }
                else
                {
                    Globals_Cache.Forms.Settings.ReloadTheme();
                }


                if (Globals_Cache.Forms.Queue.InvokeRequired)
                {
                    Globals_Cache.Forms.Queue.Invoke(new Action(() =>
                        Globals_Cache.Forms.Queue.ReloadTheme()));
                }
                else
                {
                    Globals_Cache.Forms.Queue.ReloadTheme();
                }


                if (Globals_Cache.Forms.CloudIDFriend.InvokeRequired)
                {
                    Globals_Cache.Forms.CloudIDFriend.Invoke(new Action(() =>
                        Globals_Cache.Forms.CloudIDFriend.ReloadTheme()));
                }
                else
                {
                    Globals_Cache.Forms.CloudIDFriend.ReloadTheme();
                }


                if (Globals_Cache.Forms.CharactersPreset.InvokeRequired)
                {
                    Globals_Cache.Forms.CharactersPreset.Invoke(new Action(() =>
                        Globals_Cache.Forms.CharactersPreset.ReloadTheme()));
                }
                else
                {
                    Globals_Cache.Forms.CharactersPreset.ReloadTheme();
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
                Process.Start(executablePath);
                Close();

            }




            private static bool GetIsFirstLaunch()
            {
                return WinReg.GetData_DWORD(WinReg.SE_CommonEntries.firstLaunch) != 1;
            }
            public static void CheckForFirstLaunch()
            {
                if (GetIsFirstLaunch() == true)
                {
                    Messaging.ShowMessage(Properties.Localization.MESSAGE_FirstLaunch, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button1);
                    WinReg.SetData_DWORD(WinReg.SE_CommonEntries.firstLaunch, 1);
                }
            }




            public static bool initialized = false;
            public static bool offlineMode = false;




            public static class Theme
            {
                public enum E_Themes
                {
                    defaultTheme,
                    legacy,
                    darkMemories,
                    saintsRow,
                    dracula,
                    christmas
                }

                public static E_Themes selectedTheme = (E_Themes)WinReg.GetData_DWORD(WinReg.SE_CommonEntries.applicationTheme, (int)E_Themes.darkMemories);
            }
        }

        public static class Game
        {
            private static Process GetProcess()
            {
                Process[] processArray = Process.GetProcesses();
                if (processArray.Length > 0)
                {
                    foreach (Process process in processArray)
                    {
                        if (process.ProcessName.Contains("DeadByDaylight-"))
                            return process;
                    }
                }

                return null;
            }
            public static bool IsRunning()
            {
                Process gameProcess = GetProcess();
                return gameProcess.Responding;
            }
            public static bool Exit()
            {
                Process gameProcess = GetProcess();
                gameProcess.Kill();

                return gameProcess.HasExited;
            }
        }

        public static class Queue
        {
            public enum E_NotifySounds
            {
                none,
                gongHit,
                okLetsGo,
                NFLTheme,
                gayEcho,
                rizzMelody,
                runSong,
                pedroSong,
                militaryHorn
            }

            public static E_NotifySounds selectedNotifySound = (E_NotifySounds)WinReg.GetData_DWORD(WinReg.SE_CommonEntries.queueNotifySound, (int)E_NotifySounds.none);
        }
        public static class Crosshair
        {
            public enum E_Crosshairs
            {
                none,
                cs_nafany,
                cs_donk,
                cs_felps,
                circleAqua,
                circleWhite,
                dotAqua,
                dotRed,
                dotYellow,
                dotGreen,
                tacticAqua,
                tacticWhite,
                custom01,
                custom02,
                custom03,
                custom04,
                custom05,
                custom06,
                custom07,
                custom08,
                custom09,
                custom10,
                custom11,
                custom12,
                custom13,
                custom14,
                custom15,
                custom16,
            }


            public static E_Crosshairs selectedCrosshair = (E_Crosshairs)WinReg.GetData_DWORD(WinReg.SE_CommonEntries.crosshair, (int)E_Crosshairs.none);
            public static int opacity = WinReg.GetData_DWORD(WinReg.SE_CommonEntries.crosshairOpacity, 100);

            public static readonly string customCrosshair01FilePath = Path.Combine(Application.crosshairsDirectoryPath, "Crosshair 01.png");
            public static readonly string customCrosshair02FilePath = Path.Combine(Application.crosshairsDirectoryPath, "Crosshair 02.png");
            public static readonly string customCrosshair03FilePath = Path.Combine(Application.crosshairsDirectoryPath, "Crosshair 03.png");
            public static readonly string customCrosshair04FilePath = Path.Combine(Application.crosshairsDirectoryPath, "Crosshair 04.png");
            public static readonly string customCrosshair05FilePath = Path.Combine(Application.crosshairsDirectoryPath, "Crosshair 05.png");
            public static readonly string customCrosshair06FilePath = Path.Combine(Application.crosshairsDirectoryPath, "Crosshair 06.png");
            public static readonly string customCrosshair07FilePath = Path.Combine(Application.crosshairsDirectoryPath, "Crosshair 07.png");
            public static readonly string customCrosshair08FilePath = Path.Combine(Application.crosshairsDirectoryPath, "Crosshair 08.png");
            public static readonly string customCrosshair09FilePath = Path.Combine(Application.crosshairsDirectoryPath, "Crosshair 09.png");
            public static readonly string customCrosshair10FilePath = Path.Combine(Application.crosshairsDirectoryPath, "Crosshair 10.png");
            public static readonly string customCrosshair11FilePath = Path.Combine(Application.crosshairsDirectoryPath, "Crosshair 11.png");
            public static readonly string customCrosshair12FilePath = Path.Combine(Application.crosshairsDirectoryPath, "Crosshair 12.png");
            public static readonly string customCrosshair13FilePath = Path.Combine(Application.crosshairsDirectoryPath, "Crosshair 13.png");
            public static readonly string customCrosshair14FilePath = Path.Combine(Application.crosshairsDirectoryPath, "Crosshair 14.png");
            public static readonly string customCrosshair15FilePath = Path.Combine(Application.crosshairsDirectoryPath, "Crosshair 15.png");
            public static readonly string customCrosshair16FilePath = Path.Combine(Application.crosshairsDirectoryPath, "Crosshair 16.png");
        }

        public static class GameProfile
        {
            public static string selectedPreset = WinReg.GetData_SZ(WinReg.SE_CommonEntries.gameProfile) ?? null;
        }

        public static class CharacterData
        {
            public struct S_CharacterData
            {
                public int index;
                public int bloodWebLevel;
                public int prestigeLevel;
                public int legacyPrestigeLevel;

                public S_CharacterData(int index, int bloodWebLevel, int prestigeLevel, int legacyPrestigeLevel)
                {
                    this.index = index;
                    this.bloodWebLevel = bloodWebLevel;
                    this.prestigeLevel = prestigeLevel;
                    this.legacyPrestigeLevel = legacyPrestigeLevel;
                }
            }


            public static Dictionary<string, S_CharacterData> characterDataMap = new Dictionary<string, S_CharacterData>();
        }

        public static class CharactersPreset
        {
            public static string charactersMapFilePath = Path.Combine(Application.charactersPresetDirectoryPath, "CharactersMap.json");
            public static string charactersPresetFilePath = Path.Combine(Application.charactersPresetDirectoryPath, "CharactersPreset.json");
            public static string charactersPortraitsDirectory = Path.Combine(Application.charactersPresetDirectoryPath, "CharPortraits");


            public static bool Obtain()
            {
                if (File.Exists(charactersPresetFilePath)) // Make sure that file we're looking for does exist in first place.
                {
                    string charactersPreset = File.ReadAllText(charactersPresetFilePath);
                    if (string.IsNullOrEmpty(charactersPreset) == false && charactersPreset.IsJson()) // Make sure that file isn't empty.
                    {
                        Globals_Cache.Custom.charactersPreset = charactersPreset;
                        return true;
                    }
                    else
                        return false;
                }
                else 
                    return false;
            }


            public static void SetEnabled(bool enabled)
            {
                if (enabled == true)
                {
                    JObject characterDataJSON = JObject.Parse(Globals_Cache.CursedAPI.CharacterData.data); // Whatever happens next relies on CharacterData - obtaining it is the very first thing we want to do.
                    if (CharacterData.characterDataMap.Count == 0) // Each time custom characters prestige is enabled, we need to build a new charactersMap if it's not build yet.
                    {
                        int count = 0;
                        JArray charactersListArray = (JArray)characterDataJSON["list"];

                        foreach (JToken characterEntry in charactersListArray)
                        {
                            string characterName = (string)characterEntry["characterName"];
                            int characterBloodWebLevel = (int)characterEntry["bloodWebLevel"];
                            int characterPrestigeLevel = (int)characterEntry["prestigeLevel"];
                            int characterLegacyPrestigeLevel = (int)characterEntry["legacyPrestigeLevel"];

                            CharacterData.characterDataMap.Add(characterName, new CharacterData.S_CharacterData(count, characterBloodWebLevel, characterPrestigeLevel, characterLegacyPrestigeLevel));

                            count++;
                        }
                    }


                    JObject charactersPresetJSON = JObject.Parse(Globals_Cache.Custom.charactersPreset);
                    foreach (var character in charactersPresetJSON)
                    {
                        string characterName = character.Key;
                        if (CharacterData.characterDataMap.ContainsKey(characterName))
                        {
                            int characterIndex = CharacterData.characterDataMap[characterName].index;
                            int characterBloodWebLevel = CharacterData.characterDataMap[characterName].bloodWebLevel;
                            int characterPrestigeLevel = CharacterData.characterDataMap[characterName].prestigeLevel;
                            int characterLegacyPrestigeLevel = CharacterData.characterDataMap[characterName].legacyPrestigeLevel;


                            JToken charBloodWebToken = character.Value["bloodWebLevel"]; // Check if we have this value set in characters prestige file and that its type is Integer.
                            if (charBloodWebToken != null && charBloodWebToken.Type == JTokenType.Integer)
                            {
                                characterBloodWebLevel = (int)charBloodWebToken; // Override default bloodWebLevel with custom value.
                            }

                            JToken charPrestigeToken = character.Value["prestigeLevel"]; // Check if we have this value set in characters prestige file and that its type is Integer.
                            if (charPrestigeToken != null && charPrestigeToken.Type == JTokenType.Integer)
                            {
                                characterPrestigeLevel = (int)charPrestigeToken; // Override default prestigeLevel with custom value.
                            }

                            JToken charLegacyPrestigeToken = character.Value["legacyPrestigeLevel"]; // Check if we have this value set in characters prestige file and that its type is Integer.
                            if (charLegacyPrestigeToken != null && charLegacyPrestigeToken.Type == JTokenType.Integer)
                            {
                                characterLegacyPrestigeLevel = (int)charLegacyPrestigeToken; // Override default legacyPrestigeLevel with custom value.
                            }


                            if (CharacterData.characterDataMap.ContainsKey(characterName))
                            {
                                characterDataJSON["list"][CharacterData.characterDataMap[characterName].index]["bloodWebLevel"] = characterBloodWebLevel;
                                characterDataJSON["list"][CharacterData.characterDataMap[characterName].index]["prestigeLevel"] = characterPrestigeLevel;
                                characterDataJSON["list"][CharacterData.characterDataMap[characterName].index]["legacyPrestigeLevel"] = characterLegacyPrestigeLevel;
                            }

                            CharacterData.characterDataMap[characterName] = new CharacterData.S_CharacterData(characterIndex, characterBloodWebLevel, characterPrestigeLevel, characterLegacyPrestigeLevel);
                        }
                    }

                    Globals_Cache.CursedAPI.CharacterData.Store(); // Create a backup of initial character data before overwriting the variable.
                    Globals_Cache.CursedAPI.CharacterData.data = characterDataJSON.ToString();
                    FiddlerCoreTunables.CharactersPreset.enabled = true;
                }
                else
                {
                    CharacterData.characterDataMap.Clear(); // Custom Characters Preset is OFF, invalidate characters map we have built earlier.

                    Globals_Cache.CursedAPI.CharacterData.Restore(); // Restore CharacterData variable to it's initial value.
                    FiddlerCoreTunables.CharactersPreset.enabled = false;
                }
            }
        }

        public static class FiddlerCoreTunables
        {
            public static class Catalog
            {
                public static bool enabled = false;
            }

            public static class AntiKillSwitch
            {
                public static bool enabled = false;
            }

            public static class CharacterData
            {
                public static bool enabled = false;
            }

            public static class CharactersPreset
            {
                public static bool enabled = false;
            }

            public static class CurrencySpoof
            {
                public static bool enabled                  = false;
                public static string bloodpointsAmount      = "0";
                public static string iridescentShardsAmount = "0";
                public static string auricCellsAmount       = "0";
            }

            public static class GuaranteedQuests
            {
                public static bool enabled = false;
            }
        }


        public static class GameAuth
        {
            public static void ResolveUserID(string gameAuthResponse)
            {
                if (gameAuthResponse.IsJson() == true)
                {
                    JObject json = JObject.Parse(gameAuthResponse);
                    if (json.ContainsKey("userId") == true)
                    {
                        Globals_Session.Game.userId = (string)json["userId"];
                    }
                }
            }
        }




        public static void SetGameChangerStatus(CursedAPI.E_GameChangers gameChanger, CursedAPI.E_GameChangerStatus newStatus)
        {
            if (Globals_Cache.Forms.Main.InvokeRequired)
            {
                Globals_Cache.Forms.Main.Invoke(new Action(() =>
                    Globals_Cache.Forms.Main.SetGameChangerStatus(gameChanger, newStatus)));
            }
            else
            {
                Globals_Cache.Forms.Main.SetGameChangerStatus(gameChanger, newStatus);
            }
        }
        public static void SetMainFunctionEnabled(CursedAPI.E_MainFunctions mainFunction, bool newStatus)
        {
            if (Globals_Cache.Forms.Main.InvokeRequired)
            {
                Globals_Cache.Forms.Main.Invoke(new Action(() =>
                    Globals_Cache.Forms.Main.SetMainFunctionEnabled(mainFunction, newStatus)));
            }
            else
            {
                Globals_Cache.Forms.Main.SetMainFunctionEnabled(mainFunction, newStatus);
            }
        }
    }
}