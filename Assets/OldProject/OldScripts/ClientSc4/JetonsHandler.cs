using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

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
    short jeton = 1010;

    [SerializeField] Button usageVert;
    [SerializeField] Button usageRouge;
    [SerializeField] Button societeVert;
    [SerializeField] Button societeRouge;
    [SerializeField] Button planeteVert;
    [SerializeField] Button planeteRouge;

    private int usageCompteur;
    private int societeCompteur;
    private int planeteCompteur;
    private bool isPresTime;
    // Start is called before the first frame update
    void Start()
    {
        JoueurStatic.Client.RegisterHandler(vainqueurID, OnWaitReceived);
        JoueurStatic.Client.RegisterHandler(stopID, OnStopReceived);
        JoueurStatic.Client.RegisterHandler(goID, OnGoReceived);
        JoueurStatic.Client.RegisterHandler(presID, OnPresReceived);

        usageVert.onClick.AddListener(() => OnUsageClicked(usageVert.GetComponent<Image>().sprite));       
        usageRouge.onClick.AddListener(() => OnUsageClicked(usageRouge.GetComponent<Image>().sprite));       
        societeVert.onClick.AddListener(() => OnSocieteClicked(societeVert.GetComponent<Image>().sprite));       
        societeRouge.onClick.AddListener(() => OnSocieteClicked(societeRouge.GetComponent<Image>().sprite));       
        planeteVert.onClick.AddListener(() => OnPlaneteClicked(planeteVert.GetComponent<Image>().sprite));       
        planeteRouge.onClick.AddListener(() => OnPlaneteClicked(planeteRouge.GetComponent<Image>().sprite));

        isPresTime = false;
    }

    private void OnGoReceived(NetworkMessage netMsg)
    {
        if(!isPresTime && JoueurStatic.Numero == netMsg.ReadMessage<MyNetworkMessage>().message)
        {
            OnStopReceived(netMsg);
        }
        else if (!isPresTime || JoueurStatic.Numero == netMsg.ReadMessage<MyNetworkMessage>().message)
        {
            if (usageCompteur < 2)
            {
                usageVert.gameObject.SetActive(true);
                usageRouge.gameObject.SetActive(true);
            }
            if (societeCompteur < 2)
            {
                societeVert.gameObject.SetActive(true);
                societeRouge.gameObject.SetActive(true);
            }
            if (planeteCompteur < 2)
            {
                planeteVert.gameObject.SetActive(true);
                planeteRouge.gameObject.SetActive(true);
            }
        }
    }

    private void OnStopReceived(NetworkMessage netMsg)
    {
        AllFalse();
    }

    private void AllFalse()
    {
        usageVert.gameObject.SetActive(false);
        usageRouge.gameObject.SetActive(false);
        societeVert.gameObject.SetActive(false);
        societeRouge.gameObject.SetActive(false);
        planeteVert.gameObject.SetActive(false);
        planeteRouge.gameObject.SetActive(false);
    }

    private void OnPresReceived(NetworkMessage netMsg)
    {
        isPresTime = true;
        if (JoueurStatic.Numero == netMsg.ReadMessage<MyNetworkMessage>().message)
        {
            usageVert.gameObject.SetActive(true);
            usageRouge.gameObject.SetActive(true);
            societeVert.gameObject.SetActive(true);
            societeRouge.gameObject.SetActive(true);
            planeteVert.gameObject.SetActive(true);
            planeteRouge.gameObject.SetActive(true);
        }
    }

    private void OnPlaneteClicked(Sprite s)
    {
        envoyer(s);
        AllFalse();
        planeteCompteur++;

        if (planeteCompteur >= 2)
        {
            planeteVert.gameObject.SetActive(false);
            planeteRouge.gameObject.SetActive(false);
        }
    }

    private void OnSocieteClicked(Sprite s)
    {
        envoyer(s);
        AllFalse();
        societeCompteur++;

        if (societeCompteur >= 2)
        {
            societeVert.gameObject.SetActive(false);
            societeRouge.gameObject.SetActive(false);
        }
    }

    private void OnUsageClicked(Sprite s)
    {
        envoyer(s);
        AllFalse();
        usageCompteur++;

        if (usageCompteur >= 2)
        {
            usageVert.gameObject.SetActive(false);
            usageRouge.gameObject.SetActive(false);
        }
    }

    private void OnWaitReceived(NetworkMessage netMsg)
    {
        isPresTime = false;
        canvas_choix_jetons.SetActive(false);
        canvas_vainqueur.SetActive(true);
        usageVert.gameObject.SetActive(true);
        usageRouge.gameObject.SetActive(true);
        societeVert.gameObject.SetActive(true);
        societeRouge.gameObject.SetActive(true);
        planeteVert.gameObject.SetActive(true);
        planeteRouge.gameObject.SetActive(true);
    }

    public void envoyer(Sprite s)
    {
        MyJetonMessage msg = new MyJetonMessage();
        msg.joueur = JoueurStatic.Numero;
        msg.sprite = s.name;
        JoueurStatic.Client.Send(jeton, msg);
    }

    // Update is called once per frame
    void Update()
    {
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

        usageCompteur = 0;

        societeCompteur = 0;

        planeteCompteur = 0;
    }
}
