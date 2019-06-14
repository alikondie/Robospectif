using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class EnvoyerJeton : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Button button;
    short jeton = 1010;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() => envoyer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void envoyer()
    {
        MyJetonMessage msg = new MyJetonMessage();
        msg.joueur = selectUser.positionStatic;
        string s = image.sprite.ToString();
        string msgs = "";
        for (int i = 0; i < s.Length - 21; i++)
        {
            msgs = msgs + s[i];
        }
        msg.sprite = msgs;
        JoueurStatic.Client.Send(jeton, msg);
    }
}
