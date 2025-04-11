using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CâblageCeff.Models
{
    public partial class Patch : ObservableObject
    {
        [ObservableProperty]
        private string? nom;
        [ObservableProperty]
        private string? nomPanel;
        [ObservableProperty]
        private string? type;
        [ObservableProperty]
        private string? emplacement;
        [ObservableProperty]
        private string? destination;
        [ObservableProperty]
        private string? description;

        public Patch(string? nom, string? nomPanel, string? type, string? emplacement, string? destination, string? description)
        {
            Nom = nom;
            NomPanel = nomPanel;
            Type = type;
            Emplacement = emplacement;
            Destination = destination;
            Description = description;
        }
    }
}
