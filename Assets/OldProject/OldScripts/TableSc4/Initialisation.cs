using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static Main;
using Image = UnityEngine.UI.Image;

public class Initialisation : MonoBehaviour
{
    #region variables
    [SerializeField] GameObject canvas_plateau_vehicule;
    [SerializeField] GameObject canvas_pres_persos;
    [SerializeField] GameObject children;

    private int pos;

    [SerializeField] Button button;

    public static List<string> manualEquipmentCards;
    public static List<string> programmableEquipmentCards;
    public static List<string> autoEquipmentCards;
    private int nbCartePosees;
    public static string autonomie;

    short presID = 1011;
    short CartePoseeID = 1022;
    public static int indice = 0;
    public static Sprite[,] images = new Sprite[6,5];



    private Vector2[] posCards;

    [SerializeField] GameObject Plateau;
    
    [SerializeField] GameObject cartes;

    string currentTurnData = "";
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        NetworkServer.RegisterHandler(CartePoseeID, OnCartePoseeReceived);
        button.onClick.AddListener(() => ButtonClicked());
    }

    void OnEnable()
    {
        nbCartePosees = 0;
        if (Partie.Langue == "FR")
            button.transform.GetChild(0).GetComponent<Text>().text = "Présentation terminée";
        else
            button.transform.GetChild(0).GetComponent<Text>().text = "Presentation done";

        children.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(Partie.Langue + "/Plateau");
        children.transform.GetChild(0).transform.GetChild(7).GetComponent<Image>().sprite = Resources.Load<Sprite>(Partie.Langue + "/Conduite/Conduite");

        
        posCards = new Vector2[6];
        posCards[0] = new Vector2(560, 190);
        posCards[1] = new Vector2(1360, 190);
        posCards[2] = new Vector2(1730, 540);
        posCards[3] = new Vector2(1360, 890);
        posCards[4] = new Vector2(560, 890);
        posCards[5] = new Vector2(190, 540);
        pos = Array.IndexOf(Partie.Positions, Partie.JoueurCourant) + 1;
        Rotate(pos);
        #region players cards display
        foreach (Joueur j in Partie.Joueurs)
        {
            if (j.Numero == Partie.JoueurCourant)
            {
                cartes.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = j.Dim;
                cartes.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = j.Equi1;
                cartes.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = j.Equi2;
                cartes.transform.GetChild(3).gameObject.GetComponent<Image>().sprite = j.Equi3;
                cartes.transform.GetChild(4).gameObject.GetComponent<Image>().sprite = j.Loco;
            }
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if (nbCartePosees == 6)
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

            SansHUD.data.AppendLine("Tour " + Partie.Tour);
            SansHUD.data.AppendLine("Joueur;Dimension;Loco;Conduite;Equi1;Equi2;Equi3");
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
            if (manualEquipmentCards != null)
                manualEquipmentCards.Clear();
            if (programmableEquipmentCards != null)
                programmableEquipmentCards.Clear();
            if (autoEquipmentCards != null)
                autoEquipmentCards.Clear();
        // string filePath = "donnees\\cartes_rejetees_le_" + DateTime.Now.ToString("dd-MM-yyyy") + "_a_" + DateTime.Now.ToString("hh") + "h" + DateTime.Now.ToString("mm") + "m" + DateTime.Now.ToString("ss") + "s" + ".csv";

        // File.AppendAllText(filePath, SansHUD.data.ToString());
        #endregion
        MyNetworkMessage msg = new MyNetworkMessage();
        msg.message = Partie.JoueurCourant;
        NetworkServer.SendToAll(presID, msg);

        canvas_plateau_vehicule.SetActive(false);
        canvas_pres_persos.SetActive(true);
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
        cartes.transform.rotation = rotation;
        cartes.transform.position = posCards[pos - 1];
    }

    private void OnCartePoseeReceived(NetworkMessage netMsg)
    {
        nbCartePosees += netMsg.ReadMessage<MyNetworkMessage>().message;
    }
}
