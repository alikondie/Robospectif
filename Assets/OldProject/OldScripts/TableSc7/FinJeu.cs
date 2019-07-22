using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FinJeu : MonoBehaviour
{
    [SerializeField] GameObject[] zones;
    [SerializeField] Text text;

    private string joueur;

    // Start is called before the first frame update
    void Start()
    {
        if (Partie.Langue == "FR")
        {
            text.text = "Fin de la partie";
            joueur = "Joueur ";
        }
        else
        {
            text.text = "Game over";
            joueur = "Player ";
        }

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                zones[i].transform.GetChild(j).gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < Partie.Joueurs.Count; i++)
        { 
            zones[Partie.Joueurs[i].Position].transform.GetChild(0).GetComponent<Text>().text = joueur + Partie.Joueurs[i].Numero;
            zones[Partie.Joueurs[i].Position].transform.GetChild(0).gameObject.SetActive(true);
            for (int j = 0; j < Partie.Joueurs[i].NbCouronnes; j++)
            {
                zones[Partie.Joueurs[i].Position].transform.GetChild(j + 1).gameObject.SetActive(true);
            }
        }

         string filePath = "donnees\\cartes_rejetees_le_" + DateTime.Now.ToString("dd-MM-yyyy") + "_a_" + DateTime.Now.ToString("hh") + "h" + DateTime.Now.ToString("mm") + "m" + DateTime.Now.ToString("ss") + "s" + ".csv";

         File.AppendAllText(filePath, SansHUD.data.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
