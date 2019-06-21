using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement_carte : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 initialPos;
    private Vector3 offset;
    private Vector3 curPosition;
    [SerializeField] GameObject[] targettable;
    [SerializeField] GameObject[] equipmentcards;
    private GameObject currenttarget;
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
        Debug.Log("down");
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        initialPos = gameObject.transform.position;
        offset = initialPos - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    private void OnMouseDrag()
    {
        Debug.Log("drag");
        if (!IsTheMousInTargetCollider())
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
        else if (equipmentcards.Length > 0)
        {
            CheckIfAlreadyCards();
        }
        else
        {
            gameObject.transform.position = currenttarget.transform.position;
        }
    }
    
    private bool IsTheMousInTargetCollider()
    {
        foreach (GameObject target in targettable)
        {
            float scale_x = target.GetComponent<BoxCollider2D>().bounds.extents.x;
            float scale_y = target.GetComponent<BoxCollider2D>().bounds.extents.y;

            if (Input.mousePosition.x > target.transform.position.x - scale_x &&
                Input.mousePosition.x < target.transform.position.x + scale_x &&
                Input.mousePosition.y < target.transform.position.y + scale_y &&
                Input.mousePosition.y > target.transform.position.y - scale_y)
            {
                currenttarget = target;
                return true;
            }
        }
        return false;
    }

    private void CheckIfAlreadyCards()
    {
        List<GameObject> cardstack = new List<GameObject>();
        foreach (GameObject equipment in equipmentcards)
        {
            if (equipment.transform.position == currenttarget.transform.position)
            {
                cardstack.Add(equipment);
            }
        }
        RelocateCards(cardstack);
    }

    private void RelocateCards(List<GameObject> stack)
    {
        int decalage = 0;
        foreach(GameObject card in stack)
        {
            Debug.Log(card);
            card.transform.position -= new Vector3(0.0f, currenttarget.GetComponent<BoxCollider2D>().bounds.extents.y, 0.0f);
            decalage++;
        }
        gameObject.transform.position = new Vector3(currenttarget.transform.position.x, currenttarget.transform.position.y + decalage* currenttarget.GetComponent<BoxCollider2D>().bounds.extents.y, currenttarget.transform.position.z);
    }
}
