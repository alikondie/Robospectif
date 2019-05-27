using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Equipement : MonoBehaviour
{
    public static bool[] selection;
    public static int indice;
    public Image image1;
    public Image image2;
    public Image image3;

    // Start is called before the first frame update
    void Start()
    {
        selection = new bool[6];
        selection[0] = true;
        selection[1] = true;
        selection[2] = true;
        selection[3] = false;
        selection[4] = false;
        selection[5] = false;
        indice = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static int TraiteIndice(int i)
    {
        indice = i;
        selection[indice] = false;
        bool ok = false;
        while (!ok)
        {
            indice = (indice + 1) % 6;
            if (!selection[indice])
            {
                ok = true;
            }
        }
        selection[indice] = true;
        return indice;
    }
}
