using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using Random = UnityEngine.Random;

public class Button_ready_next_scene : MonoBehaviour
{
    [SerializeField] GameObject canvas_mains;
    [SerializeField] GameObject canvas_joueurs;
    [SerializeField] Text nb_joueurs;
    [SerializeField] Button[] hands;
    [SerializeField] Sprite[] colors;

    private int[] indices;
    private int[] positions;
    short positionsID = 1005;

    // Start is called before the first frame update
    void Start()
    {
        if (Partie.Langue == "FR")
            this.transform.GetChild(0).GetComponent<Text>().text = "C'est parti";
        else
            this.transform.GetChild(0).GetComponent<Text>().text = "Let's play";

        for (int i = 0; i < hands.Length; i++)
        {
            PlayerPrefs.SetInt("LaPosition" + (i + 1), 0);
        }
        this.gameObject.SetActive(false);
        positions = new int[6];
        indices = new int[hands.Length];
        for (int i = 0; i < indices.Length; i++)
        {
            indices.SetValue(0, i);
        }
        hands[0].onClick.AddListener(() => onHandClicked(0));
        hands[1].onClick.AddListener(() => onHandClicked(1));
        hands[2].onClick.AddListener(() => onHandClicked(2));
        hands[3].onClick.AddListener(() => onHandClicked(3));
        hands[4].onClick.AddListener(() => onHandClicked(4));
        hands[5].onClick.AddListener(() => onHandClicked(5));
    }

    private void onHandClicked(int i)
    {
        indices[i] = (indices[i] + 1) % 2;
        
        hands[i].GetComponent<Animator>().SetTrigger("Shake");
        if (hands[i].gameObject.GetComponent<Image>().color == Color.green)
            hands[i].gameObject.GetComponent<Image>().color = Color.white;
        else
            hands[i].gameObject.GetComponent<Image>().color = Color.green;

        MiseAJourText();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void MiseAJourText()
    {
        int nb = 0;
        for (int i = 0; i < indices.Length; i++)
        {
            nb += indices[i];
            if (indices[i] == 1)
                PlayerPrefs.SetInt("LaPosition" + (i + 1), nb);
            else
                PlayerPrefs.SetInt("LaPosition" + (i + 1), 0);
        }
        if (nb >= 1)
            this.gameObject.SetActive(true);
        else
            this.gameObject.SetActive(false);

        if (Partie.Langue == "FR")
            nb_joueurs.text = "Il y a " + nb + " joueurs enregistrés";
        else
            nb_joueurs.text = nb + " players are registered";
        PlayerPrefs.SetInt("nbJoueur", nb);    //Envoie le nombre de Joueur
    }

    void OnMouseDown()
    {
        // ----- PARTIE NATHAN -----
        // Envoi des positions
        for (int i = 1; i <= 6; i++)
        {
            PlayerPrefs.SetInt("P" + i, PlayerPrefs.GetInt("LaPosition" + i));
        }
        // Envoies le nombre de joueur
        PlayerPrefs.SetInt("nombreJoueur", PlayerPrefs.GetInt("nbJoueur"));
        int indice = 1;
        for (int i = 0; i <= 5; i++)
        {
            if (PlayerPrefs.GetInt("P" + (i + 1)) == 0)
            {
                positions[i] = 0;
            }
            else
            {
                positions[i] = indice;
                indice++;
            }     // Position des joueurs
        }
        Partie.Positions = positions;

        for (int i = 0; i < Partie.Positions.Length; i++)
        {
            if (Partie.Positions[i] != 0)
            {
                Joueur j = new Joueur();
                j.Numero = Partie.Positions[i];
                j.Position = i;
                j.Dimensions = RandomDim();
                j.Locomotions = RandomLoco();
                j.Equipements = RandomEqui();
                j.Persos = RandomPerso();
                Partie.AddPlayer(j);
            }
        }

        MyPositionsMessage message = new MyPositionsMessage();
        message.position1 = positions[0];
        message.position2 = positions[1];
        message.position3 = positions[2];
        message.position4 = positions[3];
        message.position5 = positions[4];
        message.position6 = positions[5];
        message.langue = Partie.Langue;
        NetworkServer.SendToAll(positionsID, message);

        // --------------------------

        // Debug.Log("Click");
        //SceneManager.LoadScene("Scene_2");
        canvas_mains.SetActive(false);
        canvas_joueurs.SetActive(true);
    }

    private Sprite[] RandomDim()
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
        Main.Global.TabD.removeImage(dimensions[0]);
        Main.Global.TabD.removeImage(dimensions[1]);

        Sprite[] dim = new Sprite[dimensions.Length];

        for (int i = 0; i < dimensions.Length; i++)
        {
            dim[i] = dimensions[i].Sprite;
        }

        return dim;
    }

    private Sprite[] RandomLoco()
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
        Main.Global.TabL.removeImage(locomotions[0]);
        Main.Global.TabL.removeImage(locomotions[1]);

        Sprite[] loco = new Sprite[2];

        for (int i = 0; i < locomotions.Length; i++)
        {
            loco[i] = locomotions[i].Sprite;
        }

        return loco;
    }

    private Sprite[] RandomEqui()
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

        for (int i = 0; i < equipements.Length; i++)
        {
            Main.Global.TabE.removeImage(equipements[i]);
        }

        Sprite[] equi = new Sprite[6];

        for (int i = 0; i < equipements.Length; i++)
            equi[i] = equipements[i].Sprite;

        return equi;
    }

    private Sprite[] RandomPerso()
    {
        Main.TabImage tab = Main.Global.TabP;
        int[] indices = { 0, 0, 0, 0, 0, 0 };
        bool allDiff = false;
        while (!allDiff)
        {
            for (int i = 0; i < indices.Length; i++)
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
        Main.Image[] personnages = new Main.Image[6];
        for (int i = 0; i < personnages.Length; i++)
        {
            personnages[i] = tab.getImageind(indices[i]);
        }

        for (int i = 0; i < personnages.Length; i++)
        {
            Main.Global.TabP.removeImage(personnages[i]);
        }

        Sprite[] persos = new Sprite[personnages.Length];

        for (int i = 0; i < personnages.Length; i++)
        {
            persos[i] = personnages[i].Sprite;
        }

        return persos;

    }

}
