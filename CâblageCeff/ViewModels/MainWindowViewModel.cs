using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Platform;
using CâblageCeff.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using OfficeOpenXml;

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

            for (int i = 1; i <= 330;i++)
            {
                for (int j = 0; j <= 4;j++)
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
    }
}
