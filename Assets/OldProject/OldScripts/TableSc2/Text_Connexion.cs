using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Text_Connexion : MonoBehaviour
{
    // ---------- ATTRIBUETS ----------

    [SerializeField] GameObject canvas_joueurs;
    [SerializeField] GameObject canvas_attente_choix_cartes;

    [SerializeField] GameObject text_Position_1;      // Texte a la 1er position (En Bas a Gauche)
    [SerializeField] GameObject text_Position_2;      // Texte a la 2eme position (En Bas a Droite)
    [SerializeField] GameObject text_Position_3;      // Texte a la 3eme position (A Droite)
    [SerializeField] GameObject text_Position_4;      // Texte a la 4eme position (En Haut a Droite)
    [SerializeField] GameObject text_Position_5;      // Texte a la 5eme position (En Haut a Gauche)
    [SerializeField] GameObject text_Position_6;      // Texte a la 6eem position (A Gauche)

    // ---------- CONSTANTES ----------

    private int numJoueur;      // Pour affecter un numero de joueur (1 - 6)
    private Text affichageJoueur;   // Pour convertir de GameObjecte à Text
    private GameObject[] tabText = new GameObject[6];   //Tableau qui contient tout les GameObject "text_Position"
    private int infoAndroid;

    // Recuperation scene d'avant
    public static int nbJoueur ;  // Le nombre de joueur dans la partie (de 4 à 6)
    private static bool estDebut;
    public static int nbJoueursConnectes;

    // ---------- METHODES ----------

    // Methode d'inisialisation
    void Start()
    {

        infoAndroid = 0;
        estDebut = false;
        // Recuperation du Nombre de joueur:
        nbJoueur = PlayerPrefs.GetInt("nombreJoueur");     // Nombre de Joueurs
        //nbJoueur = 6; // Nombre de Joueurs



        //Initialisé le Tableau de GameObject "text_Position"
        tabText = new GameObject[] { text_Position_1, text_Position_2, text_Position_3, text_Position_4, text_Position_5, text_Position_6 };

        // Initialisé les textes à "Non Connecté".
        InitAffichageTextJoueur(Partie.Positions);

    }

    // Méthode de Mise A Jour
    void Update()
    {

        AfficheJoueurConnecter();
        if (nbJoueursConnectes == nbJoueur)
        {       
            //SceneManager.LoadScene("Scene_3");
            canvas_joueurs.SetActive(false);
            canvas_attente_choix_cartes.SetActive(true);
        }

    }


    // Méthode Initialise l'Affichage du Text par Joueur
    void InitAffichageTextJoueur(int[] tPosition)
    {
        numJoueur = 1;  // Compteur pour affecter le bon numero de joueur

        // Boucle FOR qui peut parcourir les 6 joueurs
        for (int i = 0; i < 6; i++)
        {
            

            // Boucle IF qui Affiche le Text seulement pour les joueurs
            if (tPosition[i] > 0) //Si il fait partie du Tableau (Joueurs choisi)
            {
                //Ecrie dans la partie Text
                affichageJoueur = tabText[i].GetComponent<Text>();
                affichageJoueur.color = Color.white; //Couleur de tout le texte 
                affichageJoueur.text = "Joueur " + numJoueur + "\n" + "<color=red> Pas Connecté </color>";

                numJoueur++;    //Incrementation du numero de Joueur
            }
            else
            {
                Destroy(tabText[i]);    // Détruit les textes qui n'ont pas de joueurs
            }
        }
    }


    // Méthode qui revoie Vrai si un element (int) est dans un tableau d'entier
    private bool EstDedans(int element, int[] tab) 
    {
        bool trouver = false;

        for (int i = 0; i <= 5; i++)
        {
            if (element == tab[i]) 
            {
                trouver = true;
            }
        }

        return trouver;
    }


    // -------------------------------------------------------------------------------------

    // Méthode qui recupere l'entier de Joueur (Android)
    public static void recupInfoJoueur(int i)
    {
        int pos = -1;
        for (int j = 0; j < 6; j++)
        {
            if (i == Partie.Positions[j])
            {
                pos = j+1;
            }
        }
        PlayerPrefs.SetInt("monInfoJoueur", pos);
        estDebut = true;
        nbJoueursConnectes++;
    }


    // Méthode Affichage de connexion par joueurs
    void AfficheJoueurConnecter()
    {

        //Recuperer les infos sur la connection du joueurs

        if (estDebut)
        {
            infoAndroid = PlayerPrefs.GetInt("monInfoJoueur");
        }

        for (int i = 0; i < Partie.Positions.Length; i++)
        {
            if ( (Partie.Positions[i] != 0) && estConnecte(Partie.Positions[i], infoAndroid)){
                affichageJoueur = tabText[i].GetComponent<Text>();
                affichageJoueur.color = Color.white;
                affichageJoueur.text = "Joueur" + Partie.Positions[i] + "\n" + "<color=blue> Connecté </color>";
            }
        }
    }


    // Méthode qui revoie Vrai si un joueur (int) est Connecté
    private bool estConnecte(int joueur, int JAndroid)
    {
        bool Connecter = false; //variable qui est Vrai si le joueur est connecté


        //Verifie si le joueur c'est conecté
        if (joueur == JAndroid)
        {
            Connecter = true;
        }

        return Connecter;
    }

}

