using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;






namespace Cursed_Market
{
    public partial class Form_Queue : Form
    {
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x20 | 0x80 | 0x80000 | 0x08000000;
                return createParams;
            }
        }




        public Form_Queue()
        {
            InitializeComponent();
            InitializeSettings();


            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;
            label_QueueStatus.Location = new Point(this.Width / 128, this.Height / 128);
        }


        private void Form_Queue_Load(object sender, EventArgs e)
        {
            this.TransparencyKey = Color.DarkSlateGray;
        }




        public void ReloadTheme()
        {
            var theme = ProgramThemes.GetCurrentPalette();
            label_QueueStatus.BackColor = theme.OverlayBack;
            label_QueueStatus.ForeColor = theme.OverlayText;
        }




        private void InitializeSettings()
        {
            ReloadTheme();




            #region localization
            label_QueueStatus.Text = Properties.Localization.QUEUE_Idle;
            #endregion
        }

        private void Form_Queue_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                e.Cancel = true;
        }

        public void LocalUpdateQueue(Queue.E_QueueStatus newQueueStatus, int newQueuePosition)
        {
            switch (newQueueStatus)
            {
                case Queue.E_QueueStatus.MatchStarting:
                    label_QueueStatus.Text = Properties.Localization.QUEUE_MatchStarting;
                    Timer.DelayedLabelUpdate(label_QueueStatus, string.Empty, 10);
                    break;

                case Queue.E_QueueStatus.LobbyIdle: // Dublicate of LobbyFound scenario.
                case Queue.E_QueueStatus.LobbyFound:
                    label_QueueStatus.Text = Properties.Localization.QUEUE_LobbyFound;
                    Timer.DelayedLabelUpdate(label_QueueStatus, string.Empty, 5);
                    break;

                case Queue.E_QueueStatus.Searching:
                    label_QueueStatus.Text = $"{newQueuePosition}";
                    break;

                case Queue.E_QueueStatus.None:
                    label_QueueStatus.Text = string.Empty;
                    break;
            }
        }
    }
}
