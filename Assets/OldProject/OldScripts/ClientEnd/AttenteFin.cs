using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AttenteFin : MonoBehaviour
{
    [SerializeField] GameObject canvas_fin_partie;
    [SerializeField] GameObject canvas_vainqueur;
    [SerializeField] GameObject canvas_pres_robot;
    [SerializeField] Text text;

    private int position;

    short nextID = 1015;
    public static NetworkClient client;
    public static Joueur joueur;

    // Start is called before the first frame update
    void Start()
    {
        joueur = Valider.joueur;
        client = Valider.client;
        text.text = "Joueur : " + joueur.Numero.ToString();
        client.RegisterHandler(nextID, onWaitReceived);
    }

    private void onWaitReceived(NetworkMessage netMsg)
    {
        string msg = netMsg.ReadMessage<MyStringMessage>().s;
        if (msg.Equals("next"))
        {
            canvas_vainqueur.SetActive(false);
            canvas_pres_robot.SetActive(true);
            //SceneManager.LoadScene("scene2bis");
        }
        else
        {
            canvas_vainqueur.SetActive(false);
            canvas_fin_partie.SetActive(true);
            //SceneManager.LoadScene("FinTel");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
