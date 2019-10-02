using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

////class de message pour communication client/serveur, donc uniquement des attributs.
public class MyActeurMessage : MessageBase
{
    public int numero;
    public string acteur1;
    public string acteur2;
    public string acteur3;
}
