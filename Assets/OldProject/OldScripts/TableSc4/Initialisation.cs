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
    #region variables
    [SerializeField] GameObject canvas_plateau_vehicule;
    [SerializeField] GameObject canvas_debat;
    [SerializeField] GameObject children;

    private int pos;

    [SerializeField] Button button;

    short debatID = 1006;

    public static int indice = 0;
    public static Sprite[,] images = new Sprite[6,5];

    [SerializeField] GameObject Plateau;

    private GameObject[,] cartes;
    [SerializeField] GameObject[] cartes1;
    [SerializeField] GameObject[] cartes2;
    [SerializeField] GameObject[] cartes3;
    [SerializeField] GameObject[] cartes4;
    [SerializeField] GameObject[] cartes5;
    [SerializeField] GameObject[] cartes6;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() => ButtonClicked());        
    }

    void OnEnable()
    {
        #region cards array
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
        #endregion
        //pos = Array.IndexOf(Partie.Positions, Partie.JoueurCourant) + 1;
        pos = 5;
        Rotate(pos);
        #region players cards display
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
        #endregion
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ButtonClicked()
    {
        MyNetworkMessage msg = new MyNetworkMessage();
        msg.message = Partie.JoueurCourant;
        NetworkServer.SendToAll(debatID, msg);
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




    //function which rotate the canvas depending on the current player who presents the robot
    private void Rotate(int pos)
    {
        Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
        if (pos == 3)
        {
            rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else if(pos == 4 || pos == 5)
        {
            rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else if (pos == 6)
        {
            rotation = Quaternion.Euler(0f, 0f, -90f);
        }
        children.transform.rotation = rotation;
    }

}
