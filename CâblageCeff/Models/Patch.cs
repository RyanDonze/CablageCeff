using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CâblageCeff.Models
{
    public partial class Patch : ObservableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint PatchId { get; set; }

        [ObservableProperty]
        public partial string? Nom { get; set; }
        [ObservableProperty]
        public partial string? Type { get; set; }
        [ObservableProperty]
        public partial string? Emplacement { get; set; }
        [ObservableProperty]
        public partial string? Destination { get; set; }
        [ObservableProperty]
        public partial string? Description { get; set; }

        public Panel? Panel { get; set; }
        public uint PanelId { get; set; }


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
