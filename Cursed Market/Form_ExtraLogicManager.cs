using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Cursed_Market.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;






namespace Cursed_Market
{
    public partial class Form_ExtraLogicManager : Form
    {
        private readonly Func<List<ExtraLogic.ModuleInfo>> scanModules;
        private List<ExtraLogic.ModuleInfo> modules = new List<ExtraLogic.ModuleInfo>();


        public Form_ExtraLogicManager(List<ExtraLogic.ModuleInfo> initialModules, Func<List<ExtraLogic.ModuleInfo>> moduleScanner)
        {
            InitializeComponent();
            InitializeSettings();

            scanModules = moduleScanner;
            FillList(initialModules);
        }


        public void ReloadTheme()
        {
            var theme = ProgramThemes.GetCurrentPalette();

            this.BackColor = theme.FormBack;
            panel_WindowHeader.Background = ToBackdropBrush(theme.HeaderBack);

            ProgramThemes.ApplyForeColor(theme.TitleText, label_FormTitle, label_Hint);
            ProgramThemes.ApplyForeColor(theme.PrimaryText, checkedListBox_Modules, textBox_Summary);

            checkedListBox_Modules.Background = theme.SecondaryCardBack.ToBrush();
            textBox_Summary.Background = theme.SecondaryCardBack.ToBrush();

            button_Start.Background = theme.PrimaryButtonBack.ToBrush();
            button_Start.Foreground = theme.PrimaryButtonText.ToBrush();
            button_Start.BorderBrush = theme.PrimaryButtonBorder.ToBrush();
        }


        private void InitializeSettings()
        {
            ReloadTheme();
        }


        private void button_WindowClose_MouseClick(object sender, RoutedEventArgs e) => CloseWithResult(DialogResult.Cancel);


        private void panel_WindowHeader_MouseDown(object sender, PointerPressedEventArgs e)
        {
            if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                BeginMoveDrag(e); // Same as sending WM_NCLBUTTONDOWN (HTCAPTION), system takes over window dragging.
            }
        }


        private void button_Start_MouseClick(object sender, RoutedEventArgs e) => CloseWithResult(DialogResult.OK);


        private void button_Cancel_MouseClick(object sender, RoutedEventArgs e) => CloseWithResult(DialogResult.Cancel);


        private void button_Refresh_MouseClick(object sender, RoutedEventArgs e) => RefreshModules();


        private void button_OpenFolder_MouseClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Directory.CreateDirectory(ProgramPaths.extraLogicDirectoryPath);
                Process.Start(new ProcessStartInfo(ProgramPaths.extraLogicDirectoryPath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                Messaging.ShowMessage($"Failed to open Extra Logic folder!\n\n{ex.Message}", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void checkedListBox_Modules_SelectedIndexChanged(object sender, SelectionChangedEventArgs e) => UpdateSummary();


        private void CloseWithResult(DialogResult result)
        {
            this.DialogResult = result;
            this.Close();
        }


        /* Modules with their final isSelected state. */
        public List<ExtraLogic.ModuleInfo> GetModules()
        {
            for (int i = 0; i < modules.Count; i++)
                modules[i].isSelected = ((CheckBox)checkedListBox_Modules.Items[i]).IsChecked == true; // Every list item is a CheckBox, see FillList.

            return modules;
        }


        private void FillList(List<ExtraLogic.ModuleInfo> newModules)
        {
            modules = newModules ?? new List<ExtraLogic.ModuleInfo>();

            checkedListBox_Modules.Items.Clear();
            foreach (ExtraLogic.ModuleInfo module in modules)
                checkedListBox_Modules.Items.Add(new CheckBox { Content = GetDisplayName(module), IsChecked = module.isSelected });

            if (checkedListBox_Modules.Items.Count > 0)
                checkedListBox_Modules.SelectedIndex = 0;
            else
                UpdateSummary();
        }


        private static string GetDisplayName(ExtraLogic.ModuleInfo module)
        {
            string displayName = module.hasRiskyCapabilities ? "[!] " + module.name : module.name;

            if (!string.IsNullOrEmpty(module.version))
                displayName += $"  v{module.version}";

            switch (module.state)
            {
                case ExtraLogic.E_ModuleState.New: return displayName + "  (new)";
                case ExtraLogic.E_ModuleState.Changed: return displayName + "  (changed)";
                default: return displayName;
            }
        }


        private void UpdateSummary()
        {
            var theme = ProgramThemes.GetCurrentPalette();
            int index = checkedListBox_Modules.SelectedIndex;

            if (index < 0 || index >= modules.Count)
            {
                textBox_Summary.Foreground = theme.PrimaryText.ToBrush();
                textBox_Summary.Text = modules.Count == 0
                    ? "No modules found. Put *.csx files into \"Extra Logic\" folder next to Cursed Market and press Refresh."
                    : string.Empty;
                return;
            }

            ExtraLogic.ModuleInfo module = modules[index];
            string header = $"{module.name}{(string.IsNullOrEmpty(module.version) ? string.Empty : "  v" + module.version)}" + Environment.NewLine +
                            $"Author: {(string.IsNullOrEmpty(module.author) ? "unknown" : module.author)}" + Environment.NewLine;

            if (module.state == ExtraLogic.E_ModuleState.Changed)
                header += "File has changed since you last reviewed it." + Environment.NewLine;

            textBox_Summary.Foreground = (module.hasRiskyCapabilities ? Colors.IndianRed : theme.PrimaryText).ToBrush();
            textBox_Summary.Text = header + Environment.NewLine + ExtraLogic.BuildSummary(module);
        }


        /* Rescans folder, keeps current ticks for files that are still present and unchanged. */
        private void RefreshModules()
        {
            if (scanModules == null)
                return;

            Dictionary<string, bool> currentTicks = GetModules().ToDictionary(m => m.fileName + "|" + m.sha256, m => m.isSelected, StringComparer.OrdinalIgnoreCase);

            List<ExtraLogic.ModuleInfo> newModules = scanModules();
            foreach (ExtraLogic.ModuleInfo module in newModules)
            {
                if (currentTicks.TryGetValue(module.fileName + "|" + module.sha256, out bool isTicked))
                    module.isSelected = isTicked;
            }

            FillList(newModules);
        }
    }
}
