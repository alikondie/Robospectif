using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fin : MonoBehaviour
{
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "Joueur : " + selectUser.positionStatic;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
