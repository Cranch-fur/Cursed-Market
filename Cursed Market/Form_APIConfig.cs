using System;
using System.Windows.Forms;






namespace Cursed_Market
{
    public partial class Form_APIConfig : Form
    {
        private bool _changesWereMade = false;

        public Form_APIConfig()
        {
            InitializeComponent();
            InitializeSettings();
        }

        private void Form_APIConfig_Shown(object sender, EventArgs e)
        {
            LoadSettingsIntoControls();
            _changesWereMade = false;
        }

        private void Form_APIConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_changesWereMade && e.CloseReason == CloseReason.UserClosing)
            {
                if (Messaging.ShowDialog("Changes were made to the API configuration, but they were not saved! Close the window without saving?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
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
                label_FormTitle, label_Host, label_Status, label_Update,
                label_MarketFull, label_MarketDLC, label_CharacterData,
                label_BloodwebData, label_CharactersList, label_ItemsList,
                label_Catalog, label_AntiKillSwitch, label_RobotoFont, label_CharactersPreset);

            button_SaveChanges.BackColor = theme.PrimaryButtonBack;
            button_SaveChanges.ForeColor = theme.PrimaryButtonText;
            button_SaveChanges.FlatAppearance.BorderColor = theme.PrimaryButtonBorder;
        }

        private void InitializeSettings()
        {
            ReloadTheme();
        }

        private void LoadSettingsIntoControls()
        {
            textBox_Host.Text = WinReg.GetData_SZ(WinReg.API.host, CursedAPI.SE_CommonEndpoints.Defaults.Host);
            textBox_Status.Text = WinReg.GetData_SZ(WinReg.API.pathStatus, CursedAPI.SE_CommonEndpoints.Defaults.Status);
            textBox_Update.Text = WinReg.GetData_SZ(WinReg.API.pathUpdate, CursedAPI.SE_CommonEndpoints.Defaults.Update);
            textBox_MarketFull.Text = WinReg.GetData_SZ(WinReg.API.pathMarketFull, CursedAPI.SE_CommonEndpoints.Defaults.MarketFull);
            textBox_MarketDLC.Text = WinReg.GetData_SZ(WinReg.API.pathMarketDLC, CursedAPI.SE_CommonEndpoints.Defaults.MarketDLC);
            textBox_CharacterData.Text = WinReg.GetData_SZ(WinReg.API.pathCharacterData, CursedAPI.SE_CommonEndpoints.Defaults.CharacterData);
            textBox_BloodwebData.Text = WinReg.GetData_SZ(WinReg.API.pathBloodwebData, CursedAPI.SE_CommonEndpoints.Defaults.BloodwebData);
            textBox_CharactersList.Text = WinReg.GetData_SZ(WinReg.API.pathCharactersList, CursedAPI.SE_CommonEndpoints.Defaults.CharactersList);
            textBox_ItemsList.Text = WinReg.GetData_SZ(WinReg.API.pathItemsList, CursedAPI.SE_CommonEndpoints.Defaults.ItemsList);
            textBox_Catalog.Text = WinReg.GetData_SZ(WinReg.API.pathCatalog, CursedAPI.SE_CommonEndpoints.Defaults.Catalog);
            textBox_AntiKillSwitch.Text = WinReg.GetData_SZ(WinReg.API.pathAntiKillSwitch, CursedAPI.SE_CommonEndpoints.Defaults.AntiKillSwitch);
            textBox_RobotoFont.Text = WinReg.GetData_SZ(WinReg.API.pathFont, CursedAPI.SE_CommonEndpoints.Defaults.Font);
            textBox_CharactersPreset.Text = WinReg.GetData_SZ(WinReg.API.pathCharactersPreset, CursedAPI.SE_CommonEndpoints.Defaults.CharactersPreset);
        }

        private void OnParameterChanged(object sender, EventArgs e)
        {
            _changesWereMade = true;
        }

        private void button_ResetDefault_MouseClick(object sender, MouseEventArgs e)
        {
            textBox_Host.Text = CursedAPI.SE_CommonEndpoints.Defaults.Host;
            textBox_Status.Text = CursedAPI.SE_CommonEndpoints.Defaults.Status;
            textBox_Update.Text = CursedAPI.SE_CommonEndpoints.Defaults.Update;
            textBox_MarketFull.Text = CursedAPI.SE_CommonEndpoints.Defaults.MarketFull;
            textBox_MarketDLC.Text = CursedAPI.SE_CommonEndpoints.Defaults.MarketDLC;
            textBox_CharacterData.Text = CursedAPI.SE_CommonEndpoints.Defaults.CharacterData;
            textBox_BloodwebData.Text = CursedAPI.SE_CommonEndpoints.Defaults.BloodwebData;
            textBox_CharactersList.Text = CursedAPI.SE_CommonEndpoints.Defaults.CharactersList;
            textBox_ItemsList.Text = CursedAPI.SE_CommonEndpoints.Defaults.ItemsList;
            textBox_Catalog.Text = CursedAPI.SE_CommonEndpoints.Defaults.Catalog;
            textBox_AntiKillSwitch.Text = CursedAPI.SE_CommonEndpoints.Defaults.AntiKillSwitch;
            textBox_RobotoFont.Text = CursedAPI.SE_CommonEndpoints.Defaults.Font;
            textBox_CharactersPreset.Text = CursedAPI.SE_CommonEndpoints.Defaults.CharactersPreset;

            _changesWereMade = true;
            Media.PlaySoundFromStream(Properties.Resources.SFX_Activate);
        }

        private void button_SaveChanges_MouseClick(object sender, MouseEventArgs e)
        {
            WinReg.SetData_SZ(WinReg.API.host, textBox_Host.Text.Trim());
            WinReg.SetData_SZ(WinReg.API.pathStatus, textBox_Status.Text.Trim());
            WinReg.SetData_SZ(WinReg.API.pathUpdate, textBox_Update.Text.Trim());
            WinReg.SetData_SZ(WinReg.API.pathMarketFull, textBox_MarketFull.Text.Trim());
            WinReg.SetData_SZ(WinReg.API.pathMarketDLC, textBox_MarketDLC.Text.Trim());
            WinReg.SetData_SZ(WinReg.API.pathCharacterData, textBox_CharacterData.Text.Trim());
            WinReg.SetData_SZ(WinReg.API.pathBloodwebData, textBox_BloodwebData.Text.Trim());
            WinReg.SetData_SZ(WinReg.API.pathCharactersList, textBox_CharactersList.Text.Trim());
            WinReg.SetData_SZ(WinReg.API.pathItemsList, textBox_ItemsList.Text.Trim());
            WinReg.SetData_SZ(WinReg.API.pathCatalog, textBox_Catalog.Text.Trim());
            WinReg.SetData_SZ(WinReg.API.pathAntiKillSwitch, textBox_AntiKillSwitch.Text.Trim());
            WinReg.SetData_SZ(WinReg.API.pathFont, textBox_RobotoFont.Text.Trim());
            WinReg.SetData_SZ(WinReg.API.pathCharactersPreset, textBox_CharactersPreset.Text.Trim());

            _changesWereMade = false;
            Media.PlaySoundFromStream(Properties.Resources.SFX_Activate);

            Messaging.ShowMessage("API configuration saved successfully!\nRestart Cursed Market to apply changes.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}