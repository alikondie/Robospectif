using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitPerso : MonoBehaviour
{
    public static Main.Player player;
    public Text text;
    public Image[] personnagesGO;
    public Image[] ticks;
    public static Main.Image[] personnages;
    private int nbJoueurs = Init.nbJoueurs;



    // Start is called before the first frame update
    void Start()
    {
        
        bool[] persosChoisis = new bool[] { JoueurStatic.Perso1Choisi, JoueurStatic.Perso2Choisi, JoueurStatic.Perso3Choisi, JoueurStatic.Perso4Choisi, JoueurStatic.Perso5Choisi, JoueurStatic.Perso6Choisi };
        for (int i = 0; i < ticks.Length; i++)
        {
            ticks[i].gameObject.SetActive(false);
        }
        
        text.text = "Joueur : " + selectUser.positionStatic;
        //Perso();
        for (int i = 0; i < persosChoisis.Length; i++)
        {
            if (persosChoisis[i])
                personnagesGO[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Perso()
    {
        personnagesGO[0].sprite = JoueurStatic.Perso1;
        personnagesGO[1].sprite = JoueurStatic.Perso2;
        personnagesGO[2].sprite = JoueurStatic.Perso3;
        personnagesGO[3].sprite = JoueurStatic.Perso4;
        personnagesGO[4].sprite = JoueurStatic.Perso5;
        personnagesGO[5].sprite = JoueurStatic.Perso6;
    }
}
