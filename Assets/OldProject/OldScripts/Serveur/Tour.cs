using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tour 
{
    private static Sprite[] persosDebat;
    private static Sprite[,] jetonsDebat;
    private static bool[,] activesDebat;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
