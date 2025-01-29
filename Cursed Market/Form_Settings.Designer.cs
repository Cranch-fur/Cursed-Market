
namespace Cursed_Market
{
    partial class Form_Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Settings));
            this.panel_WindowHeader = new System.Windows.Forms.Panel();
            this.button_WindowClose = new System.Windows.Forms.Button();
            this.textBox_Culture = new System.Windows.Forms.TextBox();
            this.comboBox_ApplicationTheme = new System.Windows.Forms.ComboBox();
            this.label_ApplicationThemeTitle = new System.Windows.Forms.Label();
            this.pictureBox_Background = new System.Windows.Forms.PictureBox();
            this.button_ClearCertificates = new System.Windows.Forms.Button();
            this.pictureBox_ControlPanelSubFrame = new System.Windows.Forms.PictureBox();
            this.label_ControlPanelTitle = new System.Windows.Forms.Label();
            this.button_ResetApplicationSettings = new System.Windows.Forms.Button();
            this.button_DisableProxy = new System.Windows.Forms.Button();
            this.textBox_Platform = new System.Windows.Forms.TextBox();
            this.label_CultureTitle = new System.Windows.Forms.Label();
            this.label_PlatformTitle = new System.Windows.Forms.Label();
            this.label_DebugInfoTitle = new System.Windows.Forms.Label();
            this.button_UpdateDebug = new System.Windows.Forms.Button();
            this.comboBox_DisabledPlatforms = new System.Windows.Forms.ComboBox();
            this.button_RemoveDisabledPlatform = new System.Windows.Forms.Button();
            this.pictureBox_DebugInfoFrame = new System.Windows.Forms.PictureBox();
            this.label_MatchFoundSoundTitle = new System.Windows.Forms.Label();
            this.comboBox_MatchFoundSound = new System.Windows.Forms.ComboBox();
            this.pictureBox_ControlPanelFrame = new System.Windows.Forms.PictureBox();
            this.button_QueueNotifySound = new System.Windows.Forms.Button();
            this.pictureBox_Discord = new System.Windows.Forms.PictureBox();
            this.button_CreateCertificatePassword = new System.Windows.Forms.Button();
            this.button_OpenCertificateDirectory = new System.Windows.Forms.Button();
            this.label_DisabledPlatformsTitle = new System.Windows.Forms.Label();
            this.pictureBox_Telegram = new System.Windows.Forms.PictureBox();
            this.pictureBox_Boosty = new System.Windows.Forms.PictureBox();
            this.panel_WindowHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Background)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ControlPanelSubFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DebugInfoFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ControlPanelFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Discord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Telegram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Boosty)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_WindowHeader
            // 
            this.panel_WindowHeader.BackColor = System.Drawing.SystemColors.Control;
            this.panel_WindowHeader.Controls.Add(this.button_WindowClose);
            this.panel_WindowHeader.Location = new System.Drawing.Point(0, 0);
            this.panel_WindowHeader.Name = "panel_WindowHeader";
            this.panel_WindowHeader.Size = new System.Drawing.Size(600, 23);
            this.panel_WindowHeader.TabIndex = 2;
            this.panel_WindowHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_WindowHeader_MouseDown);
            // 
            // button_WindowClose
            // 
            this.button_WindowClose.BackColor = System.Drawing.Color.IndianRed;
            this.button_WindowClose.FlatAppearance.BorderSize = 0;
            this.button_WindowClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_WindowClose.ForeColor = System.Drawing.Color.White;
            this.button_WindowClose.Location = new System.Drawing.Point(576, 0);
            this.button_WindowClose.Name = "button_WindowClose";
            this.button_WindowClose.Size = new System.Drawing.Size(24, 23);
            this.button_WindowClose.TabIndex = 0;
            this.button_WindowClose.Text = "X";
            this.button_WindowClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_WindowClose.UseVisualStyleBackColor = false;
            this.button_WindowClose.Click += new System.EventHandler(this.button_WindowClose_Click);
            // 
            // textBox_Culture
            // 
            this.textBox_Culture.BackColor = System.Drawing.Color.White;
            this.textBox_Culture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Culture.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_Culture.Location = new System.Drawing.Point(107, 111);
            this.textBox_Culture.Name = "textBox_Culture";
            this.textBox_Culture.ReadOnly = true;
            this.textBox_Culture.Size = new System.Drawing.Size(180, 22);
            this.textBox_Culture.TabIndex = 6;
            // 
            // comboBox_ApplicationTheme
            // 
            this.comboBox_ApplicationTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ApplicationTheme.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox_ApplicationTheme.FormattingEnabled = true;
            this.comboBox_ApplicationTheme.Items.AddRange(new object[] {
            "Default (White Theme)",
            "Lost Legacy",
            "Dark Memories",
            "Saints Row",
            "Dracula",
            "Christmas"});
            this.comboBox_ApplicationTheme.Location = new System.Drawing.Point(107, 199);
            this.comboBox_ApplicationTheme.Name = "comboBox_ApplicationTheme";
            this.comboBox_ApplicationTheme.Size = new System.Drawing.Size(180, 21);
            this.comboBox_ApplicationTheme.TabIndex = 10;
            this.comboBox_ApplicationTheme.SelectedIndexChanged += new System.EventHandler(this.comboBox_Themes_SelectedIndexChanged);
            // 
            // label_ApplicationThemeTitle
            // 
            this.label_ApplicationThemeTitle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_ApplicationThemeTitle.Location = new System.Drawing.Point(107, 181);
            this.label_ApplicationThemeTitle.Name = "label_ApplicationThemeTitle";
            this.label_ApplicationThemeTitle.Size = new System.Drawing.Size(180, 15);
            this.label_ApplicationThemeTitle.TabIndex = 11;
            this.label_ApplicationThemeTitle.Text = "Application Theme";
            this.label_ApplicationThemeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox_Background
            // 
            this.pictureBox_Background.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_Background.Location = new System.Drawing.Point(0, 22);
            this.pictureBox_Background.Name = "pictureBox_Background";
            this.pictureBox_Background.Size = new System.Drawing.Size(600, 378);
            this.pictureBox_Background.TabIndex = 12;
            this.pictureBox_Background.TabStop = false;
            // 
            // button_ClearCertificates
            // 
            this.button_ClearCertificates.BackColor = System.Drawing.Color.Transparent;
            this.button_ClearCertificates.FlatAppearance.BorderSize = 0;
            this.button_ClearCertificates.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_ClearCertificates.ForeColor = System.Drawing.Color.Black;
            this.button_ClearCertificates.Location = new System.Drawing.Point(107, 249);
            this.button_ClearCertificates.Name = "button_ClearCertificates";
            this.button_ClearCertificates.Size = new System.Drawing.Size(180, 25);
            this.button_ClearCertificates.TabIndex = 13;
            this.button_ClearCertificates.Text = "Clear Fiddler Certificates";
            this.button_ClearCertificates.UseVisualStyleBackColor = false;
            this.button_ClearCertificates.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_ClearCertificates_MouseClick);
            // 
            // pictureBox_ControlPanelSubFrame
            // 
            this.pictureBox_ControlPanelSubFrame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_ControlPanelSubFrame.Location = new System.Drawing.Point(91, 235);
            this.pictureBox_ControlPanelSubFrame.Name = "pictureBox_ControlPanelSubFrame";
            this.pictureBox_ControlPanelSubFrame.Size = new System.Drawing.Size(404, 129);
            this.pictureBox_ControlPanelSubFrame.TabIndex = 14;
            this.pictureBox_ControlPanelSubFrame.TabStop = false;
            // 
            // label_ControlPanelTitle
            // 
            this.label_ControlPanelTitle.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_ControlPanelTitle.Location = new System.Drawing.Point(107, 159);
            this.label_ControlPanelTitle.Name = "label_ControlPanelTitle";
            this.label_ControlPanelTitle.Size = new System.Drawing.Size(372, 18);
            this.label_ControlPanelTitle.TabIndex = 15;
            this.label_ControlPanelTitle.Text = "CONTROL PANEL";
            this.label_ControlPanelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_ResetApplicationSettings
            // 
            this.button_ResetApplicationSettings.BackColor = System.Drawing.Color.Transparent;
            this.button_ResetApplicationSettings.FlatAppearance.BorderSize = 0;
            this.button_ResetApplicationSettings.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_ResetApplicationSettings.ForeColor = System.Drawing.Color.Black;
            this.button_ResetApplicationSettings.Location = new System.Drawing.Point(299, 280);
            this.button_ResetApplicationSettings.Name = "button_ResetApplicationSettings";
            this.button_ResetApplicationSettings.Size = new System.Drawing.Size(180, 25);
            this.button_ResetApplicationSettings.TabIndex = 16;
            this.button_ResetApplicationSettings.Text = "Reset Cursed Market Settings";
            this.button_ResetApplicationSettings.UseVisualStyleBackColor = false;
            this.button_ResetApplicationSettings.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_SettingsReset_MouseClick);
            // 
            // button_DisableProxy
            // 
            this.button_DisableProxy.BackColor = System.Drawing.Color.Transparent;
            this.button_DisableProxy.FlatAppearance.BorderSize = 0;
            this.button_DisableProxy.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_DisableProxy.ForeColor = System.Drawing.Color.Black;
            this.button_DisableProxy.Location = new System.Drawing.Point(107, 280);
            this.button_DisableProxy.Name = "button_DisableProxy";
            this.button_DisableProxy.Size = new System.Drawing.Size(180, 25);
            this.button_DisableProxy.TabIndex = 17;
            this.button_DisableProxy.Text = "Fix Internet (Disable Proxy)";
            this.button_DisableProxy.UseVisualStyleBackColor = false;
            this.button_DisableProxy.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_DisableProxy_MouseClick);
            // 
            // textBox_Platform
            // 
            this.textBox_Platform.BackColor = System.Drawing.Color.White;
            this.textBox_Platform.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Platform.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_Platform.Location = new System.Drawing.Point(299, 111);
            this.textBox_Platform.Name = "textBox_Platform";
            this.textBox_Platform.ReadOnly = true;
            this.textBox_Platform.Size = new System.Drawing.Size(180, 22);
            this.textBox_Platform.TabIndex = 18;
            // 
            // label_CultureTitle
            // 
            this.label_CultureTitle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_CultureTitle.Location = new System.Drawing.Point(107, 92);
            this.label_CultureTitle.Name = "label_CultureTitle";
            this.label_CultureTitle.Size = new System.Drawing.Size(180, 16);
            this.label_CultureTitle.TabIndex = 19;
            this.label_CultureTitle.Text = "Windows System Culture";
            this.label_CultureTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_PlatformTitle
            // 
            this.label_PlatformTitle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_PlatformTitle.Location = new System.Drawing.Point(299, 92);
            this.label_PlatformTitle.Name = "label_PlatformTitle";
            this.label_PlatformTitle.Size = new System.Drawing.Size(180, 16);
            this.label_PlatformTitle.TabIndex = 20;
            this.label_PlatformTitle.Text = "Platform Running";
            this.label_PlatformTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_DebugInfoTitle
            // 
            this.label_DebugInfoTitle.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_DebugInfoTitle.Location = new System.Drawing.Point(107, 43);
            this.label_DebugInfoTitle.Name = "label_DebugInfoTitle";
            this.label_DebugInfoTitle.Size = new System.Drawing.Size(372, 18);
            this.label_DebugInfoTitle.TabIndex = 21;
            this.label_DebugInfoTitle.Text = "DEBUG INFORMATION";
            this.label_DebugInfoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_UpdateDebug
            // 
            this.button_UpdateDebug.BackColor = System.Drawing.Color.Transparent;
            this.button_UpdateDebug.FlatAppearance.BorderSize = 0;
            this.button_UpdateDebug.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_UpdateDebug.ForeColor = System.Drawing.Color.Black;
            this.button_UpdateDebug.Location = new System.Drawing.Point(204, 64);
            this.button_UpdateDebug.Name = "button_UpdateDebug";
            this.button_UpdateDebug.Size = new System.Drawing.Size(180, 24);
            this.button_UpdateDebug.TabIndex = 22;
            this.button_UpdateDebug.Text = "Update Debug Info";
            this.button_UpdateDebug.UseVisualStyleBackColor = false;
            this.button_UpdateDebug.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_UpdateDebug_MouseClick);
            // 
            // comboBox_DisabledPlatforms
            // 
            this.comboBox_DisabledPlatforms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_DisabledPlatforms.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox_DisabledPlatforms.FormattingEnabled = true;
            this.comboBox_DisabledPlatforms.Location = new System.Drawing.Point(107, 331);
            this.comboBox_DisabledPlatforms.Name = "comboBox_DisabledPlatforms";
            this.comboBox_DisabledPlatforms.Size = new System.Drawing.Size(112, 21);
            this.comboBox_DisabledPlatforms.TabIndex = 23;
            // 
            // button_RemoveDisabledPlatform
            // 
            this.button_RemoveDisabledPlatform.BackColor = System.Drawing.Color.Transparent;
            this.button_RemoveDisabledPlatform.FlatAppearance.BorderSize = 0;
            this.button_RemoveDisabledPlatform.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_RemoveDisabledPlatform.ForeColor = System.Drawing.Color.Black;
            this.button_RemoveDisabledPlatform.Location = new System.Drawing.Point(230, 331);
            this.button_RemoveDisabledPlatform.Name = "button_RemoveDisabledPlatform";
            this.button_RemoveDisabledPlatform.Size = new System.Drawing.Size(248, 23);
            this.button_RemoveDisabledPlatform.TabIndex = 24;
            this.button_RemoveDisabledPlatform.Text = "Remove From Disabled Platforms";
            this.button_RemoveDisabledPlatform.UseVisualStyleBackColor = false;
            this.button_RemoveDisabledPlatform.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_LimitedPlatforms_MouseClick);
            // 
            // pictureBox_DebugInfoFrame
            // 
            this.pictureBox_DebugInfoFrame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_DebugInfoFrame.Location = new System.Drawing.Point(91, 50);
            this.pictureBox_DebugInfoFrame.Name = "pictureBox_DebugInfoFrame";
            this.pictureBox_DebugInfoFrame.Size = new System.Drawing.Size(404, 98);
            this.pictureBox_DebugInfoFrame.TabIndex = 25;
            this.pictureBox_DebugInfoFrame.TabStop = false;
            // 
            // label_MatchFoundSoundTitle
            // 
            this.label_MatchFoundSoundTitle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_MatchFoundSoundTitle.Location = new System.Drawing.Point(299, 181);
            this.label_MatchFoundSoundTitle.Name = "label_MatchFoundSoundTitle";
            this.label_MatchFoundSoundTitle.Size = new System.Drawing.Size(180, 15);
            this.label_MatchFoundSoundTitle.TabIndex = 27;
            this.label_MatchFoundSoundTitle.Text = "Match Found Sound";
            this.label_MatchFoundSoundTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox_MatchFoundSound
            // 
            this.comboBox_MatchFoundSound.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_MatchFoundSound.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox_MatchFoundSound.FormattingEnabled = true;
            this.comboBox_MatchFoundSound.Items.AddRange(new object[] {
            "Disabled",
            "Gong Hit",
            "\"OK, Let\'s GO\"",
            "NFL Theme",
            "Gay Echo",
            "Rizz Melody",
            "Run Song",
            "Pedro Song",
            "Military Horn"});
            this.comboBox_MatchFoundSound.Location = new System.Drawing.Point(299, 199);
            this.comboBox_MatchFoundSound.Name = "comboBox_MatchFoundSound";
            this.comboBox_MatchFoundSound.Size = new System.Drawing.Size(152, 21);
            this.comboBox_MatchFoundSound.TabIndex = 26;
            this.comboBox_MatchFoundSound.SelectedIndexChanged += new System.EventHandler(this.comboBox_QueueNotifySound_SelectedIndexChanged);
            // 
            // pictureBox_ControlPanelFrame
            // 
            this.pictureBox_ControlPanelFrame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_ControlPanelFrame.Location = new System.Drawing.Point(91, 167);
            this.pictureBox_ControlPanelFrame.Name = "pictureBox_ControlPanelFrame";
            this.pictureBox_ControlPanelFrame.Size = new System.Drawing.Size(404, 197);
            this.pictureBox_ControlPanelFrame.TabIndex = 28;
            this.pictureBox_ControlPanelFrame.TabStop = false;
            // 
            // button_QueueNotifySound
            // 
            this.button_QueueNotifySound.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_QueueNotifySound.Location = new System.Drawing.Point(454, 198);
            this.button_QueueNotifySound.Name = "button_QueueNotifySound";
            this.button_QueueNotifySound.Size = new System.Drawing.Size(25, 23);
            this.button_QueueNotifySound.TabIndex = 29;
            this.button_QueueNotifySound.Text = "▶";
            this.button_QueueNotifySound.UseVisualStyleBackColor = true;
            this.button_QueueNotifySound.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_QueueNotifySound_MouseClick);
            // 
            // pictureBox_Discord
            // 
            this.pictureBox_Discord.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_Discord.Image = global::Cursed_Market.Properties.Resources.IMG_SOCIAL_DISCORD_BLACK;
            this.pictureBox_Discord.Location = new System.Drawing.Point(246, 365);
            this.pictureBox_Discord.Name = "pictureBox_Discord";
            this.pictureBox_Discord.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Discord.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Discord.TabIndex = 46;
            this.pictureBox_Discord.TabStop = false;
            this.pictureBox_Discord.Visible = false;
            this.pictureBox_Discord.Click += new System.EventHandler(this.pictureBox_Discord_Click);
            // 
            // button_CreateCertificatePassword
            // 
            this.button_CreateCertificatePassword.BackColor = System.Drawing.Color.Transparent;
            this.button_CreateCertificatePassword.FlatAppearance.BorderSize = 0;
            this.button_CreateCertificatePassword.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_CreateCertificatePassword.ForeColor = System.Drawing.Color.Black;
            this.button_CreateCertificatePassword.Location = new System.Drawing.Point(299, 249);
            this.button_CreateCertificatePassword.Name = "button_CreateCertificatePassword";
            this.button_CreateCertificatePassword.Size = new System.Drawing.Size(152, 25);
            this.button_CreateCertificatePassword.TabIndex = 47;
            this.button_CreateCertificatePassword.Text = "Export Cert. Password";
            this.button_CreateCertificatePassword.UseVisualStyleBackColor = false;
            this.button_CreateCertificatePassword.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_CreateCertificatePassword_MouseClick);
            // 
            // button_OpenCertificateDirectory
            // 
            this.button_OpenCertificateDirectory.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_OpenCertificateDirectory.Location = new System.Drawing.Point(454, 249);
            this.button_OpenCertificateDirectory.Name = "button_OpenCertificateDirectory";
            this.button_OpenCertificateDirectory.Size = new System.Drawing.Size(25, 25);
            this.button_OpenCertificateDirectory.TabIndex = 48;
            this.button_OpenCertificateDirectory.Text = "📂";
            this.button_OpenCertificateDirectory.UseVisualStyleBackColor = true;
            this.button_OpenCertificateDirectory.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_OpenCertificateDirectory_MouseClick);
            // 
            // label_DisabledPlatformsTitle
            // 
            this.label_DisabledPlatformsTitle.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_DisabledPlatformsTitle.Location = new System.Drawing.Point(210, 313);
            this.label_DisabledPlatformsTitle.Name = "label_DisabledPlatformsTitle";
            this.label_DisabledPlatformsTitle.Size = new System.Drawing.Size(180, 15);
            this.label_DisabledPlatformsTitle.TabIndex = 49;
            this.label_DisabledPlatformsTitle.Text = "Disabled Platforms";
            this.label_DisabledPlatformsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox_Telegram
            // 
            this.pictureBox_Telegram.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_Telegram.Image = global::Cursed_Market.Properties.Resources.IMG_SOCIAL_TELEGRAM_BLACK;
            this.pictureBox_Telegram.Location = new System.Drawing.Point(284, 365);
            this.pictureBox_Telegram.Name = "pictureBox_Telegram";
            this.pictureBox_Telegram.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Telegram.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Telegram.TabIndex = 50;
            this.pictureBox_Telegram.TabStop = false;
            this.pictureBox_Telegram.Visible = false;
            this.pictureBox_Telegram.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Telegram_MouseClick);
            // 
            // pictureBox_Boosty
            // 
            this.pictureBox_Boosty.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_Boosty.Image = global::Cursed_Market.Properties.Resources.IMG_SOCIAL_BOOSTY_BLACK;
            this.pictureBox_Boosty.Location = new System.Drawing.Point(322, 365);
            this.pictureBox_Boosty.Name = "pictureBox_Boosty";
            this.pictureBox_Boosty.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Boosty.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Boosty.TabIndex = 51;
            this.pictureBox_Boosty.TabStop = false;
            this.pictureBox_Boosty.Visible = false;
            this.pictureBox_Boosty.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Boosty_MouseClick);
            // 
            // Form_Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.pictureBox_Boosty);
            this.Controls.Add(this.pictureBox_Telegram);
            this.Controls.Add(this.label_DisabledPlatformsTitle);
            this.Controls.Add(this.button_OpenCertificateDirectory);
            this.Controls.Add(this.button_CreateCertificatePassword);
            this.Controls.Add(this.pictureBox_Discord);
            this.Controls.Add(this.button_QueueNotifySound);
            this.Controls.Add(this.label_MatchFoundSoundTitle);
            this.Controls.Add(this.comboBox_MatchFoundSound);
            this.Controls.Add(this.button_RemoveDisabledPlatform);
            this.Controls.Add(this.comboBox_DisabledPlatforms);
            this.Controls.Add(this.button_UpdateDebug);
            this.Controls.Add(this.label_DebugInfoTitle);
            this.Controls.Add(this.label_PlatformTitle);
            this.Controls.Add(this.label_CultureTitle);
            this.Controls.Add(this.textBox_Platform);
            this.Controls.Add(this.button_DisableProxy);
            this.Controls.Add(this.button_ResetApplicationSettings);
            this.Controls.Add(this.label_ControlPanelTitle);
            this.Controls.Add(this.button_ClearCertificates);
            this.Controls.Add(this.label_ApplicationThemeTitle);
            this.Controls.Add(this.comboBox_ApplicationTheme);
            this.Controls.Add(this.textBox_Culture);
            this.Controls.Add(this.panel_WindowHeader);
            this.Controls.Add(this.pictureBox_ControlPanelSubFrame);
            this.Controls.Add(this.pictureBox_DebugInfoFrame);
            this.Controls.Add(this.pictureBox_ControlPanelFrame);
            this.Controls.Add(this.pictureBox_Background);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Form_Settings_Load);
            this.Shown += new System.EventHandler(this.Form_Settings_Shown);
            this.panel_WindowHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Background)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ControlPanelSubFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DebugInfoFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ControlPanelFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Discord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Telegram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Boosty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_WindowHeader;
        private System.Windows.Forms.Button button_WindowClose;
        private System.Windows.Forms.TextBox textBox_Culture;
        private System.Windows.Forms.ComboBox comboBox_ApplicationTheme;
        private System.Windows.Forms.Label label_ApplicationThemeTitle;
        private System.Windows.Forms.PictureBox pictureBox_Background;
        private System.Windows.Forms.Button button_ClearCertificates;
        private System.Windows.Forms.PictureBox pictureBox_ControlPanelSubFrame;
        private System.Windows.Forms.Label label_ControlPanelTitle;
        private System.Windows.Forms.Button button_ResetApplicationSettings;
        private System.Windows.Forms.Button button_DisableProxy;
        private System.Windows.Forms.TextBox textBox_Platform;
        private System.Windows.Forms.Label label_CultureTitle;
        private System.Windows.Forms.Label label_PlatformTitle;
        private System.Windows.Forms.Label label_DebugInfoTitle;
        private System.Windows.Forms.Button button_UpdateDebug;
        private System.Windows.Forms.ComboBox comboBox_DisabledPlatforms;
        private System.Windows.Forms.Button button_RemoveDisabledPlatform;
        private System.Windows.Forms.PictureBox pictureBox_DebugInfoFrame;
        private System.Windows.Forms.Label label_MatchFoundSoundTitle;
        private System.Windows.Forms.ComboBox comboBox_MatchFoundSound;
        private System.Windows.Forms.PictureBox pictureBox_ControlPanelFrame;
        private System.Windows.Forms.Button button_QueueNotifySound;
        private System.Windows.Forms.PictureBox pictureBox_Discord;
        private System.Windows.Forms.Button button_CreateCertificatePassword;
        private System.Windows.Forms.Button button_OpenCertificateDirectory;
        private System.Windows.Forms.Label label_DisabledPlatformsTitle;
        private System.Windows.Forms.PictureBox pictureBox_Telegram;
        private System.Windows.Forms.PictureBox pictureBox_Boosty;
    }
}