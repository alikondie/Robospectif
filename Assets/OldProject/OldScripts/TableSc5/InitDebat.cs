using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class InitDebat : MonoBehaviour
{
    [SerializeField] GameObject canvas_debat;
    [SerializeField] GameObject canvas_choix_vainqueur;

    private List<GameObject>[] jetons;
    private int[] index;
    GameObject objet;
    short jeton = 1010;

    [SerializeField] Button button;

    [SerializeField] GameObject[] persos;

    private Vector2[] positionsButton;

    short vainqueurID = 1008;

    private Sprite[] persoSprites;

    private int[,] zones;

    private int nbRecu;

    // Start is called before the first frame update
    void Start()
    {
        jetons = new List<GameObject>[6];

        for (int i = 0; i < jetons.Length; i++)
        {
            jetons[i] = new List<GameObject>();
        }

        for (int i = 0; i < jetons.Length; i++)
        {
            for (int j = 0; j < persos[i].transform.GetChild(1).childCount; j++)
            {
                jetons[i].Add(persos[i].transform.GetChild(1).GetChild(j).gameObject);
            }
        }

        NetworkServer.RegisterHandler(jeton, onJetonReceived);


        positionsButton = new Vector2[6];
        positionsButton[0] = new Vector2(-3, (float)-3.75);
        positionsButton[1] = new Vector2(3, (float)-3.75);
        positionsButton[2] = new Vector2(7, 0);
        positionsButton[3] = new Vector2(3, (float)3.75);
        positionsButton[4] = new Vector2(-3, (float)3.75);
        positionsButton[5] = new Vector2(-7, 0);


        //button.transform.position = positionsButton[pos-1];

       /* if (pos == 3)
            button.transform.Rotate(Vector3.forward * 90);

        if ((pos == 4) || (pos == 5))
            button.transform.Rotate(Vector3.forward * 180);

        if (pos == 6)
            button.transform.Rotate(Vector3.forward * 270);*/

        button.onClick.AddListener(() => ButtonClicked());

    }

    void OnEnable()
    {
        Tour.Piles = new int[] { 0, 0, 0, 0, 0, 0 };

        index = new int[] { 0, 0, 0, 0, 0, 0 };

        int pos = Array.IndexOf(Partie.Positions, Partie.JoueurCourant);

        for (int i = 0; i < 6; i++)
        {
            persos[i].transform.GetChild(3).gameObject.SetActive(false);
            persos[i].transform.GetChild(4).gameObject.SetActive(false);
            persos[i].transform.GetChild(5).gameObject.SetActive(false);

            if (Tour.PersosDebat[i] != null)
            {
                persos[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Tour.PersosDebat[i];
                persos[i].transform.GetChild(0).gameObject.SetActive(true);
                if (Tour.ZonesDebat[i, 0] != 0)
                    persos[i].transform.GetChild(Tour.ZonesDebat[i, 0] + 2).gameObject.SetActive(true);
                if (Tour.ZonesDebat[i, 1] != 0)
                    persos[i].transform.GetChild(Tour.ZonesDebat[i, 1] + 2).gameObject.SetActive(true);
            } else
                persos[i].transform.GetChild(0).gameObject.SetActive(false);
            for (int j = 1; j <= 2; j++)
            {
                foreach (Transform child in persos[i].transform.GetChild(j))
                {
                    child.gameObject.SetActive(false);
                }
            }
        }

        //button.gameObject.SetActive(false);
    }

    private void onJetonReceived(NetworkMessage netMsg)
    {
        var v = netMsg.ReadMessage<MyJetonMessage>();
        int pos = v.joueur;
        string s = "FR/Jetons/" + v.sprite;
        Sprite jeton_actuel = Resources.Load<Sprite>(s);
        int j = Array.IndexOf(Partie.Positions, pos);
        jetons[j][index[j]].gameObject.GetComponent<Image>().sprite = jeton_actuel;
        jetons[j][index[j]].gameObject.SetActive(true);
        index[j]++;
    }

    private void ButtonClicked()
    {
        Sprite[,] sprites = new Sprite[6, persos[0].transform.GetChild(2).childCount];
        bool[,] bools = new bool[6, persos[0].transform.GetChild(2).childCount];

        for (int i = 0; i < persos.Length; i++)
        {
            for (int j = 0; j < persos[i].transform.GetChild(2).childCount; j++)
            {
                sprites[i, j] = persos[i].transform.GetChild(2).GetChild(j).gameObject.GetComponent<Image>().sprite;
                bools[i, j] = persos[i].transform.GetChild(2).GetChild(j).gameObject.activeSelf;
            }
        }
        Tour.JetonsDebat = sprites;
        Tour.ActivesDebat = bools;

        MyNetworkMessage wait = new MyNetworkMessage();
        NetworkServer.SendToAll(vainqueurID, wait);

        canvas_debat.SetActive(false);
        canvas_choix_vainqueur.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (nbRecu == Partie.Joueurs.Count - 1)
        {
            button.gameObject.SetActive(true);
        }
    }
}
