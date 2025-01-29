namespace Cursed_Market
{
    public static class Globals_Cache
    {
        public static class Forms
        {
            public static Form_Main Main;
            public static readonly Form_Settings Settings                     = new Form_Settings();
            public static readonly Form_Queue Queue                           = new Form_Queue();
            public static readonly Form_Crosshair Crosshair                   = new Form_Crosshair();
            public static readonly Form_CloudIDFriend CloudIDFriend           = new Form_CloudIDFriend();
            public static readonly Form_CharactersPreset CharactersPreset     = new Form_CharactersPreset();
        }
        public static class CursedAPI
        {
            public static class Market
            {
                public static string full = null;
                public static string DLC = null;
            }
            public static class CharacterData
            {
                private static string storenData = null;
                public static string data = null;


                public static void Store() => storenData = data;
                public static void Restore() => data = storenData;
            }

            public static string bloodWebData = null;

            public static string charactersList = null;
            public static string itemsList = null;
        }
        public static class Custom
        {
            public static string charactersPreset = null;
        }
    }
}
