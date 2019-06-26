using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private int sens;
    private int checkifintarget;
    private int decalage;
    private int checkifnomoreintarget;
    private bool test;

    #region unused start and update
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        GetSens();
        gameObject.layer = 5;
        currenttarget = null;
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
        if (equipmentcards.Length > 0)
        {
            decalage = 0;
            if (checkifintarget == 1)
            {
                CheckIfAlreadyCards();
            }
            else if (checkifnomoreintarget == 0)
            {
                CheckIfAlreadyCards();
            }
        }
        else if (currenttarget != null)
        {
            gameObject.transform.position = currenttarget.transform.position;
            gameObject.layer = 2;
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
                checkifintarget++;
                checkifnomoreintarget = -1;
                currenttarget = target;
                return true;
            }
        }
        checkifnomoreintarget++;
        checkifintarget = 0;
        return false;
    }

    private void CheckIfAlreadyCards()
    {
        List<GameObject> cardstack = new List<GameObject>();
        foreach (GameObject equipment in equipmentcards)
        {
            if (sens == 1 || sens == 3) {
                test = equipment.transform.position.y >= currenttarget.transform.position.y - 2 * currenttarget.GetComponent<BoxCollider2D>().bounds.extents.y &&
                equipment.transform.position.y <= currenttarget.transform.position.y + 2 * currenttarget.GetComponent<BoxCollider2D>().bounds.extents.y &&
                Math.Round(equipment.transform.position.x) == Math.Round(currenttarget.transform.position.x);
            }
            else
            {
                test = equipment.transform.position.x >= currenttarget.transform.position.x - 2 * currenttarget.GetComponent<BoxCollider2D>().bounds.extents.x &&
                equipment.transform.position.x <= currenttarget.transform.position.x + 2 * currenttarget.GetComponent<BoxCollider2D>().bounds.extents.x &&
                Math.Round(equipment.transform.position.y) == Math.Round(currenttarget.transform.position.y);
            }

            if(test)
            {
                decalage++;
                cardstack.Add(equipment);
            }
            
        }
        
        if (checkifnomoreintarget == -1)
        {
            List<GameObject> allCardStack = new List<GameObject>();
            allCardStack.Add(this.gameObject);
            RelocateCardsWhencardincoming(cardstack);
            AssignEquipmentsTypes(currenttarget, allCardStack);
        }
        else
        {
            RelocateCardsWhencardleaves(cardstack);
            AssignEquipmentsTypes(currenttarget, cardstack);
        }
    }

    private void RelocateCardsWhencardincoming(List<GameObject> stack)
    {
        if(stack.Count != 0)
        {
            foreach (GameObject card in stack)
            {
                if (sens == 1)
                {
                    card.transform.position += new Vector3(0.0f, currenttarget.GetComponent<BoxCollider2D>().bounds.extents.y, 0.0f);
                    gameObject.transform.position = new Vector3(currenttarget.transform.position.x, currenttarget.transform.position.y - decalage * currenttarget.GetComponent<BoxCollider2D>().bounds.extents.y, currenttarget.transform.position.z);
                }
                else if (sens == 2)
                {
                    card.transform.position += new Vector3(currenttarget.GetComponent<BoxCollider2D>().bounds.extents.x, 0.0f, 0.0f);
                    gameObject.transform.position = new Vector3(currenttarget.transform.position.x - decalage * currenttarget.GetComponent<BoxCollider2D>().bounds.extents.x, currenttarget.transform.position.y, currenttarget.transform.position.z);
                }
                else if (sens == 3)
                {
                    card.transform.position -= new Vector3(0.0f, currenttarget.GetComponent<BoxCollider2D>().bounds.extents.y, 0.0f);
                    gameObject.transform.position = new Vector3(currenttarget.transform.position.x, currenttarget.transform.position.y + decalage * currenttarget.GetComponent<BoxCollider2D>().bounds.extents.y, currenttarget.transform.position.z);
                }
                else
                {
                    card.transform.position -= new Vector3(currenttarget.GetComponent<BoxCollider2D>().bounds.extents.x, 0.0f, 0.0f);
                    gameObject.transform.position = new Vector3(currenttarget.transform.position.x + decalage * currenttarget.GetComponent<BoxCollider2D>().bounds.extents.x, currenttarget.transform.position.y, currenttarget.transform.position.z);
                }
            }
        }
        else
        {
            gameObject.transform.position = currenttarget.transform.position;
        }
    }

    private void RelocateCardsWhencardleaves(List<GameObject> stack)
    {
        if (stack.Count != 0)
        {
            foreach (GameObject card in stack)
            {
                MoveCard(card);
            }
        }
    }

    private void MoveCard(GameObject card)
    {
        if (sens == 1 || sens == 3)
        {
            if (card.transform.position.y > currenttarget.transform.position.y)
            {
                card.transform.position -= new Vector3(0.0f, currenttarget.GetComponent<BoxCollider2D>().bounds.extents.y, 0.0f);
            }
            else if(card.transform.position.y < currenttarget.transform.position.y)
            {
                card.transform.position += new Vector3(0.0f, currenttarget.GetComponent<BoxCollider2D>().bounds.extents.y, 0.0f);
            }
            else if(gameObject.transform.position.y > currenttarget.transform.position.y)
            {
                card.transform.position += new Vector3(0.0f, currenttarget.GetComponent<BoxCollider2D>().bounds.extents.y, 0.0f);
            }
            else
            {
                card.transform.position -= new Vector3(0.0f, currenttarget.GetComponent<BoxCollider2D>().bounds.extents.y, 0.0f);
            }
        }
        else /*if (sens == 2)*/
        {
            if (card.transform.position.x > currenttarget.transform.position.x)
            {
                card.transform.position -= new Vector3(currenttarget.GetComponent<BoxCollider2D>().bounds.extents.x, 0.0f, 0.0f);
            }
            else if (card.transform.position.x < currenttarget.transform.position.x)
            {
                card.transform.position += new Vector3(currenttarget.GetComponent<BoxCollider2D>().bounds.extents.x, 0.0f, 0.0f);
            }
            else if (gameObject.transform.position.x > currenttarget.transform.position.x)
            {
                card.transform.position += new Vector3(currenttarget.GetComponent<BoxCollider2D>().bounds.extents.x, 0.0f, 0.0f);
            }
            else
            {
                card.transform.position -= new Vector3(currenttarget.GetComponent<BoxCollider2D>().bounds.extents.x, 0.0f, 0.0f);
            }
        }
        //else if (sens == 3)
        //{
        //    if()
        //    card.transform.position += new Vector3(0.0f, currenttarget.GetComponent<BoxCollider2D>().bounds.extents.y, 0.0f);
        //}
        //else
        //{
        //    card.transform.position += new Vector3(currenttarget.GetComponent<BoxCollider2D>().bounds.extents.x, 0.0f, 0.0f);
        //}
    }

    private void GetSens()
    {
        int pos = Array.IndexOf(Partie.Positions, Partie.JoueurCourant) + 1;
        switch (pos)
        {
            case 1:
                sens = 1;
                break;
            case 2:
                sens = 1;
                break;
            case 3:
                sens = 2;
                break;
            case 4:
                sens = 3;
                break;
            case 5:
                sens = 3;
                break;
            case 6:
                sens = 4;
                break;
        }
    }

    private void AssignEquipmentsTypes(GameObject target, List<GameObject> equipments)
    {
        List<string> equipmentStrings = new List<string>();
        foreach(GameObject e in equipments)
        {
            equipmentStrings.Add(e.GetComponent<Image>().sprite.name);
        }
       switch (target.name)
       {
            case ("boxcollider equipment manual"):
                Initialisation.manualEquipmentCards = equipmentStrings;
                
                break;
            case ("boxcollider equipment programmable"):
                Initialisation.programmableEquipmentCards = equipmentStrings;
                
                break;
            case ("boxcollider equipment automatique"):
                Initialisation.autoEquipmentCards = equipmentStrings;
                
                break;
        }
    }
}
