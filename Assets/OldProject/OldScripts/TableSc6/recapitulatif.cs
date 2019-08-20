﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class recapitulatif : MonoBehaviour
{
    [SerializeField] GameObject canvas_debat;
    [SerializeField] GameObject canvas_choix_vainqueur;
    [SerializeField] GameObject canvas_fin_tour;
    [SerializeField] GameObject canvas_fin;
    
    [SerializeField] GameObject[] joueurs;
    [SerializeField] GameObject[] joueursdebat;

    [SerializeField] Button bouton_retour;
    [SerializeField] Button button;

    private Sprite[] images;

    short retourID = 1023;
    short nextID = 1015;

    // Start is called before the first frame update
    void Start()
    {
        bouton_retour.onClick.AddListener(() => BoutonRetourClicked());
        button.onClick.AddListener(() => ButtonClicked());
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < joueurs.Length; i++)
        {
            if (joueurs[i].transform.GetChild(2).gameObject.activeSelf)
            {
                button.gameObject.SetActive(true);
            }
        }
    }

    private void ButtonClicked()
    {
        int nb = Partie.Joueurs.Count;
        Debug.Log("jetons verts : " + Partie.NbJetonsVert);
        Debug.Log("jetons rouges : " + Partie.NbJetonsRouge);

        for(int k = 0; k < joueurs.Length; k++)
        {
            for (int j = 0; j < 8; j++)
            {
                joueurs[k].transform.GetChild(1).transform.GetChild(j).gameObject.SetActive(false);
                joueursdebat[k].transform.GetChild(2).transform.GetChild(j).gameObject.SetActive(false);
            }

        canvas_choix_vainqueur.SetActive(false);
        if (Partie.Joueurs.Count < 6 && Partie.NbJetonsVert < 8  || Partie.Joueurs.Count == 6 && Partie.NbJetonsVert < 9)
        {
            MyStringMessage endMsg = new MyStringMessage();
            endMsg.s = "end";
            NetworkServer.SendToAll(nextID, endMsg);
            canvas_fin.SetActive(true);
        }

        else
        {
            Partie.Tour++;
            int newPriv = -1;
            foreach (Joueur j in Partie.Joueurs)
            {
                if (j.IsPrive)
                {
                    j.IsPrive = false;
                    j.IsPublic = true;
                    if (j.Numero == Partie.Joueurs.Count)
                        newPriv = 1;
                    else
                        newPriv = j.Numero + 1;
                } else if (j.IsPublic)
                {
                    j.IsPublic = false;
                }
            }

            foreach (Joueur j in Partie.Joueurs)
            {
                if (j.Numero == newPriv)
                    j.IsPrive = true;

                if ((j.Acteurs != null) && (j.Acteurs[0] != null))
                {
                    for (int i = 0; i < j.Acteurs.Length; i++)
                        Main.Global.TabA.addImage(new Main.Image(Main.Global.TabA.Taille, j.Acteurs[i]));
                }
            }
            canvas_fin_tour.SetActive(true);
        }
    }

    private void OnEnable()
    {
        int compteur = 0;
        if (Partie.Langue == "FR")
            button.transform.GetChild(0).GetComponent<Text>().text = "Suivant";
        else
            button.transform.GetChild(0).GetComponent<Text>().text = "Next";
        button.gameObject.SetActive(false);
        foreach (GameObject joueur in joueurs)
        {
            joueur.SetActive(false);
        }

        //GameObject[] objects = (GameObject[])Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Pile");

        foreach (GameObject j in joueurs)
        {
            GameObject objects = j.transform.GetChild(1).gameObject;
            int jetoncount = 0;
            GameObject[] listjetons = new GameObject[objects.transform.childCount];
            for (int i = 0; i < listjetons.Length; i++)
            {
                listjetons[i] = objects.transform.GetChild(i).gameObject;
            }
            if(Tour.PersosDebat[compteur] != null)
            {
                j.SetActive(true);

                j.transform.GetChild(0).GetComponent<Image>().sprite = Tour.PersosDebat[compteur];

                int zone1 = Tour.ZonesDebat[compteur, 0];
                int zone2 = Tour.ZonesDebat[compteur, 1];
                for (int k = 1; k <= 3; k++)
                {
                    if ((k != zone1) && (k != zone2))
                    {
                        j.transform.GetChild(k + 2).gameObject.SetActive(false);
                    }
                }


                for (int k = 0; k < j.transform.GetChild(1).childCount; k++)
                {
                    j.transform.GetChild(1).GetChild(k).gameObject.GetComponent<Image>().sprite = Tour.JetonsDebat[compteur, k];
                    j.transform.GetChild(1).GetChild(k).gameObject.SetActive(Tour.ActivesDebat[compteur, k]);
                }
            }


            for (int k = 0; k < listjetons.Length; k++)
            {
                if (j.activeSelf && listjetons[k].activeSelf)
                {
                    jetoncount++;
                }
            }

            if (jetoncount < 3)
            {
                j.SetActive(false);
            }
            if (jetoncount >= 3)
            {

                Partie.NbJetonsVert -= 3;
                if(jetoncount == 4)
                {
                    j.transform.GetChild(2).gameObject.SetActive(false);
                    Partie.NbJetonsRouge--;
                }
            }
            compteur++;

        }
    }
    private void BoutonRetourClicked()
    {
        MyNetworkMessage msg = new MyNetworkMessage();
        NetworkServer.SendToAll(retourID, msg);
        canvas_choix_vainqueur.SetActive(false);
        canvas_debat.GetComponent<InitDebat>().Retour = true;
        canvas_debat.SetActive(true);
    }
}
