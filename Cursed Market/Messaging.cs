using Cursed_Market.UI;
using System.Threading.Tasks;






namespace Cursed_Market
{
    public static class Messaging
    {
        public static void ShowMessage(string message,
            MessageBoxButtons messageButtons = MessageBoxButtons.OK,
                MessageBoxIcon messageType = MessageBoxIcon.None,
                    MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            MessageBox.Show(message, ProgramPaths.executableFriendlyName, messageButtons, messageType, defaultButton);
        }

        public static DialogResult ShowDialog(string message,
            MessageBoxButtons messageButtons = MessageBoxButtons.OK,
                MessageBoxIcon messageType = MessageBoxIcon.None,
                    MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            return MessageBox.Show(message, ProgramPaths.executableFriendlyName, messageButtons, messageType, defaultButton);
        }

        /* Avalonia has no balloon notifications, shell NotifyIcon is still used for them (shown as native toast on Windows 10 / 11). */
        public static async void ShowNotify(string title,
            string message,
                System.Drawing.Icon notifyIcon,
                    ToolTipIcon tooltipIcon = ToolTipIcon.None,
                        int showUpTime = 3000)
        {
            using (System.Windows.Forms.NotifyIcon winNotify = new System.Windows.Forms.NotifyIcon())
            {
                winNotify.Visible = true;
                winNotify.Icon = notifyIcon;

                winNotify.BalloonTipTitle = title;
                winNotify.BalloonTipText = message;
                winNotify.BalloonTipIcon = (System.Windows.Forms.ToolTipIcon)tooltipIcon;

                winNotify.ShowBalloonTip(showUpTime);
                await Task.Delay(showUpTime);
                winNotify.Visible = false;
            }
        }
    }
}