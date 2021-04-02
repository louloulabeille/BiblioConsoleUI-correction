using System;
using System.Collections.Generic;

namespace Bibliotheque.BOL
{
    public partial class Pret
    {
        public string IdAdherent { get; set; }
        public int IdExemplaire { get; set; }
        public DateTime DateEmprunt { get; set; }
        public DateTime? DateRetour { get; set; }

        public virtual Adherent IdAdherentNavigation { get; set; }
        public virtual Exemplaire IdExemplaireNavigation { get; set; }
    }
}
