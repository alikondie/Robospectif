using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class AttentePersos : MonoBehaviour
{
    [SerializeField] GameObject canvas_attente_persos;
    [SerializeField] GameObject canvas_pres_persos;
    [SerializeField] Text text;
    short persosID = 1007;
    private Sprite[] persoSprites;
    private int[,] zones;
    private int nbRecu;

    // Start is called before the first frame update
    void Start()
    {
    
        NetworkServer.RegisterHandler(persosID, OnPersoReceived);
    }

    void OnEnable()
    {
        if (Partie.Langue == "FR")
            text.text = "Choisissez votre\npersonnage !";
        else
            text.text = "Chose your\ncharacter !";

        persoSprites = new Sprite[] { null, null, null, null, null, null };

        zones = new int[6, 2];

        nbRecu = 0;
    }

    private void OnPersoReceived(NetworkMessage netMsg)
    {
        var v = netMsg.ReadMessage<MyPersoMessage>();
        int i = v.numero;
        string s = v.image;
        string spriteString = "FR/Personnages/" + s;
        int zone1 = v.choixZone0;
        int zone2 = v.choixZone1;
        zones[Array.IndexOf(Partie.Positions, i), 0] = zone1;
        zones[Array.IndexOf(Partie.Positions, i), 1] = zone2;
        Sprite sp = Resources.Load<Sprite>(spriteString);
        int j = Array.IndexOf(Partie.Positions, i);
        if (Partie.Positions[j] != Partie.JoueurCourant)
        {
            persoSprites[j] = sp;
        }
        nbRecu++;
    }

    // Update is called once per frame
    void Update()
    {
        if (nbRecu == Partie.Joueurs.Count - 1)
        {
            Tour.PersosDebat = persoSprites;
            Tour.ZonesDebat = zones;
            canvas_attente_persos.SetActive(false);
            canvas_pres_persos.SetActive(true);
        }
    }
}
