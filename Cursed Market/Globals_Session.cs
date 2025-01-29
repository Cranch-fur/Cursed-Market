using System.Collections.Generic;

namespace Cursed_Market
{
    public static class Globals_Session
    {
        public static class Game
        {
            public enum E_PlayerRole
            {
                None,
                Survivor,
                Killer
            }
            public enum E_MatchType
            {
                None,
                Default,
                Custom
            }




            public static string bhvrSession = null;
            public static string userId      = null;


            public static bool isInQueue = false;
            public static bool isInMatch = false;


            public static string matchId = null;
            public static E_MatchType matchType = E_MatchType.None;
            public static E_PlayerRole playerRole = E_PlayerRole.None;


            public static class Platform
            {
                public enum E_GamePlatform
                {
                    None,
                    Android,
                    DMM,
                    IOS = 4,
                    Switch = 8,
                    PS4 = 16,
                    Steam = 32,
                    Steam_PTB = 48,
                    WinGDK = 64,
                    Xbox = 128,
                    Stadia = 512,
                    PS5 = 1024,
                    XSX = 2048,
                    Epic = 4096,
                }


                public static List<E_GamePlatform> limitedPlatforms = new List<E_GamePlatform>()
            {
                E_GamePlatform.Steam_PTB
            };
                public static E_GamePlatform currentPlatform = E_GamePlatform.None;


                public static List<string> GetPlatformHostNames(E_GamePlatform platform = E_GamePlatform.None)
                {
                    switch (platform)
                    {
                        case E_GamePlatform.Steam:
                            return new List<string>()
                        {
                            "steam.live.bhvrdbd.com",
                            "cdn.live.dbd.bhvronline.com",
                            "cdn.live.bhvrdbd.com",
                            "gamelogs.live.bhvrdbd.com"
                        };

                        case E_GamePlatform.Steam_PTB:
                            return new List<string>()
                        {
                            "latest.ptb.bhvrdbd.com",
                            "cdn.ptb.dbd.bhvronline.com"
                        };

                        case E_GamePlatform.WinGDK:
                            return new List<string>()
                        {
                            "grdk.live.bhvrdbd.com",
                            "cdn.live.dbd.bhvronline.com",
                            "cdn.live.bhvrdbd.com",
                            "gamelogs.live.bhvrdbd.com"
                        };

                        case E_GamePlatform.Epic:
                            return new List<string>()
                        {
                            "egs.live.bhvrdbd.com",
                            "cdn.live.dbd.bhvronline.com",
                            "cdn.live.bhvrdbd.com",
                            "gamelogs.live.bhvrdbd.com"
                        };

                        default:
                            List<string> combinedHostnamesList = new List<string>();
                            combinedHostnamesList.AddRange(GetPlatformHostNames(E_GamePlatform.Steam));
                            combinedHostnamesList.AddRange(GetPlatformHostNames(E_GamePlatform.WinGDK));
                            combinedHostnamesList.AddRange(GetPlatformHostNames(E_GamePlatform.Epic));

                            return combinedHostnamesList;
                    }
                }
                public static List<string> GetCurrentPlatformHostNames()
                {
                    return GetPlatformHostNames(currentPlatform);
                }


                public static E_GamePlatform ResolvePlatformFromHostName(string hostName)
                {
                    if (GetPlatformHostNames(E_GamePlatform.Steam).Contains(hostName))
                        return E_GamePlatform.Steam;

                    else if (GetPlatformHostNames(E_GamePlatform.Steam_PTB).Contains(hostName))
                        return E_GamePlatform.Steam_PTB;

                    else if (GetPlatformHostNames(E_GamePlatform.WinGDK).Contains(hostName))
                        return E_GamePlatform.WinGDK;

                    else if (GetPlatformHostNames(E_GamePlatform.Epic).Contains(hostName))
                        return E_GamePlatform.Epic;

                    else
                        return E_GamePlatform.None;
                }
            }


            public static string user_agent = null;
            public static string client_version = null;
            public static string client_provider = null;
            public static string client_platform = null;
            public static string client_os = null;
        }
    }
}
