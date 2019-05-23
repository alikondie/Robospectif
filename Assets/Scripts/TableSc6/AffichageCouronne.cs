using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AffichageCouronne : MonoBehaviour
{
    public int JoueurCourant;
    //public static Partie partieCourante;

    public GameObject personnage1;
    public GameObject environnement_1;
    public GameObject couronne_1;
    public GameObject personnage2;
    public GameObject environnement_2;
    public GameObject couronne_2;
    public GameObject personnage3;
    public GameObject environnement_3;
    public GameObject couronne_3;
    public GameObject personnage4;
    public GameObject environnement_4;
    public GameObject couronne_4;
    public GameObject personnage5;
    public GameObject environnement_5;
    public GameObject couronne_5;
    public GameObject personnage6;
    public GameObject environnement_6;
    public GameObject couronne_6;

    public Sprite[] images;



    // Start is called before the first frame update
    void Start()
    {
        JoueurCourant = Partie.JoueurCourant;

        images = InitDebat.envoi;
        
        personnage1.gameObject.SetActive(false);
        personnage2.gameObject.SetActive(false);
        personnage3.gameObject.SetActive(false);
        personnage4.gameObject.SetActive(false);
        personnage5.gameObject.SetActive(false);
        personnage6.gameObject.SetActive(false);

        for (int i = 0; i < 6; i++)
        {
            Debug.Log("images[" + i + "] = " + images[i]);
        }

        personnage1.GetComponent<SpriteRenderer>().sprite = images[0];
        personnage1.gameObject.SetActive(true);
        personnage2.GetComponent<SpriteRenderer>().sprite = images[1];
        personnage2.gameObject.SetActive(true);
        personnage3.GetComponent<SpriteRenderer>().sprite = images[2];
        personnage3.gameObject.SetActive(true);
        personnage4.GetComponent<SpriteRenderer>().sprite = images[3];
        personnage4.gameObject.SetActive(true);
        personnage5.GetComponent<SpriteRenderer>().sprite = images[4];
        personnage5.gameObject.SetActive(true);
        personnage6.GetComponent<SpriteRenderer>().sprite = images[5];
        personnage6.gameObject.SetActive(true);

        int pos = -1;

        Debug.Log("capacité : " + Partie.Joueurs.Count);

        for (int i = 0; i < Partie.Joueurs.Count; i++)
        {
            if (Partie.Joueurs[i].Numero == JoueurCourant)
            {
                pos = Partie.Joueurs[i].Position;
            }
        }

        Debug.Log(" pos : " + pos);

        switch (pos)
        {

            case 1:

                Destroy(personnage1);
                Destroy(environnement_1);
                couronne_1.SetActive(false);
                break;

            case 2:

                Destroy(personnage2);
                Destroy(environnement_2);
                couronne_2.SetActive(false);
                break;


            case 3:
                Destroy(personnage3);
                Destroy(environnement_3);
                couronne_3.SetActive(false);
                break;


            case 4:
                Destroy(personnage4);
                Destroy(environnement_4);
                couronne_4.SetActive(false);
                break;

            case 5:
                Destroy(personnage5);
                Destroy(environnement_5);
                couronne_5.SetActive(false);
                break;

            case 6:
                Destroy(personnage6);
                Destroy(environnement_6);
                couronne_6.SetActive(false);
                break;

        }
        


        /*for (int i = 0; i < partieCourante.Joueurs.Length(); i++)
        {
            if (partieCourante.Joueurs[i] == 0)
            {
                switch (i+1)
                {

                    case 1:

                        Destroy(personnage1);
                        Destroy(environnement_1);
                        Destroy(couronne_1);
                        break;

                    case 2:

                        Destroy(personnage2);
                        Destroy(environnement_2);
                        Destroy(couronne_2);

                        break;


                    case 3:
                        Destroy(personnage3);
                        Destroy(environnement_3);
                        Destroy(couronne_3);
                        break;


                    case 4:
                        Destroy(personnage4);
                        Destroy(environnement_4);
                        Destroy(couronne_4);

                        break;

                    case 5:
                        Destroy(personnage5);
                        Destroy(environnement_5);
                        Destroy(couronne_5);

                        break;

                    case 6:
                        Destroy(personnage6);
                        Destroy(environnement_6);
                        Destroy(couronne_6);

                        break;
                }
            }
        }*/

    }

    private void Update()
    {

        
    }
}
