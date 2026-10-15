using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;






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

        public sealed class ThemePalette
        {
            public bool IsLightTheme { get; set; }
            public Color FormBack { get; set; }
            public Color OverlayBack { get; set; }
            public Color OverlayText { get; set; }
            public Color HeaderBack { get; set; }
            public Color PrimaryText { get; set; }
            public Color TitleText { get; set; }
            public Color MutedText { get; set; }
            public Color SecondaryCardBack { get; set; }
            public Color PrimaryButtonBack { get; set; }
            public Color PrimaryButtonText { get; set; }
            public Color PrimaryButtonBorder { get; set; }
            public Color SecondaryButtonBack { get; set; }
            public Color TrayButtonBack { get; set; }
            public Image BannerImage { get; set; }

            /* Automatically selects black or white icon variants based on theme brightness. */
            public Image SettingsIcon => IsLightTheme ? Properties.Resources.IMG_BUTTON_SETTINGS_BLACK : Properties.Resources.IMG_BUTTON_SETTINGS_WHITE;
            public Image APIIcon => IsLightTheme ? Properties.Resources.IMG_BUTTON_API_BLACK : Properties.Resources.IMG_BUTTON_API_WHITE;
            public Image AddFriendIcon => IsLightTheme ? Properties.Resources.IMG_BUTTON_ADDFRIEND_BLACK : Properties.Resources.IMG_BUTTON_ADDFRIEND_WHITE;
            public Image CharactersPresetIcon => IsLightTheme ? Properties.Resources.IMG_BUTTON_CHARACTERSPRESET_BLACK : Properties.Resources.IMG_BUTTON_CHARACTERSPRESET_WHITE;
            public Image DiscordIcon => IsLightTheme ? Properties.Resources.IMG_SOCIAL_DISCORD_BLACK : Properties.Resources.IMG_SOCIAL_DISCORD_WHITE;
            public Image TelegramIcon => IsLightTheme ? Properties.Resources.IMG_SOCIAL_TELEGRAM_BLACK : Properties.Resources.IMG_SOCIAL_TELEGRAM_WHITE;
            public Image BoostyIcon => IsLightTheme ? Properties.Resources.IMG_SOCIAL_BOOSTY_BLACK : Properties.Resources.IMG_SOCIAL_BOOSTY_WHITE;
        }

        private static readonly Dictionary<E_Themes, ThemePalette> Palettes = new Dictionary<E_Themes, ThemePalette>
        {
            [E_Themes.defaultTheme] = new ThemePalette
            {
                IsLightTheme = true,
                FormBack = Color.White,
                OverlayBack = Color.WhiteSmoke,
                OverlayText = Color.Black,
                HeaderBack = SystemColors.Control,
                PrimaryText = Color.Black,
                TitleText = Color.Black,
                MutedText = SystemColors.ControlText,
                SecondaryCardBack = Color.WhiteSmoke,
                PrimaryButtonBack = Color.Black,
                PrimaryButtonText = Color.White,
                PrimaryButtonBorder = Color.Gray,
                SecondaryButtonBack = Color.DimGray,
                TrayButtonBack = Color.DarkGray,
                BannerImage = Properties.Resources.IMG_BANNER_BLACK
            },
            [E_Themes.legacy] = new ThemePalette
            {
                IsLightTheme = false,
                FormBack = Color.FromArgb(255, 46, 51, 73),
                OverlayBack = Color.FromArgb(255, 46, 51, 73),
                OverlayText = Color.White,
                HeaderBack = Color.FromArgb(255, 24, 30, 54),
                PrimaryText = Color.White,
                TitleText = Color.White,
                MutedText = Color.DimGray,
                SecondaryCardBack = Color.FromArgb(255, 19, 24, 45),
                PrimaryButtonBack = Color.IndianRed,
                PrimaryButtonText = Color.White,
                PrimaryButtonBorder = Color.DarkRed,
                SecondaryButtonBack = Color.RoyalBlue,
                TrayButtonBack = Color.SlateBlue,
                BannerImage = Properties.Resources.IMG_BANNER_WHITE
            },
            [E_Themes.darkMemories] = new ThemePalette
            {
                IsLightTheme = false,
                FormBack = Color.FromArgb(255, 44, 47, 51),
                OverlayBack = Color.FromArgb(255, 44, 47, 51),
                OverlayText = Color.White,
                HeaderBack = Color.FromArgb(255, 35, 39, 42),
                PrimaryText = Color.White,
                TitleText = Color.White,
                MutedText = Color.DimGray,
                SecondaryCardBack = Color.FromArgb(255, 40, 43, 46),
                PrimaryButtonBack = Color.FromArgb(255, 65, 65, 65),
                PrimaryButtonText = Color.White,
                PrimaryButtonBorder = Color.FromArgb(255, 95, 95, 95),
                SecondaryButtonBack = Color.FromArgb(255, 85, 85, 85),
                TrayButtonBack = Color.SlateBlue,
                BannerImage = Properties.Resources.IMG_BANNER_WHITE
            },
            [E_Themes.saintsRow] = new ThemePalette
            {
                IsLightTheme = false,
                FormBack = Color.FromArgb(255, 37, 13, 57),
                OverlayBack = Color.FromArgb(255, 37, 13, 57),
                OverlayText = Color.FromArgb(255, 146, 71, 214),
                HeaderBack = Color.FromArgb(255, 55, 20, 86),
                PrimaryText = Color.White,
                TitleText = Color.White,
                MutedText = Color.DimGray,
                SecondaryCardBack = Color.FromArgb(255, 31, 10, 48),
                PrimaryButtonBack = Color.FromArgb(255, 89, 67, 218),
                PrimaryButtonText = Color.White,
                PrimaryButtonBorder = Color.MediumPurple,
                SecondaryButtonBack = Color.FromArgb(255, 118, 93, 222),
                TrayButtonBack = Color.SlateBlue,
                BannerImage = Properties.Resources.IMG_BANNER_WHITE
            },
            [E_Themes.dracula] = new ThemePalette
            {
                IsLightTheme = false,
                FormBack = Color.FromArgb(255, 40, 42, 54),
                OverlayBack = Color.FromArgb(255, 40, 42, 54),
                OverlayText = Color.FromArgb(255, 248, 248, 242),
                HeaderBack = Color.FromArgb(255, 68, 71, 90),
                PrimaryText = Color.FromArgb(255, 248, 248, 242),
                TitleText = Color.White,
                MutedText = Color.FromArgb(255, 98, 114, 164),
                SecondaryCardBack = Color.FromArgb(255, 62, 65, 83),
                PrimaryButtonBack = Color.FromArgb(255, 89, 67, 218),
                PrimaryButtonText = Color.FromArgb(255, 248, 248, 242),
                PrimaryButtonBorder = Color.MediumPurple,
                SecondaryButtonBack = Color.FromArgb(255, 118, 93, 222),
                TrayButtonBack = Color.SlateBlue,
                BannerImage = Properties.Resources.IMG_BANNER_WHITE
            },
            [E_Themes.christmas] = new ThemePalette
            {
                IsLightTheme = false,
                FormBack = Color.FromArgb(255, 24, 24, 24),
                OverlayBack = Color.FromArgb(255, 24, 24, 24),
                OverlayText = Color.FromArgb(255, 255, 207, 109),
                HeaderBack = Color.FromArgb(255, 14, 14, 14),
                PrimaryText = Color.FromArgb(255, 255, 207, 109),
                TitleText = Color.White,
                MutedText = Color.FromArgb(255, 170, 142, 87),
                SecondaryCardBack = Color.FromArgb(255, 19, 19, 19),
                PrimaryButtonBack = Color.FromArgb(255, 221, 34, 85),
                PrimaryButtonText = Color.White,
                PrimaryButtonBorder = Color.HotPink,
                SecondaryButtonBack = Color.FromArgb(255, 142, 22, 55),
                TrayButtonBack = Color.SlateBlue,
                BannerImage = Properties.Resources.IMG_BANNER_CHRISTMAS2022
            }
        };

        private static E_Themes LoadInitialSelectedTheme()
        {
            int rawValue = WinReg.GetData_DWORD(WinReg.Common.applicationTheme, (int)E_Themes.darkMemories);
            return Enum.IsDefined(typeof(E_Themes), rawValue)
                ? (E_Themes)rawValue
                : E_Themes.darkMemories;
        }

        private static E_Themes selectedTheme = LoadInitialSelectedTheme();
        public static E_Themes GetSelectedTheme() => selectedTheme;

        public static ThemePalette GetCurrentPalette() =>
            Palettes.TryGetValue(selectedTheme, out var palette) ? palette : Palettes[E_Themes.defaultTheme];

        public static void SetSelectedTheme(E_Themes newTheme)
        {
            if (WinReg.SetData_DWORD(WinReg.Common.applicationTheme, (int)newTheme))
                selectedTheme = newTheme;
        }

        /* Helper method to apply foreground color to multiple controls at once. */
        public static void ApplyForeColor(Color color, params Control[] controls)
        {
            foreach (Control ctrl in controls)
            {
                if (ctrl != null) ctrl.ForeColor = color;
            }
        }

        private static void ReloadFormTheme(Form form, Action reloadAction)
        {
            if (form == null || form.IsDisposed) return;

            if (form.InvokeRequired)
                form.Invoke(reloadAction);
            else
                reloadAction();
        }

        public static void Reload()
        {
            ReloadFormTheme(ProgramCache.Forms.Main, () => ProgramCache.Forms.Main.ReloadTheme());
            ReloadFormTheme(ProgramCache.Forms.Wait, () => ProgramCache.Forms.Wait.ReloadTheme());
            ReloadFormTheme(ProgramCache.Forms.Settings, () => ProgramCache.Forms.Settings.ReloadTheme());
            ReloadFormTheme(ProgramCache.Forms.Queue, () => ProgramCache.Forms.Queue.ReloadTheme());
            ReloadFormTheme(ProgramCache.Forms.CloudIDFriend, () => ProgramCache.Forms.CloudIDFriend.ReloadTheme());
            ReloadFormTheme(ProgramCache.Forms.CharactersPreset, () => ProgramCache.Forms.CharactersPreset.ReloadTheme());
            ReloadFormTheme(ProgramCache.Forms.Timer, () => ProgramCache.Forms.Timer.ReloadTheme());
            ReloadFormTheme(ProgramCache.Forms.CrosshairSettings, () => ProgramCache.Forms.CrosshairSettings.ReloadTheme());
            ReloadFormTheme(ProgramCache.Forms.APIConfig, () => ProgramCache.Forms.APIConfig.ReloadTheme());
        }
    }
}