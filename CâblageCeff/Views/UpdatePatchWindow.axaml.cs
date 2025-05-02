using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CâblageCeff.Models;
using CâblageCeff.ViewModels;
using System;

namespace CâblageCeff;

public partial class UpdatePatchWindow : Window
{
    public Models.Patch? Patch { get; set; }
    public UpdatePatchWindow(Models.Patch patch)
    {
        InitializeComponent();
        Patch = patch;
        DataContext = new UpdatePatchWindowViewModel(patch, this);
    }
}