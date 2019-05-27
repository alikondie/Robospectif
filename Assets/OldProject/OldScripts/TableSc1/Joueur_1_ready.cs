using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur_1_ready : MonoBehaviour
{
    public SpriteRenderer rend;
    public static bool joueur_1 = false;
    public Sprite Main_beige_1, Main_verte_1;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        Main_beige_1 = Resources.Load<Sprite>("Main_beige_1");
        Main_verte_1 = Resources.Load<Sprite>("Main_verte_1");
    }

    void OnMouseDown()
    {

        if (this.gameObject.name == "Main_beige_1")
        {
            // Debug.Log("clic sur " + this.gameObject.name);
            if (joueur_1 == false)
            {
                rend.sprite = Main_verte_1;
                joueur_1 = true;
            }
            else if (joueur_1 == true)
            {
                rend.sprite = Main_beige_1;
                joueur_1 = false;
            }
        }
    }
}
