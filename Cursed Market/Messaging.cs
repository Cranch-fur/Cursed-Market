using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cursed_Market
{
    public static class Messaging
    {
        public static void ShowMessage(string message,  
            MessageBoxButtons messageButtons = MessageBoxButtons.OK, 
                MessageBoxIcon messageType = MessageBoxIcon.None, 
                    MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            MessageBox.Show(message, Globals.Application.executableFriendlyName, messageButtons, messageType, defaultButton);
        }

        public static DialogResult ShowDialog(string message,
            MessageBoxButtons messageButtons = MessageBoxButtons.OK,
                MessageBoxIcon messageType = MessageBoxIcon.None,
                    MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            return MessageBox.Show(message, Globals.Application.executableFriendlyName, messageButtons, messageType, defaultButton);
        }

        public static async void ShowNotify(string title,
            string message,
                System.Drawing.Icon notifyIcon,
                    ToolTipIcon tooltipIcon = ToolTipIcon.None,
                        int showUpTime = 3000)
        {
            await Task.Run(() =>
            {
                NotifyIcon winNotify = new NotifyIcon();
                winNotify.Visible = true;
                winNotify.Icon = notifyIcon;

                winNotify.BalloonTipTitle = title;
                winNotify.BalloonTipText = message;
                winNotify.BalloonTipIcon = tooltipIcon;

                winNotify.ShowBalloonTip(showUpTime);
                winNotify.BalloonTipClosed += (sender, e) => winNotify.Dispose(); Thread.Sleep(showUpTime); winNotify.Visible = false;
            });
        }
    }
}
