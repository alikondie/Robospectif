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

    // ça c'est pour trouver les jetons de chaque joueurs en utilisant leur numero comme un lien
    // comme ça on va trouver les jetons et les mettre dans le dict de persosAndDebate.

    private Dictionary<int, string> persosAndDebate;
    private Dictionary<int, GameObject> persosAndJetons;
    // récuperer pour chaque joueur les jetons données l'ordre c'est: SDP SDM EDP EDM UDP UDM
    private Dictionary<int, int[]> givenJetons;

    GameObject objet;
    short jeton = 1010;
    short stopID = 1012;

    [SerializeField] Button button;

    [SerializeField] GameObject[] persos;

    private Vector2[] positionsButton;

    short vainqueurID = 1008;

    private Sprite[] persoSprites;

    private int[,] zones;

    private int nbRecu;

    //sp = sm = ep = em = up = um = 0;

    // Start is called before the first frame update
    void Start()
    {
        SansHUD.data.AppendLine("Joueur;Perso;Environnement;SR+;SR-;SD+;SD-;ER+;ER-;ED+;ED-;UR+;UR-;UD+;UD-");
        persosAndDebate = new Dictionary<int, string>();
        FillPersoDict();
        //givenJetons = new Dictionary<int,  int[]{ 0, 0, 0, 0, 0, 0 } > ();
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
        MyJetonMessage msg = new MyJetonMessage();
        NetworkServer.SendToAll(stopID, msg);
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


        //GameObject p = persos[0].transform.Find("Jetons").gameObject;

        foreach(GameObject pers in persos)
        {
            FillPersoData(pers);
        }
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

    private void FillPersoDict()
    {
        persosAndJetons = new Dictionary<int, GameObject>();
        givenJetons = new Dictionary<int, int[]>();
        for (int i = 3; i < 9; i++)
        {
            GameObject currentPerso = gameObject.transform.GetChild(i).gameObject;
            string name = currentPerso.transform.GetChild(0).GetComponent<Image>().sprite.name;
            persosAndJetons.Add(i, currentPerso);
            givenJetons.Add(i, new int[] { 0, 0, 0, 0, 0, 0 });
        }
     /*   foreach (KeyValuePair<int, GameObject> p in persosAndJetons)
        {
            print(p.Value);
        }*/

    }

    private void FillPersoData(GameObject perso)
    {
        if (!perso.activeSelf)
            return;

        int number = Array.IndexOf(persos, perso);
        string environment = "";
        string character = perso.transform.GetChild(0).gameObject.GetComponent<Image>().sprite.name;

        // environnement
        if (perso.transform.GetChild(3).gameObject.activeSelf)
            environment += "Campagne,";
        if (perso.transform.GetChild(4).gameObject.activeSelf)
            environment += "Banlieue,";
        if (perso.transform.GetChild(5).gameObject.activeSelf)
            environment += "Ville";


        // société +-, environement +-, usage +-
        int sp, sm, ep, em, up, um;
        sp = sm = ep = em = up = um = 0;
        string jetonValue;

        int keyIndex = perso.transform.GetSiblingIndex();
        GameObject p = persosAndJetons[keyIndex];
       // foreach(KeyValuePair<int,GameObject> p in persosAndJetons)
       // {
            GameObject pileJetons = p.transform.GetChild(2).gameObject;
            for(int i = 0 ; i < 8; i++)
            {
                if (!pileJetons.activeSelf)
                    continue;

                jetonValue = pileJetons.transform.GetChild(i).GetComponent<Image>().sprite.name;
                switch (jetonValue)
                {
                    case "Jeton Rouge Planète 2":
                        em++;
                        break;
                    case "Jeton Rouge Société 2":
                        sm++;
                        break;
                    case "Jeton Rouge Usage 2":
                        um++;
                        break;
                    case "Jeton Vert Société 2":
                        sp++;
                        break;
                    case "Jeton Vert Usage 2":
                        up++;
                        break;
                    case "Jeton Vert Planète 2":
                        ep++;
                        break;
                }
                
            }


            //Donnée Finale

            persosAndDebate.Add(keyIndex, number + ";" + character + ";" + environment + ";" + sp + ";" + sm + ";" + givenJetons[keyIndex][0]+";"+ givenJetons[keyIndex][1]+";" + ep + ";" + em + ";" + givenJetons[keyIndex][2] + ";" + givenJetons[keyIndex][3] + ";"
                                    + up + ";" + um + ";" + givenJetons[keyIndex][4] + ";" + givenJetons[keyIndex][5] + ";");

            print( persosAndDebate[keyIndex]);
      //  }
    }

    // ajouter un jeton lorsqu'un joueur ajoute un jeton (jeton_pop.cs)
    public void AddGivenJeton(int numJoueur, GameObject jeton)
    {
        string jetonValue = jeton.GetComponent<Image>().sprite.name;
        switch (jetonValue)
        // recuperer pour chaque joueur les jetons données l'ordre c'est SDP SDM EDP EDM UDP UDM
        {
            case "Jeton Rouge Planète 2":
                givenJetons[numJoueur][3]++;
                break;
            case "Jeton Rouge Société 2":
                givenJetons[numJoueur][1]++;
                break;
            case "Jeton Rouge Usage 2":
                givenJetons[numJoueur][5]++;
                break;
            case "Jeton Vert Société 2":
                givenJetons[numJoueur][0]++;
                break;
            case "Jeton Vert Usage 2":
                givenJetons[numJoueur][4]++;
                break;
            case "Jeton Vert Planète 2":
                givenJetons[numJoueur][2]++;
                break;
        }
    }
}
