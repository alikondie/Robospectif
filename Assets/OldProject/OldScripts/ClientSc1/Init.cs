using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class Init : MonoBehaviour
{

    public static NetworkClient client;
    short messageID = 1000;
    short positionsID = 1005;
    public Button[] buttons;
    public Text[] texts;
    public static int[] positions;
    public static int nbJoueurs;
    // Start is called before the first frame update
    void Start()
    {
        positions = new int[6];
        client = SansHUD.myclient;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        client.RegisterHandler(messageID, OnMessageReceived);
        client.RegisterHandler(positionsID, OnPositionsReceived);
    }

    void OnMessageReceived(NetworkMessage message)
    {
        int i = message.ReadMessage<MyNetworkMessage>().message;
        for (int j = 0; j < positions.Length; j++)
        {
            if (positions[j] == i)
            {
                buttons[j].gameObject.SetActive(false);
            }
        }
    }

    private void OnPositionsReceived(NetworkMessage netMsg)
    {
        Debug.Log("bieng connecté");
        int i = 0;
        var message = netMsg.ReadMessage<MyPositionsMessage>();
        int[] posMsg = new int[] { message.position1, message.position2, message.position3, message.position4, message.position5, message.position6 };

        for (int j = 0; j < posMsg.Length; j++)
        {
            if (posMsg[j] != 0)
            {
                buttons[j].gameObject.SetActive(true);
                texts[j].text = posMsg[j].ToString();
                positions[j] = posMsg[j];
                i++;
            }
        }
        nbJoueurs = i;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
