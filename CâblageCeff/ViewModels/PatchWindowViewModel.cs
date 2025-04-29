using Avalonia.Controls;
using CâblageCeff.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CâblageCeff.ViewModels
{
    public partial class PatchWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private Models.Panel? panel;

        [ObservableProperty]
        private List<Patch>? patchs;

        [ObservableProperty]
        private string? patchCount;

        [ObservableProperty]
        private string? panelName;

        private readonly Window? window;

        ExcelPackage? pkg;
        ExcelWorksheet? ws;
        Patch? patch;

        public PatchWindowViewModel(Models.Panel p, Window patchWindow)
        {
            panel = p;
            panelName = $"{p.Batiment}.{p.Emplacement}.{p.Nom} Patchs";
            panelNbr = $"{p.NbrPort} patch(s)";
            window = patchWindow;

            List<Patch> patchs = [];
            List<string> subs = [];

            FileInfo file = new("Assets//Liste des patch panel et numéro.xlsx");
            file = new FileInfo(file.FullName);
            pkg = new ExcelPackage(file);
            if (pkg.Workbook.Worksheets.Count != 0)
              ws = pkg.Workbook.Worksheets[1];
            else
              throw new InvalidOperationException("The Excel file contains no worksheets.");

            for (int i = 2; i <= 17;i++)
            {
                for(int j = 1; j <= 5;j++)
                {
                    if(ws.Cells[i, j] == null)
                        break;
                    subs.Add(ws.Cells[i, j].Text);
                }
                patch = new Patch(subs[0], subs[1], subs[2], subs[3], subs[4]);
                patchs.Add(patch);
                subs.Clear();
            }


            Patchs = patchs;
            PatchCount = $"{Patchs.Count} patch(s)";
        }

        [RelayCommand]
        private void Quitter()
        {
            window?.Close();
        }
    }
}

