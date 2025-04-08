using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CâblageCeff.Models
{
    public partial class Panel : ObservableObject
    {
        [ObservableProperty]
        private string? nom;
        [ObservableProperty]
        private string? batiment;
        [ObservableProperty]
        private string? emplacement;
        [ObservableProperty]
        private int? nbrPort;

        public Panel(string? nom, string? batiment, string? emplacement, int? nbrPort)
        {
            Nom = nom;
            Batiment = batiment;
            Emplacement = emplacement;
            NbrPort = nbrPort;
        }
    }
}
