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
    [SerializeField] GameObject[] dimensionGO;
    [SerializeField] GameObject[] locomotionGO;
    [SerializeField] GameObject[] equipementGO;
    private Main.Image[] dimensions;
    private Main.Image[] locomotions;
    private Main.Image[] equipements;


    // Start is called before the first frame update
    void Start()
    {
        /*RandomDim();

        RandomLoco();

        RandomEqui();*/

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

            JoueurStatic.Dimensions[0] = Main.Global.tabD.Tabsprite.Find(v.dim1);

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
        equipements = new Main.Image[6];
        for (int i = 0; i < equipements.Length; i++)
        {
            equipements[i] = tab.getImageind(indices[i]);
        }

        for (int i = 0; i < equipements.Length; i++)
        {
            Main.Global.TabE.removeImage(equipements[i]);
        }

        /*equipementGO1.sprite = equipements[0].Sprite;
        equipementGO2.sprite = equipements[1].Sprite;
        equipementGO3.sprite = equipements[2].Sprite;*/

        JoueurStatic.Equipements = new Sprite[equipements.Length];

        for (int i = 0; i < equipements.Length; i++)
        {
            JoueurStatic.Equipements[i] = equipements[i].Sprite;
        }
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
        locomotions = new Main.Image[2];
        locomotions[0] = tab.getImageind(x);
        locomotions[1] = tab.getImageind(y);
        Main.Global.TabL.removeImage(locomotions[0]);
        Main.Global.TabL.removeImage(locomotions[1]);

        //locomotionGO1.sprite = locomotions[0].Sprite;

        JoueurStatic.Locomotions = new Sprite[locomotions.Length];

        for (int i = 0; i < locomotions.Length; i++)
        {
            JoueurStatic.Locomotions[i] = locomotions[i].Sprite;
        }
    } 

    private void RandomDim()
    {
        int x = 0, y = 0;
        Main.TabImage tab = Main.Global.TabD;
        while (tab.getImageind(x).Sprite.Equals(tab.getImageind(y).Sprite))
        {
            x = Random.Range(0, tab.Taille);
            y = Random.Range(0, tab.Taille);
        }
        dimensions = new Main.Image[2];
        dimensions[0] = tab.getImageind(x);
        dimensions[1] = tab.getImageind(y);
        Main.Global.TabD.removeImage(dimensions[0]);
        Main.Global.TabD.removeImage(dimensions[1]);

        //dimensionGO1.sprite = dimensions[0].Sprite;

        JoueurStatic.Dimensions = new Sprite[dimensions.Length];

        for (int i = 0; i < dimensions.Length; i++)
        {
            JoueurStatic.Dimensions[i] = dimensions[i].Sprite;
        }
    }
}
