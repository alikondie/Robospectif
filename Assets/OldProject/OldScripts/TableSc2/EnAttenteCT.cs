﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnAttenteCT : MonoBehaviour
{
    short cardID = 1009;

    [SerializeField] GameObject canvas_attente_choix_cartes;
    [SerializeField] GameObject canvas_sablier;
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

    void OnEnable()
    {
        if (Partie.Langue == "FR")
            this.transform.GetChild(1).GetComponent<Text>().text = "Choisissez vos cartes !";
        else
            this.transform.GetChild(1).GetComponent<Text>().text = "Chose your cards !";

        foreach (Joueur j in Partie.Joueurs)
        {
            MyCardMessage msg = new MyCardMessage();
            //Debug.Log(j.Dimensions[0].ToString().Substring(0, j.Dimensions[0].ToString().Length - 21));
            //Debug.Log(Resources.Load<Sprite>("image/Dimension/" + j.Dimensions[0].ToString().Substring(0, j.Dimensions[0].ToString().Length - 21)));
            msg.dim1 = j.Dimensions[0].name;
            msg.dim2 = j.Dimensions[1].name;

            //Debug.Log(j.Locomotions[0].ToString().Substring(0, j.Locomotions[0].ToString().Length - 21));
            //Debug.Log(Resources.Load<Sprite>("image/Locomotion/" + j.Locomotions[0].ToString().Substring(0, j.Locomotions[0].ToString().Length - 21)));
            msg.loco1 = j.Locomotions[0].name;
            msg.loco2 = j.Locomotions[1].name;
            //Debug.Log(j.Equipements[0].ToString().Substring(0, j.Equipements[0].ToString().Length - 21));
            //Debug.Log(Resources.Load<Sprite>("image/Equipements/" + j.Equipements[0].ToString().Substring(0, j.Equipements[0].ToString().Length - 21)));
            msg.equi1 = j.Equipements[0].name;
            msg.equi2 = j.Equipements[1].name;
            msg.equi3 = j.Equipements[2].name;
            msg.equi4 = j.Equipements[3].name;
            msg.equi5 = j.Equipements[4].name;
            msg.equi6 = j.Equipements[5].name;
            msg.perso1 = j.Persos[0].name;
            msg.perso2 = j.Persos[1].name;
            msg.perso3 = j.Persos[2].name;
            msg.perso4 = j.Persos[3].name;
            msg.perso5 = j.Persos[4].name;
            msg.perso6 = j.Persos[5].name;
            msg.num = j.Numero;
            NetworkServer.SendToAll(cardID, msg);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (estPremier)
        {

            //changer de scene
            //SceneManager.LoadScene(nomSceneDemander);
            canvas_attente_choix_cartes.SetActive(false);
            canvas_sablier.SetActive(true);
        }
    }

    // Méthode qui dit quand le joueur a une conception terminer (Android)
    public static void premierFini(int numJoueur)
    {
        estPremier = true;
        premierJoueurFini = numJoueur;
    }

}
