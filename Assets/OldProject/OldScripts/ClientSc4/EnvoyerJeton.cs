using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

////scritp utilisé pour indiquer au serveur qu'un jeton est déposé dans la scène qui correspond, avec 
//// le jeton  envoyé (sprite) et le joueur qui l'envoit
public class EnvoyerJeton : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Button button;
    short jeton = 1010;

    void Start()
    {
        button.onClick.AddListener(() => envoyer());
    }

    public void envoyer()
    {
        MyJetonMessage msg = new MyJetonMessage();
        msg.joueur = JoueurStatic.Numero;
        msg.sprite = image.sprite.name;
        JoueurStatic.Client.Send(jeton, msg);
    }
}
