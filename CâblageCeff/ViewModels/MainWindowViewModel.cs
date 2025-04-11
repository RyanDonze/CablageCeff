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

        

        ExcelPackage pkg;
        ExcelWorksheet ws;
        
        public  MainWindowViewModel()
        {

            List<Panel> panels = [];
            List<string> subs = [];

            FileInfo file = new("avares://CâblageCeff/Assets/Liste des patch panel et numéro.xlsx");
            pkg = new ExcelPackage(file);
            ws = pkg.Workbook.Worksheets.First();

            for (int i = 1; i <= 330; i++)
            {
                for (int j = 0; j <= 4; j++)
                {
                    if (ws.Cells[i, j] == null)
                        break;
                    subs[j] = ws.Cells[i, j].ToString();
                }
                Panel panel = new(subs[0], subs[1], subs[2], int.Parse(subs[3]));
                panels.Add(panel);
                subs.Clear();
            }

            Panels = panels;
        }

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
