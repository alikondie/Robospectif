using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene3 : MonoBehaviour
{
    public static int nbValides;

    // Start is called before the first frame update
    void Start()
    {
        nbValides = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (nbValides == Text_Connexion.nbJoueur)
        {
            SceneManager.LoadScene("Scene_4");
        }
    }

    public static void joueurValide()
    {
        nbValides++;
        Debug.Log("joueurs validés : " + nbValides);
        Debug.Log("joueurs connectés : " + Text_Connexion.nbJoueur);
    }
}
