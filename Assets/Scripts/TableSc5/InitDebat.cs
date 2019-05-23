using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class InitDebat : MonoBehaviour
{

    public Button button;
    private int JoueurCourant;
    public GameObject perso0;
    public GameObject perso1;
    public GameObject perso2;
    public GameObject perso3;
    public GameObject perso4;
    public GameObject perso5;

    private Vector2[] positionsButton;

    short persosID = 1007;

    short waitID = 1006;

    private GameObject[] persos;
    private Sprite[] persoSprites;

    private int nbJoueurs;

    private int nbRecu;

    public static Sprite[] envoi;

    public static Sprite[,] envoiSprites;
    public static bool[,] envoiActives;

    private int[] positions = Button_ready_next_scene.envoi;

    // Start is called before the first frame update
    void Start()
    {
        positionsButton = new Vector2[6];
        positionsButton[0] = new Vector2(-3, (float)-3.75);
        positionsButton[1] = new Vector2(3, (float)-3.75);
        positionsButton[2] = new Vector2(7, 0);
        positionsButton[3] = new Vector2(3, (float)3.75);
        positionsButton[4] = new Vector2(-3, (float)3.75);
        positionsButton[5] = new Vector2(-7, 0);

        JoueurCourant = Partie.JoueurCourant;

        int pos = -1;
        for (int i = 0; i < Partie.Joueurs.Count; i++)
        {
            if (Partie.Joueurs[i].Numero == JoueurCourant)
                pos = Partie.Joueurs[i].Position;
        }

        button.transform.position = positionsButton[pos-1];

        if (pos == 3)
            button.transform.Rotate(Vector3.forward * 90);

        if ((pos == 4) || (pos == 5))
            button.transform.Rotate(Vector3.forward * 180);

        if(pos == 6)
            button.transform.Rotate(Vector3.forward * 270);

        button.gameObject.SetActive(false);
        nbRecu = 0;
        for (int i = 0; i < 6; i++)
        {
            if (positions[i] != 0)
            {
                nbJoueurs++;
            }
        }

        button.onClick.AddListener(() => ButtonClicked());

        persoSprites = new Sprite[6];
        for (int i = 0; i < 6; i++)
        {
            persoSprites[i] = null;
        }
        persos = new GameObject[6];
        persos[0] = perso0;
        persos[1] = perso1;
        persos[2] = perso2;
        persos[3] = perso3;
        persos[4] = perso4;
        persos[5] = perso5;

        
        for (int i = 0; i < 6; i++)
        {
            persos[i].transform.GetChild(0).gameObject.SetActive(false);
            persos[i].transform.GetChild(3).gameObject.SetActive(false);
            persos[i].transform.GetChild(4).gameObject.SetActive(false);
            persos[i].transform.GetChild(5).gameObject.SetActive(false);
        }

        persos[0].transform.GetChild(3).gameObject.SetActive(false);
        persos[0].transform.GetChild(4).gameObject.SetActive(false);
        persos[0].transform.GetChild(5).gameObject.SetActive(false);
        NetworkServer.RegisterHandler(persosID, onPersoReceived);
        
    }

    private void ButtonClicked()
    {
        Sprite[,] sprites = new Sprite[6, perso0.transform.GetChild(2).childCount];
        bool[,] bools = new bool[6, perso0.transform.GetChild(2).childCount];

        //for (int i = 0; i < 6; i++)
        //{
        //    sprites[i] = new List<Sprite>();
        //}
        
        for (int i = 0; i < perso0.transform.GetChild(2).childCount; i++)
        {
            sprites[0, i] = perso0.transform.GetChild(2).GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite;
            bools[0, i] = perso0.transform.GetChild(2).GetChild(i).gameObject.activeSelf;
        }
        
        for (int i = 0; i < perso1.transform.GetChild(2).childCount; i++)
        {
            sprites[1, i] = perso1.transform.GetChild(2).GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite;
            bools[1, i] = perso1.transform.GetChild(2).GetChild(i).gameObject.activeSelf;
        }

        
        for (int i = 0; i < perso2.transform.GetChild(2).childCount; i++)
        {
            sprites[2, i] = perso2.transform.GetChild(2).GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite;
            bools[2, i] = perso2.transform.GetChild(2).GetChild(i).gameObject.activeSelf;
        }
        
        for (int i = 0; i < perso3.transform.GetChild(2).childCount; i++)
        {
            sprites[3, i] = perso3.transform.GetChild(2).GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite;
            bools[3, i] = perso3.transform.GetChild(2).GetChild(i).gameObject.activeSelf;
        }

        
        for (int i = 0; i < perso4.transform.GetChild(2).childCount; i++)
        {
            sprites[4, i] = perso4.transform.GetChild(2).GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite;
            bools[4, i] = perso4.transform.GetChild(2).GetChild(i).gameObject.activeSelf;
        }

        
        for (int i = 0; i < perso5.transform.GetChild(2).childCount; i++)
        {
            sprites[5, i] = perso5.transform.GetChild(2).GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite;
            bools[5, i] = perso5.transform.GetChild(2).GetChild(i).gameObject.activeSelf;
        }
        

        envoiSprites = sprites;
        envoiActives = bools;
        
        envoi = persoSprites;

        MyNetworkMessage wait = new MyNetworkMessage();
        NetworkServer.SendToAll(waitID, wait);

        SceneManager.LoadScene("Scene_6");
    }

    private void onPersoReceived(NetworkMessage netMsg)
    {
        var v = netMsg.ReadMessage<MyPersoMessage>();
        int i = v.numero;
        Debug.Log("recu : " + i);
        string s = v.image;
        string spriteString = "image/Personnages/" + s;
        int zone1 = v.choixZone0;
        int zone2 = v.choixZone1;
        Sprite sp = Resources.Load<Sprite>(spriteString);
        for (int j = 0; j < 6; j++)
        {
            if ((positions[j] == i) && (positions[j] != JoueurCourant))
            {
                persoSprites[j] = sp;
                persos[j].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = sp;
                if (zone1 != 0)
                    persos[j].transform.GetChild(zone1 + 2).gameObject.SetActive(true);
                if (zone2 != 0)
                    persos[j].transform.GetChild(zone2 + 2).gameObject.SetActive(true);
                persos[j].transform.GetChild(0).gameObject.SetActive(true);

            }
        }
        nbRecu++;
        if (nbRecu == nbJoueurs - 1)
        {
            button.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
