using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Init5 : MonoBehaviour
{
    public static NetworkClient client;

    public Text text;

    short waitID = 1006;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "Joueur " + selectUser.positionStatic;
        client = Valider.client;
        client.RegisterHandler(waitID, OnWaitReceived);
    }

    private void OnWaitReceived(NetworkMessage netMsg)
    {
        SceneManager.LoadScene("scenePreEnd");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
