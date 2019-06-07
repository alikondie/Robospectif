using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConceptionTerminer : MonoBehaviour
{
    [SerializeField] GameObject canvas_sablier;
    [SerializeField] GameObject canvas_plateau_vehicule;
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
            //SceneManager.LoadScene(nomSceneDemander);
            canvas_sablier.SetActive(false);
            canvas_plateau_vehicule.SetActive(true);
        }
    }

    // Méthode qui dit quand le joueur a une conception terminer (Android)
    public static void finiMaConception()
    {
        nbJoueurConceptionTerminer++;
    }
}
