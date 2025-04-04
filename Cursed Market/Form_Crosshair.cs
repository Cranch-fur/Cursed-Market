using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Cursed_Market
{
    public partial class Form_Crosshair : Form
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




        public Form_Crosshair()
        {
            InitializeComponent();
            InitializeSettings();


            this.Width = Screen.PrimaryScreen.Bounds.Width;   // Get user screen size and apply it to Crosshair Form width.
            this.Height = Screen.PrimaryScreen.Bounds.Height; // Get user screen size and apply it to Crosshair Form height.
        }
        private void Form_Crosshair_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false; // We need to disable Safety Measures in order to call user32.dll.

            int initialStyle = GetWindowLong(this.Handle, -20);             // Get GWL_EXSTYLE of this Form.
            SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20); // 0x80000 - WS_EX_LAYERED (Allows window to be half or fully transparent), 0x20 - WS_EX_TRANSPARENT (Makes window invisible for mouse click, so user can not interact with it)
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);     // Enables transparent background support for our Form.
        }
        private void Form_Crosshair_Shown(object sender, EventArgs e) // Update crosshair settings each time it's initially displayed on screen.
        {
            ForceInitializeSettings();
        }
        private void Crosshair_FormClosing(object sender, FormClosingEventArgs e) // Prevent Crosshair Form from being closed by any means.
        {
            e.Cancel = true;
        }




        private void SetCrosshairFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    Image crosshairImage = Image.FromFile(filePath);
                    pictureBox_Crosshair.Image = crosshairImage;
                    return; // Code is complete, we force return to never reach ICON_MISSING scenario.
                }
                catch { }
            }

            pictureBox_Crosshair.Image = Properties.Resources.IMG_CROSSHAIR_MISSING; // Use missing icon if either file doesn't exist or there wasn't any success reading it as an image.
        }




        public void ForceInitializeSettings() => InitializeSettings(); // Public proxy function for initializing settings at any time given, from any class.
        private void InitializeSettings()
        {
            switch (Globals.Crosshair.selectedCrosshair)
            {
                default:
                    pictureBox_Crosshair.Image = Properties.Resources.IMG_MISSING; // Default crosshair to use if nothing is set.
                    break;

                case Globals.Crosshair.E_Crosshairs.none:
                    break;

                case Globals.Crosshair.E_Crosshairs.cs_nafany:
                    pictureBox_Crosshair.Image = Properties.Resources.IMG_CROSSHAIR_CS_nafany;
                    break;

                case Globals.Crosshair.E_Crosshairs.cs_donk:
                    pictureBox_Crosshair.Image = Properties.Resources.IMG_CROSSHAIR_CS_donk;
                    break;

                case Globals.Crosshair.E_Crosshairs.cs_felps:
                    pictureBox_Crosshair.Image = Properties.Resources.IMG_CROSSHAIR_CS_Felps;
                    break;

                case Globals.Crosshair.E_Crosshairs.circleAqua:
                    pictureBox_Crosshair.Image = Properties.Resources.IMG_CROSSHAIR_DEFAULT_CIRCLE_AQUA;
                    break;

                case Globals.Crosshair.E_Crosshairs.circleWhite:
                    pictureBox_Crosshair.Image = Properties.Resources.IMG_CROSSHAIR_DEFAULT_CIRCLE_WHITE;
                    break;

                case Globals.Crosshair.E_Crosshairs.dotAqua:
                    pictureBox_Crosshair.Image = Properties.Resources.IMG_CROSSHAIR_DEFAULT_DOT_AQUA;
                    break;

                case Globals.Crosshair.E_Crosshairs.dotGreen:
                    pictureBox_Crosshair.Image = Properties.Resources.IMG_CROSSHAIR_DEFAULT_DOT_GREEN;
                    break;

                case Globals.Crosshair.E_Crosshairs.dotRed:
                    pictureBox_Crosshair.Image = Properties.Resources.IMG_CROSSHAIR_DEFAULT_DOT_RED;
                    break;

                case Globals.Crosshair.E_Crosshairs.dotYellow:
                    pictureBox_Crosshair.Image = Properties.Resources.IMG_CROSSHAIR_DEFAULT_DOT_YELLOW;
                    break;

                case Globals.Crosshair.E_Crosshairs.tacticAqua:
                    pictureBox_Crosshair.Image = Properties.Resources.IMG_CROSSHAIR_TACTIC_AQUA;
                    break;

                case Globals.Crosshair.E_Crosshairs.tacticWhite:
                    pictureBox_Crosshair.Image = Properties.Resources.IMG_CROSSHAIR_TACTIC_WHITE;
                    break;

                case Globals.Crosshair.E_Crosshairs.custom01:
                    SetCrosshairFromFile(Globals.Crosshair.customCrosshair01FilePath);
                    break;

                case Globals.Crosshair.E_Crosshairs.custom02:
                    SetCrosshairFromFile(Globals.Crosshair.customCrosshair02FilePath);
                    break;

                case Globals.Crosshair.E_Crosshairs.custom03:
                    SetCrosshairFromFile(Globals.Crosshair.customCrosshair03FilePath);
                    break;

                case Globals.Crosshair.E_Crosshairs.custom04:
                    SetCrosshairFromFile(Globals.Crosshair.customCrosshair04FilePath);
                    break;

                case Globals.Crosshair.E_Crosshairs.custom05:
                    SetCrosshairFromFile(Globals.Crosshair.customCrosshair05FilePath);
                    break;

                case Globals.Crosshair.E_Crosshairs.custom06:
                    SetCrosshairFromFile(Globals.Crosshair.customCrosshair06FilePath);
                    break;

                case Globals.Crosshair.E_Crosshairs.custom07:
                    SetCrosshairFromFile(Globals.Crosshair.customCrosshair07FilePath);
                    break;

                case Globals.Crosshair.E_Crosshairs.custom08:
                    SetCrosshairFromFile(Globals.Crosshair.customCrosshair08FilePath);
                    break;

                case Globals.Crosshair.E_Crosshairs.custom09:
                    SetCrosshairFromFile(Globals.Crosshair.customCrosshair09FilePath);
                    break;

                case Globals.Crosshair.E_Crosshairs.custom10:
                    SetCrosshairFromFile(Globals.Crosshair.customCrosshair10FilePath);
                    break;

                case Globals.Crosshair.E_Crosshairs.custom11:
                    SetCrosshairFromFile(Globals.Crosshair.customCrosshair11FilePath);
                    break;

                case Globals.Crosshair.E_Crosshairs.custom12:
                    SetCrosshairFromFile(Globals.Crosshair.customCrosshair12FilePath);
                    break;

                case Globals.Crosshair.E_Crosshairs.custom13:
                    SetCrosshairFromFile(Globals.Crosshair.customCrosshair13FilePath);
                    break;

                case Globals.Crosshair.E_Crosshairs.custom14:
                    SetCrosshairFromFile(Globals.Crosshair.customCrosshair14FilePath);
                    break;

                case Globals.Crosshair.E_Crosshairs.custom15:
                    SetCrosshairFromFile(Globals.Crosshair.customCrosshair15FilePath);
                    break;

                case Globals.Crosshair.E_Crosshairs.custom16:
                    SetCrosshairFromFile(Globals.Crosshair.customCrosshair16FilePath);
                    break;
            }


            this.BackColor = this.pictureBox_Crosshair.BackColor; // Applying crosshair back color to the Form itself.
            this.TransparencyKey = this.pictureBox_Crosshair.BackColor; // Announcing Form back color as Form transparency key. This way we're getting rid of background color, making crosshair display as a proper PNG image.


            pictureBox_Crosshair.Location = new Point(this.Width / 2 - (pictureBox_Crosshair.Width / 2), this.Height / 2 - (pictureBox_Crosshair.Height / 2)); // Calculating X & Y coordinates of the user screen center and moving crosshair to them.
            this.Opacity = (float)Globals.Crosshair.opacity / 100; // Crosshair opacity is stored as percent number (0-100), bu WinForms opacity settings are designed for a float in range 0.0-1.0 | 100 / 100 = 1.0
        }
    }
}
