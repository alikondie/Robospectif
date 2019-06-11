using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.Networking;

public class MainScript : MonoBehaviour
{
    public static Main.Player player;
    public static Joueur joueur;
    public Text text;
    public Image dimensionGO1;
    public Image locomotionGO1;
    public Image equipementGO1;
    public Image equipementGO2;
    public Image equipementGO3;
    public static Main.Image[] dimensions;
    public static Main.Image[] locomotions;
    public static Main.Image[] equipements;
    private int position = selectUser.positionStatic;

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

        equipementGO1.sprite = equipements[0].Sprite;
        equipementGO2.sprite = equipements[1].Sprite;
        equipementGO3.sprite = equipements[2].Sprite;
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

        locomotionGO1.sprite = locomotions[0].Sprite;
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

        dimensionGO1.sprite = dimensions[0].Sprite;
    }


    // Start is called before the first frame update
    void Start()
    {
        joueur = new Joueur();
        joueur.Numero = position;        
        text.text = "Joueur : " + position.ToString();

        RandomDim();
        
        RandomLoco();

        RandomEqui();
    }


    // Update is called once per frame
    void Update()
    {
        text.text = "Joueur : " + position.ToString();
    }
}
