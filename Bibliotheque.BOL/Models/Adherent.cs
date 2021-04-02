using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bibliotheque.BOL
{
    public partial class Adherent
    {
        public Adherent()
        {
            Prets = new HashSet<Pret>();
        }
        [Display(Name = "Identifiant", Description = "L'identifiant de l'adhérent")]
        [Required(ErrorMessage = "L'identifiant de l'adhérent est requis")]
        public string IdAdherent { get; set; }
        [StringLength(50, ErrorMessage = "Longueur invalide", MinimumLength = 2)]
        public string NomAdherent { get; set; }
        [StringLength(50, ErrorMessage = "Longueur invalide", MinimumLength = 2)]
        public string PrenomAdherent { get; set; }

        public virtual ICollection<Pret> Prets { get; set; }
    }
}
