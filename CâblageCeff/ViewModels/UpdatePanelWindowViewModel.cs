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
    public partial class UpdatePanelWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private Models.Panel? panel;
        
        private Window? window;
        public UpdatePanelWindowViewModel(Models.Panel p, Window updatePanelWindow)
        {
            panel = p;
            window = updatePanelWindow;
        }

        [RelayCommand]
        private void Save()
        {
            window?.Close();
        }

        [RelayCommand]
        private void Cancel()
        {
            window?.Close();
        }
    }
}
