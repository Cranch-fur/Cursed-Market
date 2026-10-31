using Cursed_Market.UI;






namespace Cursed_Market
{
    public partial class Form_Wait : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                /* Retrieve the base window creation parameters. */
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
            if (e.CloseReason == CloseReason.UserClosing)
                e.Cancel = true;
        }




        public void ReloadTheme()
        {
            var theme = ProgramThemes.GetCurrentPalette();
            this.BackColor = theme.OverlayBack;
            label_Wait.Foreground = theme.OverlayText.ToBrush();
        }




        private void InitializeSettings()
        {
            ReloadTheme();
        }
    }
}
