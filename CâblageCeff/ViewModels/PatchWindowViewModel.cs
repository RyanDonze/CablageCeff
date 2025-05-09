using Avalonia.Controls;
using Avalonia.Controls.Converters;
using CâblageCeff.Models;
using CâblageCeff.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CâblageCeff.ViewModels
{
    public partial class PatchWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private Models.Panel? panel;

        [ObservableProperty]
        private List<Patch>? patchs;

        [ObservableProperty]
        private string? patchCount;

        [ObservableProperty]
        private string? panelName;

        private readonly Window? window;

        Patch? patch;
        DbModel context;
        // UI Dialogs
        public delegate Task<Patch> ShowUpdatePatchDialogFunc(Patch patch);
        public ShowUpdatePatchDialogFunc? ShowUpdatePatchDialog { get; set; }
        public PatchWindowViewModel(Models.Panel p, Window patchWindow,DbModel contextPanel)
        {
            
            contextPanel.Database.EnsureCreated();
            context = contextPanel;
            panel = p;
            panelName = $"{p.Batiment}.{p.Emplacement}.{p.Nom} Patchs";
            window = patchWindow;

            if (contextPanel.Patchs?.Where(p => p.PanelId == panel.PanelId).Count() == 0)
            {
                int e = 1;
                for (int i = 2; i <= panel.NbrPort + 1; i++)
                {
                    patch = new Patch(e.ToString(), "", "", "", "");
                    panel.Patchs?.Add(patch);
                    contextPanel.Patchs.Add(patch);
                    contextPanel.SaveChanges();
                    e++;
                }
            }
                Patchs = contextPanel.Patchs?.Where(p => p.PanelId == panel.PanelId).ToList();
                PatchCount = $"{Patchs?.Count} patch(s)";
        }

        [RelayCommand]
        private async Task EditPatch(Object c)
        {
            var patch = c as Patch;
            if (patch == null || ShowUpdatePatchDialog == null)
                return;
            var result = await ShowUpdatePatchDialog(patch);
            if (result != null)
            {
                
                patchs[patchs.IndexOf(patch)] = result;
                context.Patchs.Update(result);
                context.SaveChanges();
                Patchs = context.Patchs?.Where(p => p.PanelId == panel.PanelId).ToList();
                PatchCount = $"{Patchs?.Count} patch panel(s)";
            }
        }

        [RelayCommand]
        private void Quitter()
        {
            window?.Close();
        }

        [RelayCommand]
        private void DeletePatch(IList list)
        {
          
                var patchsToRemove = new Patch?[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    patchsToRemove[i] = list[i] as Patch;
                }
                var patchs = Patchs?.ToList();
                foreach (var c in patchsToRemove)
                {
                    if (c != null)
                    {
                        patchs[patchs.IndexOf(c)].Type = null;
                        patchs[patchs.IndexOf(c)].Destination = null;
                        patchs[patchs.IndexOf(c)].Description = null;
                        context.Patchs.Update(patchs[patchs.IndexOf(c)]);
                        context.SaveChanges();
                    }
                }
                Patchs = context.Patchs?.Where(p => p.PanelId == panel.PanelId).ToList();
                PatchCount = $"{Patchs?.Count} patch(s)";
            
        }
    }
}

