using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Platform;
using CâblageCeff.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using OfficeOpenXml;

using Avalonia;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.Collections;
using CâblageCeff.Views;
using System.Collections.ObjectModel;

namespace CâblageCeff.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<Panel>? panels;

        [ObservableProperty]
        private string? panelCount;


        ExcelPackage pkg;
        ExcelWorksheet ws;
        Panel? panel;

        [ObservableProperty]
        private Panel editablePanel = new Panel("", "", "",0);

        // UI Dialogs
        public delegate Task ShowPatchDialogFunc(Panel panel);
        //public delegate Task<Panel> ShowUpdatePanelDialogFunc(Panel panel);
        public ShowPatchDialogFunc? ShowPatchDialog { get; set; }
        //public ShowUpdatePanelDialogFunc? ShowUpdatePanelDialog { get; set; }


        public  MainWindowViewModel()
        {

            List<Panel> panels = [];
            List<string> subs = [];

            FileInfo file = new("Assets//Liste des patch panel et numéro.xlsx");
            if(file.Exists)
            {
                file = new FileInfo(file.FullName);
                pkg = new ExcelPackage(file);
                if (pkg.Workbook.Worksheets.Count != 0)
                    ws = pkg.Workbook.Worksheets.First();
                else
                    throw new InvalidOperationException("The Excel file contains no worksheets.");
            }
            else
                throw new FileNotFoundException("The specified file does not exist.", file.FullName);
            
            
            for (int i = 2; i <= 330;i++)
            {
                for (int j = 1; j <= 4;j++)
                {
                    
                    if (ws.Cells[i, j] == null)
                        break;
                    subs.Add(ws.Cells[i, j].Text);
                }
                if (subs[3] == "")
                {
                    panel = new(subs[0], subs[1], subs[2], 0);
                }
                else
                {
                    panel = new(subs[0], subs[1], subs[2], int.Parse(subs[3]));
                }
                panels.Add(panel);
                subs.Clear();
            }

            Panels = new ObservableCollection<Panel>(panels);
            PanelCount = $"{Panels.Count} patch panel(s)";
        }

        [RelayCommand]
        private async Task AddPanel()
        {
            var panel = new Panel($"{Panels.Count + 1}", "", "", 0);

            Panels?.Add(panel);
            PanelCount = $"{Panels?.Count} patch panel(s)";

            EditablePanel = panel;

            MainScreenIsVisible = false;
            UpdatePanelScreenIsVisible = true;

            //if (ShowUpdatePanelDialog == null)
            //    return;
            //var panel = new Panel("", "", "", 0);
            //panel = await ShowUpdatePanelDialog(panel);
            //if (panel != null)
            //{
            //    Panels?.Add(panel);
            //    PanelCount = $"{Panels?.Count} patch panel(s)";
            //}
        }

        [ObservableProperty]
        private bool mainScreenIsVisible = true;

        [ObservableProperty]
        private bool updatePanelScreenIsVisible = false;

        [RelayCommand]
        private async Task EditPanel(Object c)
        {
            var panel = c as Panel;
            if (panel == null)
                return;
            EditablePanel = panel;

            MainScreenIsVisible = false;
            UpdatePanelScreenIsVisible = true;

            //if (panel == null || ShowUpdatePanelDialog == null)
            //    return;
            //var result = await ShowUpdatePanelDialog(panel);
            //if (result != null)
            //{
            //    var panels = Panels?.ToList();
            //    panels[panels.IndexOf(panel)] = result;
            //    Panels = panels;
            //    PanelCount = $"{Panels?.Count} patch panel(s)";
            //}
        }

        [RelayCommand]
        private async Task OpenPatchScreen(Object c)
        {
            var panel = c as Panel;
            if (panel == null || ShowPatchDialog == null || panel.NbrPort == 0)
                return;
            await ShowPatchDialog(panel);
        }

        [RelayCommand]
        private static void ChangeTheme()
        {
            Application.Current!.RequestedThemeVariant = Application.Current.RequestedThemeVariant == ThemeVariant.Light
                ? ThemeVariant.Dark
                : ThemeVariant.Light;
        }

        [RelayCommand]
        private static void Quitter()
        {
            Environment.Exit(0);
        }

        [RelayCommand]
        private static void Restart()
        {
            var fileName = Environment.ProcessPath;
            if (fileName != null)
            {
                System.Diagnostics.Process.Start(fileName);
                Environment.Exit(0);
            }
        }

        [RelayCommand]
        private void DeletePanel(IList list)
        {
            var panelsToRemove = new Panel?[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                panelsToRemove[i] = list[i] as Panel;
            }
            var panels = Panels?.ToList();
            foreach (var c in panelsToRemove)
            {
                if (c != null)
                {
                    panels[panels.IndexOf(c)].Batiment = null;
                    panels[panels.IndexOf(c)].Emplacement = null;
                    panels[panels.IndexOf(c)].NbrPort = 0;
                }
            }
            //Panels = panels;
            //PanelCount = $"{Panels?.Count} patch panel(s)";
        }

        //--------------------------------------------------------------------------------------------------

        [RelayCommand]
        void Cancel()
        {
            MainScreenIsVisible = true;
            UpdatePanelScreenIsVisible = false;
        }
    }
}
