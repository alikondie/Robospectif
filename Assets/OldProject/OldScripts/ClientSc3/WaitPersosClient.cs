using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class WaitPersosClient : MonoBehaviour
{
    [SerializeField] GameObject canvas_persos_table;
    [SerializeField] GameObject canvas_choix_jetons;
    [SerializeField] Text text;
    [SerializeField] Text central;

    short debatID = 1006;
    short joueurID = 1019;
    short presentateurID = 1020;
    private string fr;
    private string en;
    private bool presentateur_changed;
    private int[] listtourattente;

    // Start is called before the first frame update
    void Start()
    {
        JoueurStatic.Client.RegisterHandler(debatID, OnDebatReceived);
        JoueurStatic.Client.RegisterHandler(presentateurID, OnNextPresReceived);
    }

    // Update is called once per frame
    void Update()
    {
        if(presentateur_changed)
        {
            int attentejoueurcourant = listtourattente[JoueurStatic.Numero];
            if (attentejoueurcourant == 0)
            {
                central.text = "Présentez un usage du véhicule par votre personnage.\n Une fois l'usage présenté, passez au joueur suivant";
            }
            else if (attentejoueurcourant < 0)
            {
                central.text = "Présentation des usages";
            }
            else
            {
                presentateur_changed = false;
                central.text = "Vous présentez dans " + attentejoueurcourant + " tours";
            }
            MyNetworkMessage msg = new MyNetworkMessage();
            JoueurStatic.Client.Send(joueurID, msg);
        }
    }

    void OnEnable()
    {
        presentateur_changed = false;
        //if (JoueurStatic.Type == "expert")
        //{
        //    fr = "acteurs";
        //    en = "Roles";
        //}
        //else
        //{
        //    fr = "personnages";
        //    en = "Characters";
        //}
        //if (JoueurStatic.Langue == "FR")
        //{
        //    text.text = "Joueur " + JoueurStatic.Numero.ToString();
        //    central.text = "Présentation des\n" + fr;
        //} else
        //{
        //    text.text = "Player " + JoueurStatic.Numero;
        //    central.text = en + "\npresentation";
        //}
    }

    private void OnDebatReceived(NetworkMessage netMsg)
    {
        canvas_persos_table.SetActive(false);
        canvas_choix_jetons.SetActive(true);
    }

    private void OnNextPresReceived(NetworkMessage netMsg)
    {
        listtourattente = netMsg.ReadMessage<MyNetworkMessage>().tableau;
        presentateur_changed = true;
    }
}
