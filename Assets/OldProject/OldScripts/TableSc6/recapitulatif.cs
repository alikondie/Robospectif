using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class recapitulatif : MonoBehaviour
{
    [SerializeField] GameObject canvas_choix_vainqueur;
    [SerializeField] GameObject canvas_fin_tour;
    [SerializeField] GameObject canvas_fin;
    
    [SerializeField] GameObject[] joueurs;

    [SerializeField] Button button;

    private Sprite[] images;

    short nextID = 1015;

    // Start is called before the first frame update
    void Start()
    {
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


            for (int k = 0; 3 < k; k++)
            {
                if (listjetons[k].activeSelf)
                {
                    jetoncount++;
                }
            }

            if (jetoncount == 3)
            {
                j.SetActive(true);
            }
            compteur++;

            Debug.Log("joueur " + compteur + " possède " + jetoncount + " jetons");

        }
    }
}
