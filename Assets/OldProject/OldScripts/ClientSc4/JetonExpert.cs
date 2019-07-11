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
        msg.sprite = this.name.Substring(0, 1);
        msg.joueur = JoueurStatic.Numero;
        JoueurStatic.Client.Send(jeton, msg);
        this.gameObject.SetActive(false);
    }
}
