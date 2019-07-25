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
    [SerializeField] Button nextbutton;
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
        nextbutton.gameObject.SetActive(false);
        nextbutton.onClick.AddListener(() => ButtonClicked());
        JoueurStatic.Client.RegisterHandler(debatID, OnDebatReceived);
        JoueurStatic.Client.RegisterHandler(presentateurID, OnNextPresReceived);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("le presentateur change : " + presentateur_changed);
        Debug.Log(" taille listtourattente " + listtourattente.Length);

        if (presentateur_changed)
        {
            Debug.Log("on change de presentateur");
            int attentejoueurcourant = listtourattente[JoueurStatic.Numero - 1];
            Debug.Log("il reste " + attentejoueurcourant + " tours");
            if (attentejoueurcourant == 0)
            {
                presentateur_changed = false;
                central.text = "Présentez un usage du véhicule par votre personnage.\n Une fois l'usage présenté, passez au joueur suivant";
                nextbutton.gameObject.SetActive(true);
            }
            else if (attentejoueurcourant < 0)
            {
                presentateur_changed = false;
                central.text = "Présentation des usages";
                nextbutton.gameObject.SetActive(false);
            }
            else
            {
                nextbutton.gameObject.SetActive(false);
                presentateur_changed = false;
                central.text = "Vous présentez dans " + attentejoueurcourant + " tours";
            }
        }
    }

    void OnEnable()
    {
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

    private void ButtonClicked()
    {
        MyNetworkMessage msg = new MyNetworkMessage();
        JoueurStatic.Client.Send(joueurID, msg);
    }

    private void OnDebatReceived(NetworkMessage netMsg)
    {
        Debug.Log("debat received");
        presentateur_changed = false;
        nextbutton.gameObject.SetActive(false);
        canvas_persos_table.SetActive(false);
        canvas_choix_jetons.SetActive(true);
    }

    private void OnNextPresReceived(NetworkMessage netMsg)
    {
        Debug.Log("message recu");
        var objet = netMsg.ReadMessage<MyNetworkMessage>();
        listtourattente = objet.tableau;
        nextbutton.transform.GetChild(0).gameObject.GetComponent<Text>().text = objet.text;
        presentateur_changed = true;
    }
}
