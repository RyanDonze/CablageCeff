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
                vm.ShowUpdatePanelDialog = ShowUpdatePanelDialog;
            }
        }

        private async Task ShowPatchDialog(Models.Panel panel,Utils.DbModel context)
        {
            if (panel == null)
                return;
            var contactDialog = new PatchWindow(panel,context);
            await contactDialog.ShowDialog(this);
        }

        private async Task<Models.Panel?> ShowUpdatePanelDialog(Models.Panel panel)
        {
            if (panel == null)
                panel = new Models.Panel("","","",0);
            var panelDialog = new UpdatePanelWindow(panel);
            await panelDialog.ShowDialog(this);
            if (panelDialog.Panel == null || panelDialog.Panel != panel)
                return null;
            return panelDialog.Panel;
        }
    }
}