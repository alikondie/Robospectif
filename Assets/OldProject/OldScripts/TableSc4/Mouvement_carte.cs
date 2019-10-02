using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//// script attaché aux cartes, permet de gérer tout le déplacement des cartes.
public class Mouvement_carte : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 initialPos;
    private Vector3 offset;
    private Vector3 curPosition;
    [SerializeField] GameObject[] targettable;
    [SerializeField] GameObject[] equipmentcards;
    [SerializeField] GameObject pres_terminee;
    private GameObject currenttarget; //indique la cible avec laquelle la carte entre en collision
    private int sens;
    private int checkifintarget;
    private int decalage; //décalage lorsque plusieurs cartes équipement sont placées au même endroit
    private int checkifnomoreintarget;
    private bool was_in_target = false; //pour savoir si la carte entre dans la position cible, ou en ressort
    private bool test;
    private int nbCartePosees;

    void OnEnable()
    {
        was_in_target = false;
        GetSens();
        gameObject.layer = 5;
        checkifintarget = 0;
        checkifnomoreintarget = 0;
        currenttarget = null;
    }

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
            ////fonction de déplacement basique des cartes
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }

        ////si la carte est un équipement
        if (targettable.Length >= 2)
        {
            decalage = 0;
            if (checkifintarget == 1)
            {
                ////si la carte entre dans une cible
                CheckIfAlreadyCards();
            }
            else if (checkifnomoreintarget == 0)
            {
                ////si la carte sort d'une cible
                CheckIfAlreadyCards();
            }
        }
        else if (currenttarget != null)
        {
            ////si la carte entre en collision avec la cible et que toutes les autres conditions n'ont pas 
            //// été remplie on place la carte à l'emplacement
            if(!was_in_target)
            {
                Tour.NbCartesPosees++;
                was_in_target = true;
            }
            gameObject.transform.position = currenttarget.transform.position;
            gameObject.layer = 2;
        }
    }
    
    ////fonction pour savoir si la carte entre en collision avec une cible
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

    ////vérifie s'il y a déjà des cartes à l'emplacement (dans le cas des cartes équipements)
    ////et met à jour le décalage en fonction.
    private void CheckIfAlreadyCards()
    {
        List<GameObject> cardstack = new List<GameObject>();
        foreach (GameObject equipment in equipmentcards)
        {
            Debug.Log("currenttarget = " + currenttarget);
            Debug.Log("equipment = " + equipment);
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

    ////méthode qui bougent les cartes déjà posées à l'emplacement (dans le cas des équipements)
    ////si on pose une carte équipement sur un emplacementpossédant déjà au moins 1 carte.
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

    ////même objectif que la méthode précédente, mais lorsque qu'un équipement est retiré
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

    ////positionnement basique des cartes (autres que équipements)lors de la collision avec un emplacement
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
        else 
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
    }

    ////récupère le sens, en fonction de la position du joueur qui présente
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
                case ("boxcollider attention"):
                    Initialisation.autonomie = "Attention Requise";
                    break;
                case ("boxcollider autonomie"):
                    Initialisation.autonomie = "Autonome";
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
                break;
                
            case ("boxcollider equipment programmable"):
                if(Initialisation.programmableEquipmentCards == null)
                    Initialisation.programmableEquipmentCards = equipmentStrings;
                else
                    Initialisation.programmableEquipmentCards.AddRange(equipmentStrings);

                break;
            case ("boxcollider equipment automatique"):
                if (Initialisation.autoEquipmentCards == null)
                    Initialisation.autoEquipmentCards = equipmentStrings;

                else
                    Initialisation.autoEquipmentCards.AddRange(equipmentStrings);

                break;
            case ("boxcollider attention"):
                Initialisation.autonomie = "Attention Requise";
                break;
            case ("boxcollider autonomie"):
                Initialisation.autonomie = "Autonome";
                break;

        }
     /*   int m = Initialisation.manualEquipmentCards == null ? 0 : Initialisation.manualEquipmentCards.Count;
        int p = Initialisation.programmableEquipmentCards == null ? 0 : Initialisation.programmableEquipmentCards.Count;
        int a = Initialisation.autoEquipmentCards == null ? 0 : Initialisation.autoEquipmentCards.Count;
        print("manualEquipmentCards: " + m);
        print("programmableEquipmentCards: " + p);
        print("autoEquipmentCards: " + a);*/
    }
}
