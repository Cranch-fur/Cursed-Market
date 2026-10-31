using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Cursed_Market.UI;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;






namespace Cursed_Market
{
    public partial class Form_CharactersPreset : Form
    {
        public struct S_Character
        {
            public string friendlyName;
            public Bitmap characterPortrait;
            public bool canObtainLegacy;

            public S_Character(string friendlyName, Bitmap characterPortrait, bool canObtainLegacy)
            {
                this.friendlyName = friendlyName;
                this.characterPortrait = characterPortrait;
                this.canObtainLegacy = canObtainLegacy;
            }
        }

        /* Cache prestige icons once in memory. */
        private static readonly Bitmap[] PrestigeImages = new Bitmap[]
        {
            Properties.Resources.IMG_PRESTIGE_M01_ASH.ToAvaloniaBitmap(),
            Properties.Resources.IMG_PRESTIGE_M01_BRONZE.ToAvaloniaBitmap(),
            Properties.Resources.IMG_PRESTIGE_M01_SILVER.ToAvaloniaBitmap(),
            Properties.Resources.IMG_PRESTIGE_M01_GOLD.ToAvaloniaBitmap(),
            Properties.Resources.IMG_PRESTIGE_M01_IRIDESCENT.ToAvaloniaBitmap(),
            Properties.Resources.IMG_PRESTIGE_M02_ASH.ToAvaloniaBitmap(),
            Properties.Resources.IMG_PRESTIGE_M02_BRONZE.ToAvaloniaBitmap(),
            Properties.Resources.IMG_PRESTIGE_M02_SILVER.ToAvaloniaBitmap(),
            Properties.Resources.IMG_PRESTIGE_M02_GOLD.ToAvaloniaBitmap(),
            Properties.Resources.IMG_PRESTIGE_M02_IRIDESCENT.ToAvaloniaBitmap(),
            Properties.Resources.IMG_PRESTIGE_M03_ASH.ToAvaloniaBitmap(),
            Properties.Resources.IMG_PRESTIGE_M03_BRONZE.ToAvaloniaBitmap(),
            Properties.Resources.IMG_PRESTIGE_M03_SILVER.ToAvaloniaBitmap(),
            Properties.Resources.IMG_PRESTIGE_M03_GOLD.ToAvaloniaBitmap(),
            Properties.Resources.IMG_PRESTIGE_M03_IRIDESCENT.ToAvaloniaBitmap(),
            Properties.Resources.IMG_PRESTIGE_M04_ASH.ToAvaloniaBitmap(),
            Properties.Resources.IMG_PRESTIGE_M04_BRONZE.ToAvaloniaBitmap(),
            Properties.Resources.IMG_PRESTIGE_M04_SILVER.ToAvaloniaBitmap(),
            Properties.Resources.IMG_PRESTIGE_M04_GOLD.ToAvaloniaBitmap(),
            Properties.Resources.IMG_PRESTIGE_M04_IRIDESCENT.ToAvaloniaBitmap()
        };

        private static readonly Bitmap Prestige100Image = Properties.Resources.IMG_PRESTIGE_M05.ToAvaloniaBitmap();
        private static readonly Bitmap PrestigeLegacyBg = Properties.Resources.IMG_PRESTIGE_LEGACY.ToAvaloniaBitmap();
        private static readonly Random Rnd = new Random();

        internal Dictionary<string, S_Character> charactersDictionary = new Dictionary<string, S_Character>();
        internal JObject outCharactersPreset;

        internal string _selectedCharacterID = null;
        internal bool updateCharacterDisplayInProgress = false;
        internal bool changesWasMade = false;

        public Form_CharactersPreset()
        {
            InitializeComponent();
            InitializeSettings();
        }

        private void EnsureCharacterPresetEntry(string characterId)
        {
            if (outCharactersPreset == null || string.IsNullOrEmpty(characterId))
                return;

            if (!(outCharactersPreset[characterId] is JObject))
            {
                outCharactersPreset[characterId] = new JObject
                {
                    ["bloodWebLevel"] = 1,
                    ["prestigeLevel"] = 0,
                    ["legacyPrestigeLevel"] = 0
                };
            }
        }

        private async void Form_CharactersPreset_Shown(object sender, EventArgs e)
        {
            /* Clear previous state when the dialog is reopened. */
            CleanupCharacterResources();

            /* First opening in session checks API for updates (also covers missing files). */
            bool isUpdateOffered = await CharactersPresetSync.CheckOnFirstOpenAsync(this);

            /* Offer download from API instead of a plain error when preset files are missing. */
            if (!isUpdateOffered && (!File.Exists(ProgramPaths.charactersMapFilePath) || !File.Exists(ProgramPaths.charactersPresetFilePath)))
            {
                if (Messaging.ShowDialog("Characters Preset files are missing!\n\nDownload them from Cursed Market API now?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    await CharactersPresetSync.RunWithConfirmationAsync(this, false);
            }

            if (!File.Exists(ProgramPaths.charactersMapFilePath))
            {
                Messaging.ShowMessage($"\"CharactersMap.json\" File doesn't exist!\nFull Path: {ProgramPaths.charactersMapFilePath}", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                this.Close();
                return;
            }

            if (!File.Exists(ProgramPaths.charactersPresetFilePath))
            {
                Messaging.ShowMessage($"\"CharactersPreset.json\" File doesn't exist!\nFull Path: {ProgramPaths.charactersPresetFilePath}", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                this.Close();
                return;
            }

            string charactersMap = File.ReadAllText(ProgramPaths.charactersMapFilePath);
            if (string.IsNullOrEmpty(charactersMap) || !charactersMap.IsJson())
            {
                Messaging.ShowMessage("Failed to read \"CharactersMap.json\" file! Make sure that the file isn't empty and its content is in JSON format.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                this.Close();
                return;
            }

            string charactersPreset = File.ReadAllText(ProgramPaths.charactersPresetFilePath);
            if (string.IsNullOrEmpty(charactersPreset) || !charactersPreset.IsJson())
            {
                Messaging.ShowMessage("Failed to read \"CharactersPreset.json\" file! Make sure that the file isn't empty and its content is in JSON format.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                this.Close();
                return;
            }

            outCharactersPreset = JObject.Parse(charactersPreset);

            JObject charactersMapJSON = JObject.Parse(charactersMap);
            foreach (var character in charactersMapJSON)
            {
                string friendlyName = (string)character.Value["friendlyName"];
                string characterPortrait = (string)character.Value["charPortrait"];
                bool canObtainLegacy = (bool)(character.Value["canObtainLegacy"] ?? false);

                if (!string.IsNullOrEmpty(friendlyName) && !string.IsNullOrEmpty(characterPortrait))
                {
                    string characterPortraitFilePath = Path.Combine(ProgramPaths.charactersPortraitsDirectory, characterPortrait);
                    if (File.Exists(characterPortraitFilePath))
                    {
                        /* Load image into memory without locking the file on disk. */
                        byte[] imgBytes = File.ReadAllBytes(characterPortraitFilePath);
                        Bitmap characterPortraitImage;
                        using (MemoryStream ms = new MemoryStream(imgBytes))
                        {
                            characterPortraitImage = new Bitmap(ms);
                        }

                        comboBox_CharacterSelect.Items.Add(friendlyName);
                        charactersDictionary[character.Key] = new S_Character(friendlyName, characterPortraitImage, canObtainLegacy);
                        EnsureCharacterPresetEntry(character.Key);
                    }
                }
            }

            changesWasMade = false;

            if (comboBox_CharacterSelect.Items.Count > 0)
            {
                comboBox_CharacterSelect.SelectedIndex = 0;
            }
        }

        private void Form_CharactersPreset_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changesWasMade && e.CloseReason == CloseReason.UserClosing)
            {
                if (Messaging.ShowDialog(Properties.Localization.MESSAGE_CharactersPresetUnsavedChanges, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                {
                    e.Cancel = true;
                }
            }
        }

        private void Form_CharactersPreset_FormClosed(object sender, FormClosedEventArgs e)
        {
            CleanupCharacterResources();
        }

        private void CleanupCharacterResources()
        {
            changesWasMade = false;
            _selectedCharacterID = null;
            pictureBox_CharacterPortrait.Source = null;
            foreach (var kvp in charactersDictionary)
            {
                kvp.Value.characterPortrait?.Dispose();
            }
            charactersDictionary.Clear();
            comboBox_CharacterSelect.Items.Clear();
        }

        private void button_WindowClose_MouseClick(object sender, RoutedEventArgs e) => this.Close();

        private void panel_WindowHeader_MouseDown(object sender, PointerPressedEventArgs e)
        {
            if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                BeginMoveDrag(e); // Same as sending WM_NCLBUTTONDOWN (HTCAPTION), system takes over window dragging.
            }
        }

        public void ReloadTheme()
        {
            var theme = ProgramThemes.GetCurrentPalette();

            this.BackColor = theme.FormBack;
            panel_WindowHeader.Background = ToBackdropBrush(theme.HeaderBack);

            ProgramThemes.ApplyForeColor(theme.TitleText,
                label_CharactersPresetTitle, label_CharacterSelectTitle, label_BloodWebTitle,
                label_PrestigeTitle, label_LegacyPrestigeTitle, label_ExtraActionsTitle,
                label_MaximumBloodWebLevelTitle, label_MaximumPrestigeLevelTitle);

            pictureBox_CharacterPortraitBackground.Background = theme.SecondaryCardBack.ToBrush();
            button_SaveChanges.Background = theme.PrimaryButtonBack.ToBrush();
            button_SaveChanges.Foreground = theme.PrimaryButtonText.ToBrush();
            button_SaveChanges.BorderBrush = theme.PrimaryButtonBorder.ToBrush();
        }

        private void InitializeSettings()
        {
            ReloadTheme();

            /* Prestige badge & its label already live on top of the portrait (see .axaml), only their position is calculated here. */
            Canvas.SetLeft(pictureBox_CharacterPrestige, pictureBox_CharacterPortrait.Width - pictureBox_CharacterPrestige.Width - 8);
            Canvas.SetTop(pictureBox_CharacterPrestige, pictureBox_CharacterPortrait.Height - pictureBox_CharacterPrestige.Height - 8);

            Canvas.SetLeft(label_CharacterPrestige, Canvas.GetLeft(pictureBox_CharacterPrestige) + (pictureBox_CharacterPrestige.Width - label_CharacterPrestige.Width) / 2 + 1);
            Canvas.SetTop(label_CharacterPrestige, Canvas.GetTop(pictureBox_CharacterPrestige) + (pictureBox_CharacterPrestige.Height - label_CharacterPrestige.Height) / 2);

            #region localization
            label_CharactersPresetTitle.Text = Properties.Localization.CHARACTERSPRESET_CharactersPresetTitle;
            label_CharacterSelectTitle.Text = Properties.Localization.CHARACTERSPRESET_CharacterSelectTitle;
            label_BloodWebTitle.Text = Properties.Localization.CHARACTERSPRESET_BloodWebTitle;
            label_PrestigeTitle.Text = Properties.Localization.CHARACTERSPRESET_PrestigeTitle;
            label_LegacyPrestigeTitle.Text = Properties.Localization.CHARACTERSPRESET_LegacyPrestigeTitle;
            label_ExtraActionsTitle.Text = Properties.Localization.CHARACTERSPRESET_ExtraActionsTitle;
            button_SpreadPreset.Content = Properties.Localization.CHARACTERSPRESET_SpreadPreset;
            button_RandomizePresets.Content = Properties.Localization.CHARACTERSPRESET_RandomizePresets;
            button_SaveChanges.Content = Properties.Localization.CHARACTERSPRESET_SaveChanges;
            #endregion
        }

        private static decimal ClampDecimal(int value, NumericUpDown control)
        {
            return Math.Max(control.Minimum, Math.Min(control.Maximum, value));
        }

        private void UpdateCharacterPrestigeDisplay()
        {
            int prestigeLevel = (int)numericUpDown_Prestige.Value;
            int legacyPrestigeLevel = (int)numericUpDown_LegacyPrestige.Value;

            if (prestigeLevel == 0)
            {
                label_CharacterPrestige.IsVisible = false;
                pictureBox_CharacterPrestige.IsVisible = false;
            }
            else
            {
                label_CharacterPrestige.IsVisible = true;
                pictureBox_CharacterPrestige.IsVisible = true;

                if (prestigeLevel == 100)
                {
                    pictureBox_CharacterPrestige.Source = Prestige100Image;
                }
                else
                {
                    int imageIndex = Math.Min((prestigeLevel - 1) / 5, PrestigeImages.Length - 1);
                    pictureBox_CharacterPrestige.Source = PrestigeImages[imageIndex];
                }

                label_CharacterPrestige.Text = prestigeLevel.ToString();
            }

            pictureBox_CharacterPortraitBackgroundImage.Source = legacyPrestigeLevel == 3 ? PrestigeLegacyBg : null;
        }

        private void UpdateCharacterDisplay()
        {
            if (string.IsNullOrEmpty(_selectedCharacterID) || outCharactersPreset == null || !charactersDictionary.ContainsKey(_selectedCharacterID))
                return;

            EnsureCharacterPresetEntry(_selectedCharacterID);

            updateCharacterDisplayInProgress = true;

            int bwLevel = (int)(outCharactersPreset[_selectedCharacterID]["bloodWebLevel"] ?? 1);
            int pLevel = (int)(outCharactersPreset[_selectedCharacterID]["prestigeLevel"] ?? 0);
            int lpLevel = (int)(outCharactersPreset[_selectedCharacterID]["legacyPrestigeLevel"] ?? 0);

            numericUpDown_BloodWeb.Value = ClampDecimal(bwLevel, numericUpDown_BloodWeb);
            numericUpDown_Prestige.Value = ClampDecimal(pLevel, numericUpDown_Prestige);

            var character = charactersDictionary[_selectedCharacterID];
            if (character.canObtainLegacy)
            {
                numericUpDown_LegacyPrestige.IsEnabled = true;
                numericUpDown_LegacyPrestige.Value = ClampDecimal(lpLevel, numericUpDown_LegacyPrestige);
            }
            else
            {
                numericUpDown_LegacyPrestige.IsEnabled = false;
                numericUpDown_LegacyPrestige.Value = 0;
            }

            pictureBox_CharacterPortrait.Source = character.characterPortrait;
            UpdateCharacterPrestigeDisplay();

            updateCharacterDisplayInProgress = false;
        }

        private void OnCharacterPresetChanged()
        {
            if (!updateCharacterDisplayInProgress && !string.IsNullOrEmpty(_selectedCharacterID) && outCharactersPreset != null)
            {
                EnsureCharacterPresetEntry(_selectedCharacterID);

                outCharactersPreset[_selectedCharacterID]["bloodWebLevel"] = (int)numericUpDown_BloodWeb.Value;
                outCharactersPreset[_selectedCharacterID]["prestigeLevel"] = (int)numericUpDown_Prestige.Value;
                outCharactersPreset[_selectedCharacterID]["legacyPrestigeLevel"] = (int)numericUpDown_LegacyPrestige.Value;
            }
        }

        private void comboBox_CharacterSelect_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_CharacterSelect.SelectedItem == null) return;
            string selectedName = comboBox_CharacterSelect.SelectedItem.ToString();

            foreach (var character in charactersDictionary)
            {
                if (character.Value.friendlyName == selectedName)
                {
                    _selectedCharacterID = character.Key;
                    break;
                }
            }

            UpdateCharacterDisplay();
        }

        private void numericUpDown_BloodWeb_ValueChanged(object sender, NumericUpDownValueChangedEventArgs e)
        {
            if (!updateCharacterDisplayInProgress)
                changesWasMade = true;

            OnCharacterPresetChanged();
        }

        private void numericUpDown_Prestige_ValueChanged(object sender, NumericUpDownValueChangedEventArgs e)
        {
            if (!updateCharacterDisplayInProgress)
                changesWasMade = true;

            UpdateCharacterPrestigeDisplay();
            OnCharacterPresetChanged();
        }

        private void numericUpDown_LegacyPrestige_ValueChanged(object sender, NumericUpDownValueChangedEventArgs e)
        {
            if (!updateCharacterDisplayInProgress)
                changesWasMade = true;

            UpdateCharacterPrestigeDisplay();
            OnCharacterPresetChanged();
        }

        private void button_SpreadPreset_MouseClick(object sender, RoutedEventArgs e)
        {
            if (outCharactersPreset == null) return;

            foreach (var character in charactersDictionary)
            {
                EnsureCharacterPresetEntry(character.Key);

                outCharactersPreset[character.Key]["bloodWebLevel"] = (int)numericUpDown_BloodWeb.Value;
                outCharactersPreset[character.Key]["prestigeLevel"] = (int)numericUpDown_Prestige.Value;
                outCharactersPreset[character.Key]["legacyPrestigeLevel"] = character.Value.canObtainLegacy
                    ? (int)numericUpDown_LegacyPrestige.Value
                    : 0;
            }

            changesWasMade = true;
            Media.PlaySoundFromStream(Properties.Resources.SFX_Activate);
        }

        private void button_RandomizePresets_MouseClick(object sender, RoutedEventArgs e)
        {
            if (outCharactersPreset == null) return;

            int maxBw = (int)numericUpDown_MaximumBloodWebLevel.Value;
            int maxPrestige = (int)numericUpDown_MaximumPrestigeLevel.Value;

            foreach (var character in charactersDictionary)
            {
                EnsureCharacterPresetEntry(character.Key);

                outCharactersPreset[character.Key]["bloodWebLevel"] = Rnd.Next(15, maxBw + 1); // 15 to maxBw inclusive.
                outCharactersPreset[character.Key]["prestigeLevel"] = Rnd.Next(0, maxPrestige + 1);
                outCharactersPreset[character.Key]["legacyPrestigeLevel"] = character.Value.canObtainLegacy
                    ? Rnd.Next(0, 4) // 0 to 3 inclusive.
                    : 0;
            }

            changesWasMade = true;
            UpdateCharacterDisplay();
            Media.PlaySoundFromStream(Properties.Resources.SFX_Activate);
        }

        private void button_SaveChanges_MouseClick(object sender, RoutedEventArgs e)
        {
            if (outCharactersPreset == null) return;

            changesWasMade = false;
            File.WriteAllText(ProgramPaths.charactersPresetFilePath, outCharactersPreset.ToString());

            /* Immediately reload active preset in memory if the feature is currently enabled in Form_Main. */
            if (ProgramFeatures.CharactersPreset.enabled && ProgramFeatures.CharactersPreset.Obtain())
            {
                ProgramFeatures.CharactersPreset.SetEnabled(true);
            }

            Media.PlaySoundFromStream(Properties.Resources.SFX_Activate);
        }
    }
}