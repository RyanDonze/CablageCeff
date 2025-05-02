using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CâblageCeff.ViewModels
{
    public partial class UpdatePatchWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private Models.Patch? patch;
        
        private Window? windowPatch;
        public UpdatePatchWindowViewModel(Models.Patch p, Window updatePanelWindow)
        {
            patch = p;
            windowPatch = updatePanelWindow;
        }

        [RelayCommand]
        private void Save()
        {
            windowPatch?.Close();
        }

        [RelayCommand]
        private void Cancel()
        {
            windowPatch?.Close();
        }
    }
}
