using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

////class de message pour communication client/serveur, donc uniquement des attributs.
public class MyImageMessage : MessageBase
{
    public string dim;
    public string loco;
    public string equi1;
    public string equi2;
    public string equi3;
    public int num;
    public int zone;
}
