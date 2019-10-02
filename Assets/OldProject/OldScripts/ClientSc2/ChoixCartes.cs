using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

////Script attaché au canvas de choix des acteurs (côté client) version experte
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

    void Start()
    {
        JoueurStatic.Client.RegisterHandler(decideurID, OnDecideurReceived);
        JoueurStatic.Client.RegisterHandler(conceptionID, OnConceptionReceived);
    }

    private void OnEnable()
    {
        ////on initialise les textes qui seront affichés
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


    ////Les joueurs autres que les décideurs reçoivent un message permettant stocker les acteurs
    private void OnConceptionReceived(NetworkMessage netMsg)
    {
        var message = netMsg.ReadMessage<MyActeurMessage>();
        if (message.numero == JoueurStatic.Numero)
        {
            ////On remplit les informations donnant les acteurs piochés par le joueur non décideur
            string a1 = message.acteur1;
            string a2 = message.acteur2;
            string a3 = message.acteur3;
            JoueurStatic.Acteurs = new Sprite[3];
            JoueurStatic.Acteurs[0] = Resources.Load<Sprite>(JoueurStatic.Langue + "/Acteurs/" + a1);
            JoueurStatic.Acteurs[1] = Resources.Load<Sprite>(JoueurStatic.Langue + "/Acteurs/" + a2);
            JoueurStatic.Acteurs[2] = Resources.Load<Sprite>(JoueurStatic.Langue + "/Acteurs/" + a3);

            canvas_choix_cartes.SetActive(false);

            canvas_choix_acteur.SetActive(true);
        }
        if (JoueurStatic.IsPrive || JoueurStatic.IsPublic)
        {
            ////Les décideurs passent directement au canvas suivant pour attendre que les joueurs choisissent leur acteur
            canvas_choix_cartes.SetActive(false);
            canvas_attente_acteur.SetActive(true);
        }
    }
    ////Méthode de réception qui permet de stocker les informations dynamique de chaque joueur, notamment si c'est un 
    ////présentateur d'acteur ou un décideur. Affiche aussi le bon texte en fonction du rôle du joueur
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
