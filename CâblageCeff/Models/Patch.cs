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
        public string? nom;
        [ObservableProperty]
        public string? type;
        [ObservableProperty]
        public string? emplacement;
        [ObservableProperty]
        public string? destination;
        [ObservableProperty]
        public string? description;

        public Patch(string? nom, string? type, string? emplacement, string? destination, string? description)
        {
            Nom = nom;
            Type = type;
            Emplacement = emplacement;
            Destination = destination;
            Description = description;
        }
    }
}
