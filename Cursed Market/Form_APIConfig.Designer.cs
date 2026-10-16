namespace Cursed_Market
{
    partial class Form_APIConfig
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
            this.label_FormTitle = new System.Windows.Forms.Label();
            this.pictureBox_Background = new System.Windows.Forms.PictureBox();
            this.label_Host = new System.Windows.Forms.Label();
            this.textBox_Host = new System.Windows.Forms.TextBox();
            this.label_Status = new System.Windows.Forms.Label();
            this.textBox_Status = new System.Windows.Forms.TextBox();
            this.label_Update = new System.Windows.Forms.Label();
            this.textBox_Update = new System.Windows.Forms.TextBox();
            this.label_MarketFull = new System.Windows.Forms.Label();
            this.textBox_MarketFull = new System.Windows.Forms.TextBox();
            this.label_MarketDLC = new System.Windows.Forms.Label();
            this.textBox_MarketDLC = new System.Windows.Forms.TextBox();
            this.label_CharacterData = new System.Windows.Forms.Label();
            this.textBox_CharacterData = new System.Windows.Forms.TextBox();
            this.label_BloodwebData = new System.Windows.Forms.Label();
            this.textBox_BloodwebData = new System.Windows.Forms.TextBox();
            this.label_CharactersList = new System.Windows.Forms.Label();
            this.textBox_CharactersList = new System.Windows.Forms.TextBox();
            this.label_ItemsList = new System.Windows.Forms.Label();
            this.textBox_ItemsList = new System.Windows.Forms.TextBox();
            this.label_Catalog = new System.Windows.Forms.Label();
            this.textBox_Catalog = new System.Windows.Forms.TextBox();
            this.label_AntiKillSwitch = new System.Windows.Forms.Label();
            this.textBox_AntiKillSwitch = new System.Windows.Forms.TextBox();
            this.label_RobotoFont = new System.Windows.Forms.Label();
            this.textBox_RobotoFont = new System.Windows.Forms.TextBox();
            this.label_CharactersPreset = new System.Windows.Forms.Label();
            this.textBox_CharactersPreset = new System.Windows.Forms.TextBox();
            this.button_ResetDefault = new System.Windows.Forms.Button();
            this.button_SaveChanges = new System.Windows.Forms.Button();
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
            this.label_FormTitle.TabIndex = 2;
            this.label_FormTitle.Text = "CONFIGURE API ENDPOINTS";
            this.label_FormTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox_Background
            // 
            this.pictureBox_Background.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_Background.Location = new System.Drawing.Point(0, 22);
            this.pictureBox_Background.Name = "pictureBox_Background";
            this.pictureBox_Background.Size = new System.Drawing.Size(700, 473);
            this.pictureBox_Background.TabIndex = 1;
            this.pictureBox_Background.TabStop = false;
            // 
            // label_Host
            // 
            this.label_Host.Font = new System.Drawing.Font("Roboto", 9F);
            this.label_Host.Location = new System.Drawing.Point(24, 75);
            this.label_Host.Name = "label_Host";
            this.label_Host.Size = new System.Drawing.Size(310, 15);
            this.label_Host.TabIndex = 25;
            this.label_Host.Text = "API HOST";
            this.label_Host.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // textBox_Host
            // 
            this.textBox_Host.Font = new System.Drawing.Font("Roboto", 9F);
            this.textBox_Host.Location = new System.Drawing.Point(24, 93);
            this.textBox_Host.Name = "textBox_Host";
            this.textBox_Host.Size = new System.Drawing.Size(310, 22);
            this.textBox_Host.TabIndex = 24;
            this.textBox_Host.TextChanged += new System.EventHandler(this.OnParameterChanged);
            // 
            // label_Status
            // 
            this.label_Status.Font = new System.Drawing.Font("Roboto", 9F);
            this.label_Status.Location = new System.Drawing.Point(354, 75);
            this.label_Status.Name = "label_Status";
            this.label_Status.Size = new System.Drawing.Size(310, 15);
            this.label_Status.TabIndex = 23;
            this.label_Status.Text = "STATUS PATH";
            this.label_Status.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // textBox_Status
            // 
            this.textBox_Status.Font = new System.Drawing.Font("Roboto", 9F);
            this.textBox_Status.Location = new System.Drawing.Point(354, 93);
            this.textBox_Status.Name = "textBox_Status";
            this.textBox_Status.Size = new System.Drawing.Size(310, 22);
            this.textBox_Status.TabIndex = 22;
            this.textBox_Status.TextChanged += new System.EventHandler(this.OnParameterChanged);
            // 
            // label_Update
            // 
            this.label_Update.Font = new System.Drawing.Font("Roboto", 9F);
            this.label_Update.Location = new System.Drawing.Point(24, 125);
            this.label_Update.Name = "label_Update";
            this.label_Update.Size = new System.Drawing.Size(310, 15);
            this.label_Update.TabIndex = 21;
            this.label_Update.Text = "UPDATE PATH";
            this.label_Update.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // textBox_Update
            // 
            this.textBox_Update.Font = new System.Drawing.Font("Roboto", 9F);
            this.textBox_Update.Location = new System.Drawing.Point(24, 143);
            this.textBox_Update.Name = "textBox_Update";
            this.textBox_Update.Size = new System.Drawing.Size(310, 22);
            this.textBox_Update.TabIndex = 20;
            this.textBox_Update.TextChanged += new System.EventHandler(this.OnParameterChanged);
            // 
            // label_MarketFull
            // 
            this.label_MarketFull.Font = new System.Drawing.Font("Roboto", 9F);
            this.label_MarketFull.Location = new System.Drawing.Point(354, 125);
            this.label_MarketFull.Name = "label_MarketFull";
            this.label_MarketFull.Size = new System.Drawing.Size(310, 15);
            this.label_MarketFull.TabIndex = 19;
            this.label_MarketFull.Text = "MARKET FULL PATH";
            this.label_MarketFull.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // textBox_MarketFull
            // 
            this.textBox_MarketFull.Font = new System.Drawing.Font("Roboto", 9F);
            this.textBox_MarketFull.Location = new System.Drawing.Point(354, 143);
            this.textBox_MarketFull.Name = "textBox_MarketFull";
            this.textBox_MarketFull.Size = new System.Drawing.Size(310, 22);
            this.textBox_MarketFull.TabIndex = 18;
            this.textBox_MarketFull.TextChanged += new System.EventHandler(this.OnParameterChanged);
            // 
            // label_MarketDLC
            // 
            this.label_MarketDLC.Font = new System.Drawing.Font("Roboto", 9F);
            this.label_MarketDLC.Location = new System.Drawing.Point(24, 175);
            this.label_MarketDLC.Name = "label_MarketDLC";
            this.label_MarketDLC.Size = new System.Drawing.Size(310, 15);
            this.label_MarketDLC.TabIndex = 17;
            this.label_MarketDLC.Text = "MARKET DLC PATH";
            this.label_MarketDLC.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // textBox_MarketDLC
            // 
            this.textBox_MarketDLC.Font = new System.Drawing.Font("Roboto", 9F);
            this.textBox_MarketDLC.Location = new System.Drawing.Point(24, 193);
            this.textBox_MarketDLC.Name = "textBox_MarketDLC";
            this.textBox_MarketDLC.Size = new System.Drawing.Size(310, 22);
            this.textBox_MarketDLC.TabIndex = 16;
            this.textBox_MarketDLC.TextChanged += new System.EventHandler(this.OnParameterChanged);
            // 
            // label_CharacterData
            // 
            this.label_CharacterData.Font = new System.Drawing.Font("Roboto", 9F);
            this.label_CharacterData.Location = new System.Drawing.Point(354, 175);
            this.label_CharacterData.Name = "label_CharacterData";
            this.label_CharacterData.Size = new System.Drawing.Size(310, 15);
            this.label_CharacterData.TabIndex = 15;
            this.label_CharacterData.Text = "CHARACTER DATA PATH";
            this.label_CharacterData.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // textBox_CharacterData
            // 
            this.textBox_CharacterData.Font = new System.Drawing.Font("Roboto", 9F);
            this.textBox_CharacterData.Location = new System.Drawing.Point(354, 193);
            this.textBox_CharacterData.Name = "textBox_CharacterData";
            this.textBox_CharacterData.Size = new System.Drawing.Size(310, 22);
            this.textBox_CharacterData.TabIndex = 14;
            this.textBox_CharacterData.TextChanged += new System.EventHandler(this.OnParameterChanged);
            // 
            // label_BloodwebData
            // 
            this.label_BloodwebData.Font = new System.Drawing.Font("Roboto", 9F);
            this.label_BloodwebData.Location = new System.Drawing.Point(24, 225);
            this.label_BloodwebData.Name = "label_BloodwebData";
            this.label_BloodwebData.Size = new System.Drawing.Size(310, 15);
            this.label_BloodwebData.TabIndex = 13;
            this.label_BloodwebData.Text = "BLOODWEB DATA PATH";
            this.label_BloodwebData.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // textBox_BloodwebData
            // 
            this.textBox_BloodwebData.Font = new System.Drawing.Font("Roboto", 9F);
            this.textBox_BloodwebData.Location = new System.Drawing.Point(24, 243);
            this.textBox_BloodwebData.Name = "textBox_BloodwebData";
            this.textBox_BloodwebData.Size = new System.Drawing.Size(310, 22);
            this.textBox_BloodwebData.TabIndex = 12;
            this.textBox_BloodwebData.TextChanged += new System.EventHandler(this.OnParameterChanged);
            // 
            // label_CharactersList
            // 
            this.label_CharactersList.Font = new System.Drawing.Font("Roboto", 9F);
            this.label_CharactersList.Location = new System.Drawing.Point(354, 225);
            this.label_CharactersList.Name = "label_CharactersList";
            this.label_CharactersList.Size = new System.Drawing.Size(310, 15);
            this.label_CharactersList.TabIndex = 11;
            this.label_CharactersList.Text = "CHARACTERS LIST PATH";
            this.label_CharactersList.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // textBox_CharactersList
            // 
            this.textBox_CharactersList.Font = new System.Drawing.Font("Roboto", 9F);
            this.textBox_CharactersList.Location = new System.Drawing.Point(354, 243);
            this.textBox_CharactersList.Name = "textBox_CharactersList";
            this.textBox_CharactersList.Size = new System.Drawing.Size(310, 22);
            this.textBox_CharactersList.TabIndex = 10;
            this.textBox_CharactersList.TextChanged += new System.EventHandler(this.OnParameterChanged);
            // 
            // label_ItemsList
            // 
            this.label_ItemsList.Font = new System.Drawing.Font("Roboto", 9F);
            this.label_ItemsList.Location = new System.Drawing.Point(24, 275);
            this.label_ItemsList.Name = "label_ItemsList";
            this.label_ItemsList.Size = new System.Drawing.Size(310, 15);
            this.label_ItemsList.TabIndex = 9;
            this.label_ItemsList.Text = "ITEMS LIST PATH";
            this.label_ItemsList.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // textBox_ItemsList
            // 
            this.textBox_ItemsList.Font = new System.Drawing.Font("Roboto", 9F);
            this.textBox_ItemsList.Location = new System.Drawing.Point(24, 293);
            this.textBox_ItemsList.Name = "textBox_ItemsList";
            this.textBox_ItemsList.Size = new System.Drawing.Size(310, 22);
            this.textBox_ItemsList.TabIndex = 8;
            this.textBox_ItemsList.TextChanged += new System.EventHandler(this.OnParameterChanged);
            // 
            // label_Catalog
            // 
            this.label_Catalog.Font = new System.Drawing.Font("Roboto", 9F);
            this.label_Catalog.Location = new System.Drawing.Point(354, 275);
            this.label_Catalog.Name = "label_Catalog";
            this.label_Catalog.Size = new System.Drawing.Size(310, 15);
            this.label_Catalog.TabIndex = 7;
            this.label_Catalog.Text = "CATALOG PATH";
            this.label_Catalog.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // textBox_Catalog
            // 
            this.textBox_Catalog.Font = new System.Drawing.Font("Roboto", 9F);
            this.textBox_Catalog.Location = new System.Drawing.Point(354, 293);
            this.textBox_Catalog.Name = "textBox_Catalog";
            this.textBox_Catalog.Size = new System.Drawing.Size(310, 22);
            this.textBox_Catalog.TabIndex = 6;
            this.textBox_Catalog.TextChanged += new System.EventHandler(this.OnParameterChanged);
            // 
            // label_AntiKillSwitch
            // 
            this.label_AntiKillSwitch.Font = new System.Drawing.Font("Roboto", 9F);
            this.label_AntiKillSwitch.Location = new System.Drawing.Point(24, 325);
            this.label_AntiKillSwitch.Name = "label_AntiKillSwitch";
            this.label_AntiKillSwitch.Size = new System.Drawing.Size(310, 15);
            this.label_AntiKillSwitch.TabIndex = 5;
            this.label_AntiKillSwitch.Text = "ANTI KILL SWITCH PATH";
            this.label_AntiKillSwitch.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // textBox_AntiKillSwitch
            // 
            this.textBox_AntiKillSwitch.Font = new System.Drawing.Font("Roboto", 9F);
            this.textBox_AntiKillSwitch.Location = new System.Drawing.Point(24, 343);
            this.textBox_AntiKillSwitch.Name = "textBox_AntiKillSwitch";
            this.textBox_AntiKillSwitch.Size = new System.Drawing.Size(310, 22);
            this.textBox_AntiKillSwitch.TabIndex = 4;
            this.textBox_AntiKillSwitch.TextChanged += new System.EventHandler(this.OnParameterChanged);
            // 
            // label_RobotoFont
            // 
            this.label_RobotoFont.Font = new System.Drawing.Font("Roboto", 9F);
            this.label_RobotoFont.Location = new System.Drawing.Point(354, 325);
            this.label_RobotoFont.Name = "label_RobotoFont";
            this.label_RobotoFont.Size = new System.Drawing.Size(310, 15);
            this.label_RobotoFont.TabIndex = 3;
            this.label_RobotoFont.Text = "ROBOTO FONT PATH";
            this.label_RobotoFont.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // textBox_RobotoFont
            // 
            this.textBox_RobotoFont.Font = new System.Drawing.Font("Roboto", 9F);
            this.textBox_RobotoFont.Location = new System.Drawing.Point(354, 343);
            this.textBox_RobotoFont.Name = "textBox_RobotoFont";
            this.textBox_RobotoFont.Size = new System.Drawing.Size(310, 22);
            this.textBox_RobotoFont.TabIndex = 2;
            this.textBox_RobotoFont.TextChanged += new System.EventHandler(this.OnParameterChanged);
            // 
            // label_CharactersPreset
            // 
            this.label_CharactersPreset.Font = new System.Drawing.Font("Roboto", 9F);
            this.label_CharactersPreset.Location = new System.Drawing.Point(24, 375);
            this.label_CharactersPreset.Name = "label_CharactersPreset";
            this.label_CharactersPreset.Size = new System.Drawing.Size(310, 15);
            this.label_CharactersPreset.TabIndex = 3;
            this.label_CharactersPreset.Text = "CHARACTERS PRESET PATH";
            this.label_CharactersPreset.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // textBox_CharactersPreset
            // 
            this.textBox_CharactersPreset.Font = new System.Drawing.Font("Roboto", 9F);
            this.textBox_CharactersPreset.Location = new System.Drawing.Point(24, 393);
            this.textBox_CharactersPreset.Name = "textBox_CharactersPreset";
            this.textBox_CharactersPreset.Size = new System.Drawing.Size(310, 22);
            this.textBox_CharactersPreset.TabIndex = 2;
            this.textBox_CharactersPreset.TextChanged += new System.EventHandler(this.OnParameterChanged);
            // 
            // button_ResetDefault
            // 
            this.button_ResetDefault.Font = new System.Drawing.Font("Roboto", 9F);
            this.button_ResetDefault.Location = new System.Drawing.Point(24, 445);
            this.button_ResetDefault.Name = "button_ResetDefault";
            this.button_ResetDefault.Size = new System.Drawing.Size(310, 30);
            this.button_ResetDefault.TabIndex = 1;
            this.button_ResetDefault.Text = "Reset to Defaults";
            this.button_ResetDefault.UseVisualStyleBackColor = true;
            this.button_ResetDefault.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_ResetDefault_MouseClick);
            // 
            // button_SaveChanges
            // 
            this.button_SaveChanges.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_SaveChanges.Font = new System.Drawing.Font("Roboto", 9F);
            this.button_SaveChanges.Location = new System.Drawing.Point(354, 445);
            this.button_SaveChanges.Name = "button_SaveChanges";
            this.button_SaveChanges.Size = new System.Drawing.Size(310, 30);
            this.button_SaveChanges.TabIndex = 0;
            this.button_SaveChanges.Text = "Confirm and Save Changes";
            this.button_SaveChanges.UseVisualStyleBackColor = true;
            this.button_SaveChanges.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_SaveChanges_MouseClick);
            // 
            // Form_APIConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(700, 495);
            this.Controls.Add(this.button_SaveChanges);
            this.Controls.Add(this.button_ResetDefault);
            this.Controls.Add(this.textBox_CharactersPreset);
            this.Controls.Add(this.label_CharactersPreset);
            this.Controls.Add(this.textBox_RobotoFont);
            this.Controls.Add(this.label_RobotoFont);
            this.Controls.Add(this.textBox_AntiKillSwitch);
            this.Controls.Add(this.label_AntiKillSwitch);
            this.Controls.Add(this.textBox_Catalog);
            this.Controls.Add(this.label_Catalog);
            this.Controls.Add(this.textBox_ItemsList);
            this.Controls.Add(this.label_ItemsList);
            this.Controls.Add(this.textBox_CharactersList);
            this.Controls.Add(this.label_CharactersList);
            this.Controls.Add(this.textBox_BloodwebData);
            this.Controls.Add(this.label_BloodwebData);
            this.Controls.Add(this.textBox_CharacterData);
            this.Controls.Add(this.label_CharacterData);
            this.Controls.Add(this.textBox_MarketDLC);
            this.Controls.Add(this.label_MarketDLC);
            this.Controls.Add(this.textBox_MarketFull);
            this.Controls.Add(this.label_MarketFull);
            this.Controls.Add(this.textBox_Update);
            this.Controls.Add(this.label_Update);
            this.Controls.Add(this.textBox_Status);
            this.Controls.Add(this.label_Status);
            this.Controls.Add(this.textBox_Host);
            this.Controls.Add(this.label_Host);
            this.Controls.Add(this.label_FormTitle);
            this.Controls.Add(this.panel_WindowHeader);
            this.Controls.Add(this.pictureBox_Background);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = global::Cursed_Market.Properties.Resources.Icon_Other;
            this.Name = "Form_APIConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "API Endpoints Configurator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_APIConfig_FormClosing);
            this.Shown += new System.EventHandler(this.Form_APIConfig_Shown);
            this.panel_WindowHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Background)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_WindowHeader;
        private System.Windows.Forms.Button button_WindowClose;
        private System.Windows.Forms.Label label_FormTitle;
        private System.Windows.Forms.PictureBox pictureBox_Background;

        private System.Windows.Forms.Label label_Host;
        private System.Windows.Forms.TextBox textBox_Host;
        private System.Windows.Forms.Label label_Status;
        private System.Windows.Forms.TextBox textBox_Status;
        private System.Windows.Forms.Label label_Update;
        private System.Windows.Forms.TextBox textBox_Update;
        private System.Windows.Forms.Label label_MarketFull;
        private System.Windows.Forms.TextBox textBox_MarketFull;
        private System.Windows.Forms.Label label_MarketDLC;
        private System.Windows.Forms.TextBox textBox_MarketDLC;
        private System.Windows.Forms.Label label_CharacterData;
        private System.Windows.Forms.TextBox textBox_CharacterData;
        private System.Windows.Forms.Label label_BloodwebData;
        private System.Windows.Forms.TextBox textBox_BloodwebData;
        private System.Windows.Forms.Label label_CharactersList;
        private System.Windows.Forms.TextBox textBox_CharactersList;
        private System.Windows.Forms.Label label_ItemsList;
        private System.Windows.Forms.TextBox textBox_ItemsList;
        private System.Windows.Forms.Label label_Catalog;
        private System.Windows.Forms.TextBox textBox_Catalog;
        private System.Windows.Forms.Label label_AntiKillSwitch;
        private System.Windows.Forms.TextBox textBox_AntiKillSwitch;
        private System.Windows.Forms.Label label_RobotoFont;
        private System.Windows.Forms.TextBox textBox_RobotoFont;
        private System.Windows.Forms.Label label_CharactersPreset;
        private System.Windows.Forms.TextBox textBox_CharactersPreset;

        private System.Windows.Forms.Button button_ResetDefault;
        private System.Windows.Forms.Button button_SaveChanges;
    }
}