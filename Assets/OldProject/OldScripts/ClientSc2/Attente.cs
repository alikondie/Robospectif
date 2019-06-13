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
    [SerializeField] GameObject canvas_choix_jetons;
    [SerializeField] GameObject canvas_pres_robot;
    [SerializeField] Text text;

    private int position;

    short waitID = 1006;
    public static NetworkClient client;
    public static Joueur joueur;

    // Start is called before the first frame update
    void Start()
    {
        joueur = Valider.joueur;
        client = Valider.client;
        position = Valider.position;
        text.text = "Joueur : " + joueur.Numero.ToString();
        client.RegisterHandler(waitID, onWaitReceived);
    }

    private void onWaitReceived(NetworkMessage netMsg)
    {
        int fini = netMsg.ReadMessage<MyNetworkMessage>().message;
        if (position == fini)
        {
            //SceneManager.LoadScene("Scene_ChoixJetons");
            canvas_pres_robot.SetActive(false);
            canvas_choix_jetons.SetActive(true);
        }
        else
        {
            canvas_pres_robot.SetActive(false);
            canvas_choix_persos.SetActive(true);
            //SceneManager.LoadScene("scene3");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
