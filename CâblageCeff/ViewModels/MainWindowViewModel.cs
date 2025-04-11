using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Platform;
using CâblageCeff.Models;
using CommunityToolkit.Mvvm.ComponentModel;

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
