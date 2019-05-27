using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1_jeton1 : MonoBehaviour
{
    public int[] jetonTab ;
    public GameObject Jeton;
    
    public SpriteRenderer rend;
    public static bool joueur_4 = false;
    public Sprite jeton_societe_positif, jeton_societe_negatif;
    public Sprite jeton_usage_positif, jeton_usage_negatif;
    public Sprite jeton_planete_positif, jeton_planete_negatif;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        Jeton.SetActive(false);
        jeton_societe_positif = Resources.Load<Sprite>("jeton_societe_positif");
        jeton_societe_negatif = Resources.Load<Sprite>("jeton_societe_negatif");

        jeton_usage_positif = Resources.Load<Sprite>("jeton_usage_positif");
        jeton_usage_negatif = Resources.Load<Sprite>("jeton_usage_negatif");

        jeton_planete_positif = Resources.Load<Sprite>("jeton_planete_positif");
        jeton_planete_negatif = Resources.Load<Sprite>("jeton_planete_negatif");


            int caseSwitch = jetonTab[0];
            switch (caseSwitch)
            {
                case 0:
                
                    break;
                case 1:
                    Jeton.SetActive(true);
                    rend.sprite = jeton_societe_positif;
                    break;
                case 2:
                    Jeton.SetActive(true);
                    rend.sprite = jeton_societe_negatif;
                    break;
                case 3:
                    Jeton.SetActive(true);
                    rend.sprite = jeton_usage_positif;
                    break;
                case 4:
                    Jeton.SetActive(true);
                    rend.sprite = jeton_usage_negatif;
                    break;
                case 5:
                    Jeton.SetActive(true);
                    rend.sprite = jeton_planete_positif;
                    break;
                case 6:
                    Jeton.SetActive(true);
                    rend.sprite = jeton_planete_negatif;
                    break;

            }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
