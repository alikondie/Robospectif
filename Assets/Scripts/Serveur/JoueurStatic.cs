using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoueurStatic
{
    private static int numero;
    private static int position;
    private static Sprite loco;
    private static Sprite dim;
    private static Sprite equi1;
    private static Sprite equi2;
    private static Sprite equi3;
    private static Sprite perso1;
    private static Sprite perso2;
    private static Sprite perso3;
    private static Sprite perso4;
    private static Sprite perso5;
    private static Sprite perso6;
    private static bool perso1Choisi;
    private static bool perso2Choisi;
    private static bool perso3Choisi;
    private static bool perso4Choisi;
    private static bool perso5Choisi;
    private static bool perso6Choisi;
    private static int nbCouronnes;


    // Start is called before the first frame update
    void Start()
    {

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

    public static Sprite Perso1
    {
        get => perso1;
        set => perso1 = value;
    }

    public static Sprite Perso2
    {
        get => perso2;
        set => perso2 = value;
    }

    public static Sprite Perso3
    {
        get => perso3;
        set => perso3 = value;
    }

    public static Sprite Perso4
    {
        get => perso4;
        set => perso4 = value;
    }

    public static Sprite Perso5
    {
        get => perso5;
        set => perso5 = value;
    }

    public static Sprite Perso6
    {
        get => perso6;
        set => perso6 = value;
    }

    public static bool Perso1Choisi
    {
        get => perso1Choisi;
        set => perso1Choisi = value;
    }

    public static bool Perso2Choisi
    {
        get => perso2Choisi;
        set => perso2Choisi = value;
    }

    public static bool Perso3Choisi
    {
        get => perso3Choisi;
        set => perso3Choisi = value;
    }

    public static bool Perso4Choisi
    {
        get => perso4Choisi;
        set => perso4Choisi = value;
    }

    public static bool Perso5Choisi
    {
        get => perso5Choisi;
        set => perso5Choisi = value;
    }

    public static bool Perso6Choisi
    {
        get => perso6Choisi;
        set => perso6Choisi = value;
    }

    public static int NbCouronnes
    {
        get => nbCouronnes;
        set => nbCouronnes = value;
    }
}
