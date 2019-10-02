using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_Sablier : MonoBehaviour
{
    // ---------- ATTRIBUETS ----------
    public string nomSceneSuivant;

    // ---------- METHODES ----------

    // Methode changement de scene
    public void passerSceneSuivante()
    {
        SceneManager.LoadScene(nomSceneSuivant);
    }
}
