using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

////class de message pour communication client/serveur, donc uniquement des attributs.
public class MyPersoMessage : MessageBase
{
    public int numero;
    public string image;
    public int choixZone0;
    public int choixZone1;
}
