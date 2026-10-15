using System;
using System.Diagnostics;
using System.Drawing;
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
            ProgramGlobals.Close();
        }




        public Form_Main()
        {
            InitializeComponent();
            InitializeSettings();
        }
        private void Form_Main_Load(object sender, EventArgs e)
        {
            ProgramCache.Forms.Main = this; // Getting form cached allows us to interact with it from any class, at any time given and avoid situations where we are forced to create a new instance of the Form due to variable being lost.
            this.TopMost = true; // We've just started the application and we want it to be shown above any other running, param is set to false on Form_Main_Shown!




            ProgramGlobals.CheckForFirstLaunch();




            notifyIcon.Icon = Properties.Resources.Icon;
            notifyIcon.MouseClick += new MouseEventHandler(OnNotifyIconClick);

            contextMenuStrip.Items.Add(Properties.Localization.MAIN_NOTIFYICON_Show, null, OnMessageShowClick);
            contextMenuStrip.Items.Add(Properties.Localization.MAIN_NOTIFYICON_Close, null, OnMessageCloseClick);




#if DEBUG
            button_DebugButton.Visible = true;
#endif


            label_QueueStatus.Text = GetRandomGreetingsText();


            if (ProgramStartupArguments.HasStartupArgument(ProgramStartupArguments.Common.offlineMode))
            {
                InitializeOffline();
                return;
            }

            if (CursedAPI.Status() == false)
            {
                if (Messaging.ShowDialog("Cursed Market failed to connect to it's web services! It's likely that you're facing one of these scenarios:\n1) ISP blacklisted Cursed Market services;\n2) Cursed Market API is currently on maintance.\n\nWould you like to open Cursed Market API status checked in your internet browser? The page should say \"OK\" if services are up and you have access to them.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo(CursedAPI.SE_CommonEndpoints.status) { UseShellExecute = true });
                }

                InitializeOffline();
                return;
            }

            InitializeOnline();
        }
        private void Form_Main_Shown(object sender, EventArgs e) // We only want to make crosshair visibly when Form has already been loaded! If we do that too early, white square will appear in middle of the screen due to Crosshair Form wasn't yet intialized.
        {
            UpdateCrosshairVisibilityUI();

            if (ProgramStartupArguments.HasStartupArgument(ProgramStartupArguments.Common.timerToggleFeature))
            {
                ProgramCache.Forms.Timer.Show();
            }

            ProgramCache.Forms.Wait?.Hide();
            this.TopMost = false;
        }




        public void ReloadTheme()
        {
            var theme = ProgramThemes.GetCurrentPalette();

            this.BackColor = theme.FormBack;
            panel_WindowHeader.BackColor = theme.HeaderBack;
            pictureBox_WindowBorder.Visible = theme.IsLightTheme;

            pictureBox_Title.Image = theme.BannerImage;
            pictureBox_Button_Settings.Image = theme.SettingsIcon;
            pictureBox_Button_API.Image = theme.APIIcon;
            pictureBox_Button_CloudIDFriend.Image = theme.AddFriendIcon;
            pictureBox_Button_CharactersPreset.Image = theme.CharactersPresetIcon;
            pictureBox_Button_CrosshairSettings.Image = theme.CharactersPresetIcon;

            ProgramThemes.ApplyForeColor(theme.PrimaryText,
                label_GameChangersTitle, label_CustomizationsKingTitle, label_MainFeaturesTitle,
                label_QueueStatus, label_CurrenciesAdjustTitle, label_CrosshairOpacityPercent,
                label_AntiKillSwitchTitle, label_CrosshairOpacityTitle,
                MainCheckBox_01, MainCheckBox_02, MainCheckBox_03, MainCheckBox_04,
                MainCheckBox_05, MainCheckBox_06, MainCheckBox_07);

            label_CrosshairTitle.ForeColor = theme.TitleText;

            Color watermarkColor = theme.IsLightTheme ? Color.Gainsboro : theme.MutedText;
            ProgramThemes.ApplyForeColor(watermarkColor, label_AuthorWaterMark, label_VersionWaterMark);
            ProgramThemes.ApplyForeColor(theme.MutedText, label_CustomizationsKingDescription, label_AntiKillSwitchDescription);

            button_Start.BackColor = theme.PrimaryButtonBack;
            button_Start.ForeColor = theme.PrimaryButtonText;
            button_WindowTray.BackColor = theme.TrayButtonBack;
            button_ApiKeyCopy.BackColor = theme.SecondaryButtonBack;
        }




        private void InitializeSettings()
        {
            ReloadTheme();
            label_VersionWaterMark.Text = $"{ProgramGlobals.version[0]}.{ProgramGlobals.version[1]}.{ProgramGlobals.version[2]}.{ProgramGlobals.version[3]}";

            ProgramFeatures.Crosshair.E_Crosshairs initialCrosshair = ProgramFeatures.Crosshair.GetSelectedCrosshair();
            comboBox_Crosshairs.SelectedIndex = (int)initialCrosshair;
            pictureBox_Button_CrosshairSettings.Visible = initialCrosshair == ProgramFeatures.Crosshair.E_Crosshairs.customizable;

            if (initialCrosshair == ProgramFeatures.Crosshair.E_Crosshairs.none)
            {
                label_CrosshairOpacityTitle.Visible = false;
                trackBar_CrosshairOpacity.Visible = false;
                label_CrosshairOpacityPercent.Visible = false;
            }

            trackBar_CrosshairOpacity.Value = (ProgramFeatures.Crosshair.GetOpacity() / 10).Clamp(trackBar_CrosshairOpacity.Minimum, trackBar_CrosshairOpacity.Maximum); // Clamp value between minimum & maximum trackBar can handle.
            UpdateCrosshairOpacityUI();


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

            textBox_ApiKey.Text = Properties.Localization.MAIN_ApiKey;
            #endregion
        }



        private void button_WindowClose_Click(object sender, EventArgs e) => this.Close();
        private void button_WindowMinimize_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;
        private void panel_WindowHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                panel_WindowHeader.Capture = false;
                Message mouse = Message.Create(Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
                WndProc(ref mouse);
            }
        }
        private void button_WindowTray_MouseClick(object sender, MouseEventArgs e)
        {
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            notifyIcon.Visible = true;
        }




        public void InitializeOffline()
        {
            ProgramGlobals.isOfflineMode = true;

            SetGameChangerStatus(CursedAPI.E_GameChangers.customizationsKing, CursedAPI.E_GameChangerStatus.Offline);
            SetGameChangerStatus(CursedAPI.E_GameChangers.antiKillSwitch, CursedAPI.E_GameChangerStatus.Offline);
            MainCheckBox_05.Enabled = false; // Game changers already are offline and unavailable, there's no need in keeping an option to disable / enable them.

            CursedAPI.ResponseFiles.market = CursedAPI.GetMarketFile(CursedAPI.E_ActionMode.Offline);

            ProgramCache.CursedAPI.CharacterData.data = CursedAPI.GetCharacterData(CursedAPI.E_ActionMode.Offline);
            ProgramCache.CursedAPI.bloodWebData = CursedAPI.GetBloodWebData(CursedAPI.E_ActionMode.Offline);
            if (string.IsNullOrEmpty(ProgramCache.CursedAPI.CharacterData.data) || string.IsNullOrEmpty(ProgramCache.CursedAPI.bloodWebData))
            {
                MainCheckBox_03.Enabled = false;
                MainCheckBox_04.Enabled = false;
            }

            ProgramCache.CursedAPI.charactersList = CursedAPI.GetCharactersList(CursedAPI.E_ActionMode.Offline);
            ProgramCache.CursedAPI.itemsList = CursedAPI.GetItemsList(CursedAPI.E_ActionMode.Offline);

            ProgramGlobals.isInitialized = true;
        }
        public void InitializeOnline()
        {
            CursedAPI.Update();

            CursedAPI.ResponseFiles.market = CursedAPI.GetMarketFile(CursedAPI.E_ActionMode.Online);

            if (ProgramStartupArguments.HasStartupArgument(ProgramStartupArguments.Common.noCharacterData))
            {
                ProgramCache.CursedAPI.CharacterData.data = CursedAPI.GetCharacterData(CursedAPI.E_ActionMode.Offline);
                ProgramCache.CursedAPI.bloodWebData = CursedAPI.GetBloodWebData(CursedAPI.E_ActionMode.Offline);
            }
            else
            {
                ProgramCache.CursedAPI.CharacterData.data = CursedAPI.GetCharacterData(CursedAPI.E_ActionMode.Online);
                ProgramCache.CursedAPI.bloodWebData = CursedAPI.GetBloodWebData(CursedAPI.E_ActionMode.Online);
            }


            ProgramCache.CursedAPI.charactersList = CursedAPI.GetCharactersList(CursedAPI.E_ActionMode.Online);
            ProgramCache.CursedAPI.itemsList = CursedAPI.GetItemsList(CursedAPI.E_ActionMode.Online);


            CursedAPI.ObtainCatalog();
            CursedAPI.ObtainKillSwitch();


            ProgramGlobals.isInitialized = true;
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
                    statusLabel = label_AntiKillSwitchTitle;
                    break;
            }

            string statusText = statusLabel.Text;
            int colonIndex = statusText.IndexOf(':');
            string cleanStatusText = colonIndex >= 0
                ? statusText.Substring(0, colonIndex + 1).TrimEnd()
                : statusText.TrimEnd() + ":";

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
                    MainCheckBox_01.Visible = enabled;
                    break;

                case CursedAPI.E_MainFunctions.MainFunction_02:
                    MainCheckBox_02.Enabled = enabled;
                    MainCheckBox_02.Visible = enabled;
                    break;

                case CursedAPI.E_MainFunctions.MainFunction_03:
                    MainCheckBox_03.Enabled = enabled;
                    MainCheckBox_03.Visible = enabled;
                    break;

                case CursedAPI.E_MainFunctions.MainFunction_04:
                    MainCheckBox_04.Enabled = enabled;
                    MainCheckBox_04.Visible = enabled;
                    break;

                case CursedAPI.E_MainFunctions.MainFunction_05:
                    MainCheckBox_05.Enabled = enabled;
                    MainCheckBox_05.Visible = enabled;
                    break;

                case CursedAPI.E_MainFunctions.MainFunction_06:
                    MainCheckBox_06.Enabled = enabled;
                    MainCheckBox_06.Visible = enabled;
                    break;

                case CursedAPI.E_MainFunctions.MainFunction_07:
                    MainCheckBox_07.Enabled = enabled;
                    MainCheckBox_07.Visible = enabled;
                    break;
            }
        }




        private void pictureBox_Buttons_Settings_MouseClick(object sender, MouseEventArgs e) => ProgramCache.Forms.Settings.ShowDialog();
        private void pictureBox_Button_API_MouseClick(object sender, MouseEventArgs e) => ProgramCache.Forms.APIConfig.ShowDialog();
        private void pictureBox_Button_CloudIDFriend_MouseClick(object sender, MouseEventArgs e) => ProgramCache.Forms.CloudIDFriend.ShowDialog();
        private void pictureBox_Button_CharactersPreset_MouseClick(object sender, MouseEventArgs e) => ProgramCache.Forms.CharactersPreset.ShowDialog();
        private void button_Start_MouseClick(object sender, MouseEventArgs e)
        {
            if (ProgramGlobals.isInitialized == false)
                return;

            bool forceClearContentCache = ProgramStartupArguments.HasStartupArgument(ProgramStartupArguments.Common.forceClearContentCache);
            if (ProgramFeatures.Catalog.enabled || forceClearContentCache)
                if (Game.ClearContentCache("catalog.json") == false)
                    Messaging.ShowMessage($"Cursed Market failed to clear content cache!\n\n\"{ProgramPaths.contentCacheDirectoryPath}\\catalog.json\"", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            if (ProgramFeatures.AntiKillSwitch.enabled || forceClearContentCache)
                if (Game.ClearContentCache("itemsKillswitch.json") == false)
                    Messaging.ShowMessage($"Cursed Market failed to clear content cache!\n\n\"{ProgramPaths.contentCacheDirectoryPath}\\itemsKillswitch.json\"", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            if (FiddlerCore.Start() == false)
            {
                Messaging.ShowMessage("Cursed Market failed to start proxy instance!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }

            button_Start.Visible = false;
        }




        private void MainCheckBox_01_CheckedChanged(object sender, EventArgs e)
        {
            if (MainCheckBox_01.Checked == true)
            {
                ProgramCache.Forms.Queue.Show();
            }
            else
            {
                ProgramCache.Forms.Queue.Hide();
            }
        }
        private void MainCheckBox_02_CheckedChanged(object sender, EventArgs e)
        {
            ProgramFeatures.CurrencySpoof.enabled = MainCheckBox_02.Checked;
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

            ProgramFeatures.CharacterData.enabled = wasChecked;

            pictureBox_CharactersPresetDisabled.Visible = !wasChecked; // Show when checkBox isn't checked and hide when it's checked.
            pictureBox_Button_CharactersPreset.Visible = wasChecked; // Show when checkBox is checked and hide when it's not.

            MainCheckBox_04.Enabled = wasChecked;
            MainCheckBox_04.Visible = wasChecked;
        }
        private void MainCheckBox_04_OnCheckedChanged(object sender, EventArgs e)
        {
            if (MainCheckBox_04.Checked == true)
            {
                if (ProgramFeatures.CharactersPreset.Obtain() == false)
                {
                    Messaging.ShowMessage(Properties.Localization.MESSAGE_FailedToReadCharactersPreset, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    MainCheckBox_04.Checked = false;
                    return;
                }


                ProgramFeatures.CharactersPreset.SetEnabled(true);
            }
            else
            {
                ProgramFeatures.CharactersPreset.SetEnabled(false);
            }
        }
        private void MainCheckBox_05_CheckedChanged(object sender, EventArgs e)
        {
            if (MainCheckBox_05.Checked == true)
            {
                SetGameChangerStatus(CursedAPI.E_GameChangers.customizationsKing, CursedAPI.E_GameChangerStatus.Disabled);
                ProgramFeatures.Catalog.enabled = false;

                SetGameChangerStatus(CursedAPI.E_GameChangers.antiKillSwitch, CursedAPI.E_GameChangerStatus.Disabled);
                ProgramFeatures.AntiKillSwitch.enabled = false;
            }
            else
            {
                if (CursedAPI.KillSwitch.isCatalogDisabled || ProgramStartupArguments.HasStartupArgument(ProgramStartupArguments.Common.noCustomizationsKing))
                {
                    SetGameChangerStatus(CursedAPI.E_GameChangers.customizationsKing, CursedAPI.E_GameChangerStatus.Disabled);
                    ProgramFeatures.Catalog.enabled = false;
                }
                else if (CursedAPI.ResponseFiles.catalog != null)
                {
                    SetGameChangerStatus(CursedAPI.E_GameChangers.customizationsKing, CursedAPI.E_GameChangerStatus.Enabled);
                    ProgramFeatures.Catalog.enabled = true;
                }
                else
                {
                    SetGameChangerStatus(CursedAPI.E_GameChangers.customizationsKing, CursedAPI.E_GameChangerStatus.Failed);
                    ProgramFeatures.Catalog.enabled = false;
                }


                if (CursedAPI.KillSwitch.isAntiKillSwitchDisabled || ProgramStartupArguments.HasStartupArgument(ProgramStartupArguments.Common.noAntiKillSwitch))
                {
                    SetGameChangerStatus(CursedAPI.E_GameChangers.antiKillSwitch, CursedAPI.E_GameChangerStatus.Disabled);
                    ProgramFeatures.AntiKillSwitch.enabled = false;
                }
                else if (CursedAPI.ResponseFiles.killSwitch != null)
                {
                    SetGameChangerStatus(CursedAPI.E_GameChangers.antiKillSwitch, CursedAPI.E_GameChangerStatus.Enabled);
                    ProgramFeatures.AntiKillSwitch.enabled = true;
                }
                else
                {
                    SetGameChangerStatus(CursedAPI.E_GameChangers.antiKillSwitch, CursedAPI.E_GameChangerStatus.Failed);
                    ProgramFeatures.AntiKillSwitch.enabled = false;
                }
            }
        }
        private void MainCheckBox_06_OnCheckedChanged(object sender, EventArgs e)
        {
            bool wasChecked = MainCheckBox_06.Checked;

            if (ProgramGlobals.isOfflineMode == true)
            {
                CursedAPI.ResponseFiles.market = CursedAPI.GetMarketFile(CursedAPI.E_ActionMode.Offline, wasChecked);
            }
            else
            {
                CursedAPI.ResponseFiles.market = CursedAPI.GetMarketFile(CursedAPI.E_ActionMode.Online, wasChecked);
            }

            if (FiddlerCore.IsRunning() == true)
            {
                if (Game.IsRunning() == true)
                {
                    if (Messaging.ShowDialog("Swapping Market File on the run will likely lead to catastrophic results, it's required to restart the game!\n\nRestart the game now?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        Game.Exit();
                }
            }
        }
        private void MainCheckBox_07_CheckedChanged(object sender, EventArgs e)
        {
            ProgramFeatures.GuaranteedQuests.enabled = MainCheckBox_07.Checked;
        }




        private void SetCrosshairVisibility(bool newVisibility)
        {
            if (newVisibility)
                ProgramFeatures.Crosshair.Show();
            else
                ProgramFeatures.Crosshair.Hide();

            ProgramFeatures.Crosshair.Reload();

        }
        private void SetCrosshairOpacity(int newOpacity)
        {
            ProgramFeatures.Crosshair.SetOpacity(newOpacity);
            if (ProgramFeatures.Crosshair.GetOpacity() == newOpacity)
            {
                ProgramFeatures.Crosshair.Reload();
            }
        }
        private void UpdateCrosshairOpacityUI()
        {
            label_CrosshairOpacityPercent.Text = $"{ProgramFeatures.Crosshair.GetOpacity()}%";
        }
        private void UpdateCrosshairVisibilityUI()
        {
            bool shouldBeVisible = ProgramFeatures.Crosshair.GetSelectedCrosshair() != ProgramFeatures.Crosshair.E_Crosshairs.none;
            if (shouldBeVisible)
            {
                trackBar_CrosshairOpacity.Enabled = true;
                SetCrosshairVisibility(true);
                UpdateCrosshairOpacityUI();
            }
            else
            {
                trackBar_CrosshairOpacity.Enabled = false;
                SetCrosshairVisibility(false);
                label_CrosshairOpacityPercent.Text = "0%";
            }
        }




        private static bool IsKeypressDigit(KeyPressEventArgs e)
        {
            /* Only allow numbers and control keys (such as Backspace). */
            return char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar);
        }

        private static void NormalizeCurrencyTextBox(TextBox textBox, Action<string> updateValue)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "0";
                textBox.SelectionStart = 1;
            }
            else if (textBox.Text.Length > 1 && textBox.Text.StartsWith("0"))
            {
                int selectionStart = textBox.SelectionStart;
                textBox.Text = textBox.Text.TrimStart('0');
                if (string.IsNullOrEmpty(textBox.Text)) textBox.Text = "0";
                textBox.SelectionStart = Math.Max(0, selectionStart - 1);
            }

            updateValue(textBox.Text);
        }
        private void textBox_BloodPoints_TextChanged(object sender, EventArgs e) => NormalizeCurrencyTextBox(textBox_BloodPoints, val => ProgramFeatures.CurrencySpoof.bloodpointsAmount = val);
        private void textBox_BloodPoints_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!IsKeypressDigit(e))
                e.Handled = true;
        }
        private void textBox_IridescentShards_TextChanged(object sender, EventArgs e) => NormalizeCurrencyTextBox(textBox_IridescentShards, val => ProgramFeatures.CurrencySpoof.iridescentShardsAmount = val);
        private void textBox_IridescentShards_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!IsKeypressDigit(e))
                e.Handled = true;
        }
        private void textBox_AuricCells_TextChanged(object sender, EventArgs e) => NormalizeCurrencyTextBox(textBox_AuricCells, val => ProgramFeatures.CurrencySpoof.auricCellsAmount = val);
        private void textBox_AuricCells_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!IsKeypressDigit(e))
                e.Handled = true;
        }


        private void button_ApiKeyCopy_MouseClick(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox_ApiKey.Text))
            {
                Clipboard.SetText(textBox_ApiKey.Text);
                Media.PlaySoundFromStream(Properties.Resources.SFX_Activate);
            }
        }


        private void comboBox_Crosshairs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ProgramGlobals.isInitialized)
            {
                ProgramFeatures.Crosshair.E_Crosshairs crosshair = (ProgramFeatures.Crosshair.E_Crosshairs)comboBox_Crosshairs.SelectedIndex;

                bool crosshairEnabled = crosshair != ProgramFeatures.Crosshair.E_Crosshairs.none;
                label_CrosshairOpacityTitle.Visible = crosshairEnabled;
                trackBar_CrosshairOpacity.Visible = crosshairEnabled;
                label_CrosshairOpacityPercent.Visible = crosshairEnabled;

                ProgramFeatures.Crosshair.SetSelectedCrosshair(crosshair);
                UpdateCrosshairVisibilityUI();

                pictureBox_Button_CrosshairSettings.Visible = crosshair == ProgramFeatures.Crosshair.E_Crosshairs.customizable;
            }
        }

        private void pictureBox_Button_CrosshairSettings_MouseClick(object sender, MouseEventArgs e) => ProgramCache.Forms.CrosshairSettings.ShowDialog();

        private void trackBar_CrosshairOpacity_ValueChanged(object sender, EventArgs e)
        {
            if (ProgramGlobals.isInitialized == true)
            {
                int crosshairOpacity = trackBar_CrosshairOpacity.Value * 10;
                SetCrosshairOpacity(crosshairOpacity);

                UpdateCrosshairOpacityUI();
            }
        }

        public void UpdateApiKey()
        {
            ProgramCache.Forms.Main.Invoke(new Action(() =>
            {
                textBox_ApiKey.Width = 439; // What a generous approach <:3.
                textBox_ApiKey.Text = ProgramSession.Game.api_key;

                button_ApiKeyCopy.Visible = true;
                pictureBox_Button_CloudIDFriend.Visible = true;
            }));
        }

        private string GetRandomGreetingsText()
        {
            string[] greetingsPhrases =
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
            return $">>> {greetingsPhrases[rnd.Next(greetingsPhrases.Length)]} <<<";
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
            Messaging.ShowMessage($"User-ID: {ProgramSession.Game.userId}\nMatch-ID: {ProgramSession.Game.matchId}\nMatch Type: {ProgramSession.Game.matchType}\nPlayer Role: {ProgramSession.Game.playerRole}", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
#endif
        }
    }
}