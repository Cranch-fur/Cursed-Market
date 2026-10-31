using Avalonia.Input;
using Avalonia.Interactivity;
using CranchyLib.Networking;
using Cursed_Market.UI;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;






namespace Cursed_Market
{
    public partial class Form_CloudIDFriend : Form
    {
        public Form_CloudIDFriend()
        {
            InitializeComponent();
            InitializeSettings();

            /* MouseClick equivalent: TextBox handles pointer events itself, so handled ones have to be listened to as well. */
            textBox_FriendCloudID.AddHandler(PointerReleasedEvent, textBox_FriendCloudID_MouseClick, RoutingStrategies.Bubble, true);
        }
        private void Form_AddFriend_Shown(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ProgramSession.Game.userId) == false)
            {
                textBox_MyCloudID.Width = 275; // What a generous approach <:3.
                textBox_MyCloudID.Text = ProgramSession.Game.userId;

                button_MyCloudIDCopy.IsVisible = true;
            }
            else
            {
                textBox_MyCloudID.Width = 296; // What a generous approach <:3.
                textBox_MyCloudID.Text = "NONE";

                button_MyCloudIDCopy.IsVisible = false;
            }
        }




        public void ReloadTheme()
        {
            var theme = ProgramThemes.GetCurrentPalette();

            this.BackColor = theme.FormBack;
            panel_WindowHeader.Background = ToBackdropBrush(theme.HeaderBack);
            pictureBox_WindowBorder.IsVisible = theme.IsLightTheme;

            ProgramThemes.ApplyForeColor(theme.PrimaryText,
                label_Title, label_MyCloudIDTitle, label_FriendCloudIDTitle);

            button_MyCloudIDCopy.Background = theme.SecondaryButtonBack.ToBrush();
            button_FriendCloudIDSend.Background = theme.SecondaryButtonBack.ToBrush();
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




        private void button_WindowClose_MouseClick(object sender, RoutedEventArgs e) => this.Close();
        private void panel_WindowHeader_MouseDown(object sender, PointerPressedEventArgs e)
        {
            if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                BeginMoveDrag(e); // Same as sending WM_NCLBUTTONDOWN (HTCAPTION), system takes over window dragging.
            }
        }




        private void button_MyCloudIDCopy_MouseClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox_MyCloudID.Text))
            {
                _ = this.Clipboard?.SetTextAsync(textBox_MyCloudID.Text);
                Media.PlaySoundFromStream(Properties.Resources.SFX_Activate);
            }
        }
        private void textBox_FriendCloudID_MouseClick(object sender, PointerReleasedEventArgs e)
        {
            textBox_FriendCloudID.Text = string.Empty;
        }
        private void button_FriendCloudIDSend_MouseClick(object sender, RoutedEventArgs e)
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