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
        [ObservableProperty]
        private Patch editablePatch = new Patch("", "", "", "", "");
        private List<Patch>? patchsToBeEdited;
 
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

        [ObservableProperty]
        private bool patchScreenIsVisible = true;

        [ObservableProperty]
        private bool updatePatchScreenIsVisible = false;

        [ObservableProperty]
        private bool isOK = true;

        [ObservableProperty]
        private bool isOKMultiple = false;

        [RelayCommand]
        private Task EditPatch(Object c)
        {
            var patch = c as Patch;
            if (patch == null)
                return Task.CompletedTask;
            EditablePatch = patch;

            IsOK = true;
            IsOKMultiple = false;
            PatchScreenIsVisible = false;
            UpdatePatchScreenIsVisible = true;
            return Task.CompletedTask;
        }

        [RelayCommand]
        private Task EditPatchs(IList list)
        {
            if (list.Count == 0)
                return Task.CompletedTask;
            var patchsToEdit = new Patch?[list.Count];
            for (int i = 0; i < list.Count; i++)
            {

                patchsToEdit[i] = list[i] as Patch;
            }
            EditablePatch = patchsToEdit[0]!;
            PatchScreenIsVisible = false;
            UpdatePatchScreenIsVisible = true;
            IsOK = false;
            IsOKMultiple = true;
            foreach (var c in patchsToEdit)
            {
                if (c != null)
                {
                    Patchs![Patchs.IndexOf(c)].Type = EditablePatch.Type;
                    Patchs[Patchs.IndexOf(c)].Emplacement = EditablePatch.Emplacement;
                    Patchs[Patchs.IndexOf(c)].Destination = EditablePatch.Destination;
                    Patchs[Patchs.IndexOf(c)].Description = EditablePatch.Description;
                    context.Patchs.Update(EditablePatch);
                    context.SaveChanges();
                }
            }
            patchsToBeEdited = patchsToEdit.ToList()!;
            Patchs = context.Patchs?.Where(p => p.PanelId == Panel!.PanelId).ToList();
             PatchCount = $"{Patchs?.Count} patch panel(s)";
            return Task.CompletedTask;
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
                        patchs![patchs.IndexOf(c)].Type = null;
                        patchs[patchs.IndexOf(c)].Destination = null;
                        patchs[patchs.IndexOf(c)].Description = null;
                        context.Patchs.Update(patchs[patchs.IndexOf(c)]);
                        context.SaveChanges();
                    }
                }
             
            Patchs = context.Patchs?.Where(p => p.PanelId == Panel!.PanelId).ToList();
            PatchCount = $"{Patchs?.Count} patch(s)";
        }

        //--------------------------------------------------------------------------------------------------

        [RelayCommand]
        void Cancel()
        {
            context.Patchs.Update(EditablePatch);
            context.SaveChanges();
            PatchScreenIsVisible = true;
            UpdatePatchScreenIsVisible = false;
        }

        [RelayCommand]
        void CancelMultiple()
        {
            PatchScreenIsVisible = true;
            UpdatePatchScreenIsVisible = false;
            if(patchsToBeEdited == null)
                return;
            foreach (var c in patchsToBeEdited)
            {
                if (c != null)
                {
                    Patchs![Patchs.IndexOf(c)].Type = EditablePatch.Type;
                    Patchs[Patchs.IndexOf(c)].Emplacement = EditablePatch.Emplacement;
                    Patchs[Patchs.IndexOf(c)].Destination = EditablePatch.Destination;
                    Patchs[Patchs.IndexOf(c)].Description = EditablePatch.Description;
                    context.Patchs.Update(EditablePatch);
                    context.SaveChanges();
                }
            }
        }
    }
}

