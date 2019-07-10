using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mouvement_carte_expert : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 initialPos;
    private Vector3 offset;
    private Vector3 curPosition;
    [SerializeField] GameObject[] targettable;
    [SerializeField] GameObject[] equipmentcards;
    [SerializeField] GameObject[] dimensioncard;
    [SerializeField] GameObject[] locomotioncard;
    [SerializeField] GameObject pres_terminee;
    private GameObject currenttarget;
    private int checkifintarget;
    private int decalage;
    private int checkifnomoreintarget;
    private bool was_in_target = false;
    private bool test;

    private int nbCartePosees;
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
        if (!IsTheMousInTargetCollider() || CheckIfOtherCardIsInTarget())
        {
            currenttarget = null;
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }

        if (targettable.Length >= 3)
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
            if(!was_in_target)
            {
                Tour.NbCartesPosees++;
                was_in_target = true;
            }
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
            test = equipment.transform.position.y >= currenttarget.transform.position.y - 2 * currenttarget.GetComponent<BoxCollider2D>().bounds.extents.y &&
                   equipment.transform.position.y <= currenttarget.transform.position.y + 2 * currenttarget.GetComponent<BoxCollider2D>().bounds.extents.y &&
                   Math.Round(equipment.transform.position.x) == Math.Round(currenttarget.transform.position.x);

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
            if (!was_in_target)
            {
                was_in_target = true;
                Tour.NbCartesPosees++;
            }
            RelocateCardsWhencardincoming(cardstack);
            AssignEquipmentsTypes(currenttarget, allCardStack,true);
        }
        else
        {
            was_in_target = false;
            Tour.NbCartesPosees--;
            RelocateCardsWhencardleaves(cardstack);
            AssignEquipmentsTypes(currenttarget, cardstack,false);
        }
    }

    private void RelocateCardsWhencardincoming(List<GameObject> stack)
    {
        if(stack.Count != 0)
        {
            foreach (GameObject card in stack)
            {
                card.transform.position += new Vector3(0.0f, currenttarget.GetComponent<BoxCollider2D>().bounds.extents.y, 0.0f);
                gameObject.transform.position = new Vector3(currenttarget.transform.position.x, currenttarget.transform.position.y - decalage * currenttarget.GetComponent<BoxCollider2D>().bounds.extents.y, currenttarget.transform.position.z);
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

    private bool CheckIfOtherCardIsInTarget()
    {
        bool retour = false;
        if((dimensioncard.Length == 0 && locomotioncard.Length == 0) || currenttarget == null)
        {
            retour = true;
        }
        else if (dimensioncard.Length != 0)
        {
            foreach (GameObject carte in dimensioncard)
            {
                if(carte.transform.position == currenttarget.transform.position)
                {
                    retour = true;
                    break;
                }
            }
        }
        else if (locomotioncard.Length != 0)
        {
            foreach (GameObject carte in locomotioncard)
            {
                if(carte.transform.position == currenttarget.transform.position)
                {
                    retour = true;
                    break;
                }
            }
        }
        return retour;
    }

    #region data
    private void AssignEquipmentsTypes(GameObject target, List<GameObject> equipments,bool isIncoming)
    {
        List<string> equipmentStrings = new List<string>();
        foreach (GameObject e in equipments)
        {
            equipmentStrings.Add(e.GetComponent<Image>().sprite.name);
        }
        if (!isIncoming) { 

           switch (target.name)
           {
                case ("boxcollider equipment manual"):
                    Initialisation.manualEquipmentCards = equipmentStrings;
                    foreach (string s in Initialisation.manualEquipmentCards)
                        print("man "+s);
                    break;
                case ("boxcollider equipment programmable"):
                    Initialisation.programmableEquipmentCards = equipmentStrings;
                    foreach (string s in Initialisation.programmableEquipmentCards)
                        print("prog "+s);
                    break;
                case ("boxcollider equipment automatique"):
                    Initialisation.autoEquipmentCards = equipmentStrings;
                    foreach (string s in Initialisation.autoEquipmentCards)
                        print("auto "+s);

                    break;
            }
    /*          int me = Initialisation.manualEquipmentCards == null ? 0 : Initialisation.manualEquipmentCards.Count;
              int pe = Initialisation.programmableEquipmentCards == null ? 0 : Initialisation.programmableEquipmentCards.Count;
              int ae = Initialisation.autoEquipmentCards == null ? 0 : Initialisation.autoEquipmentCards.Count;
              print("manualEquipmentCards: " + me);
              print("programmableEquipmentCards: " + pe);
              print("autoEquipmentCards: " + ae);*/
            return;
        }

        switch (target.name)
        {
            case ("boxcollider equipment manual"):
                if (Initialisation.manualEquipmentCards == null)
                    Initialisation.manualEquipmentCards = equipmentStrings;

                else
                    Initialisation.manualEquipmentCards.AddRange(equipmentStrings);

                //debug
                foreach (string s in Initialisation.manualEquipmentCards)
                    print("man " + s);
                break;
                
            case ("boxcollider equipment programmable"):
                if(Initialisation.programmableEquipmentCards == null)
                    Initialisation.programmableEquipmentCards = equipmentStrings;

                else
                    Initialisation.programmableEquipmentCards.AddRange(equipmentStrings);

                foreach (string s in Initialisation.programmableEquipmentCards)
                    print("prog " + s);
                break;
            case ("boxcollider equipment automatique"):
                if (Initialisation.autoEquipmentCards == null)
                    Initialisation.autoEquipmentCards = equipmentStrings;

                else
                    Initialisation.autoEquipmentCards.AddRange(equipmentStrings);


                foreach (string s in Initialisation.autoEquipmentCards)
                    print("auto " + s);
                break;
                
        }
     /*   int m = Initialisation.manualEquipmentCards == null ? 0 : Initialisation.manualEquipmentCards.Count;
        int p = Initialisation.programmableEquipmentCards == null ? 0 : Initialisation.programmableEquipmentCards.Count;
        int a = Initialisation.autoEquipmentCards == null ? 0 : Initialisation.autoEquipmentCards.Count;
        print("manualEquipmentCards: " + m);
        print("programmableEquipmentCards: " + p);
        print("autoEquipmentCards: " + a);*/
    }
    #endregion
}
