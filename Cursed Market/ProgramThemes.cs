using System;






namespace Cursed_Market
{
    public static class ProgramThemes
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
        private static E_Themes selectedTheme = (E_Themes)WinReg.GetData_DWORD(WinReg.Common.applicationTheme, (int)E_Themes.darkMemories);
        public static E_Themes GetSelectedTheme()
        {
            return selectedTheme;
        }
        public static void SetSelectedTheme(E_Themes newTheme)
        {
            if (WinReg.SetData_DWORD(WinReg.Common.applicationTheme, (int)newTheme))
                selectedTheme = newTheme;
        }




        public static void Reload()
        {
            if (ProgramCache.Forms.Main.InvokeRequired)
            {
                ProgramCache.Forms.Main.Invoke(new Action(() =>
                    ProgramCache.Forms.Main.ReloadTheme()));
            }
            else
            {
                ProgramCache.Forms.Main.ReloadTheme();
            }


            if ((ProgramCache.Forms.Wait != null) && ProgramCache.Forms.Wait.InvokeRequired)
            {
                ProgramCache.Forms.Wait.Invoke(new Action(() =>
                    ProgramCache.Forms.Wait.ReloadTheme()));
            }
            else
            {
                ProgramCache.Forms.Wait.ReloadTheme();
            }


            if (ProgramCache.Forms.Settings.InvokeRequired)
            {
                ProgramCache.Forms.Settings.Invoke(new Action(() =>
                    ProgramCache.Forms.Settings.ReloadTheme()));
            }
            else
            {
                ProgramCache.Forms.Settings.ReloadTheme();
            }


            if (ProgramCache.Forms.Queue.InvokeRequired)
            {
                ProgramCache.Forms.Queue.Invoke(new Action(() =>
                    ProgramCache.Forms.Queue.ReloadTheme()));
            }
            else
            {
                ProgramCache.Forms.Queue.ReloadTheme();
            }


            if (ProgramCache.Forms.CloudIDFriend.InvokeRequired)
            {
                ProgramCache.Forms.CloudIDFriend.Invoke(new Action(() =>
                    ProgramCache.Forms.CloudIDFriend.ReloadTheme()));
            }
            else
            {
                ProgramCache.Forms.CloudIDFriend.ReloadTheme();
            }


            if (ProgramCache.Forms.CharactersPreset.InvokeRequired)
            {
                ProgramCache.Forms.CharactersPreset.Invoke(new Action(() =>
                    ProgramCache.Forms.CharactersPreset.ReloadTheme()));
            }
            else
            {
                ProgramCache.Forms.CharactersPreset.ReloadTheme();
            }


            if (ProgramCache.Forms.Timer.InvokeRequired)
            {
                ProgramCache.Forms.Timer.Invoke(new Action(() =>
                    ProgramCache.Forms.Timer.ReloadTheme()));
            }
            else
            {
                ProgramCache.Forms.Timer.ReloadTheme();
            }
        }
    }
}