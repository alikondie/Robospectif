using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Valider : MonoBehaviour
{
    public static Main.Image[] personnages;
    [SerializeField] GameObject canvas_choix_cartes;
    [SerializeField] GameObject canvas_pres_robot;

    public Button button;
    public Image loco;
    public Image dim;
    public Image equi0;
    public Image equi1;
    public Image equi2;
    public static Joueur joueur;
    public static int position;
    public static NetworkClient client;
    short idMessage = 1001;
    short conceptionID = 1002;
    short chronoID = 1003;


    // Start is called before the first frame update
    void Start()
    {
        client = selectUser.client;
        button.onClick.AddListener(() => ButtonClicked());
        position = selectUser.positionStatic;
        client.RegisterHandler(chronoID, onChronoReceived);
    }

    private void onChronoReceived(NetworkMessage netMsg)
    {
        int joueurFini = netMsg.ReadMessage<MyNetworkMessage>().message;
        if (joueurFini != position)
        {
            ScriptTimer.debutChrono();
        }
    }

    private void RandomPerso()
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
        personnages = new Main.Image[6];
        for (int i = 0; i < personnages.Length; i++)
        {
            personnages[i] = tab.getImageind(indices[i]);
        }

        for (int i = 0; i < personnages.Length; i++)
        {
            Main.Global.TabP.removeImage(personnages[i]);
        }

        JoueurStatic.Perso1 = personnages[0].Sprite;
        JoueurStatic.Perso2 = personnages[1].Sprite;
        JoueurStatic.Perso3 = personnages[2].Sprite;
        JoueurStatic.Perso4 = personnages[3].Sprite;
        JoueurStatic.Perso5 = personnages[4].Sprite;
        JoueurStatic.Perso6 = personnages[5].Sprite;

    }

    private void ButtonClicked()
    {
        joueur = MainScript.joueur;
        joueur.Dim = dim.sprite;
        joueur.Loco = loco.sprite;
        joueur.Equi1 = equi0.sprite;
        joueur.Equi2 = equi1.sprite;
        joueur.Equi3 = equi2.sprite;
        MyNetworkMessage conception = new MyNetworkMessage();
        conception.message = position;
        client.Send(conceptionID, conception);
        MyImageMessage robot = new MyImageMessage();
        robot.loco= loco.sprite.ToString();
        robot.dim = dim.sprite.ToString();
        robot.equi1 = equi0.sprite.ToString();
        robot.equi2 = equi1.sprite.ToString();
        robot.equi3 = equi2.sprite.ToString();
        robot.num = position;
        robot.zone = selectUser.zone;
        client.Send(idMessage, robot);
        RandomPerso();
        canvas_choix_cartes.SetActive(false);
        canvas_pres_robot.SetActive(true);
        //SceneManager.LoadScene("scene2bis"); 
    }

    // Update is called once per frame
    void Update()
    {

    }
}
