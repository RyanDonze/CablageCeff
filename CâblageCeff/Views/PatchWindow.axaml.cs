using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CâblageCeff.Models;
using CâblageCeff.ViewModels;
using System;

namespace CâblageCeff;

public partial class PatchWindow : Window
{
    public Models.Panel? Panel { get; set; }
    public PatchWindow(Models.Panel panel)
    {
        InitializeComponent();
        Panel = panel;
        DataContext = new PatchWindowViewModel(panel ,this);
    }
}
