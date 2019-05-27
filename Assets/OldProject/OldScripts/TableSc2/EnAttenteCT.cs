using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnAttenteCT : MonoBehaviour
{
    public string nomSceneDemander;
    private int nbJoueur; //Nb Joueurs
    private static int premierJoueurFini; //numero du 1er joueur a avoir fini sa conception 
    private static bool estPremier;

    // ---------- METHODES ----------

    // Methode d'inisialisation
    void Start()
    {
        // Initialise du boolean
        estPremier = false;

        // Recuperation du Nombre de joueur:
        nbJoueur = PlayerPrefs.GetInt("nombreJoueur");     // Nombre de Joueurs

    }

    // Update is called once per frame
    void Update()
    {
        if (estPremier)
        {

            //changer de scene
            SceneManager.LoadScene(nomSceneDemander);

            //estPremier = false;

        }
    }

    // Méthode qui dit quand le joueur a une conception terminer (Android)
    public static void premierFini(int numJoueur)
    {
        estPremier = true;
        premierJoueurFini = numJoueur;
    }

}
