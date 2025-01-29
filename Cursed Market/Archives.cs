using CranchyLib.Networking;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Cursed_Market
{
    public static class Archives
    {
        public struct S_MatchData
        {
            public string matchId;
            public string krakenMatchId;

            public S_MatchData(string matchId, string krakenMatchId)
            {
                this.matchId = matchId;
                this.krakenMatchId = krakenMatchId;
            }
        }
        public struct S_Quest
        {
            public int level;
            public string nodeId;
            public string storyId;
            public int currentProgression;
            public int neededProgression;
            public JArray questEvents;

            public S_Quest(int level, string nodeId, string storyId, int currentProgression, int neededProgression, JArray questEvents)
            {
                this.level = level;
                this.nodeId = nodeId;
                this.storyId = storyId;
                this.currentProgression = currentProgression;
                this.neededProgression = neededProgression;
                this.questEvents = questEvents;
            }
        }




        public static S_MatchData lastSuccessfulMatch;




        private static S_Quest GetActiveQuest()
        {
            List<string> headers = new List<string>()
            {
                $"Cookie: bhvrSession={Globals_Session.Game.bhvrSession}",
                $"User-Agent: {Globals_Session.Game.user_agent}",
                $"x-kraken-client-platform: {Globals_Session.Game.client_platform}",
                $"x-kraken-client-provider: {Globals_Session.Game.client_provider}",
                $"x-kraken-client-os: {Globals_Session.Game.client_os}",
                $"x-kraken-client-version: {Globals_Session.Game.client_version}",
                "Content-Type: application/json"
            };

            var getActiveNodeResponse = Networking.Get($"https://{Globals_Session.Game.Platform.GetCurrentPlatformHostNames()[0]}/api/v1/archives/stories/get/activeNode", headers);
            if (getActiveNodeResponse.statusCode == Networking.E_StatusCode.OK)
            {
                if (getActiveNodeResponse.content.IsJson())
                {
                    JObject activeNodeJson = JObject.Parse(getActiveNodeResponse.content);
                    JToken specificRoleNodeData = null;
                    int currentProgression = 0;
                    int neededProgression = 0;
                    JArray questEvents = new JArray();

                    switch (Globals_Session.Game.playerRole)
                    {
                        case Globals_Session.Game.E_PlayerRole.Survivor:
                            if (activeNodeJson.ContainsKey("survivorActiveNode"))
                            {
                                specificRoleNodeData = activeNodeJson["survivorActiveNode"];
                            }
                            else
                            {
                                return new S_Quest(-1, null, null, -1, -1, null); // Something went wrong, return dummy quest data.
                            }

                            break;

                        case Globals_Session.Game.E_PlayerRole.Killer:
                            if (activeNodeJson.ContainsKey("killerActiveNode"))
                            {
                                specificRoleNodeData = activeNodeJson["killerActiveNode"];
                            }
                            else
                            {
                                return new S_Quest(-1, null, null, -1, -1, null); // Something went wrong, return dummy quest data.
                            }

                            break;
                    }

                    foreach (JToken activeNode in activeNodeJson["activeNode"])
                    {
                        JToken activeNodeTreeCoordinate = activeNode["nodeTreeCoordinate"];
                        if ((string)activeNodeTreeCoordinate["nodeId"] == (string)specificRoleNodeData["nodeId"])
                        {
                            JArray activeNodeObjectives = (JArray)activeNode["objectives"];
                            foreach (JToken objective in activeNodeObjectives)
                            {
                                currentProgression = (int)objective["currentProgress"];
                                neededProgression = (int)objective["neededProgression"];

                                JArray objectiveQuestEvents = (JArray)objective["questEvent"];
                                foreach (JToken questEvent in objectiveQuestEvents) // I don't think there's any quest consist of more than 1 quest event, foreach loop is here just in case.
                                {
                                    questEvents.Add(questEvent);
                                }
                            }
                        }
                    }

                    return new S_Quest((int)specificRoleNodeData["level"], (string)specificRoleNodeData["nodeId"], (string)specificRoleNodeData["storyId"], currentProgression, neededProgression, questEvents);
                }
            }

            return new S_Quest(-1, null, null, -1, -1, null); // This return can only be achieved if response status code wasn't 200 (OK) or response wasn't JSON.
        }
        public static void CompleteActiveQuest(S_MatchData matchData)
        {
            if (matchData.matchId != lastSuccessfulMatch.matchId && matchData.krakenMatchId != lastSuccessfulMatch.krakenMatchId) // Verify that match we're about to use isn't one that we've already used.
            {
                S_Quest activeQuest = GetActiveQuest();
                if (activeQuest.nodeId != null && (activeQuest.currentProgression != activeQuest.neededProgression)) // Make sure that we've successfully retrieved a quest data and that quest isn't yet complete (there's progression steps to do)
                {
                    List<string> headers = new List<string>()
                    {
                        $"Cookie: bhvrSession={Globals_Session.Game.bhvrSession}",
                        $"User-Agent: {Globals_Session.Game.user_agent}",
                        $"x-kraken-client-platform: {Globals_Session.Game.client_platform}",
                        $"x-kraken-client-provider: {Globals_Session.Game.client_provider}",
                        $"x-kraken-client-os: {Globals_Session.Game.client_os}",
                        $"x-kraken-client-version: {Globals_Session.Game.client_version}",
                        "Content-Type: application/json"
                    };

                    if (activeQuest.questEvents.Count == 1)
                    {
                        if ((int)activeQuest.questEvents[0]["repetition"] <= activeQuest.neededProgression)
                        {
                            int questProgressionToComplete = activeQuest.neededProgression - activeQuest.currentProgression;
                            activeQuest.questEvents[0]["repetition"] = questProgressionToComplete;
                        }
                    }

                    JObject requestBodyJson = JObject.FromObject(new
                    {
                        matchId = matchData.matchId,
                        krakenMatchId = matchData.krakenMatchId,
                        questEvents = activeQuest.questEvents,
                        role = Globals_Session.Game.playerRole.ToString().ToLower() // We need player role to be written in lower case specifically!
                    });

                    var updateQuestProgressResponse = Networking.Post($"https://{Globals_Session.Game.Platform.GetCurrentPlatformHostNames()[0]}/api/v1/archives/stories/update/quest-progress-v3/", headers, requestBodyJson.ToString());
                    if (updateQuestProgressResponse.statusCode == Networking.E_StatusCode.CONTINUE)
                    {
                        lastSuccessfulMatch = matchData;
                    }
                }
            }
        }
    }
}
