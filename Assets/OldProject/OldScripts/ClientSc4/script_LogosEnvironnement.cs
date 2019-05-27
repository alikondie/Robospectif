using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class script_LogosEnvironnement : MonoBehaviour
{
    private int[] choixZone; // TABLEAU A RECUPERER 

    public Button button;
    private int position;
    public Image perso;
    private string persoSprite;

    public static NetworkClient client = Valider.client;
    short persosID = 1007;

    public GameObject rural;
    public Material couleurRural;
    public GameObject periUrbain;
    public Material couleurPeriUrbain;
    public GameObject urbain;
    public Material couleurUrbain;

    public Material couleurChoisie;


    private bool zoneToucher1;
    private bool zoneToucher2;
    private bool zoneToucher3;

    private int nombreChoix;

    

    // METHODE

    // Start is called before the first frame update
    void Start()
    {
        position = selectUser.positionStatic;
        string spriteString = perso.sprite.ToString();
        string s = "";
        for (int i = 0; i < spriteString.Length - 21; i++)
        {
            s = s + spriteString[i];
        }
        persoSprite = s;

        choixZone = new int[2];
        choixZone[0] = 0;
        choixZone[1] = 0;

        button.onClick.AddListener(() => ButtonClicked());
        button.gameObject.SetActive(false);

        rural.GetComponent<Image>().material = couleurRural;
        periUrbain.GetComponent<Image>().material = couleurPeriUrbain;
        urbain.GetComponent<Image>().material = couleurUrbain;
        zoneToucher1 = false;
        zoneToucher2 = false;
        zoneToucher3 = false;

        nombreChoix = 0;

    }

    private void ButtonClicked()
    {
        Debug.Log("choixZone[0] = " + choixZone[0] + " | choixZone[1] = " + choixZone[1]);
        MyPersoMessage msg = new MyPersoMessage();
        msg.numero = position;
        msg.image = persoSprite;
        msg.choixZone0 = choixZone[0];
        msg.choixZone1 = choixZone[1];
        client.Send(persosID, msg);
        SceneManager.LoadScene("Scene_ChoixJetons");
    }

    void Update()
    {
        if (zoneToucher1)
        {
            choixZone[0] = 1;
            if (zoneToucher2)
            {
                choixZone[1] = 2;
            }
            else
            {
                if (zoneToucher3)
                {
                    choixZone[1] = 3;
                }
                else
                {
                    choixZone[1] = 0;
                }
            }
        }
        else
        {
            if (zoneToucher2)
            {
                choixZone[0] = 2;
                if (zoneToucher3)
                {
                    choixZone[1] = 3;
                }
                else
                {
                    choixZone[1] = 0;
                }
            }
            else
            {
                if (zoneToucher3)
                {
                    choixZone[0] = 3;
                    choixZone[1] = 0;
                }
                else
                {
                    choixZone[0] = 0;
                    choixZone[1] = 0;
                }
            }
        }

        if (choixZone[0] == 0) button.gameObject.SetActive(false);
        else button.gameObject.SetActive(true);

    }


    // TROIS ZONES

    // Actions lorsqu'on click sur le bouton Rural
    public void zoneRural()
    {
        if (!zoneToucher1)
        {
            if(nombreChoix < 2)
            {
                rural.GetComponent<Image>().material = couleurChoisie;
                zoneToucher1 = true;
                nombreChoix++;
            }

        }
        else
        {
            rural.GetComponent<Image>().material = couleurRural;
            zoneToucher1 = false;
            nombreChoix--;
        }
        
    }

    // Actions lorsqu'on click sur le bouton Péripherique urbaine
    public void zonePeriUrbaine()
    {

        if (!zoneToucher2)
        {
            if (nombreChoix < 2)
            {
                periUrbain.GetComponent<Image>().material = couleurChoisie;
                zoneToucher2 = true;
                nombreChoix++;
            }
                
        }
        else
        {
            periUrbain.GetComponent<Image>().material = couleurPeriUrbain;
            zoneToucher2 = false;
            nombreChoix--;
        }
    }

    // Actions lorsqu'on click sur le bouton Urbaine
    public void zoneUrbaine()
    {
        if (!zoneToucher3)
        {
            if (nombreChoix < 2)
            {
                urbain.GetComponent<Image>().material = couleurChoisie;
                zoneToucher3 = true;
                nombreChoix++;
            }               
        }
        else
        {
            urbain.GetComponent<Image>().material = couleurUrbain;
            zoneToucher3 = false;
            nombreChoix--;
        }
    }
}
