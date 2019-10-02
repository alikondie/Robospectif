using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



////Script du canvas de fin de partie. Il indique seulement au joueur que la partie est terminée
public class Fin : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Text central;

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
}
