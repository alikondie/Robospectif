using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class selectUser : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button;
    private int positionEffective;
    public static int positionStatic;
    public int position;
    public static int zone;
    public static NetworkClient client = Init.client;
    public Scene sceneSuivante;
    short messageID = 1000;
    

    void Start()
    {
        button.gameObject.SetActive(false);
        button.onClick.AddListener(() => ButtonClicked());
    }

    void Update()
    {
        positionEffective = (Init.positions[position - 1]);
    }

    void ButtonClicked()
    {
        positionStatic = positionEffective;
        zone = position;
        MyNetworkMessage message = new MyNetworkMessage();
        message.message = positionEffective;
        client.Send(messageID, message);
        SceneManager.LoadScene("scene2");
    }
}

