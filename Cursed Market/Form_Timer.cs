using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Cursed_Market
{
    public partial class Form_Timer : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);


        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);


        // Delegate for the hook callback function
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);




        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x20 | 0x80 | 0x80000 | 0x08000000; // 0x80 = WS_EX_TOOLWINDOW | 0x08000000 = WS_EX_NOACTIVATE 
                return createParams;
            }
        }




        // Global hook variables
        private IntPtr _hookID = IntPtr.Zero;
        private LowLevelMouseProc _proc;

        // Constants for hook and mouse event
        private const int WH_MOUSE_LL = 14;
        private const int WM_MBUTTONDOWN = 0x207;

        // Background workers for mouse hook and timer
        private BackgroundWorker bgwHook;
        private BackgroundWorker bgwTimer;

        // Timer duration in seconds
        private const int TimerDuration = 60;




        // Set the global mouse hook
        private IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }
        // Global hook callback function
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // If nCode is non-negative and the event is middle mouse button down
            if (nCode >= 0 && wParam == (IntPtr)WM_MBUTTONDOWN)
            {
                // Invoke ToggleTimer on the UI thread
                this.BeginInvoke((Action)(() =>
                {
                    ToggleTimer();
                }));
            }
            // Pass the hook information to the next hook in the chain
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }




        // Background worker method for global mouse hook
        private void bgwHook_DoWork(object sender, DoWorkEventArgs e)
        {
            // Set the global hook
            _hookID = SetHook(_proc);
            // Start a message loop to keep the hook active
            Application.Run();
        }

        // Background worker method for the timer
        private void bgwTimer_DoWork(object sender, DoWorkEventArgs e)
        {
            int remaining = TimerDuration;
            while (remaining >= 0)
            {
                // Check if cancellation is requested
                if (bgwTimer.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }


                if (remaining == 3)
                {
                    Media.PlaySoundFromStream(Properties.Resources.SFX_RaceCountdown);
                }


                // Report progress with the remaining time
                bgwTimer.ReportProgress(0, remaining);
                Thread.Sleep(1000);
                remaining--;
            }
        }




        // Update the label with the remaining time (executed on the UI thread)
        private void bgwTimer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int remaining = (int)e.UserState;
            label_Timer.Text = $"{remaining:D2}";


            this.Opacity = 100;
        }

        // Hide the label when the timer completes or is cancelled
        private void bgwTimer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Opacity = 0;
        }




        // Toggle timer: start it if not running, or stop it if already running
        public void ToggleTimer()
        {
            if (bgwTimer.IsBusy)
            {
                bgwTimer.CancelAsync();
            }
            else
            {
                bgwTimer.RunWorkerAsync();
            }
        }
        public void StopTimer()
        {
            if (bgwTimer.IsBusy)
            {
                bgwTimer.CancelAsync();
            }
        }
        public void StartTimer()
        {
            if (bgwTimer.IsBusy == false)
            {
                bgwTimer.RunWorkerAsync();
            }
        }



        private void InitializeBackgroundWorkers()
        {
            // Initialize background worker for global mouse hook
            bgwHook = new BackgroundWorker();
            bgwHook.WorkerSupportsCancellation = true;
            bgwHook.DoWork += bgwHook_DoWork;


            // Initialize background worker for timer
            bgwTimer = new BackgroundWorker();
            bgwTimer.WorkerReportsProgress = true;
            bgwTimer.WorkerSupportsCancellation = true;
            bgwTimer.DoWork += bgwTimer_DoWork;
            bgwTimer.ProgressChanged += bgwTimer_ProgressChanged;
            bgwTimer.RunWorkerCompleted += bgwTimer_RunWorkerCompleted;
        }




        public Form_Timer()
        {
            InitializeComponent();
            InitializeSettings();
            InitializeBackgroundWorkers();


            // Initialize mouse hook delegate
            _proc = HookCallback;


            // Move the Form to right top
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            this.Location = new Point(workingArea.Right - this.Width, workingArea.Top);
        }
        private void Form_Timer_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            this.TransparencyKey = Color.DarkSlateGray;


            bgwHook.RunWorkerAsync();
        }




        public void ReloadTheme()
        {
            switch (Globals.Application.Theme.selectedTheme)
            {
                default:
                    label_Timer.ForeColor = Color.Black;
                    label_Timer.BackColor = Color.WhiteSmoke;
                    break;

                case Globals.Application.Theme.E_Themes.legacy:
                    label_Timer.ForeColor = Color.White;
                    label_Timer.BackColor = Color.FromArgb(255, 46, 51, 73);
                    break;

                case Globals.Application.Theme.E_Themes.darkMemories:
                    label_Timer.ForeColor = Color.White;
                    label_Timer.BackColor = Color.FromArgb(255, 44, 47, 51);
                    break;

                case Globals.Application.Theme.E_Themes.saintsRow:
                    label_Timer.ForeColor = Color.FromArgb(255, 146, 71, 214);
                    label_Timer.BackColor = Color.FromArgb(255, 37, 13, 57);
                    break;

                case Globals.Application.Theme.E_Themes.dracula:
                    label_Timer.ForeColor = Color.FromArgb(255, 248, 248, 242);
                    label_Timer.BackColor = Color.FromArgb(255, 40, 42, 54);
                    break;

                case Globals.Application.Theme.E_Themes.christmas:
                    label_Timer.ForeColor = Color.FromArgb(255, 255, 207, 109);
                    label_Timer.BackColor = Color.FromArgb(255, 24, 24, 24);
                    break;
            }
        }




        private void InitializeSettings()
        {
            ReloadTheme();
        }
    }
}
