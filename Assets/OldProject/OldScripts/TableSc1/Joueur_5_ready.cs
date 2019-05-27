using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur_5_ready : MonoBehaviour
{
    public SpriteRenderer rend;
    public static bool joueur_5 = false;
    public Sprite Main_beige_5, Main_verte_5;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        Main_beige_5 = Resources.Load<Sprite>("Main_beige_5");
        Main_verte_5 = Resources.Load<Sprite>("Main_verte_5");
    }

    void OnMouseDown()
    {

        if (this.gameObject.name == "Main_beige_5")
        {
            // Debug.Log("clic sur " + this.gameObject.name);
            if (joueur_5 == false)
            {
                rend.sprite = Main_verte_5;
                joueur_5 = true;
            }
            else if (joueur_5 == true)
            {
                rend.sprite = Main_beige_5;
                joueur_5 = false;
            }
        }

    }
}