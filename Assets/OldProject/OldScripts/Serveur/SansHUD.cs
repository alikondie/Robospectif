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
using System.IO;

class RegisterHostMessage : MessageBase { public float message; }

public class SansHUD : NetworkManager
{
    [SerializeField] GameObject canvas_serveur;
    [SerializeField] GameObject canvas_client;
    [SerializeField] NetworkManager manager;
    private NetworkClient myclient;
    short messageID = 1000;
    short imageID = 1001;
    short conceptionID = 1002;
    short chronoID = 1003;
    short playerInfoId = 1050;
    public static short clientID = 123;
    private bool conceptionTerminee;
    public static int premierFini;
    private string Ip_serveur = "172.21.232.220";  // IP Table 192.168.43.40    192.168.1.10  127.0.0.1
    public static string spriteString;
    // recolte de données main script
    public static StringBuilder data;

    void Start()
    {
        
        conceptionTerminee = false;
        string ipv4 = IPManager.GetIP(IPManager.ADDRESSFAM.IPv4); // On met l'adresse IP de l'appareil courant dans ipv4
        if(ipv4 == Ip_serveur) 
        {
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

        data = new StringBuilder();
    }


    private void RegisterHandlers()
    {
        NetworkServer.RegisterHandler(MsgType.Connect, OnClientConnected);
        NetworkServer.RegisterHandler(messageID, OnMessageReceived);
        NetworkServer.RegisterHandler(imageID, onImageReceived);
        NetworkServer.RegisterHandler(conceptionID, onConceptionReceived);
        NetworkServer.RegisterHandler(playerInfoId, OnPlayerInfoReceived);
        //NetworkServer.RegisterHandler(1005, onTestReceived);
    }

    private void onTestReceived(NetworkMessage netMsg)
    {
        var v = netMsg.ReadMessage<MyImageMessage>();
    }

    private void onConceptionReceived(NetworkMessage netMsg)
    {
        if (!conceptionTerminee)
        {
            premierFini = netMsg.ReadMessage<MyNetworkMessage>().message;

            Partie.JoueurCourant = premierFini;
            EnAttenteCT.premierFini(premierFini);
            envoiChrono(premierFini);
            conceptionTerminee = true;
        }
        ConceptionTerminer.finiMaConception();
    }

    private void envoiChrono(int premierFini)
    {
        MyNetworkMessage message = new MyNetworkMessage();
        message.message = premierFini;
        NetworkServer.SendToAll(chronoID, message);
    }

    private void onImageReceived(NetworkMessage netMsg)
    {
        var objectMessage = netMsg.ReadMessage<MyImageMessage>();
        Sprite dim = Resources.Load<Sprite>(Partie.Langue + "/Dimension/" + objectMessage.dim);
        Sprite loco = Resources.Load<Sprite>(Partie.Langue + "/Locomotion/" + objectMessage.loco);
        Sprite equi1 = Resources.Load<Sprite>(Partie.Langue + "/Equipements/" + objectMessage.equi1);
        Sprite equi2 = Resources.Load<Sprite>(Partie.Langue + "/Equipements/" + objectMessage.equi2);
        Sprite equi3 = Resources.Load<Sprite>(Partie.Langue + "/Equipements/" + objectMessage.equi3);
        int numero = objectMessage.num;
        int z = objectMessage.zone;

        Sprite[] images = new Sprite[5];
        images[0] = loco;
        images[1] = dim;
        images[2] = equi1;
        images[3] = equi2;
        images[4] = equi3;

        foreach (Joueur j in Partie.Joueurs)
        {
            if (j.Numero == numero)
            {
                j.Dim = dim;
                j.Loco = loco;
                j.Equi1 = equi1;
                j.Equi2 = equi2;
                j.Equi3 = equi3;
            }
        }

        Initialisation.get(images, z);
    }

    // connexion d'un joueur sur un des boutons du client
    void OnMessageReceived(NetworkMessage message)
    {
        int id = message.ReadMessage<MyNetworkMessage>().message;
        MyNetworkMessage msg = new MyNetworkMessage();
        msg.message = id;
        NetworkServer.SendToAll(messageID, msg);
        Text_Connexion.recupInfoJoueur(id);        
    }




    private void OnPlayerInfoReceived(NetworkMessage netMsg)
    {
        var objectMessage = netMsg.ReadMessage<PlayerInfoMessage>();
        PlayerInfoData personnage = new PlayerInfoData
        {
            Nom = objectMessage.lastName,
            Prenom = objectMessage.firstName,
            Sex = objectMessage.sex,
            Age = objectMessage.age,
            Specialite = objectMessage.specialty,
            Etablissement = objectMessage.establishment,
            Remarques = "",
        };
        string json = JsonUtility.ToJson(personnage);
        File.WriteAllText(Application.dataPath + "/joueur_"+personnage.Nom+".json", json);

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



}




