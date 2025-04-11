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

namespace CâblageCeff.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private List<Panel>? panels;

        [ObservableProperty]
        private string? panelCount;


        ExcelPackage pkg;
        ExcelWorksheet ws;
        Panel? panel;
        
        public  MainWindowViewModel()
        {
            
            List<Panel> panels = [];
            List<string> subs = [];

            FileInfo file = new("C:\\Users\\cp-23rul\\Source\\Repos\\CablageCeff\\CâblageCeff\\Assets\\Liste des patch panel et numéro.xlsx");
            if(file.Exists)
            {
                file = new FileInfo(file.FullName);
                pkg = new ExcelPackage(file);
                if (pkg.Workbook.Worksheets.Any())
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
            
            Panels = panels;
            PanelCount = $"{panels.Count} patch panel(s)";
        }
        public string Greeting { get; } = "Welcome to Avalonia!";

        public delegate Task ShowPatchDialogFunc();
        public ShowPatchDialogFunc? ShowPatchDialog { get; set; }

        [RelayCommand]
        private async Task OpenPatchScreen()
        {
            if (ShowPatchDialog == null)
                return;
            await ShowPatchDialog();
        }

        [RelayCommand]
        private void ChangeTheme()
        {
            Application.Current!.RequestedThemeVariant = Application.Current.RequestedThemeVariant == ThemeVariant.Light
                ? ThemeVariant.Dark
                : ThemeVariant.Light;
        }

        [RelayCommand]
        private void Quitter()
        {
            Environment.Exit(0);
        }
    }
}
