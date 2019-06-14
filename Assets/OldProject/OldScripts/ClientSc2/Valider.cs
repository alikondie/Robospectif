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

    [SerializeField] Button button;
    [SerializeField] Image loco;
    [SerializeField] Image dim;
    [SerializeField] Image equi0;
    [SerializeField] Image equi1;
    [SerializeField] Image equi2;
    short idMessage = 1001;
    short conceptionID = 1002;
    short chronoID = 1003;


    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() => ButtonClicked());
        JoueurStatic.Client.RegisterHandler(chronoID, onChronoReceived);
    }

    private void onChronoReceived(NetworkMessage netMsg)
    {
        int joueurFini = netMsg.ReadMessage<MyNetworkMessage>().message;
        if (joueurFini != JoueurStatic.Numero)
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

        for (int i = 0; i < personnages.Length; i++)
        {
            JoueurStatic.Persos[i] = personnages[i].Sprite;
        }

    }

    private void ButtonClicked()
    {
        JoueurStatic.Dim = dim.sprite;
        JoueurStatic.Loco = loco.sprite;
        JoueurStatic.Equi1 = equi0.sprite;
        JoueurStatic.Equi2 = equi1.sprite;
        JoueurStatic.Equi3 = equi2.sprite;
        MyNetworkMessage conception = new MyNetworkMessage();
        conception.message = JoueurStatic.Numero;
        JoueurStatic.Client.Send(conceptionID, conception);
        MyImageMessage robot = new MyImageMessage();
        robot.loco= loco.sprite.ToString();
        robot.dim = dim.sprite.ToString();
        robot.equi1 = equi0.sprite.ToString();
        robot.equi2 = equi1.sprite.ToString();
        robot.equi3 = equi2.sprite.ToString();
        robot.num = JoueurStatic.Numero;
        robot.zone = JoueurStatic.Position;
        JoueurStatic.Client.Send(idMessage, robot);
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
