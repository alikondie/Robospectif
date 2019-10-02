using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////classe permettant de stocker des variales globales d'un tour dans une partie
public class Tour 
{
    private static Sprite[] persosDebat;
    private static Sprite[,] jetonsDebat;
    private static bool[,] activesDebat;
    private static int[,] zonesDebat;
    private static int[] piles;
    private static int nbCartesPosees;
    private static bool[] listiscartesposees;

    public static Sprite[] PersosDebat
    {
        get => persosDebat;
        set => persosDebat = value;
    }

    public static Sprite[,] JetonsDebat
    {
        get => jetonsDebat;
        set => jetonsDebat = value;
    }

    public static bool[,] ActivesDebat
    {
        get => activesDebat;
        set => activesDebat = value;
    }

    public static int[,] ZonesDebat
    {
        get => zonesDebat;
        set => zonesDebat = value;
    }

    public static int[] Piles
    {
        get => piles;
        set => piles = value;
    }
    public static int NbCartesPosees { get => nbCartesPosees; set => nbCartesPosees = value; }

    public static bool[] Listiscartesposees
    {
        get => listiscartesposees;
        set => listiscartesposees = value;
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
