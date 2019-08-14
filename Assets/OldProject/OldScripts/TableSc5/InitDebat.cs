﻿ using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.IO;

public class InitDebat : MonoBehaviour
{
    [SerializeField] GameObject canvas_debat;
    [SerializeField] GameObject canvas_choix_vainqueur;
    [SerializeField] GameObject helpBg;
    [SerializeField] GameObject[] cartes;
    [SerializeField] GameObject conduite;
    [SerializeField] GameObject canvas_pres_vehicule;
    [SerializeField] Text central;
    [SerializeField] Sprite baseSprite;
    private List<GameObject>[] jetons;
    private int[] index;

    // ça c'est pour trouver les jetons de chaque joueurs en utilisant leur numero comme un lien
    // comme ça on va trouver les jetons et les mettre dans le dict de persosAndDebate.

    private Dictionary<int, string> persosAndDebate;
    private Dictionary<int, GameObject> persosAndJetons;
    // récuperer pour chaque joueur les jetons données l'ordre c'est: SDP SDM EDP EDM UDP UDM
    private Dictionary<int, int[]> givenJetons;
    public Debate debate;
    private bool isDictsEmpty = true;

    GameObject objet;
    short jeton = 1010;
    short stopID = 1012;
    short goID = 1013;
    short nextID = 1015;
    short publicID = 1016;
    short presID = 1017;
    short RetourID = 1022;

    [SerializeField] Button button;
    [SerializeField] Button help;
    [SerializeField] Button bouton_retour;

    [SerializeField] GameObject[] persos;

    private Vector2[] positionsButton;

    short vainqueurID = 1008;
    short hasstartID = 1018;

    private Sprite[] persoSprites;

    private int[,] zones;

    private int nbRecu;

    private string fr;
    private string en;

    private int nbClicked;
    private bool clienthasstart;
    public bool Retour = false;
    private string vainqueurstring;

    //sp = sm = ep = em = up = um = 0;

    // Start is called before the first frame update
    void Start()
    {
        bouton_retour.gameObject.SetActive(false);
        clienthasstart = false;
        persosAndDebate = new Dictionary<int, string>();
        FillPersoDict();
        debate = new Debate();
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
        NetworkServer.RegisterHandler(hasstartID, onClientStart);

        button.onClick.AddListener(() => ButtonClicked());
        help.onClick.AddListener(() => HelpClicked());
        bouton_retour.onClick.AddListener(() => RetourButtonClicked());

    }

    private void HelpClicked()
    {
        canvas_pres_vehicule.SetActive(false);
        helpBg.SetActive(true);
    }

    void OnEnable()
    {
        
        button.transform.GetChild(0).GetComponent<Text>().text = "Terminer le débat";
        central.text = "Posez vos jetons pour les usages adverses";
        nbClicked = 0;
        canvas_pres_vehicule.SetActive(false);
        canvas_pres_vehicule.SetActive(true);
        canvas_pres_vehicule.GetComponent<CanvasScaler>().referenceResolution = new Vector2(10000f, 10000f);
        canvas_pres_vehicule.transform.GetChild(0).gameObject.SetActive(false);
        canvas_pres_vehicule.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
        if (Partie.Type == "expert")
            canvas_pres_vehicule.GetComponent<Initialisation_expert>().enabled = false;
        else
            canvas_pres_vehicule.GetComponent<Initialisation>().enabled = false;
        canvas_pres_vehicule.transform.GetChild(1).GetChild(0).GetChild(7).GetComponent<BoxCollider2D>().enabled = false;
        canvas_pres_vehicule.transform.GetChild(1).GetChild(0).GetChild(7).GetComponent<Mouvement_carte>().enabled = false;
        foreach (GameObject carte in cartes)
        {
            carte.GetComponent<BoxCollider2D>().enabled = false;
            if (Partie.Type == "expert")
                carte.GetComponent<Mouvement_carte_expert>().enabled = false;
            else
                carte.GetComponent<Mouvement_carte>().enabled = false;
        }
        int pos = Array.IndexOf(Partie.Positions, Partie.JoueurCourant);

        if (Partie.Type == "expert")
        {
            fr = "Fin des investissements privés";
            en = "Private investments done";
        }
        else
        {
            fr = "Terminer le débat";
            en = "End debate";
            button.transform.position = persos[pos].transform.position;
            switch (pos)
            {
                case 0: case 1:
                    button.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    break;
                case 2:
                    button.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                    break;
                case 3: case 4:
                    button.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                    break;
                case 5:
                    button.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
                    break;
            }
        }

        if (Partie.Langue == "FR")
        {
            button.transform.GetChild(0).GetComponent<Text>().text = fr;
            vainqueurstring = "Choix du vainqueur";
        }
        else
        {
            button.transform.GetChild(0).GetComponent<Text>().text = en;
            vainqueurstring = "Winner's choice";
        }
        if (!Retour)
        {
            Tour.Piles = new int[] { 0, 0, 0, 0, 0, 0 };

            index = new int[] { 0, 0, 0, 0, 0, 0 };

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
                }
                else
                    persos[i].transform.GetChild(0).gameObject.SetActive(false);
                for (int j = 1; j <= 2; j++)
                {
                    foreach (Transform child in persos[i].transform.GetChild(j))
                    {
                        child.gameObject.SetActive(false);
                    }
                }
            }
        }
        else
        {
            bouton_retour.gameObject.SetActive(true);
        }


    }

    private void onJetonReceived(NetworkMessage netMsg)
    {
        MyJetonMessage msg = new MyJetonMessage();
        NetworkServer.SendToAll(stopID, msg);
        var v = netMsg.ReadMessage<MyJetonMessage>();
        int pos = v.joueur;
        string s = Partie.Langue +  "/Jetons/" + v.sprite;
        Sprite jeton_actuel = Resources.Load<Sprite>(s);
        int j = Array.IndexOf(Partie.Positions, pos);
        jetons[j][index[j]].gameObject.GetComponent<Image>().sprite = jeton_actuel;
        jetons[j][index[j]].gameObject.SetActive(true);
        index[j]++;
    }

    private void onClientStart(NetworkMessage netMsg)
    {
        clienthasstart = true;
    }

    private void ButtonClicked()
    {
        
        
        if (Partie.Type == "expert")
        {
            if (nbClicked == 0)
            {
                MyStringMessage msg = new MyStringMessage();
                NetworkServer.SendToAll(publicID, msg);
                nbClicked++;
                if (Partie.Langue == "FR")
                    button.transform.GetChild(0).GetComponent<Text>().text = "Fin des investissements\npublics";
                else
                    button.transform.GetChild(0).GetComponent<Text>().text = "Public investments\ndone";
            }
            else
            {
                Sprite[,] sprites = new Sprite[6, persos[0].transform.GetChild(2).childCount];
                bool[,] bools = new bool[6, persos[0].transform.GetChild(2).childCount];

                for (int i = 0; i < persos.Length; i++)
                {
                    for (int j = 0; j < persos[i].transform.GetChild(2).childCount; j++)
                    {
                        sprites[i, j] = persos[i].transform.GetChild(2).GetChild(j).gameObject.GetComponent<Image>().sprite;
                        Debug.Log("sprites[" + i + ", " + j + "] = " + sprites[i, j]);
                        bools[i, j] = persos[i].transform.GetChild(2).GetChild(j).gameObject.activeSelf;
                        Debug.Log("bools[" + i + ", " + j + "] = " + bools[i, j]);
                    }
                }
                Tour.JetonsDebat = sprites;
                Tour.ActivesDebat = bools;
                MyStringMessage msg = new MyStringMessage();
                NetworkServer.SendToAll(nextID, msg);

                bouton_retour.gameObject.SetActive(false);

                canvas_pres_vehicule.SetActive(false);
                canvas_pres_vehicule.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                canvas_pres_vehicule.transform.GetChild(0).gameObject.SetActive(true);
                canvas_pres_vehicule.transform.GetChild(1).gameObject.SetActive(true);
                canvas_pres_vehicule.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920f, 1080f);
                if (Partie.Type == "expert")
                    canvas_pres_vehicule.GetComponent<Initialisation_expert>().enabled = true;
                else
                    canvas_pres_vehicule.GetComponent<Initialisation>().enabled = true;
                canvas_pres_vehicule.transform.GetChild(1).GetChild(0).GetChild(7).GetComponent<BoxCollider2D>().enabled = true;
                canvas_pres_vehicule.transform.GetChild(1).GetChild(0).GetChild(7).GetComponent<Mouvement_carte>().enabled = true;

                foreach (GameObject carte in cartes)
                {
                    carte.GetComponent<BoxCollider2D>().enabled = true;
                    if (Partie.Type == "expert")
                        carte.GetComponent<Mouvement_carte_expert>().enabled = true;
                    else
                        carte.GetComponent<Mouvement_carte>().enabled = true;
                }

                canvas_debat.SetActive(false);
                canvas_choix_vainqueur.SetActive(true);
            }
        }
        else
        {
            if (nbClicked == 1)
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
                //SansHUD.data.AppendLine("Joueur;Perso;Environnement;SR+;SR-;SD+;SD-;ER+;ER-;ED+;ED-;UR+;UR-;UD+;UD-");
                foreach (GameObject pers in persos)
                {
                    FillPersoData(pers);
                }
                persosAndDebate.Clear();
                persosAndJetons.Clear();
                givenJetons.Clear();
                isDictsEmpty = true;
                //ReinitializeCards();
                bouton_retour.gameObject.SetActive(false);

                canvas_pres_vehicule.SetActive(false);
                canvas_pres_vehicule.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                canvas_pres_vehicule.transform.GetChild(0).gameObject.SetActive(true);
                canvas_pres_vehicule.transform.GetChild(1).gameObject.SetActive(true);
                canvas_pres_vehicule.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920f, 1080f);
                if (Partie.Type == "expert")
                    canvas_pres_vehicule.GetComponent<Initialisation_expert>().enabled = true;
                else
                    canvas_pres_vehicule.GetComponent<Initialisation>().enabled = true;
                canvas_pres_vehicule.transform.GetChild(1).GetChild(0).GetChild(7).GetComponent<BoxCollider2D>().enabled = true;
                canvas_pres_vehicule.transform.GetChild(1).GetChild(0).GetChild(7).GetComponent<Mouvement_carte>().enabled = true;
                
                foreach (GameObject carte in cartes)
                {
                    carte.GetComponent<BoxCollider2D>().enabled = true;
                    if (Partie.Type == "expert")
                        carte.GetComponent<Mouvement_carte_expert>().enabled = true;
                    else
                        carte.GetComponent<Mouvement_carte>().enabled = true;
                }
                canvas_debat.SetActive(false);
                canvas_choix_vainqueur.SetActive(true);
                return ;
            }
            button.transform.GetChild(0).GetComponent<Text>().text = vainqueurstring;
            central.text = "Le joueur " + Partie.JoueurCourant +" pose ses jetons";
            bouton_retour.gameObject.SetActive(true);
            MyJetonMessage msg = new MyJetonMessage();
            NetworkServer.SendToAll(stopID, msg);
            MyNetworkMessage pres = new MyNetworkMessage();
            pres.message = Partie.JoueurCourant;
            NetworkServer.SendToAll(presID, pres);
            nbClicked++;
        }
    }

    private void RetourButtonClicked()
    {
        if (Partie.Langue == "FR")
            button.transform.GetChild(0).GetComponent<Text>().text = fr;
        else
            button.transform.GetChild(0).GetComponent<Text>().text = en;
        nbClicked--;
        bouton_retour.gameObject.SetActive(false);
        MyNetworkMessage msg = new MyNetworkMessage();
        msg.message = Partie.JoueurCourant;
        NetworkServer.SendToAll(RetourID, msg);
    }

    // Update is called once per frame
    void Update()
    {
        checkifvehiculeclicked();
        if (clienthasstart)
        {
            MyNetworkMessage msg = new MyNetworkMessage();
            msg.message = Partie.JoueurCourant;
            NetworkServer.SendToAll(goID, msg);
            checkifvehiculeclicked();
            clienthasstart = false;
        }
        
        if (nbRecu == Partie.Joueurs.Count - 1)
        {
            button.gameObject.SetActive(true);
        }

        if (isDictsEmpty) { 
            FillPersoDict();
            isDictsEmpty = false;
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

    }

    private void FillPersoData(GameObject perso)
    {
        int number = Array.IndexOf(persos, perso);

        if (number >= Partie.Joueurs.Count)// || number+1 == Partie.JoueurCourant)
            return;
        string environment = "";
        string character = perso.transform.GetChild(0).gameObject.GetComponent<Image>().sprite.name;
        // environnement
        if (perso.transform.GetChild(3).gameObject.activeSelf)
            environment += "Campagne,";
        if (perso.transform.GetChild(4).gameObject.activeSelf)
            environment += "Banlieue,";
        if (perso.transform.GetChild(5).gameObject.activeSelf)
            environment += "Ville";

        if (number +1 == Partie.JoueurCourant)
        {
            character = "Concepteur";
            environment = "Concepteur";
        }

        int keyIndex = perso.transform.GetSiblingIndex();

        Personnage personnage = new Personnage
        {
             Joueur = keyIndex,
             Person = character,
             Environnement = environment,
             UsagePropose = "",
        };
        string json = JsonUtility.ToJson(personnage);
        File.WriteAllText(Application.dataPath + "/personnage," + DateTime.Now.ToString("dd-MM-yyyy") + "_a_" + DateTime.Now.ToString("hh") + "h" + DateTime.Now.ToString("mm") + "m" + DateTime.Now.ToString("ss") + "s" +".json", json);


        // société +-, environement +-, usage +-
        int sp, sm, ep, em, up, um;
        sp = sm = ep = em = up = um = 0;
        string jetonValue;

        
        if(number + 1 != Partie.JoueurCourant) { 
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
                    case "planeteRouge":
                        em++;
                        break;
                    case "societeRouge":
                        sm++;
                        break;
                    case "usageRouge":
                        um++;
                        break;
                    case "societeVert":
                        sp++;
                        break;
                    case "usageVert":
                        up++;
                        break;
                    case "planeteVert":
                        ep++;
                        break;
                }
                
            }
        }

        //Donnée Finale

        persosAndDebate.Add(keyIndex, number+1 + ";" + character + ";" + environment + ";" + sp + ";" + sm + ";" + givenJetons[keyIndex][0]+";"+ givenJetons[keyIndex][1]+";" + ep + ";" + em + ";" + givenJetons[keyIndex][2] + ";" + givenJetons[keyIndex][3] + ";"
                                    + up + ";" + um + ";" + givenJetons[keyIndex][4] + ";" + givenJetons[keyIndex][5] + ";");



          
            //SansHUD.data.AppendLine(persosAndDebate[keyIndex]);


        //  }
    }

    // ajouter un jeton lorsqu'un joueur ajoute un jeton (jeton_pop.cs)
    public void AddGivenJeton()
    {
        string json = JsonUtility.ToJson(debate);
        File.WriteAllText(Application.dataPath + "/debat" + debate.JoueurDonnant+","+ DateTime.Now.ToString("dd-MM-yyyy") + "_a_" + DateTime.Now.ToString("hh") + "h" + DateTime.Now.ToString("mm") + "m" + DateTime.Now.ToString("ss") + "s" + ".json", json);
    }

    private void ReinitializeCards()
    {
        foreach(GameObject p in persos)
        {
            List<GameObject> jetonSprites = new List<GameObject>();
            foreach(Transform ch in p.transform.GetChild(2).transform)
            {
                ch.GetComponent<Image>().sprite = baseSprite;
            }
        }
    }

    private void checkifvehiculeclicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x < 1000 && Input.mousePosition.x > 900 &&
                Input.mousePosition.y < 600 && Input.mousePosition.y > 500 &&
                canvas_pres_vehicule.GetComponent<CanvasScaler>().referenceResolution == new Vector2(10000f, 10000f))
            {
                canvas_pres_vehicule.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920f, 1080f);
            }
            else if (Input.mousePosition.x < 1500 && Input.mousePosition.x > 420 &&
                     Input.mousePosition.y < 1070 && Input.mousePosition.y > 0 &&
                     canvas_pres_vehicule.GetComponent<CanvasScaler>().referenceResolution == new Vector2(1920f, 1080f))
            {
                canvas_pres_vehicule.GetComponent<CanvasScaler>().referenceResolution = new Vector2(10000f, 10000f);
            }
        }
    }
}


