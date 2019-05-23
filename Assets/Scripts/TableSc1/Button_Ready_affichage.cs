using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Ready_affichage : MonoBehaviour
{
    public GameObject ready;

    // Start is called before the first frame update
    void Start()
    {

    }

    void affichageButtonReady()
    {

        // Debug.Log("Combien de joueurs : " + Text_nb_joueurs.nb_joueurs);

        if (Text_nb_joueurs.nb_joueurs >= 4)
        {
        
            ready.SetActive(true);
            // Debug.Log("true");
        }
        else
        {
            ready.SetActive(false);
            // Debug.Log("false");
        }
    }



    void Update()
    {
        affichageButtonReady();
    }
}
