using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Platform;
using CâblageCeff.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CâblageCeff.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private List<Panel> panels;
      
        public MainWindowViewModel()
        {
            List<Panel> panels = [];

            using var ressource = AssetLoader.Open(new Uri("Liste des patch panel et numéro.xlsx"));
            using var reader = new StreamReader(ressource);

            string contents = reader.ReadToEnd();
            string[] lines = contents.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                string[] subs = lines[i].Split('\t');
                Panel panel = new(subs[0], subs[1], subs[2], int.Parse(subs[3]));
                panels.Add(panel);
            }
        }
    }
}
