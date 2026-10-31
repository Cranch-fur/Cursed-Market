using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Styling;
using Avalonia.Threading;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;






namespace Cursed_Market.UI
{
    /* Same values as System.Windows.Forms.DialogResult (and Win32 IDOK, IDCANCEL...). */
    public enum DialogResult
    {
        None = 0,
        OK = 1,
        Cancel = 2,
        Abort = 3,
        Retry = 4,
        Ignore = 5,
        Yes = 6,
        No = 7
    }

    public enum CloseReason
    {
        None,
        WindowsShutDown,
        FormOwnerClosing,
        UserClosing,
        ApplicationExitCall
    }

    public sealed class FormClosingEventArgs : CancelEventArgs
    {
        public CloseReason CloseReason { get; }

        public FormClosingEventArgs(CloseReason closeReason, bool cancel = false) : base(cancel)
        {
            CloseReason = closeReason;
        }
    }

    public sealed class FormClosedEventArgs : EventArgs
    {
        public CloseReason CloseReason { get; }

        public FormClosedEventArgs(CloseReason closeReason)
        {
            CloseReason = closeReason;
        }
    }

    /* Only extended window styles are carried over from WinForms, they're applied to native window once it's created. */
    public sealed class CreateParams
    {
        public int ExStyle { get; set; }
    }




    /* Avalonia Window that behaves like WinForms Form where Cursed Market relies on it:
       - Load is raised before window is shown for the first time, Shown right after it has been displayed (both again on every ShowDialog(), WinForms recreates modal window handle each time);
       - ShowDialog() is synchronous and returns DialogResult, setting DialogResult closes modal window;
       - closing a window only hides it, so cached instances in ProgramCache.Forms can be shown again (Main window & Dispose() close for real);
       - Win11 look: client area is extended over system frame, so DWM still draws rounded corners, shadow and Mica backdrop. */
    public class Form : Window, IDisposable
    {
        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll")]
        private static extern bool SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dwFlags);
        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hWnd, int dwAttribute, ref int pvAttribute, int cbAttribute);
        [DllImport("ntdll.dll")]
        private static extern int RtlGetVersion(ref S_OSVERSIONINFOEX versionInfo);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct S_OSVERSIONINFOEX
        {
            public int dwOSVersionInfoSize;
            public int dwMajorVersion;
            public int dwMinorVersion;
            public int dwBuildNumber;
            public int dwPlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
            public ushort wServicePackMajor;
            public ushort wServicePackMinor;
            public ushort wSuiteMask;
            public byte wProductType;
            public byte wReserved;
        }

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_LAYERED = 0x80000;
        private const uint LWA_ALPHA = 0x2;
        private const int DWMWA_WINDOW_CORNER_PREFERENCE = 33;
        private const int DWMWCP_ROUND = 2;

        /* How much of the theme color is kept over Mica, the rest lets backdrop shine through. */
        private const double BACKDROP_TINT_OPACITY = 0.78;




        /* Environment.OSVersion lies without supportedOS manifest entries, RtlGetVersion doesn't. Mica is available starting with Windows 11 (build 22000). */
        private static bool ObtainIsMicaSupported()
        {
            try
            {
                S_OSVERSIONINFOEX versionInfo = new S_OSVERSIONINFOEX { dwOSVersionInfoSize = Marshal.SizeOf(typeof(S_OSVERSIONINFOEX)) };
                return RtlGetVersion(ref versionInfo) == 0 && versionInfo.dwMajorVersion >= 10 && versionInfo.dwBuildNumber >= 22000;
            }
            catch
            {
                return false;
            }
        }

        public static readonly bool isMicaSupported = ObtainIsMicaSupported();




        private bool isFirstShowPrepared = false;
        private bool isShownRaised = false;
        private bool isModal = false;
        private bool isClosingForReal = false;
        private bool isClosedDuringShow = false;
        private bool isClosedWhileVisible = false;
        private CloseReason lastCloseReason = CloseReason.None;
        private DispatcherFrame modalFrame = null;
        private DialogResult dialogResult = DialogResult.None;
        private Color backColor = Colors.White;

        public event EventHandler Load;
        public event EventHandler Shown;
        public event EventHandler<FormClosingEventArgs> FormClosing;
        public event EventHandler<FormClosedEventArgs> FormClosed;

        public bool IsDisposed { get; private set; } = false;
        public bool IsHandleCreated => !IsDisposed && TryGetPlatformHandle() != null;
        public bool InvokeRequired => !Dispatcher.UIThread.CheckAccess();

        public void Invoke(Action action) => Dispatcher.UIThread.Invoke(action);
        public void BeginInvoke(Action action) => Dispatcher.UIThread.Post(action);

        protected virtual CreateParams CreateParams => new CreateParams();




        public Form()
        {
            this.CanResize = false;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Icon = Properties.Resources.Icon.ToWindowIcon();

            this.ExtendClientAreaToDecorationsHint = true;
            this.ExtendClientAreaChromeHints = ExtendClientAreaChromeHints.NoChrome;
            this.ExtendClientAreaTitleBarHeightHint = 0; // Every form draws its own header and drags the window from it.

            this.TransparencyLevelHint = new[] { WindowTransparencyLevel.Mica, WindowTransparencyLevel.AcrylicBlur, WindowTransparencyLevel.None };
            this.Background = Brushes.Transparent;
        }




        /* Same as WinForms BackColor, but also picks light / dark system theme variant and keeps Mica visible behind the color. */
        public Color BackColor
        {
            get => backColor;
            set
            {
                backColor = value;
                this.RequestedThemeVariant = ProgramThemes.GetCurrentPalette().IsLightTheme ? ThemeVariant.Light : ThemeVariant.Dark;
                this.Background = ToBackdropBrush(value);
            }
        }

        /* Translucent brush when window is going to have Mica / Acrylic backdrop, solid one otherwise. */
        public IBrush ToBackdropBrush(Color color)
        {
            bool hasBackdrop = isMicaSupported && this.TransparencyLevelHint.Any(level => level == WindowTransparencyLevel.Mica || level == WindowTransparencyLevel.AcrylicBlur);
            return hasBackdrop
                ? new SolidColorBrush(color, BACKDROP_TINT_OPACITY)
                : new SolidColorBrush(color);
        }

        public DialogResult DialogResult
        {
            get => dialogResult;
            set
            {
                dialogResult = value;

                if (isModal && value != DialogResult.None)
                    this.Close();
            }
        }




        private void PrepareFirstShow()
        {
            if (isFirstShowPrepared)
                return;

            isFirstShowPrepared = true;
            ApplyNativeStyles();
            Load?.Invoke(this, EventArgs.Empty);
        }

        private void ApplyNativeStyles()
        {
            IntPtr? handle = TryGetPlatformHandle()?.Handle;
            if (handle == null || handle == IntPtr.Zero || !RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return;

            int exStyle = CreateParams.ExStyle;
            if (exStyle != 0)
            {
                SetWindowLong(handle.Value, GWL_EXSTYLE, GetWindowLong(handle.Value, GWL_EXSTYLE) | exStyle);

                // Layered window stays invisible until its attributes are set, full alpha keeps Avalonia rendering as it is.
                if ((exStyle & WS_EX_LAYERED) != 0)
                    SetLayeredWindowAttributes(handle.Value, 0, 255, LWA_ALPHA);
            }

            if (this.SystemDecorations != SystemDecorations.None)
            {
                int cornerPreference = DWMWCP_ROUND;
                DwmSetWindowAttribute(handle.Value, DWMWA_WINDOW_CORNER_PREFERENCE, ref cornerPreference, sizeof(int));
            }
        }

        public new void Show()
        {
            PrepareFirstShow();
            base.Show();
        }

        public DialogResult ShowDialog()
        {
            dialogResult = DialogResult.None;
            isClosedDuringShow = false;
            isModal = true;

            PrepareFirstShow();

            /* Load handler may close the window already, WinForms doesn't show it in such case. */
            if (!isClosedDuringShow && !IsDisposed)
            {
                Window owner = GetDialogOwner();
                if (owner != null)
                    _ = base.ShowDialog(owner);
                else
                    base.Show();

                if (this.IsVisible)
                {
                    modalFrame = new DispatcherFrame();
                    Dispatcher.UIThread.PushFrame(modalFrame); // Same as WinForms modal loop, returns once window has been hidden or closed.
                    modalFrame = null;
                }
            }

            isModal = false;
            isFirstShowPrepared = false; // Next ShowDialog() raises Load & Shown again.
            isShownRaised = false;
            return dialogResult;
        }

        private Window GetDialogOwner()
        {
            if (!(Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop))
                return null;

            Window activeWindow = desktop.Windows.FirstOrDefault(window => window != this && window.IsVisible && window.IsActive);
            if (activeWindow != null)
                return activeWindow;

            return desktop.MainWindow != null && desktop.MainWindow != this && desktop.MainWindow.IsVisible ? desktop.MainWindow : null;
        }

        private void EndModalLoop()
        {
            if (modalFrame != null)
                modalFrame.Continue = false;
        }

        private bool GetIsMainWindow()
        {
            return Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop && desktop.MainWindow == this;
        }




        protected override void OnOpened(EventArgs e)
        {
            PrepareFirstShow(); // Window may be shown by Avalonia itself (e.g. lifetime's MainWindow), Load must still come first.
            base.OnOpened(e);

            if (!isShownRaised)
            {
                isShownRaised = true;
                Dispatcher.UIThread.Post(() => Shown?.Invoke(this, EventArgs.Empty), DispatcherPriority.Background);
            }
        }

        protected override void OnClosing(WindowClosingEventArgs e)
        {
            /* Dispose() of an already hidden window: WinForms raises no closing events for it. */
            if (isClosingForReal && !this.IsVisible)
            {
                isClosedWhileVisible = false;
                base.OnClosing(e);
                return;
            }

            CloseReason closeReason;
            switch (e.CloseReason)
            {
                case WindowCloseReason.ApplicationShutdown:
                    closeReason = CloseReason.ApplicationExitCall;
                    break;

                case WindowCloseReason.OSShutdown:
                    closeReason = CloseReason.WindowsShutDown;
                    break;

                case WindowCloseReason.OwnerWindowClosing:
                    closeReason = CloseReason.FormOwnerClosing;
                    break;

                default:
                    closeReason = CloseReason.UserClosing; // WinForms reports Close() calls as UserClosing as well.
                    break;
            }

            FormClosingEventArgs formClosingArgs = new FormClosingEventArgs(closeReason, e.Cancel);
            FormClosing?.Invoke(this, formClosingArgs);
            e.Cancel = formClosingArgs.Cancel;

            base.OnClosing(e);

            if (e.Cancel)
                return;

            lastCloseReason = closeReason;
            isClosedDuringShow = true;
            isClosedWhileVisible = this.IsVisible;

            bool isShuttingDown = closeReason == CloseReason.ApplicationExitCall || closeReason == CloseReason.WindowsShutDown;
            if (!isClosingForReal && !isShuttingDown && !GetIsMainWindow())
            {
                e.Cancel = true;
                this.Hide();

                if (isClosedWhileVisible)
                    FormClosed?.Invoke(this, new FormClosedEventArgs(closeReason));
            }
        }

        public override void Hide()
        {
            base.Hide();
            EndModalLoop();
        }

        protected override void OnClosed(EventArgs e)
        {
            IsDisposed = true;
            EndModalLoop();
            base.OnClosed(e);

            if (isClosedWhileVisible)
                FormClosed?.Invoke(this, new FormClosedEventArgs(lastCloseReason));
        }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            isClosingForReal = true;
            this.Close();
        }
    }
}