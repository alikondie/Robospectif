using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

////class de message pour communication client/serveur, donc uniquement des attributs.
public class MyPositionsMessage : MessageBase
{
    public int position1;
    public int position2;
    public int position3;
    public int position4;
    public int position5;
    public int position6;
    public string langue;
    public string type;
}
