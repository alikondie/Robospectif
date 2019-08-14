using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class AttenteActeurs : MonoBehaviour
{
    [SerializeField] GameObject canvas_attente;
    [SerializeField] GameObject canvas_persos;
    [SerializeField] Text joueur;
    [SerializeField] Text central;
    [SerializeField] Image decideur;

    short debatclientID = 1021;

    // Start is called before the first frame update
    void Start()
    {
        JoueurStatic.Client.RegisterHandler(debatclientID, OnActeurSelected);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (JoueurStatic.Langue == "FR")
        {
            joueur.text = "Joueur " + JoueurStatic.Numero;
            central.text = "Sélection des\nacteurs";
        } else
        {
            joueur.text = "Player " + JoueurStatic.Numero;
            central.text = "Roles selection";
        }
        if (JoueurStatic.IsPrive)
            decideur.sprite = Resources.Load<Sprite>(JoueurStatic.Langue + "/Decideurs/DecideurPrive");
        else
            decideur.sprite = Resources.Load<Sprite>(JoueurStatic.Langue + "/Decideurs/DecideurPublic");
    }

    private void OnActeurSelected(NetworkMessage netMsg)
    {
        canvas_attente.SetActive(false);
        canvas_persos.SetActive(true);
    }
}
