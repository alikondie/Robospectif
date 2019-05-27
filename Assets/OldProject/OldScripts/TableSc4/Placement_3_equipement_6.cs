using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement_3_equipement_6 : MonoBehaviour
{
    public GameObject equipement;

    public static bool carte_3_position_1;
    private float x_position_1_carte_1;
    private float y_position_1_carte_1;
    private float x_position_1_carte_2;
    private float y_position_1_carte_2;
    private float x_position_1_carte_3;
    private float y_position_1_carte_3;

    public static bool carte_3_position_2;
    private float x_position_2_carte_1;
    private float y_position_2_carte_1;
    private float x_position_2_carte_2;
    private float y_position_2_carte_2;
    private float x_position_2_carte_3;
    private float y_position_2_carte_3;

    public static bool carte_3_position_3;
    private float x_position_3_carte_1;
    private float y_position_3_carte_1;
    private float x_position_3_carte_2;
    private float y_position_3_carte_2;
    private float x_position_3_carte_3;
    private float y_position_3_carte_3;

    // Start is called before the first frame update
    void Start()
    {
        carte_3_position_1 = false;
        x_position_1_carte_1 = 1.01f;
        y_position_1_carte_1 = 2.4f;
        x_position_1_carte_2 = 0.75f;
        y_position_1_carte_2 = 2.4f;
        x_position_1_carte_3 = 0.49f;
        y_position_1_carte_3 = 2.4f;

        carte_3_position_2 = false;
        x_position_2_carte_1 = 1.01f;
        y_position_2_carte_1 = -0.16f;
        x_position_2_carte_2 = 0.75f;
        y_position_2_carte_2 = -0.16f;
        x_position_2_carte_3 = 0.49f;
        y_position_2_carte_3 = -0.16f;

        carte_3_position_3 = false;
        x_position_3_carte_1 = 1.01f;
        y_position_3_carte_1 = -2.61f;
        x_position_3_carte_2 = 0.75f;
        y_position_3_carte_2 = -2.61f;
        x_position_3_carte_3 = 0.49f;
        y_position_3_carte_3 = -2.61f;
    }

    // Update is called once per frame
    void Update()
    {
        if (x_position_1_carte_1 - 1 < equipement.transform.position.x &&
            equipement.transform.position.x < x_position_1_carte_1 + 1 &&
            equipement.transform.position.x != x_position_1_carte_1 &&
            equipement.transform.position.x != x_position_1_carte_2 &&
            equipement.transform.position.x != x_position_1_carte_3)
        {
            if (y_position_1_carte_1 - 1 < equipement.transform.position.y &&
                equipement.transform.position.y < y_position_1_carte_1 + 1 &&
                equipement.transform.position.y != y_position_1_carte_1 &&
                equipement.transform.position.y != y_position_1_carte_2 &&
                equipement.transform.position.y != y_position_1_carte_3)
            {
                switch (Nb_carte_position_6.nb_carte_position_1)
                {
                    case 1:
                        equipement.transform.position = new Vector3(x_position_1_carte_1, y_position_1_carte_1, -1);
                        break;
                    case 2:
                        equipement.transform.position = new Vector3(x_position_1_carte_2, y_position_1_carte_2, -2);
                        break;
                    case 3:
                        equipement.transform.position = new Vector3(x_position_1_carte_3, y_position_1_carte_3, -3);
                        break;
                }
                carte_3_position_1 = true;
                carte_3_position_2 = false;
                carte_3_position_3 = false;
            }
        }



        if (x_position_2_carte_1 - 1 < equipement.transform.position.x &&
            equipement.transform.position.x < x_position_2_carte_1 + 1 &&
            equipement.transform.position.x != x_position_2_carte_1 &&
            equipement.transform.position.x != x_position_2_carte_2 &&
            equipement.transform.position.x != x_position_2_carte_3)
        {
            if (y_position_2_carte_1 - 1 < equipement.transform.position.y &&
                equipement.transform.position.y < y_position_2_carte_1 + 1 &&
                equipement.transform.position.y != y_position_2_carte_1 &&
                equipement.transform.position.y != y_position_2_carte_2 &&
                equipement.transform.position.y != y_position_2_carte_3)
            {

                switch (Nb_carte_position_6.nb_carte_position_2)
                {
                    case 1:
                        equipement.transform.position = new Vector3(x_position_2_carte_1, y_position_2_carte_1, -1);

                        break;
                    case 2:
                        equipement.transform.position = new Vector3(x_position_2_carte_2, y_position_2_carte_2, -2);
                        break;
                    case 3:
                        equipement.transform.position = new Vector3(x_position_2_carte_3, y_position_2_carte_3, -3);
                        break;
                }
                carte_3_position_1 = false;
                carte_3_position_2 = true;
                carte_3_position_3 = false;
            }
        }

        if (x_position_3_carte_1 - 1 < equipement.transform.position.x &&
            equipement.transform.position.x < x_position_3_carte_1 + 1 &&
            equipement.transform.position.x != x_position_3_carte_1 &&
            equipement.transform.position.x != x_position_3_carte_2 &&
            equipement.transform.position.x != x_position_3_carte_3)
        {
            if (y_position_3_carte_1 - 1 < equipement.transform.position.y &&
                equipement.transform.position.y < y_position_3_carte_1 + 1 &&
                equipement.transform.position.y != y_position_3_carte_1 &&
                equipement.transform.position.y != y_position_3_carte_2 &&
                equipement.transform.position.y != y_position_3_carte_3)
            {

                switch (Nb_carte_position_6.nb_carte_position_3)
                {
                    case 1:
                        equipement.transform.position = new Vector3(x_position_3_carte_1, y_position_3_carte_1, -1);
                        break;
                    case 2:
                        equipement.transform.position = new Vector3(x_position_3_carte_2, y_position_3_carte_2, -2);
                        break;
                    case 3:
                        equipement.transform.position = new Vector3(x_position_3_carte_3, y_position_3_carte_3, -3);
                        break;
                }
                carte_3_position_1 = false;
                carte_3_position_2 = false;
                carte_3_position_3 = true;
            }
        }

    }
}