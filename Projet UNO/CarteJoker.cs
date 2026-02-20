using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    public class CarteJoker : Carte
    {
        public CarteJoker(string type) : base("Noir", type) { }

        public override bool PeutJouerSur(Carte actuelle)
        {
            return true; // toujours jouable
        }
    }
}
