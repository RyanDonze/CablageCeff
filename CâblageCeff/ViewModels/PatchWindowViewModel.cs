using CommunityToolkit.Mvvm.Input;
using System;

namespace CâblageCeff.ViewModels
{
    public partial class PatchWindowViewModel : ViewModelBase
    {
        [RelayCommand]
        private void Quitter()
        {
            Environment.Exit(0);
        }
    }
}

