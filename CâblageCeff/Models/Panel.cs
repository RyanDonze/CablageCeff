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
        public string? nom; // public partial
        [ObservableProperty]
        public string? batiment;
        [ObservableProperty]
        public string? emplacement;
        [ObservableProperty]
        public int? nbrPort; //Collection

        public Panel(string? nom, string? batiment, string? emplacement, int? nbrPort)
        {
            Nom = nom;
            Batiment = batiment;
            Emplacement = emplacement;
            NbrPort = nbrPort;
        }
    }
}
