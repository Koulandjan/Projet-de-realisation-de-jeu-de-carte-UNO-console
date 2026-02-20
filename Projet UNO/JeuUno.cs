using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    public class JeuUno
    {
        private List<Joueur> joueurs = new List<Joueur>();
        private  List<Carte> pioche = new List<Carte>();
        private   Carte carteActuelle;
        public List<Joueur> Joueurs
        {
            get { return joueurs; }     
            set { joueurs = value; }    
        }

        public List<Carte> Pioche
        {
            get { return pioche; }
            set { pioche = value; }
        }

        public Carte CarteActuelle
        {
            get { return carteActuelle; }
            set { carteActuelle = value; }
        }

        Random rand = new Random();

        public void Demarrer()
        {
            // Saisie des joueurs
            // Console.Write("Noms des joueurs (séparés par espaces) : ");
            Console.Write("Combien de joueurs ? ");
            int nombreDeJoueur = int.Parse(Console.ReadLine());
           
                for (int i = 1; i <= nombreDeJoueur; i++)
                {

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("==============================================");
                Console.WriteLine($"           🎮 SAISIE DU JOUEUR {i} 🎮");
                Console.WriteLine("==============================================");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" Entrez le nom du joueur : ");
                Console.ResetColor();

                var nom = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n✔ {nom} enregistré !");
                Console.ResetColor();

           

                Console.Write("Chargement du joueur suivant");
                for (int dot = 0; dot < 3; dot++)
                {
                    Thread.Sleep(300);
                    Console.Write(".");
                }

                Thread.Sleep(500);

                joueurs.Add(new Joueur(nom));
                }
            Console.Clear();

            void AfficherDebutJeu()
            {
                Console.Clear();
                // Animation de chargement
                Console.Write("Préparation du jeu");
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(400);  // 0.4 sec
                    Console.Write(".");
                }

                Thread.Sleep(1000); // Laisser 1 seconde avant de continuer
            }


            //Transition pour gerer l'affichage des tours 
             void AfficherTransition(string prochainJoueur)
            {
                string message = $"Passage au joueur {prochainJoueur}";

                for (int i = 0; i < 3; i++)
                {
                    Console.Clear();
                    Console.Write(message);
                    Console.Write(new string('.', i + 1));
                    Thread.Sleep(200); // 0.5 sec
                }
            }


            // Créer le paquet
            CreerPaquet();

            // Distribuer 7 cartes
            foreach (var j in joueurs)
                for (int i = 0; i < 7; i++)
                    j.Ajouter(Piocher());

            // Première carte (pas noir)
            do carteActuelle = Piocher();
            while (carteActuelle.Couleur == "Noir");

            // Boucle de jeu
            int tour = 0;
            while (true)
            {

                // Effets avant l'écran du joueur actuel
                if (tour > 0) // éviter d'afficher l'effet au premier tour
                    AfficherTransition(joueurs[tour].Nom);

                Console.Clear();

                var j = joueurs[tour];
                Console.WriteLine($"\n--- À {j.Nom} ---\n");
                Console.WriteLine($"Carte actuelle : {carteActuelle} \n");
                j.AfficherMain();
               
                // Gestion +2
                if (carteActuelle.Valeur == "+2" && !j.PeutJouer(carteActuelle))
                {
                    Console.WriteLine($"{j.Nom}, vous subissez +2 !");
                    j.Ajouter(Piocher());
                    j.Ajouter(Piocher());
                    tour = (tour + 1) % joueurs.Count;
                    continue;
                }

                // Choisir action
                string saisie;
                Carte choix = null;
                bool ok = false;
                while (!ok)
                {
                    Console.Write("Jouez une carte (ex: Vert2) ou 'pioche' : ");
                    saisie = Console.ReadLine();

                    if (saisie == "pioche")
                    {
                        j.Ajouter(Piocher());
                        ok = true;
                        break;
                    }

                    choix = j.Main.Find(c => c.ToString() == saisie);
                    if (choix == null || !choix.PeutJouerSur(carteActuelle))
                    {
                        Console.WriteLine("Carte invalide.");
                        continue;
                    }

                    j.Main.Remove(choix);
                    carteActuelle = choix;
                    ok = true;

                    // Choix couleur si Joker
                    if (choix.Couleur == "Noir")
                    {
                        Console.Write("Couleur ? (R/B/V/J) : ");
                        string c = Console.ReadLine()?.ToUpper() switch
                        {
                            "R" => "Rouge",
                            "B" => "Bleu",
                            "V" => "Vert",
                            "J" => "Jaune",
                            _ => "Rouge"
                        };
                        carteActuelle = new CarteSpeciale(c, choix.Valeur);
                    }
                }

                // Victoire ?
                if (j.Main.Count == 0)
                {
                    Console.WriteLine($"\n🎉 {j.Nom} gagne !");
                    break;
                }

                tour = (tour + 1) % joueurs.Count;
            }
        }



       public void CreerPaquet()
        {
            string[] cols = { "Rouge", "Bleu", "Vert", "Jaune" };
            foreach (string c in cols)
            {
                pioche.Add(new CarteNumero(c, 0));
                for (int i = 1; i <= 9; i++)
                {
                    pioche.Add(new CarteNumero(c, i));
                    pioche.Add(new CarteNumero(c, i));
                }
                foreach (string s in new[] { "+2", "Inverser", "Passe" })
                {
                    pioche.Add(new CarteSpeciale(c, s));
                    pioche.Add(new CarteSpeciale(c, s));
                }
            }
            for (int i = 0; i < 4; i++)
            {
                pioche.Add(new CarteJoker("Joker"));
                pioche.Add(new CarteJoker("+4"));
            }
            // Mélanger
            pioche = pioche.OrderBy(x => rand.Next()).ToList();
        }

      public  Carte Piocher()
        {
            if (pioche.Count == 0) return new CarteNumero("Rouge", 0);
            var c = pioche[0];
            pioche.RemoveAt(0);
            return c;
        }
    }
}
