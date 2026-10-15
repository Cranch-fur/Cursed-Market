namespace Cursed_Market
{
    partial class Form_CrosshairSettings
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
            this.panel_WindowHeader = new System.Windows.Forms.Panel();
            this.button_WindowClose = new System.Windows.Forms.Button();
            this.label_FormTitle = new System.Windows.Forms.Label();
            this.pictureBox_CrosshairPreview = new System.Windows.Forms.PictureBox();
            this.label_LengthTitle = new System.Windows.Forms.Label();
            this.numericUpDown_Length = new System.Windows.Forms.NumericUpDown();
            this.label_ThicknessTitle = new System.Windows.Forms.Label();
            this.numericUpDown_Thickness = new System.Windows.Forms.NumericUpDown();
            this.label_GapTitle = new System.Windows.Forms.Label();
            this.numericUpDown_Gap = new System.Windows.Forms.NumericUpDown();
            this.label_OutlineTitle = new System.Windows.Forms.Label();
            this.numericUpDown_Outline = new System.Windows.Forms.NumericUpDown();
            this.label_DotSizeTitle = new System.Windows.Forms.Label();
            this.numericUpDown_DotSize = new System.Windows.Forms.NumericUpDown();
            this.label_CircleRadiusTitle = new System.Windows.Forms.Label();
            this.numericUpDown_CircleRadius = new System.Windows.Forms.NumericUpDown();
            this.checkBox_Outline = new System.Windows.Forms.CheckBox();
            this.checkBox_CenterDot = new System.Windows.Forms.CheckBox();
            this.checkBox_Circle = new System.Windows.Forms.CheckBox();
            this.checkBox_TStyle = new System.Windows.Forms.CheckBox();
            this.label_ColorPresetTitle = new System.Windows.Forms.Label();
            this.comboBox_ColorPreset = new System.Windows.Forms.ComboBox();
            this.label_AlphaTitle = new System.Windows.Forms.Label();
            this.numericUpDown_Alpha = new System.Windows.Forms.NumericUpDown();
            this.label_RedTitle = new System.Windows.Forms.Label();
            this.numericUpDown_Red = new System.Windows.Forms.NumericUpDown();
            this.label_GreenTitle = new System.Windows.Forms.Label();
            this.numericUpDown_Green = new System.Windows.Forms.NumericUpDown();
            this.label_BlueTitle = new System.Windows.Forms.Label();
            this.numericUpDown_Blue = new System.Windows.Forms.NumericUpDown();
            this.label_ShareCodeSectionTitle = new System.Windows.Forms.Label();
            this.pictureBox_ShareCodeBorder = new System.Windows.Forms.PictureBox();
            this.label_ShareCodeInputTitle = new System.Windows.Forms.Label();
            this.textBox_ShareCode = new System.Windows.Forms.TextBox();
            this.button_CopyShareCode = new System.Windows.Forms.Button();
            this.button_ImportShareCode = new System.Windows.Forms.Button();
            this.button_ResetDefault = new System.Windows.Forms.Button();
            this.button_SaveChanges = new System.Windows.Forms.Button();
            this.pictureBox_Background = new System.Windows.Forms.PictureBox();
            this.panel_WindowHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_CrosshairPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Length)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Thickness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Gap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Outline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_DotSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_CircleRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Alpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Red)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Green)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Blue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ShareCodeBorder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Background)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_WindowHeader
            // 
            this.panel_WindowHeader.BackColor = System.Drawing.SystemColors.Control;
            this.panel_WindowHeader.Controls.Add(this.button_WindowClose);
            this.panel_WindowHeader.Location = new System.Drawing.Point(0, 0);
            this.panel_WindowHeader.Name = "panel_WindowHeader";
            this.panel_WindowHeader.Size = new System.Drawing.Size(700, 23);
            this.panel_WindowHeader.TabIndex = 0;
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
            // label_FormTitle
            // 
            this.label_FormTitle.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_FormTitle.Location = new System.Drawing.Point(24, 35);
            this.label_FormTitle.Name = "label_FormTitle";
            this.label_FormTitle.Size = new System.Drawing.Size(648, 24);
            this.label_FormTitle.TabIndex = 1;
            this.label_FormTitle.Text = "CONFIGURE CROSSHAIR";
            this.label_FormTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox_CrosshairPreview
            // 
            this.pictureBox_CrosshairPreview.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBox_CrosshairPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_CrosshairPreview.Location = new System.Drawing.Point(24, 75);
            this.pictureBox_CrosshairPreview.Name = "pictureBox_CrosshairPreview";
            this.pictureBox_CrosshairPreview.Size = new System.Drawing.Size(223, 223);
            this.pictureBox_CrosshairPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox_CrosshairPreview.TabIndex = 2;
            this.pictureBox_CrosshairPreview.TabStop = false;
            // 
            // label_LengthTitle
            // 
            this.label_LengthTitle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_LengthTitle.Location = new System.Drawing.Point(262, 75);
            this.label_LengthTitle.Name = "label_LengthTitle";
            this.label_LengthTitle.Size = new System.Drawing.Size(126, 17);
            this.label_LengthTitle.TabIndex = 3;
            this.label_LengthTitle.Text = "LENGTH";
            this.label_LengthTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown_Length
            // 
            this.numericUpDown_Length.DecimalPlaces = 1;
            this.numericUpDown_Length.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDown_Length.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_Length.Location = new System.Drawing.Point(262, 95);
            this.numericUpDown_Length.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown_Length.Name = "numericUpDown_Length";
            this.numericUpDown_Length.Size = new System.Drawing.Size(126, 23);
            this.numericUpDown_Length.TabIndex = 4;
            this.numericUpDown_Length.Value = new decimal(new int[] {
            25,
            0,
            0,
            65536});
            this.numericUpDown_Length.ValueChanged += new System.EventHandler(this.OnCrosshairParameterChanged);
            // 
            // label_ThicknessTitle
            // 
            this.label_ThicknessTitle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_ThicknessTitle.Location = new System.Drawing.Point(404, 75);
            this.label_ThicknessTitle.Name = "label_ThicknessTitle";
            this.label_ThicknessTitle.Size = new System.Drawing.Size(126, 17);
            this.label_ThicknessTitle.TabIndex = 5;
            this.label_ThicknessTitle.Text = "THICKNESS";
            this.label_ThicknessTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown_Thickness
            // 
            this.numericUpDown_Thickness.DecimalPlaces = 1;
            this.numericUpDown_Thickness.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDown_Thickness.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_Thickness.Location = new System.Drawing.Point(404, 95);
            this.numericUpDown_Thickness.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_Thickness.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_Thickness.Name = "numericUpDown_Thickness";
            this.numericUpDown_Thickness.Size = new System.Drawing.Size(126, 23);
            this.numericUpDown_Thickness.TabIndex = 6;
            this.numericUpDown_Thickness.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Thickness.ValueChanged += new System.EventHandler(this.OnCrosshairParameterChanged);
            // 
            // label_GapTitle
            // 
            this.label_GapTitle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_GapTitle.Location = new System.Drawing.Point(546, 75);
            this.label_GapTitle.Name = "label_GapTitle";
            this.label_GapTitle.Size = new System.Drawing.Size(126, 17);
            this.label_GapTitle.TabIndex = 7;
            this.label_GapTitle.Text = "CENTER GAP";
            this.label_GapTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown_Gap
            // 
            this.numericUpDown_Gap.DecimalPlaces = 1;
            this.numericUpDown_Gap.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDown_Gap.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_Gap.Location = new System.Drawing.Point(546, 95);
            this.numericUpDown_Gap.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_Gap.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDown_Gap.Name = "numericUpDown_Gap";
            this.numericUpDown_Gap.Size = new System.Drawing.Size(126, 23);
            this.numericUpDown_Gap.TabIndex = 8;
            this.numericUpDown_Gap.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDown_Gap.ValueChanged += new System.EventHandler(this.OnCrosshairParameterChanged);
            // 
            // label_OutlineTitle
            // 
            this.label_OutlineTitle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_OutlineTitle.Location = new System.Drawing.Point(262, 127);
            this.label_OutlineTitle.Name = "label_OutlineTitle";
            this.label_OutlineTitle.Size = new System.Drawing.Size(126, 17);
            this.label_OutlineTitle.TabIndex = 9;
            this.label_OutlineTitle.Text = "OUTLINE SIZE";
            this.label_OutlineTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown_Outline
            // 
            this.numericUpDown_Outline.DecimalPlaces = 1;
            this.numericUpDown_Outline.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDown_Outline.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDown_Outline.Location = new System.Drawing.Point(262, 147);
            this.numericUpDown_Outline.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDown_Outline.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDown_Outline.Name = "numericUpDown_Outline";
            this.numericUpDown_Outline.Size = new System.Drawing.Size(126, 23);
            this.numericUpDown_Outline.TabIndex = 10;
            this.numericUpDown_Outline.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Outline.ValueChanged += new System.EventHandler(this.OnCrosshairParameterChanged);
            // 
            // label_DotSizeTitle
            // 
            this.label_DotSizeTitle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_DotSizeTitle.Location = new System.Drawing.Point(404, 127);
            this.label_DotSizeTitle.Name = "label_DotSizeTitle";
            this.label_DotSizeTitle.Size = new System.Drawing.Size(126, 17);
            this.label_DotSizeTitle.TabIndex = 11;
            this.label_DotSizeTitle.Text = "DOT SIZE";
            this.label_DotSizeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown_DotSize
            // 
            this.numericUpDown_DotSize.DecimalPlaces = 1;
            this.numericUpDown_DotSize.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDown_DotSize.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_DotSize.Location = new System.Drawing.Point(404, 147);
            this.numericUpDown_DotSize.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_DotSize.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDown_DotSize.Name = "numericUpDown_DotSize";
            this.numericUpDown_DotSize.Size = new System.Drawing.Size(126, 23);
            this.numericUpDown_DotSize.TabIndex = 12;
            this.numericUpDown_DotSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_DotSize.ValueChanged += new System.EventHandler(this.OnCrosshairParameterChanged);
            // 
            // label_CircleRadiusTitle
            // 
            this.label_CircleRadiusTitle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_CircleRadiusTitle.Location = new System.Drawing.Point(546, 127);
            this.label_CircleRadiusTitle.Name = "label_CircleRadiusTitle";
            this.label_CircleRadiusTitle.Size = new System.Drawing.Size(126, 17);
            this.label_CircleRadiusTitle.TabIndex = 13;
            this.label_CircleRadiusTitle.Text = "CIRCLE RADIUS";
            this.label_CircleRadiusTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown_CircleRadius
            // 
            this.numericUpDown_CircleRadius.DecimalPlaces = 1;
            this.numericUpDown_CircleRadius.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDown_CircleRadius.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDown_CircleRadius.Location = new System.Drawing.Point(546, 147);
            this.numericUpDown_CircleRadius.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDown_CircleRadius.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_CircleRadius.Name = "numericUpDown_CircleRadius";
            this.numericUpDown_CircleRadius.Size = new System.Drawing.Size(126, 23);
            this.numericUpDown_CircleRadius.TabIndex = 14;
            this.numericUpDown_CircleRadius.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_CircleRadius.ValueChanged += new System.EventHandler(this.OnCrosshairParameterChanged);
            // 
            // checkBox_Outline
            // 
            this.checkBox_Outline.AutoSize = true;
            this.checkBox_Outline.Checked = true;
            this.checkBox_Outline.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Outline.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_Outline.Location = new System.Drawing.Point(262, 180);
            this.checkBox_Outline.Name = "checkBox_Outline";
            this.checkBox_Outline.Size = new System.Drawing.Size(95, 18);
            this.checkBox_Outline.TabIndex = 15;
            this.checkBox_Outline.Text = "Draw Outline";
            this.checkBox_Outline.UseVisualStyleBackColor = true;
            this.checkBox_Outline.CheckedChanged += new System.EventHandler(this.OnCrosshairParameterChanged);
            // 
            // checkBox_CenterDot
            // 
            this.checkBox_CenterDot.AutoSize = true;
            this.checkBox_CenterDot.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_CenterDot.Location = new System.Drawing.Point(372, 180);
            this.checkBox_CenterDot.Name = "checkBox_CenterDot";
            this.checkBox_CenterDot.Size = new System.Drawing.Size(83, 18);
            this.checkBox_CenterDot.TabIndex = 16;
            this.checkBox_CenterDot.Text = "Center Dot";
            this.checkBox_CenterDot.UseVisualStyleBackColor = true;
            this.checkBox_CenterDot.CheckedChanged += new System.EventHandler(this.OnCrosshairParameterChanged);
            // 
            // checkBox_Circle
            // 
            this.checkBox_Circle.AutoSize = true;
            this.checkBox_Circle.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_Circle.Location = new System.Drawing.Point(470, 180);
            this.checkBox_Circle.Name = "checkBox_Circle";
            this.checkBox_Circle.Size = new System.Drawing.Size(94, 18);
            this.checkBox_Circle.TabIndex = 17;
            this.checkBox_Circle.Text = "Center Circle";
            this.checkBox_Circle.UseVisualStyleBackColor = true;
            this.checkBox_Circle.CheckedChanged += new System.EventHandler(this.OnCrosshairParameterChanged);
            // 
            // checkBox_TStyle
            // 
            this.checkBox_TStyle.AutoSize = true;
            this.checkBox_TStyle.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_TStyle.Location = new System.Drawing.Point(580, 180);
            this.checkBox_TStyle.Name = "checkBox_TStyle";
            this.checkBox_TStyle.Size = new System.Drawing.Size(60, 18);
            this.checkBox_TStyle.TabIndex = 18;
            this.checkBox_TStyle.Text = "T-Style";
            this.checkBox_TStyle.UseVisualStyleBackColor = true;
            this.checkBox_TStyle.CheckedChanged += new System.EventHandler(this.OnCrosshairParameterChanged);
            // 
            // label_ColorPresetTitle
            // 
            this.label_ColorPresetTitle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_ColorPresetTitle.Location = new System.Drawing.Point(262, 206);
            this.label_ColorPresetTitle.Name = "label_ColorPresetTitle";
            this.label_ColorPresetTitle.Size = new System.Drawing.Size(195, 17);
            this.label_ColorPresetTitle.TabIndex = 19;
            this.label_ColorPresetTitle.Text = "COLOR PRESET";
            this.label_ColorPresetTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox_ColorPreset
            // 
            this.comboBox_ColorPreset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ColorPreset.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox_ColorPreset.FormattingEnabled = true;
            this.comboBox_ColorPreset.Items.AddRange(new object[] {
            "Red",
            "Green",
            "Yellow",
            "Blue",
            "Cyan",
            "White",
            "Pink",
            "Custom RGB"});
            this.comboBox_ColorPreset.Location = new System.Drawing.Point(262, 226);
            this.comboBox_ColorPreset.Name = "comboBox_ColorPreset";
            this.comboBox_ColorPreset.Size = new System.Drawing.Size(195, 23);
            this.comboBox_ColorPreset.TabIndex = 20;
            this.comboBox_ColorPreset.SelectedIndexChanged += new System.EventHandler(this.comboBox_ColorPreset_SelectedIndexChanged);
            // 
            // label_AlphaTitle
            // 
            this.label_AlphaTitle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_AlphaTitle.Location = new System.Drawing.Point(477, 206);
            this.label_AlphaTitle.Name = "label_AlphaTitle";
            this.label_AlphaTitle.Size = new System.Drawing.Size(195, 17);
            this.label_AlphaTitle.TabIndex = 21;
            this.label_AlphaTitle.Text = "ALPHA (10-255)";
            this.label_AlphaTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown_Alpha
            // 
            this.numericUpDown_Alpha.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDown_Alpha.Location = new System.Drawing.Point(477, 226);
            this.numericUpDown_Alpha.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_Alpha.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_Alpha.Name = "numericUpDown_Alpha";
            this.numericUpDown_Alpha.Size = new System.Drawing.Size(195, 23);
            this.numericUpDown_Alpha.TabIndex = 22;
            this.numericUpDown_Alpha.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_Alpha.ValueChanged += new System.EventHandler(this.OnCrosshairParameterChanged);
            // 
            // label_RedTitle
            // 
            this.label_RedTitle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_RedTitle.Location = new System.Drawing.Point(262, 255);
            this.label_RedTitle.Name = "label_RedTitle";
            this.label_RedTitle.Size = new System.Drawing.Size(126, 17);
            this.label_RedTitle.TabIndex = 23;
            this.label_RedTitle.Text = "RED (R)";
            this.label_RedTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown_Red
            // 
            this.numericUpDown_Red.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDown_Red.Location = new System.Drawing.Point(262, 275);
            this.numericUpDown_Red.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_Red.Name = "numericUpDown_Red";
            this.numericUpDown_Red.Size = new System.Drawing.Size(126, 23);
            this.numericUpDown_Red.TabIndex = 24;
            this.numericUpDown_Red.ValueChanged += new System.EventHandler(this.OnRgbValueChanged);
            // 
            // label_GreenTitle
            // 
            this.label_GreenTitle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_GreenTitle.Location = new System.Drawing.Point(404, 255);
            this.label_GreenTitle.Name = "label_GreenTitle";
            this.label_GreenTitle.Size = new System.Drawing.Size(126, 17);
            this.label_GreenTitle.TabIndex = 25;
            this.label_GreenTitle.Text = "GREEN (G)";
            this.label_GreenTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown_Green
            // 
            this.numericUpDown_Green.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDown_Green.Location = new System.Drawing.Point(404, 275);
            this.numericUpDown_Green.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_Green.Name = "numericUpDown_Green";
            this.numericUpDown_Green.Size = new System.Drawing.Size(126, 23);
            this.numericUpDown_Green.TabIndex = 26;
            this.numericUpDown_Green.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_Green.ValueChanged += new System.EventHandler(this.OnRgbValueChanged);
            // 
            // label_BlueTitle
            // 
            this.label_BlueTitle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_BlueTitle.Location = new System.Drawing.Point(546, 255);
            this.label_BlueTitle.Name = "label_BlueTitle";
            this.label_BlueTitle.Size = new System.Drawing.Size(126, 17);
            this.label_BlueTitle.TabIndex = 27;
            this.label_BlueTitle.Text = "BLUE (B)";
            this.label_BlueTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown_Blue
            // 
            this.numericUpDown_Blue.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDown_Blue.Location = new System.Drawing.Point(546, 275);
            this.numericUpDown_Blue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_Blue.Name = "numericUpDown_Blue";
            this.numericUpDown_Blue.Size = new System.Drawing.Size(126, 23);
            this.numericUpDown_Blue.TabIndex = 28;
            this.numericUpDown_Blue.ValueChanged += new System.EventHandler(this.OnRgbValueChanged);
            // 
            // label_ShareCodeSectionTitle
            // 
            this.label_ShareCodeSectionTitle.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_ShareCodeSectionTitle.Location = new System.Drawing.Point(145, 336);
            this.label_ShareCodeSectionTitle.Name = "label_ShareCodeSectionTitle";
            this.label_ShareCodeSectionTitle.Size = new System.Drawing.Size(410, 20);
            this.label_ShareCodeSectionTitle.TabIndex = 29;
            this.label_ShareCodeSectionTitle.Text = "SHARE CODE && ACTIONS";
            this.label_ShareCodeSectionTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox_ShareCodeBorder
            // 
            this.pictureBox_ShareCodeBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_ShareCodeBorder.Location = new System.Drawing.Point(24, 345);
            this.pictureBox_ShareCodeBorder.Name = "pictureBox_ShareCodeBorder";
            this.pictureBox_ShareCodeBorder.Size = new System.Drawing.Size(648, 120);
            this.pictureBox_ShareCodeBorder.TabIndex = 30;
            this.pictureBox_ShareCodeBorder.TabStop = false;
            // 
            // label_ShareCodeInputTitle
            // 
            this.label_ShareCodeInputTitle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_ShareCodeInputTitle.Location = new System.Drawing.Point(42, 368);
            this.label_ShareCodeInputTitle.Name = "label_ShareCodeInputTitle";
            this.label_ShareCodeInputTitle.Size = new System.Drawing.Size(617, 17);
            this.label_ShareCodeInputTitle.TabIndex = 31;
            this.label_ShareCodeInputTitle.Text = "CURRENT / IMPORT SHARE CODE (XXXXX-XXXXX-XXXXX-XXXXX-XXXXX)";
            this.label_ShareCodeInputTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_ShareCode
            // 
            this.textBox_ShareCode.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_ShareCode.Location = new System.Drawing.Point(42, 392);
            this.textBox_ShareCode.Name = "textBox_ShareCode";
            this.textBox_ShareCode.Size = new System.Drawing.Size(438, 23);
            this.textBox_ShareCode.TabIndex = 32;
            this.textBox_ShareCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_CopyShareCode
            // 
            this.button_CopyShareCode.BackColor = System.Drawing.Color.DimGray;
            this.button_CopyShareCode.FlatAppearance.BorderSize = 0;
            this.button_CopyShareCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_CopyShareCode.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_CopyShareCode.ForeColor = System.Drawing.Color.White;
            this.button_CopyShareCode.Location = new System.Drawing.Point(484, 392);
            this.button_CopyShareCode.Name = "button_CopyShareCode";
            this.button_CopyShareCode.Size = new System.Drawing.Size(23, 23);
            this.button_CopyShareCode.TabIndex = 33;
            this.button_CopyShareCode.Text = "⧉";
            this.button_CopyShareCode.UseVisualStyleBackColor = false;
            this.button_CopyShareCode.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_CopyShareCode_MouseClick);
            // 
            // button_ImportShareCode
            // 
            this.button_ImportShareCode.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_ImportShareCode.Location = new System.Drawing.Point(515, 391);
            this.button_ImportShareCode.Name = "button_ImportShareCode";
            this.button_ImportShareCode.Size = new System.Drawing.Size(144, 25);
            this.button_ImportShareCode.TabIndex = 34;
            this.button_ImportShareCode.Text = "Apply Share Code";
            this.button_ImportShareCode.UseVisualStyleBackColor = true;
            this.button_ImportShareCode.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_ImportShareCode_MouseClick);
            // 
            // button_ResetDefault
            // 
            this.button_ResetDefault.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_ResetDefault.Location = new System.Drawing.Point(42, 422);
            this.button_ResetDefault.Name = "button_ResetDefault";
            this.button_ResetDefault.Size = new System.Drawing.Size(617, 30);
            this.button_ResetDefault.TabIndex = 35;
            this.button_ResetDefault.Text = "Reset to Default Crosshair";
            this.button_ResetDefault.UseVisualStyleBackColor = true;
            this.button_ResetDefault.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_ResetDefault_MouseClick);
            // 
            // button_SaveChanges
            // 
            this.button_SaveChanges.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.button_SaveChanges.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_SaveChanges.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_SaveChanges.Location = new System.Drawing.Point(24, 499);
            this.button_SaveChanges.Name = "button_SaveChanges";
            this.button_SaveChanges.Size = new System.Drawing.Size(648, 30);
            this.button_SaveChanges.TabIndex = 36;
            this.button_SaveChanges.Text = "Confirm and Save Crosshair";
            this.button_SaveChanges.UseVisualStyleBackColor = true;
            this.button_SaveChanges.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_SaveChanges_MouseClick);
            // 
            // pictureBox_Background
            // 
            this.pictureBox_Background.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_Background.Location = new System.Drawing.Point(0, 22);
            this.pictureBox_Background.Name = "pictureBox_Background";
            this.pictureBox_Background.Size = new System.Drawing.Size(700, 526);
            this.pictureBox_Background.TabIndex = 37;
            this.pictureBox_Background.TabStop = false;
            // 
            // Form_CrosshairSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(700, 548);
            this.Controls.Add(this.button_SaveChanges);
            this.Controls.Add(this.button_ResetDefault);
            this.Controls.Add(this.button_ImportShareCode);
            this.Controls.Add(this.button_CopyShareCode);
            this.Controls.Add(this.textBox_ShareCode);
            this.Controls.Add(this.label_ShareCodeInputTitle);
            this.Controls.Add(this.label_ShareCodeSectionTitle);
            this.Controls.Add(this.pictureBox_ShareCodeBorder);
            this.Controls.Add(this.numericUpDown_Blue);
            this.Controls.Add(this.label_BlueTitle);
            this.Controls.Add(this.numericUpDown_Green);
            this.Controls.Add(this.label_GreenTitle);
            this.Controls.Add(this.numericUpDown_Red);
            this.Controls.Add(this.label_RedTitle);
            this.Controls.Add(this.numericUpDown_Alpha);
            this.Controls.Add(this.label_AlphaTitle);
            this.Controls.Add(this.comboBox_ColorPreset);
            this.Controls.Add(this.label_ColorPresetTitle);
            this.Controls.Add(this.checkBox_TStyle);
            this.Controls.Add(this.checkBox_Circle);
            this.Controls.Add(this.checkBox_CenterDot);
            this.Controls.Add(this.checkBox_Outline);
            this.Controls.Add(this.numericUpDown_CircleRadius);
            this.Controls.Add(this.label_CircleRadiusTitle);
            this.Controls.Add(this.numericUpDown_DotSize);
            this.Controls.Add(this.label_DotSizeTitle);
            this.Controls.Add(this.numericUpDown_Outline);
            this.Controls.Add(this.label_OutlineTitle);
            this.Controls.Add(this.numericUpDown_Gap);
            this.Controls.Add(this.label_GapTitle);
            this.Controls.Add(this.numericUpDown_Thickness);
            this.Controls.Add(this.label_ThicknessTitle);
            this.Controls.Add(this.numericUpDown_Length);
            this.Controls.Add(this.label_LengthTitle);
            this.Controls.Add(this.pictureBox_CrosshairPreview);
            this.Controls.Add(this.label_FormTitle);
            this.Controls.Add(this.panel_WindowHeader);
            this.Controls.Add(this.pictureBox_Background);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = global::Cursed_Market.Properties.Resources.Icon_Other;
            this.Name = "Form_CrosshairSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crosshair Configurator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_CrosshairSettings_FormClosing);
            this.Shown += new System.EventHandler(this.Form_CrosshairSettings_Shown);
            this.panel_WindowHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_CrosshairPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Length)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Thickness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Gap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Outline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_DotSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_CircleRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Alpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Red)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Green)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Blue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ShareCodeBorder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Background)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_WindowHeader;
        private System.Windows.Forms.Button button_WindowClose;
        private System.Windows.Forms.Label label_FormTitle;
        private System.Windows.Forms.PictureBox pictureBox_CrosshairPreview;
        private System.Windows.Forms.Label label_LengthTitle;
        private System.Windows.Forms.NumericUpDown numericUpDown_Length;
        private System.Windows.Forms.Label label_ThicknessTitle;
        private System.Windows.Forms.NumericUpDown numericUpDown_Thickness;
        private System.Windows.Forms.Label label_GapTitle;
        private System.Windows.Forms.NumericUpDown numericUpDown_Gap;
        private System.Windows.Forms.Label label_OutlineTitle;
        private System.Windows.Forms.NumericUpDown numericUpDown_Outline;
        private System.Windows.Forms.Label label_DotSizeTitle;
        private System.Windows.Forms.NumericUpDown numericUpDown_DotSize;
        private System.Windows.Forms.Label label_CircleRadiusTitle;
        private System.Windows.Forms.NumericUpDown numericUpDown_CircleRadius;
        private System.Windows.Forms.CheckBox checkBox_Outline;
        private System.Windows.Forms.CheckBox checkBox_CenterDot;
        private System.Windows.Forms.CheckBox checkBox_Circle;
        private System.Windows.Forms.CheckBox checkBox_TStyle;
        private System.Windows.Forms.Label label_ColorPresetTitle;
        private System.Windows.Forms.ComboBox comboBox_ColorPreset;
        private System.Windows.Forms.Label label_AlphaTitle;
        private System.Windows.Forms.NumericUpDown numericUpDown_Alpha;
        private System.Windows.Forms.Label label_RedTitle;
        private System.Windows.Forms.NumericUpDown numericUpDown_Red;
        private System.Windows.Forms.Label label_GreenTitle;
        private System.Windows.Forms.NumericUpDown numericUpDown_Green;
        private System.Windows.Forms.Label label_BlueTitle;
        private System.Windows.Forms.NumericUpDown numericUpDown_Blue;
        private System.Windows.Forms.Label label_ShareCodeSectionTitle;
        private System.Windows.Forms.PictureBox pictureBox_ShareCodeBorder;
        private System.Windows.Forms.Label label_ShareCodeInputTitle;
        private System.Windows.Forms.TextBox textBox_ShareCode;
        private System.Windows.Forms.Button button_CopyShareCode;
        private System.Windows.Forms.Button button_ImportShareCode;
        private System.Windows.Forms.Button button_ResetDefault;
        private System.Windows.Forms.Button button_SaveChanges;
        private System.Windows.Forms.PictureBox pictureBox_Background;
    }
}