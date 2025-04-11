using Avalonia.Controls;
using CâblageCeff.Models;
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

        private async Task ShowPatchDialog(Models.Panel panel)
        {
            if (panel == null)
                return;
            var contactDialog = new PatchWindow(panel);
            await contactDialog.ShowDialog(this);
        }
    }
}