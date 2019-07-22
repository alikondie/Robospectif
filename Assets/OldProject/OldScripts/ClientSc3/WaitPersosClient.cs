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
    private string fr;
    private string en;


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
        if (JoueurStatic.Type == "expert")
        {
            fr = "acteurs";
            en = "Roles";
        }
        else
        {
            fr = "personnages";
            en = "Characters";
        }
        if (JoueurStatic.Langue == "FR")
        {
            text.text = "Joueur " + JoueurStatic.Numero.ToString();
            central.text = "Présentation des\n" + fr;
        } else
        {
            text.text = "Player " + JoueurStatic.Numero;
            central.text = en + "\npresentation";
        }
    }

    private void OnDebatReceived(NetworkMessage netMsg)
    {
        canvas_persos_table.SetActive(false);
        canvas_choix_jetons.SetActive(true);
    }
}
