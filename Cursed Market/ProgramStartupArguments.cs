using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;






namespace Cursed_Market
{
    public static class ProgramStartupArguments
    {
        public static class Common
        {
            public static readonly string languageOverride = "-lang=";
            public static readonly string offlineMode = "-offline";
            public static readonly string noCustomizationsKing = "-noCustomizationsKing";
            public static readonly string noAntiKillSwitch = "-noAntiKillSwitch";
            public static readonly string noCharacterData = "-noCharacterData";


            public static readonly string forceClearContentCache = "-forceClearContentCache";


            public static readonly string timerToggleFeature = "-timerToggleFeature";
            public static readonly string crosshairToggleFeature = "-crosshairToggleFeature";
        }




        /* As soon as we obtain command line arguments, we immediately convert them into convenient to work with List<string>. */
        private static readonly List<string> startupArguments = new List<string>(Environment.GetCommandLineArgs());
        public static List<string> GetStartupArguments()
        {
            return startupArguments;
        }
        public static bool HasStartupArgument(string startupArgument)
        {
            return startupArguments.Contains(startupArgument);
        }




        public static CultureInfo GetCulture()
        {
            /* Lookup if application was started with "-lang=" argument, use current Windows UI language if it wasn't. */
            return startupArguments.FirstOrDefault(arg => arg.StartsWith(Common.languageOverride)) != null && startupArguments.FirstOrDefault(arg => arg.StartsWith(Common.languageOverride)).Length == 8
            ? new CultureInfo(startupArguments.First(arg => arg.StartsWith(Common.languageOverride)).Substring(6))
            : CultureInfo.InstalledUICulture;
        }
    }
}
