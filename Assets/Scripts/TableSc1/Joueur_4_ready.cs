        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;

public class Joueur_4_ready : MonoBehaviour
    {
        public SpriteRenderer rend;
        public static bool joueur_4 = false;
        public Sprite Main_beige_4, Main_verte_4;

        // Start is called before the first frame update
        void Start()
        {
            rend = GetComponent<SpriteRenderer>();
            Main_beige_4 = Resources.Load<Sprite>("Main_beige_4");
            Main_verte_4 = Resources.Load<Sprite>("Main_verte_4");
        }

        void OnMouseDown()
        {

            if (this.gameObject.name == "Main_beige_4")
            {
                // Debug.Log("clic sur " + this.gameObject.name);
                if (joueur_4 == false)
                {
                    rend.sprite = Main_verte_4;
                    joueur_4 = true;
                }
                else if (joueur_4 == true)
                {
                    rend.sprite = Main_beige_4;
                    joueur_4 = false;
                }
            }

        }
    }