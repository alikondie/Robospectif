using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConceptionTerminer : MonoBehaviour
{
    public string nomSceneDemander;
    private int nbJoueur; //Nb Joueurs
    private static int nbJoueurConceptionTerminer; //Conteur du nombre de joueurs a avoir Terminer leur conception
    
    // ---------- METHODES ----------

    // Methode d'inisialisation
    void Start()
    {
        // Initialise le compteur
        nbJoueurConceptionTerminer = 1;

        // Recuperation du Nombre de joueur:
        nbJoueur = PlayerPrefs.GetInt("nombreJoueur");     // Nombre de Joueurs

    }

    // Update is called once per frame
    void Update()
    {
        if(nbJoueurConceptionTerminer == nbJoueur)
        {
            for (int i = 0; i < 6; i++)
            {
                Debug.Log("envoi[" + i + "] = " + Button_ready_next_scene.envoi[i]);
            }
            SceneManager.LoadScene(nomSceneDemander);
        }
    }

    // Méthode qui dit quand le joueur a une conception terminer (Android)
    public static void finiMaConception()
    {
        nbJoueurConceptionTerminer++;
        Debug.Log("joueurs valides : " + nbJoueurConceptionTerminer);
    }
}
