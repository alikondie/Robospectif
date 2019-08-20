using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class wait_type_partie : MonoBehaviour
{

    short startID = 1025;

    // Start is called before the first frame update
    void Start()
    {
        JoueurStatic.Client.RegisterHandler(startID, OnStartReceived);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnStartReceived(NetworkMessage netMsg)
    {
        string msg = netMsg.ReadMessage<MyStringMessage>().s;

        if (msg == "expert")
        {
            SceneManager.LoadScene("expert_game_client");
        }
        else
        {
            SceneManager.LoadScene("standard_game_client");
        }
    }
}
