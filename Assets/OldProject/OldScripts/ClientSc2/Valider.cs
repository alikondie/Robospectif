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

    public Button button;
    public Image loco;
    public Image dim;
    public Image equi0;
    public Image equi1;
    public Image equi2;
    public static Joueur joueur;
    public static int position;
    public static NetworkClient client = selectUser.client;
    short idMessage = 1001;
    short conceptionID = 1002;
    short chronoID = 1003;


    // Start is called before the first frame update
    void Start()
    {
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
            indices[0] = Random.Range(0, tab.Taille);
            indices[1] = Random.Range(0, tab.Taille);
            indices[2] = Random.Range(0, tab.Taille);
            indices[3] = Random.Range(0, tab.Taille);
            indices[4] = Random.Range(0, tab.Taille);
            indices[5] = Random.Range(0, tab.Taille);
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
        personnages[0] = tab.getImageind(indices[0]);
        personnages[1] = tab.getImageind(indices[1]);
        personnages[2] = tab.getImageind(indices[2]);
        personnages[3] = tab.getImageind(indices[3]);
        personnages[4] = tab.getImageind(indices[4]);
        personnages[5] = tab.getImageind(indices[5]);
        Main.Global.TabE.removeImage(personnages[0]);
        Main.Global.TabE.removeImage(personnages[1]);
        Main.Global.TabE.removeImage(personnages[2]);
        Main.Global.TabE.removeImage(personnages[3]);
        Main.Global.TabE.removeImage(personnages[4]);
        Main.Global.TabE.removeImage(personnages[5]);

        Debug.Log("perso : " + personnages[0].Sprite.ToString());
        Debug.Log("perso : " + personnages[1].Sprite.ToString());
        Debug.Log("perso : " + personnages[2].Sprite.ToString());
        Debug.Log("perso : " + personnages[3].Sprite.ToString());
        Debug.Log("perso : " + personnages[4].Sprite.ToString());
        Debug.Log("perso : " + personnages[5].Sprite.ToString());

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
        MyImageMessage test = new MyImageMessage();
        MyImageMessage robot = new MyImageMessage();
        //string s = UnityEditor.AssetDatabase.GetAssetPath(loco.sprite);
        robot.loco= loco.sprite.ToString();
        robot.dim = dim.sprite.ToString();
        robot.equi1 = equi0.sprite.ToString();
        robot.equi2 = equi1.sprite.ToString();
        robot.equi3 = equi2.sprite.ToString();
        robot.num = position;
       // Debug.Log("zone : " + selectUser.zone);
        robot.zone = selectUser.zone;
        client.Send(idMessage, robot);
        RandomPerso();
        SceneManager.LoadScene("scene2bis");
        //if (position == SansHUD.premierFini) {
        //    MyImageMessage mLoco = new MyImageMessage();
        //    mLoco.image = loco.sprite.ToString();
        //    mLoco.type = "Locomotion";
        //    MyImageMessage mDim = new MyImageMessage();
        //    mDim.image = dim.sprite.ToString().ToString();
        //    mDim.type = "Dimension";
        //    MyImageMessage mEqui0 = new MyImageMessage();
        //    mEqui0.image = equi0.sprite.ToString().ToString();
        //    mEqui0.type = "Equipements";
        //    MyImageMessage mEqui1 = new MyImageMessage();
        //    mEqui1.image = equi1.sprite.ToString().ToString();
        //    mEqui1.type = "Equipements";
        //    MyImageMessage mEqui2 = new MyImageMessage();
        //    mEqui2.image = equi2.sprite.ToString().ToString();
        //    mEqui2.type = "Equipements";
        //    client.Send(idMessage, mLoco);
        //    client.Send(idMessage, mDim);
        //    client.Send(idMessage, mEqui0);
        //    client.Send(idMessage, mEqui1);
        //    client.Send(idMessage, mEqui2);
        //}        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
