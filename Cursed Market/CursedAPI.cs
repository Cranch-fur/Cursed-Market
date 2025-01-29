using CranchyLib.Networking;
using Newtonsoft.Json;
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
            public static readonly string heartBeat      = "https://cursed.cranchpalace.info/DaylightGame/v2/heartBeat";
            public static readonly string versionCheck   = "https://cursed.cranchpalace.info/DaylightGame/v2/versionCheck";
            public static readonly string marketFull     = "https://cursed.cranchpalace.info/DaylightGame/v2/data/marketFull.json";
            public static readonly string marketDLC      = "https://cursed.cranchpalace.info/DaylightGame/v2/data/marketDLC.json";
            public static readonly string characterData  = "https://cursed.cranchpalace.info/DaylightGame/v2/data/characterData.json";
            public static readonly string bloodwebData   = "https://cursed.cranchpalace.info/DaylightGame/v2/data/bloodwebData.json";
            public static readonly string charactersList = "https://cursed.cranchpalace.info/DaylightGame/v2/data/charactersList.json";
            public static readonly string itemsList      = "https://cursed.cranchpalace.info/DaylightGame/v2/data/itemsList.json";
            public static readonly string catalog        = "https://cursed.cranchpalace.info/DaylightGame/v2/data/catalog.json";
            public static readonly string antiKillSwitch = "https://cursed.cranchpalace.info/DaylightGame/v2/data/killSwitch.json";
            public static readonly string robotoFont     = "https://cursed.cranchpalace.info/DaylightGame/v2/data/Roboto-Regular.ttf";
        }




        public static class ResponseFiles
        {
            public static string market         = null;
            public static string catalog        = null;
            public static string antiKillSwitch = null;
        }

        public static class SocialLinks
        {
            public static string discord  = null;
            public static string telegram = null;
            public static string boosty   = null;
        }




        public static bool HeartBeat()
        {
            try
            {
                var heartBeatResponse = Networking.Get(SE_CommonEndpoints.heartBeat, Globals.Application.Networking.defaultHeaders);
                return heartBeatResponse.statusCode == Networking.E_StatusCode.OK && heartBeatResponse.content == "OK";
            }
            catch
            {
                return false;
            }
        }


        public static void VersionCheck()
        {
            var versionCheckResponse = Networking.Get(SE_CommonEndpoints.versionCheck, Globals.Application.Networking.defaultHeaders);
            if (versionCheckResponse.statusCode != Networking.E_StatusCode.OK)
            {
                Messaging.ShowMessage($"Cursed Market failed to check for updates!\nSTATUS CODE: {versionCheckResponse.statusCode}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (versionCheckResponse.content.IsJson() == false)
            {
                Messaging.ShowMessage($"Cursed Market failed to check for updates!\n\nGiven by web server response has unexpected format and couldn't be parsed as JSON.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            JObject versionJson = JObject.Parse(versionCheckResponse.content);
            bool updateRequired = (bool)versionJson["updateRequired"];

            if (updateRequired)
            {
                string currentVersion = Globals.Application.version;
                string latestVersion = (string)versionJson["latestVersion"];
                string updateDownloadUrl = (string)versionJson["updateDownloadUrl"];
                string downloadDestinationPath = Globals.Directories.Windows.downloadsDirectoryPath;

                if (Messaging.ShowDialog($"Cursed Market update is available for download!\nCurrent Version: {currentVersion}\nLatest Version: {latestVersion}\n\nDo you want to download updated client now?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    var updateDownloadRequest = Networking.Download(updateDownloadUrl, Globals.Application.Networking.defaultHeaders, downloadDestinationPath);
                    if (File.Exists(updateDownloadRequest.content) == true)
                    {
                        Process.Start(updateDownloadRequest.content);
                        Globals.Application.Close();
                    }
                    else
                    {
                        Messaging.ShowMessage(updateDownloadRequest.content);
                        if (Messaging.ShowDialog("Cursed Market failed to download update directly!\n\nShould we try alternative method instead?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Process.Start(updateDownloadUrl);
                            Globals.Application.Close();
                        }
                    }
                }
            }

            if (versionJson.ContainsKey("socialLinks"))
            {
                if (versionJson["socialLinks"]["discord"] != null)
                    SocialLinks.discord = (string)versionJson["socialLinks"]["discord"];

                if (versionJson["socialLinks"]["telegram"] != null)
                    SocialLinks.telegram = (string)versionJson["socialLinks"]["telegram"];

                if (versionJson["socialLinks"]["boosty"] != null)
                    SocialLinks.boosty = (string)versionJson["socialLinks"]["boosty"];
            }

            if (versionJson.ContainsKey("killSwitch"))
            {
                JToken killSwitch = versionJson["killSwitch"];


                if (killSwitch["platforms"] != null)
                {
                    JToken platforms = killSwitch["platforms"];
                    foreach (JObject platform in platforms)
                    {
                        string hostname = (string)platform["hostname"];

                        Globals_Session.Game.Platform.E_GamePlatform gamePlatform = Globals_Session.Game.Platform.ResolvePlatformFromHostName(hostname);
                        Globals_Session.Game.Platform.limitedPlatforms.Add(gamePlatform);
                    }
                }


                if (killSwitch["functions"] != null)
                {
                    JToken functions = killSwitch["functions"];
                    foreach (string function in functions)
                    {
                        if (Enum.TryParse(function, out E_MainFunctions mainFunction))
                        {
                            Globals.SetMainFunctionEnabled(mainFunction, false);
                        }
                    }
                }
            }

            if (versionJson.TryGetValue("extraActions", out var extraActions) && extraActions != null)
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
                    // Check if we already have requested data pre-cached in the memory.
                    if (!dlcOnly && Globals_Cache.CursedAPI.Market.full != null)
                        return Globals_Cache.CursedAPI.Market.full;

                    if (dlcOnly && Globals_Cache.CursedAPI.Market.DLC != null)
                        return Globals_Cache.CursedAPI.Market.DLC;

                    // Send web request for the Market File. 
                    var marketFileResponse = dlcOnly
                        ? Networking.Get(SE_CommonEndpoints.marketDLC, Globals.Application.Networking.defaultHeaders)   // Request DLC Market File if dlcOnly == true.
                        : Networking.Get(SE_CommonEndpoints.marketFull, Globals.Application.Networking.defaultHeaders); // Request Full Market File if dlcOnly == false.

                    if (marketFileResponse.statusCode != Networking.E_StatusCode.OK)
                    {
                        Messaging.ShowMessage($"Cursed Market failed to obtain Market File!\nSTATUS CODE: {marketFileResponse.statusCode}\n\nLocally stored, offline image will be used instead.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return GetMarketFile(E_ActionMode.Offline, dlcOnly);
                    }

                    // Decompress server response and cache it in the memory.
                    string decompressedResult = Compression.ZLIB.Decompress(marketFileResponse.content);
                    if (dlcOnly == true)
                        Globals_Cache.CursedAPI.Market.DLC = decompressedResult;
                    else
                        Globals_Cache.CursedAPI.Market.full = decompressedResult;

                    return decompressedResult;

                // There's no unique scenario for Local mode, so Offline & Local both return the same thing.
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

                    if (Globals_Cache.CursedAPI.CharacterData.data != null)
                        return Globals_Cache.CursedAPI.CharacterData.data;

                    var characterDataResponse = Networking.Get(SE_CommonEndpoints.characterData, Globals.Application.Networking.defaultHeaders, null, 30); // Character Data is a large chunk of data, so we want to await response for up to 30 seconds (in case someone has slow internet connection)
                    if (characterDataResponse.statusCode != Networking.E_StatusCode.OK)
                    {
                        Messaging.ShowMessage($"Cursed Market failed to obtain Character Data!\nSTATUS CODE: {characterDataResponse.statusCode}\n\nLocally stored, offline image will be used instead.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return GetCharacterData(E_ActionMode.Offline);
                    }

                    return Compression.ZLIB.Decompress(characterDataResponse.content);

                // There's no unique scenario for Local mode, so Offline & Local both return the same thing.
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

                    if (Globals_Cache.CursedAPI.bloodWebData != null)
                        return Globals_Cache.CursedAPI.bloodWebData;

                    var bloodWebDataResponse = Networking.Get(SE_CommonEndpoints.bloodwebData, Globals.Application.Networking.defaultHeaders);
                    if (bloodWebDataResponse.statusCode != Networking.E_StatusCode.OK)
                    {
                        Messaging.ShowMessage($"Cursed Market failed to obtain Bloodweb Data!\nSTATUS CODE: {bloodWebDataResponse.statusCode}\n\nLocally stored, offline image will be used instead.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return GetBloodWebData(E_ActionMode.Offline);
                    }

                    return Compression.ZLIB.Decompress(bloodWebDataResponse.content);

                // There's no unique scenario for Local mode, so Offline & Local both return the same thing.
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

                    if (Globals_Cache.CursedAPI.charactersList != null)
                        return Globals_Cache.CursedAPI.charactersList;

                    var charactersListResponse = Networking.Get(SE_CommonEndpoints.charactersList, Globals.Application.Networking.defaultHeaders);
                    if (charactersListResponse.statusCode != Networking.E_StatusCode.OK)
                    {
                        Messaging.ShowMessage($"Cursed Market failed to obtain Characters List!\nSTATUS CODE: {charactersListResponse.statusCode}\n\nLocally stored, offline image will be used instead.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return GetItemsList(E_ActionMode.Offline);
                    }

                    return Compression.ZLIB.Decompress(charactersListResponse.content);

                // There's no unique scenario for Local mode, so Offline & Local both return the same thing.
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

                    if (Globals_Cache.CursedAPI.itemsList != null)
                        return Globals_Cache.CursedAPI.itemsList;

                    var itemsListResponse = Networking.Get(SE_CommonEndpoints.itemsList, Globals.Application.Networking.defaultHeaders);
                    if (itemsListResponse.statusCode != Networking.E_StatusCode.OK)
                    {
                        Messaging.ShowMessage($"Cursed Market failed to obtain Items List!\nSTATUS CODE: {itemsListResponse.statusCode}\n\nLocally stored, offline image will be used instead.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return GetItemsList(E_ActionMode.Offline);
                    }

                    return Compression.ZLIB.Decompress(itemsListResponse.content);

                // There's no unique scenario for Local mode, so Offline & Local both return the same thing.
                case E_ActionMode.Offline:
                case E_ActionMode.Local:
                    return Compression.ZLIB.Decompress(Properties.OfflineAPI.ItemsList);

                default: // We need default case in order to convince compiler that all code scenarios lead to an return.
                    throw new Exception($"{actionMode} was never designed to proceed through default!");
            }
        }




        public static void ObtainCatalog()
        {
            if (Globals.Application.startupArguments.Contains(Globals.Application.SE_CommonStartupArguments.noCustomizationsKing))
            {
                Globals.SetGameChangerStatus(E_GameChangers.customizationsKing, E_GameChangerStatus.Disabled);
                return;
            }

            var catalogResponse = Networking.Get(SE_CommonEndpoints.catalog, Globals.Application.Networking.defaultHeaders, null, 30); // Catalog is a large chunk of data, so we want to await response for up to 30 seconds (in case someone has slow internet connection)
            if (catalogResponse.statusCode == Networking.E_StatusCode.OK)
            {
                ResponseFiles.catalog = catalogResponse.content;
                Globals.FiddlerCoreTunables.Catalog.enabled = true;

                Globals.SetGameChangerStatus(E_GameChangers.customizationsKing, E_GameChangerStatus.Enabled);
            }
            else
            {
                Globals.SetGameChangerStatus(E_GameChangers.customizationsKing, E_GameChangerStatus.Failed);
            }
        }

        public static void ObtainAntiKillSwitch()
        {
            if (Globals.Application.startupArguments.Contains("-nokillswitch"))
            {
                Globals.SetGameChangerStatus(E_GameChangers.antiKillSwitch, E_GameChangerStatus.Disabled);
                return;
            }

            var antiKillSwitchResponse = Networking.Get(SE_CommonEndpoints.antiKillSwitch, Globals.Application.Networking.defaultHeaders);
            if (antiKillSwitchResponse.statusCode == Networking.E_StatusCode.OK)
            {
                Globals.FiddlerCoreTunables.AntiKillSwitch.enabled = true;
                ResponseFiles.antiKillSwitch = antiKillSwitchResponse.content;

                Globals.SetGameChangerStatus(E_GameChangers.antiKillSwitch, E_GameChangerStatus.Enabled);
            }
            else
            {
                Globals.SetGameChangerStatus(E_GameChangers.antiKillSwitch, E_GameChangerStatus.Failed);
            }
        }




        public static string GetPopulatedMarketFile(string actualMarketFile, E_MarketFilePopulationType populationType)
        {
            JArray charactersArray = JArray.Parse(Globals_Cache.CursedAPI.charactersList);
            JObject actualMarketFileJSON = JObject.Parse(actualMarketFile);
            JObject modifiedMarketFileJSON = JObject.Parse(ResponseFiles.market);


            HashSet<string> actualItemsList = new HashSet<string>(actualMarketFileJSON["inventoryItems"].Select(item => (string)item["objectId"]));
            List<JObject> charactersToAdd = charactersArray
                .Where(c => actualItemsList.Contains((string)c))
                .Select(c => new JObject
                {
                    ["objectId"] = c,
                    ["quantity"] = 1
                })
                .ToList();


            JArray modifiedMarketItems = (JArray)modifiedMarketFileJSON["inventoryItems"];
            foreach (var character in charactersToAdd)
            {
                modifiedMarketItems.Add(character);
            }


            if (populationType != E_MarketFilePopulationType.None)
            {
                JArray inventory = (JArray)modifiedMarketFileJSON["inventoryItems"];
                JObject itemsList = JObject.Parse(Globals_Cache.CursedAPI.itemsList);


                Random rnd = new Random();
                long startupTimeStamp = Globals.Application.startupTimeStamp;


                // Mapping types to property names.
                var propertyMapping = new Dictionary<E_MarketFilePopulationType, string>
                    {
                        { E_MarketFilePopulationType.Perks, "perks" },
                        { E_MarketFilePopulationType.Items, "items" },
                        { E_MarketFilePopulationType.Addons, "addons" },
                        { E_MarketFilePopulationType.Offerings, "offerings" }
                    };
                // If specific populationType is selected, we only add it.
                // If E_MarketFilePopulationType.All is selected, add all types.
                IEnumerable<string> propertiesToProcess = populationType == E_MarketFilePopulationType.All ? propertyMapping.Values : propertyMapping.Where(p => p.Key == populationType).Select(p => p.Value);


                foreach (var property in propertiesToProcess)
                {
                    JArray propertyContentArray = (JArray)itemsList[property];
                    foreach (JToken content in propertyContentArray)
                    {
                        inventory.Add(new JObject(
                            new JProperty("lastUpdateAt", rnd.NextLong(startupTimeStamp - 300, startupTimeStamp + 300)),
                            new JProperty("objectId", content),
                            new JProperty("quantity", property == "perks" ? 3 : 50) // Quantity determines 
                        ));
                    }
                }
            }


            return modifiedMarketFileJSON.ToString();
        }
    }
}
