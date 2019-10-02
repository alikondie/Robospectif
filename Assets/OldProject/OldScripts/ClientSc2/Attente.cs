using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


////Srcipt attaché au canvas de présentation du robot (côté client). il est utilisé par les versions standards et experte.
//// Il indique au joueur que le robot est en cours de présentation
public class Attente : MonoBehaviour
{

    [SerializeField] GameObject canvas_choix_persos;
    [SerializeField] GameObject canvas_persos_table;
    [SerializeField] GameObject canvas_pres_robot;
    [SerializeField] Text text;
    [SerializeField] Text central;

    short presID = 1011;

    void Start()
    {
        JoueurStatic.Client.RegisterHandler(presID, onWaitReceived);
    }

    ////réception de message indiquant que la présentation du robot est terminé, et qu'il faut donc changer de canvas
    private void onWaitReceived(NetworkMessage netMsg)
    {
        int fini = netMsg.ReadMessage<MyNetworkMessage>().message;
        canvas_pres_robot.SetActive(false);
        if (JoueurStatic.Numero == fini)
        {
            ////si le joueur est le présentateur du robot, on active le canvas correspondant, qui l'invite à écouter 
            ////les usages des autres joueurs
            canvas_persos_table.SetActive(true);
        }
        else
        {
            ////on active le canvas de choix des persos pour les joueurs autre que le présentateur du robot
            canvas_choix_persos.SetActive(true);
        }
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
