using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur_2_ready : MonoBehaviour
{
    public SpriteRenderer rend;
    public static bool joueur_2 = false;
    public Sprite Main_beige_2, Main_verte_2;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        Main_beige_2 = Resources.Load<Sprite>("Main_beige_2");
        Main_verte_2 = Resources.Load<Sprite>("Main_verte_2");
    }

    void OnMouseDown()
    {

        if (this.gameObject.name == "Main_beige_2")
        {
            // Debug.Log("clic sur " + this.gameObject.name);
            if (joueur_2 == false)
            {
                rend.sprite = Main_verte_2;
                joueur_2 = true;
            }
            else if (joueur_2 == true)
            {
                rend.sprite = Main_beige_2;
                joueur_2 = false;
            }
        }

    }
}
