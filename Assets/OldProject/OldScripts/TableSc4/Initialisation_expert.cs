using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using static Main;
using Image = UnityEngine.UI.Image;

public class Initialisation_expert : MonoBehaviour
{
    #region variables
    [SerializeField] GameObject canvas_plateau_vehicule;
    [SerializeField] GameObject canvas_pres_persos;
    [SerializeField] GameObject children;
    [SerializeField] GameObject[] dimension;
    [SerializeField] GameObject[] locomotion;
    [SerializeField] GameObject[] equipement;
    private Sprite[] dimensionssprites;
    private Sprite[] locomotionsprites;
    private Sprite[] equipementsprites;

    private int pos;

    [SerializeField] Button button;

    public static List<string> manualEquipmentCards;
    public static List<string> programmableEquipmentCards;
    public static List<string> autoEquipmentCards;
    public static string autonomie;

    short conceptionID = 1002;
    short presID = 1011;
    short decideurID = 1014;

    public static int indice = 0;
    public static Sprite[,] images = new Sprite[6,5];

    [SerializeField] GameObject Plateau;
    
    [SerializeField] GameObject cartes;

    string currentTurnData = "";
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() => ButtonClicked());
        SansHUD.data.AppendLine("Tour no° " + Partie.Tour);
        SansHUD.data.AppendLine("Joueur;Dimension;Loco;Conduite;Equi1;Equi2;Equi3");
    }

    void OnEnable()
    {
        //Tour.NbCartesPosees = 0;
        if (Partie.Type == "expert")
        {
            MyDecideurMessage msg = new MyDecideurMessage();
            foreach (Joueur j in Partie.Joueurs)
            {
                if (j.IsPrive)
                    msg.priv = j.Numero;
                else if (j.IsPublic)
                    msg.pub = j.Numero;
            }
            NetworkServer.SendToAll(decideurID, msg);
        }

        if (Partie.Langue == "FR")
            button.transform.GetChild(0).GetComponent<Text>().text = "Présentation terminée";
        else
            button.transform.GetChild(0).GetComponent<Text>().text = "Presentation done";

        children.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Partie.Langue + "/Plateau");
        children.transform.GetChild(0).transform.GetChild(7).GetComponent<Image>().sprite = Resources.Load<Sprite>(Partie.Langue + "/Conduite/Conduite");

        RandomDim();
        RandomEqui();
        RandomLoco();

        #region players cards display
        LoadSprite(dimension, dimensionssprites);
        LoadSprite(locomotion, locomotionsprites);
        LoadSprite(equipement, equipementsprites);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if (Tour.NbCartesPosees == 6)
        {
            button.gameObject.SetActive(true);
        }
        else
        {
            button.gameObject.SetActive(false);
        }
    }

    private void ButtonClicked()
    {
        #region recup données
        /*
           currentTurnData = "J " + Partie.JoueurCourant + ";" + Partie.Joueurs[Partie.JoueurCourant-1].Dim.name + ";" + Partie.Joueurs[Partie.JoueurCourant-1].Loco.name + ";";
           currentTurnData += autonomie + ";";

           if (manualEquipmentCards != null)
           {
               foreach (string s in manualEquipmentCards)

                   currentTurnData += "M " + s + ";";
           }
           if (programmableEquipmentCards != null)
           {
               foreach (string s in programmableEquipmentCards)

                   currentTurnData += "P " + s + ";";
           }
           if (autoEquipmentCards != null)
           {
               foreach (string s in autoEquipmentCards)

                   currentTurnData += "A " + s + ";";
           }
           // suppression du dernier point-virgule
           currentTurnData = currentTurnData.Remove(currentTurnData.Length - 1);
           SansHUD.data.AppendLine(currentTurnData);
           //print(SansHUD.data.ToString());
          // string filePath = "donnees\\cartes_rejetees_le_" + DateTime.Now.ToString("dd-MM-yyyy") + "_a_" + DateTime.Now.ToString("hh") + "h" + DateTime.Now.ToString("mm") + "m" + DateTime.Now.ToString("ss") + "s" + ".csv";

          // File.AppendAllText(filePath, SansHUD.data.ToString());
       */
        #endregion
        if (Partie.Type == "expert")
        {
            Debug.Log(Partie.Joueurs.Count);
            foreach (Joueur j in Partie.Joueurs)
            {
                if (!j.IsPrive && !j.IsPublic)
                {
                    RandomActeur(j);
                }
            }
        }
        else
        {
            MyNetworkMessage msg = new MyNetworkMessage();
            msg.message = Partie.JoueurCourant;
            NetworkServer.SendToAll(presID, msg);
        }
        canvas_plateau_vehicule.SetActive(false);
        canvas_pres_persos.SetActive(true);
    }

    private void RandomActeur(Joueur j)
    {
        Main.TabImage tab = Main.Global.TabA;
        int x = Random.Range(0, tab.Taille);
        int y = Random.Range(0, tab.Taille);
        int z = Random.Range(0, tab.Taille);
        Main.Image[] acteurs = new Main.Image[3];
        acteurs[0] = tab.getImageind(x);
        acteurs[1] = tab.getImageind(y);
        acteurs[2] = tab.getImageind(z);

        j.Acteurs = new Sprite[3];

        for (int i = 0; i < acteurs.Length; i++)
        {
            j.Acteurs[i] = acteurs[i].sprite;
        }
        
        for (int k = 0; k < acteurs.Length; k++)
        {
            tab.removeImage(acteurs[k]);
        }        

        MyActeurMessage msg = new MyActeurMessage();
        msg.numero = j.Numero;
        msg.acteur1 = j.Acteurs[0].ToString().Substring(0, j.Acteurs[0].ToString().Length - 21);
        msg.acteur2 = j.Acteurs[1].ToString().Substring(0, j.Acteurs[1].ToString().Length - 21);
        msg.acteur3 = j.Acteurs[2].ToString().Substring(0, j.Acteurs[2].ToString().Length - 21);
        NetworkServer.SendToAll(conceptionID, msg);
    }

    #region random
    private void RandomDim()
    {
        int x = 0, y = 0;
        Main.TabImage tab = Main.Global.TabD;
        while (tab.getImageind(x).Sprite.Equals(tab.getImageind(y).Sprite))
        {
            x = Random.Range(0, tab.Taille);
            y = Random.Range(0, tab.Taille);
        }
        Main.Image[] dimensions = new Main.Image[2];
        dimensions[0] = tab.getImageind(x);
        dimensions[1] = tab.getImageind(y);

        Sprite[] dim = new Sprite[dimensions.Length];

        for (int i = 0; i < dimensions.Length; i++)
        {
            dim[i] = dimensions[i].Sprite;
        }

        dimensionssprites = dim;
    }

    private void RandomLoco()
    {
        int x = 0, y = 0;
        Main.TabImage tab = Main.Global.TabL;
        while (tab.getImageind(x).Sprite.Equals(tab.getImageind(y).Sprite))
        {
            x = Random.Range(0, (tab.Taille - 1));
            y = Random.Range(0, (tab.Taille - 1));
        }
        Main.Image[] locomotions = new Main.Image[2];
        locomotions[0] = tab.getImageind(x);
        locomotions[1] = tab.getImageind(y);

        Sprite[] loco = new Sprite[2];

        for (int i = 0; i < locomotions.Length; i++)
        {
            loco[i] = locomotions[i].Sprite;
        }

        locomotionsprites = loco;
    }

    private void RandomEqui()
    {
        Main.TabImage tab = Main.Global.TabE;
        int[] indices = { 0, 0, 0, 0, 0, 0 };
        bool allDiff = false;
        while (!allDiff)
        {
            for (int i = 0; i < 6; i++)
            {
                indices[i] = Random.Range(0, tab.Taille - 1);
            }
            allDiff = true;
            for (int i = 0; i < indices.Length - 1; i++)
            {
                for (int j = i + 1; j < indices.Length; j++)
                {
                    if (tab.getImageind(indices[i]).Sprite.Equals(tab.getImageind(indices[j]).Sprite))
                    {
                        allDiff = false;
                    }
                }
            }
        }
        Main.Image[] equipements = new Main.Image[6];
        for (int i = 0; i < equipements.Length; i++)
        {
            equipements[i] = tab.getImageind(indices[i]);
        }

        Sprite[] equi = new Sprite[6];

        for (int i = 0; i < equipements.Length; i++)
            equi[i] = equipements[i].Sprite;

        equipementsprites = equi;
    }
    #endregion

    private void LoadSprite(GameObject[] cards, Sprite[] sprites)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].GetComponent<Image>().sprite = sprites[i];
        }
    }

}
