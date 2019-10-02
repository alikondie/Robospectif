using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

////class de message pour communication client/serveur, donc uniquement des attributs.
public class MyJetonMessage : MessageBase
{
    public string sprite;
    public int joueur;
}
