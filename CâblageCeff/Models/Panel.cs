using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CâblageCeff.Models
{
    public partial class Panel : ObservableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint PanelId { get; set; }

        [ObservableProperty]
        public partial string? Nom { get; set; }
        [ObservableProperty]
        public partial string? Batiment { get; set; }
        [ObservableProperty]
        public partial string? Emplacement { get; set; }
        [ObservableProperty]
        public partial int? NbrPort { get; set; }

        public ObservableCollection<Patch>? Patchs { get; set; }

        public Panel(string? nom, string? batiment, string? emplacement, int? nbrPort)
        {
            Nom = nom;
            Batiment = batiment;
            Emplacement = emplacement;
            NbrPort = nbrPort;
            Patchs = new();
        }
    }
}
