using Avalonia;
using Avalonia.Media;
using Cursed_Market.UI;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;






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


        [DllImport("user32.dll")]
        private static extern int GetMessage(out S_MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);


        [DllImport("user32.dll")]
        private static extern bool TranslateMessage([In] ref S_MSG lpMsg);


        [DllImport("user32.dll")]
        private static extern IntPtr DispatchMessage([In] ref S_MSG lpmsg);


        [StructLayout(LayoutKind.Sequential)]
        private struct S_MSG
        {
            public IntPtr hwnd;
            public uint message;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public int ptX;
            public int ptY;
        }


        /* Delegate for the hook callback function. */
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);




        protected override CreateParams CreateParams
        {
            get
            {
                /* Retrieve the base window creation parameters. */
                CreateParams createParams = base.CreateParams;

                // The ExStyle property sets the extended window styles.
                // Here we enable several flags using the bitwise OR operator:
                //
                // 0x20  - WS_EX_TRANSPARENT:
                //         Makes the window transparent to mouse events, meaning that clicks pass through the window.
                //
                // 0x80  - WS_EX_TOOLWINDOW:
                //         Designates the window as a tool window, which prevents it from appearing on the taskbar.
                //
                // 0x80000  - WS_EX_LAYERED:
                //         Allows the use of transparency effects and alpha blending for the window.
                //
                // 0x08000000  - WS_EX_NOACTIVATE:
                //         Prevents the window from being activated when clicked, i.e., it does not gain focus.
                createParams.ExStyle |= 0x20 | 0x80 | 0x80000 | 0x08000000;


                return createParams;
            }
        }




        /* Global hook variables. */
        private IntPtr _hookID = IntPtr.Zero;
        private LowLevelMouseProc _proc;

        /* Constants for hook and mouse event. */
        private const int WH_MOUSE_LL = 14;
        private const int WM_MBUTTONDOWN = 0x207;

        /* Background workers for mouse hook and timer. */
        private BackgroundWorker bgwHook;
        private BackgroundWorker bgwTimer;

        /* Timer duration in seconds. */
        private const int TimerDuration = 60;




        /* Set the global mouse hook. */
        private IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }
        /* Global hook callback function. */
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            /* If nCode is non-negative and the event is middle mouse button down. */
            if (nCode >= 0 && wParam == (IntPtr)WM_MBUTTONDOWN)
            {
                // Invoke ToggleTimer on the UI thread
                this.BeginInvoke((Action)(() =>
                {
                    ToggleTimer();
                }));
            }
            /* Pass the hook information to the next hook in the chain. */
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }




        /* Background worker method for global mouse hook. */
        private void bgwHook_DoWork(object sender, DoWorkEventArgs e)
        {
            _hookID = SetHook(_proc); // Set the global hook.

            /* Start a message loop to keep the hook active (same as WinForms Application.Run() on this thread). */
            while (GetMessage(out S_MSG msg, IntPtr.Zero, 0, 0) > 0)
            {
                TranslateMessage(ref msg);
                DispatchMessage(ref msg);
            }
        }

        /* Background worker method for the timer. */
        private void bgwTimer_DoWork(object sender, DoWorkEventArgs e)
        {
            int remaining = TimerDuration;
            while (remaining >= 0)
            {
                /* Check if cancellation is requested. */
                if (bgwTimer.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }


                if (remaining == 3)
                {
                    Media.PlaySoundFromStream(Properties.Resources.SFX_RaceCountdown);
                }


                /* Report progress with the remaining time. */
                bgwTimer.ReportProgress(0, remaining);
                Thread.Sleep(1000);
                remaining--;
            }
        }




        /* Update the label with the remaining time (executed on the UI thread). */
        private void bgwTimer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int remaining = (int)e.UserState;
            label_Timer.Text = $"{remaining:D2}";


            this.Opacity = 1;
        }

        /* Hide the label when the timer completes or is cancelled. */
        private void bgwTimer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Opacity = 0;
        }




        /* Toggle timer: start it if not running, or stop it if already running. */
        public void ToggleTimer()
        {
            if (bgwTimer.IsBusy)
            {
                Media.PlaySoundFromStream(Properties.Resources.SFX_Boopie_Error);
                bgwTimer.CancelAsync();
            }
            else
            {
                Media.PlaySoundFromStream(Properties.Resources.SFX_Boopie);
                bgwTimer.RunWorkerAsync();
            }
        }




        private void InitializeBackgroundWorkers()
        {
            /* Initialize background worker for global mouse hook. */
            bgwHook = new BackgroundWorker();
            bgwHook.WorkerSupportsCancellation = true;
            bgwHook.DoWork += bgwHook_DoWork;


            /* Initialize background worker for timer. */
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

            _proc = HookCallback; // Initialize mouse hook delegate.

            /* Move the Form to right top. */
            PixelRect workingArea = this.Screens.Primary?.WorkingArea ?? new PixelRect(0, 0, 1920, 1080);
            double screenScaling = this.Screens.Primary?.Scaling ?? 1.0;
            this.Position = new PixelPoint(workingArea.Right - (int)Math.Ceiling(this.Width * screenScaling), workingArea.Y); // WorkingArea is in physical pixels, Width in DIPs.
        }
        private void Form_Timer_Load(object sender, EventArgs e)
        {
            // TransparencyKey isn't needed anymore, window itself is transparent (see Form_Timer.axaml).


            bgwHook.RunWorkerAsync();
        }
        private void Form_Timer_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }




        public void ReloadTheme()
        {
            switch (ProgramThemes.GetSelectedTheme())
            {
                default:
                    label_Timer.Foreground = Colors.Black.ToBrush();
                    label_Timer.Background = Colors.WhiteSmoke.ToBrush();
                    break;

                case ProgramThemes.E_Themes.legacy:
                    label_Timer.Foreground = Colors.White.ToBrush();
                    label_Timer.Background = Color.FromArgb(255, 46, 51, 73).ToBrush();
                    break;

                case ProgramThemes.E_Themes.darkMemories:
                    label_Timer.Foreground = Colors.White.ToBrush();
                    label_Timer.Background = Color.FromArgb(255, 44, 47, 51).ToBrush();
                    break;

                case ProgramThemes.E_Themes.saintsRow:
                    label_Timer.Foreground = Color.FromArgb(255, 146, 71, 214).ToBrush();
                    label_Timer.Background = Color.FromArgb(255, 37, 13, 57).ToBrush();
                    break;

                case ProgramThemes.E_Themes.dracula:
                    label_Timer.Foreground = Color.FromArgb(255, 248, 248, 242).ToBrush();
                    label_Timer.Background = Color.FromArgb(255, 40, 42, 54).ToBrush();
                    break;

                case ProgramThemes.E_Themes.christmas:
                    label_Timer.Foreground = Color.FromArgb(255, 255, 207, 109).ToBrush();
                    label_Timer.Background = Color.FromArgb(255, 24, 24, 24).ToBrush();
                    break;
            }
        }




        private void InitializeSettings()
        {
            ReloadTheme();
        }
    }
}
