using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur
{
    private int numero;
    private int position;
    private Sprite loco;
    private Sprite dim;
    private Sprite equi1;
    private Sprite equi2;
    private Sprite equi3;
    private Sprite perso1;
    private Sprite perso2;
    private Sprite perso3;
    private Sprite perso4;
    private Sprite perso5;
    private Sprite perso6;
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

    public Sprite Loco
    {
        get => loco;
        set => loco = value;
    }

    public Sprite Dim
    {
        get => dim;
        set => dim = value;
    }

    public Sprite Equi1
    {
        get => equi1;
        set => equi1 = value;
    }

    public Sprite Equi2
    {
        get => equi2;
        set => equi2 = value;
    }

    public Sprite Equi3
    {
        get => equi3;
        set => equi3 = value;
    }

    public Sprite Perso1
    {
        get => perso1;
        set => perso1 = value;
    }

    public Sprite Perso2
    {
        get => perso2;
        set => perso2 = value;
    }

    public Sprite Perso3
    {
        get => perso3;
        set => perso3 = value;
    }

    public Sprite Perso4
    {
        get => perso4;
        set => perso4 = value;
    }

    public Sprite Perso5
    {
        get => perso5;
        set => perso5 = value;
    }

    public Sprite Perso6
    {
        get => perso6;
        set => perso6 = value;
    }

    public int NbCouronnes
    {
        get => nbCouronnes;
        set => nbCouronnes = value;
    }
}
