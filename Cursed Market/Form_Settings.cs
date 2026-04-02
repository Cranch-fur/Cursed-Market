using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;






namespace Cursed_Market
{
    public partial class Form_Settings : Form
    {
        static bool applicationRestartRequired = false;
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
            pictureBox_Discord.Visible  = CursedAPI.SocialLinks.discord != null;
            pictureBox_Telegram.Visible = CursedAPI.SocialLinks.telegram != null;
            pictureBox_Boosty.Visible   = CursedAPI.SocialLinks.boosty != null;
        }




        public void ReloadTheme()
        {
            switch (ProgramThemes.GetSelectedTheme())
            {
                default:
                    comboBox_ApplicationTheme.SelectedIndex = 0;
                    this.BackColor = Color.White;
                    panel_WindowHeader.BackColor = SystemColors.Control;
                    label_CultureTitle.ForeColor = Color.Black;
                    label_ApplicationThemeTitle.ForeColor = Color.Black;
                    label_ControlPanelTitle.ForeColor = Color.Black;
                    label_PlatformTitle.ForeColor = Color.Black;
                    label_DebugInfoTitle.ForeColor = Color.Black;
                    label_MatchFoundSoundTitle.ForeColor = Color.Black;
                    label_DisabledPlatformsTitle.ForeColor = Color.Black;
                    pictureBox_Discord.Image = Properties.Resources.IMG_SOCIAL_DISCORD_BLACK;
                    pictureBox_Telegram.Image = Properties.Resources.IMG_SOCIAL_TELEGRAM_BLACK;
                    pictureBox_Boosty.Image = Properties.Resources.IMG_SOCIAL_BOOSTY_BLACK;
                    break;

                case ProgramThemes.E_Themes.legacy:
                    comboBox_ApplicationTheme.SelectedIndex = 1;
                    this.BackColor = Color.FromArgb(255, 46, 51, 73);
                    panel_WindowHeader.BackColor = Color.FromArgb(255, 24, 30, 54);
                    label_CultureTitle.ForeColor = Color.White;
                    label_ApplicationThemeTitle.ForeColor = Color.White;
                    label_ControlPanelTitle.ForeColor = Color.White;
                    label_PlatformTitle.ForeColor = Color.White;
                    label_DebugInfoTitle.ForeColor = Color.White;
                    label_MatchFoundSoundTitle.ForeColor = Color.White;
                    label_DisabledPlatformsTitle.ForeColor = Color.White;
                    pictureBox_Discord.Image = Properties.Resources.IMG_SOCIAL_DISCORD_WHITE;
                    pictureBox_Telegram.Image = Properties.Resources.IMG_SOCIAL_TELEGRAM_WHITE;
                    pictureBox_Boosty.Image = Properties.Resources.IMG_SOCIAL_BOOSTY_WHITE;
                    break;

                case ProgramThemes.E_Themes.darkMemories:
                    comboBox_ApplicationTheme.SelectedIndex = 2;
                    this.BackColor = Color.FromArgb(255, 44, 47, 51);
                    panel_WindowHeader.BackColor = Color.FromArgb(255, 35, 39, 42);
                    label_CultureTitle.ForeColor = Color.White;
                    label_ApplicationThemeTitle.ForeColor = Color.White;
                    label_ControlPanelTitle.ForeColor = Color.White;
                    label_PlatformTitle.ForeColor = Color.White;
                    label_DebugInfoTitle.ForeColor = Color.White;
                    label_MatchFoundSoundTitle.ForeColor = Color.White;
                    label_DisabledPlatformsTitle.ForeColor = Color.White;
                    pictureBox_Discord.Image = Properties.Resources.IMG_SOCIAL_DISCORD_WHITE;
                    pictureBox_Telegram.Image = Properties.Resources.IMG_SOCIAL_TELEGRAM_WHITE;
                    pictureBox_Boosty.Image = Properties.Resources.IMG_SOCIAL_BOOSTY_WHITE;
                    break;

                case ProgramThemes.E_Themes.saintsRow:
                    comboBox_ApplicationTheme.SelectedIndex = 3;
                    this.BackColor = Color.FromArgb(255, 37, 13, 57);
                    panel_WindowHeader.BackColor = Color.FromArgb(255, 55, 20, 86);
                    label_CultureTitle.ForeColor = Color.White;
                    label_ApplicationThemeTitle.ForeColor = Color.White;
                    label_ControlPanelTitle.ForeColor = Color.White;
                    label_PlatformTitle.ForeColor = Color.White;
                    label_DebugInfoTitle.ForeColor = Color.White;
                    label_MatchFoundSoundTitle.ForeColor = Color.White;
                    label_DisabledPlatformsTitle.ForeColor = Color.White;
                    pictureBox_Discord.Image = Properties.Resources.IMG_SOCIAL_DISCORD_WHITE;
                    pictureBox_Telegram.Image = Properties.Resources.IMG_SOCIAL_TELEGRAM_WHITE;
                    pictureBox_Boosty.Image = Properties.Resources.IMG_SOCIAL_BOOSTY_WHITE;
                    break;

                case ProgramThemes.E_Themes.dracula:
                    comboBox_ApplicationTheme.SelectedIndex = 4;
                    this.BackColor = Color.FromArgb(255, 40, 42, 54);
                    panel_WindowHeader.BackColor = Color.FromArgb(255, 68, 71, 90);
                    label_CultureTitle.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    label_ApplicationThemeTitle.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    label_ControlPanelTitle.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    label_PlatformTitle.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    label_DebugInfoTitle.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    label_MatchFoundSoundTitle.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    label_DisabledPlatformsTitle.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    pictureBox_Discord.Image = Properties.Resources.IMG_SOCIAL_DISCORD_WHITE;
                    pictureBox_Telegram.Image = Properties.Resources.IMG_SOCIAL_TELEGRAM_WHITE;
                    pictureBox_Boosty.Image = Properties.Resources.IMG_SOCIAL_BOOSTY_WHITE;
                    break;

                case ProgramThemes.E_Themes.christmas:
                    comboBox_ApplicationTheme.SelectedIndex = 5;
                    this.BackColor = Color.FromArgb(255, 24, 24, 24);
                    panel_WindowHeader.BackColor = Color.FromArgb(255, 14, 14, 14);
                    label_CultureTitle.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    label_ApplicationThemeTitle.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    label_ControlPanelTitle.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    label_PlatformTitle.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    label_DebugInfoTitle.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    label_MatchFoundSoundTitle.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    label_DisabledPlatformsTitle.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    pictureBox_Discord.Image = Properties.Resources.IMG_SOCIAL_DISCORD_WHITE;
                    pictureBox_Telegram.Image = Properties.Resources.IMG_SOCIAL_TELEGRAM_WHITE;
                    pictureBox_Boosty.Image = Properties.Resources.IMG_SOCIAL_BOOSTY_WHITE;
                    break;
            }
        }




        private void InitializeSettings()
        {
            ReloadTheme();


            comboBox_MatchFoundSound.SelectedIndex = (int)ProgramFeatures.QueueNotify.GetSelectedNotifySound();
        }




        private void button_WindowClose_Click(object sender, EventArgs e)
        {
            if (applicationRestartRequired == true)
            {
                if (Game.IsRunning())
                {
                    Game.Exit();
                }

                if (FiddlerCore.IsRunning() == true)
                {
                    FiddlerCore.Stop();
                }

                Application.Restart();
            }
            else this.Close();
        }
        private async void panel_WindowHeader_MouseDown(object sender, MouseEventArgs e)
        {
            panel_WindowHeader.Capture = false;

            await Task.Run(() =>
            {
                this.Invoke(new Action(() =>
                {
                    Message mouse = Message.Create(Handle, 0xa1, new IntPtr(2), IntPtr.Zero); // 0xA1 - WM_NCLBUTTONDOWN (Posted when the user presses the left mouse button while the cursor is within the nonclient area of a window) | new IntPtr(2) - HTCAPTION (We're making system aware that we have pressed LMB in window title area) | IntPtr.Zero - lParam (Unused in our scenario)
                    WndProc(ref mouse);
                }));
            });
        }




        private void button_UpdateDebug_MouseClick(object sender, MouseEventArgs e)
        {
            textBox_Culture.Text = ProgramGlobals.culture.TwoLetterISOLanguageName ?? "NONE";
            textBox_Platform.Text = ProgramSession.Game.Platform.currentPlatform.ToString() ?? "NONE";
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
            if (ProgramFeatures.QueueNotify.GetSelectedNotifySound() != ProgramFeatures.QueueNotify.E_NotifySounds.none)
            {
                button_QueueNotifySound.Visible = true;
            }
            else
            {
                button_QueueNotifySound.Visible = false;
            }

            Media.StopSound();
        }




        private void button_ClearCertificates_MouseClick(object sender, MouseEventArgs e)
        {
            if (FiddlerCore.DestroyRootCertificates() == true)
            {
                Messaging.ShowMessage("Root Certificates have been successfully removed from your PC.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                applicationRestartRequired = true;
            }
            else
                Messaging.ShowMessage("Something went wrong in process of Root Certificates removal!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void button_DisableProxy_MouseClick(object sender, MouseEventArgs e)
        {
            if (WinReg.DisableProxy() == true)
            {
                Media.PlaySoundFromStream(Properties.Resources.SFX_Activate);
            }
            else
            {
                Messaging.ShowMessage($"Cursed Market failed to disable proxy!");
            }
        }
        private void button_CreateCertificatePassword_MouseClick(object sender, MouseEventArgs e)
        {
            if (ProgramNetworking.WritePasswordFile(true) == false)
                Messaging.ShowMessage($"Failed To Write Certificate Password File!\nCertificate Password: {ProgramNetworking.password}", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }
        private void button_OpenCertificateDirectory_MouseClick(object sender, MouseEventArgs e)
        {
            if (Directory.Exists(ProgramPaths.GetDataFolderPath()))
                Process.Start(ProgramPaths.GetDataFolderPath());
            else
                Messaging.ShowMessage($"Failed To Open Cursed Market Data Folder!\nCursed Market Must be Started At Least Once To Create Data Folder.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }
        private void button_ClearContentCache_MouseClick(object sender, MouseEventArgs e)
        {
            if (Game.ClearContentCache() == false)
                Messaging.ShowMessage($"Cursed Market failed to clear content cache!\n\n\"{ProgramPaths.contentCacheDirectoryPath}\"", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            else
                Media.PlaySoundFromStream(Properties.Resources.SFX_Activate);
        }
        private void button_SettingsReset_MouseClick(object sender, MouseEventArgs e)
        {
            DialogResult settingsResetDialogueResult = Messaging.ShowDialog("This action will lead to all locally stored settings to be removed, data will be irretrievably lost!\n\nDo you want to proceed anyways?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (settingsResetDialogueResult == DialogResult.Yes)
            {
                if (FiddlerCore.IsRunning() == true)
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
            if (comboBox_DisabledPlatforms.Items.Count > 0)
            {
                List<ProgramSession.Game.Platform.E_GamePlatform> platforms = new List<ProgramSession.Game.Platform.E_GamePlatform>(ProgramSession.Game.Platform.limitedPlatforms);
                platforms.RemoveAt(comboBox_DisabledPlatforms.SelectedIndex);

                ProgramSession.Game.Platform.limitedPlatforms = platforms;
                ObtainLimitedPlatforms();
            }
        }
        



        private void pictureBox_Discord_Click(object sender, EventArgs e)            => Process.Start(CursedAPI.SocialLinks.discord);
        private void pictureBox_Telegram_MouseClick(object sender, MouseEventArgs e) => Process.Start(CursedAPI.SocialLinks.telegram);
        private void pictureBox_Boosty_MouseClick(object sender, MouseEventArgs e)   => Process.Start(CursedAPI.SocialLinks.boosty);
    }
}
