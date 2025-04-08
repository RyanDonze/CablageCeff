using Avalonia.Controls;
using CâblageCeff.ViewModels;
using System;
using System.Threading.Tasks;

namespace CâblageCeff.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RegisterInteractions(object? sender, EventArgs e)
        {
            var vm = DataContext as MainWindowViewModel;
            if (vm != null)
            {
                vm.ShowPatchDialog = ShowPatchDialog;
            }
        }
        private async Task ShowPatchDialog()
        {
            var contactDialog = new PatchWindow();
            await contactDialog.ShowDialog(this);
        }
    }
}