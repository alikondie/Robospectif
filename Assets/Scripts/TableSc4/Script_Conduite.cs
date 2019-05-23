using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_Conduite : MonoBehaviour
{
    // ---------- ATTRIBUTS ----------

    public int positionJoueur;

    public int[] positions;

    public Button button;

    // Curseur
    public Image barre;
    // Partie Attention Requise (TEXTE)
    public Image cadreTxt0;
    public Text texteNiv0;
    // Partie Autonomie Complète (TEXTE)
    public Image cadreTxt1;
    public Text texteNiv1;


    // Données construction partie Conduite
    private float positionX;
    private float positionY;

    private float[] postionX_Defaut = { 1, 3, -1, -3 };
    private float[] postionY_Defaut = { -3, 1, 3, -1 };

    private int ecart = 2;

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
        button.gameObject.SetActive(false);

        positions = Text_Connexion.positions;

        // Position du joueur
        positionJoueur = Partie.JoueurCourant;

        int pos = -1;

        for (int i = 0; i < 6; i++)
        {
            if (positions[i] == positionJoueur)
            {
                pos = i + 1;
            }
        }

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


        //Initialisation
        // BARRE
        barre = barre.GetComponent<Image>();
        barre.rectTransform.sizeDelta = new Vector2(60, 5);
        barre.rectTransform.position = new Vector2(positionX, positionY);   // Position de la Barre
        barre.rectTransform.Rotate(0, 0, orientation);    // Rotation Barre
        //text bas
        cadreTxt0 = cadreTxt0.GetComponent<Image>();
        cadreTxt0.rectTransform.sizeDelta = tailleCadreMax;    // Taille du Cadre bas
        cadreTxt0.rectTransform.Rotate(0, 0, orientation);    // Rotation du Cadre Bas

        texteNiv0 = texteNiv0.GetComponent<Text>();
        texteNiv0.fontSize = tailleTxtMax;    // Taille du texte bas
        texteNiv0.rectTransform.Rotate(0, 0, 0);    // Rotation du Texte Bas
        //text Haut
        cadreTxt1 = cadreTxt1.GetComponent<Image>();
        cadreTxt1.rectTransform.sizeDelta = tailleCadreMax;    // Taille du Cadre Haut
        cadreTxt1.rectTransform.Rotate(0, 0, orientation);    // Rotation du Cadre Haut

        texteNiv1 = texteNiv1.GetComponent<Text>();
        texteNiv1.fontSize = tailleTxtMax;    // Taille du texte Haut
        texteNiv1.rectTransform.Rotate(0, 0, 0);    // Rotation du Texte Haut

        // -----------------------------


        switch (SENS)
        {
            case 1:
                cadreTxt0.rectTransform.position = new Vector2((positionX - ecart), (positionY + (ecart / 2)));     // Position du Cadre Bas
                texteNiv0.rectTransform.position = new Vector2((positionX - ecart), (positionY + (ecart / 2)));     // Position du Texte Bas
                cadreTxt1.rectTransform.position = new Vector2((positionX + ecart), (positionY + (ecart / 2)));     // Position du Cadre Haut
                texteNiv1.rectTransform.position = new Vector2((positionX + ecart), (positionY + (ecart / 2)));     // Position du Texte Haut
                break;
            case 2:
                cadreTxt0.rectTransform.position = new Vector2((positionX - (ecart / 2)), (positionY - ecart));     // Position du Cadre Bas
                texteNiv0.rectTransform.position = new Vector2((positionX - (ecart / 2)), (positionY - ecart));     // Position du Texte Bas
                cadreTxt1.rectTransform.position = new Vector2((positionX - (ecart / 2)), (positionY + ecart));     // Position du Cadre Haut
                texteNiv1.rectTransform.position = new Vector2((positionX - (ecart / 2)), (positionY + ecart));     // Position du Texte Haut
                break;
            case 3:
                cadreTxt0.rectTransform.position = new Vector2((positionX + ecart), (positionY - (ecart / 2)));     // Position du Cadre Bas
                texteNiv0.rectTransform.position = new Vector2((positionX + ecart), (positionY - (ecart / 2)));     // Position du Texte Bas
                cadreTxt1.rectTransform.position = new Vector2((positionX - ecart), (positionY - (ecart / 2)));     // Position du Cadre Haut
                texteNiv1.rectTransform.position = new Vector2((positionX - ecart), (positionY - (ecart / 2)));     // Position du Texte Haut
                break;
            case 4:
                cadreTxt0.rectTransform.position = new Vector2((positionX + (ecart / 2)), (positionY + ecart));     // Position du Cadre Bas
                texteNiv0.rectTransform.position = new Vector2((positionX + (ecart / 2)), (positionY + ecart));     // Position du Texte Bas
                cadreTxt1.rectTransform.position = new Vector2((positionX + (ecart / 2)), (positionY - ecart));     // Position du Cadre Haut
                texteNiv1.rectTransform.position = new Vector2((positionX + (ecart / 2)), (positionY - ecart));     // Position du Texte Haut
                break;
        }


    }

    // Méthode de mise a jour
    void Update()
    {
        if(niveauChoisi == 0)
        {
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
            if(niveauChoisi == 1)
            {
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
