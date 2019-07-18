using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ChoixCartes : MonoBehaviour
{

    [SerializeField] GameObject canvas_choix_cartes;
    [SerializeField] GameObject canvas_choix_acteur;
    [SerializeField] GameObject canvas_attente_acteur;
    [SerializeField] Text joueur;
    [SerializeField] Text choix;
    [SerializeField] Text attente;
    [SerializeField] GameObject decideur;

    short decideurID = 1014;
    short conceptionID = 1002;

    // Start is called before the first frame update
    void Start()
    {
        JoueurStatic.Client.RegisterHandler(decideurID, OnDecideurReceived);
        JoueurStatic.Client.RegisterHandler(conceptionID, OnConceptionReceived);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        choix.gameObject.SetActive(false);
        attente.gameObject.SetActive(false);
        decideur.gameObject.SetActive(false);

        if (JoueurStatic.Langue == "FR")
        {
            joueur.text = "Joueur " + JoueurStatic.Numero;
            choix.text = "Choisissez les cartes\ndu véhicule autonome";
            attente.text = "Attendez que les autres joueurs\nforment un véhicule autonome";
        } else
        {
            joueur.text = "Player " + JoueurStatic.Numero;
            choix.text = "Chose the cards for the\nautonomous vehicle";
            attente.text = "Wait for the other players to shape\an autonomous vehicle";
        }


    }

    private void OnConceptionReceived(NetworkMessage netMsg)
    {
        var message = netMsg.ReadMessage<MyActeurMessage>();
        if (message.numero == JoueurStatic.Numero)
        {
            string a1 = message.acteur1;
            string a2 = message.acteur2;
            string a3 = message.acteur3;
            JoueurStatic.Acteurs = new Sprite[3];
            JoueurStatic.Acteurs[0] = Resources.Load<Sprite>(JoueurStatic.Langue + "/Acteurs/" + a1);
            JoueurStatic.Acteurs[1] = Resources.Load<Sprite>(JoueurStatic.Langue + "/Acteurs/" + a2);
            JoueurStatic.Acteurs[2] = Resources.Load<Sprite>(JoueurStatic.Langue + "/Acteurs/" + a3);
        }

        canvas_choix_cartes.SetActive(false);

        if (JoueurStatic.IsPrive || JoueurStatic.IsPublic)
            canvas_attente_acteur.SetActive(true);
        else
            canvas_choix_acteur.SetActive(true);
    }

    private void OnDecideurReceived(NetworkMessage netMsg)
    {
        var message = netMsg.ReadMessage<MyDecideurMessage>();
        int priv = message.priv;
        int pub = message.pub;
        if (JoueurStatic.Numero == priv)
        {
            JoueurStatic.IsPrive = true;
            JoueurStatic.IsPublic = false;
            decideur.GetComponent<Image>().sprite = Resources.Load<Sprite>(JoueurStatic.Langue + "/Decideurs/DecideurPrive");
            decideur.SetActive(true);
            attente.gameObject.SetActive(true);
        } else if (JoueurStatic.Numero == pub)
        {
            JoueurStatic.IsPublic = true;
            JoueurStatic.IsPrive = false;
            decideur.GetComponent<Image>().sprite = Resources.Load<Sprite>(JoueurStatic.Langue + "/Decideurs/DecideurPublic");
            decideur.SetActive(true);
            attente.gameObject.SetActive(true);
        } else
        {
            JoueurStatic.IsPublic = false;
            JoueurStatic.IsPrive = false;
            choix.gameObject.SetActive(true);
        }
    }
}
