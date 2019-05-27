using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinTour : MonoBehaviour
{
    public Button prochain;
    public Button fin;

    short nextID = 1015;

    // Start is called before the first frame update
    void Start()
    {
        prochain.onClick.AddListener(() => ProchainClicked());
        fin.onClick.AddListener(() => FinClicked());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ProchainClicked()
    {
        MyStringMessage next = new MyStringMessage();
        next.s = "next";
        NetworkServer.SendToAll(nextID, next);
        SceneManager.LoadScene("Scene_4");
    }

    private void FinClicked()
    {
        MyStringMessage end = new MyStringMessage();
        end.s = "end";
        NetworkServer.SendToAll(nextID, end);
        SceneManager.LoadScene("SceneFin");
    }
}
