using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CâblageCeff.Models;
using CâblageCeff.ViewModels;

namespace CâblageCeff;

public partial class UpdatePanelWindow : Window
{
    public Models.Panel? Panel { get; set; }
    public UpdatePanelWindow(Models.Panel panel)
    {
        InitializeComponent();
        Panel = panel;
        DataContext = new UpdatePanelWindowViewModel(panel, this);
    }
}