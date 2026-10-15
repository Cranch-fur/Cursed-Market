using CranchyLib.Networking;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;






namespace Cursed_Market
{
    public static class CursedAPI
    {
        public enum E_GameChangers
        {
            customizationsKing,
            antiKillSwitch
        }
        public enum E_GameChangerStatus
        {
            Enabled,
            Failed,
            Disabled,
            Offline
        }


        public enum E_ActionMode
        {
            Online,
            Offline,
            Local
        }
        private enum E_ExtraActions
        {
            MessageBox,
            NotifyIcon
        }


        public enum E_MainFunctions
        {
            MainFunction_01,
            MainFunction_02,
            MainFunction_03,
            MainFunction_04,
            MainFunction_05,
            MainFunction_06,
            MainFunction_07
        }
        private static readonly Dictionary<string, E_ExtraActions> extraActionsMappings = new Dictionary<string, E_ExtraActions>
        {
            { "MessageBox",   E_ExtraActions.MessageBox  },
            { "NotifyIcon",   E_ExtraActions.NotifyIcon  }
        };




        public enum E_MarketFilePopulationType
        {
            None,
            Perks,
            Items,
            Addons,
            Offerings,
            All
        }




        public static class SE_CommonEndpoints
        {
            public static class Defaults
            {
                public const string Host = "api.cranchpalace.info";
                public const string Status = "/status";
                public const string Update = "/cm/update";
                public const string MarketFull = "/cm/static/marketFull.json";
                public const string MarketDLC = "/cm/static/marketDLC.json";
                public const string CharacterData = "/cm/static/characterData.json";
                public const string BloodwebData = "/cm/static/bloodwebData.json";
                public const string CharactersList = "/cm/static/charactersList.json";
                public const string ItemsList = "/cm/static/itemsList.json";
                public const string Catalog = "/cm/static/catalog.json";
                public const string AntiKillSwitch = "/cm/static/killSwitch.json";
                public const string Font = "/cm/data/Roboto-Regular.ttf";
            }

            public static string Host => WinReg.GetData_SZ(WinReg.API.host, Defaults.Host);

            public static string status => $"https://{Host}{WinReg.GetData_SZ(WinReg.API.pathStatus, Defaults.Status)}";
            public static string update => $"https://{Host}{WinReg.GetData_SZ(WinReg.API.pathUpdate, Defaults.Update)}";
            public static string marketFull => $"https://{Host}{WinReg.GetData_SZ(WinReg.API.pathMarketFull, Defaults.MarketFull)}";
            public static string marketDLC => $"https://{Host}{WinReg.GetData_SZ(WinReg.API.pathMarketDLC, Defaults.MarketDLC)}";
            public static string characterData => $"https://{Host}{WinReg.GetData_SZ(WinReg.API.pathCharacterData, Defaults.CharacterData)}";
            public static string bloodwebData => $"https://{Host}{WinReg.GetData_SZ(WinReg.API.pathBloodwebData, Defaults.BloodwebData)}";
            public static string charactersList => $"https://{Host}{WinReg.GetData_SZ(WinReg.API.pathCharactersList, Defaults.CharactersList)}";
            public static string itemsList => $"https://{Host}{WinReg.GetData_SZ(WinReg.API.pathItemsList, Defaults.ItemsList)}";
            public static string catalog => $"https://{Host}{WinReg.GetData_SZ(WinReg.API.pathCatalog, Defaults.Catalog)}";
            public static string killSwitch => $"https://{Host}{WinReg.GetData_SZ(WinReg.API.pathAntiKillSwitch, Defaults.AntiKillSwitch)}";
            public static string font => $"https://{Host}{WinReg.GetData_SZ(WinReg.API.pathFont, Defaults.Font)}";
        }




        public static class ResponseFiles
        {
            public static string market         = null;
            public static string catalog        = null;
            public static string killSwitch = null;
        }

        public static class KillSwitch
        {
            public static bool isCatalogDisabled = false;
            public static bool isAntiKillSwitchDisabled = false;
        }

        public static class SocialLinks
        {
            public static string discord  = null;
            public static string telegram = null;
            public static string boosty   = null;
        }




        public static bool Status()
        {
            try
            {
                var statusResponse = Networking.Get(SE_CommonEndpoints.status, ProgramNetworking.defaultHeaders);
                return statusResponse.statusCode == Networking.E_StatusCode.OK && statusResponse.content == "OK";
            }
            catch
            {
                return false;
            }
        }


        public static void Update()
        {
            var updateResponse = Networking.Get(SE_CommonEndpoints.update, ProgramNetworking.defaultHeaders);
            if (updateResponse.statusCode != Networking.E_StatusCode.OK)
            {
                Messaging.ShowMessage($"Cursed Market failed to check for updates!\nSTATUS CODE: {updateResponse.statusCode}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (updateResponse.content.IsJson() == false)
            {
                Messaging.ShowMessage("Cursed Market failed to check for updates!\n\nGiven by web server response has unexpected format and couldn't be parsed as JSON.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            JObject updateJson = JObject.Parse(updateResponse.content);
            bool updateRequired = (bool)(updateJson["updateRequired"] ?? false);

            if (updateRequired)
            {
                string currentVersion = ProgramGlobals.version;
                string latestVersion = (string)updateJson["latestVersion"];
                string updateDownloadUrl = (string)updateJson["updateDownloadUrl"];
                string downloadDestinationPath = ProgramPaths.Windows.downloadsDirectoryPath;

                if (Messaging.ShowDialog($"Cursed Market update is available for download!\nCurrent Version: {currentVersion}\nLatest Version: {latestVersion}\n\nDo you want to download updated client now?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    var updateDownloadRequest = Networking.Download(updateDownloadUrl, ProgramNetworking.defaultHeaders, downloadDestinationPath);
                    if (File.Exists(updateDownloadRequest.content) == true)
                    {
                        Process.Start(new ProcessStartInfo(updateDownloadRequest.content) { UseShellExecute = true });
                        ProgramGlobals.Close();
                    }
                    else
                    {
                        Messaging.ShowMessage(updateDownloadRequest.content);
                        if (Messaging.ShowDialog("Cursed Market failed to download update directly!\n\nShould we try alternative method instead?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Process.Start(new ProcessStartInfo(updateDownloadUrl) { UseShellExecute = true });
                            ProgramGlobals.Close();
                        }
                    }
                }
            }

            if (updateJson.ContainsKey("socialLinks"))
            {
                if (updateJson["socialLinks"]["discord"] != null)
                    SocialLinks.discord = (string)updateJson["socialLinks"]["discord"];

                if (updateJson["socialLinks"]["telegram"] != null)
                    SocialLinks.telegram = (string)updateJson["socialLinks"]["telegram"];

                if (updateJson["socialLinks"]["boosty"] != null)
                    SocialLinks.boosty = (string)updateJson["socialLinks"]["boosty"];
            }

            if (updateJson.ContainsKey("killSwitch"))
            {
                JToken killSwitch = updateJson["killSwitch"];


                if (killSwitch["platforms"] != null)
                {
                    JToken platforms = killSwitch["platforms"];
                    foreach (JObject platform in platforms)
                    {
                        string hostname = (string)platform["hostname"];

                        ProgramSession.Game.Platform.E_GamePlatform gamePlatform = ProgramSession.Game.Platform.ResolvePlatformFromHostName(hostname);
                        ProgramSession.Game.Platform.limitedPlatforms.Add(gamePlatform);
                    }
                }


                if (killSwitch["functions"] != null)
                {
                    JToken functions = killSwitch["functions"];
                    foreach (string function in functions)
                    {
                        if (Enum.TryParse(function, out E_MainFunctions mainFunction))
                        {
                            ProgramGlobals.SetMainFunctionEnabled(mainFunction, false);
                        }
                    }
                }


                JToken gameChangersToken = killSwitch["gameChangers"];
                if (gameChangersToken != null)
                {
                    if (gameChangersToken.Type == JTokenType.Object)
                    {
                        KillSwitch.isCatalogDisabled = (bool)(gameChangersToken["catalog"] ?? gameChangersToken["customizationsKing"] ?? false);
                        KillSwitch.isAntiKillSwitchDisabled = (bool)(gameChangersToken["killSwitch"] ?? gameChangersToken["antiKillSwitch"] ?? false);
                    }
                    else if (gameChangersToken.Type == JTokenType.Array)
                    {
                        foreach (string entry in gameChangersToken)
                        {
                            if (string.Equals(entry, "catalog", StringComparison.OrdinalIgnoreCase) ||
                                string.Equals(entry, "catalog.json", StringComparison.OrdinalIgnoreCase) ||
                                string.Equals(entry, nameof(E_GameChangers.customizationsKing), StringComparison.OrdinalIgnoreCase))
                            {
                                KillSwitch.isCatalogDisabled = true;
                            }
                            else if (string.Equals(entry, "killSwitch", StringComparison.OrdinalIgnoreCase) ||
                                     string.Equals(entry, "killSwitch.json", StringComparison.OrdinalIgnoreCase) ||
                                     string.Equals(entry, nameof(E_GameChangers.antiKillSwitch), StringComparison.OrdinalIgnoreCase))
                            {
                                KillSwitch.isAntiKillSwitchDisabled = true;
                            }
                        }
                    }

                    if (KillSwitch.isCatalogDisabled && KillSwitch.isAntiKillSwitchDisabled)
                    {
                        ProgramGlobals.SetMainFunctionEnabled(E_MainFunctions.MainFunction_05, false);
                    }
                }
            }

            if (updateJson.TryGetValue("extraActions", out var extraActions) && extraActions != null)
            {
                foreach (JObject action in extraActions)
                {
                    if (extraActionsMappings.TryGetValue((string)action["actionType"], out var actionType))
                    {
                        string actionPayload = (string)action["actionPayload"];
                        switch (actionType)
                        {
                            case E_ExtraActions.MessageBox:
                                Messaging.ShowMessage(actionPayload, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;

                            case E_ExtraActions.NotifyIcon:
                                Messaging.ShowNotify("Cursed Market News", actionPayload, Properties.Resources.Icon);
                                break;

                            default: // We need default case in order to convince compiler that all code scenarios lead to an return.
                                throw new Exception($"{actionType} was never designed to proceed through default!");

                        }
                    }
                }
            }
        }




        public static string GetMarketFile(E_ActionMode actionMode, bool dlcOnly = false)
        {
            switch (actionMode)
            {
                case E_ActionMode.Online:
                    /* Check if we already have requested data pre-cached in the memory. */
                    if (!dlcOnly && ProgramCache.CursedAPI.Market.full != null)
                        return ProgramCache.CursedAPI.Market.full;

                    if (dlcOnly && ProgramCache.CursedAPI.Market.DLC != null)
                        return ProgramCache.CursedAPI.Market.DLC;

                    /* Send web request for the Market File. */
                    var marketFileResponse = dlcOnly
                        ? Networking.Get(SE_CommonEndpoints.marketDLC, ProgramNetworking.defaultHeaders)   // Request DLC Market File if dlcOnly == true.
                        : Networking.Get(SE_CommonEndpoints.marketFull, ProgramNetworking.defaultHeaders); // Request Full Market File if dlcOnly == false.

                    if (marketFileResponse.statusCode != Networking.E_StatusCode.OK)
                    {
                        Messaging.ShowMessage($"Cursed Market failed to obtain Market File!\nSTATUS CODE: {marketFileResponse.statusCode}\n\nLocally stored, offline image will be used instead.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return GetMarketFile(E_ActionMode.Offline, dlcOnly);
                    }

                    /* Decompress server response and cache it in the memory. */
                    string decompressedResult = Compression.ZLIB.Decompress(marketFileResponse.content);
                    if (dlcOnly == true)
                        ProgramCache.CursedAPI.Market.DLC = decompressedResult;
                    else
                        ProgramCache.CursedAPI.Market.full = decompressedResult;

                    return decompressedResult;

                /* There's no unique scenario for Local mode, so Offline & Local both return the same thing.*/
                case E_ActionMode.Offline:
                case E_ActionMode.Local:
                    return dlcOnly
                      ? Compression.ZLIB.Decompress(Properties.OfflineAPI.MarketDLC)   // Return DLC Market File if dlcOnly == true.
                      : Compression.ZLIB.Decompress(Properties.OfflineAPI.MarketFull); // Return Full Market File if dlcOnly == true.

                default: // We need default case in order to convince compiler that all code scenarios lead to an return.
                    throw new Exception($"{actionMode} was never designed to proceed through default!");
            }
        }




        public static string GetCharacterData(E_ActionMode actionMode)
        {
            switch (actionMode)
            {
                case E_ActionMode.Online:

                    if (ProgramCache.CursedAPI.CharacterData.data != null)
                        return ProgramCache.CursedAPI.CharacterData.data;

                    /* Character Data is a large chunk of data, so we want to await response for up to 30 seconds (in case someone has slow internet connection). */
                    var characterDataResponse = Networking.Get(SE_CommonEndpoints.characterData, ProgramNetworking.defaultHeaders, null, 30);
                    if (characterDataResponse.statusCode != Networking.E_StatusCode.OK)
                    {
                        Messaging.ShowMessage($"Cursed Market failed to obtain Character Data!\nSTATUS CODE: {characterDataResponse.statusCode}\n\nLocally stored, offline image will be used instead.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return GetCharacterData(E_ActionMode.Offline);
                    }

                    return Compression.ZLIB.Decompress(characterDataResponse.content);

                /* There's no unique scenario for Local mode, so Offline & Local both return the same thing. */
                case E_ActionMode.Offline:
                case E_ActionMode.Local:
                    return Compression.ZLIB.Decompress(Properties.OfflineAPI.CharacterData);

                default: // We need default case in order to convince compiler that all code scenarios lead to an return.
                    throw new Exception($"{actionMode} was never designed to proceed through default!");
            }
        }
        public static string GetBloodWebData(E_ActionMode actionMode)
        {
            switch (actionMode)
            {
                case E_ActionMode.Online:

                    if (ProgramCache.CursedAPI.bloodWebData != null)
                        return ProgramCache.CursedAPI.bloodWebData;

                    var bloodWebDataResponse = Networking.Get(SE_CommonEndpoints.bloodwebData, ProgramNetworking.defaultHeaders);
                    if (bloodWebDataResponse.statusCode != Networking.E_StatusCode.OK)
                    {
                        Messaging.ShowMessage($"Cursed Market failed to obtain Bloodweb Data!\nSTATUS CODE: {bloodWebDataResponse.statusCode}\n\nLocally stored, offline image will be used instead.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return GetBloodWebData(E_ActionMode.Offline);
                    }

                    return Compression.ZLIB.Decompress(bloodWebDataResponse.content);

                /* There's no unique scenario for Local mode, so Offline & Local both return the same thing. */
                case E_ActionMode.Offline:
                case E_ActionMode.Local:
                    return Compression.ZLIB.Decompress(Properties.OfflineAPI.BloodWebData);

                default: // We need default case in order to convince compiler that all code scenarios lead to an return.
                    throw new Exception($"{actionMode} was never designed to proceed through default!");
            }
        }




        public static string GetCharactersList(E_ActionMode actionMode)
        {
            switch (actionMode)
            {
                case E_ActionMode.Online:

                    if (ProgramCache.CursedAPI.charactersList != null)
                        return ProgramCache.CursedAPI.charactersList;

                    var charactersListResponse = Networking.Get(SE_CommonEndpoints.charactersList, ProgramNetworking.defaultHeaders);
                    if (charactersListResponse.statusCode != Networking.E_StatusCode.OK)
                    {
                        Messaging.ShowMessage($"Cursed Market failed to obtain Characters List!\nSTATUS CODE: {charactersListResponse.statusCode}\n\nLocally stored, offline image will be used instead.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return GetCharactersList(E_ActionMode.Offline);
                    }

                    return Compression.ZLIB.Decompress(charactersListResponse.content);

                /* There's no unique scenario for Local mode, so Offline & Local both return the same thing. */
                case E_ActionMode.Offline:
                case E_ActionMode.Local:
                    return Compression.ZLIB.Decompress(Properties.OfflineAPI.CharactersList);

                default: // We need default case in order to convince compiler that all code scenarios lead to an return.
                    throw new Exception($"{actionMode} was never designed to proceed through default!");
            }
        }
        public static string GetItemsList(E_ActionMode actionMode)
        {
            switch (actionMode)
            {
                case E_ActionMode.Online:

                    if (ProgramCache.CursedAPI.itemsList != null)
                        return ProgramCache.CursedAPI.itemsList;

                    var itemsListResponse = Networking.Get(SE_CommonEndpoints.itemsList, ProgramNetworking.defaultHeaders);
                    if (itemsListResponse.statusCode != Networking.E_StatusCode.OK)
                    {
                        Messaging.ShowMessage($"Cursed Market failed to obtain Items List!\nSTATUS CODE: {itemsListResponse.statusCode}\n\nLocally stored, offline image will be used instead.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return GetItemsList(E_ActionMode.Offline);
                    }

                    return Compression.ZLIB.Decompress(itemsListResponse.content);

                /* There's no unique scenario for Local mode, so Offline & Local both return the same thing. */
                case E_ActionMode.Offline:
                case E_ActionMode.Local:
                    return Compression.ZLIB.Decompress(Properties.OfflineAPI.ItemsList);

                default: // We need default case in order to convince compiler that all code scenarios lead to an return.
                    throw new Exception($"{actionMode} was never designed to proceed through default!");
            }
        }




        public static void ObtainCatalog()
        {
            if (KillSwitch.isCatalogDisabled || ProgramStartupArguments.HasStartupArgument(ProgramStartupArguments.Common.noCustomizationsKing))
            {
                ResponseFiles.catalog = null;
                ProgramFeatures.Catalog.enabled = false;
                ProgramGlobals.SetGameChangerStatus(E_GameChangers.customizationsKing, E_GameChangerStatus.Disabled);
                return;
            }

            /* Catalog is a large chunk of data, so we want to await response for up to 30 seconds (in case someone has slow internet connection). */
            var catalogResponse = Networking.Get(SE_CommonEndpoints.catalog, ProgramNetworking.defaultHeaders, null, 30);
            if (catalogResponse.statusCode == Networking.E_StatusCode.OK)
            {
                ResponseFiles.catalog = catalogResponse.content;
                ProgramFeatures.Catalog.enabled = true;

                ProgramGlobals.SetGameChangerStatus(E_GameChangers.customizationsKing, E_GameChangerStatus.Enabled);
            }
            else
            {
                ProgramGlobals.SetGameChangerStatus(E_GameChangers.customizationsKing, E_GameChangerStatus.Failed);
            }
        }

        public static void ObtainKillSwitch()
        {
            if (KillSwitch.isAntiKillSwitchDisabled || ProgramStartupArguments.HasStartupArgument(ProgramStartupArguments.Common.noAntiKillSwitch))
            {
                ResponseFiles.killSwitch = null;
                ProgramFeatures.AntiKillSwitch.enabled = false;
                ProgramGlobals.SetGameChangerStatus(E_GameChangers.antiKillSwitch, E_GameChangerStatus.Disabled);
                return;
            }

            var antiKillSwitchResponse = Networking.Get(SE_CommonEndpoints.killSwitch, ProgramNetworking.defaultHeaders);
            if (antiKillSwitchResponse.statusCode == Networking.E_StatusCode.OK)
            {
                ProgramFeatures.AntiKillSwitch.enabled = true;
                ResponseFiles.killSwitch = antiKillSwitchResponse.content;

                ProgramGlobals.SetGameChangerStatus(E_GameChangers.antiKillSwitch, E_GameChangerStatus.Enabled);
            }
            else
            {
                ProgramGlobals.SetGameChangerStatus(E_GameChangers.antiKillSwitch, E_GameChangerStatus.Failed);
            }
        }




        public static string GetPopulatedMarketFile(string actualMarketFile, E_MarketFilePopulationType populationType, bool unlockAllCharacters = false)
        {
            JArray charactersArray = JArray.Parse(ProgramCache.CursedAPI.charactersList);
            JObject actualMarketFileJSON = JObject.Parse(actualMarketFile);
            JObject modifiedMarketFileJSON = JObject.Parse(ResponseFiles.market);

            JArray modifiedMarketItems = (JArray)modifiedMarketFileJSON["inventoryItems"];

            /* Get ID's of items player actually own. */
            HashSet<string> actualItemsList = new HashSet<string>(actualMarketFileJSON["inventoryItems"].Select(item => (string)item["objectId"]));

            /* Convert characters list to HashSet for faster search. */
            HashSet<string> allKnownCharacters = new HashSet<string>(charactersArray.Select(c => (string)c));

            if (!unlockAllCharacters)
            {
                /* Remove all characters player doesn't own. */
                for (int i = modifiedMarketItems.Count - 1; i >= 0; i--)
                {
                    string objId = (string)modifiedMarketItems[i]["objectId"];

                    /* If element is a character and player doesn't own it - remove. */
                    if (allKnownCharacters.Contains(objId) && !actualItemsList.Contains(objId))
                    {
                        modifiedMarketItems.RemoveAt(i);
                    }
                }

                /* Add characters that player own yet they've been missing from Market file. */
                HashSet<string> modifiedItemsList = new HashSet<string>(modifiedMarketItems.Select(item => (string)item["objectId"]));
                foreach (string characterId in allKnownCharacters)
                {
                    if (actualItemsList.Contains(characterId) && !modifiedItemsList.Contains(characterId))
                    {
                        modifiedMarketItems.Add(new JObject
                        {
                            ["objectId"] = characterId,
                            ["quantity"] = 1
                        });
                    }
                }
            }

            if (populationType != E_MarketFilePopulationType.None)
            {
                JArray inventory = (JArray)modifiedMarketFileJSON["inventoryItems"];
                JObject itemsList = JObject.Parse(ProgramCache.CursedAPI.itemsList);

                Random rnd = new Random();
                long startupTimeStamp = ProgramGlobals.startupTimeStamp;

                /* Mapping types to property names. */
                var propertyMapping = new Dictionary<E_MarketFilePopulationType, string>
                {
                    { E_MarketFilePopulationType.Perks, "perks" },
                    { E_MarketFilePopulationType.Items, "items" },
                    { E_MarketFilePopulationType.Addons, "addons" },
                    { E_MarketFilePopulationType.Offerings, "offerings" }
                };

                IEnumerable<string> propertiesToProcess = populationType == E_MarketFilePopulationType.All ? propertyMapping.Values : propertyMapping.Where(p => p.Key == populationType).Select(p => p.Value);

                foreach (var property in propertiesToProcess)
                {
                    JArray propertyContentArray = (JArray)itemsList[property];
                    foreach (JToken content in propertyContentArray)
                    {
                        inventory.Add(new JObject(
                            new JProperty("lastUpdateAt", rnd.NextLong(startupTimeStamp - 300, startupTimeStamp + 300)),
                            new JProperty("objectId", content),
                            new JProperty("quantity", property == "perks" ? 3 : 50)
                        ));
                    }
                }
            }

            return modifiedMarketFileJSON.ToString();
        }




        public static string GetReasonableCharacterData(string actualCharacterData, bool unlockAllCharacters = false)
        {
            JObject actualCharacterDataJSON = JObject.Parse(actualCharacterData);
            JObject modifiedCharacterDataJSON = JObject.Parse(ProgramCache.CursedAPI.CharacterData.data);

            JArray actualCharactersList = (JArray)actualCharacterDataJSON["list"];
            JArray modifiedCharactersList = (JArray)modifiedCharacterDataJSON["list"];

            if (!unlockAllCharacters)
            {
                /* Get the list of characters actually owned by the player (isEntitled == true). */
                HashSet<string> ownedCharacters = new HashSet<string>(
                    actualCharactersList
                        .Where(c => (bool?)c["isEntitled"] == true)
                        .Select(c => (string)c["characterName"]),
                    StringComparer.OrdinalIgnoreCase
                );

                /* Set isEntitled = false for characters not owned by the player. */
                foreach (JObject character in modifiedCharactersList)
                {
                    string characterName = (string)character["characterName"];
                    character["isEntitled"] = ownedCharacters.Contains(characterName);
                }

                /* If the player owns a character that is missing from the modified file, add it. */
                HashSet<string> modifiedCharacterNames = new HashSet<string>(
                    modifiedCharactersList.Select(c => (string)c["characterName"]),
                    StringComparer.OrdinalIgnoreCase
                );

                foreach (JObject actualCharacter in actualCharactersList)
                {
                    string characterName = (string)actualCharacter["characterName"];
                    if (ownedCharacters.Contains(characterName) && !modifiedCharacterNames.Contains(characterName))
                    {
                        modifiedCharactersList.Add(actualCharacter.DeepClone());
                    }
                }
            }
            else
            {
                /* Ensure that all characters are unlocked when unlockAllCharacters = true. */
                foreach (JObject character in modifiedCharactersList)
                {
                    character["isEntitled"] = true;
                }
            }

            return modifiedCharacterDataJSON.ToString();
        }
    }
}
