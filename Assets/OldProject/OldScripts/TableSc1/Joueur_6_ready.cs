using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Joueur_6_ready : MonoBehaviour
{
    public SpriteRenderer rend;
    public static bool joueur_6 = false;
    public Sprite Main_beige_6, Main_verte_6;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        Main_beige_6 = Resources.Load<Sprite>("Main_beige_6");
        Main_verte_6 = Resources.Load<Sprite>("Main_verte_6");
    }

    void OnMouseDown()
    {

        if (this.gameObject.name == "Main_beige_6")
        {
            // Debug.Log("clic sur " + this.gameObject.name);
            if (joueur_6 == false)
            {
                rend.sprite = Main_verte_6;
                joueur_6 = true;
            }
            else if (joueur_6 == true)
            {
                rend.sprite = Main_beige_6;
                joueur_6 = false;
            }
        }

    }
}