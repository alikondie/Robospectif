#pragma warning disable 0618

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class Init : MonoBehaviour
{

    [SerializeField] GameObject canvas_choix_cartes;
    [SerializeField] GameObject canvas_position_joueurs;
    short messageID = 1000;
    short positionsID = 1005;
    [SerializeField] Button[] buttons;
    [SerializeField] Text[] texts;
    public int[] positions;
    public static int nbJoueurs;
    // Start is called before the first frame update
    void Start()
    {
        positions = new int[6];
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }

        buttons[0].onClick.AddListener(() => onButtonClicked(0));
        buttons[1].onClick.AddListener(() => onButtonClicked(1));
        buttons[2].onClick.AddListener(() => onButtonClicked(2));
        buttons[3].onClick.AddListener(() => onButtonClicked(3));
        buttons[4].onClick.AddListener(() => onButtonClicked(4));
        buttons[5].onClick.AddListener(() => onButtonClicked(5));


        Screen.orientation = ScreenOrientation.LandscapeLeft;
        JoueurStatic.Client.RegisterHandler(messageID, OnMessageReceived);
        JoueurStatic.Client.RegisterHandler(positionsID, OnPositionsReceived);
    }

    private void onButtonClicked(int i)
    {
        JoueurStatic.Numero = positions[i];
        JoueurStatic.Position = i + 1;
        MyNetworkMessage message = new MyNetworkMessage();
        message.message = JoueurStatic.Numero;
        JoueurStatic.Client.Send(messageID, message);
        canvas_position_joueurs.SetActive(false);
        canvas_choix_cartes.SetActive(true);
    }

    // un autre joueur a sélectionné l'un des boutons
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
        JoueurStatic.Langue = message.langue;
        JoueurStatic.Type = message.type;
        if (JoueurStatic.Langue == "FR")
            this.transform.GetChild(7).GetComponent<Text>().text = "Choisissez le numéro de joueur\ncorrespondant à votre position";
        else
            this.transform.GetChild(7).GetComponent<Text>().text = "Chose the player number that\ncorresponds to your position";
        nbJoueurs = i;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
