using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

////Ce script est utilisé par le canvas d'attente du choix du vainqueur par le présentateur (côté client)
////, il permet soit de passer au tour suivant, soit de terminer la partie.
////Le canvas côté serveur est composé de 3 boutons, un pour continuer la partie, un autre pour la terminer, 
////et un dernier pour retourner au débat (ajouté suite au constat que les joueurs appuient parfois sur le
//// bouton pour passer à la suite, sans avoir terminé le débat).
////Ce script va donc réagir au bouton sur lequel les joueurs ont décidé d'appuyer.

public class AttenteFin : MonoBehaviour
{
    [SerializeField] GameObject canvas_fin_partie;
    [SerializeField] GameObject canvas_vainqueur;
    [SerializeField] GameObject canvas_pres_robot;
    [SerializeField] GameObject canvas_choix_jetons;
    [SerializeField] Text text;
    [SerializeField] Text central;

    private int position;

    short nextID = 1015;
    short retourID = 1023;
    short suiteID = 1026;

    void Start()
    {
        JoueurStatic.Client.RegisterHandler(nextID, onWaitReceived);
        JoueurStatic.Client.RegisterHandler(retourID, onRetourReceived);
        JoueurStatic.Client.RegisterHandler(suiteID, onSuiteReceived);
    }

    ////Cette méthode de réception de message venant du serveur permet de passer soit au canvas de fin de partie 
    ////(si le bouton correspondant a été cliqué sur la table numérique, le serveur envoie le message comportant 
    ////le string "end"), soit au canvas de présentation du robot du présentateur suivant (message reçu : "next")
    private void onWaitReceived(NetworkMessage netMsg)
    {
        //on réinitialise ces variables car le tour est fini, on ne pourra donc plus revenir au débat avant le tour suivant
        JoueurStatic.SocieteCompteur = 0;
        JoueurStatic.PlaneteCompteur = 0;
        JoueurStatic.UsageCompteur = 0;

        string msg = netMsg.ReadMessage<MyStringMessage>().s;
        if (msg.Equals("next"))
        {
            canvas_vainqueur.SetActive(false);
            canvas_pres_robot.SetActive(true);
        }
        else
        {
            canvas_vainqueur.SetActive(false);
            canvas_fin_partie.SetActive(true);
        }
    }

    ////à l'activation du canvas de fin de tour côté client, l'affichage dynamique se met à jour si la langue 
    ////choisie est l'anglais
    void OnEnable()
    {
        if (JoueurStatic.Langue == "FR")
        {
            text.text = "Joueur " + JoueurStatic.Numero.ToString();
            central.text = "Attente du choix du\ncréateur du robot";
        } else
        {
            text.text = "Player " + JoueurStatic.Numero.ToString();
            central.text = "Waiting for the robot\ncreator's choice";
        }
    }

    ////Méthode de réception de message suite à l'appuie du bouton retour côté serveur, donc désactive 
    ////le canvas courant, et réactive le précédent.
    private void onRetourReceived(NetworkMessage netMsg)
    {
        canvas_vainqueur.SetActive(false);
        canvas_choix_jetons.SetActive(true);
    }

    ////Méthode de réception de message suite à l'appuie du bouton valider côté serveur, qui implique la fin du tour,
    ////donc pas de nouveau canvas côté client, car on change seulement le texte affiché pour indiquer que le tour est fini
    private void onSuiteReceived(NetworkMessage netMsg)
    {
        if (JoueurStatic.Langue == "FR")
        {
            central.text = "Fin du tour";
        }
        else
        {
            central.text = "End of turn";
        }
    }
}
