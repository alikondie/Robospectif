using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class Init : MonoBehaviour
{

    public static NetworkClient client = SansHUD.myclient;
    short messageID = 1000;
    short positionsID = 1005;
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    public Button button6;
    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;
    public Text text5;
    public Text text6;
    public static int position1;
    public static int position2;
    public static int position3;
    public static int position4;
    public static int position5;
    public static int position6;
    public static int nbJoueurs;
    // Start is called before the first frame update
    void Start()
    {
        client.RegisterHandler(messageID, OnMessageReceived);
        client.RegisterHandler(positionsID, OnPositionsReceived);
    }

    void OnMessageReceived(NetworkMessage message)
    {
        int i = message.ReadMessage<MyNetworkMessage>().message;
        if (Init.position1 == i)
            button1.gameObject.SetActive(false);
        if (Init.position2 == i)
            button2.gameObject.SetActive(false);
        if (Init.position3 == i)
            button3.gameObject.SetActive(false);
        if (Init.position4 == i)
            button4.gameObject.SetActive(false);
        if (Init.position5 == i)
            button5.gameObject.SetActive(false);
        if (Init.position6 == i)
            button6.gameObject.SetActive(false);
    }

    private void OnPositionsReceived(NetworkMessage netMsg)
    {
        Debug.Log("bieng connecté");
        int i = 0;
        var message = netMsg.ReadMessage<MyPositionsMessage>();
        if (message.position1 != 0)
        {
            button1.gameObject.SetActive(true);
            text1.text = message.position1.ToString();
            position1 = message.position1;
            i++;
        }
        if (message.position2 != 0)
        {
            button2.gameObject.SetActive(true);
            text2.text = message.position2.ToString();
            position2 = message.position2;
            i++;
        }
        if (message.position3 != 0)
        {
            button3.gameObject.SetActive(true);
            text3.text = message.position3.ToString();
            position3 = message.position3;
            i++;
        }
        if (message.position4 != 0)
        {
            button4.gameObject.SetActive(true);
            text4.text = message.position4.ToString();
            position4 = message.position4;
            i++;
        }
        if (message.position5 != 0)
        {
            button5.gameObject.SetActive(true);
            text5.text = message.position5.ToString();
            position5 = message.position5;
            i++;
        }
        if (message.position6 != 0)
        {
            button6.gameObject.SetActive(true);
            text6.text = message.position6.ToString();
            position6 = message.position6;
            i++;
        }
        nbJoueurs = i;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
