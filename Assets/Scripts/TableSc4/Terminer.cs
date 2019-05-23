using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Terminer : MonoBehaviour
{

    public Button button;
    short waitID = 1006;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() => ButtonClicked());
    }

    private void ButtonClicked()
    {
        int joueurCourant = Initialisation.premierJoueur;
        MyNetworkMessage msg = new MyNetworkMessage();
        msg.message = joueurCourant;
        NetworkServer.SendToAll(waitID, msg);
        SceneManager.LoadScene("Scene5");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
