using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tour 
{
    private static Sprite[] persosDebat;
    private static Sprite[,] jetonsDebat;
    private static bool[,] activesDebat;
    private static int[,] zonesDebat;
    private static int[] piles;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
