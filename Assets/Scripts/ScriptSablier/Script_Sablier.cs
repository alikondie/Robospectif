using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_Sablier : MonoBehaviour
{
    // ---------- ATTRIBUETS ----------
    public string nomSceneSuivant;

    // ---------- METHODES ----------

    // Methode d'inisialisation
    void Start()
    {
        
    }

    // Méthode de mise a jour
    void Update()
    {
        //Condition pour changer de scene
        // SI tous ls joueur sont a l'etat conceptionTermine
        // fonction
        //Alors changer scéne
        //passerSceneSuivante();
    }

    // Methode changement de scene
    public void passerSceneSuivante()
    {
        SceneManager.LoadScene(nomSceneSuivant);
    }
}
