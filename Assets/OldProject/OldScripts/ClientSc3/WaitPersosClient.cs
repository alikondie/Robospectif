﻿using System.Collections;
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

    short debatID = 1006;


    // Start is called before the first frame update
    void Start()
    {
        JoueurStatic.Client.RegisterHandler(debatID, OnDebatReceived);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        text.text = "Joueur : " + JoueurStatic.Numero.ToString();
    }

    private void OnDebatReceived(NetworkMessage netMsg)
    {
        canvas_persos_table.SetActive(false);
        canvas_choix_jetons.SetActive(true);
    }
}
