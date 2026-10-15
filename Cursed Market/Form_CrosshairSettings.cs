using System;
using System.Drawing;
using System.Windows.Forms;






namespace Cursed_Market
{
    public partial class Form_CrosshairSettings : Form
    {
        private bool _updatingControls = false;
        private bool _changesWereMade = false;
        private CrosshairGenerator.S_CrosshairSettings _workingSettings;

        public Form_CrosshairSettings()
        {
            InitializeComponent();
            InitializeSettings();
        }

        private void Form_CrosshairSettings_Shown(object sender, EventArgs e)
        {
            _workingSettings = ProgramFeatures.Crosshair.Settings;
            LoadSettingsIntoControls(_workingSettings);
            _changesWereMade = false;
        }

        private void Form_CrosshairSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_changesWereMade && e.CloseReason == CloseReason.UserClosing)
            {
                if (Messaging.ShowDialog(Properties.Localization.MESSAGE_CrosshairSettingsUnsavedChanges, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                {
                    e.Cancel = true;
                }
            }
        }

        private void button_WindowClose_MouseClick(object sender, MouseEventArgs e) => this.Close();

        private void panel_WindowHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                panel_WindowHeader.Capture = false;
                Message mouse = Message.Create(Handle, 0xa1, new IntPtr(2), IntPtr.Zero); // 0xA1 - WM_NCLBUTTONDOWN | new IntPtr(2) - HTCAPTION.
                WndProc(ref mouse);
            }
        }

        public void ReloadTheme()
        {
            var theme = ProgramThemes.GetCurrentPalette();

            this.BackColor = theme.FormBack;
            panel_WindowHeader.BackColor = theme.HeaderBack;

            ProgramThemes.ApplyForeColor(theme.TitleText,
                label_FormTitle,
                label_LengthTitle,
                label_ThicknessTitle,
                label_GapTitle,
                label_OutlineTitle,
                label_DotSizeTitle,
                label_CircleRadiusTitle,
                label_ColorPresetTitle,
                label_AlphaTitle,
                label_RedTitle,
                label_GreenTitle,
                label_BlueTitle,
                label_ShareCodeSectionTitle,
                label_ShareCodeInputTitle,
                checkBox_Outline,
                checkBox_CenterDot,
                checkBox_Circle,
                checkBox_TStyle);

            pictureBox_CrosshairPreview.BackColor = theme.SecondaryCardBack;
            button_CopyShareCode.BackColor = theme.SecondaryButtonBack;
            button_SaveChanges.BackColor = theme.PrimaryButtonBack;
            button_SaveChanges.ForeColor = theme.PrimaryButtonText;
            button_SaveChanges.FlatAppearance.BorderColor = theme.PrimaryButtonBorder;
        }

        private void InitializeSettings()
        {
            ReloadTheme();

            #region localization
            label_FormTitle.Text = Properties.Localization.CROSSHAIRSETTINGS_Title;
            label_LengthTitle.Text = Properties.Localization.CROSSHAIRSETTINGS_LengthTitle;
            label_ThicknessTitle.Text = Properties.Localization.CROSSHAIRSETTINGS_ThicknessTitle;
            label_GapTitle.Text = Properties.Localization.CROSSHAIRSETTINGS_GapTitle;
            label_OutlineTitle.Text = Properties.Localization.CROSSHAIRSETTINGS_OutlineTitle;
            label_DotSizeTitle.Text = Properties.Localization.CROSSHAIRSETTINGS_DotSizeTitle;
            label_CircleRadiusTitle.Text = Properties.Localization.CROSSHAIRSETTINGS_CircleRadiusTitle;

            checkBox_Outline.Text = Properties.Localization.CROSSHAIRSETTINGS_OutlineCheckBox;
            checkBox_CenterDot.Text = Properties.Localization.CROSSHAIRSETTINGS_CenterDotCheckBox;
            checkBox_Circle.Text = Properties.Localization.CROSSHAIRSETTINGS_CircleCheckBox;
            checkBox_TStyle.Text = Properties.Localization.CROSSHAIRSETTINGS_TStyleCheckBox;

            label_ColorPresetTitle.Text = Properties.Localization.CROSSHAIRSETTINGS_ColorPresetTitle;
            label_AlphaTitle.Text = Properties.Localization.CROSSHAIRSETTINGS_AlphaTitle;
            label_RedTitle.Text = Properties.Localization.CROSSHAIRSETTINGS_RedTitle;
            label_GreenTitle.Text = Properties.Localization.CROSSHAIRSETTINGS_GreenTitle;
            label_BlueTitle.Text = Properties.Localization.CROSSHAIRSETTINGS_BlueTitle;

            label_ShareCodeSectionTitle.Text = Properties.Localization.CROSSHAIRSETTINGS_ShareCodeSectionTitle;
            label_ShareCodeInputTitle.Text = Properties.Localization.CROSSHAIRSETTINGS_ShareCodeInputTitle;
            button_ImportShareCode.Text = Properties.Localization.CROSSHAIRSETTINGS_ImportShareCode;
            button_ResetDefault.Text = Properties.Localization.CROSSHAIRSETTINGS_ResetDefault;
            button_SaveChanges.Text = Properties.Localization.CROSSHAIRSETTINGS_SaveChanges;
            #endregion
        }

        private static decimal ClampDecimal(decimal val, NumericUpDown ctrl)
        {
            return Math.Max(ctrl.Minimum, Math.Min(ctrl.Maximum, val));
        }

        private void LoadSettingsIntoControls(CrosshairGenerator.S_CrosshairSettings cfg)
        {
            _updatingControls = true;

            numericUpDown_Length.Value = ClampDecimal((decimal)cfg.Length, numericUpDown_Length);
            numericUpDown_Thickness.Value = ClampDecimal((decimal)cfg.Thickness, numericUpDown_Thickness);
            numericUpDown_Gap.Value = ClampDecimal((decimal)cfg.Gap, numericUpDown_Gap);
            numericUpDown_Outline.Value = ClampDecimal((decimal)cfg.OutlineThickness, numericUpDown_Outline);
            numericUpDown_DotSize.Value = ClampDecimal((decimal)cfg.DotSize, numericUpDown_DotSize);
            numericUpDown_CircleRadius.Value = ClampDecimal((decimal)cfg.CircleRadius, numericUpDown_CircleRadius);
            numericUpDown_Alpha.Value = ClampDecimal(cfg.Alpha, numericUpDown_Alpha);

            checkBox_Outline.Checked = cfg.HasOutline;
            numericUpDown_Outline.Enabled = cfg.HasOutline;

            checkBox_CenterDot.Checked = cfg.CenterDot;
            numericUpDown_DotSize.Enabled = cfg.CenterDot;

            checkBox_Circle.Checked = cfg.HasCircle;
            numericUpDown_CircleRadius.Enabled = cfg.HasCircle;

            checkBox_TStyle.Checked = cfg.TStyle;

            numericUpDown_Red.Value = cfg.Color.R;
            numericUpDown_Green.Value = cfg.Color.G;
            numericUpDown_Blue.Value = cfg.Color.B;

            comboBox_ColorPreset.SelectedIndex = ResolveColorPresetIndex(cfg.Color);

            _updatingControls = false;
            UpdatePreviewAndShareCode();
        }

        private static int ResolveColorPresetIndex(Color c)
        {
            if (c.R == 250 && c.G == 0 && c.B == 0) return 0;     // Red.
            if (c.R == 0 && c.G == 255 && c.B == 0) return 1;     // Green.
            if (c.R == 255 && c.G == 255 && c.B == 0) return 2;   // Yellow.
            if (c.R == 0 && c.G == 0 && c.B == 255) return 3;     // Blue.
            if (c.R == 0 && c.G == 255 && c.B == 255) return 4;   // Cyan.
            if (c.R == 255 && c.G == 255 && c.B == 255) return 5; // White.
            if (c.R == 255 && c.G == 0 && c.B == 255) return 6;   // Pink.
            return 7;                                             // Custom RGB.
        }

        private void SyncControlsToWorkingSettings()
        {
            _workingSettings.Length = (float)numericUpDown_Length.Value;
            _workingSettings.Thickness = (float)numericUpDown_Thickness.Value;
            _workingSettings.Gap = (float)numericUpDown_Gap.Value;
            _workingSettings.OutlineThickness = (float)numericUpDown_Outline.Value;
            _workingSettings.DotSize = (float)numericUpDown_DotSize.Value;
            _workingSettings.CircleRadius = (float)numericUpDown_CircleRadius.Value;
            _workingSettings.HasOutline = checkBox_Outline.Checked;
            _workingSettings.CenterDot = checkBox_CenterDot.Checked;
            _workingSettings.HasCircle = checkBox_Circle.Checked;
            _workingSettings.TStyle = checkBox_TStyle.Checked;
            _workingSettings.Alpha = (byte)numericUpDown_Alpha.Value;
            _workingSettings.Color = Color.FromArgb(
                255,
                (int)numericUpDown_Red.Value,
                (int)numericUpDown_Green.Value,
                (int)numericUpDown_Blue.Value);
        }

        private void UpdatePreviewAndShareCode()
        {
            SyncControlsToWorkingSettings();

            /* Update live Share Code representation in textBox_ShareCode. */
            textBox_ShareCode.Text = CrosshairGenerator.EncodeShareCode(_workingSettings);

            Image oldPreview = pictureBox_CrosshairPreview.Image;
            pictureBox_CrosshairPreview.Image = CrosshairGenerator.RenderCrosshair(_workingSettings, pictureBox_CrosshairPreview.Width);
            oldPreview?.Dispose();
        }

        private void OnCrosshairParameterChanged(object sender, EventArgs e)
        {
            if (_updatingControls) return;

            numericUpDown_Outline.Enabled = checkBox_Outline.Checked;
            numericUpDown_DotSize.Enabled = checkBox_CenterDot.Checked;
            numericUpDown_CircleRadius.Enabled = checkBox_Circle.Checked;

            _changesWereMade = true;
            UpdatePreviewAndShareCode();
        }

        private void comboBox_ColorPreset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_updatingControls) return;

            _updatingControls = true;
            switch (comboBox_ColorPreset.SelectedIndex)
            {
                case 0: // Red.
                    numericUpDown_Red.Value = 250; numericUpDown_Green.Value = 0; numericUpDown_Blue.Value = 0;
                    break;
                case 1: // Green.
                    numericUpDown_Red.Value = 0; numericUpDown_Green.Value = 255; numericUpDown_Blue.Value = 0;
                    break;
                case 2: // Yellow.
                    numericUpDown_Red.Value = 255; numericUpDown_Green.Value = 255; numericUpDown_Blue.Value = 0;
                    break;
                case 3: // Blue.
                    numericUpDown_Red.Value = 0; numericUpDown_Green.Value = 0; numericUpDown_Blue.Value = 255;
                    break;
                case 4: // Cyan.
                    numericUpDown_Red.Value = 0; numericUpDown_Green.Value = 255; numericUpDown_Blue.Value = 255;
                    break;
                case 5: // White.
                    numericUpDown_Red.Value = 255; numericUpDown_Green.Value = 255; numericUpDown_Blue.Value = 255;
                    break;
                case 6: // Pink.
                    numericUpDown_Red.Value = 255; numericUpDown_Green.Value = 0; numericUpDown_Blue.Value = 255;
                    break;
            }
            _updatingControls = false;

            _changesWereMade = true;
            UpdatePreviewAndShareCode();
        }

        private void OnRgbValueChanged(object sender, EventArgs e)
        {
            if (_updatingControls) return;

            _updatingControls = true;
            Color currentRgb = Color.FromArgb(255, (int)numericUpDown_Red.Value, (int)numericUpDown_Green.Value, (int)numericUpDown_Blue.Value);
            comboBox_ColorPreset.SelectedIndex = ResolveColorPresetIndex(currentRgb);
            _updatingControls = false;

            _changesWereMade = true;
            UpdatePreviewAndShareCode();
        }

        private void button_CopyShareCode_MouseClick(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox_ShareCode.Text))
            {
                Clipboard.SetText(textBox_ShareCode.Text.Trim());
                Media.PlaySoundFromStream(Properties.Resources.SFX_Activate);
            }
        }

        private void button_ImportShareCode_MouseClick(object sender, MouseEventArgs e)
        {
            string code = textBox_ShareCode.Text;
            if (CrosshairGenerator.TryParseShareCode(code, out var parsedSettings))
            {
                _workingSettings = parsedSettings;
                LoadSettingsIntoControls(_workingSettings);
                _changesWereMade = true;
                Media.PlaySoundFromStream(Properties.Resources.SFX_Activate);
            }
            else
            {
                Messaging.ShowMessage(Properties.Localization.MESSAGE_InvalidCrosshairShareCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                /* Restore valid share code for current settings. */
            textBox_ShareCode.Text = CrosshairGenerator.EncodeShareCode(_workingSettings);
            }
        }

        private void button_ResetDefault_MouseClick(object sender, MouseEventArgs e)
        {
            _workingSettings = CrosshairGenerator.S_CrosshairSettings.Default;
            LoadSettingsIntoControls(_workingSettings);
            _changesWereMade = true;
            Media.PlaySoundFromStream(Properties.Resources.SFX_Activate);
        }

        private void button_SaveChanges_MouseClick(object sender, MouseEventArgs e)
        {
            SyncControlsToWorkingSettings();

            ProgramFeatures.Crosshair.Settings = _workingSettings;
            ProgramFeatures.Crosshair.SaveSettings();

            if (ProgramFeatures.Crosshair.GetSelectedCrosshair() == ProgramFeatures.Crosshair.E_Crosshairs.customizable)
            {
                ProgramFeatures.Crosshair.Reload();
            }

            _changesWereMade = false;
            Media.PlaySoundFromStream(Properties.Resources.SFX_Activate);
        }
    }
}