using Avalonia.Controls;
using Avalonia.Controls.Converters;
using CâblageCeff.Models;
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

        ExcelPackage? pkg;
        ExcelWorksheet? ws;
        Patch? patch;

        [ObservableProperty]
        private Patch editablePatch = new Patch("", "", "", "", "");

        private List<Patch> patchsToBeEdited;

        // UI Dialogs
        //public delegate Task<Patch> ShowUpdatePatchDialogFunc(Patch patch);
        //public ShowUpdatePatchDialogFunc? ShowUpdatePatchDialog { get; set; }
        public PatchWindowViewModel(Models.Panel p, Window patchWindow)
        {
            int e = 1;
            panel = p;
            panelName = $"{p.Batiment}.{p.Emplacement}.{p.Nom} Patchs";
            window = patchWindow;

            List<Patch> patchs = [];
            List<string> subs = [];

            FileInfo file = new("Assets//Liste des patch panel et numéro.xlsx");
            file = new FileInfo(file.FullName);
            pkg = new ExcelPackage(file);
            if (pkg.Workbook.Worksheets.Count > 1)
            {
                while (ws == null) { 
                    for (int i = 1; i < pkg.Workbook.Worksheets.Count; i++)
                    {
                        if (pkg.Workbook.Worksheets[i].Name == panel.Nom)
                        {
                            ws = pkg.Workbook.Worksheets[i];
                            break;
                        }
                    }
                    if (ws == null)
                    {
                        pkg.Workbook.Worksheets.Add(panel.Nom);

                    }
                }
            }  
            else
              throw new InvalidOperationException("The Excel file contains no worksheets.");

            for (int i = 2; i <= panel.NbrPort + 1;i++)
            {
                
                for(int j = 1; j <= 5;j++)
                {
                    if(ws.Cells[i, j] == null)
                        break;
                    subs.Add(ws.Cells[i, j].Text);
                }
                patch = new Patch(e.ToString(), subs[1], subs[2], subs[3], subs[4]);
                patchs.Add(patch);
                subs.Clear();
                ws.Cells[i, 1].Value = e;
                e++;
            }

            Patchs = patchs;
            PatchCount = $"{Patchs.Count} patch(s)";
            pkg.Save();
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
        private async Task EditPatch(Object c)
        {
            var patch = c as Patch;
            if (patch == null)
                return;
            EditablePatch = patch;

            IsOK = true;
            IsOKMultiple = false;
            PatchScreenIsVisible = false;
            UpdatePatchScreenIsVisible = true;

            //if (patch == null || ShowUpdatePatchDialog == null)
            //    return;
            //var result = await ShowUpdatePatchDialog(patch);
            //if (result != null)
            //{
            //    var patchs = Patchs?.ToList();
            //    patchs[patchs.IndexOf(patch)] = result;
            //    Patchs = patchs;
            //    PatchCount = $"{Patchs?.Count} patch panel(s)";
            //}
        }

        [RelayCommand]
        private async Task EditPatchs(IList list)
        {
            if (list.Count == 0)
                return;
            var patchsToEdit = new Patch?[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                patchsToEdit[i] = list[i] as Patch;
            }
            var patchs = Patchs?.ToList();
            EditablePatch = patchsToEdit[0];
            PatchScreenIsVisible = false;
            UpdatePatchScreenIsVisible = true;
            IsOK = false;
            IsOKMultiple = true;
            foreach (var c in patchsToEdit)
            {
                if (c != null)
                {
                    patchs[patchs.IndexOf(c)].Type = EditablePatch.Type;
                    patchs[patchs.IndexOf(c)].Emplacement = EditablePatch.Emplacement;
                    patchs[patchs.IndexOf(c)].Destination = EditablePatch.Destination;
                    patchs[patchs.IndexOf(c)].Description = EditablePatch.Description;
                }
            }
            Patchs = patchs;
            patchsToBeEdited = patchsToEdit.ToList();
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
                    patchs[patchs.IndexOf(c)].Emplacement = null;
                    patchs[patchs.IndexOf(c)].Destination = null;
                    patchs[patchs.IndexOf(c)].Description = null;
                }
            }
            //Patchs = patchs;
            //PatchCount = $"{Patchs?.Count} patch(s)";
        }

        //--------------------------------------------------------------------------------------------------

        [RelayCommand]
        void Cancel()
        {
            PatchScreenIsVisible = true;
            UpdatePatchScreenIsVisible = false;
        }

        [RelayCommand]
        void CancelMultiple()
        {
            PatchScreenIsVisible = true;
            UpdatePatchScreenIsVisible = false;

            foreach (var c in patchsToBeEdited)
            {
                if (c != null)
                {
                    patchs[patchs.IndexOf(c)].Type = EditablePatch.Type;
                    patchs[patchs.IndexOf(c)].Emplacement = EditablePatch.Emplacement;
                    patchs[patchs.IndexOf(c)].Destination = EditablePatch.Destination;
                    patchs[patchs.IndexOf(c)].Description = EditablePatch.Description;
                }
            }
        }
    }
}

