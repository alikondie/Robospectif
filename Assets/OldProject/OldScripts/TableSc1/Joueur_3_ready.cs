using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur_3_ready : MonoBehaviour
{
    public SpriteRenderer rend;
    public static bool joueur_3 = false;
    public Sprite Main_beige_3, Main_verte_3;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        Main_beige_3 = Resources.Load<Sprite>("Main_beige_3");
        Main_verte_3 = Resources.Load<Sprite>("Main_verte_3");
    }

    void OnMouseDown()
    {

        if (this.gameObject.name == "Main_beige_3")
        {
            // Debug.Log("clic sur " + this.gameObject.name);
            if (joueur_3 == false)
            {
                rend.sprite = Main_verte_3;
                joueur_3 = true;
            }
            else if (joueur_3 == true)
            {
                rend.sprite = Main_beige_3;
                joueur_3 = false;
            }
        }

    }
}
