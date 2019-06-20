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
    private Mouvement_carte cardmove;

    // Start is called before the first frame update
    void Start()
    {
        cardmove = gameObject.GetComponent<Mouvement_carte>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseOver()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision/*, GameObject selected_card, GameObject type_card_location*/)
    {
        if(gameObject.tag == collision.gameObject.tag)
        {
            gameObject.transform.position = collision.gameObject.transform.position;
            Debug.Log("position carte : " + gameObject.transform.position);
            Debug.Log("position target : " + collision.gameObject.transform.position);
        }
    }
}
