using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement_dimension_6 : MonoBehaviour
{
    public GameObject dimension;
    private float x_position_dimension;
    private float y_position_dimension;

    // Start is called before the first frame update
    void Start()
    {
        x_position_dimension = -2.92f;
        y_position_dimension = 3.19f;

    }

    // Update is called once per frame
    void Update()
    {


        if (x_position_dimension - 1 < dimension.transform.position.x && dimension.transform.position.x < x_position_dimension + 1)
        {
            if (y_position_dimension - 1 < dimension.transform.position.y && dimension.transform.position.y < y_position_dimension + 1)
            {
                dimension.transform.position = new Vector3(x_position_dimension, y_position_dimension, dimension.transform.position.z);
            }
        }

    }
}
