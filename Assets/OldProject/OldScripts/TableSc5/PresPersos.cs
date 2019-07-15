using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class PresPersos : MonoBehaviour
{
    #region Properties
    short debatID = 1006;
    private Sprite[] persoSprites;
    private int[,] zones;
    private int nbRecu;

    private string en;
    private string fr;

    private int presentateur;
    private Joueur pres;
    #endregion

    #region Inputs
    [SerializeField] GameObject canvas_pres_persos;
    [SerializeField] GameObject canvas_debat;
    [SerializeField] GameObject[] persos;
    [SerializeField] Button button;
    [SerializeField] Text text;
	#endregion
	
	#region Go or components
	#endregion
	
	#region Variables
	#endregion

	#region Unity loop
    void Start()
    {
        if (Partie.Type == "expert")
        {
            fr = "acteur";
            en = "role";
        } else
        {
            fr = "personnage";
            en = "character";
        }
        button.onClick.AddListener(() => ButtonClicked());
    }

    void OnEnable()
    {
        for (int i = 0; i < 6; i++)
        {
            persos[i].transform.GetChild(0).gameObject.SetActive(false);
            persos[i].transform.GetChild(1).gameObject.SetActive(false);
            persos[i].transform.GetChild(2).gameObject.SetActive(false);
            persos[i].transform.GetChild(3).gameObject.SetActive(false);

            if (Tour.PersosDebat[i] != null)
            {
                persos[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Tour.PersosDebat[i];
               /* persos[i].transform.GetChild(0).gameObject.SetActive(true);
                if (Tour.ZonesDebat[i, 0] != 0)
                    persos[i].transform.GetChild(Tour.ZonesDebat[i, 0]).gameObject.SetActive(true);
                if (Tour.ZonesDebat[i, 1] != 0)
                    persos[i].transform.GetChild(Tour.ZonesDebat[i, 1]).gameObject.SetActive(true);*/
            }
            else
                persos[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        if (Partie.Langue == "FR")
            button.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Joueur suivant";
        else
            button.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Next player";

        presentateur = GetNextPres(Partie.JoueurCourant);
    }
	
    void Update()
    {
        foreach (Joueur j in Partie.Joueurs)
        {
            if (presentateur == j.Numero)
            {
                pres = j;
            }
        }

        for (int i = 0; i < persos.Length; i++)
        {
            if (pres.Position == i)
            {
                persos[i].transform.GetChild(0).gameObject.SetActive(true);
                if (Tour.ZonesDebat[i, 0] != 0)
                    persos[i].transform.GetChild(Tour.ZonesDebat[i, 0]).gameObject.SetActive(true);
                if (Tour.ZonesDebat[i, 1] != 0)
                    persos[i].transform.GetChild(Tour.ZonesDebat[i, 1]).gameObject.SetActive(true);
            }
        }

        if (GetNextPres(presentateur) == Partie.JoueurCourant)
            if (Partie.Langue == "FR")
                button.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Commencer le débat";
            else
                button.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Start debate";
        if (Partie.Langue == "FR")
            text.text = "Le joueur " + presentateur + " présente son " + fr;
        else
            text.text = "Player " + presentateur + " presents their " + en;
    }
    #endregion

    #region Methods

    private void ButtonClicked()
    {
        if ((button.transform.GetChild(0).gameObject.GetComponent<Text>().text == "Next player") || (button.transform.GetChild(0).gameObject.GetComponent<Text>().text == "Joueur suivant"))
        {
            presentateur = GetNextPres(presentateur);
        }
        else
        {
            MyStringMessage msg = new MyStringMessage();
            NetworkServer.SendToAll(debatID, msg);
            canvas_pres_persos.SetActive(false);
            canvas_debat.SetActive(true);
        }        
    }

    private int GetNextPres(int i)
    {
        int res;
        if (i - 1 <= 0)
            res = Partie.Joueurs.Count;
        else
            res = i - 1;
        return res;
    }
    #endregion

}
