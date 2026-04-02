using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;






namespace Cursed_Market
{
    public static class ProgramFeatures
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
            public static bool enabled = false;


            public static bool Obtain()
            {
                if (File.Exists(ProgramPaths.charactersPresetFilePath)) // Make sure that file we're looking for does exist in first place.
                {
                    string charactersPreset = File.ReadAllText(ProgramPaths.charactersPresetFilePath);
                    if (string.IsNullOrEmpty(charactersPreset) == false && charactersPreset.IsJson()) // Make sure that file isn't empty.
                    {
                        ProgramCache.Custom.charactersPreset = charactersPreset;
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
                    JObject characterDataJSON = JObject.Parse(ProgramCache.CursedAPI.CharacterData.data); // Whatever happens next relies on CharacterData - obtaining it is the very first thing we want to do.
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


                    JObject charactersPresetJSON = JObject.Parse(ProgramCache.Custom.charactersPreset);
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

                    ProgramCache.CursedAPI.CharacterData.Store(); // Create a backup of initial character data before overwriting the variable.
                    ProgramCache.CursedAPI.CharacterData.data = characterDataJSON.ToString();
                    CharactersPreset.enabled = true;
                }
                else
                {
                    CharacterData.characterDataMap.Clear(); // Custom Characters Preset is OFF, invalidate characters map we have built earlier.

                    ProgramCache.CursedAPI.CharacterData.Restore(); // Restore CharacterData variable to it's initial value.
                    CharactersPreset.enabled = false;
                }
            }
        }




        public static class CurrencySpoof
        {
            public static bool enabled = false;
            public static string bloodpointsAmount = "0";
            public static string iridescentShardsAmount = "0";
            public static string auricCellsAmount = "0";
        }




        public static class GuaranteedQuests
        {
            public static bool enabled = false;
        }




        public static class GameProfile
        {
            private static string storedPreset = WinReg.GetData_SZ(WinReg.Common.gameProfile) ?? null;
            public static string GetStoredPreset()
            {
                return storedPreset;
            }
            public static void SetStoredPreset(string newPreset)
            {
                if (WinReg.SetData_SZ(WinReg.Common.gameProfile, newPreset))
                    storedPreset = newPreset;
            }
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


            private static E_Crosshairs selectedCrosshair = (E_Crosshairs)WinReg.GetData_DWORD(WinReg.Common.crosshair, (int)E_Crosshairs.none);
            public static E_Crosshairs GetSelectedCrosshair()
            {
                return selectedCrosshair;
            }
            public static void SetSelectedCrosshair(E_Crosshairs newCrosshair)
            {
                if (WinReg.SetData_DWORD(WinReg.Common.crosshair, (int)newCrosshair))
                    selectedCrosshair = newCrosshair;
            }


            private static int opacity = WinReg.GetData_DWORD(WinReg.Common.crosshairOpacity, 100);
            public static int GetOpacity()
            {
                return opacity;
            }
            public static void SetOpacity(int newOpacity)
            {
                if (WinReg.SetData_DWORD(WinReg.Common.crosshairOpacity, newOpacity))
                    opacity = newOpacity;
            }




            public static void Show()
            {
                ProgramCache.Forms.Crosshair.Show();
            }
            public static void Hide()
            {
                ProgramCache.Forms.Crosshair.Hide();
            }
            public static void Reload()
            {
                ProgramCache.Forms.Crosshair.ForceInitializeSettings();
            }
        }




        public static class QueueNotify
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


            private static E_NotifySounds selectedNotifySound = (E_NotifySounds)WinReg.GetData_DWORD(WinReg.Common.queueNotifySound, (int)E_NotifySounds.none);
            public static E_NotifySounds GetSelectedNotifySound()
            {
                return selectedNotifySound;
            }
            public static void SetSelectedNotifySound(E_NotifySounds newNotifySound)
            {
                selectedNotifySound = newNotifySound;
            }
        }
    }
}
