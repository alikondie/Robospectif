using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

////Script attaché au cartes équipement dans le canvas de choix des cartes (côté client).
////il permet d'initialiser les cartes sélectionnées par le joueur (cartes face au joueur)
public class Equipement : MonoBehaviour
{
    public static bool[] selection;
    public static int indice;
    [SerializeField] Image image1;
    [SerializeField] Image image2;
    [SerializeField] Image image3;

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
