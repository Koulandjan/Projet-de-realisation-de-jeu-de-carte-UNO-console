using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    public class CarteSpeciale : Carte
    {
        public CarteSpeciale(string couleur, string type) : base(couleur, type) { }

        public override bool PeutJouerSur(Carte actuelle)
        {
            return Couleur == actuelle.Couleur || Valeur == actuelle.Valeur;
        }
    }
}
