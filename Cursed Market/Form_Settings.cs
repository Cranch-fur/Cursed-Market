using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;






namespace Cursed_Market
{
    public partial class Form_Settings : Form
    {
        private static bool applicationRestartRequired = false;

        public Form_Settings()
        {
            InitializeComponent();
            InitializeSettings();
        }

        private void Form_Settings_Load(object sender, EventArgs e)
        {
            ObtainLimitedPlatforms();
        }

        private void Form_Settings_Shown(object sender, EventArgs e)
        {
            pictureBox_Discord.Visible = CursedAPI.SocialLinks.discord != null;
            pictureBox_Telegram.Visible = CursedAPI.SocialLinks.telegram != null;
            pictureBox_Boosty.Visible = CursedAPI.SocialLinks.boosty != null;
        }

        public void ReloadTheme()
        {
            var theme = ProgramThemes.GetCurrentPalette();
            comboBox_ApplicationTheme.SelectedIndex = (int)ProgramThemes.GetSelectedTheme();

            this.BackColor = theme.FormBack;
            panel_WindowHeader.BackColor = theme.HeaderBack;

            ProgramThemes.ApplyForeColor(theme.PrimaryText,
                label_CultureTitle,
                label_ApplicationThemeTitle,
                label_ControlPanelTitle,
                label_PlatformTitle,
                label_DebugInfoTitle,
                label_MatchFoundSoundTitle,
                label_DisabledPlatformsTitle);

            button_ResetApplicationSettings.BackColor = theme.PrimaryButtonBack;
            button_ResetApplicationSettings.ForeColor = theme.PrimaryButtonText;
            button_ResetApplicationSettings.FlatAppearance.BorderColor = theme.PrimaryButtonBorder;

            pictureBox_Discord.Image = theme.DiscordIcon;
            pictureBox_Telegram.Image = theme.TelegramIcon;
            pictureBox_Boosty.Image = theme.BoostyIcon;
        }

        private void InitializeSettings()
        {
            ReloadTheme();

            comboBox_MatchFoundSound.SelectedIndex = (int)ProgramFeatures.QueueNotify.GetSelectedNotifySound();
            UpdateQueueNotifyUI();
        }

        private void button_WindowClose_Click(object sender, EventArgs e)
        {
            if (applicationRestartRequired)
            {
                if (Game.IsRunning())
                {
                    Game.Exit();
                }

                ProgramGlobals.Restart();
            }
            else
            {
                this.Close();
            }
        }

        private void panel_WindowHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                panel_WindowHeader.Capture = false;
                Message mouse = Message.Create(Handle, 0xa1, new IntPtr(2), IntPtr.Zero); // 0xA1 - WM_NCLBUTTONDOWN | new IntPtr(2) - HTCAPTION.
                WndProc(ref mouse);
            }
        }

        private void button_UpdateDebug_MouseClick(object sender, MouseEventArgs e)
        {
            textBox_Culture.Text = ProgramGlobals.culture?.TwoLetterISOLanguageName ?? "NONE";
            textBox_Platform.Text = ProgramSession.Game.Platform.currentPlatform.ToString();
        }

        private void comboBox_Themes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_ApplicationTheme.SelectedIndex != (int)ProgramThemes.GetSelectedTheme())
            {
                ProgramThemes.SetSelectedTheme((ProgramThemes.E_Themes)comboBox_ApplicationTheme.SelectedIndex);
                ProgramThemes.Reload();
            }
        }

        private void comboBox_QueueNotifySound_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProgramFeatures.QueueNotify.SetSelectedNotifySound((ProgramFeatures.QueueNotify.E_NotifySounds)comboBox_MatchFoundSound.SelectedIndex);
            UpdateQueueNotifyUI();
        }

        private void button_QueueNotifySound_MouseClick(object sender, MouseEventArgs e) => Queue.PlaySound();

        private void UpdateQueueNotifyUI()
        {
            button_QueueNotifySound.Visible = ProgramFeatures.QueueNotify.GetSelectedNotifySound() != ProgramFeatures.QueueNotify.E_NotifySounds.none;
            Media.StopSound();
        }

        private void button_ClearCertificates_MouseClick(object sender, MouseEventArgs e)
        {
            if (FiddlerCore.DestroyRootCertificates())
            {
                Messaging.ShowMessage("Root Certificates have been successfully removed from your PC.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                applicationRestartRequired = true;
            }
            else
            {
                Messaging.ShowMessage("Something went wrong in process of Root Certificates removal!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_DisableProxy_MouseClick(object sender, MouseEventArgs e)
        {
            if (WinReg.DisableProxy())
            {
                Media.PlaySoundFromStream(Properties.Resources.SFX_Activate);
            }
            else
            {
                Messaging.ShowMessage("Cursed Market failed to disable proxy!");
            }
        }

        private void button_CreateCertificatePassword_MouseClick(object sender, MouseEventArgs e)
        {
            if (!ProgramNetworking.WritePasswordFile(true))
                Messaging.ShowMessage($"Failed To Write Certificate Password File!\nCertificate Password: {ProgramNetworking.password}", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }

        private void button_OpenCertificateDirectory_MouseClick(object sender, MouseEventArgs e)
        {
            string dataFolder = ProgramPaths.GetDataFolderPath();
            if (!string.IsNullOrEmpty(dataFolder) && Directory.Exists(dataFolder))
                Process.Start(dataFolder);
            else
                Messaging.ShowMessage("Failed To Open Cursed Market Data Folder!\nCursed Market Must be Started At Least Once To Create Data Folder.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }

        private void button_ClearContentCache_MouseClick(object sender, MouseEventArgs e)
        {
            if (!Game.ClearContentCache())
                Messaging.ShowMessage($"Cursed Market failed to clear content cache!\n\n\"{ProgramPaths.contentCacheDirectoryPath}\"", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            else
                Media.PlaySoundFromStream(Properties.Resources.SFX_Activate);
        }

        private void button_SettingsReset_MouseClick(object sender, MouseEventArgs e)
        {
            DialogResult settingsResetDialogueResult = Messaging.ShowDialog("This action will lead to all locally stored settings to be removed, data will be irretrievably lost!\n\nDo you want to proceed anyways?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (settingsResetDialogueResult == DialogResult.Yes)
            {
                if (FiddlerCore.IsRunning())
                    FiddlerCore.Stop();

                if (WinReg.DestroySubKey())
                {
                    if (File.Exists(ProgramPaths.networkingCertificateFilePath))
                        File.Delete(ProgramPaths.networkingCertificateFilePath);
                }

                ProgramGlobals.Restart();
            }
        }

        private void ObtainLimitedPlatforms()
        {
            comboBox_DisabledPlatforms.Items.Clear();

            foreach (ProgramSession.Game.Platform.E_GamePlatform limitedPlatform in ProgramSession.Game.Platform.limitedPlatforms)
                comboBox_DisabledPlatforms.Items.Add(limitedPlatform);

            if (comboBox_DisabledPlatforms.Items.Count > 0)
                comboBox_DisabledPlatforms.SelectedIndex = 0;
        }

        private void button_LimitedPlatforms_MouseClick(object sender, MouseEventArgs e)
        {
            if (comboBox_DisabledPlatforms.Items.Count > 0 && comboBox_DisabledPlatforms.SelectedIndex >= 0)
            {
                List<ProgramSession.Game.Platform.E_GamePlatform> platforms = new List<ProgramSession.Game.Platform.E_GamePlatform>(ProgramSession.Game.Platform.limitedPlatforms);
                platforms.RemoveAt(comboBox_DisabledPlatforms.SelectedIndex);

                ProgramSession.Game.Platform.limitedPlatforms = platforms;
                ObtainLimitedPlatforms();
            }
        }

        private void pictureBox_Discord_Click(object sender, EventArgs e) => Process.Start(CursedAPI.SocialLinks.discord);
        private void pictureBox_Telegram_MouseClick(object sender, MouseEventArgs e) => Process.Start(CursedAPI.SocialLinks.telegram);
        private void pictureBox_Boosty_MouseClick(object sender, MouseEventArgs e) => Process.Start(CursedAPI.SocialLinks.boosty);
    }
}