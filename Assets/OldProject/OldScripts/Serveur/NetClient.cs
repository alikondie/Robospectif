using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetClient : NetworkDiscovery
{

    void Start()
    {
        startClient();
    }

    public void startClient()
    {
        Initialize();
        StartAsClient();
    }

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        Debug.Log("Server Found");
    }
}
