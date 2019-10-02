using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;


////Script du canvas du positionnement des joueurs (côté client), avant le début de la partie
public class Init : MonoBehaviour
{
    [SerializeField] GameObject canvas_infos_joueurs;
    [SerializeField] GameObject canvas_position_joueurs;
    short messageID = 1000;
    short positionsID = 1005;
    [SerializeField] Button[] buttons;
    [SerializeField] Text[] texts;
    public int[] positions;

    void Start()
    {

        ////on initialise toutes les variables de positions des joueurs, et on attribut les listener aux boutons
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

        ////lorsque le joueur clique sur sa position, on met à jour dans joueur static le numéro du joueur et sa 
        ////position, puis on envoie au serveur un message pour lui indiquer que le joueur i a cliqué
        JoueurStatic.Numero = positions[i];
        JoueurStatic.Position = i + 1;
        MyNetworkMessage message = new MyNetworkMessage();
        message.message = JoueurStatic.Numero;
        JoueurStatic.Client.Send(messageID, message);
        ////on passe au canvas suivant
        canvas_position_joueurs.SetActive(false);
        canvas_infos_joueurs.SetActive(true);
    }

    // méthode de réception de message indiquant qu'une position a été prise et qu'elle n'est donc plus disponible
    void OnMessageReceived(NetworkMessage message)
    {
        int i = message.ReadMessage<MyNetworkMessage>().message;
        for (int j = 0; j < positions.Length; j++)
        {
            if (positions[j] == i)
            {
                ////on désactive le bouton qui a été cliqué par un autre joueur
                buttons[j].gameObject.SetActive(false);
            }
        }
    }

    ////message de réception pour indiquer qu'il faut choisir sa position
    private void OnPositionsReceived(NetworkMessage netMsg)
    {
        ////On positionne et affiche seulement les boutons correspondant aux emplacements sélectionnés sur le serveur au canvas précédent
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

        ////le message contient les informations globales de la partie, on les stocke donc dans joueurstatic pour 
        //// y avoir accès à tout moment depuis le client
        JoueurStatic.Langue = message.langue;
        JoueurStatic.Type = message.type;
        if (JoueurStatic.Langue == "FR")
            this.transform.GetChild(7).GetComponent<Text>().text = "Choisissez le numéro de joueur\ncorrespondant à votre position";
        else
            this.transform.GetChild(7).GetComponent<Text>().text = "Chose the player number that\ncorresponds to your position";
        JoueurStatic.NbJoueurs = i;
    }
}
