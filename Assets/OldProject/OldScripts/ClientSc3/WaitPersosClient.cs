using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

////Script attaché au canvas perso table(côté client), il indique au joueur si il doit présenter son perso,
//// ou dans combien de temps il présente
public class WaitPersosClient : MonoBehaviour
{
    [SerializeField] GameObject canvas_persos_table;
    [SerializeField] GameObject canvas_choix_jetons;
    [SerializeField] Button nextbutton;
    [SerializeField] Text text;
    [SerializeField] Text central;

    short debatID = 1006;
    short debatclientID = 1021;
    short joueurID = 1019;
    short presentateurID = 1020;
    private bool presentateur_changed;
    private int[] listtourattente;

    void Start()
    {
        nextbutton.gameObject.SetActive(false);
        nextbutton.onClick.AddListener(() => ButtonClicked());
        JoueurStatic.Client.RegisterHandler(presentateurID, OnNextPresReceived);
        JoueurStatic.Client.RegisterHandler(debatclientID, OnDebatReceived);
    }

    ////On met à jour le texte afficher grâce au update
    void Update()
    {
        if (presentateur_changed)
        {
            ////si le joueur qui présentait son perso a terminé, alors on met à jour le texte en fonction du rôle 
            //// du joueur courant
            int attentejoueurcourant = listtourattente[JoueurStatic.Numero-1];
            if (attentejoueurcourant == 0)
            {
                ////Le joueur est le nouveau présentateur
                if (JoueurStatic.Langue == "FR")
                    central.text = "Présentez un usage du véhicule par votre personnage.\n Une fois l'usage présenté, passez au joueur suivant";
                else
                    central.text = "Present how your character would use this vehicle.\n When you're done, hand over to the next player";
                nextbutton.gameObject.SetActive(true);
            }
            else if (attentejoueurcourant < 0)
            {
                ////Le joueur a déjà présenté
                if (JoueurStatic.Langue == "FR")
                    central.text = "Présentation des usages";
                else
                    central.text = "Uses presentation";
                nextbutton.gameObject.SetActive(false);
            }
            else
            {
                ////Le joueur n'a pas encore présenté et ce n'est pas encore son tour
                nextbutton.gameObject.SetActive(false);
                if (JoueurStatic.Langue == "FR")
                    central.text = "Vous présentez dans " + attentejoueurcourant + " tours";
                else
                    central.text = "Your present in " + attentejoueurcourant + " turns";
            }
            presentateur_changed = false;
        }
    }

    ////On initialise les textes en focntions de la langue
    void OnEnable()
    {
        if (JoueurStatic.Langue == "FR")
        {
            text.text = "Joueur " + JoueurStatic.Numero.ToString();
        }
        else
        {
            text.text = "Player " + JoueurStatic.Numero;
        }
    }

    ////lorsque le joueur a son bouton actif (si c est le présentateur) et qu'il appuie dessus,
    //// on passe soit au présentateur suivant, soit on passe au débat si c'est le dernier joueur à
    ////présenter un personnage
    private void ButtonClicked()
    {
        nextbutton.gameObject.SetActive(false);
        if ((nextbutton.transform.GetChild(0).gameObject.GetComponent<Text>().text == "Commencer le débat") || 
            (nextbutton.transform.GetChild(0).gameObject.GetComponent<Text>().text == "Start debate"))
        {
            ////On indique au serveur que la présentation du joueur est terminée
            MyNetworkMessage msg = new MyNetworkMessage();
            JoueurStatic.Client.Send(debatID, msg);
        }
        else
        {
            ////On indique au serveur que la présentation du joueur est terminée
            MyNetworkMessage msg = new MyNetworkMessage();
            JoueurStatic.Client.Send(joueurID, msg);
        }
    }

    ////Après avoir reçu le message de fin de présentation par le serveur, il envoi aux clients
    ////un message indiquant que le présentateur a changé
    private void OnNextPresReceived(NetworkMessage netMsg)
    {
        var objet = netMsg.ReadMessage<MyNetworkMessage>();
        ////Cette liste contient les tours à attendre pour chaque joueur
        listtourattente = objet.tableau;
        nextbutton.transform.GetChild(0).gameObject.GetComponent<Text>().text = objet.text;
        presentateur_changed = true;
    }

    ////message qui indique qu'on passe au canvas suivant
    private void OnDebatReceived(NetworkMessage netMsg)
    {
        presentateur_changed = false;
        nextbutton.gameObject.SetActive(false);
        canvas_persos_table.SetActive(false);
        canvas_choix_jetons.SetActive(true);
    }
}
