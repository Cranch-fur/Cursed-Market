using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
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
        private void Form_CharactersPreset_Shown(object sender, EventArgs e)
        {
            if (File.Exists(Globals.CharactersPreset.charactersMapFilePath) == false)
            {
                Messaging.ShowMessage($"\"CharactersMap.json\" File doesn't exist!\nFull Path: {Globals.CharactersPreset.charactersMapFilePath}", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                this.Close();
            }

            if (File.Exists(Globals.CharactersPreset.charactersPresetFilePath) == false)
            {
                Messaging.ShowMessage($"\"CharactersPreset.json\" File doesn't exist!\nFull Path: {Globals.CharactersPreset.charactersPresetFilePath}", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                this.Close();
            }


            string charactersMap = File.ReadAllText(Globals.CharactersPreset.charactersMapFilePath);
            if (string.IsNullOrEmpty(charactersMap) || charactersMap.IsJson() == false)
            {
                Messaging.ShowMessage($"Failed to read \"CharactersMap.json\" file! Make sure that file isn't empty and it's content is in JSON format.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                this.Close();
            }

            string charactersPreset = File.ReadAllText(Globals.CharactersPreset.charactersPresetFilePath);
            if (string.IsNullOrEmpty(charactersPreset) || charactersPreset.IsJson() == false)
            {
                Messaging.ShowMessage($"Failed to read \"charactersPreset.json\" file! Make sure that file isn't empty and it's content is in JSON format.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                this.Close();
            }


            JObject charactersMapJSON = JObject.Parse(charactersMap);
            foreach (var character in charactersMapJSON) // Iterate through each character we have listed in mappings file. 
            {
                string friendlyName = (string)character.Value["friendlyName"];
                string characterPortrait = (string)character.Value["charPortrait"];
                bool canObtainLegacy = (bool)character.Value["canObtainLegacy"];

                if (string.IsNullOrEmpty(friendlyName) == false && string.IsNullOrEmpty(characterPortrait) == false) // Check if both character name & character portrait fields aren't empty & valid.
                {
                    string characterPortraitFilePath = Path.Combine(Globals.CharactersPreset.charactersPortraitsDirectory, characterPortrait);
                    if (File.Exists(characterPortraitFilePath)) // Check if specified character portrait leads to an valid file.
                    {
                        Image characterPortraitImage = Image.FromFile(characterPortraitFilePath);
                        comboBox_CharacterSelect.Items.Add(friendlyName); // For end-user, we only display friendly character name (such as Dwight Fairfield).

                        S_Character characterObject = new S_Character()
                        {
                            friendlyName = friendlyName,
                            characterPortrait = characterPortraitImage,
                            canObtainLegacy = canObtainLegacy
                        };
                        charactersDictionary.Add(character.Key, characterObject); // {Character ID}, {S_Character}
                    }
                }
            }


            outCharactersPreset = JObject.Parse(charactersPreset);


            changesWasMade = false;
            comboBox_CharacterSelect.SelectedIndex = 0;
            UpdateCharacterDisplay();
        }
        private void Form_CharactersPreset_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changesWasMade == true)
            {
                if (Messaging.ShowDialog(Properties.Localization.MESSAGE_CharactersPresetUnsavedChanges, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                {
                    e.Cancel = true;
                }
            }
        }
        private void Form_CharactersPreset_FormClosed(object sender, FormClosedEventArgs e)
        {
            charactersDictionary.Clear();
        }




        private void button_WindowClose_MouseClick(object sender, MouseEventArgs e) => this.Close();
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




        public void ReloadTheme()
        {
            switch (Globals.Application.Theme.selectedTheme)
            {
                default:
                    this.BackColor = Color.White;
                    panel_WindowHeader.BackColor = SystemColors.Control;
                    label_CharactersPresetTitle.ForeColor = Color.Black;
                    label_CharacterSelectTitle.ForeColor = Color.Black;
                    label_BloodWebTitle.ForeColor = Color.Black;
                    label_PrestigeTitle.ForeColor = Color.Black;
                    label_LegacyPrestigeTitle.ForeColor = Color.Black;
                    label_ExtraActionsTitle.ForeColor = Color.Black;
                    label_MaximumBloodWebLevelTitle.ForeColor = Color.Black;
                    label_MaximumPrestigeLevelTitle.ForeColor = Color.Black;
                    pictureBox_CharacterPortrait.BackColor = Color.WhiteSmoke;
                    button_SaveChanges.BackColor = Color.Black;
                    button_SaveChanges.ForeColor = Color.White;
                    button_SaveChanges.FlatAppearance.BorderColor = Color.Gray;
                    break;

                case Globals.Application.Theme.E_Themes.legacy:
                    this.BackColor = Color.FromArgb(255, 46, 51, 73);
                    panel_WindowHeader.BackColor = Color.FromArgb(255, 24, 30, 54);
                    label_CharactersPresetTitle.ForeColor = Color.White;
                    label_CharacterSelectTitle.ForeColor = Color.White;
                    label_BloodWebTitle.ForeColor = Color.White;
                    label_PrestigeTitle.ForeColor = Color.White;
                    label_LegacyPrestigeTitle.ForeColor = Color.White;
                    label_ExtraActionsTitle.ForeColor = Color.White;
                    label_MaximumBloodWebLevelTitle.ForeColor = Color.White;
                    label_MaximumPrestigeLevelTitle.ForeColor = Color.White;
                    pictureBox_CharacterPortrait.BackColor = Color.FromArgb(255, 19, 24, 45);
                    button_SaveChanges.BackColor = Color.IndianRed;
                    button_SaveChanges.ForeColor = Color.White;
                    button_SaveChanges.FlatAppearance.BorderColor = Color.DarkRed;
                    break;

                case Globals.Application.Theme.E_Themes.darkMemories:
                    this.BackColor = Color.FromArgb(255, 44, 47, 51);
                    panel_WindowHeader.BackColor = Color.FromArgb(255, 35, 39, 42);
                    label_CharactersPresetTitle.ForeColor = Color.White;
                    label_CharacterSelectTitle.ForeColor = Color.White;
                    label_BloodWebTitle.ForeColor = Color.White;
                    label_PrestigeTitle.ForeColor = Color.White;
                    label_LegacyPrestigeTitle.ForeColor = Color.White;
                    label_ExtraActionsTitle.ForeColor = Color.White;
                    label_MaximumBloodWebLevelTitle.ForeColor = Color.White;
                    label_MaximumPrestigeLevelTitle.ForeColor = Color.White;
                    pictureBox_CharacterPortrait.BackColor = Color.FromArgb(255, 40, 43, 46);
                    button_SaveChanges.BackColor = Color.FromArgb(255, 65, 65, 65);
                    button_SaveChanges.ForeColor = Color.White;
                    button_SaveChanges.FlatAppearance.BorderColor = Color.FromArgb(255, 95, 95, 95);
                    break;

                case Globals.Application.Theme.E_Themes.saintsRow:
                    this.BackColor = Color.FromArgb(255, 37, 13, 57);
                    panel_WindowHeader.BackColor = Color.FromArgb(255, 55, 20, 86);
                    label_CharactersPresetTitle.ForeColor = Color.White;
                    label_CharacterSelectTitle.ForeColor = Color.White;
                    label_BloodWebTitle.ForeColor = Color.White;
                    label_PrestigeTitle.ForeColor = Color.White;
                    label_LegacyPrestigeTitle.ForeColor = Color.White;
                    label_ExtraActionsTitle.ForeColor = Color.White;
                    label_MaximumBloodWebLevelTitle.ForeColor = Color.White;
                    label_MaximumPrestigeLevelTitle.ForeColor = Color.White;
                    pictureBox_CharacterPortrait.BackColor = Color.FromArgb(255, 31, 10, 48);
                    button_SaveChanges.BackColor = Color.FromArgb(255, 89, 67, 218);
                    button_SaveChanges.ForeColor = Color.White;
                    button_SaveChanges.FlatAppearance.BorderColor = Color.MediumPurple;
                    break;

                case Globals.Application.Theme.E_Themes.dracula:
                    this.BackColor = Color.FromArgb(255, 40, 42, 54);
                    panel_WindowHeader.BackColor = Color.FromArgb(255, 68, 71, 90);
                    label_CharactersPresetTitle.ForeColor = Color.White;
                    label_CharacterSelectTitle.ForeColor = Color.White;
                    label_BloodWebTitle.ForeColor = Color.White;
                    label_PrestigeTitle.ForeColor = Color.White;
                    label_LegacyPrestigeTitle.ForeColor = Color.White;
                    label_ExtraActionsTitle.ForeColor = Color.White;
                    label_MaximumBloodWebLevelTitle.ForeColor = Color.White;
                    label_MaximumPrestigeLevelTitle.ForeColor = Color.White;
                    pictureBox_CharacterPortrait.BackColor = Color.FromArgb(255, 62, 65, 83);
                    button_SaveChanges.BackColor = Color.FromArgb(255, 89, 67, 218);
                    button_SaveChanges.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    button_SaveChanges.FlatAppearance.BorderColor = Color.MediumPurple;
                    break;

                case Globals.Application.Theme.E_Themes.christmas:
                    this.BackColor = Color.FromArgb(255, 24, 24, 24);
                    panel_WindowHeader.BackColor = Color.FromArgb(255, 14, 14, 14);
                    label_CharactersPresetTitle.ForeColor = Color.White;
                    label_CharacterSelectTitle.ForeColor = Color.White;
                    label_BloodWebTitle.ForeColor = Color.White;
                    label_PrestigeTitle.ForeColor = Color.White;
                    label_LegacyPrestigeTitle.ForeColor = Color.White;
                    label_ExtraActionsTitle.ForeColor = Color.White;
                    label_MaximumBloodWebLevelTitle.ForeColor = Color.White;
                    label_MaximumPrestigeLevelTitle.ForeColor = Color.White;
                    pictureBox_CharacterPortrait.BackColor = Color.FromArgb(255, 19, 19, 19);
                    button_SaveChanges.BackColor = Color.FromArgb(255, 221, 34, 85);
                    button_SaveChanges.ForeColor = Color.White;
                    button_SaveChanges.FlatAppearance.BorderColor = Color.HotPink;
                    break;
            }
        }




        private void InitializeSettings()
        {
            ReloadTheme();




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



                pictureBox_CharacterPrestige.Parent = pictureBox_CharacterPortrait;
                pictureBox_CharacterPrestige.BackColor = Color.Transparent;

                int prestigeImageRelativeLocationX = pictureBox_CharacterPortrait.Width - pictureBox_CharacterPrestige.Width; // Right
                prestigeImageRelativeLocationX -= 8; // Left offset
                int prestigeImageRelativeLocationY = pictureBox_CharacterPortrait.Height - pictureBox_CharacterPrestige.Height; // Bottom
                prestigeImageRelativeLocationY -= 8; // Up offset
                pictureBox_CharacterPrestige.Location = new Point(prestigeImageRelativeLocationX, prestigeImageRelativeLocationY);


                label_CharacterPrestige.Parent = pictureBox_CharacterPrestige;
                label_CharacterPrestige.BackColor = Color.Transparent;

                int prestigeNumberRelativeLocationX = pictureBox_CharacterPrestige.Width - label_CharacterPrestige.Width; // Right
                prestigeNumberRelativeLocationX /= 2; // Horizontal center
                prestigeNumberRelativeLocationX += 1; // Right offset (P.S: Horizontal formula (/=2) makes label slightly off to the left)
                int prestigeNumberRelativeLocationY = pictureBox_CharacterPrestige.Height - label_CharacterPrestige.Height; // Bottom
                prestigeNumberRelativeLocationY /= 2; // Vertical center
                label_CharacterPrestige.Location = new Point(prestigeNumberRelativeLocationX, prestigeNumberRelativeLocationY);



                if (prestigeLevel == 100)
                {
                    pictureBox_CharacterPrestige.Image = Properties.Resources.IMG_PRESTIGE_M05;
                }
                else
                {
                    Bitmap[] prestigeImages = new Bitmap[]
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

                    int imageIndex = (prestigeLevel - 1) / 5; // Each 5 levels correspond to one image in the array (e.g., levels 1-5 use the first image, levels 6-10 use the second, and so on).
                    imageIndex = Math.Min(imageIndex, prestigeImages.Length - 1); // Ensure the index does not exceed the bounds of the prestigeImages array.
                    pictureBox_CharacterPrestige.Image = prestigeImages[imageIndex];
                }


                label_CharacterPrestige.Text = prestigeLevel.ToString();
            }


            pictureBox_CharacterPortrait.BackgroundImage = legacyPrestigeLevel == 3 ? Properties.Resources.IMG_PRESTIGE_LEGACY : null;
        }
        private void UpdateCharacterDisplay()
        {
            updateCharacterDisplayInProgress = true;


            numericUpDown_BloodWeb.Value = (int)outCharactersPreset[_selectedCharacterID]["bloodWebLevel"];
            numericUpDown_Prestige.Value = (int)outCharactersPreset[_selectedCharacterID]["prestigeLevel"];


            var character = charactersDictionary[_selectedCharacterID];
            if (character.canObtainLegacy == true)
            {
                numericUpDown_LegacyPrestige.Enabled = true;
                numericUpDown_LegacyPrestige.Value = (int)outCharactersPreset[_selectedCharacterID]["legacyPrestigeLevel"];
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
            if (updateCharacterDisplayInProgress == false)
            {
                outCharactersPreset[_selectedCharacterID]["bloodWebLevel"] = (int)numericUpDown_BloodWeb.Value;
                outCharactersPreset[_selectedCharacterID]["prestigeLevel"] = (int)numericUpDown_Prestige.Value;
                outCharactersPreset[_selectedCharacterID]["legacyPrestigeLevel"] = (int)numericUpDown_LegacyPrestige.Value;
            }
        }




        private void comboBox_CharacterSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var character in charactersDictionary)
            {
                if (character.Value.friendlyName == comboBox_CharacterSelect.SelectedItem.ToString())
                { 
                    _selectedCharacterID = character.Key;
                }
            }

            UpdateCharacterDisplay();
        }




        private void numericUpDown_BloodWeb_ValueChanged(object sender, EventArgs e)
        {
            if (updateCharacterDisplayInProgress == false)
                changesWasMade = true;

            OnCharacterPresetChanged();
        }
        private void numericUpDown_Prestige_ValueChanged(object sender, EventArgs e)
        {
            if (updateCharacterDisplayInProgress == false)
                changesWasMade = true;

            UpdateCharacterPrestigeDisplay();
            OnCharacterPresetChanged();
        }
        private void numericUpDown_LegacyPrestige_ValueChanged(object sender, EventArgs e)
        {
            if (updateCharacterDisplayInProgress == false)
                changesWasMade = true;

            UpdateCharacterPrestigeDisplay();
            OnCharacterPresetChanged();
        }

        


        private void button_SpreadPreset_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (var character in charactersDictionary)
            {
                outCharactersPreset[character.Key]["bloodWebLevel"] = (int)numericUpDown_BloodWeb.Value;
                outCharactersPreset[character.Key]["prestigeLevel"] = (int)numericUpDown_Prestige.Value;

                if (character.Value.canObtainLegacy == true)
                {
                    outCharactersPreset[character.Key]["legacyPrestigeLevel"] = numericUpDown_LegacyPrestige.Value;
                }
            }

            changesWasMade = true;
            Media.PlaySoundFromStream(Properties.Resources.SFX_Activate);
        }

        private void button_RandomizePresets_MouseClick(object sender, MouseEventArgs e)
        {
            Random rnd = new Random();
            foreach (var character in charactersDictionary)
            {
                outCharactersPreset[character.Key]["bloodWebLevel"] = rnd.Next(15, (int)numericUpDown_MaximumBloodWebLevel.Value);
                outCharactersPreset[character.Key]["prestigeLevel"] = rnd.Next(0, (int)numericUpDown_MaximumPrestigeLevel.Value);

                if (character.Value.canObtainLegacy == true)
                {
                    outCharactersPreset[character.Key]["legacyPrestigeLevel"] = rnd.Next(0, 3);
                }
            }

            changesWasMade = true;
            UpdateCharacterDisplay(); // We've made changes to ALL characters including one we're currently on, UI update is required.
            Media.PlaySoundFromStream(Properties.Resources.SFX_Activate);
        }




        private void button_SaveChanges_MouseClick(object sender, MouseEventArgs e)
        {
            changesWasMade = false;
            File.WriteAllText(Globals.CharactersPreset.charactersPresetFilePath, outCharactersPreset.ToString());
            Media.PlaySoundFromStream(Properties.Resources.SFX_Activate);
        }
    }
}
