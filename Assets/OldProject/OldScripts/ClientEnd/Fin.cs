using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fin : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Text central;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {
        if (JoueurStatic.Langue == "FR")
        {
            text.text = "Joueur " + JoueurStatic.Numero;
            central.text = "Fin de la partie";
        } else
        {
            text.text = "Player " + JoueurStatic.Numero;
            central.text = "Game over";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
