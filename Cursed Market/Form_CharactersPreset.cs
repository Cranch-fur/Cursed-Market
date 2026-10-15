using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;






namespace Cursed_Market
{
    public partial class Form_CharactersPreset : Form
    {
        public struct S_Character
        {
            public string friendlyName;
            public Image characterPortrait;
            public bool canObtainLegacy;

            public S_Character(string friendlyName, Image characterPortrait, bool canObtainLegacy)
            {
                this.friendlyName = friendlyName;
                this.characterPortrait = characterPortrait;
                this.canObtainLegacy = canObtainLegacy;
            }
        }

        /* Cache prestige icons once in memory. */
        private static readonly Bitmap[] PrestigeImages = new Bitmap[]
        {
            Properties.Resources.IMG_PRESTIGE_M01_ASH,
            Properties.Resources.IMG_PRESTIGE_M01_BRONZE,
            Properties.Resources.IMG_PRESTIGE_M01_SILVER,
            Properties.Resources.IMG_PRESTIGE_M01_GOLD,
            Properties.Resources.IMG_PRESTIGE_M01_IRIDESCENT,
            Properties.Resources.IMG_PRESTIGE_M02_ASH,
            Properties.Resources.IMG_PRESTIGE_M02_BRONZE,
            Properties.Resources.IMG_PRESTIGE_M02_SILVER,
            Properties.Resources.IMG_PRESTIGE_M02_GOLD,
            Properties.Resources.IMG_PRESTIGE_M02_IRIDESCENT,
            Properties.Resources.IMG_PRESTIGE_M03_ASH,
            Properties.Resources.IMG_PRESTIGE_M03_BRONZE,
            Properties.Resources.IMG_PRESTIGE_M03_SILVER,
            Properties.Resources.IMG_PRESTIGE_M03_GOLD,
            Properties.Resources.IMG_PRESTIGE_M03_IRIDESCENT,
            Properties.Resources.IMG_PRESTIGE_M04_ASH,
            Properties.Resources.IMG_PRESTIGE_M04_BRONZE,
            Properties.Resources.IMG_PRESTIGE_M04_SILVER,
            Properties.Resources.IMG_PRESTIGE_M04_GOLD,
            Properties.Resources.IMG_PRESTIGE_M04_IRIDESCENT
        };

        private static readonly Bitmap Prestige100Image = Properties.Resources.IMG_PRESTIGE_M05;
        private static readonly Bitmap PrestigeLegacyBg = Properties.Resources.IMG_PRESTIGE_LEGACY;
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

        private void Form_CharactersPreset_Shown(object sender, EventArgs e)
        {
            /* Clear previous state when the dialog is reopened. */
            CleanupCharacterResources();

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
                        Image characterPortraitImage;
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
            pictureBox_CharacterPortrait.Image = null;
            foreach (var kvp in charactersDictionary)
            {
                kvp.Value.characterPortrait?.Dispose();
            }
            charactersDictionary.Clear();
            comboBox_CharacterSelect.Items.Clear();
        }

        private void button_WindowClose_MouseClick(object sender, MouseEventArgs e) => this.Close();

        private void panel_WindowHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                panel_WindowHeader.Capture = false;
                Message mouse = Message.Create(Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
                WndProc(ref mouse);
            }
        }

        public void ReloadTheme()
        {
            var theme = ProgramThemes.GetCurrentPalette();

            this.BackColor = theme.FormBack;
            panel_WindowHeader.BackColor = theme.HeaderBack;

            ProgramThemes.ApplyForeColor(theme.TitleText,
                label_CharactersPresetTitle, label_CharacterSelectTitle, label_BloodWebTitle,
                label_PrestigeTitle, label_LegacyPrestigeTitle, label_ExtraActionsTitle,
                label_MaximumBloodWebLevelTitle, label_MaximumPrestigeLevelTitle);

            pictureBox_CharacterPortrait.BackColor = theme.SecondaryCardBack;
            button_SaveChanges.BackColor = theme.PrimaryButtonBack;
            button_SaveChanges.ForeColor = theme.PrimaryButtonText;
            button_SaveChanges.FlatAppearance.BorderColor = theme.PrimaryButtonBorder;
        }

        private void InitializeSettings()
        {
            ReloadTheme();

            pictureBox_CharacterPrestige.Parent = pictureBox_CharacterPortrait;
            pictureBox_CharacterPrestige.BackColor = Color.Transparent;
            pictureBox_CharacterPrestige.Location = new Point(
                pictureBox_CharacterPortrait.Width - pictureBox_CharacterPrestige.Width - 8,
                pictureBox_CharacterPortrait.Height - pictureBox_CharacterPrestige.Height - 8);

            label_CharacterPrestige.Parent = pictureBox_CharacterPrestige;
            label_CharacterPrestige.BackColor = Color.Transparent;
            label_CharacterPrestige.Location = new Point(
                (pictureBox_CharacterPrestige.Width - label_CharacterPrestige.Width) / 2 + 1,
                (pictureBox_CharacterPrestige.Height - label_CharacterPrestige.Height) / 2);

            #region localization
            label_CharactersPresetTitle.Text = Properties.Localization.CHARACTERSPRESET_CharactersPresetTitle;
            label_CharacterSelectTitle.Text = Properties.Localization.CHARACTERSPRESET_CharacterSelectTitle;
            label_BloodWebTitle.Text = Properties.Localization.CHARACTERSPRESET_BloodWebTitle;
            label_PrestigeTitle.Text = Properties.Localization.CHARACTERSPRESET_PrestigeTitle;
            label_LegacyPrestigeTitle.Text = Properties.Localization.CHARACTERSPRESET_LegacyPrestigeTitle;
            label_ExtraActionsTitle.Text = Properties.Localization.CHARACTERSPRESET_ExtraActionsTitle;
            button_SpreadPreset.Text = Properties.Localization.CHARACTERSPRESET_SpreadPreset;
            button_RandomizePresets.Text = Properties.Localization.CHARACTERSPRESET_RandomizePresets;
            button_SaveChanges.Text = Properties.Localization.CHARACTERSPRESET_SaveChanges;
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
                label_CharacterPrestige.Visible = false;
                pictureBox_CharacterPrestige.Visible = false;
            }
            else
            {
                label_CharacterPrestige.Visible = true;
                pictureBox_CharacterPrestige.Visible = true;

                if (prestigeLevel == 100)
                {
                    pictureBox_CharacterPrestige.Image = Prestige100Image;
                }
                else
                {
                    int imageIndex = Math.Min((prestigeLevel - 1) / 5, PrestigeImages.Length - 1);
                    pictureBox_CharacterPrestige.Image = PrestigeImages[imageIndex];
                }

                label_CharacterPrestige.Text = prestigeLevel.ToString();
            }

            pictureBox_CharacterPortrait.BackgroundImage = legacyPrestigeLevel == 3 ? PrestigeLegacyBg : null;
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
                numericUpDown_LegacyPrestige.Enabled = true;
                numericUpDown_LegacyPrestige.Value = ClampDecimal(lpLevel, numericUpDown_LegacyPrestige);
            }
            else
            {
                numericUpDown_LegacyPrestige.Enabled = false;
                numericUpDown_LegacyPrestige.Value = 0;
            }

            pictureBox_CharacterPortrait.Image = character.characterPortrait;
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

        private void comboBox_CharacterSelect_SelectedIndexChanged(object sender, EventArgs e)
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

        private void numericUpDown_BloodWeb_ValueChanged(object sender, EventArgs e)
        {
            if (!updateCharacterDisplayInProgress)
                changesWasMade = true;

            OnCharacterPresetChanged();
        }

        private void numericUpDown_Prestige_ValueChanged(object sender, EventArgs e)
        {
            if (!updateCharacterDisplayInProgress)
                changesWasMade = true;

            UpdateCharacterPrestigeDisplay();
            OnCharacterPresetChanged();
        }

        private void numericUpDown_LegacyPrestige_ValueChanged(object sender, EventArgs e)
        {
            if (!updateCharacterDisplayInProgress)
                changesWasMade = true;

            UpdateCharacterPrestigeDisplay();
            OnCharacterPresetChanged();
        }

        private void button_SpreadPreset_MouseClick(object sender, MouseEventArgs e)
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

        private void button_RandomizePresets_MouseClick(object sender, MouseEventArgs e)
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

        private void button_SaveChanges_MouseClick(object sender, MouseEventArgs e)
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