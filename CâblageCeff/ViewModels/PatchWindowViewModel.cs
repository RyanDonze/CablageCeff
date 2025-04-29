using Avalonia.Controls;
using CâblageCeff.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace CâblageCeff.ViewModels
{
    public partial class PatchWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private Models.Panel? panel;

        [ObservableProperty]
        private string? panelName;

        [ObservableProperty]
        private string? panelNbr;

        private Window? window;

        public PatchWindowViewModel(Models.Panel p, Window patchWindow)
        {
            panel = p;
            panelName = $"{p.Batiment}.{p.Emplacement}.{p.Nom} Patchs";
            panelNbr = $"{p.NbrPort} patch(s)";
            window = patchWindow;
        }

        [RelayCommand]
        private void Quitter()
        {
            window?.Close();
        }
    }
}

