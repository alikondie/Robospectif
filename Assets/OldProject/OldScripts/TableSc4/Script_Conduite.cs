using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_Conduite : MonoBehaviour
{
    // ---------- ATTRIBUTS ----------

    [SerializeField] Button button;

    // Curseur
    [SerializeField] Image barre;
    // Partie Attention Requise (TEXTE)
    [SerializeField] Image cadreTxt0;
    [SerializeField] Text texteNiv0;
    // Partie Autonomie Complète (TEXTE)
    [SerializeField] Image cadreTxt1;
    [SerializeField] Text texteNiv1;


    // Données construction partie Conduite
    private float positionX;
    private float positionY;

    private float[] postionX_Defaut = { 1, 3, -1, -3 };
    private float[] postionY_Defaut = { -3, 1, 3, -1 };

    private int tailleTxtMin = 30 ;
    private int tailleTxtMax = 40 ;
    private Vector2 tailleCadreMin = new Vector2(25, 15);
    private Vector2 tailleCadreMax = new Vector2(30, 20);

    private int SENS = 1;

    private int orientation;
    private int[] tabOrien = {0,90,180,270};


    private static int niveauChoisi;

    // ---------- METHODES ----------

    // Methode d'inisialisation
    void Start()
    {
        //button.gameObject.SetActive(false);

        // Position du joueur

        //int pos = Array.IndexOf(Partie.Positions, Partie.JoueurCourant) + 1;
        int pos = 3;

        // Definie l'orientation et la postion de la partie Conduit
        // En fonction de la position du joueur
        switch (pos)
        {
            case 1:
                SENS = 1;
                break;
            case 2:
                SENS = 1;
                break;
            case 3:
                SENS = 2;
                break;
            case 4:
                SENS = 3;
                break;
            case 5:
                SENS = 3;
                break;
            case 6:
                SENS = 4;
                break;
        }

        positionX = postionX_Defaut[SENS - 1];
        positionY = postionY_Defaut[SENS - 1];
        orientation = tabOrien[SENS - 1];


    }

    // Méthode de mise a jour
    void Update()
    {
        if(niveauChoisi == 1)
        {
            Debug.Log("niveau 1");
            // Niveau choisi: BAS
            cadreTxt0.rectTransform.sizeDelta = tailleCadreMax;    // Taille du Cadre bas  
            texteNiv0.fontSize = tailleTxtMax;                     // Taille du texte bas
            // Niveau pas choisi: HAUT
            cadreTxt1.rectTransform.sizeDelta = tailleCadreMin;    // Taille du Cadre Haut
            texteNiv1.fontSize = tailleTxtMin;                     // Taille du texte Haut
            //button.gameObject.SetActive(true);
        }
        else
        {
            if(niveauChoisi == 2)
            {
                Debug.Log("niveau 2");
                // Niveau choisi: HAUT
                cadreTxt1.rectTransform.sizeDelta = tailleCadreMax;    // Taille du Cadre Haut
                texteNiv1.fontSize = tailleTxtMax;                     // Taille du texte Haut
                // Niveau pas choisi: BAS
                cadreTxt0.rectTransform.sizeDelta = tailleCadreMin;    // Taille du Cadre bas  
                texteNiv0.fontSize = tailleTxtMin;                     // Taille du texte bas
                button.gameObject.SetActive(true);
            }
        }
    }

    public static void dimentionTexteConduite(int niveau)
    {
        niveauChoisi = niveau;
    }
    


}
