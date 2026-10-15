using CranchyLib.Networking;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Windows.Forms;






namespace Cursed_Market
{
    public partial class Form_CloudIDFriend : Form
    {
        public Form_CloudIDFriend()
        {
            InitializeComponent();
            InitializeSettings();
        }
        private void Form_AddFriend_Shown(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ProgramSession.Game.userId) == false)
            {
                textBox_MyCloudID.Width = 275; // What a generous approach <:3.
                textBox_MyCloudID.Text = ProgramSession.Game.userId;

                button_MyCloudIDCopy.Visible = true;
            }
            else
            {
                textBox_MyCloudID.Width = 296; // What a generous approach <:3.
                textBox_MyCloudID.Text = "NONE";

                button_MyCloudIDCopy.Visible = false;
            }
        }




        public void ReloadTheme()
        {
            var theme = ProgramThemes.GetCurrentPalette();

            this.BackColor = theme.FormBack;
            panel_WindowHeader.BackColor = theme.HeaderBack;
            pictureBox_WindowBorder.Visible = theme.IsLightTheme;

            ProgramThemes.ApplyForeColor(theme.PrimaryText,
                label_Title, label_MyCloudIDTitle, label_FriendCloudIDTitle);

            button_MyCloudIDCopy.BackColor = theme.SecondaryButtonBack;
            button_FriendCloudIDSend.BackColor = theme.SecondaryButtonBack;
        }




        private void InitializeSettings()
        {
            ReloadTheme();




            #region localization
            label_Title.Text = Properties.Localization.FRIEND_Title;
            label_MyCloudIDTitle.Text = Properties.Localization.FRIEND_MyCloudIDTitle;
            label_FriendCloudIDTitle.Text = Properties.Localization.FRIEND_FriendCloudIDTitle;
            #endregion
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




        private void button_MyCloudIDCopy_MouseClick(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox_MyCloudID.Text))
            {
                Clipboard.SetText(textBox_MyCloudID.Text);
                Media.PlaySoundFromStream(Properties.Resources.SFX_Activate);
            }
        }
        private void textBox_FriendCloudID_MouseClick(object sender, MouseEventArgs e)
        {
            textBox_FriendCloudID.Text = string.Empty;
        }
        private void button_FriendCloudIDSend_MouseClick(object sender, MouseEventArgs e)
        {
            string userInput = textBox_FriendCloudID.Text?.Trim();
            if (string.IsNullOrEmpty(userInput) || userInput.Contains(" "))
            {
                textBox_FriendCloudID.Text = "Cloud-ID can not be empty or null!";
                Media.PlaySoundFromStream(Properties.Resources.SFX_Error);
                return;
            }

            if (string.IsNullOrEmpty(ProgramSession.Game.api_key))
            {
                textBox_FriendCloudID.Text = "Game session is not initialized yet!";
                Media.PlaySoundFromStream(Properties.Resources.SFX_Error);
                return;
            }

            List<string> headers = new List<string>()
            {
                $"api-key: {ProgramSession.Game.api_key}",
                $"User-Agent: {ProgramSession.Game.user_agent}",
                $"x-kraken-client-platform: {ProgramSession.Game.client_platform}",
                $"x-kraken-client-provider: {ProgramSession.Game.client_provider}",
                $"x-kraken-client-os: {ProgramSession.Game.client_os}",
                $"x-kraken-client-version: {ProgramSession.Game.client_version}",
                "Content-Type: application/json"
            };

            List<string> cloudIDsToAdd = new List<string> { userInput };
            JObject requestBodyJson = JObject.FromObject(new
            {
                ids = new JArray(cloudIDsToAdd),
                platform = "kraken"
            });

            List<string> hostNames = ProgramSession.Game.Platform.GetCurrentPlatformHostNames();
            if (hostNames == null || hostNames.Count == 0)
            {
                textBox_FriendCloudID.Text = "Failed to resolve platform host!";
                Media.PlaySoundFromStream(Properties.Resources.SFX_Error);
                return;
            }

            var friendRequestResponse = Networking.Post($"https://{hostNames[0]}/api/v1/players/friends/add", headers, requestBodyJson.ToString());
            if (friendRequestResponse.statusCode == Networking.E_StatusCode.CREATED)
            {
                Media.PlaySoundFromStream(Properties.Resources.SFX_Activate);
            }
            else if (friendRequestResponse.statusCode == Networking.E_StatusCode.BAD_REQUEST)
            {
                textBox_FriendCloudID.Text = "Invalid Cloud-ID OR friend request already sent!";
                Media.PlaySoundFromStream(Properties.Resources.SFX_Error);
            }
            else
            {
                textBox_FriendCloudID.Text = $"Failed to send a friend request! [{friendRequestResponse.statusCode}]";
                Media.PlaySoundFromStream(Properties.Resources.SFX_Error);
            }
        }
    }
}