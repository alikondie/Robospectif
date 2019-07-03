﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ValiderVainqueur : MonoBehaviour
{
    [SerializeField] GameObject canvas_choix_vainqueur;
    [SerializeField] GameObject canvas_fin_tour;
    [SerializeField] GameObject canvas_fin;

    private int vainqueur;
  

    [SerializeField] GameObject[] couronnes;
    [SerializeField] GameObject[] joueurs;

    [SerializeField] Button button;

    private Sprite[] images;

    short nextID = 1015;

    // Start is called before the first frame update
    void Start()
    {
        vainqueur = 0;
        button.onClick.AddListener(() => ButtonClicked());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(v, Vector2.zero);
            if (hit)
            {
                OnCardClicked(hit.collider.gameObject);
            }

        }
        for (int i = 0; i < joueurs.Length; i++)
        {
            if (joueurs[i].transform.GetChild(2).gameObject.activeSelf)
            {
                vainqueur = i;
                button.gameObject.SetActive(true);
            }
        }
    }

    private void OnCardClicked(GameObject carte)
    {
        Transform joueur = carte.transform.parent;
        joueur.GetChild(2).gameObject.SetActive(true);
        for (int i = 0; i < joueurs.Length; i++)
        {
            if (joueurs[i].gameObject != joueur.gameObject)
                joueurs[i].transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    private void ButtonClicked()
    {
        int nb = Partie.Joueurs.Count;
        int i = Array.IndexOf(Partie.Positions, vainqueur);
        foreach(Joueur j in Partie.Joueurs)
        {
            if (j.Position == vainqueur)
            {
                j.NbCouronnes++;
            }
        }
        canvas_choix_vainqueur.SetActive(false);
        if (nb != Partie.Tour)
        {
            Partie.Tour++;
            Partie.JoueurCourant++;
            if (Partie.JoueurCourant > Partie.Joueurs.Count)
            {
                Partie.JoueurCourant = 1;
            }
            canvas_fin_tour.SetActive(true);
            //SceneManager.LoadScene("Scene_fin_tour");
        }
        else
        {
            MyStringMessage endMsg = new MyStringMessage();
            endMsg.s = "end";
            NetworkServer.SendToAll(nextID, endMsg);
            canvas_fin.SetActive(true);
        }
    }

    private void OnEnable()
    {
        if (Partie.Langue == "FR")
            button.transform.GetChild(0).GetComponent<Text>().text = "Valider";
        else
            button.transform.GetChild(0).GetComponent<Text>().text = "Confirm";
        button.gameObject.SetActive(false);
        foreach (GameObject joueur in joueurs)
        {
            joueur.transform.GetChild(2).gameObject.SetActive(false);
            for (int i = 3; i < joueur.transform.childCount; i++)
            {
                joueur.transform.GetChild(i).gameObject.SetActive(true);
            }
            joueur.SetActive(false);
        }

        for (int i = 0; i < joueurs.Length; i++)
        {
            if (Tour.PersosDebat[i] != null)
            {
                joueurs[i].transform.GetChild(0).GetComponent<Image>().sprite = Tour.PersosDebat[i];


                int zone1 = Tour.ZonesDebat[i, 0];
                int zone2 = Tour.ZonesDebat[i, 1];
                for (int j = 1; j <= 3; j++)
                {
                    if ((j != zone1) && (j != zone2))
                    {
                        joueurs[i].transform.GetChild(j + 2).gameObject.SetActive(false);
                    }
                }


                for (int j = 0; j < joueurs[i].transform.GetChild(1).childCount; j++)
                {
                    joueurs[i].transform.GetChild(1).GetChild(j).gameObject.GetComponent<Image>().sprite = Tour.JetonsDebat[i, j];
                    joueurs[i].transform.GetChild(1).GetChild(j).gameObject.SetActive(Tour.ActivesDebat[i, j]);
                }

                joueurs[i].SetActive(true);
            }
        }

        int pos = Array.IndexOf(Partie.Positions, Partie.JoueurCourant) + 1;
        joueurs[pos - 1].SetActive(false);
    }
}
