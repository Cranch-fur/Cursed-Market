using CranchyLib.Networking;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
            if (string.IsNullOrEmpty(Globals_Session.Game.userId) == false)
            {
                textBox_MyCloudID.Width = 275; // What a generous approach <:3
                textBox_MyCloudID.Text = Globals_Session.Game.userId;

                button_MyCloudIDCopy.Visible = true;
            }
            else
            {
                textBox_MyCloudID.Width = 296; // What a generous approach <:3
                textBox_MyCloudID.Text = "NONE";

                button_MyCloudIDCopy.Visible = false;
            }
        }




        public void ReloadTheme()
        {
            switch (Globals.Application.Theme.selectedTheme)
            {
                default:
                    this.BackColor = Color.White;
                    panel_WindowHeader.BackColor = SystemColors.Control;
                    label_Title.ForeColor = Color.Black;
                    label_MyCloudIDTitle.ForeColor = Color.Black;
                    label_FriendCloudIDTitle.ForeColor = Color.Black;
                    button_MyCloudIDCopy.BackColor = Color.DimGray;
                    button_FriendCloudIDSend.BackColor = Color.DimGray;
                    break;

                case Globals.Application.Theme.E_Themes.legacy:
                    this.BackColor = Color.FromArgb(255, 46, 51, 73);
                    panel_WindowHeader.BackColor = Color.FromArgb(255, 24, 30, 54);
                    label_Title.ForeColor = Color.White;
                    label_MyCloudIDTitle.ForeColor = Color.White;
                    label_FriendCloudIDTitle.ForeColor = Color.White;
                    button_MyCloudIDCopy.BackColor = Color.RoyalBlue;
                    button_FriendCloudIDSend.BackColor = Color.RoyalBlue;
                    break;

                case Globals.Application.Theme.E_Themes.darkMemories:
                    this.BackColor = Color.FromArgb(255, 44, 47, 51);
                    panel_WindowHeader.BackColor = Color.FromArgb(255, 35, 39, 42);
                    label_Title.ForeColor = Color.White;
                    label_MyCloudIDTitle.ForeColor = Color.White;
                    label_FriendCloudIDTitle.ForeColor = Color.White;
                    button_MyCloudIDCopy.BackColor = Color.FromArgb(255, 85, 85, 85);
                    button_FriendCloudIDSend.BackColor = Color.FromArgb(255, 85, 85, 85);
                    break;

                case Globals.Application.Theme.E_Themes.saintsRow:
                    this.BackColor = Color.FromArgb(255, 37, 13, 57);
                    panel_WindowHeader.BackColor = Color.FromArgb(255, 55, 20, 86);
                    label_Title.ForeColor = Color.White;
                    label_MyCloudIDTitle.ForeColor = Color.White;
                    label_FriendCloudIDTitle.ForeColor = Color.White;
                    button_MyCloudIDCopy.BackColor = Color.FromArgb(255, 118, 93, 222);
                    button_FriendCloudIDSend.BackColor = Color.FromArgb(255, 118, 93, 222);
                    break;

                case Globals.Application.Theme.E_Themes.dracula:
                    this.BackColor = Color.FromArgb(255, 40, 42, 54);
                    panel_WindowHeader.BackColor = Color.FromArgb(255, 68, 71, 90);
                    label_Title.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    label_MyCloudIDTitle.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    label_FriendCloudIDTitle.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    button_MyCloudIDCopy.BackColor = Color.FromArgb(255, 118, 93, 222);
                    button_FriendCloudIDSend.BackColor = Color.FromArgb(255, 118, 93, 222);
                    break;

                case Globals.Application.Theme.E_Themes.christmas:
                    this.BackColor = Color.FromArgb(255, 24, 24, 24);
                    panel_WindowHeader.BackColor = Color.FromArgb(255, 14, 14, 14);
                    label_Title.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    label_MyCloudIDTitle.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    label_FriendCloudIDTitle.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    button_MyCloudIDCopy.BackColor = Color.FromArgb(255, 142, 22, 55);
                    button_FriendCloudIDSend.BackColor = Color.FromArgb(255, 142, 22, 55);
                    break;
            }
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




        private void button_MyCloudIDCopy_MouseClick(object sender, MouseEventArgs e)
        {
            Clipboard.SetText(textBox_MyCloudID.Text);
            Media.PlaySoundFromStream(Properties.Resources.SFX_Activate);
        }
        private void textBox_FriendCloudID_MouseClick(object sender, MouseEventArgs e)
        {
            textBox_FriendCloudID.Text = string.Empty;
        }
        private void button_FriendCloudIDSend_MouseClick(object sender, MouseEventArgs e)
        {
            string userInput = textBox_FriendCloudID.Text;
            if (string.IsNullOrEmpty(userInput) == true)
            {
                textBox_FriendCloudID.Text = "Cloud-ID can not be empty or null!";
                Media.PlaySoundFromStream(Properties.Resources.SFX_Error);
                return;
            }




            List<string> headers = new List<string>()
            {
                $"Cookie: bhvrSession={Globals_Session.Game.bhvrSession}",
                $"User-Agent: {Globals_Session.Game.user_agent}",
                $"x-kraken-client-platform: {Globals_Session.Game.client_platform}",
                $"x-kraken-client-provider: {Globals_Session.Game.client_provider}",
                $"x-kraken-client-os: {Globals_Session.Game.client_os}",
                $"x-kraken-client-version: {Globals_Session.Game.client_version}",
                "Content-Type: application/json"
            };

            List<string> cloudIDsToAdd = new List<string> { userInput };
            JObject requestBodyJson = JObject.FromObject(new
            {
                ids = new JArray(cloudIDsToAdd),
                platform = "kraken"
            });

            var friendRequestResponse = Networking.Post($"https://{Globals_Session.Game.Platform.GetCurrentPlatformHostNames()[0]}/api/v1/players/friends/add", headers, requestBodyJson.ToString());
            if (friendRequestResponse.statusCode == Networking.E_StatusCode.CREATED)
            {
                Media.PlaySoundFromStream(Properties.Resources.SFX_Activate);
            }
            else if (friendRequestResponse.statusCode == Networking.E_StatusCode.BAD_REQUEST)
            {
                textBox_FriendCloudID.Text = $"Invalid Cloud-ID OR friend request already sent!";
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
