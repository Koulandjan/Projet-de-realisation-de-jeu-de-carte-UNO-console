using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    public class CarteNumero : Carte
    {
        public CarteNumero(string couleur, int numero)
            : base(couleur, numero.ToString()) { }

        public override bool PeutJouerSur(Carte actuelle)
        {
            return Couleur == actuelle.Couleur || Valeur == actuelle.Valeur;
        }
    }
}
