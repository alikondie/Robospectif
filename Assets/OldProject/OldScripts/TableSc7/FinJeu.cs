using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinJeu : MonoBehaviour
{
    [SerializeField] GameObject[] zones;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                zones[i].transform.GetChild(j).gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < Partie.Joueurs.Count; i++)
        {
            zones[Partie.Joueurs[i].Position].transform.GetChild(0).GetComponent<Text>().text = "Joueur " + Partie.Joueurs[i].Numero;
            zones[Partie.Joueurs[i].Position].transform.GetChild(0).gameObject.SetActive(true);
            for (int j = 0; j < Partie.Joueurs[i].NbCouronnes; j++)
            {
                zones[Partie.Joueurs[i].Position + 1].transform.GetChild(j + 1).gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
