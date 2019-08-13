using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class recap_client : MonoBehaviour
{
    [SerializeField] GameObject canvas_vainqueur;
    [SerializeField] GameObject canvas_fin;
    [SerializeField] GameObject canvas_choix_jetons;
    [SerializeField] GameObject canvas_choix_cartes;
    short retourID = 1023;
    short nextexpertID = 1024;

    // Start is called before the first frame update
    void Start()
    {
        JoueurStatic.Client.RegisterHandler(nextexpertID, OnNextPresReceived);
        JoueurStatic.Client.RegisterHandler(retourID, onRetourReceived);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void onRetourReceived(NetworkMessage netMsg)
    {
        canvas_vainqueur.SetActive(false);
        canvas_choix_jetons.SetActive(true);
    }

    private void OnNextPresReceived(NetworkMessage netMsg)
    {
        if (netMsg.Equals("next"))
        {
            canvas_vainqueur.SetActive(false);
            canvas_choix_cartes.SetActive(true);
        }
        else
        {
            canvas_vainqueur.SetActive(false);
            canvas_fin.SetActive(true);
        }
    }
}
