using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AttenteFin : MonoBehaviour
{
    [SerializeField] GameObject canvas_fin_partie;
    [SerializeField] GameObject canvas_vainqueur;
    [SerializeField] GameObject canvas_pres_robot;
    [SerializeField] GameObject canvas_choix_jetons;
    [SerializeField] Text text;
    [SerializeField] Text central;

    private int position;

    short nextID = 1015;
    short retourID = 1023;
    short suiteID = 1026;

    // Start is called before the first frame update
    void Start()
    {
        JoueurStatic.Client.RegisterHandler(nextID, onWaitReceived);
        JoueurStatic.Client.RegisterHandler(retourID, onRetourReceived);
        JoueurStatic.Client.RegisterHandler(suiteID, onSuiteReceived);
    }

    private void onWaitReceived(NetworkMessage netMsg)
    {
        JoueurStatic.SocieteCompteur = 0;
        JoueurStatic.PlaneteCompteur = 0;
        JoueurStatic.UsageCompteur = 0;

        string msg = netMsg.ReadMessage<MyStringMessage>().s;
        if (msg.Equals("next"))
        {
            canvas_vainqueur.SetActive(false);
            canvas_pres_robot.SetActive(true);
        }
        else
        {
            canvas_vainqueur.SetActive(false);
            canvas_fin_partie.SetActive(true);
        }
    }

    void OnEnable()
    {
        if (JoueurStatic.Langue == "FR")
        {
            text.text = "Joueur " + JoueurStatic.Numero.ToString();
            central.text = "Attente du choix du\ncréateur du robot";
        } else
        {
            text.text = "Player " + JoueurStatic.Numero.ToString();
            central.text = "Waiting for the robot\ncreator's choice";
        }
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

    private void onSuiteReceived(NetworkMessage netMsg)
    {
        if (JoueurStatic.Langue == "FR")
        {
            central.text = "Fin du tour";
        }
        else
        {
            central.text = "End of turn";
        }
    }
}
