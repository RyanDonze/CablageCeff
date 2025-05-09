using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CâblageCeff.Models;
using CâblageCeff.ViewModels;
using System;
using System.Threading.Tasks;

namespace CâblageCeff;

public partial class PatchWindow : Window
{
    public Models.Panel? Panel { get; set; }
    public PatchWindow(Models.Panel panel,Utils.DbModel context)
    {
        InitializeComponent();
        Panel = panel;
        DataContext = new PatchWindowViewModel(panel ,this,context);

        var vm = DataContext as PatchWindowViewModel;
        if (vm != null)
        {
            //vm.ShowUpdatePatchDialog = ShowUpdatePatchDialog;
        }
    }

    //private void RegisterInteractions(object? sender, EventArgs e)
    //{
        
    //}

    //private async Task<Models.Patch> ShowUpdatePatchDialog(Models.Patch patch)
    //{
    //    if (patch == null)
    //        patch = new Models.Patch("", "", "", "", "");
    //    //var patchDialog = new UpdatePatchWindow(patch);
    //    await patchDialog.ShowDialog(this);
    //    if (patchDialog.Patch == null || patchDialog.Patch != patch)
    //        return null;
    //    return patchDialog.Patch;
    //}
}
