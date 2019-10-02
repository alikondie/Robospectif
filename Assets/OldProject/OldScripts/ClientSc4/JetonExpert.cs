using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

////script attaché aux jetons pour indiquer que les jetons sont des boutons
public class JetonExpert : MonoBehaviour
{
    short jeton = 1010;

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => OnButtonClicked());
    }

    private void OnButtonClicked()
    {
        MyJetonMessage msg = new MyJetonMessage();
        msg.sprite = this.GetComponent<Image>().sprite.name;
        msg.joueur = JoueurStatic.Numero;
        JoueurStatic.Client.Send(jeton, msg);
        this.gameObject.SetActive(false);
    }
}
