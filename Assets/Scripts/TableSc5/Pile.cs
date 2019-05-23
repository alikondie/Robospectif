using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pile : MonoBehaviour
{
    public static int pile_joueur1;
    public static int pile_joueur2;
    public static int pile_joueur3;
    public static int pile_joueur4;
    public static int pile_joueur5;
    public static int pile_joueur6;

    public static int[] piles;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < this.transform.GetChildCount(); i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
        piles = new int[6];

        for (int y = 0; y<5; y++)
        {
            piles[y] = 0;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
