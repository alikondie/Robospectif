using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class JoueurStatic
{
    private static int numero;
    private static int position;
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
    private static int nbCouronnes;
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
}
