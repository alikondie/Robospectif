using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partie
{
    private static string type;
    private static string langue;
    private static List<Joueur> joueurs;
    private static int tour;
    private static int joueurCourant;
    private static int nbJetonsVert;
    private static int nbJetonsRouge;
    private static int[] positions;

    public static void Initialize()
    {
        tour = 1;
        nbJetonsVert = 36;
        nbJetonsRouge = 12;
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

    public static int NbJetonsVert
    {
        get => nbJetonsVert;
        set => nbJetonsVert = value;
    }

    public static int NbJetonsRouge
    {
        get => nbJetonsRouge;
        set => nbJetonsRouge = value;
    }

    public static int JoueurCourant
    {
        get => joueurCourant;
        set => joueurCourant = value;
    }

    public static int[] Positions
    {
        get => positions;
        set => positions = value;
    }
    public static string Type { get => type; set => type = value; }
    public static string Langue { get => langue; set => langue = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
