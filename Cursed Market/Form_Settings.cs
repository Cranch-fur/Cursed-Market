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
            switch (Globals.Application.Theme.selectedTheme)
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

                case Globals.Application.Theme.E_Themes.legacy:
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

                case Globals.Application.Theme.E_Themes.darkMemories:
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

                case Globals.Application.Theme.E_Themes.saintsRow:
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

                case Globals.Application.Theme.E_Themes.dracula:
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

                case Globals.Application.Theme.E_Themes.christmas:
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


            comboBox_MatchFoundSound.SelectedIndex = (int)Globals.Queue.selectedNotifySound;
        }




        private void button_WindowClose_Click(object sender, EventArgs e)
        {
            if (applicationRestartRequired == true)
            {
                if (Globals.Game.IsRunning())
                {
                    Globals.Game.Exit();
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
            textBox_Culture.Text = Globals.Application.culture.TwoLetterISOLanguageName ?? "NONE";
            textBox_Platform.Text = Globals_Session.Game.Platform.currentPlatform.ToString() ?? "NONE";
        }




        private void comboBox_Themes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_ApplicationTheme.SelectedIndex != (int)Globals.Application.Theme.selectedTheme)
            {
                if (WinReg.SetData_DWORD(WinReg.SE_CommonEntries.applicationTheme, comboBox_ApplicationTheme.SelectedIndex))
                {
                    Globals.Application.Theme.selectedTheme = (Globals.Application.Theme.E_Themes)comboBox_ApplicationTheme.SelectedIndex;
                    Globals.Application.ReloadThemes();
                }
            }
        }
        private void comboBox_QueueNotifySound_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (WinReg.SetData_DWORD(WinReg.SE_CommonEntries.queueNotifySound, comboBox_MatchFoundSound.SelectedIndex))
            {
                Globals.Queue.selectedNotifySound = (Globals.Queue.E_NotifySounds)comboBox_MatchFoundSound.SelectedIndex;
            }
        }
        private void button_QueueNotifySound_MouseClick(object sender, MouseEventArgs e) => Queue.PlaySound();




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
            if (FiddlerCore.RootCertificate.WritePasswordFile())
                Process.Start(FiddlerCore.RootCertificate.passwordFilePath);

            else
                Messaging.ShowMessage($"Failed To Write Certificate Password File!\nCertificate Password: {FiddlerCore.RootCertificate.passwordFilePath}", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }
        private void button_OpenCertificateDirectory_MouseClick(object sender, MouseEventArgs e)
        {
            if (Directory.Exists(Globals.Application.GetDataFolderPath()))
                Process.Start(Globals.Application.GetDataFolderPath());

            else
                Messaging.ShowMessage($"Failed To Open Cursed Market Data Folder!\nCursed Market Must be Started At Least Once To Create Data Folder.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
                    if (File.Exists(FiddlerCore.RootCertificate.filePath))
                        File.Delete(FiddlerCore.RootCertificate.filePath);
                }

                Globals.Application.Restart();
            }
        }


        

        private void ObtainLimitedPlatforms()
        {
            comboBox_DisabledPlatforms.Items.Clear();

            foreach (Globals_Session.Game.Platform.E_GamePlatform limitedPlatform in Globals_Session.Game.Platform.limitedPlatforms)
                comboBox_DisabledPlatforms.Items.Add(limitedPlatform);

            if (comboBox_DisabledPlatforms.Items.Count > 0)
                comboBox_DisabledPlatforms.SelectedIndex = 0;
        }
        private void button_LimitedPlatforms_MouseClick(object sender, MouseEventArgs e)
        {
            if (comboBox_DisabledPlatforms.Items.Count > 0)
            {
                List<Globals_Session.Game.Platform.E_GamePlatform> platforms = new List<Globals_Session.Game.Platform.E_GamePlatform>(Globals_Session.Game.Platform.limitedPlatforms);
                platforms.RemoveAt(comboBox_DisabledPlatforms.SelectedIndex);

                Globals_Session.Game.Platform.limitedPlatforms = platforms;
                ObtainLimitedPlatforms();
            }
        }
        



        private void pictureBox_Discord_Click(object sender, EventArgs e)            => Process.Start(CursedAPI.SocialLinks.discord);
        private void pictureBox_Telegram_MouseClick(object sender, MouseEventArgs e) => Process.Start(CursedAPI.SocialLinks.telegram);
        private void pictureBox_Boosty_MouseClick(object sender, MouseEventArgs e)   => Process.Start(CursedAPI.SocialLinks.boosty);
    }
}
