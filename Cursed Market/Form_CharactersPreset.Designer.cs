
namespace Cursed_Market
{
    partial class Form_CharactersPreset
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_CharactersPreset));
            this.panel_WindowHeader = new System.Windows.Forms.Panel();
            this.button_WindowClose = new System.Windows.Forms.Button();
            this.pictureBox_CharacterPortrait = new System.Windows.Forms.PictureBox();
            this.comboBox_CharacterSelect = new System.Windows.Forms.ComboBox();
            this.pictureBox_Background = new System.Windows.Forms.PictureBox();
            this.label_CharacterSelectTitle = new System.Windows.Forms.Label();
            this.button_SaveChanges = new System.Windows.Forms.Button();
            this.numericUpDown_BloodWeb = new System.Windows.Forms.NumericUpDown();
            this.label_BloodWebTitle = new System.Windows.Forms.Label();
            this.label_PrestigeTitle = new System.Windows.Forms.Label();
            this.numericUpDown_Prestige = new System.Windows.Forms.NumericUpDown();
            this.label_LegacyPrestigeTitle = new System.Windows.Forms.Label();
            this.numericUpDown_LegacyPrestige = new System.Windows.Forms.NumericUpDown();
            this.pictureBox_CharacterPrestige = new System.Windows.Forms.PictureBox();
            this.label_CharacterPrestige = new System.Windows.Forms.Label();
            this.label_CharactersPresetTitle = new System.Windows.Forms.Label();
            this.button_SpreadPreset = new System.Windows.Forms.Button();
            this.pictureBox_ExtraActions = new System.Windows.Forms.PictureBox();
            this.label_ExtraActionsTitle = new System.Windows.Forms.Label();
            this.button_RandomizePresets = new System.Windows.Forms.Button();
            this.label_MaximumPrestigeLevelTitle = new System.Windows.Forms.Label();
            this.numericUpDown_MaximumPrestigeLevel = new System.Windows.Forms.NumericUpDown();
            this.label_MaximumBloodWebLevelTitle = new System.Windows.Forms.Label();
            this.numericUpDown_MaximumBloodWebLevel = new System.Windows.Forms.NumericUpDown();
            this.panel_WindowHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_CharacterPortrait)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Background)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BloodWeb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Prestige)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_LegacyPrestige)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_CharacterPrestige)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ExtraActions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MaximumPrestigeLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MaximumBloodWebLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_WindowHeader
            // 
            this.panel_WindowHeader.BackColor = System.Drawing.SystemColors.Control;
            this.panel_WindowHeader.Controls.Add(this.button_WindowClose);
            this.panel_WindowHeader.Location = new System.Drawing.Point(0, 0);
            this.panel_WindowHeader.Name = "panel_WindowHeader";
            this.panel_WindowHeader.Size = new System.Drawing.Size(700, 23);
            this.panel_WindowHeader.TabIndex = 3;
            this.panel_WindowHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_WindowHeader_MouseDown);
            // 
            // button_WindowClose
            // 
            this.button_WindowClose.BackColor = System.Drawing.Color.IndianRed;
            this.button_WindowClose.FlatAppearance.BorderSize = 0;
            this.button_WindowClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_WindowClose.ForeColor = System.Drawing.Color.White;
            this.button_WindowClose.Location = new System.Drawing.Point(676, 0);
            this.button_WindowClose.Name = "button_WindowClose";
            this.button_WindowClose.Size = new System.Drawing.Size(24, 23);
            this.button_WindowClose.TabIndex = 0;
            this.button_WindowClose.Text = "X";
            this.button_WindowClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_WindowClose.UseVisualStyleBackColor = false;
            this.button_WindowClose.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_WindowClose_MouseClick);
            // 
            // pictureBox_CharacterPortrait
            // 
            this.pictureBox_CharacterPortrait.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBox_CharacterPortrait.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_CharacterPortrait.Location = new System.Drawing.Point(24, 75);
            this.pictureBox_CharacterPortrait.Name = "pictureBox_CharacterPortrait";
            this.pictureBox_CharacterPortrait.Size = new System.Drawing.Size(209, 209);
            this.pictureBox_CharacterPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_CharacterPortrait.TabIndex = 4;
            this.pictureBox_CharacterPortrait.TabStop = false;
            // 
            // comboBox_CharacterSelect
            // 
            this.comboBox_CharacterSelect.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox_CharacterSelect.FormattingEnabled = true;
            this.comboBox_CharacterSelect.Location = new System.Drawing.Point(262, 99);
            this.comboBox_CharacterSelect.Name = "comboBox_CharacterSelect";
            this.comboBox_CharacterSelect.Size = new System.Drawing.Size(410, 23);
            this.comboBox_CharacterSelect.TabIndex = 5;
            this.comboBox_CharacterSelect.SelectedIndexChanged += new System.EventHandler(this.comboBox_CharacterSelect_SelectedIndexChanged);
            // 
            // pictureBox_Background
            // 
            this.pictureBox_Background.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_Background.Location = new System.Drawing.Point(0, 22);
            this.pictureBox_Background.Name = "pictureBox_Background";
            this.pictureBox_Background.Size = new System.Drawing.Size(700, 558);
            this.pictureBox_Background.TabIndex = 13;
            this.pictureBox_Background.TabStop = false;
            // 
            // label_CharacterSelectTitle
            // 
            this.label_CharacterSelectTitle.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_CharacterSelectTitle.Location = new System.Drawing.Point(262, 75);
            this.label_CharacterSelectTitle.Name = "label_CharacterSelectTitle";
            this.label_CharacterSelectTitle.Size = new System.Drawing.Size(410, 20);
            this.label_CharacterSelectTitle.TabIndex = 14;
            this.label_CharacterSelectTitle.Text = "SELECT CHARACTER";
            this.label_CharacterSelectTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_SaveChanges
            // 
            this.button_SaveChanges.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.button_SaveChanges.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_SaveChanges.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_SaveChanges.Location = new System.Drawing.Point(24, 533);
            this.button_SaveChanges.Name = "button_SaveChanges";
            this.button_SaveChanges.Size = new System.Drawing.Size(648, 30);
            this.button_SaveChanges.TabIndex = 15;
            this.button_SaveChanges.Text = "Confirm and Save Changes";
            this.button_SaveChanges.UseVisualStyleBackColor = true;
            this.button_SaveChanges.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_SaveChanges_MouseClick);
            // 
            // numericUpDown_BloodWeb
            // 
            this.numericUpDown_BloodWeb.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDown_BloodWeb.Location = new System.Drawing.Point(262, 157);
            this.numericUpDown_BloodWeb.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDown_BloodWeb.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_BloodWeb.Name = "numericUpDown_BloodWeb";
            this.numericUpDown_BloodWeb.Size = new System.Drawing.Size(410, 23);
            this.numericUpDown_BloodWeb.TabIndex = 16;
            this.numericUpDown_BloodWeb.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_BloodWeb.ValueChanged += new System.EventHandler(this.numericUpDown_BloodWeb_ValueChanged);
            // 
            // label_BloodWebTitle
            // 
            this.label_BloodWebTitle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_BloodWebTitle.Location = new System.Drawing.Point(262, 137);
            this.label_BloodWebTitle.Name = "label_BloodWebTitle";
            this.label_BloodWebTitle.Size = new System.Drawing.Size(410, 17);
            this.label_BloodWebTitle.TabIndex = 17;
            this.label_BloodWebTitle.Text = "BLOODWEB LEVEL";
            this.label_BloodWebTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_PrestigeTitle
            // 
            this.label_PrestigeTitle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_PrestigeTitle.Location = new System.Drawing.Point(262, 189);
            this.label_PrestigeTitle.Name = "label_PrestigeTitle";
            this.label_PrestigeTitle.Size = new System.Drawing.Size(410, 17);
            this.label_PrestigeTitle.TabIndex = 19;
            this.label_PrestigeTitle.Text = "PRESTIGE LEVEL";
            this.label_PrestigeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown_Prestige
            // 
            this.numericUpDown_Prestige.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDown_Prestige.Location = new System.Drawing.Point(262, 209);
            this.numericUpDown_Prestige.Name = "numericUpDown_Prestige";
            this.numericUpDown_Prestige.Size = new System.Drawing.Size(410, 23);
            this.numericUpDown_Prestige.TabIndex = 18;
            this.numericUpDown_Prestige.ValueChanged += new System.EventHandler(this.numericUpDown_Prestige_ValueChanged);
            // 
            // label_LegacyPrestigeTitle
            // 
            this.label_LegacyPrestigeTitle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_LegacyPrestigeTitle.Location = new System.Drawing.Point(262, 241);
            this.label_LegacyPrestigeTitle.Name = "label_LegacyPrestigeTitle";
            this.label_LegacyPrestigeTitle.Size = new System.Drawing.Size(410, 17);
            this.label_LegacyPrestigeTitle.TabIndex = 21;
            this.label_LegacyPrestigeTitle.Text = "LEGACY PRESTIGE LEVEL";
            this.label_LegacyPrestigeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown_LegacyPrestige
            // 
            this.numericUpDown_LegacyPrestige.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDown_LegacyPrestige.Location = new System.Drawing.Point(262, 261);
            this.numericUpDown_LegacyPrestige.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDown_LegacyPrestige.Name = "numericUpDown_LegacyPrestige";
            this.numericUpDown_LegacyPrestige.Size = new System.Drawing.Size(410, 23);
            this.numericUpDown_LegacyPrestige.TabIndex = 20;
            this.numericUpDown_LegacyPrestige.ValueChanged += new System.EventHandler(this.numericUpDown_LegacyPrestige_ValueChanged);
            // 
            // pictureBox_CharacterPrestige
            // 
            this.pictureBox_CharacterPrestige.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBox_CharacterPrestige.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_CharacterPrestige.Image = global::Cursed_Market.Properties.Resources.IMG_PRESTIGE_M01_ASH;
            this.pictureBox_CharacterPrestige.Location = new System.Drawing.Point(161, 212);
            this.pictureBox_CharacterPrestige.Name = "pictureBox_CharacterPrestige";
            this.pictureBox_CharacterPrestige.Size = new System.Drawing.Size(72, 72);
            this.pictureBox_CharacterPrestige.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_CharacterPrestige.TabIndex = 22;
            this.pictureBox_CharacterPrestige.TabStop = false;
            // 
            // label_CharacterPrestige
            // 
            this.label_CharacterPrestige.BackColor = System.Drawing.Color.Black;
            this.label_CharacterPrestige.Font = new System.Drawing.Font("Roboto", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_CharacterPrestige.ForeColor = System.Drawing.Color.White;
            this.label_CharacterPrestige.Location = new System.Drawing.Point(161, 231);
            this.label_CharacterPrestige.Name = "label_CharacterPrestige";
            this.label_CharacterPrestige.Size = new System.Drawing.Size(72, 32);
            this.label_CharacterPrestige.TabIndex = 23;
            this.label_CharacterPrestige.Text = "100";
            this.label_CharacterPrestige.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_CharactersPresetTitle
            // 
            this.label_CharactersPresetTitle.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_CharactersPresetTitle.Location = new System.Drawing.Point(24, 35);
            this.label_CharactersPresetTitle.Name = "label_CharactersPresetTitle";
            this.label_CharactersPresetTitle.Size = new System.Drawing.Size(648, 24);
            this.label_CharactersPresetTitle.TabIndex = 24;
            this.label_CharactersPresetTitle.Text = "CONFIGURE CHARACTERS PRESET";
            this.label_CharactersPresetTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_SpreadPreset
            // 
            this.button_SpreadPreset.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_SpreadPreset.Location = new System.Drawing.Point(42, 363);
            this.button_SpreadPreset.Name = "button_SpreadPreset";
            this.button_SpreadPreset.Size = new System.Drawing.Size(617, 30);
            this.button_SpreadPreset.TabIndex = 25;
            this.button_SpreadPreset.Text = "Spread current preset among all characters";
            this.button_SpreadPreset.UseVisualStyleBackColor = true;
            this.button_SpreadPreset.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_SpreadPreset_MouseClick);
            // 
            // pictureBox_ExtraActions
            // 
            this.pictureBox_ExtraActions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_ExtraActions.Location = new System.Drawing.Point(24, 345);
            this.pictureBox_ExtraActions.Name = "pictureBox_ExtraActions";
            this.pictureBox_ExtraActions.Size = new System.Drawing.Size(648, 160);
            this.pictureBox_ExtraActions.TabIndex = 26;
            this.pictureBox_ExtraActions.TabStop = false;
            // 
            // label_ExtraActionsTitle
            // 
            this.label_ExtraActionsTitle.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_ExtraActionsTitle.Location = new System.Drawing.Point(145, 336);
            this.label_ExtraActionsTitle.Name = "label_ExtraActionsTitle";
            this.label_ExtraActionsTitle.Size = new System.Drawing.Size(410, 20);
            this.label_ExtraActionsTitle.TabIndex = 27;
            this.label_ExtraActionsTitle.Text = "EXTRA ACTIONS";
            this.label_ExtraActionsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_RandomizePresets
            // 
            this.button_RandomizePresets.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_RandomizePresets.Location = new System.Drawing.Point(42, 402);
            this.button_RandomizePresets.Name = "button_RandomizePresets";
            this.button_RandomizePresets.Size = new System.Drawing.Size(617, 30);
            this.button_RandomizePresets.TabIndex = 28;
            this.button_RandomizePresets.Text = "Randomize all characters presets";
            this.button_RandomizePresets.UseVisualStyleBackColor = true;
            this.button_RandomizePresets.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_RandomizePresets_MouseClick);
            // 
            // label_MaximumPrestigeLevelTitle
            // 
            this.label_MaximumPrestigeLevelTitle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_MaximumPrestigeLevelTitle.Location = new System.Drawing.Point(370, 444);
            this.label_MaximumPrestigeLevelTitle.Name = "label_MaximumPrestigeLevelTitle";
            this.label_MaximumPrestigeLevelTitle.Size = new System.Drawing.Size(289, 17);
            this.label_MaximumPrestigeLevelTitle.TabIndex = 30;
            this.label_MaximumPrestigeLevelTitle.Text = "MAX PRESTIGE LEVEL";
            this.label_MaximumPrestigeLevelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown_MaximumPrestigeLevel
            // 
            this.numericUpDown_MaximumPrestigeLevel.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDown_MaximumPrestigeLevel.Location = new System.Drawing.Point(370, 464);
            this.numericUpDown_MaximumPrestigeLevel.Name = "numericUpDown_MaximumPrestigeLevel";
            this.numericUpDown_MaximumPrestigeLevel.Size = new System.Drawing.Size(289, 23);
            this.numericUpDown_MaximumPrestigeLevel.TabIndex = 29;
            this.numericUpDown_MaximumPrestigeLevel.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label_MaximumBloodWebLevelTitle
            // 
            this.label_MaximumBloodWebLevelTitle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_MaximumBloodWebLevelTitle.Location = new System.Drawing.Point(42, 444);
            this.label_MaximumBloodWebLevelTitle.Name = "label_MaximumBloodWebLevelTitle";
            this.label_MaximumBloodWebLevelTitle.Size = new System.Drawing.Size(289, 17);
            this.label_MaximumBloodWebLevelTitle.TabIndex = 32;
            this.label_MaximumBloodWebLevelTitle.Text = "MAX BLOODWEB LEVEL";
            this.label_MaximumBloodWebLevelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown_MaximumBloodWebLevel
            // 
            this.numericUpDown_MaximumBloodWebLevel.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDown_MaximumBloodWebLevel.Location = new System.Drawing.Point(42, 464);
            this.numericUpDown_MaximumBloodWebLevel.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDown_MaximumBloodWebLevel.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDown_MaximumBloodWebLevel.Name = "numericUpDown_MaximumBloodWebLevel";
            this.numericUpDown_MaximumBloodWebLevel.Size = new System.Drawing.Size(289, 23);
            this.numericUpDown_MaximumBloodWebLevel.TabIndex = 31;
            this.numericUpDown_MaximumBloodWebLevel.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // Form_CharactersPreset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(700, 580);
            this.Controls.Add(this.label_MaximumBloodWebLevelTitle);
            this.Controls.Add(this.numericUpDown_MaximumBloodWebLevel);
            this.Controls.Add(this.label_MaximumPrestigeLevelTitle);
            this.Controls.Add(this.numericUpDown_MaximumPrestigeLevel);
            this.Controls.Add(this.button_RandomizePresets);
            this.Controls.Add(this.label_ExtraActionsTitle);
            this.Controls.Add(this.button_SpreadPreset);
            this.Controls.Add(this.pictureBox_ExtraActions);
            this.Controls.Add(this.label_CharactersPresetTitle);
            this.Controls.Add(this.label_CharacterPrestige);
            this.Controls.Add(this.pictureBox_CharacterPrestige);
            this.Controls.Add(this.label_LegacyPrestigeTitle);
            this.Controls.Add(this.numericUpDown_LegacyPrestige);
            this.Controls.Add(this.label_PrestigeTitle);
            this.Controls.Add(this.numericUpDown_Prestige);
            this.Controls.Add(this.label_BloodWebTitle);
            this.Controls.Add(this.numericUpDown_BloodWeb);
            this.Controls.Add(this.button_SaveChanges);
            this.Controls.Add(this.label_CharacterSelectTitle);
            this.Controls.Add(this.comboBox_CharacterSelect);
            this.Controls.Add(this.pictureBox_CharacterPortrait);
            this.Controls.Add(this.panel_WindowHeader);
            this.Controls.Add(this.pictureBox_Background);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_CharactersPreset";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Characters Prestige Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_CharactersPreset_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_CharactersPreset_FormClosed);
            this.Shown += new System.EventHandler(this.Form_CharactersPreset_Shown);
            this.panel_WindowHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_CharacterPortrait)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Background)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BloodWeb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Prestige)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_LegacyPrestige)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_CharacterPrestige)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ExtraActions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MaximumPrestigeLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MaximumBloodWebLevel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_WindowHeader;
        private System.Windows.Forms.Button button_WindowClose;
        private System.Windows.Forms.PictureBox pictureBox_CharacterPortrait;
        private System.Windows.Forms.ComboBox comboBox_CharacterSelect;
        private System.Windows.Forms.PictureBox pictureBox_Background;
        private System.Windows.Forms.Label label_CharacterSelectTitle;
        private System.Windows.Forms.Button button_SaveChanges;
        private System.Windows.Forms.NumericUpDown numericUpDown_BloodWeb;
        private System.Windows.Forms.Label label_BloodWebTitle;
        private System.Windows.Forms.Label label_PrestigeTitle;
        private System.Windows.Forms.NumericUpDown numericUpDown_Prestige;
        private System.Windows.Forms.Label label_LegacyPrestigeTitle;
        private System.Windows.Forms.NumericUpDown numericUpDown_LegacyPrestige;
        private System.Windows.Forms.PictureBox pictureBox_CharacterPrestige;
        private System.Windows.Forms.Label label_CharacterPrestige;
        private System.Windows.Forms.Label label_CharactersPresetTitle;
        private System.Windows.Forms.Button button_SpreadPreset;
        private System.Windows.Forms.PictureBox pictureBox_ExtraActions;
        private System.Windows.Forms.Label label_ExtraActionsTitle;
        private System.Windows.Forms.Button button_RandomizePresets;
        private System.Windows.Forms.Label label_MaximumPrestigeLevelTitle;
        private System.Windows.Forms.NumericUpDown numericUpDown_MaximumPrestigeLevel;
        private System.Windows.Forms.Label label_MaximumBloodWebLevelTitle;
        private System.Windows.Forms.NumericUpDown numericUpDown_MaximumBloodWebLevel;
    }
}