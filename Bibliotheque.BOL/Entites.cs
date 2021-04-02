using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bibliotheque.BOL
{
    public partial class Livre
    {
        public override bool Equals(object obj)
        {
            Livre livre = obj as Livre;
            return (livre == null ? false : livre.Isbn == this.Isbn);
        }
        public override int GetHashCode()
        {
            return (this.Isbn == null ? 0 : this.Isbn.GetHashCode());
        }
    }

    [MetadataType(typeof(AdherentMetaData))]
    public partial class Adherent
    {

        public bool PeutEmprunter()
        {
            int nombrePretsEnCours = 0;
            foreach (Pret item in this.Prets)
            {
                if (!item.DateRetour.HasValue || item.DateRetour.Value == DateTime.MinValue)
                    nombrePretsEnCours++;
            }
            if (nombrePretsEnCours < 3) return true; else return false;
        }

        public bool IsValid()
        {
            AssociatedMetadataTypeTypeDescriptionProvider assoc =
            new AssociatedMetadataTypeTypeDescriptionProvider(typeof(Adherent), typeof(AdherentMetaData));

            TypeDescriptor.AddProviderTransparent(assoc, typeof(Adherent));

            var context = new ValidationContext(this, serviceProvider: null, items: null);
            List<ValidationResult> results = new List<ValidationResult>();

            Validator.TryValidateObject(this, context, results, true);
            if (results.Count == 0) return true; else return false;



        }

    }

    public sealed class AdherentMetaData
    {
        [Display(Name = "Identifiant", Description = "L'identifiant de l'adhérent")]
        [Required(ErrorMessage = "L'identifiant de l'adhérent est requis")]
        public string IdAdherent;
        [StringLength(50, ErrorMessage = "Longueur invalide", MinimumLength = 2)]
        [Required(ErrorMessage = "Le nom de l'adhérent est requis")]
        public string NomAdherent;
        [StringLength(50, ErrorMessage = "Longueur invalide", MinimumLength = 2)]
        public string PrenomAdherent;

    }

    public partial class Exemplaire
    {

    }
    [MetadataType(typeof(PretMetaData))]
    public partial class Pret
    {

    }
    public sealed class PretMetaData
    {


    }
}
