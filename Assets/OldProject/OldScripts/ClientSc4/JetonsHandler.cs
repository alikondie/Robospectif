﻿using System;
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

    short waitID = 1006;

    [SerializeField] Button usageVert;
    [SerializeField] Button usageRouge;
    [SerializeField] Button societeVert;
    [SerializeField] Button societeRouge;
    [SerializeField] Button planeteVert;
    [SerializeField] Button planeteRouge;

    private int usageCompteur;
    private int societeCompteur;
    private int planeteCompteur;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "Joueur " + selectUser.positionStatic;
        JoueurStatic.Client.RegisterHandler(waitID, OnWaitReceived);

        usageCompteur = 0;
        societeCompteur = 0;
        planeteCompteur = 0;
        usageVert.onClick.AddListener(() => OnUsageClicked());       
        usageRouge.onClick.AddListener(() => OnUsageClicked());       
        societeVert.onClick.AddListener(() => OnSocieteClicked());       
        societeRouge.onClick.AddListener(() => OnSocieteClicked());       
        planeteVert.onClick.AddListener(() => OnPlaneteClicked());       
        planeteRouge.onClick.AddListener(() => OnPlaneteClicked());       
    }

    private void OnPlaneteClicked()
    {
        planeteCompteur++;
    }

    private void OnSocieteClicked()
    {
        societeCompteur++;
    }

    private void OnUsageClicked()
    {
        usageCompteur++;
    }

    private void OnWaitReceived(NetworkMessage netMsg)
    {
        canvas_choix_jetons.SetActive(false);
        canvas_vainqueur.SetActive(true);
        //SceneManager.LoadScene("scenePreEnd");
    }

    // Update is called once per frame
    void Update()
    {
        if (usageCompteur >= 2)
        {
            usageVert.gameObject.SetActive(false);
            usageRouge.gameObject.SetActive(false);
        }

        if (societeCompteur >= 2)
        {
            societeVert.gameObject.SetActive(false);
            societeRouge.gameObject.SetActive(false);
        }

        if (planeteCompteur >= 2)
        {
            planeteVert.gameObject.SetActive(false);
            planeteRouge.gameObject.SetActive(false);
        }
    }
}
