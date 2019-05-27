using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinJeu : MonoBehaviour
{
    public GameObject zone1;
    public GameObject zone2;
    public GameObject zone3;
    public GameObject zone4;
    public GameObject zone5;
    public GameObject zone6;
    private GameObject[] zones;

    // Start is called before the first frame update
    void Start()
    {
        zones = new GameObject[6];
        zones[0] = zone1;
        zones[1] = zone2;
        zones[2] = zone3;
        zones[3] = zone4;
        zones[4] = zone5;
        zones[5] = zone6;
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                zones[i].transform.GetChild(j).gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < Partie.Joueurs.Count; i++)
        {
            zones[Partie.Joueurs[i].Position - 1].transform.GetChild(0).GetComponent<Text>().text = "Joueur " + Partie.Joueurs[i].Numero;
            zones[Partie.Joueurs[i].Position - 1].transform.GetChild(0).gameObject.SetActive(true);
            for (int j = 0; j < Partie.Joueurs[i].NbCouronnes; j++)
            {
                zones[Partie.Joueurs[i].Position - 1].transform.GetChild(j + 1).gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
