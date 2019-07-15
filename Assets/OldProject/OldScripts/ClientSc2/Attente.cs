using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Attente : MonoBehaviour
{

    [SerializeField] GameObject canvas_choix_persos;
    [SerializeField] GameObject canvas_persos_table;
    [SerializeField] GameObject canvas_pres_robot;
    [SerializeField] Text text;
    [SerializeField] Text central;

    short presID = 1011;

    // Start is called before the first frame update
    void Start()
    {
        JoueurStatic.Client.RegisterHandler(presID, onWaitReceived);
    }

    private void onWaitReceived(NetworkMessage netMsg)
    {
        int fini = netMsg.ReadMessage<MyNetworkMessage>().message;
        canvas_pres_robot.SetActive(false);
        if (JoueurStatic.Numero == fini)
        {
            canvas_persos_table.SetActive(true);
        }
        else
        {
            canvas_choix_persos.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        if (JoueurStatic.Langue == "FR")
        {
            if (JoueurStatic.Type != "expert")
            {
                text.text = "Joueur " + JoueurStatic.Numero.ToString();
                central.text = "Présentation du robot";
            }
            else
            {
                text.text = "Joueur " + JoueurStatic.Numero.ToString();
                central.text = "Veuillez choisir le robot avec les autres joueurs";
            }
        }
        else
        {
            if (JoueurStatic.Type != "expert")
            {
                text.text = "Player " + JoueurStatic.Numero.ToString();
                central.text = "Robot presentation";
            }
            else
            {
                text.text = "Player " + JoueurStatic.Numero.ToString();
                central.text = "Robot presentation";
            }
        }

    }
}
