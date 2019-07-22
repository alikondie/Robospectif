using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class JetonExpert : MonoBehaviour
{
    short jeton = 1010;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => OnButtonClicked());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnButtonClicked()
    {
        MyJetonMessage msg = new MyJetonMessage();
        string s = this.GetComponent<Image>().sprite.ToString();
        msg.sprite = s.Substring(0, s.Length - 21);
        msg.joueur = JoueurStatic.Numero;
        JoueurStatic.Client.Send(jeton, msg);
        this.gameObject.SetActive(false);
    }
}
