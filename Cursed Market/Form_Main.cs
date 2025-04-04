using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cursed_Market
{
    public partial class Form_Main : Form
    {
        private void OnNotifyIconClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                notifyIcon.Visible = false;
            }
        }
        private void OnMessageShowClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }
        private void OnMessageCloseClick(object sender, EventArgs e)
        {
            Globals.Application.Close();
        }




        public Form_Main()
        {
            InitializeComponent();
            InitializeSettings();
        }
        private void Form_Main_Load(object sender, EventArgs e)
        {
            Globals_Cache.Forms.Main = this; // Getting form cached allows us to interact with it from any class, at any time given and avoid situations where we are forced to create a new instance of the Form due to variable being lost.




            Globals.Application.CheckForFirstLaunch();




            notifyIcon.Icon = Properties.Resources.Icon;
            notifyIcon.MouseClick += new MouseEventHandler(OnNotifyIconClick);

            contextMenuStrip.Items.Add(Properties.Localization.MAIN_NOTIFYICON_Show, null, OnMessageShowClick);
            contextMenuStrip.Items.Add(Properties.Localization.MAIN_NOTIFYICON_Close, null, OnMessageCloseClick);




#if DEBUG
            button_DebugButton.Visible = true;
#endif


            label_QueueStatus.Text = GetRandomGreetingsText();


            if (Globals.Application.startupArguments.Contains(Globals.Application.SE_CommonStartupArguments.offlineMode))
            {
                InitializeOffline();
                return;
            }

            if (CursedAPI.HeartBeat() == false)
            {
                if (Messaging.ShowDialog("Cursed Market failed to connect to it's web services! It's likely that you're facing one of these scenarios:\n1) ISP blacklisted Cursed Market services;\n2) Cursed Market API is currently on maintance.\n\nWould you like to open Cursed Market API status checked in your internet browser? The page should say \"OK\" if services are up and you have access to them.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start(CursedAPI.SE_CommonEndpoints.heartBeat);
                }

                InitializeOffline();
                return;
            }

            InitializeOnline();
        }
        private void Form_Main_Shown(object sender, EventArgs e) // We only want to make crosshair visibly when Form has already been loaded! If we do that too early, white square will appear in middle of the screen due to Crosshair Form wasn't yet intialized.
        {
            SetCrosshairVisibility(Globals.Crosshair.selectedCrosshair != Globals.Crosshair.E_Crosshairs.none);
        }




        public void ReloadTheme()
        {
            switch (Globals.Application.Theme.selectedTheme)
            {
                default:
                    pictureBox_WindowBorder.Visible = true;
                    pictureBox_Title.Image = Properties.Resources.IMG_BANNER_BLACK;
                    pictureBox_Button_Settings.Image = Properties.Resources.IMG_BUTTON_SETTINGS_BLACK;
                    pictureBox_Button_CloudIDFriend.Image = Properties.Resources.IMG_BUTTON_ADDFRIEND_BLACK;
                    pictureBox_Button_CharactersPreset.Image = Properties.Resources.IMG_BUTTON_CHARACTERSPRESET_BLACK;
                    this.BackColor = Color.White;
                    panel_WindowHeader.BackColor = SystemColors.Control;
                    label_GameChangersTitle.ForeColor = Color.Black;
                    label_CustomizationsKingTitle.ForeColor = Color.Black;
                    label_MainFeaturesTitle.ForeColor = Color.Black;
                    label_QueueStatus.ForeColor = Color.Black;
                    label_CurrenciesAdjustTitle.ForeColor = Color.Black;
                    label_AuthorWaterMark.ForeColor = Color.Gainsboro;
                    label_CrosshairOpacityPercent.ForeColor = Color.Black;
                    label_AntiKillSwitchTitle.ForeColor = Color.Black;
                    label_VersionWaterMark.ForeColor = Color.Gainsboro;
                    label_CustomizationsKingDescription.ForeColor = SystemColors.ControlText;
                    label_AntiKillSwitchDescription.ForeColor = SystemColors.ControlText;
                    label_CrosshairTitle.ForeColor = Color.Black;
                    label_CrosshairOpacityTitle.ForeColor = Color.Black;
                    MainCheckBox_01.ForeColor = Color.Black;
                    MainCheckBox_02.ForeColor = Color.Black;
                    MainCheckBox_03.ForeColor = Color.Black;
                    MainCheckBox_04.ForeColor = Color.Black;
                    MainCheckBox_05.ForeColor = Color.Black;
                    MainCheckBox_06.ForeColor = Color.Black;
                    MainCheckBox_07.ForeColor = Color.Black;
                    button_Start.BackColor = Color.Black;
                    button_Start.ForeColor = Color.White;
                    button_WindowTray.BackColor = Color.DarkGray;
                    button_BhvrSessionCopy.BackColor = Color.DimGray;
                    break;

                case Globals.Application.Theme.E_Themes.legacy:
                    pictureBox_WindowBorder.Visible = false;
                    pictureBox_Title.Image = Properties.Resources.IMG_BANNER_WHITE;
                    pictureBox_Button_Settings.Image = Properties.Resources.IMG_BUTTON_SETTINGS_WHITE;
                    pictureBox_Button_CloudIDFriend.Image = Properties.Resources.IMG_BUTTON_ADDFRIEND_WHITE;
                    pictureBox_Button_CharactersPreset.Image = Properties.Resources.IMG_BUTTON_CHARACTERSPRESET_WHITE;
                    this.BackColor = Color.FromArgb(255, 46, 51, 73);
                    panel_WindowHeader.BackColor = Color.FromArgb(255, 24, 30, 54);
                    label_GameChangersTitle.ForeColor = Color.White;
                    label_CustomizationsKingTitle.ForeColor = Color.White;
                    label_MainFeaturesTitle.ForeColor = Color.White;
                    label_QueueStatus.ForeColor = Color.White;
                    label_CurrenciesAdjustTitle.ForeColor = Color.White;
                    label_AuthorWaterMark.ForeColor = Color.DimGray;
                    label_CrosshairOpacityPercent.ForeColor = Color.White;
                    label_AntiKillSwitchTitle.ForeColor = Color.White;
                    label_VersionWaterMark.ForeColor = Color.DimGray;
                    label_CustomizationsKingDescription.ForeColor = Color.DimGray;
                    label_AntiKillSwitchDescription.ForeColor = Color.DimGray;
                    label_CrosshairTitle.ForeColor = Color.White;
                    label_CrosshairOpacityTitle.ForeColor = Color.White;
                    MainCheckBox_01.ForeColor = Color.White;
                    MainCheckBox_02.ForeColor = Color.White;
                    MainCheckBox_03.ForeColor = Color.White;
                    MainCheckBox_04.ForeColor = Color.White;
                    MainCheckBox_05.ForeColor = Color.White;
                    MainCheckBox_06.ForeColor = Color.White;
                    MainCheckBox_07.ForeColor = Color.White;
                    button_Start.BackColor = Color.IndianRed;
                    button_Start.ForeColor = Color.White;
                    button_WindowTray.BackColor = Color.SlateBlue;
                    button_BhvrSessionCopy.BackColor = Color.RoyalBlue;
                    break;

                case Globals.Application.Theme.E_Themes.darkMemories:
                    pictureBox_WindowBorder.Visible = false;
                    pictureBox_Title.Image = Properties.Resources.IMG_BANNER_WHITE;
                    pictureBox_Button_Settings.Image = Properties.Resources.IMG_BUTTON_SETTINGS_WHITE;
                    pictureBox_Button_CloudIDFriend.Image = Properties.Resources.IMG_BUTTON_ADDFRIEND_WHITE;
                    pictureBox_Button_CharactersPreset.Image = Properties.Resources.IMG_BUTTON_CHARACTERSPRESET_WHITE;
                    this.BackColor = Color.FromArgb(255, 44, 47, 51);
                    panel_WindowHeader.BackColor = Color.FromArgb(255, 35, 39, 42);
                    label_GameChangersTitle.ForeColor = Color.White;
                    label_CustomizationsKingTitle.ForeColor = Color.White;
                    label_MainFeaturesTitle.ForeColor = Color.White;
                    label_QueueStatus.ForeColor = Color.White;
                    label_CurrenciesAdjustTitle.ForeColor = Color.White;
                    label_AuthorWaterMark.ForeColor = Color.DimGray;
                    label_CrosshairOpacityPercent.ForeColor = Color.White;
                    label_AntiKillSwitchTitle.ForeColor = Color.White;
                    label_VersionWaterMark.ForeColor = Color.DimGray;
                    label_CustomizationsKingDescription.ForeColor = Color.DimGray;
                    label_AntiKillSwitchDescription.ForeColor = Color.DimGray;
                    label_CrosshairTitle.ForeColor = Color.White;
                    label_CrosshairOpacityTitle.ForeColor = Color.White;
                    MainCheckBox_01.ForeColor = Color.White;
                    MainCheckBox_02.ForeColor = Color.White;
                    MainCheckBox_03.ForeColor = Color.White;
                    MainCheckBox_04.ForeColor = Color.White;
                    MainCheckBox_05.ForeColor = Color.White;
                    MainCheckBox_06.ForeColor = Color.White;
                    MainCheckBox_07.ForeColor = Color.White;
                    button_Start.BackColor = Color.FromArgb(255, 65, 65, 65);
                    button_Start.ForeColor = Color.White;
                    button_WindowTray.BackColor = Color.SlateBlue;
                    button_BhvrSessionCopy.BackColor = Color.FromArgb(255, 85, 85, 85);
                    break;

                case Globals.Application.Theme.E_Themes.saintsRow:
                    pictureBox_WindowBorder.Visible = false;
                    pictureBox_Title.Image = Properties.Resources.IMG_BANNER_WHITE;
                    pictureBox_Button_Settings.Image = Properties.Resources.IMG_BUTTON_SETTINGS_WHITE;
                    pictureBox_Button_CloudIDFriend.Image = Properties.Resources.IMG_BUTTON_ADDFRIEND_WHITE;
                    pictureBox_Button_CharactersPreset.Image = Properties.Resources.IMG_BUTTON_CHARACTERSPRESET_WHITE;
                    this.BackColor = Color.FromArgb(255, 37, 13, 57);
                    panel_WindowHeader.BackColor = Color.FromArgb(255, 55, 20, 86);
                    label_GameChangersTitle.ForeColor = Color.White;
                    label_CustomizationsKingTitle.ForeColor = Color.White;
                    label_MainFeaturesTitle.ForeColor = Color.White;
                    label_QueueStatus.ForeColor = Color.White;
                    label_CurrenciesAdjustTitle.ForeColor = Color.White;
                    label_AuthorWaterMark.ForeColor = Color.DimGray;
                    label_CrosshairOpacityPercent.ForeColor = Color.White;
                    label_AntiKillSwitchTitle.ForeColor = Color.White;
                    label_VersionWaterMark.ForeColor = Color.DimGray;
                    label_CustomizationsKingDescription.ForeColor = Color.DimGray;
                    label_AntiKillSwitchDescription.ForeColor = Color.DimGray;
                    label_CrosshairTitle.ForeColor = Color.White;
                    label_CrosshairOpacityTitle.ForeColor = Color.White;
                    MainCheckBox_01.ForeColor = Color.White;
                    MainCheckBox_02.ForeColor = Color.White;
                    MainCheckBox_03.ForeColor = Color.White;
                    MainCheckBox_04.ForeColor = Color.White;
                    MainCheckBox_05.ForeColor = Color.White;
                    MainCheckBox_06.ForeColor = Color.White;
                    MainCheckBox_07.ForeColor = Color.White;
                    button_Start.BackColor = Color.FromArgb(255, 89, 67, 218);
                    button_Start.ForeColor = Color.White;
                    button_WindowTray.BackColor = Color.SlateBlue;
                    button_BhvrSessionCopy.BackColor = Color.FromArgb(255, 118, 93, 222);
                    break;

                case Globals.Application.Theme.E_Themes.dracula:
                    pictureBox_WindowBorder.Visible = false;
                    pictureBox_Title.Image = Properties.Resources.IMG_BANNER_WHITE;
                    pictureBox_Button_Settings.Image = Properties.Resources.IMG_BUTTON_SETTINGS_WHITE;
                    pictureBox_Button_CloudIDFriend.Image = Properties.Resources.IMG_BUTTON_ADDFRIEND_WHITE;
                    pictureBox_Button_CharactersPreset.Image = Properties.Resources.IMG_BUTTON_CHARACTERSPRESET_WHITE;
                    this.BackColor = Color.FromArgb(255, 40, 42, 54);
                    panel_WindowHeader.BackColor = Color.FromArgb(255, 68, 71, 90);
                    label_GameChangersTitle.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    label_CustomizationsKingTitle.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    label_MainFeaturesTitle.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    label_QueueStatus.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    label_CurrenciesAdjustTitle.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    label_AuthorWaterMark.ForeColor = Color.FromArgb(255, 98, 114, 164);
                    label_CrosshairOpacityPercent.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    label_AntiKillSwitchTitle.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    label_VersionWaterMark.ForeColor = Color.FromArgb(255, 98, 114, 164);
                    label_CustomizationsKingDescription.ForeColor = Color.FromArgb(255, 98, 114, 164);
                    label_AntiKillSwitchDescription.ForeColor = Color.FromArgb(255, 98, 114, 164);
                    label_CrosshairTitle.ForeColor = Color.White;
                    label_CrosshairOpacityTitle.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    MainCheckBox_01.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    MainCheckBox_02.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    MainCheckBox_03.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    MainCheckBox_04.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    MainCheckBox_05.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    MainCheckBox_06.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    MainCheckBox_07.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    button_Start.BackColor = Color.FromArgb(255, 89, 67, 218);
                    button_Start.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    button_WindowTray.BackColor = Color.SlateBlue;
                    button_BhvrSessionCopy.BackColor = Color.FromArgb(255, 118, 93, 222);
                    break;

                case Globals.Application.Theme.E_Themes.christmas:
                    pictureBox_WindowBorder.Visible = false;
                    pictureBox_Title.Image = Properties.Resources.IMG_BANNER_CHRISTMAS2022;
                    pictureBox_Button_Settings.Image = Properties.Resources.IMG_BUTTON_SETTINGS_WHITE;
                    pictureBox_Button_CloudIDFriend.Image = Properties.Resources.IMG_BUTTON_ADDFRIEND_WHITE;
                    pictureBox_Button_CharactersPreset.Image = Properties.Resources.IMG_BUTTON_CHARACTERSPRESET_WHITE;
                    this.BackColor = Color.FromArgb(255, 24, 24, 24);
                    panel_WindowHeader.BackColor = Color.FromArgb(255, 14, 14, 14);
                    label_GameChangersTitle.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    label_CustomizationsKingTitle.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    label_MainFeaturesTitle.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    label_QueueStatus.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    label_CurrenciesAdjustTitle.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    label_AuthorWaterMark.ForeColor = Color.FromArgb(255, 170, 142, 87);
                    label_CrosshairOpacityPercent.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    label_AntiKillSwitchTitle.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    label_VersionWaterMark.ForeColor = Color.FromArgb(255, 170, 142, 87);
                    label_CustomizationsKingDescription.ForeColor = Color.FromArgb(255, 170, 142, 87);
                    label_AntiKillSwitchDescription.ForeColor = Color.FromArgb(255, 170, 142, 87);
                    label_CrosshairTitle.ForeColor = Color.White;
                    label_CrosshairOpacityTitle.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    MainCheckBox_01.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    MainCheckBox_02.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    MainCheckBox_03.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    MainCheckBox_04.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    MainCheckBox_05.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    MainCheckBox_06.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    MainCheckBox_07.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    button_Start.BackColor = Color.FromArgb(255, 221, 34, 85);
                    button_Start.ForeColor = Color.White;
                    button_WindowTray.BackColor = Color.SlateBlue;
                    button_BhvrSessionCopy.BackColor = Color.FromArgb(255, 142, 22, 55);
                    break;
            }
        }




        private void InitializeSettings()
        {
            ReloadTheme();
            label_VersionWaterMark.Text = $"{Globals.Application.version[0]}.{Globals.Application.version[1]}.{Globals.Application.version[2]}.{Globals.Application.version[3]}";




            comboBox_Crosshairs.SelectedIndex = (int)Globals.Crosshair.selectedCrosshair;
            trackBar_CrosshairOpacity.Value = (Globals.Crosshair.opacity / 10).Clamp(trackBar_CrosshairOpacity.Minimum, trackBar_CrosshairOpacity.Maximum); // Clamp value between minimum & maximum trackBar can handle.
            SetCrosshairOpacity(Globals.Crosshair.opacity);




            #region localization
            button_Start.Text = Properties.Localization.MAIN_Start;

            label_GameChangersTitle.Text = Properties.Localization.MAIN_GameChangersTitle;

            label_CustomizationsKingTitle.Text = Properties.Localization.MAIN_CustomizationsKingTitle;
            label_CustomizationsKingDescription.Text = Properties.Localization.MAIN_CustomizationsKingDescription;

            label_AntiKillSwitchTitle.Text = Properties.Localization.MAIN_AntiKillSwitchTitle;
            label_AntiKillSwitchDescription.Text = Properties.Localization.MAIN_AntiKillSwitchDescription;



            label_MainFeaturesTitle.Text = Properties.Localization.MAIN_MainFeaturesTitle;

            MainCheckBox_01.Text = Properties.Localization.MAIN_MainCheckBox_01;
            MainCheckBox_02.Text = Properties.Localization.MAIN_MainCheckBox_02;
            MainCheckBox_03.Text = Properties.Localization.MAIN_MainCheckBox_03;
            MainCheckBox_04.Text = Properties.Localization.MAIN_MainCheckBox_04;
            MainCheckBox_05.Text = Properties.Localization.MAIN_MainCheckBox_05;
            MainCheckBox_06.Text = Properties.Localization.MAIN_MainCheckBox_06;
            MainCheckBox_07.Text = Properties.Localization.MAIN_MainCheckBox_07;

            label_CurrenciesAdjustTitle.Text = Properties.Localization.MAIN_CurrenciesAdjustTitle;

            label_CrosshairTitle.Text = Properties.Localization.MAIN_CrosshairTitle;
            label_CrosshairOpacityTitle.Text = Properties.Localization.MAIN_CrosshairOpacityTitle;

            textBox_BhvrSession.Text = Properties.Localization.MAIN_BhvrSession;
            #endregion
        }



        private void button_WindowClose_Click(object sender, EventArgs e) => this.Close();
        private void button_WindowMinimize_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;
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
        private void button_WindowTray_MouseClick(object sender, MouseEventArgs e)
        {
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            notifyIcon.Visible = true;
        }




        public void InitializeOffline()
        {
            Globals.Application.offlineMode = true;

            SetGameChangerStatus(CursedAPI.E_GameChangers.customizationsKing, CursedAPI.E_GameChangerStatus.Offline);
            SetGameChangerStatus(CursedAPI.E_GameChangers.antiKillSwitch, CursedAPI.E_GameChangerStatus.Offline);
            MainCheckBox_05.Enabled = false; // Game changers already are offline and unavailable, there's no need in keeping an option to disable / enable them.

            CursedAPI.ResponseFiles.market = CursedAPI.GetMarketFile(CursedAPI.E_ActionMode.Offline);

            Globals_Cache.CursedAPI.CharacterData.data = CursedAPI.GetCharacterData(CursedAPI.E_ActionMode.Offline);
            Globals_Cache.CursedAPI.bloodWebData = CursedAPI.GetBloodWebData(CursedAPI.E_ActionMode.Offline);
            if (string.IsNullOrEmpty(Globals_Cache.CursedAPI.CharacterData.data) || string.IsNullOrEmpty(Globals_Cache.CursedAPI.bloodWebData))
            {
                MainCheckBox_03.Enabled = false;
                MainCheckBox_04.Enabled = false;
            }

            Globals_Cache.CursedAPI.charactersList = CursedAPI.GetCharactersList(CursedAPI.E_ActionMode.Offline);
            Globals_Cache.CursedAPI.itemsList = CursedAPI.GetItemsList(CursedAPI.E_ActionMode.Offline);

            Globals.Application.initialized = true;
        }
        public void InitializeOnline()
        {
            CursedAPI.VersionCheck();

            CursedAPI.ResponseFiles.market = CursedAPI.GetMarketFile(CursedAPI.E_ActionMode.Online);

            if (Globals.Application.HasStartupArgument(Globals.Application.SE_CommonStartupArguments.noCharacterData))
            {
                Globals_Cache.CursedAPI.CharacterData.data = CursedAPI.GetCharacterData(CursedAPI.E_ActionMode.Offline);
                Globals_Cache.CursedAPI.bloodWebData = CursedAPI.GetBloodWebData(CursedAPI.E_ActionMode.Offline);
            }
            else
            {
                Globals_Cache.CursedAPI.CharacterData.data = CursedAPI.GetCharacterData(CursedAPI.E_ActionMode.Online);
                Globals_Cache.CursedAPI.bloodWebData = CursedAPI.GetBloodWebData(CursedAPI.E_ActionMode.Online);
            }


            Globals_Cache.CursedAPI.charactersList = CursedAPI.GetCharactersList(CursedAPI.E_ActionMode.Online);
            Globals_Cache.CursedAPI.itemsList = CursedAPI.GetItemsList(CursedAPI.E_ActionMode.Online);


            CursedAPI.ObtainCatalog();
            CursedAPI.ObtainAntiKillSwitch();


            Globals.Application.initialized = true;
        }




        public void SetGameChangerStatus(CursedAPI.E_GameChangers gameChanger, CursedAPI.E_GameChangerStatus newStatus)
        {
            Label statusLabel = null;
            switch (gameChanger)
            {
                case CursedAPI.E_GameChangers.customizationsKing:
                    statusLabel = label_CustomizationsKingTitle;
                    break;

                case CursedAPI.E_GameChangers.antiKillSwitch:
                    statusLabel =  label_AntiKillSwitchTitle;
                    break;
            }

            string statusText = statusLabel.Text;
            string cleanStatusText = statusText.Substring(0, statusText.IndexOf(':') + 1).TrimEnd();

            switch (newStatus)
            {
                case CursedAPI.E_GameChangerStatus.Enabled:
                    statusLabel.Text = $"{cleanStatusText} {Properties.Localization.MAIN_GAMECHANGERSTATUS_CurrentlyInUse}";
                    break;

                case CursedAPI.E_GameChangerStatus.Failed:
                    statusLabel.Text = $"{cleanStatusText} {Properties.Localization.MAIN_GAMECHANGERSTATUS_FailedToObtain}";
                    break;

                case CursedAPI.E_GameChangerStatus.Disabled:
                    statusLabel.Text = $"{cleanStatusText} {Properties.Localization.MAIN_GAMECHANGERSTATUS_Disabled}";
                    break;

                case CursedAPI.E_GameChangerStatus.Offline:
                    statusLabel.Text = $"{cleanStatusText} {Properties.Localization.MAIN_GAMECHANGERSTATUS_Offline}";
                    break;
            }

        }
        public void SetMainFunctionEnabled(CursedAPI.E_MainFunctions mainFunction, bool enabled)
        {
            switch (mainFunction)
            {
                case CursedAPI.E_MainFunctions.MainFunction_01:
                    MainCheckBox_01.Enabled = enabled;
                    break;

                case CursedAPI.E_MainFunctions.MainFunction_02:
                    MainCheckBox_02.Enabled = enabled;
                    break;

                case CursedAPI.E_MainFunctions.MainFunction_03:
                    MainCheckBox_03.Enabled = enabled;
                    break;

                case CursedAPI.E_MainFunctions.MainFunction_04:
                    MainCheckBox_04.Enabled = enabled;
                    break;

                case CursedAPI.E_MainFunctions.MainFunction_05:
                    MainCheckBox_05.Enabled = enabled;
                    break;

                case CursedAPI.E_MainFunctions.MainFunction_06:
                    MainCheckBox_06.Enabled = enabled;
                    break;

                case CursedAPI.E_MainFunctions.MainFunction_07:
                    MainCheckBox_07.Enabled = enabled;
                    break;
            }
        }




        private void pictureBox_Buttons_Settings_MouseClick(object sender, MouseEventArgs e) => Globals_Cache.Forms.Settings.ShowDialog();
        private void pictureBox_Button_CloudIDFriend_MouseClick(object sender, MouseEventArgs e) => Globals_Cache.Forms.CloudIDFriend.ShowDialog();
        private void pictureBox_Button_CharactersPreset_MouseClick(object sender, MouseEventArgs e) => Globals_Cache.Forms.CharactersPreset.ShowDialog();
        private void button_Start_MouseClick(object sender, MouseEventArgs e)
        {
            if (Globals.Application.initialized == false)
                return;

            if (FiddlerCore.Start() == false)
            {
                Messaging.ShowMessage("Cursed Market failed to start proxy instance!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }

            Globals_Cache.Forms.TimerForm.Show();
            button_Start.Visible = false;
        }




        private void MainCheckBox_01_CheckedChanged(object sender, EventArgs e)
        {
            if (MainCheckBox_01.Checked == true)
            {
                Globals_Cache.Forms.Queue.Show();
            }
            else
            {
                Globals_Cache.Forms.Queue.Hide();
            }
        }
        private void MainCheckBox_02_CheckedChanged(object sender, EventArgs e)
        {
            Globals.FiddlerCoreTunables.CurrencySpoof.enabled = MainCheckBox_02.Checked;
            label_CurrenciesAdjustTitle.Visible = MainCheckBox_02.Checked;

            pictureBox_BloodPoints.Visible = MainCheckBox_02.Checked;
            textBox_BloodPoints.Visible = MainCheckBox_02.Checked;

            pictureBox_IridescentShards.Visible = MainCheckBox_02.Checked;
            textBox_IridescentShards.Visible = MainCheckBox_02.Checked;

            pictureBox_AuricCells.Visible = MainCheckBox_02.Checked;
            textBox_AuricCells.Visible = MainCheckBox_02.Checked;
        }
        private void MainCheckBox_03_CheckedChanged(object sender, EventArgs e)
        {
            bool wasChecked = MainCheckBox_03.Checked;


            Globals.FiddlerCoreTunables.CharacterData.enabled = wasChecked;
            if (wasChecked)
            {
                Messaging.ShowMessage(Properties.Localization.WARNING_CharacterOwnership, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MainCheckBox_04.Checked = false;
            }

            pictureBox_CharactersPresetDisabled.Visible = !wasChecked; // Show when checkBox isn't checked and hide when it's checked.
            pictureBox_Button_CharactersPreset.Visible = wasChecked; // Show when checkBox is checked and hide when it's not.

            MainCheckBox_04.Enabled = wasChecked;
            MainCheckBox_04.Visible = wasChecked;
        }
        private void MainCheckBox_04_OnCheckedChanged(object sender, EventArgs e)
        {
            if (MainCheckBox_04.Checked == true)
            {
                if (Globals.CharactersPreset.Obtain() == false)
                {
                    Messaging.ShowMessage(Properties.Localization.MESSAGE_FailedToReadCharactersPreset, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    MainCheckBox_04.Checked = false;
                    return;
                }


                Globals.CharactersPreset.SetEnabled(true);
            }
            else
            {
                Globals.CharactersPreset.SetEnabled(false);
            }
        }
        private void MainCheckBox_05_CheckedChanged(object sender, EventArgs e)
        {
            if (MainCheckBox_05.Checked == true)
            {
                SetGameChangerStatus(CursedAPI.E_GameChangers.customizationsKing, CursedAPI.E_GameChangerStatus.Disabled);
                Globals.FiddlerCoreTunables.Catalog.enabled = false;

                SetGameChangerStatus(CursedAPI.E_GameChangers.antiKillSwitch, CursedAPI.E_GameChangerStatus.Disabled);
                Globals.FiddlerCoreTunables.AntiKillSwitch.enabled = false;
            }
            else
            {
                if (CursedAPI.ResponseFiles.catalog != null)
                {
                    SetGameChangerStatus(CursedAPI.E_GameChangers.customizationsKing, CursedAPI.E_GameChangerStatus.Enabled);
                    Globals.FiddlerCoreTunables.Catalog.enabled = true;
                }
                else
                {
                    SetGameChangerStatus(CursedAPI.E_GameChangers.customizationsKing, CursedAPI.E_GameChangerStatus.Failed);
                    Globals.FiddlerCoreTunables.Catalog.enabled = false;
                }


                if (CursedAPI.ResponseFiles.antiKillSwitch != null)
                {
                    SetGameChangerStatus(CursedAPI.E_GameChangers.antiKillSwitch, CursedAPI.E_GameChangerStatus.Enabled);
                    Globals.FiddlerCoreTunables.AntiKillSwitch.enabled = true;
                }
                else
                {
                    SetGameChangerStatus(CursedAPI.E_GameChangers.antiKillSwitch, CursedAPI.E_GameChangerStatus.Failed);
                    Globals.FiddlerCoreTunables.AntiKillSwitch.enabled = false;
                }
            }
        }
        private void MainCheckBox_06_OnCheckedChanged(object sender, EventArgs e)
        {
            bool wasChecked = MainCheckBox_06.Checked;

            if (Globals.Application.offlineMode == true)
            {
                CursedAPI.ResponseFiles.market = CursedAPI.GetMarketFile(CursedAPI.E_ActionMode.Offline, wasChecked);
            }
            else
            {
                CursedAPI.ResponseFiles.market = CursedAPI.GetMarketFile(CursedAPI.E_ActionMode.Online, wasChecked);
            }

            if (FiddlerCore.IsRunning() == true)
            {
                if (Globals.Game.IsRunning() == true)
                {
                    if (Messaging.ShowDialog("Swapping Market File on the run will likely lead to catastrophic results, it's required to restart the game!\n\nRestart the game now?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        Globals.Game.Exit();
                }
            }
        }
        private void MainCheckBox_07_CheckedChanged(object sender, EventArgs e)
        {
            Globals.FiddlerCoreTunables.GuaranteedQuests.enabled = MainCheckBox_07.Checked;
        }




        private void UpdateCrosshair()
        {
            Globals_Cache.Forms.Crosshair.ForceInitializeSettings();
        }
        private void SetCrosshairVisibility(bool newVisibility)
        {
            if (newVisibility == true)
            {
                UpdateCrosshair(); // We want to update crosshair before we show it up
                Globals_Cache.Forms.Crosshair.Show();
            }
            else
                Globals_Cache.Forms.Crosshair.Hide();

        }
        private void SetCrosshairOpacity(int newOpacity)
        {
            if (WinReg.SetData_DWORD(WinReg.SE_CommonEntries.crosshairOpacity, newOpacity))
            {
                Globals.Crosshair.opacity = newOpacity;
                label_CrosshairOpacityPercent.Text = $"{newOpacity}%";

                UpdateCrosshair();
            }
        }
        



        private static bool isKeypressDigit(KeyPressEventArgs e, string currentstring)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 127)
                return false;
            else if (currentstring != "")
            {
                if (currentstring[0] == '0')
                {
                    if (e.KeyChar >= 49 && e.KeyChar <= 57 && e.KeyChar != 8)
                        return false;
                }
            }
            return true;
        }
        private void textBox_BloodPoints_TextChanged(object sender, EventArgs e) => Globals.FiddlerCoreTunables.CurrencySpoof.bloodpointsAmount = textBox_BloodPoints.Text;
        private void textBox_BloodPoints_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!isKeypressDigit(e, Globals.FiddlerCoreTunables.CurrencySpoof.bloodpointsAmount))
                e.Handled = true;
        }
        private void textBox_IridescentShards_TextChanged(object sender, EventArgs e) => Globals.FiddlerCoreTunables.CurrencySpoof.iridescentShardsAmount = textBox_IridescentShards.Text;
        private void textBox_IridescentShards_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!isKeypressDigit(e, Globals.FiddlerCoreTunables.CurrencySpoof.iridescentShardsAmount))
                e.Handled = true;
        }
        private void textBox_AuricCells_TextChanged(object sender, EventArgs e) => Globals.FiddlerCoreTunables.CurrencySpoof.auricCellsAmount = textBox_AuricCells.Text;
        private void textBox_AuricCells_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!isKeypressDigit(e, Globals.FiddlerCoreTunables.CurrencySpoof.auricCellsAmount))
                e.Handled = true;
        }


        private void button_BhvrSessionCopy_MouseClick(object sender, MouseEventArgs e)
        {
            Clipboard.SetText(textBox_BhvrSession.Text);
            Media.PlaySoundFromStream(Properties.Resources.SFX_Activate);
        }


        private void comboBox_Crosshairs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Globals.Application.initialized == true)
            {
                if (WinReg.SetData_DWORD(WinReg.SE_CommonEntries.crosshair, comboBox_Crosshairs.SelectedIndex))
                {
                    Globals.Crosshair.selectedCrosshair = (Globals.Crosshair.E_Crosshairs)comboBox_Crosshairs.SelectedIndex;
                    SetCrosshairVisibility(comboBox_Crosshairs.SelectedIndex != (int)Globals.Crosshair.E_Crosshairs.none);
                }
            }
        }

        private void trackBar_CrosshairOpacity_ValueChanged(object sender, EventArgs e)
        {
            if (Globals.Application.initialized == true)
            {
                int crosshairOpacity = trackBar_CrosshairOpacity.Value * 10;
                SetCrosshairOpacity(crosshairOpacity);
            }
        }

        public void UpdateBhvrSession() 
        {
            Globals_Cache.Forms.Main.Invoke(new Action(() =>
            {
                textBox_BhvrSession.Width = 439; // What a generous approach <:3
                textBox_BhvrSession.Text = Globals_Session.Game.bhvrSession;

                button_BhvrSessionCopy.Visible = true;
                pictureBox_Button_CloudIDFriend.Visible = true;
            }));
        }

        private string GetRandomGreetingsText() // This function is being executed once at Main Form Load event, at every next found match then.
        {
            List<string> greetingsPhrases = new List<string>()
            {
                Properties.Localization.GREETINGS_01,
                Properties.Localization.GREETINGS_02,
                Properties.Localization.GREETINGS_03,
                Properties.Localization.GREETINGS_04,
                Properties.Localization.GREETINGS_05,
                Properties.Localization.GREETINGS_06,
                Properties.Localization.GREETINGS_07,
                Properties.Localization.GREETINGS_08
            };


            Random rnd = new Random();
            return $">>> {greetingsPhrases[rnd.Next(greetingsPhrases.Count)]} <<<";
        }
        public void LocalUpdateQueue(Queue.E_QueueStatus newQueueStatus, int newQueuePosition)
        {
            switch (newQueueStatus)
            {
                case Queue.E_QueueStatus.MatchStarting:
                    Queue.PlaySound();
                    label_QueueStatus.Text = Properties.Localization.MAIN_QUEUE_MatchStarting;

                    Timer.DelayedLabelUpdate(label_QueueStatus, GetRandomGreetingsText(), 15);
                    break;

                case Queue.E_QueueStatus.LobbyIdle: // There's no unique scenario for LobbyIdle state.
                case Queue.E_QueueStatus.LobbyFound:
                    label_QueueStatus.Text = Properties.Localization.MAIN_QUEUE_LobbyFound;
                    break;

                case Queue.E_QueueStatus.Searching:
                    label_QueueStatus.Text = $"{Properties.Localization.MAIN_QUEUE_QueuePosition} {newQueuePosition}";
                    break;

                case Queue.E_QueueStatus.None:
                    label_QueueStatus.Text = GetRandomGreetingsText();
                    break;
            }
        }

        private void button_DebugButton_MouseClick(object sender, MouseEventArgs e)
        {
#if DEBUG
            Messaging.ShowMessage($"User-ID: {Globals_Session.Game.userId}\nMatch-ID: {Globals_Session.Game.matchId}\nMatch Type: {Globals_Session.Game.matchType}\nPlayer Role: {Globals_Session.Game.playerRole}", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
#endif
        }
    }
}
