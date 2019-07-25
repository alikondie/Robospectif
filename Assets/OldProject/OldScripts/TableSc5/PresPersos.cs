using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using UnityEditor;

public class PresPersos : MonoBehaviour
{
    #region Properties
    short debatID = 1006;
    short joueurID = 1019;
    short presentateurID = 1020;
    private Sprite[] persoSprites;
    private int[,] zones;
    private int[] listtourattente;
    private int nbRecu;

    private string en;
    private string fr;

    private string textbutton;

    private int presentateur;
    private Joueur pres;
    private Joueur next;
    #endregion

    #region Inputs
    [SerializeField] GameObject canvas_pres_vehicule;
    [SerializeField] GameObject canvas_pres_persos;
    [SerializeField] GameObject canvas_debat;
    [SerializeField] GameObject[] persos;
    [SerializeField] GameObject[] cartes;
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
        //button.onClick.AddListener(() => ButtonClicked());
        NetworkServer.RegisterHandler(debatID, OnDebatReceived);
        NetworkServer.RegisterHandler(joueurID, OnReceivedJoueurFinished);
    }

    void OnEnable()
    {
        listtourattente = new int[Partie.Joueurs.Count];
        canvas_pres_vehicule.SetActive(true);
        canvas_pres_vehicule.GetComponent<CanvasScaler>().referenceResolution = new Vector2(10000f, 10000f);
        canvas_pres_vehicule.transform.GetChild(0).gameObject.SetActive(false);
        canvas_pres_vehicule.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
        canvas_pres_vehicule.GetComponent<Initialisation>().enabled = false;
        canvas_pres_vehicule.transform.GetChild(1).GetChild(0).GetChild(7).GetComponent< BoxCollider2D>().enabled = false;
        canvas_pres_vehicule.transform.GetChild(1).GetChild(0).GetChild(7).GetComponent<Mouvement_carte>().enabled = false;
        foreach (GameObject carte in cartes)
        {
            carte.GetComponent<BoxCollider2D>().enabled = false;
            carte.GetComponent<Mouvement_carte>().enabled = false;
        }
        if (Partie.Type == "expert")
        {
            fr = "acteur";
            en = "role";
        }
        else
        {
            fr = "personnage";
            en = "character";
        }
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
            //button.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Joueur suivant";
            textbutton = "Joueur suivant";
        else
            textbutton = "Next player";
            //button.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Next player";
        if (Partie.Type == "expert")
        {
            foreach (Joueur j in Partie.Joueurs)
            {
                if (j.IsPublic)
                {
                    presentateur = GetNextPres(j.Numero);
                }
            }
        }
        else
        {
            presentateur = GetNextPres(Partie.JoueurCourant);
            InitTourAttenteList();
            for (int i = 0; i < listtourattente.Length; i++)
                Debug.Log("Joueur " + (i + 1) + " : temps d'attente > " + listtourattente[i]);
            MyNetworkMessage msg = new MyNetworkMessage();
            msg.tableau = listtourattente;
            msg.text = textbutton;
            NetworkServer.SendToAll(presentateurID, msg);
        }
    }
	
    void Update()
    {
        checkifvehiculeclicked();

        foreach (Joueur j in Partie.Joueurs)
        {
            if (presentateur == j.Numero)
            {
                pres = j;
            }
            if (GetNextPres(presentateur) == j.Numero)
            {
                next = j;
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

        if (Partie.Langue == "FR")
            text.text = "Le joueur " + presentateur + " présente son " + fr;
        else
            text.text = "Player " + presentateur + " presents their " + en;
    }
    #endregion

    #region Methods

    private int GetNextPres(int i)
    {
        int res;
        if (i - 1 <= 0)
            res = Partie.Joueurs.Count;

        else
            res = i - 1;
        return res;
    }

    private void checkifvehiculeclicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x < 1000 && Input.mousePosition.x > 900 && 
                Input.mousePosition.y < 600 && Input.mousePosition.y > 500 && 
                canvas_pres_vehicule.GetComponent<CanvasScaler>().referenceResolution == new Vector2(10000f, 10000f))
            {
                canvas_pres_vehicule.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920f, 1080f);
            }
            else if (Input.mousePosition.x < 1500 && Input.mousePosition.x > 420 && 
                     Input.mousePosition.y < 1070 && Input.mousePosition.y > 0 &&
                     canvas_pres_vehicule.GetComponent<CanvasScaler>().referenceResolution == new Vector2(1920f, 1080f))
            {
                canvas_pres_vehicule.GetComponent<CanvasScaler>().referenceResolution = new Vector2(10000f, 10000f);
            }
        }
    }

    private void OnReceivedJoueurFinished(NetworkMessage netMsg)
    {
        presentateur = GetNextPres(presentateur);
        for (int k = 0; k < listtourattente.Length; k++)
        {
            listtourattente[k]--;
        }

        if ((GetNextPres(presentateur) == Partie.JoueurCourant) || next.IsPrive)
        {
            if (Partie.Langue == "FR")
            {
                textbutton = "Commencer le débat";
            }

            else
            {
                textbutton = "Start debate";
            }
        }
        MyNetworkMessage msg = new MyNetworkMessage();
        msg.tableau = listtourattente;
        msg.text = textbutton;
        NetworkServer.SendToAll(presentateurID, msg);
    }

    private void OnDebatReceived(NetworkMessage netMsg)
    {
        canvas_pres_persos.SetActive(false);
        canvas_debat.SetActive(true);
    }

    private void InitTourAttenteList()
    {
        int compteur = Partie.Joueurs.Count - 2;
        listtourattente[Partie.JoueurCourant - 1] = -1;
        for (int k = Partie.JoueurCourant; k < listtourattente.Length + Partie.JoueurCourant - 1; k++)
        {
            listtourattente[k % listtourattente.Length] = compteur;
            compteur--;
        }
    }

    #endregion

}
