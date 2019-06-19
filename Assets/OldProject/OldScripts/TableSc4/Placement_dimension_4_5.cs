using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement_dimension_4_5 : MonoBehaviour
{
    [SerializeField] GameObject dimension_target;
    [SerializeField] GameObject locomotion_target;
    [SerializeField] GameObject manual_equipment_target;
    [SerializeField] GameObject programmable_equipment_target;
    [SerializeField] GameObject automatic_equipment_target;
    private float x_position_dimension;
    private float y_position_dimension;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision/*, GameObject selected_card, GameObject type_card_location*/)
    {
        Debug.Log("dimension carte : " + gameObject.tag);
        Debug.Log("dimension location : " + collision.gameObject.tag);

        if(gameObject.tag == collision.gameObject.tag)
        {
            Debug.Log("ca passssssse");
            gameObject.transform.position = collision.gameObject.transform.position;
        }
    }
}
