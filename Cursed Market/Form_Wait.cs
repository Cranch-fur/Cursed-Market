using System.Drawing;
using System.Windows.Forms;






namespace Cursed_Market
{
    public partial class Form_Wait : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                // Retrieve the base window creation parameters.
                CreateParams createParams = base.CreateParams;

                // The ExStyle property sets the extended window styles.
                // Here we enable several flags using the bitwise OR operator:
                //
                // 0x80  - WS_EX_TOOLWINDOW:
                //         Designates the window as a tool window, which prevents it from appearing on the taskbar.
                //
                // 0x08000000  - WS_EX_NOACTIVATE:
                //         Prevents the window from being activated when clicked, i.e., it does not gain focus.
                createParams.ExStyle |= 0x80 | 0x08000000;


                return createParams;
            }
        }




        public Form_Wait()
        {
            InitializeComponent();
            InitializeSettings();
        }




        private void Form_Startup_Load(object sender, System.EventArgs e)
        {
            ProgramCache.Forms.Wait = this;
            label_Wait.Text = Properties.Localization.PLEASE_WAIT;
        }
        private void Form_Wait_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }




        public void ReloadTheme()
        {
            switch (ProgramThemes.GetSelectedTheme())
            {
                default:
                    label_Wait.ForeColor = Color.Black;
                    this.BackColor = Color.WhiteSmoke;
                    break;

                case ProgramThemes.E_Themes.legacy:
                    label_Wait.ForeColor = Color.White;
                    this.BackColor = Color.FromArgb(255, 46, 51, 73);
                    break;

                case ProgramThemes.E_Themes.darkMemories:
                    label_Wait.ForeColor = Color.White;
                    this.BackColor = Color.FromArgb(255, 44, 47, 51);
                    break;

                case ProgramThemes.E_Themes.saintsRow:
                    label_Wait.ForeColor = Color.FromArgb(255, 146, 71, 214);
                    this.BackColor = Color.FromArgb(255, 37, 13, 57);
                    break;

                case ProgramThemes.E_Themes.dracula:
                    label_Wait.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    this.BackColor = Color.FromArgb(255, 40, 42, 54);
                    break;

                case ProgramThemes.E_Themes.christmas:
                    label_Wait.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    this.BackColor = Color.FromArgb(255, 24, 24, 24);
                    break;
            }
        }




        private void InitializeSettings()
        {
            ReloadTheme();
        }
    }
}
