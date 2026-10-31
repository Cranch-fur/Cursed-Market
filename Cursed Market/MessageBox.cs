using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;






namespace Cursed_Market.UI
{
    /* Same values as System.Windows.Forms enums (and Win32 MB_* flags), so they can be passed straight to native MessageBox. */
    public enum MessageBoxButtons
    {
        OK = 0,
        OKCancel = 1,
        AbortRetryIgnore = 2,
        YesNoCancel = 3,
        YesNo = 4,
        RetryCancel = 5
    }

    public enum MessageBoxIcon
    {
        None = 0,
        Hand = 0x10,
        Stop = 0x10,
        Error = 0x10,
        Question = 0x20,
        Exclamation = 0x30,
        Warning = 0x30,
        Asterisk = 0x40,
        Information = 0x40
    }

    public enum MessageBoxDefaultButton
    {
        Button1 = 0,
        Button2 = 0x100,
        Button3 = 0x200
    }

    public enum ToolTipIcon
    {
        None = 0,
        Info = 1,
        Warning = 2,
        Error = 3
    }




    /* Win11 styled replacement of WinForms MessageBox. Show() blocks just like the original one, no matter which thread calls it. */
    public sealed class MessageBox : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "MessageBoxW")]
        private static extern int NativeMessageBox(IntPtr hWnd, string text, string caption, uint type);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr MB_GetString(uint wBtn);
        [DllImport("user32.dll")]
        private static extern bool MessageBeep(uint uType);

        private const uint MB_TOPMOST = 0x40000;




        /* Button captions in user's Windows language, same ones native MessageBox uses. */
        private static string GetButtonText(DialogResult button)
        {
            string fallbackText = button.ToString();

            try
            {
                IntPtr textPointer = MB_GetString((uint)button - 1);
                string nativeText = textPointer != IntPtr.Zero ? Marshal.PtrToStringUni(textPointer) : null;
                return string.IsNullOrEmpty(nativeText) ? fallbackText : nativeText.Replace("&", string.Empty);
            }
            catch
            {
                return fallbackText;
            }
        }

        private static DialogResult[] GetButtons(MessageBoxButtons buttons)
        {
            switch (buttons)
            {
                case MessageBoxButtons.OKCancel: return new[] { DialogResult.OK, DialogResult.Cancel };
                case MessageBoxButtons.AbortRetryIgnore: return new[] { DialogResult.Abort, DialogResult.Retry, DialogResult.Ignore };
                case MessageBoxButtons.YesNoCancel: return new[] { DialogResult.Yes, DialogResult.No, DialogResult.Cancel };
                case MessageBoxButtons.YesNo: return new[] { DialogResult.Yes, DialogResult.No };
                case MessageBoxButtons.RetryCancel: return new[] { DialogResult.Retry, DialogResult.Cancel };
                default: return new[] { DialogResult.OK };
            }
        }

        /* Segoe Fluent Icons glyphs (Windows 11), Segoe MDL2 Assets shares the same code points on Windows 10. */
        private static (string glyph, Color color) GetIconGlyph(MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.Error: return ("", Color.FromRgb(196, 43, 28));
                case MessageBoxIcon.Question: return ("", Color.FromRgb(0, 120, 212));
                case MessageBoxIcon.Warning: return ("", Color.FromRgb(157, 93, 0));
                case MessageBoxIcon.Information: return ("", Color.FromRgb(0, 120, 212));
                default: return (null, Colors.Transparent);
            }
        }




        public static DialogResult Show(string text,
            string caption = "",
                MessageBoxButtons buttons = MessageBoxButtons.OK,
                    MessageBoxIcon icon = MessageBoxIcon.None,
                        MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            /* Avalonia isn't running yet (early startup checks) or anymore, native box is the only option. */
            if (Application.Current == null || Application.Current.ApplicationLifetime == null)
                return (DialogResult)NativeMessageBox(IntPtr.Zero, text, caption, (uint)buttons | (uint)icon | (uint)defaultButton | MB_TOPMOST);

            if (!Dispatcher.UIThread.CheckAccess())
                return Dispatcher.UIThread.Invoke(() => Show(text, caption, buttons, icon, defaultButton));

            using (MessageBox messageBox = new MessageBox(text, caption, buttons, icon, defaultButton))
            {
                MessageBeep((uint)icon);
                DialogResult result = messageBox.ShowDialog();

                /* Closed by X / Alt+F4: WinForms gives Cancel if there's such button, otherwise the only (OK) button. */
                if (result == DialogResult.None)
                    result = messageBox.cancelResult;

                return result;
            }
        }




        private readonly DialogResult cancelResult;

        private MessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            var theme = ProgramThemes.GetCurrentPalette();
            DialogResult[] dialogButtons = GetButtons(buttons);
            int defaultButtonIndex = Math.Min((int)defaultButton >> 8, dialogButtons.Length - 1);

            cancelResult = Array.IndexOf(dialogButtons, DialogResult.Cancel) >= 0
                ? DialogResult.Cancel
                : (dialogButtons.Length == 1 ? dialogButtons[0] : DialogResult.None);

            this.Title = caption;
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.MinWidth = 360;
            this.MaxWidth = 620;
            this.ShowInTaskbar = false;
            this.Topmost = true;
            this.FontFamily = new FontFamily("Roboto");
            this.BackColor = theme.FormBack;




            /* Header, used for dragging the window around just like every other form. */
            TextBlock textBlock_Caption = new TextBlock
            {
                Text = caption,
                FontSize = 12,
                Foreground = theme.PrimaryText.ToBrush(),
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(16, 0, 16, 0)
            };
            Border panel_Header = new Border
            {
                Height = 32,
                Background = Brushes.Transparent,
                Child = textBlock_Caption
            };
            panel_Header.PointerPressed += (sender, e) =>
            {
                if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
                    BeginMoveDrag(e);
            };


            /* Icon & message. */
            StackPanel panel_Content = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Spacing = 16,
                Margin = new Thickness(24, 8, 24, 24)
            };

            (string glyph, Color glyphColor) = GetIconGlyph(icon);
            if (glyph != null)
            {
                panel_Content.Children.Add(new TextBlock
                {
                    Text = glyph,
                    FontFamily = new FontFamily("Segoe Fluent Icons, Segoe MDL2 Assets"),
                    FontSize = 28,
                    Foreground = new SolidColorBrush(glyphColor),
                    VerticalAlignment = VerticalAlignment.Top
                });
            }

            panel_Content.Children.Add(new SelectableTextBlock
            {
                Text = text,
                FontSize = 14,
                TextWrapping = TextWrapping.Wrap,
                MaxWidth = glyph != null ? 520 : 564,
                Foreground = theme.PrimaryText.ToBrush(),
                VerticalAlignment = VerticalAlignment.Center
            });


            /* Buttons, laid out on a darker footer like Win11 dialogs do. */
            StackPanel panel_Buttons = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right,
                Spacing = 8
            };

            List<Button> buttonControls = new List<Button>();
            for (int i = 0; i < dialogButtons.Length; i++)
            {
                DialogResult buttonResult = dialogButtons[i];
                Button button = new Button
                {
                    Content = GetButtonText(buttonResult),
                    MinWidth = 96,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    IsDefault = i == defaultButtonIndex,
                    IsCancel = buttonResult == cancelResult
                };

                if (i == defaultButtonIndex)
                    button.Classes.Add("accent");

                button.Click += (sender, e) => this.DialogResult = buttonResult;
                panel_Buttons.Children.Add(button);
                buttonControls.Add(button);
            }

            Border panel_Footer = new Border
            {
                Background = new SolidColorBrush(theme.IsLightTheme ? Colors.Black : Colors.White, 0.04),
                BorderBrush = new SolidColorBrush(theme.IsLightTheme ? Colors.Black : Colors.White, 0.08),
                BorderThickness = new Thickness(0, 1, 0, 0),
                Padding = new Thickness(24),
                Child = panel_Buttons
            };


            DockPanel panel_Root = new DockPanel();
            DockPanel.SetDock(panel_Header, Dock.Top);
            DockPanel.SetDock(panel_Footer, Dock.Bottom);
            panel_Root.Children.Add(panel_Header);
            panel_Root.Children.Add(panel_Footer);
            panel_Root.Children.Add(panel_Content);
            this.Content = panel_Root;

            this.Opened += (sender, e) => buttonControls[defaultButtonIndex].Focus();
            this.KeyDown += (sender, e) =>
            {
                if (e.Key == Key.C && e.KeyModifiers.HasFlag(KeyModifiers.Control))
                {
                    /* Ctrl + C copies message like native MessageBox does. */
                    StringBuilder clipboardText = new StringBuilder();
                    clipboardText.AppendLine(caption).AppendLine().AppendLine(text);
                    _ = this.Clipboard?.SetTextAsync(clipboardText.ToString());
                }
            };
        }
    }
}