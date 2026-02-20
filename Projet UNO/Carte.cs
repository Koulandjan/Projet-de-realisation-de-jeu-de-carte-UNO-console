using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 public abstract class Carte
{
    // Attributs privés
    private string couleur;
    private string valeur;

    // Propriétés publiques
    public string Couleur
    {
        get { return couleur; }
        protected set { couleur = value; } 
    }

    public string Valeur
    {
        get { return valeur; }
        protected set { valeur = value; } 
    }

    // Constructeur
    public Carte(string couleur, string valeur)
    {
        this.couleur = couleur;
        this.valeur = valeur;
    }

    // Méthode abstraite : implémentée dans les classes filles
    public abstract bool PeutJouerSur(Carte actuelle);

    // Affichage
    public override string ToString()
    {
        return $"{couleur}{valeur}";
    }
}



