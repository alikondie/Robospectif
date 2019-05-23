using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement_locomotion_3 : MonoBehaviour
{
    public GameObject locomotion;
    private float x_position_locomotion;
    private float y_position_locomotion;

    // Start is called before the first frame update
    void Start()
    {
        x_position_locomotion = -2.999f;
        y_position_locomotion = 3.01f;

    }

    // Update is called once per frame
    void Update()
    {


        if (x_position_locomotion - 1 < locomotion.transform.position.x && locomotion.transform.position.x < x_position_locomotion + 1)
        {
            if (y_position_locomotion - 1 < locomotion.transform.position.y && locomotion.transform.position.y < y_position_locomotion + 1)
            {
                locomotion.transform.position = new Vector3(x_position_locomotion, y_position_locomotion, locomotion.transform.position.z);
            }
        }

    }
}