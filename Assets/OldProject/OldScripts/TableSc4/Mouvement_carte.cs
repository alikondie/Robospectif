using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement_carte : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 initialPos;
    private Vector3 offset;
    private Vector3 curPosition;
    [SerializeField] GameObject target;
    private bool is_static;

    #region unused start and update
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    private void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        initialPos = gameObject.transform.position;
        offset = initialPos - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    private void OnMouseDrag()
    {
        if (!IsTheMousInTargetCollider())
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
        else
        {
            gameObject.transform.position = target.transform.position;
        }
    }
    
    //private void OnCollisionEnter2D(Collision2D collision/*, GameObject selected_card, GameObject type_card_location*/)
    //{
    //    if (gameObject.tag == collision.gameObject.tag)
    //    {
    //        is_static = true;
    //        gameObject.transform.position = collision.gameObject.transform.position;
    //    }
    //}

    private bool IsTheMousInTargetCollider()
    {
        float scale_x = target.GetComponent<BoxCollider2D>().bounds.extents.x;
        float scale_y = target.GetComponent<BoxCollider2D>().bounds.extents.y;
        return Input.mousePosition.x > target.transform.position.x - scale_x &&
               Input.mousePosition.x < target.transform.position.x + scale_x &&
               Input.mousePosition.y < target.transform.position.y + scale_y &&
               Input.mousePosition.y < target.transform.position.y + scale_y;
    }
}
