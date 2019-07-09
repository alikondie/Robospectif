using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur
{
    private int numero;
    private int position;
    private Sprite dim;
    private Sprite[] dimensions;
    private Sprite loco;
    private Sprite[] locomotions;
    private Sprite equi1;
    private Sprite equi2;
    private Sprite equi3;
    private Sprite[] equipements;
    private Sprite[] persos;
    private bool[] persosChoisis;
    private bool isPrive;
    private bool isPublic;
    private int nbCouronnes;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int Numero
    {
        get => numero;
        set => numero = value;
    }

    public int Position
    {
        get => position;
        set => position = value;
    }

    public int NbCouronnes
    {
        get => nbCouronnes;
        set => nbCouronnes = value;
    }
    public Sprite[] Persos { get => persos; set => persos = value; }
    public bool[] PersosChoisis { get => persosChoisis; set => persosChoisis = value; }
    public Sprite Dim { get => dim; set => dim = value; }
    public Sprite Loco { get => loco; set => loco = value; }
    public Sprite Equi1 { get => equi1; set => equi1 = value; }
    public Sprite Equi2 { get => equi2; set => equi2 = value; }
    public Sprite Equi3 { get => equi3; set => equi3 = value; }
    public Sprite[] Dimensions { get => dimensions; set => dimensions = value; }
    public Sprite[] Locomotions { get => locomotions; set => locomotions = value; }
    public Sprite[] Equipements { get => equipements; set => equipements = value; }
    public bool IsPrive { get => isPrive; set => isPrive = value; }
    public bool IsPublic { get => isPublic; set => isPublic = value; }
}
