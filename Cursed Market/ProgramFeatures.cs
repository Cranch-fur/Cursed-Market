using Newtonsoft.Json.Linq;
using System;
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
                if (File.Exists(ProgramPaths.charactersPresetFilePath)) // Make sure that the file we're looking for exists in the first place.
                {
                    string charactersPreset = File.ReadAllText(ProgramPaths.charactersPresetFilePath);
                    if (!string.IsNullOrEmpty(charactersPreset) && charactersPreset.IsJson()) // Make sure that the file isn't empty and contains valid JSON.
                    {
                        ProgramCache.Custom.charactersPreset = charactersPreset;
                        return true;
                    }
                }
                return false;
            }

            public static void SetEnabled(bool enable)
            {
                if (enable)
                {
                    if (string.IsNullOrEmpty(ProgramCache.CursedAPI.CharacterData.data) || string.IsNullOrEmpty(ProgramCache.Custom.charactersPreset))
                        return;

                    /* If already enabled, restore original data first so we don't overwrite the backup with modified data. */
                    if (CharactersPreset.enabled)
                    {
                        ProgramCache.CursedAPI.CharacterData.Restore();
                        CharacterData.characterDataMap.Clear();
                    }

                    JObject characterDataJSON = JObject.Parse(ProgramCache.CursedAPI.CharacterData.data); // Whatever happens next relies on CharacterData - obtaining it is the very first thing we want to do.
                    if (CharacterData.characterDataMap.Count == 0) // Each time custom characters prestige is enabled, we need to build a new charactersMap if it's not built yet.
                    {
                        int count = 0;
                        JArray charactersListArray = (JArray)characterDataJSON["list"];

                        if (charactersListArray != null)
                        {
                            foreach (JToken characterEntry in charactersListArray)
                            {
                                string characterName = (string)characterEntry["characterName"];
                                int characterBloodWebLevel = (int)characterEntry["bloodWebLevel"];
                                int characterPrestigeLevel = (int)characterEntry["prestigeLevel"];
                                int characterLegacyPrestigeLevel = (int)characterEntry["legacyPrestigeLevel"];

                                CharacterData.characterDataMap[characterName] = new CharacterData.S_CharacterData(count, characterBloodWebLevel, characterPrestigeLevel, characterLegacyPrestigeLevel);
                                count++;
                            }
                        }
                    }

                    JObject charactersPresetJSON = JObject.Parse(ProgramCache.Custom.charactersPreset);
                    foreach (var character in charactersPresetJSON)
                    {
                        string characterName = character.Key;
                        if (CharacterData.characterDataMap.TryGetValue(characterName, out CharacterData.S_CharacterData charData))
                        {
                            int characterIndex = charData.index;
                            int characterBloodWebLevel = charData.bloodWebLevel;
                            int characterPrestigeLevel = charData.prestigeLevel;
                            int characterLegacyPrestigeLevel = charData.legacyPrestigeLevel;

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

                            characterDataJSON["list"][characterIndex]["bloodWebLevel"] = characterBloodWebLevel;
                            characterDataJSON["list"][characterIndex]["prestigeLevel"] = characterPrestigeLevel;
                            characterDataJSON["list"][characterIndex]["legacyPrestigeLevel"] = characterLegacyPrestigeLevel;

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

                    if (CharactersPreset.enabled)
                    {
                        ProgramCache.CursedAPI.CharacterData.Restore(); // Restore CharacterData variable to its initial value.
                    }
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
            private static string storedPreset = WinReg.GetData_SZ(WinReg.Common.gameProfile);
            public static string GetStoredPreset() => storedPreset;
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
                customizable,
                circleAqua,
                circleWhite,
                dotAqua,
                dotGreen,
                dotRed,
                dotYellow,
                tacticAqua,
                tacticWhite,
                custom01,
                custom02,
                custom03,
            }

            public static CrosshairGenerator.S_CrosshairSettings Settings = LoadInitialSettings();

            private static CrosshairGenerator.S_CrosshairSettings LoadInitialSettings()
            {
                string savedCode = WinReg.GetData_SZ(WinReg.Common.crosshairCode);
                if (!string.IsNullOrEmpty(savedCode) && CrosshairGenerator.TryParseShareCode(savedCode, out var parsedFromCode))
                {
                    return parsedFromCode;
                }

                return CrosshairGenerator.S_CrosshairSettings.Default;
            }

            public static void SaveSettings()
            {
                string shareCode = CrosshairGenerator.EncodeShareCode(Settings);
                WinReg.SetData_SZ(WinReg.Common.crosshairCode, shareCode);
            }

            private static E_Crosshairs LoadInitialSelectedCrosshair()
            {
                int rawValue = WinReg.GetData_DWORD(WinReg.Common.crosshair, (int)E_Crosshairs.none);
                return Enum.IsDefined(typeof(E_Crosshairs), rawValue)
                    ? (E_Crosshairs)rawValue
                    : E_Crosshairs.none;
            }

            private static E_Crosshairs selectedCrosshair = LoadInitialSelectedCrosshair();
            public static E_Crosshairs GetSelectedCrosshair() => selectedCrosshair;
            public static void SetSelectedCrosshair(E_Crosshairs newCrosshair)
            {
                if (WinReg.SetData_DWORD(WinReg.Common.crosshair, (int)newCrosshair))
                    selectedCrosshair = newCrosshair;
            }

            private static int opacity = WinReg.GetData_DWORD(WinReg.Common.crosshairOpacity, 100);
            public static int GetOpacity() => opacity;
            public static void SetOpacity(int newOpacity)
            {
                if (WinReg.SetData_DWORD(WinReg.Common.crosshairOpacity, newOpacity))
                    opacity = newOpacity;
            }

            public static void Show() => ProgramCache.Forms.Crosshair.Show();
            public static void Hide() => ProgramCache.Forms.Crosshair.Hide();
            public static void Reload() => ProgramCache.Forms.Crosshair.ForceInitializeSettings();
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
            public static E_NotifySounds GetSelectedNotifySound() => selectedNotifySound;
            public static void SetSelectedNotifySound(E_NotifySounds newNotifySound)
            {
                if (WinReg.SetData_DWORD(WinReg.Common.queueNotifySound, (int)newNotifySound))
                    selectedNotifySound = newNotifySound;
            }
        }
    }
}