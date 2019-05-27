using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Attente : MonoBehaviour
{

    public Text text;

    private int position;

    short waitID = 1006;
    public static NetworkClient client = Valider.client;
    public static Joueur joueur = Valider.joueur;

    // Start is called before the first frame update
    void Start()
    {
        position = Valider.position;
        text.text = "Joueur : " + joueur.Numero.ToString();
        client.RegisterHandler(waitID, onWaitReceived);
    }

    private void onWaitReceived(NetworkMessage netMsg)
    {
        int fini = netMsg.ReadMessage<MyNetworkMessage>().message;
        if (position == fini)
        {
            SceneManager.LoadScene("Scene_ChoixJetons");
        }
        else
        {
            SceneManager.LoadScene("scene3");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
