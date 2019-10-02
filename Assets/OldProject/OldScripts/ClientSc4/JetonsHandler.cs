using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

////main script de choix jeton. Il gère toute la scène (affichage des jetons dispo, etc.)
public class JetonsHandler : MonoBehaviour
{
    [SerializeField] GameObject canvas_choix_jetons;
    [SerializeField] GameObject canvas_vainqueur;

    [SerializeField] Text text;

    short vainqueurID = 1008;
    short stopID = 1012;
    short goID = 1013;
    short presID = 1017;
    short hasstartID = 1018;
    short RetourID = 1022;
    short jeton = 1010;

    [SerializeField] Button usageVert;
    [SerializeField] Button usageRouge;
    [SerializeField] Button societeVert;
    [SerializeField] Button societeRouge;
    [SerializeField] Button planeteVert;
    [SerializeField] Button planeteRouge;

    private bool isPresTime;

    void Start()
    {
        JoueurStatic.Client.RegisterHandler(vainqueurID, OnWaitReceived);
        JoueurStatic.Client.RegisterHandler(stopID, OnStopReceived);
        JoueurStatic.Client.RegisterHandler(goID, OnGoReceived);
        JoueurStatic.Client.RegisterHandler(presID, OnPresReceived);
        JoueurStatic.Client.RegisterHandler(RetourID, OnRetourReceived);

        ////les jetons sont des boutons
        usageVert.onClick.AddListener(() => OnUsageClicked(usageVert.GetComponent<Image>().sprite));       
        usageRouge.onClick.AddListener(() => OnUsageClicked(usageRouge.GetComponent<Image>().sprite));       
        societeVert.onClick.AddListener(() => OnSocieteClicked(societeVert.GetComponent<Image>().sprite));       
        societeRouge.onClick.AddListener(() => OnSocieteClicked(societeRouge.GetComponent<Image>().sprite));       
        planeteVert.onClick.AddListener(() => OnPlaneteClicked(planeteVert.GetComponent<Image>().sprite));       
        planeteRouge.onClick.AddListener(() => OnPlaneteClicked(planeteRouge.GetComponent<Image>().sprite));

        isPresTime = false;
    }

    //// message permettant d'activer les jetons pour les joueurs hormis le présentateur du véhicule
    private void OnGoReceived(NetworkMessage netMsg)
    {
        if(!isPresTime && JoueurStatic.Numero == netMsg.ReadMessage<MyNetworkMessage>().message)
        {
            OnStopReceived(netMsg);
        }
        else if (!isPresTime || JoueurStatic.Numero == netMsg.ReadMessage<MyNetworkMessage>().message)
        {
            SetTrue();
        }
    }

    ////lorsque qu'un joueur a choisi un jeton, plus aucun joueur ne peut poser de jeton
    //// tant que le jeton n'a pas été attribué sur le serveur
    private void OnStopReceived(NetworkMessage netMsg)
    {
        AllFalse();
    }

    ////appui sur le bouton retour de la scène suivante
    private void OnRetourReceived(NetworkMessage netMsg)
    {
        isPresTime = false;
        if(JoueurStatic.Numero == netMsg.ReadMessage<MyNetworkMessage>().message)
        {
            //// si le joueur est le présentateur
            AllFalse();
        }
        else
        {
            SetTrue();
        }
    }

    ////on "désaffiche" tous les jetons
    private void AllFalse()
    {
        usageVert.gameObject.SetActive(false);
        usageRouge.gameObject.SetActive(false);
        societeVert.gameObject.SetActive(false);
        societeRouge.gameObject.SetActive(false);
        planeteVert.gameObject.SetActive(false);
        planeteRouge.gameObject.SetActive(false);
    }

    //// on affiche tous les jetons dispo
    private void SetTrue()
    {
        ////les 3 variables compteur indiquent le nombre de jetons de chaque type posés,ceci est utile
        ////qu'en cas d'usage du bouton retour de la scène suivante.
        if (JoueurStatic.UsageCompteur < 2)
        {
            usageVert.gameObject.SetActive(true);
            usageRouge.gameObject.SetActive(true);
        }
        if (JoueurStatic.SocieteCompteur < 2)
        {
            societeVert.gameObject.SetActive(true);
            societeRouge.gameObject.SetActive(true);
        }
        if (JoueurStatic.PlaneteCompteur < 2)
        {
            planeteVert.gameObject.SetActive(true);
            planeteRouge.gameObject.SetActive(true);
        }
    }

    private void OnPresReceived(NetworkMessage netMsg)
    {
        isPresTime = true;
        if (JoueurStatic.Numero == netMsg.ReadMessage<MyNetworkMessage>().message)
        {
            SetTrue();
        }
    }

    ////les 3 fonctions suivantes envoi sur le serveur le jeton cliqué
    //// et gère le nombre de jetons à afficher selon le nombre de jeton 
    //// de chaque type déjà utilisé
    private void OnPlaneteClicked(Sprite s)
    {
        envoyer(s);
        AllFalse();
        JoueurStatic.PlaneteCompteur++;

        if (JoueurStatic.PlaneteCompteur >= 2)
        {
            planeteVert.gameObject.SetActive(false);
            planeteRouge.gameObject.SetActive(false);
        }
    }

    private void OnSocieteClicked(Sprite s)
    {
        envoyer(s);
        AllFalse();
        JoueurStatic.SocieteCompteur++;

        if (JoueurStatic.SocieteCompteur >= 2)
        {
            societeVert.gameObject.SetActive(false);
            societeRouge.gameObject.SetActive(false);
        }
    }

    private void OnUsageClicked(Sprite s)
    {
        envoyer(s);
        AllFalse();
        JoueurStatic.UsageCompteur++;

        if (JoueurStatic.UsageCompteur >= 2)
        {
            usageVert.gameObject.SetActive(false);
            usageRouge.gameObject.SetActive(false);
        }
    }

    ////apres l appui sur le bouton de choix vainqueur sur le serveur pour prévenir les clients
    private void OnWaitReceived(NetworkMessage netMsg)
    {
        isPresTime = false;
        canvas_choix_jetons.SetActive(false);
        canvas_vainqueur.SetActive(true);
        SetTrue();
    }

    ////on paramètre l'envoi en fonction du jeton cliqué
    public void envoyer(Sprite s)
    {
        MyJetonMessage msg = new MyJetonMessage();
        msg.joueur = JoueurStatic.Numero;
        msg.sprite = s.name;
        JoueurStatic.Client.Send(jeton, msg);
    }

    void OnEnable()
    {
        MyNetworkMessage hasstart = new MyNetworkMessage();
        JoueurStatic.Client.Send(hasstartID, hasstart);
        if (JoueurStatic.Langue == "FR")
            text.text = "Joueur " + JoueurStatic.Numero;
        else
            text.text = "Player " + JoueurStatic.Numero;

        usageVert.GetComponent<Image>().sprite = Resources.Load<Sprite>(JoueurStatic.Langue + "/Jetons/usageVert");
        usageRouge.GetComponent<Image>().sprite = Resources.Load<Sprite>(JoueurStatic.Langue + "/Jetons/usageRouge");
        societeVert.GetComponent<Image>().sprite = Resources.Load<Sprite>(JoueurStatic.Langue + "/Jetons/societeVert");
        societeRouge.GetComponent<Image>().sprite = Resources.Load<Sprite>(JoueurStatic.Langue + "/Jetons/societeRouge");
        planeteVert.GetComponent<Image>().sprite = Resources.Load<Sprite>(JoueurStatic.Langue + "/Jetons/planeteVert");
        planeteRouge.GetComponent<Image>().sprite = Resources.Load<Sprite>(JoueurStatic.Langue + "/Jetons/planeteRouge");
    }
}
