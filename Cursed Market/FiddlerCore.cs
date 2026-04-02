using Fiddler;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;






namespace Cursed_Market
{
    public static class FiddlerCore
    {
        static FiddlerCore()
        {
            FiddlerApplication.BeforeRequest += FiddlerToCatchBeforeRequest;
            FiddlerApplication.BeforeResponse += FiddlerToCatchBeforeResponse;
            FiddlerApplication.AfterSessionComplete += FiddlerToCatchAfterSessionComplete;
        }


        private static bool EnsureRootCertificate()
        {
            BCCertMaker.BCCertMaker certProvider = new BCCertMaker.BCCertMaker();
            CertMaker.oCertProvider = certProvider;

            if (File.Exists(ProgramPaths.networkingCertificateFilePath) == false)
            {
                certProvider.CreateRootCertificate();
                certProvider.WriteRootCertificateAndPrivateKeyToPkcs12File(ProgramPaths.networkingCertificateFilePath, ProgramNetworking.password);
            }
            else
            {
                try
                {
                    certProvider.ReadRootCertificateAndPrivateKeyFromPkcs12File(ProgramPaths.networkingCertificateFilePath, ProgramNetworking.password);
                }
                catch
                {
                    File.Delete(ProgramPaths.networkingCertificateFilePath); // Destroy corrupt certificate file.
                    EnsureRootCertificate(); // Re-execute function to build a new certificate.
                }
            }
        
            if (CertMaker.rootCertIsTrusted() == false)
            {
                return CertMaker.trustRootCert();
            }


            return true;

        }
        public static bool DestroyRootCertificates()
        {
            if (CertMaker.rootCertExists())
            {
                if (!CertMaker.removeFiddlerGeneratedCerts(true))
                    return false;
            }

            return true;
        }




        public static bool LoadAndRunExtraLogic(string extraLogicFilePath)
        {
            if (File.Exists(extraLogicFilePath) == false)
            {
                return false;
            }


            try
            {
                string extraLogicCode = File.ReadAllText(extraLogicFilePath);
                if (string.IsNullOrEmpty(extraLogicCode))
                {
                    return false;
                }


                CSharpScript.RunAsync(
                extraLogicCode,
                ScriptOptions.Default
                    .WithReferences(
                        typeof(FiddlerApplication).Assembly,
                        typeof(object).Assembly,
                        typeof(Extensions).Assembly,
                        typeof(MessageBox).Assembly,
                        typeof(JObject).Assembly
                    )
                    .WithImports("System", "Fiddler", "Cursed_Market", "System.Windows.Forms", "Newtonsoft.Json.Linq")
                )
                .GetAwaiter()
                .GetResult();


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to compile Extra Logic module!\nModule: \"{extraLogicFilePath}\"\nException: {ex.ToString()}", "Extra Logic", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
        }




        public static bool Start()
        {
            if (EnsureRootCertificate() == false)
            {
                Messaging.ShowMessage("Cursed Market failed to ensure root certificate!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
            FiddlerApplication.Startup(new FiddlerCoreStartupSettingsBuilder().ListenOnPort(8888).RegisterAsSystemProxy().ChainToUpstreamGateway().DecryptSSL().OptimizeThreadPool().Build());
            

            List<string> runningExtraLogic = new List<string>();
            if (LoadAndRunExtraLogic(ProgramPaths.extraLogicBeforeRequestFilePath))
            {
                runningExtraLogic.Add(ProgramPaths.extraLogicBeforeRequestFilePath);
            }
            if (LoadAndRunExtraLogic(ProgramPaths.extraLogicBeforeResponseFilePath))
            {
                runningExtraLogic.Add(ProgramPaths.extraLogicBeforeResponseFilePath);
            }
            if (LoadAndRunExtraLogic(ProgramPaths.extraLogicAfterSessionCompleteFilePath))
            {
                runningExtraLogic.Add(ProgramPaths.extraLogicAfterSessionCompleteFilePath);
            }


            if (runningExtraLogic.Count > 0) 
            {
                IEnumerable<string> numberedLines = runningExtraLogic.Select((item, index) => $"{index + 1}) \"{item}\"");
                string outText = string.Join(Environment.NewLine, numberedLines);


                Thread messageThread = new Thread(() =>
                {
                    MessageBox.Show($"Fiddler Core is now running with following modules:\n{outText}", "Extra Logic", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                });


                messageThread.SetApartmentState(ApartmentState.STA);
                messageThread.IsBackground = true;
                messageThread.Start();
            }


            return IsRunning();
        }
        public static void Stop()
        {
            if (FiddlerApplication.IsStarted())
            {
                FiddlerApplication.Shutdown();
            }
        }
        public static bool IsRunning() 
        { 
            return FiddlerApplication.IsStarted(); 
        }




        public static void FiddlerToCatchBeforeRequest(Session oSession)
        {
            if (oSession.uriContains("/login?token=") || oSession.uriContains("/loginWithTokenBody"))
            {
                if (oSession.oRequest["User-Agent"].Length > 0)
                    ProgramSession.Game.user_agent = oSession.oRequest["User-Agent"];

                if (oSession.oRequest["x-kraken-client-platform"].Length > 0)
                    ProgramSession.Game.client_platform = oSession.oRequest["x-kraken-client-platform"];

                if (oSession.oRequest["x-kraken-client-provider"].Length > 0)
                    ProgramSession.Game.client_provider = oSession.oRequest["x-kraken-client-provider"];

                if (oSession.oRequest["x-kraken-client-os"].Length > 0)
                    ProgramSession.Game.client_os = oSession.oRequest["x-kraken-client-os"];

                if (oSession.oRequest["x-kraken-client-version"].Length > 0)
                    ProgramSession.Game.client_version = oSession.oRequest["x-kraken-client-version"];


                ProgramSession.Game.Platform.currentPlatform = ProgramSession.Game.Platform.ResolvePlatformFromHostName(oSession.hostname);
            }


            if (oSession.uriContains("/api/v1/config"))
            {
                if (oSession.oRequest["api-key"].Length > 0)
                {
                    ProgramSession.Game.api_key = oSession.oRequest["api-key"];
                    ProgramCache.Forms.Main.UpdateApiKey();
                }

                return;
            }


            if (ProgramSession.Game.Platform.GetCurrentPlatformHostNames().Contains(oSession.hostname))
            {
                if (oSession.uriContains("/api/v1/inventories"))
                {
                    oSession.utilCreateResponseAndBypassServer();

                    if (oSession.uriContains("/consume"))
                    {
                        oSession.utilSetResponseBody(oSession.GetRequestBodyAsString());
                        return;
                    }


                    return;
                }


                if (oSession.uriContains("/api/v1/dbd-inventories/all"))
                {
                    oSession.bBufferResponse = true;
                    return;
                }


                if (ProgramFeatures.Catalog.enabled == true)
                {
                    if (ProgramSession.Game.Platform.limitedPlatforms.Contains(ProgramSession.Game.Platform.currentPlatform))
                    {
                        ProgramFeatures.Catalog.enabled = false;
                        return;
                    }


                    if (oSession.uriContains("/catalog.json"))
                    {
                        oSession.utilCreateResponseAndBypassServer();
                        oSession.utilSetResponseBody(CursedAPI.ResponseFiles.catalog);
                        return;
                    }
                }


                if (ProgramFeatures.AntiKillSwitch.enabled == true)
                {
                    if (ProgramSession.Game.Platform.limitedPlatforms.Contains(ProgramSession.Game.Platform.currentPlatform))
                    {
                        ProgramFeatures.AntiKillSwitch.enabled = false;
                        return;
                    }

                    if (oSession.uriContains("/itemsKillswitch.json"))
                    {
                        oSession.utilCreateResponseAndBypassServer();
                        oSession.utilSetResponseBody(CursedAPI.ResponseFiles.antiKillSwitch);
                        return;
                    }
                }


                if (oSession.uriContains("/api/v1/dbd-character-data/get-all"))
                {
                    if (ProgramFeatures.CharacterData.enabled == true)
                    {
                        oSession.utilCreateResponseAndBypassServer();
                        oSession.utilSetResponseBody(ProgramCache.CursedAPI.CharacterData.data);
                        return;
                    }
                }


                if (oSession.uriContains("/api/v1/dbd-character-data/bloodweb")) // We need to buffer response in order to be capable of swapping it with our own.
                {
                    oSession.bBufferResponse = true;
                    return;
                }


                if (ProgramFeatures.CurrencySpoof.enabled == true)
                {
                    if (oSession.uriContains("api/v1/wallet/currencies"))
                    {
                        oSession.bBufferResponse = true;
                        return;
                    }
                }

                if (oSession.uriContains("/api/v1/dbd-player-card"))
                {
                    if (oSession.url.EndsWith("/set"))
                    {
                        oSession.utilCreateResponseAndBypassServer();
                        string requestBody = oSession.GetRequestBodyAsString();

                        ProgramFeatures.GameProfile.SetStoredPreset(requestBody);

                        oSession.utilSetResponseBody(requestBody);
                        return;
                    }

                    if (oSession.url.EndsWith("/get"))
                    {
                        if (ProgramFeatures.GameProfile.GetStoredPreset() != null)
                        {
                            oSession.utilCreateResponseAndBypassServer();

                            oSession.utilSetResponseBody(ProgramFeatures.GameProfile.GetStoredPreset());
                            return;
                        }
                    }
                }
            }
        }

        public static void FiddlerToCatchBeforeResponse(Session oSession)
        {
            if (ProgramSession.Game.Platform.GetCurrentPlatformHostNames().Contains(oSession.hostname))
            {
                if (oSession.uriContains("/api/v1/dbd-inventories/all"))
                {
                    string responseBody = oSession.GetResponseBodyAsString();


                    if (ProgramFeatures.CharacterData.enabled == true)
                    {
                        if (ProgramSession.Game.isInMatch)
                        {
                            oSession.utilSetResponseBody(CursedAPI.GetPopulatedMarketFile(responseBody, CursedAPI.E_MarketFilePopulationType.All));
                        }
                        else
                        {
                            oSession.utilSetResponseBody(CursedAPI.GetPopulatedMarketFile(responseBody, CursedAPI.E_MarketFilePopulationType.Perks));
                        }
                    }
                    else
                    {
                        oSession.utilSetResponseBody(CursedAPI.GetPopulatedMarketFile(responseBody, CursedAPI.E_MarketFilePopulationType.None));
                    }
                }


                if (oSession.uriContains("/api/v1/dbd-character-data/bloodweb"))
                {
                    if (ProgramFeatures.CharacterData.enabled == true || (ProgramFeatures.CharacterData.enabled == false && oSession.responseCode != 200)) // Requesting bloodweb for a character we doesn't own results server to return an exception, so we need to swap data for these characters anyways.
                    {
                        string requestBody = oSession.GetRequestBodyAsString();
                        if (requestBody.IsJson())
                        {
                            JObject requestBodyJSON = JObject.Parse(requestBody);
                            string characterName = (string)requestBodyJSON["characterName"]; // Get name (ID) of the character game is currently requesting bloodweb for.

                            JObject customResponseJSON = JObject.Parse(ProgramCache.CursedAPI.bloodWebData);
                            customResponseJSON["characterName"] = characterName; // Apply character name (ID) we've got from game request to our response.


                            if (ProgramFeatures.CharacterData.enabled == false)
                            {
                                customResponseJSON["characterItems"] = new JArray(); // We want to remove all character progression from response if user isn't looking for it.
                                customResponseJSON["bloodWebLevel"] = 1; // Invalidate bloodWebLevel.
                                customResponseJSON["prestigeLevel"] = 0; // Invalidate prestigeLevel.
                                customResponseJSON["legacyPrestigeLevel"] = 0; // Invalidate legacyPrestigeLevel. (Legacy Prestige indicates prestige character had before maximum number of prestiges was increased 3 --> 100)

                            }
                            if (ProgramFeatures.CharactersPreset.enabled == true)
                            {
                                if (ProgramFeatures.CharacterData.characterDataMap.ContainsKey(characterName)) // Check if characters map contains character we're currently looking for.
                                {
                                    customResponseJSON["bloodWebLevel"] = ProgramFeatures.CharacterData.characterDataMap[characterName].bloodWebLevel; // Set custom bloodWebLevel.
                                    customResponseJSON["prestigeLevel"] = ProgramFeatures.CharacterData.characterDataMap[characterName].prestigeLevel; // Set custom prestigeLevel.
                                    customResponseJSON["legacyPrestigeLevel"] = ProgramFeatures.CharacterData.characterDataMap[characterName].legacyPrestigeLevel; // Set custom legacyPrestigeLevel. (Legacy Prestige indicates prestige character had before maximum number of prestiges was increased 3 --> 100)
                                }

                            }


                            oSession.responseCode = 200; // Game doesn't initially care about the response content and looking for the response code first.
                            oSession.utilSetResponseBody(customResponseJSON.ToString());
                            return;
                        }
                        else
                        {
                            oSession.utilSetResponseBody(ProgramCache.CursedAPI.bloodWebData.ToString());
                            return;
                        }
                    }
                }

                if (ProgramFeatures.CurrencySpoof.enabled == true)
                {
                    if (oSession.uriContains("api/v1/wallet/currencies"))
                    {
                        string responseBody = oSession.GetResponseBodyAsString();
                        if (responseBody.IsJson())
                        {
                            JObject responseBodyJSON = JObject.Parse(responseBody);
                            JToken currenciesList = responseBodyJSON["list"];
                            foreach (JToken currency in currenciesList)
                            {
                                string currencyName = (string)currency["currency"];
                                switch (currencyName) 
                                {
                                    case "Bloodpoints":
                                        currency["balance"] = 0; // Invalidate amount of bloodpoints, so they won't add up to bonus bloodpoints.
                                        break;

                                    case "BonusBloodpoints":
                                        currency["balance"] = ProgramFeatures.CurrencySpoof.bloodpointsAmount;
                                        break;

                                    case "Shards":
                                        currency["balance"] = ProgramFeatures.CurrencySpoof.iridescentShardsAmount;
                                        break;

                                    case "Cells":
                                        currency["balance"] = ProgramFeatures.CurrencySpoof.auricCellsAmount;
                                        break;
                                }
                            }

                            oSession.utilSetResponseBody(responseBodyJSON.ToString());
                            return;
                        }
                    }
                }
            }
        }
        public static void FiddlerToCatchAfterSessionComplete(Session oSession)
        {
            if (ProgramSession.Game.Platform.GetCurrentPlatformHostNames().Contains(oSession.hostname))
            {
                if (oSession.uriContains("/login?token=") || oSession.uriContains("steam/login") || oSession.uriContains("grdk/loginWithTokenBody"))
                {
                    oSession.utilDecodeResponse();
                    ProgramGlobals.GameAuth.ResolveUserID(oSession.GetResponseBodyAsString());
                }

                if (oSession.uriContains("/api/v1/queue"))
                {
                    if (oSession.fullUrl.EndsWith("/token/issue"))
                        return;

                    if (oSession.fullUrl.EndsWith("/cancel"))
                    {
                        Queue.SetQueueStatus(Queue.E_QueueStatus.None);
                    }


                    ProgramSession.Game.isInQueue = true;
                    ProgramSession.Game.isInMatch = false; // Searching for a new lobby means that player is no longer in the match.


                    ProgramSession.Game.matchId = null; // We're currently looking for a new match, invalidate old matchId.
                    ProgramSession.Game.matchType = ProgramSession.Game.E_MatchType.None;
                    ProgramSession.Game.playerRole = ProgramSession.Game.E_PlayerRole.None;


                    oSession.utilDecodeResponse();
                    string responseBody = oSession.GetResponseBodyAsString();

                    if (string.IsNullOrEmpty(responseBody) == false)
                    {
                        if (responseBody.IsJson() == true)
                        {
                            JObject queueJson = JObject.Parse(responseBody);
                            if ((string)queueJson["status"] == "QUEUED")
                            {
                                Queue.SetQueueStatus(Queue.E_QueueStatus.Searching);
                                Queue.SetQueuePosition((int)queueJson["queueData"]["position"]);
                            }
                            else if ((string)queueJson["status"] == "MATCHED")
                                Queue.SetQueueStatus(Queue.E_QueueStatus.LobbyFound);
                            else
                                Queue.SetQueueStatus(Queue.E_QueueStatus.None);


                            Queue.UpdateQueue();
                        }
                    }


                    return;
                }


                if (oSession.uriContains("/api/v1/match"))
                {
                    oSession.utilDecodeResponse();
                    string responseBody = oSession.GetResponseBodyAsString();

                    if (responseBody.IsJson())
                    {
                        JObject responseJson = JObject.Parse(responseBody);

                        if (responseJson.ContainsKey("sideA") && responseJson.ContainsKey("sideB"))
                        {
                            JArray survivorsArray = (JArray)responseJson["sideB"];
                            JArray killersArray = (JArray)responseJson["sideA"];

                            if (ProgramSession.Game.isInQueue == true)
                            {
                                int survivorsCount = survivorsArray.Count;

                                if (survivorsCount == 4)
                                {
                                    Queue.SetQueueStatus(Queue.E_QueueStatus.LobbyIdle);
                                    Queue.UpdateQueue();

                                    ProgramSession.Game.isInQueue = false;
                                }
                            }

                            foreach (string killer in killersArray)
                            {
                                if (killer == ProgramSession.Game.userId)
                                    ProgramSession.Game.playerRole = ProgramSession.Game.E_PlayerRole.Killer;
                            }

                            foreach (string survivor in survivorsArray)
                            {
                                if (survivor == ProgramSession.Game.userId)
                                    ProgramSession.Game.playerRole = ProgramSession.Game.E_PlayerRole.Survivor;
                            }
                        }
                        
                        if (responseJson.ContainsKey("status") && responseJson.ContainsKey("reason"))
                        {
                            if ((string)responseJson["status"] == "CLOSED" && (string)responseJson["reason"] == "closed")
                            {
                                if (ProgramSession.Game.isInMatch == false) // There's additional /match request at the end of the match, so we have to make sure that we aren't already in match first.
                                {
                                    if (responseJson.ContainsKey("props"))
                                    {
                                        if ((string)responseJson["props"]["GameType"] == ":1") // GameType :1 - Custom Game. We do not want our Queue Status logic to apply to the custom match.
                                        {
                                            ProgramSession.Game.matchType = ProgramSession.Game.E_MatchType.Custom;
                                        }
                                        else
                                        {
                                            ProgramSession.Game.matchType = ProgramSession.Game.E_MatchType.Default;

                                            Queue.SetQueueStatus(Queue.E_QueueStatus.MatchStarting);
                                            Queue.UpdateQueue();
                                        }
                                    }

                                    ProgramSession.Game.isInQueue = false; // Not really needed since we already have that check above, let it be here just in case.
                                    ProgramSession.Game.isInMatch = true; // Match was found and closed, at this point player is already loading in.
                                }
                            }
                        }

                        string searchPattern = @"match\/([a-f0-9\-]+)$"; // "/api/v1/match/{GUID}"
                        Match match = Regex.Match(oSession.fullUrl, searchPattern);
                        if (match.Success)
                        {
                            ProgramSession.Game.matchId = match.Groups[1].Value;
                        }

                    }
                }


                if (ProgramFeatures.GuaranteedQuests.enabled == true)
                {
                    if (oSession.uriContains("/api/v1/gameDataAnalytics/v2/batch"))
                    {
                        oSession.utilDecodeRequest();
                        string requestBody = oSession.GetRequestBodyAsString();

                        if (requestBody.IsJson())
                        {
                            JObject requestJson = JObject.Parse(requestBody);

                            if (requestJson.ContainsKey("events"))
                            {
                                foreach (JToken gameEvent in requestJson["events"])
                                {
                                    JToken eventData = gameEvent["data"];

                                    if (eventData != null)
                                    {
                                        JToken matchId = eventData["match_id"];
                                        JToken krakenMatchId = eventData["kraken_match_id"];

                                        if (matchId != null && krakenMatchId != null) // Check if matchId & krakenMatchId are set in logs request.
                                        {
                                            Archives.S_MatchData matchData = new Archives.S_MatchData((string)matchId, (string)krakenMatchId);
                                            Archives.CompleteActiveQuest(matchData);
                                        }
                                        else
                                        {
                                            matchId = eventData["matchmaking_session_guid"];

                                            if (matchId != null && krakenMatchId != null)
                                            {
                                                Archives.S_MatchData matchData = new Archives.S_MatchData((string)matchId, (string)krakenMatchId);
                                                Archives.CompleteActiveQuest(matchData);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
