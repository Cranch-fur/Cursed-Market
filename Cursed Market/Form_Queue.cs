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
            // https://stackoverflow.com/questions/3900947/hiding-winforms-app-from-taskbar
            // No idea what exactly it does, but it works :)
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x80;
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
            CheckForIllegalCrossThreadCalls = false;

            this.TransparencyKey = Color.DarkSlateGray; // !!! TRANSPARENCY KEY MUST BE SET THEFORE WE MANIPULATE SetWindowLong() !!! Other way we will loose control over our whole PC !!!

            int initialStyle = GetWindowLong(this.Handle, -20);
            SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);
        }




        public void ReloadTheme()
        {
            switch (Globals.Application.Theme.selectedTheme)
            {
                default:
                    label_QueueStatus.ForeColor = Color.Black;
                    label_QueueStatus.BackColor = Color.WhiteSmoke;
                    break;

                case Globals.Application.Theme.E_Themes.legacy:
                    label_QueueStatus.ForeColor = Color.White;
                    label_QueueStatus.BackColor = Color.FromArgb(255, 46, 51, 73);
                    break;

                case Globals.Application.Theme.E_Themes.darkMemories:
                    label_QueueStatus.ForeColor = Color.White;
                    label_QueueStatus.BackColor = Color.FromArgb(255, 44, 47, 51);
                    break;

                case Globals.Application.Theme.E_Themes.saintsRow:
                    label_QueueStatus.ForeColor = Color.FromArgb(255, 146, 71, 214);
                    label_QueueStatus.BackColor = Color.FromArgb(255, 37, 13, 57);
                    break;

                case Globals.Application.Theme.E_Themes.dracula:
                    label_QueueStatus.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    label_QueueStatus.BackColor = Color.FromArgb(255, 40, 42, 54);
                    break;

                case Globals.Application.Theme.E_Themes.christmas:
                    label_QueueStatus.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    label_QueueStatus.BackColor = Color.FromArgb(255, 24, 24, 24);
                    break;
            }
        }




        private void InitializeSettings()
        {
            ReloadTheme();




            #region localization
            label_QueueStatus.Text = Properties.Localization.QUEUE_Idle;
            #endregion
        }

        private void Form_Queue_FormClosing(object sender, FormClosingEventArgs e) => e.Cancel = true;

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
