using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partie
{

    private static List<Joueur> joueurs;
    private static int tour;
    private static int joueurCourant;

    public static void Initialize()
    {
        tour = 1;
        joueurs = new List<Joueur>();
    }

    public static void AddPlayer(Joueur j)
    {
        joueurs.Add(j);
    }

    public static List<Joueur> Joueurs
    {
        get => joueurs;
        set => joueurs = value;
    }

    public static int Tour
    {
        get => tour;
        set => tour = value;
    }

    public static int JoueurCourant
    {
        get => joueurCourant;
        set => joueurCourant = value;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
