using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class PresPersos : MonoBehaviour
{
    #region Properties
    short persosID = 1007;
    private Sprite[] persoSprites;
    private int[,] zones;
    private int nbRecu;
    #endregion

    #region Inputs
    [SerializeField] GameObject canvas_pres_persos;
    [SerializeField] GameObject canvas_debat;
    [SerializeField] GameObject[] persos;
    [SerializeField] Button button;
	#endregion
	
	#region Go or components
	#endregion
	
	#region Variables
	#endregion

	#region Unity loop
    void Start()
    {
        button.onClick.AddListener(() => ButtonClicked());
        NetworkServer.RegisterHandler(persosID, OnPersoReceived);
    }

    void OnEnable()
    {
        persoSprites = new Sprite[] { null, null, null, null, null, null };

        zones = new int[6, 2];

        nbRecu = 0;

        button.gameObject.SetActive(false);
    }
	
    void Update()
    {
        if (nbRecu == Partie.Joueurs.Count - 1)
        {
            canvas_pres_persos.SetActive(false);
            canvas_debat.SetActive(false);
        }
    }
    #endregion

    #region Methods

    private void ButtonClicked()
    {
        throw new NotImplementedException();
    }

    private void OnPersoReceived(NetworkMessage netMsg)
    {
        var v = netMsg.ReadMessage<MyPersoMessage>();
        int i = v.numero;
        string s = v.image;
        string spriteString = "image/Personnages/" + s;
        int zone1 = v.choixZone0;
        int zone2 = v.choixZone1;
        zones[Array.IndexOf(Partie.Positions, i), 0] = zone1;
        zones[Array.IndexOf(Partie.Positions, i), 1] = zone2;
        Sprite sp = Resources.Load<Sprite>(spriteString);
        for (int j = 0; j < 6; j++)
        {
            if ((Partie.Positions[j] == i) && (Partie.Positions[j] != Partie.JoueurCourant))
            {
                persoSprites[j] = sp;
                persos[j].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = sp;
                if (zone1 != 0)
                    persos[j].transform.GetChild(zone1).gameObject.SetActive(true);
                if (zone2 != 0)
                    persos[j].transform.GetChild(zone2).gameObject.SetActive(true);
                persos[j].transform.GetChild(0).gameObject.SetActive(true);

            }
        }
        nbRecu++;
    }
    #endregion

}
