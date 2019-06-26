using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.Networking;

public class MainScript : MonoBehaviour
{
    short cardID = 1009;

    public static Main.Player player;
    [SerializeField] Text text;
    [SerializeField] Button button;
    [SerializeField] GameObject[] dimensionGO;
    [SerializeField] GameObject[] locomotionGO;
    [SerializeField] GameObject[] equipementGO;
    private Main.Image[] dimensions;
    private Main.Image[] locomotions;
    private Main.Image[] equipements;


    // Start is called before the first frame update
    void Start()
    {
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


    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        text.text = "Joueur : " + JoueurStatic.Numero.ToString();
    }

    private void OnCardsReceived(NetworkMessage netMsg)
    {
        var v = netMsg.ReadMessage<MyCardMessage>();
        if (v.num == JoueurStatic.Numero)
        {
            JoueurStatic.Dimensions = new Sprite[2];
            JoueurStatic.Locomotions = new Sprite[2];
            JoueurStatic.Equipements = new Sprite[6];
            JoueurStatic.Dimensions[0] = Resources.Load<Sprite>("image/Dimension/" + v.dim1);
            JoueurStatic.Dimensions[1] = Resources.Load<Sprite>("image/Dimension/" + v.dim2);
            JoueurStatic.Locomotions[0] = Resources.Load<Sprite>("image/Locomotion/" + v.loco1);
            JoueurStatic.Locomotions[1] = Resources.Load<Sprite>("image/Locomotion/" + v.loco2);
            JoueurStatic.Equipements[0] = Resources.Load<Sprite>("image/Equipements/" + v.equi1);
            JoueurStatic.Equipements[1] = Resources.Load<Sprite>("image/Equipements/" + v.equi2);
            JoueurStatic.Equipements[2] = Resources.Load<Sprite>("image/Equipements/" + v.equi3);
            JoueurStatic.Equipements[3] = Resources.Load<Sprite>("image/Equipements/" + v.equi4);
            JoueurStatic.Equipements[4] = Resources.Load<Sprite>("image/Equipements/" + v.equi5);
            JoueurStatic.Equipements[5] = Resources.Load<Sprite>("image/Equipements/" + v.equi6);
            JoueurStatic.Persos[0] = Resources.Load<Sprite>("image/Personnages/" + v.perso1);
            JoueurStatic.Persos[1] = Resources.Load<Sprite>("image/Personnages/" + v.perso2);
            JoueurStatic.Persos[2] = Resources.Load<Sprite>("image/Personnages/" + v.perso3);
            JoueurStatic.Persos[3] = Resources.Load<Sprite>("image/Personnages/" + v.perso4);
            JoueurStatic.Persos[4] = Resources.Load<Sprite>("image/Personnages/" + v.perso5);
            JoueurStatic.Persos[5] = Resources.Load<Sprite>("image/Personnages/" + v.perso6);

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
