using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static Main;
using Image = UnityEngine.UI.Image;

public class Initialisation : MonoBehaviour
{
    [SerializeField] GameObject canvas_plateau_vehicule;
    [SerializeField] GameObject canvas_debat;

    private int pos;

    [SerializeField] Button button;

    short waitID = 1006;

    public static int indice = 0;
    public static Sprite[,] images = new Sprite[6,5];

    [SerializeField] GameObject Plateau;

    [SerializeField] GameObject J1carte1;
    [SerializeField] GameObject J1carte2;
    [SerializeField] GameObject J1carte3;
    [SerializeField] GameObject J1carte4;
    [SerializeField] GameObject J1carte5;

    [SerializeField] GameObject J2carte1;
    [SerializeField] GameObject J2carte2;
    [SerializeField] GameObject J2carte3;
    [SerializeField] GameObject J2carte4;
    [SerializeField] GameObject J2carte5;

    [SerializeField] GameObject J3carte1;
    [SerializeField] GameObject J3carte2;
    [SerializeField] GameObject J3carte3;
    [SerializeField] GameObject J3carte4;
    [SerializeField] GameObject J3carte5;

    [SerializeField] GameObject J4carte1;
    [SerializeField] GameObject J4carte2;
    [SerializeField] GameObject J4carte3;
    [SerializeField] GameObject J4carte4;
    [SerializeField] GameObject J4carte5;

    [SerializeField] GameObject J5carte1;
    [SerializeField] GameObject J5carte2;
    [SerializeField] GameObject J5carte3;
    [SerializeField] GameObject J5carte4;
    [SerializeField] GameObject J5carte5;

    [SerializeField] GameObject J6carte1;
    [SerializeField] GameObject J6carte2;
    [SerializeField] GameObject J6carte3;
    [SerializeField] GameObject J6carte4;
    [SerializeField] GameObject J6carte5;

    private GameObject[,] cartes;
    [SerializeField] GameObject[] cartes1;
    [SerializeField] GameObject[] cartes2;
    [SerializeField] GameObject[] cartes3;
    [SerializeField] GameObject[] cartes4;
    [SerializeField] GameObject[] cartes5;
    [SerializeField] GameObject[] cartes6;


    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() => ButtonClicked());        
    }

    void OnEnable()
    {
        cartes = new GameObject[6,cartes1.Length];
        
        for (int j = 0; j < cartes1.Length; j++)
        {
            cartes[0, j] = cartes1[j];
        }

        for (int j = 0; j < cartes2.Length; j++)
        {
            cartes[1, j] = cartes2[j];
        }

        for (int j = 0; j < cartes3.Length; j++)
        {
            cartes[2, j] = cartes3[j];
        }

        for (int j = 0; j < cartes4.Length; j++)
        {
            cartes[3, j] = cartes4[j];
        }

        for (int j = 0; j < cartes5.Length; j++)
        {
            cartes[4, j] = cartes5[j];
        }

        for (int j = 0; j < cartes6.Length; j++)
        {
            cartes[5, j] = cartes6[j];
        }

        pos = Array.IndexOf(Partie.Positions, Partie.JoueurCourant) + 1;

        if (pos == 3)
        {
            Plateau.transform.Rotate(Vector3.forward * 90);
            button.transform.Rotate(Vector3.forward * 90);
            Vector2 posButton = new Vector2((float)-2.78, 0);
            button.transform.position = posButton;
        }

        if ((pos == 4) || (pos == 5))
        {
            Plateau.transform.Rotate(Vector3.forward * 180);
            button.transform.Rotate(Vector3.forward * 180);
            Vector2 posButton = new Vector2(0, (float)-2.78);
            button.transform.position = posButton;
        }

        if (pos == 6)
        {
            Plateau.transform.Rotate(Vector3.forward * -90);
            button.transform.Rotate(Vector3.forward * -90);
            Vector2 posButton = new Vector2((float)2.78, 0);
            button.transform.position = posButton;
        }
        
        for (int i = 0; i < 6; i++)
        {
            if (i == (pos-1))
            {
                for (int j = 0; j < 5; j++)
                {
                    cartes[i, j].GetComponent<SpriteRenderer>().sprite = images[i, j];
                    cartes[i, j].SetActive(true);
                }
            }
            else
            {
                for (int j = 0; j < 5; j++)
                {
                    cartes[i, j].SetActive(false);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ButtonClicked()
    {
        MyNetworkMessage msg = new MyNetworkMessage();
        msg.message = Partie.JoueurCourant;
        NetworkServer.SendToAll(waitID, msg);
        canvas_plateau_vehicule.SetActive(false);
        canvas_debat.SetActive(true);
        //SceneManager.LoadScene("Scene5");
    }

    public static void get(Sprite[] image, int zone)
    {
        for (int i = 0; i < 5; i++)
        {
            images[zone-1, i] = image[i];

        }
    }

}
