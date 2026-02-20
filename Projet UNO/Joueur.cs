using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    public class Joueur
    {
        // Attributs privés
        private string nom;
        private List<Carte> main;

        // Propriétés publiques pour accéder aux attributs
        public string Nom
        {
            get { return nom; }      
            set { nom = value; }     
        }

        public List<Carte> Main
        {
            get { return main; }     
            set { main = value; }    
        }

        // Constructeur
        public Joueur(string nom)
        {
            this.nom = nom;
            this.main = new List<Carte>();
        }

        // Ajouter une carte à la main
        public void Ajouter(Carte c)
        {
            main.Add(c);
        }

        // Afficher la main
        public void AfficherMain()
        {
            Console.WriteLine($"{nom}, vos cartes : [{string.Join(" ", main)}]");
        }

        // Vérifie si le joueur peut jouer sur la carte actuelle
        public bool PeutJouer(Carte actuelle)
        {
            return main.Any(c => c.PeutJouerSur(actuelle));
        }
    }

}
