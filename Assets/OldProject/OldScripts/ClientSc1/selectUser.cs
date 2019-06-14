using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class selectUser : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject canvas_choix_cartes;
    [SerializeField] GameObject canvas_position_joueurs;
    [SerializeField] Button button;
    private int positionEffective;
    [SerializeField] int position;
    short messageID = 1000;
    

    void Start()
    {
        /*button.gameObject.SetActive(false);
        button.onClick.AddListener(() => ButtonClicked());*/
    }

    void Update()
    {
    }

    void ButtonClicked()
    {
        /*JoueurStatic.Numero = positionEffective;
        JoueurStatic.Position = position;
        MyNetworkMessage message = new MyNetworkMessage();
        message.message = positionEffective;
        JoueurStatic.Client.Send(messageID, message);
        canvas_position_joueurs.SetActive(false);
        canvas_choix_cartes.SetActive(true);*/
        //SceneManager.LoadScene("scene2");
    }
}

