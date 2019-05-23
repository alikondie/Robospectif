using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AttenteFin : MonoBehaviour
{

    public Text text;

    private int position;

    short nextID = 1015;
    public static NetworkClient client = Valider.client;
    public static Joueur joueur = Valider.joueur;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "Joueur : " + joueur.Numero.ToString();
        client.RegisterHandler(nextID, onWaitReceived);
    }

    private void onWaitReceived(NetworkMessage netMsg)
    {
        string msg = netMsg.ReadMessage<MyStringMessage>().s;
        if (msg.Equals("next"))
        {
            SceneManager.LoadScene("scene2bis");
        }
        else
        {
            SceneManager.LoadScene("FinJeu");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
