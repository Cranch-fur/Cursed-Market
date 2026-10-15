using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;






namespace Cursed_Market
{
    public partial class Form_Crosshair : Form
    {
        #region Win32 API (Hooks & Layered Window)

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pptSrc, uint crKey, [In] ref BLENDFUNCTION pblend, uint dwFlags);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        private static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        private const byte AC_SRC_OVER = 0x00;
        private const byte AC_SRC_ALPHA = 0x01;
        private const uint ULW_ALPHA = 0x00000002;

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        #endregion

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                /* 0x20 (WS_EX_TRANSPARENT) | 0x80 (WS_EX_TOOLWINDOW) | 0x80000 (WS_EX_LAYERED) | 0x08000000 (WS_EX_NOACTIVATE). */
                createParams.ExStyle |= 0x20 | 0x80 | 0x80000 | 0x08000000;
                return createParams;
            }
        }

        private IntPtr _hookID = IntPtr.Zero;
        private readonly LowLevelMouseProc _proc;
        private Bitmap _currentBitmap;

        private const int WH_MOUSE_LL = 14;
        private const int WM_RBUTTONDOWN = 0x204;
        private const int WM_RBUTTONUP = 0x205;

        public Form_Crosshair()
        {
            InitializeComponent();

            /* Hide legacy PictureBox since UpdateLayeredWindow renders directly onto the form surface. */
            pictureBox_Crosshair.Visible = false;

            _proc = HookCallback;

            if (ProgramStartupArguments.HasStartupArgument(ProgramStartupArguments.Common.crosshairToggleFeature))
            {
                _hookID = SetHook(_proc);
            }
        }

        private IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && this.IsHandleCreated && !this.IsDisposed)
            {
                if (wParam == (IntPtr)WM_RBUTTONDOWN)
                {
                    this.BeginInvoke((Action)(() => ApplyLayeredBitmap(_currentBitmap, ProgramFeatures.Crosshair.GetOpacity())));
                }
                else if (wParam == (IntPtr)WM_RBUTTONUP)
                {
                    this.BeginInvoke((Action)(() => ApplyLayeredBitmap(_currentBitmap, 0)));
                }
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private void Form_Crosshair_Load(object sender, EventArgs e)
        {
            ForceInitializeSettings();
        }

        private void Form_Crosshair_Shown(object sender, EventArgs e)
        {
            ForceInitializeSettings();
        }

        private void Crosshair_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                return;
            }

            if (_hookID != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_hookID);
                _hookID = IntPtr.Zero;
            }

            _currentBitmap?.Dispose();
        }

        /* Applies true 32-bit per-pixel alpha bitmap to the window. */
        private void ApplyLayeredBitmap(Bitmap bitmap, int opacityPercent)
        {
            if (!this.IsHandleCreated || this.IsDisposed || bitmap == null)
                return;

            /* Ensure the bitmap is formatted as 32bppArgb for alpha channel support. */
            Bitmap argbBitmap = bitmap.PixelFormat == PixelFormat.Format32bppArgb
                ? bitmap
                : bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format32bppArgb);

            IntPtr screenDc = GetDC(IntPtr.Zero);
            IntPtr memDc = CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;

            try
            {
                hBitmap = argbBitmap.GetHbitmap(Color.FromArgb(0));
                oldBitmap = SelectObject(memDc, hBitmap);

                Rectangle screenBounds = Screen.PrimaryScreen.Bounds;
                Size size = new Size(argbBitmap.Width, argbBitmap.Height);
                Point topPos = new Point(
                    screenBounds.Width / 2 - size.Width / 2,
                    screenBounds.Height / 2 - size.Height / 2);
                Point pointSource = new Point(0, 0);

                byte alphaValue = (byte)Math.Round(Math.Max(0, Math.Min(100, opacityPercent)) * 2.55f);

                BLENDFUNCTION blend = new BLENDFUNCTION
                {
                    BlendOp = AC_SRC_OVER,
                    BlendFlags = 0,
                    SourceConstantAlpha = alphaValue,
                    AlphaFormat = AC_SRC_ALPHA
                };

                UpdateLayeredWindow(this.Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, ULW_ALPHA);
            }
            finally
            {
                ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    SelectObject(memDc, oldBitmap);
                    DeleteObject(hBitmap);
                }
                DeleteDC(memDc);

                if (!ReferenceEquals(argbBitmap, bitmap))
                    argbBitmap.Dispose();
            }
        }

        private void SetCrosshairBitmap(Image sourceImage)
        {
            _currentBitmap?.Dispose();
            _currentBitmap = sourceImage != null ? new Bitmap(sourceImage) : new Bitmap(1, 1, PixelFormat.Format32bppArgb);
            ApplyLayeredBitmap(_currentBitmap, ProgramFeatures.Crosshair.GetOpacity());
        }

        private void SetCrosshairFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    byte[] bytes = File.ReadAllBytes(filePath);
                    using (MemoryStream ms = new MemoryStream(bytes))
                    using (Image img = Image.FromStream(ms))
                    {
                        SetCrosshairBitmap(img);
                        return;
                    }
                }
                catch { }
            }

            SetCrosshairBitmap(Properties.Resources.IMG_CROSSHAIR_MISSING);
        }

        public void ForceInitializeSettings() => InitializeSettings();

        private void InitializeSettings()
        {
            switch (ProgramFeatures.Crosshair.GetSelectedCrosshair())
            {
                default:
                    SetCrosshairBitmap(Properties.Resources.IMG_MISSING);
                    break;
                case ProgramFeatures.Crosshair.E_Crosshairs.none:
                    SetCrosshairBitmap(null);
                    break;
                case ProgramFeatures.Crosshair.E_Crosshairs.customizable:
                    using (Bitmap customBmp = CrosshairGenerator.RenderCrosshair(ProgramFeatures.Crosshair.Settings))
                    {
                        SetCrosshairBitmap(customBmp);
                    }
                    break;
                case ProgramFeatures.Crosshair.E_Crosshairs.circleAqua:
                    SetCrosshairBitmap(Properties.Resources.IMG_CROSSHAIR_DEFAULT_CIRCLE_AQUA);
                    break;
                case ProgramFeatures.Crosshair.E_Crosshairs.circleWhite:
                    SetCrosshairBitmap(Properties.Resources.IMG_CROSSHAIR_DEFAULT_CIRCLE_WHITE);
                    break;
                case ProgramFeatures.Crosshair.E_Crosshairs.dotAqua:
                    SetCrosshairBitmap(Properties.Resources.IMG_CROSSHAIR_DEFAULT_DOT_AQUA);
                    break;
                case ProgramFeatures.Crosshair.E_Crosshairs.dotGreen:
                    SetCrosshairBitmap(Properties.Resources.IMG_CROSSHAIR_DEFAULT_DOT_GREEN);
                    break;
                case ProgramFeatures.Crosshair.E_Crosshairs.dotRed:
                    SetCrosshairBitmap(Properties.Resources.IMG_CROSSHAIR_DEFAULT_DOT_RED);
                    break;
                case ProgramFeatures.Crosshair.E_Crosshairs.dotYellow:
                    SetCrosshairBitmap(Properties.Resources.IMG_CROSSHAIR_DEFAULT_DOT_YELLOW);
                    break;
                case ProgramFeatures.Crosshair.E_Crosshairs.tacticAqua:
                    SetCrosshairBitmap(Properties.Resources.IMG_CROSSHAIR_TACTIC_AQUA);
                    break;
                case ProgramFeatures.Crosshair.E_Crosshairs.tacticWhite:
                    SetCrosshairBitmap(Properties.Resources.IMG_CROSSHAIR_TACTIC_WHITE);
                    break;
                case ProgramFeatures.Crosshair.E_Crosshairs.custom01:
                    SetCrosshairFromFile(ProgramPaths.customCrosshair01FilePath);
                    break;
                case ProgramFeatures.Crosshair.E_Crosshairs.custom02:
                    SetCrosshairFromFile(ProgramPaths.customCrosshair02FilePath);
                    break;
                case ProgramFeatures.Crosshair.E_Crosshairs.custom03:
                    SetCrosshairFromFile(ProgramPaths.customCrosshair03FilePath);
                    break;
            }
        }
    }
}