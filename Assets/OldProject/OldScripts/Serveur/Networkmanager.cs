using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class Networkmanager : NetworkManager
{
    [SerializeField] GameObject canvas_serveur;
    [SerializeField] GameObject canvas_client;
    [SerializeField] NetworkManager manager;
    [SerializeField] NetworkDiscovery Netserver;

    private NetworkClient myclient;
    short messageID = 1000;
    short imageID = 1001;
    short conceptionID = 1002;
    short chronoID = 1003;
    private string Ip_serveur;

    void Start()
    {
        //Debug.Log("niniufniueu" + System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable());
        //Debug.Log("niunefiurbvyubuybv " + System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()));
        //var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
        //foreach (var ip in host.AddressList)
        //{
        //    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
        //    {
        //        Debug.Log("ip : " + ip.ToString());
        //    }
        //}
        Debug.Log("ip server : " + Ip_serveur);
        string ipv4 = IPManager.GetIP(IPManager.ADDRESSFAM.IPv4); // On met l'adresse IP de l'appareil courant dans ipv4
        if (Netserver.isServer)
        {
            Ip_serveur = ipv4;
            Partie.Initialize();
            manager.StartServer(); // Connection Serveur
            RegisterHandlers();
            Debug.Log("Serveur connecté");
        }
        else
        {
            manager.StartClient(); // Connection Smartphone
            Debug.Log("client");
            myclient = new NetworkClient();
            myclient.Connect(Ip_serveur, 7777);
            JoueurStatic.Client = myclient;
            canvas_serveur.SetActive(false);
            canvas_client.SetActive(true);
        }
    }


    private void RegisterHandlers()
    {
        NetworkServer.RegisterHandler(MsgType.Connect, OnClientConnected);
        NetworkServer.RegisterHandler(messageID, OnMessageReceived);
    }

    private void onTestReceived(NetworkMessage netMsg)
    {
        var v = netMsg.ReadMessage<MyImageMessage>();
    }

    private void onImageReceived(NetworkMessage netMsg)
    {
        var objectMessage = netMsg.ReadMessage<MyImageMessage>();

    }

    // connexion d'un joueur sur un des boutons du client
    void OnMessageReceived(NetworkMessage message)
    {

    }




    public void OnCommandSent(NetworkMessage netMsg)
    {
        Debug.Log(netMsg);
    }


    ////////////////////       Partie Network     ///////////////////////////////////////


    public override void OnServerConnect(NetworkConnection conn)
    {
        NetworkServer.RegisterHandler(MsgType.Connect, OnClientConnected);
    }


    void OnClientConnected(NetworkMessage message)
    {
        Debug.Log("Client connecté");
    }


    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {

        var player = (GameObject)GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);


        Debug.Log("Client has requested to get his player added to the game");

    }











    public override void OnServerDisconnect(NetworkConnection conn)
    {

        NetworkServer.DestroyPlayersForConnection(conn);

        if (conn.lastError != NetworkError.Ok)
        {

            if (LogFilter.logError) { Debug.LogError("ServerDisconnected due to error: " + conn.lastError); }

        }

        Debug.Log("A client disconnected from the server: " + conn);

    }

    public override void OnServerReady(NetworkConnection conn)
    {

        NetworkServer.SetClientReady(conn);

        Debug.Log("Client is set to the ready state (ready to receive state updates): " + conn);

    }




    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        Debug.Log("connexion établie");
    }

    public override void OnServerRemovePlayer(NetworkConnection conn, PlayerController player)
    {

        if (player.gameObject != null)

            NetworkServer.Destroy(player.gameObject);

    }

    public override void OnServerError(NetworkConnection conn, int errorCode)
    {

        Debug.Log("Server network error occurred: " + (NetworkError)errorCode);

    }

    public override void OnStartHost()
    {

        Debug.Log("Host has started");

    }

    public override void OnStartServer()
    {

        Debug.Log("Server has started");

    }

    public override void OnStopServer()
    {

        Debug.Log("Server has stopped");

    }

    public override void OnStopHost()
    {

        Debug.Log("Host has stopped");

    }

    // Client callbacks

    public override void OnClientConnect(NetworkConnection conn)

    {

        base.OnClientConnect(conn);

        Debug.Log("Connected successfully to server, now to set up other stuff for the client...");

    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {

        StopClient();

        if (conn.lastError != NetworkError.Ok)

        {

            if (LogFilter.logError) { Debug.LogError("ClientDisconnected due to error: " + conn.lastError); }

        }

        Debug.Log("Client disconnected from server: " + conn);

    }

    public override void OnClientError(NetworkConnection conn, int errorCode)
    {

        Debug.Log("Client network error occurred: " + (NetworkError)errorCode);

    }

    public override void OnClientNotReady(NetworkConnection conn)
    {

        Debug.Log("Server has set client to be not-ready (stop getting state updates)");

    }

    public override void OnStartClient(NetworkClient client)
    {

        Debug.Log("Client has started");

    }

    public override void OnStopClient()
    {

        Debug.Log("Client has stopped");

    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {

        base.OnClientSceneChanged(conn);

        Debug.Log("Server triggered scene change and we've done the same, do any extra work here for the client...");

    }








    //#region test
    //private int server_port = 5000;
    //private string server_ip;

    //// multicast
    //private int startup_port = 5100;
    //private IPAddress group_address = IPAddress.Parse("127.0.0.1");
    //private UdpClient udp_client;
    //private IPEndPoint remote_end;


    //void Start()
    //{
    //    // loaded elsewhere
    //    //if (Loader.IsPC)
    //    //    StartGameServer();
    //    //else
    //        StartGameClient();
    //}

    //void StartGameServer()
    //{
    //    // the Unity3d way to become a server
    //    //NetworkConnectionError init_status = Network.InitializeServer(10, server_port, false);
    //    //Debug.Log("status: " + init_status);

    //    //StartCoroutine(StartBroadcast());
    //}

    //void StartGameClient()
    //{
    //    // multicast receive setup
    //    remote_end = new IPEndPoint(IPAddress.Any, startup_port);
    //    udp_client = new UdpClient(remote_end);
    //    udp_client.JoinMulticastGroup(group_address);

    //    // async callback for multicast
    //    udp_client.BeginReceive(new AsyncCallback(ServerLookup), null);

    //    StartCoroutine(MakeConnection());
    //}

    //IEnumerator MakeConnection()
    //{
    //    // continues after we get server's address
    //    while (string.IsNullOrEmpty(server_ip))
    //        yield return null;

    //    while (Network.peerType == NetworkPeerType.Disconnected)
    //    {
    //        Debug.Log("connecting: " + server_ip + ":" + server_port);

    //        // the Unity3d way to connect to a server
    //        NetworkConnectionError error;
    //        error = Network.Connect(server_ip, server_port);
    //        Debug.Log("status: " + error);
    //        yield return new WaitForSeconds(1);
    //    }
    //}

    ///******* broadcast functions *******/
    //void ServerLookup(IAsyncResult ar)
    //{
    //    // receivers package and identifies IP
    //    var receiveBytes = udp_client.EndReceive(ar, ref remote_end);

    //    server_ip = remote_end.Address.ToString();
    //    Debug.Log("Server: " + server_ip);
    //}

    //IEnumerator StartBroadcast()
    //{
    //    // multicast send setup
    //    udp_client = new UdpClient();
    //    udp_client.JoinMulticastGroup(group_address);
    //    remote_end = new IPEndPoint(group_address, startup_port);

    //    // sends multicast
    //    while (true)
    //    {
    //        var buffer = Encoding.ASCII.GetBytes("GameServer");
    //        udp_client.Send(buffer, buffer.Length, remote_end);

    //        yield return new WaitForSeconds(1);
    //    }
    //}
    //#endregion
}
