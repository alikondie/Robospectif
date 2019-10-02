﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

////classe joueur, qui permet de stocker des variables globales d'une partie 

public class JoueurStatic
{
    private static int numero;
    private static int position;
    private static int nbJoueurs;
    private static Sprite loco;
    private static Sprite[] locomotions;
    private static Sprite dim;
    private static Sprite[] dimensions;
    private static Sprite equi1;
    private static Sprite equi2;
    private static Sprite equi3;
    private static Sprite[] equipements;
    private static Sprite[] robot;
    private static Sprite[] persos;
    private static bool[] persosChoisis;
    private static Sprite[] acteurs;
    private static Sprite actif;
    private static int nbCouronnes;
    private static string langue;
    private static string type;
    private static bool isPrive;
    private static bool isPublic;
    private static int usageCompteur;
    private static int societeCompteur;
    private static int planeteCompteur;
    private static int compteurVert;
    private static int compteurRouge;
#pragma warning disable CS0618 // Le type ou le membre est obsolète
    private static NetworkClient client;
#pragma warning restore CS0618 // Le type ou le membre est obsolète


    // Start is called before the first frame update
    void Start()
    {
        robot = new Sprite[5];
        persos = new Sprite[6];
        persosChoisis = new bool[] { false, false, false, false, false, false };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static int Numero
    {
        get => numero;
        set => numero = value;
    }

    public static int Position
    {
        get => position;
        set => position = value;
    }

    public static Sprite Loco
    {
        get => loco;
        set => loco = value;
    }

    public static Sprite Dim
    {
        get => dim;
        set => dim = value;
    }

    public static Sprite Equi1
    {
        get => equi1;
        set => equi1 = value;
    }

    public static Sprite Equi2
    {
        get => equi2;
        set => equi2 = value;
    }

    public static Sprite Equi3
    {
        get => equi3;
        set => equi3 = value;
    }

    public static int NbCouronnes
    {
        get => nbCouronnes;
        set => nbCouronnes = value;
    }
    public static Sprite[] Persos { get => persos; set => persos = value; }
    public static Sprite[] Robot { get => robot; set => robot = value; }
    public static bool[] PersosChoisis { get => persosChoisis; set => persosChoisis = value; }
    public static NetworkClient Client { get => client; set => client = value; }
    public static Sprite[] Locomotions { get => locomotions; set => locomotions = value; }
    public static Sprite[] Dimensions { get => dimensions; set => dimensions = value; }
    public static Sprite[] Equipements { get => equipements; set => equipements = value; }
    public static string Langue { get => langue; set => langue = value; }
    public static string Type { get => type; set => type = value; }
    public static Sprite[] Acteurs { get => acteurs; set => acteurs = value; }
    public static Sprite Actif { get => actif; set => actif = value; }
    public static int NbJoueurs { get => nbJoueurs; set => nbJoueurs = value; }
    public static bool IsPrive { get => isPrive; set => isPrive = value; }
    public static bool IsPublic { get => isPublic; set => isPublic = value; }
    public static int UsageCompteur { get => usageCompteur; set => usageCompteur = value; }
    public static int SocieteCompteur { get => societeCompteur; set => societeCompteur = value; }
    public static int PlaneteCompteur { get => planeteCompteur; set => planeteCompteur = value; }
    public static int CompteurVert { get => compteurVert; set => compteurVert = value; }
    public static int CompteurRouge { get => compteurRouge; set => compteurRouge = value; }
}
