using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_nb_joueurs : MonoBehaviour
{
    public static int nb_joueurs = 0;
    public GameObject text_nb_joueurs;

    // Start is called before the first frame update
    void Start()
    {
        MiseAJourText();
    }

    void MiseAJourText()
    {
        // ----- PARTIE NATHAN -----
        // Initialise les envoies de position
        for (int i = 1; i <= 6; i++)
        {
            PlayerPrefs.SetInt("LaPosition" + i, 0);    //Affecte les positions a '0' par défaut
        }
        // --------------------------

            nb_joueurs = 0;

        if (Joueur_1_ready.joueur_1)
        {
            nb_joueurs += 1;
            PlayerPrefs.SetInt("LaPosition1" , 1);    //Envoie la position 1.
        }

        if (Joueur_2_ready.joueur_2)
        {
            nb_joueurs += 1;
            PlayerPrefs.SetInt("LaPosition2", 2);    //Envoie la position 2.
        }

        if (Joueur_3_ready.joueur_3)
        {
            nb_joueurs += 1;
            PlayerPrefs.SetInt("LaPosition3", 3);    //Envoie la position 3.
        }

        if (Joueur_4_ready.joueur_4)
        {
            nb_joueurs += 1;
            PlayerPrefs.SetInt("LaPosition4", 4);    //Envoie la position 4.
        }

        if (Joueur_5_ready.joueur_5)
        {
            nb_joueurs += 1;
            PlayerPrefs.SetInt("LaPosition5", 5);    //Envoie la position 5.
        }

        if (Joueur_6_ready.joueur_6)
        {
            nb_joueurs += 1;
            PlayerPrefs.SetInt("LaPosition6", 6);    //Envoie la position 6.
        }

        Text nb_joueurs_text = text_nb_joueurs.GetComponent<Text>();
        nb_joueurs_text.text = "Il y a " + nb_joueurs + " joueurs enregistrés.";

        PlayerPrefs.SetInt("nbJoueur", nb_joueurs);    //Envoie le nombre de Joueur
    }


    void OnMouseDown()
    {
        MiseAJourText();
        Debug.Log("Clique sur le texte");
    }

    private void Update()
    {
        MiseAJourText();
    }
}
