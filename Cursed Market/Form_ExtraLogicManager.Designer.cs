namespace Cursed_Market
{
    partial class Form_ExtraLogicManager
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panel_WindowHeader = new System.Windows.Forms.Panel();
            this.button_WindowClose = new System.Windows.Forms.Button();
            this.pictureBox_Background = new System.Windows.Forms.PictureBox();
            this.label_FormTitle = new System.Windows.Forms.Label();
            this.label_Hint = new System.Windows.Forms.Label();
            this.checkedListBox_Modules = new System.Windows.Forms.CheckedListBox();
            this.textBox_Summary = new System.Windows.Forms.TextBox();
            this.button_OpenFolder = new System.Windows.Forms.Button();
            this.button_Refresh = new System.Windows.Forms.Button();
            this.button_Start = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.panel_WindowHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Background)).BeginInit();
            this.SuspendLayout();
            //
            // panel_WindowHeader
            //
            this.panel_WindowHeader.BackColor = System.Drawing.SystemColors.Control;
            this.panel_WindowHeader.Controls.Add(this.button_WindowClose);
            this.panel_WindowHeader.Location = new System.Drawing.Point(0, 0);
            this.panel_WindowHeader.Name = "panel_WindowHeader";
            this.panel_WindowHeader.Size = new System.Drawing.Size(760, 23);
            this.panel_WindowHeader.TabIndex = 0;
            this.panel_WindowHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_WindowHeader_MouseDown);
            //
            // button_WindowClose
            //
            this.button_WindowClose.BackColor = System.Drawing.Color.IndianRed;
            this.button_WindowClose.FlatAppearance.BorderSize = 0;
            this.button_WindowClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_WindowClose.ForeColor = System.Drawing.Color.White;
            this.button_WindowClose.Location = new System.Drawing.Point(736, 0);
            this.button_WindowClose.Name = "button_WindowClose";
            this.button_WindowClose.Size = new System.Drawing.Size(24, 23);
            this.button_WindowClose.TabIndex = 0;
            this.button_WindowClose.TabStop = false;
            this.button_WindowClose.Text = "X";
            this.button_WindowClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_WindowClose.UseVisualStyleBackColor = false;
            this.button_WindowClose.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_WindowClose_MouseClick);
            //
            // pictureBox_Background
            //
            this.pictureBox_Background.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_Background.Location = new System.Drawing.Point(0, 22);
            this.pictureBox_Background.Name = "pictureBox_Background";
            this.pictureBox_Background.Size = new System.Drawing.Size(760, 448);
            this.pictureBox_Background.TabIndex = 1;
            this.pictureBox_Background.TabStop = false;
            //
            // label_FormTitle
            //
            this.label_FormTitle.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_FormTitle.Location = new System.Drawing.Point(24, 35);
            this.label_FormTitle.Name = "label_FormTitle";
            this.label_FormTitle.Size = new System.Drawing.Size(712, 24);
            this.label_FormTitle.TabIndex = 2;
            this.label_FormTitle.Text = "EXTRA LOGIC MODULES";
            this.label_FormTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // label_Hint
            //
            this.label_Hint.Font = new System.Drawing.Font("Roboto", 9F);
            this.label_Hint.Location = new System.Drawing.Point(24, 64);
            this.label_Hint.Name = "label_Hint";
            this.label_Hint.Size = new System.Drawing.Size(712, 18);
            this.label_Hint.TabIndex = 3;
            this.label_Hint.Text = "Tick modules to run with the proxy. New or changed files stay disabled until you review and tick them.";
            this.label_Hint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // checkedListBox_Modules
            //
            this.checkedListBox_Modules.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.checkedListBox_Modules.Font = new System.Drawing.Font("Roboto", 9F);
            this.checkedListBox_Modules.FormattingEnabled = true;
            this.checkedListBox_Modules.HorizontalScrollbar = true;
            this.checkedListBox_Modules.IntegralHeight = false;
            this.checkedListBox_Modules.Location = new System.Drawing.Point(24, 92);
            this.checkedListBox_Modules.Name = "checkedListBox_Modules";
            this.checkedListBox_Modules.Size = new System.Drawing.Size(280, 318);
            this.checkedListBox_Modules.TabIndex = 4;
            this.checkedListBox_Modules.SelectedIndexChanged += new System.EventHandler(this.checkedListBox_Modules_SelectedIndexChanged);
            //
            // textBox_Summary
            //
            this.textBox_Summary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Summary.Font = new System.Drawing.Font("Roboto", 9F);
            this.textBox_Summary.Location = new System.Drawing.Point(316, 92);
            this.textBox_Summary.Multiline = true;
            this.textBox_Summary.Name = "textBox_Summary";
            this.textBox_Summary.ReadOnly = true;
            this.textBox_Summary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Summary.Size = new System.Drawing.Size(420, 318);
            this.textBox_Summary.TabIndex = 5;
            this.textBox_Summary.TabStop = false;
            //
            // button_OpenFolder
            //
            this.button_OpenFolder.Font = new System.Drawing.Font("Roboto", 9F);
            this.button_OpenFolder.Location = new System.Drawing.Point(24, 422);
            this.button_OpenFolder.Name = "button_OpenFolder";
            this.button_OpenFolder.Size = new System.Drawing.Size(172, 30);
            this.button_OpenFolder.TabIndex = 6;
            this.button_OpenFolder.Text = "Open Folder";
            this.button_OpenFolder.UseVisualStyleBackColor = true;
            this.button_OpenFolder.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_OpenFolder_MouseClick);
            //
            // button_Refresh
            //
            this.button_Refresh.Font = new System.Drawing.Font("Roboto", 9F);
            this.button_Refresh.Location = new System.Drawing.Point(204, 422);
            this.button_Refresh.Name = "button_Refresh";
            this.button_Refresh.Size = new System.Drawing.Size(172, 30);
            this.button_Refresh.TabIndex = 7;
            this.button_Refresh.Text = "Refresh";
            this.button_Refresh.UseVisualStyleBackColor = true;
            this.button_Refresh.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_Refresh_MouseClick);
            //
            // button_Start
            //
            this.button_Start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Start.Font = new System.Drawing.Font("Roboto", 9F);
            this.button_Start.Location = new System.Drawing.Point(384, 422);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(172, 30);
            this.button_Start.TabIndex = 8;
            this.button_Start.Text = "Start";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_Start_MouseClick);
            //
            // button_Cancel
            //
            this.button_Cancel.Font = new System.Drawing.Font("Roboto", 9F);
            this.button_Cancel.Location = new System.Drawing.Point(564, 422);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(172, 30);
            this.button_Cancel.TabIndex = 9;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_Cancel_MouseClick);
            //
            // Form_ExtraLogicManager
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(760, 470);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Start);
            this.Controls.Add(this.button_Refresh);
            this.Controls.Add(this.button_OpenFolder);
            this.Controls.Add(this.textBox_Summary);
            this.Controls.Add(this.checkedListBox_Modules);
            this.Controls.Add(this.label_Hint);
            this.Controls.Add(this.label_FormTitle);
            this.Controls.Add(this.panel_WindowHeader);
            this.Controls.Add(this.pictureBox_Background);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = global::Cursed_Market.Properties.Resources.Icon_Other;
            this.Name = "Form_ExtraLogicManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Extra Logic";
            this.TopMost = true;
            this.panel_WindowHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Background)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_WindowHeader;
        private System.Windows.Forms.Button button_WindowClose;
        private System.Windows.Forms.PictureBox pictureBox_Background;
        private System.Windows.Forms.Label label_FormTitle;
        private System.Windows.Forms.Label label_Hint;
        private System.Windows.Forms.CheckedListBox checkedListBox_Modules;
        private System.Windows.Forms.TextBox textBox_Summary;
        private System.Windows.Forms.Button button_OpenFolder;
        private System.Windows.Forms.Button button_Refresh;
        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.Button button_Cancel;
    }
}
