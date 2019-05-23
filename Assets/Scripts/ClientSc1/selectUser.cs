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
    private static int isActive;
    private int i;
    public int position;
    public static int zone;
    public Text text;
    public Scene scene;
    private Main.Global g;
    public Main.Player player;
    public static NetworkClient client = Init.client;
    public Scene sceneSuivante;
    short messageID = 1000;
    short positionsID = 1005;

    // public Text NumJoueur;

    void Start()
    {
        button.gameObject.SetActive(false);
        button.onClick.AddListener(() => ButtonClicked(i));
    }

    void Update()
    {
        switch (position)
        {
            case 1:
                positionEffective = Init.position1;
                break;
            case 2:
                positionEffective = Init.position2;
                break;
            case 3:
                positionEffective = Init.position3;
                break;
            case 4:
                positionEffective = Init.position4;
                break;
            case 5:
                positionEffective = Init.position5;
                break;
            case 6:
                positionEffective = Init.position6;
                break;
        }
    }

    /*public static void OnPositionsReceived(NetworkMessage netMsg)
    {
        var message = netMsg.ReadMessage<MyPositionsMessage>();
        switch (position)
        {
            case 1:
                isActive = message.position1;
                break;
            case 2:
                isActive = message.position2;
                break;
            case 3:
                isActive = message.position3;
                break;
            case 4:
                isActive = message.position4;
                break;
            case 5:
                isActive = message.position5;
                break;
            case 6:
                isActive = message.position6;
                break;
        }
        Debug.Log("isActive : " + isActive);
        if (isActive != 0)
        {
            button.gameObject.SetActive(true);
            text.text = isActive.ToString();
            positionEffective = isActive;
        }
    }*/

    void ButtonClicked(int i)
    {
        Debug.Log("bouton cliqué");
        Debug.Log(" i " + i);
        //Output this to console when the Button3 is clicked
        Main.Global.Player = new Main.Player(i);
        Debug.Log(" ID " + Main.Global.Player.Id);
        positionStatic = positionEffective;
        Debug.Log("position : " + position);
        zone = position;
        Debug.Log("zone : " + zone);
        MyNetworkMessage message = new MyNetworkMessage();
        message.message = positionEffective;
        client.Send(messageID, message);
        PlayerPrefs.SetInt("idplayer", positionEffective);
        //StartCoroutine(selectPlayer(i));
        SceneManager.LoadScene("scene2");
        Debug.Log(i);
    }

    IEnumerator selectPlayer(int i)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", i);
        WWW www = new WWW("https://primsie-spears.000webhostapp.com/estSelect.php", form);
        yield return www;
    }

    private void requette()
    {
        button.onClick.AddListener(() => ButtonClicked(i));
    }
}

