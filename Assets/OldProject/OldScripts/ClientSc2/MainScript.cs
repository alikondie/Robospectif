using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.Networking;


////Script principal de choix des cartes (cotés client)
public class MainScript : MonoBehaviour
{
    short cardID = 1009;

    public static Main.Player player;
    [SerializeField] Text text;
    [SerializeField] Button button;
    [SerializeField] GameObject[] dimensionGO;
    [SerializeField] GameObject[] locomotionGO;
    [SerializeField] GameObject[] equipementGO;
    [SerializeField] Text[] choix;
    private Main.Image[] dimensions;
    private Main.Image[] locomotions;
    private Main.Image[] equipements;

    void Start()
    {
        ////Initialisation de tous les éléments d'UI
        button.gameObject.SetActive(false);

        foreach (GameObject i in dimensionGO)
        {
            i.SetActive(false);
        }

        foreach (GameObject i in locomotionGO)
        {
            i.SetActive(false);
        }

        foreach (GameObject i in equipementGO)
        {
            i.SetActive(false);
        }

        JoueurStatic.Client.RegisterHandler(cardID, OnCardsReceived);
    }

    ////à l'activation du canvas, les textes sont mis à jour selon la langue choisie
    void OnEnable()
    {
        if (JoueurStatic.Langue == "FR")
        {
            text.text = "Joueur " + JoueurStatic.Numero.ToString();
            button.transform.GetChild(0).GetComponent<Text>().text = "Valider";
            for (int i = 0; i < choix.Length; i++)
            {
                choix[i].text = "Choix " + (i + 1);
            }
        } else
        {
            text.text = "Player " + JoueurStatic.Numero;
            button.transform.GetChild(0).GetComponent<Text>().text = "Confirm";
            for (int i = 0; i < choix.Length; i++)
            {
                choix[i].text = "Choice " + (i + 1);
            }
        }
    }

    ////le serveur génère puis envoit les cartes à travers un message, puis le joueur les stocke et les affiche
    private void OnCardsReceived(NetworkMessage netMsg)
    {
        var v = netMsg.ReadMessage<MyCardMessage>();
        if (v.num == JoueurStatic.Numero)
        {
            JoueurStatic.Dimensions = new Sprite[2];
            JoueurStatic.Locomotions = new Sprite[2];
            JoueurStatic.Equipements = new Sprite[6];
            JoueurStatic.Dimensions[0] = Resources.Load<Sprite>(JoueurStatic.Langue + "/Dimension/" + v.dim1);
            JoueurStatic.Dimensions[1] = Resources.Load<Sprite>(JoueurStatic.Langue + "/Dimension/" + v.dim2);
            JoueurStatic.Locomotions[0] = Resources.Load<Sprite>(JoueurStatic.Langue + "/Locomotion/" + v.loco1);
            JoueurStatic.Locomotions[1] = Resources.Load<Sprite>(JoueurStatic.Langue + "/Locomotion/" + v.loco2);
            JoueurStatic.Equipements[0] = Resources.Load<Sprite>(JoueurStatic.Langue + "/Equipements/" + v.equi1);
            JoueurStatic.Equipements[1] = Resources.Load<Sprite>(JoueurStatic.Langue + "/Equipements/" + v.equi2);
            JoueurStatic.Equipements[2] = Resources.Load<Sprite>(JoueurStatic.Langue + "/Equipements/" + v.equi3);
            JoueurStatic.Equipements[3] = Resources.Load<Sprite>(JoueurStatic.Langue + "/Equipements/" + v.equi4);
            JoueurStatic.Equipements[4] = Resources.Load<Sprite>(JoueurStatic.Langue + "/Equipements/" + v.equi5);
            JoueurStatic.Equipements[5] = Resources.Load<Sprite>(JoueurStatic.Langue + "/Equipements/" + v.equi6);
            JoueurStatic.Persos[0] = Resources.Load<Sprite>(JoueurStatic.Langue + "/Personnages/" + v.perso1);
            JoueurStatic.Persos[1] = Resources.Load<Sprite>(JoueurStatic.Langue + "/Personnages/" + v.perso2);
            JoueurStatic.Persos[2] = Resources.Load<Sprite>(JoueurStatic.Langue + "/Personnages/" + v.perso3);
            JoueurStatic.Persos[3] = Resources.Load<Sprite>(JoueurStatic.Langue + "/Personnages/" + v.perso4);
            JoueurStatic.Persos[4] = Resources.Load<Sprite>(JoueurStatic.Langue + "/Personnages/" + v.perso5);
            JoueurStatic.Persos[5] = Resources.Load<Sprite>(JoueurStatic.Langue + "/Personnages/" + v.perso6);

            dimensionGO[0].GetComponent<Image>().sprite = JoueurStatic.Dimensions[0];
            locomotionGO[0].GetComponent<Image>().sprite = JoueurStatic.Locomotions[0];
            for (int i = 0; i < 3; i++)
                equipementGO[i].GetComponent<Image>().sprite = JoueurStatic.Equipements[i];

            foreach (GameObject i in dimensionGO)
            {
                i.SetActive(true);
            }

            foreach (GameObject i in locomotionGO)
            {
                i.SetActive(true);
            }

            foreach (GameObject i in equipementGO)
            {
                i.SetActive(true);
            }
        }
        button.gameObject.SetActive(true);
    }
}
