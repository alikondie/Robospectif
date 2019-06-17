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

    short persosID = 1007;

    short waitID = 1006;

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
        NetworkServer.RegisterHandler(persosID, onPersoReceived);

    }

    void OnEnable()
    {
        Tour.Piles = new int[] { 0, 0, 0, 0, 0, 0 };

        index = new int[] { 0, 0, 0, 0, 0, 0 };

        nbRecu = 0;

        persoSprites = new Sprite[] { null, null, null, null, null, null};

        int pos = Array.IndexOf(Partie.Positions, Partie.JoueurCourant);

        for (int i = 0; i < 6; i++)
        {
            persos[i].transform.GetChild(0).gameObject.SetActive(false);
            persos[i].transform.GetChild(3).gameObject.SetActive(false);
            persos[i].transform.GetChild(4).gameObject.SetActive(false);
            persos[i].transform.GetChild(5).gameObject.SetActive(false);
        }

        zones = new int[6, 2];

        button.gameObject.SetActive(false);
    }

    private void onJetonReceived(NetworkMessage netMsg)
    {
        var v = netMsg.ReadMessage<MyJetonMessage>();
        int pos = v.joueur;
        string s = "Jetons/" + v.sprite;
        Sprite jeton_actuel = Resources.Load<Sprite>(s);
        for (int j = 0; j < 6; j++)
        {
            if (Partie.Positions[j] == pos)
            {
                jetons[j][index[j]].gameObject.GetComponent<Image>().sprite = jeton_actuel;
                jetons[j][index[j]].gameObject.SetActive(true);
                index[j]++;
            }
        }
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

        Tour.PersosDebat = persoSprites;
        Tour.ZonesDebat = zones;

        MyNetworkMessage wait = new MyNetworkMessage();
        NetworkServer.SendToAll(waitID, wait);

        canvas_debat.SetActive(false);
        canvas_choix_vainqueur.SetActive(true);
        //SceneManager.LoadScene("Scene_6");
    }

    private void onPersoReceived(NetworkMessage netMsg)
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
                    persos[j].transform.GetChild(zone1 + 2).gameObject.SetActive(true);
                if (zone2 != 0)
                    persos[j].transform.GetChild(zone2 + 2).gameObject.SetActive(true);
                persos[j].transform.GetChild(0).gameObject.SetActive(true);

            }
        }
        nbRecu++;
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
