using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_Autonomie_clean : MonoBehaviour
{
    #region attributs
    [SerializeField] GameObject volant;
    [SerializeField] Image Attention;
    [SerializeField] Image Autonomie;

    //Déplacement souris
    [SerializeField] Image sprite;

    //Pour la position du centre des Objets au debut
    private float positionDebutX;  
    private float positionDebutY;
    private float positionDebutZ;
    private int epsilon = 275;

    private int position;

    private int tailleTxtMin = 15;
    private int tailleTxtMax = 25;
    private Vector2 tailleCadreMin = new Vector2(25, 15);
    private Vector2 tailleCadreMax = new Vector2(30, 20);


    //Deplacement 
    private bool isClicked;
    private bool toucher = false;
    Vector3 mouseStartPos;
    Vector3 playerStartPos;


    // En fonction du sens
    private int SENS;

    private int orientation;
    private int[] tabOrien = { 0, 90, 180, 270 };


    #endregion

    #region main functions
    void Start()
    {
        position = 0;
        // Position du joueur
        int pos = Array.IndexOf(Partie.Positions, Partie.JoueurCourant) + 1;
        // Definie l'orientation et la postion de la partie Conduit
        // En fonction de la position du joueur
        switch (pos)
        {
            case 1:
                SENS = 1;
                break;
            case 2:
                SENS = 1;
                break;
            case 3:
                SENS = 2;
                break;
            case 4:
                SENS = 3;
                break;
            case 5:
                SENS = 3;
                break;
            case 6:
                SENS = 4;
                break;
        }
        positionDebutX = volant.transform.position.x;
        positionDebutY = volant.transform.position.y;
        positionDebutZ = volant.transform.position.z;
        orientation = tabOrien[SENS - 1];
        // Initialise position et orientation du Volant
        sprite = volant.GetComponent<Image>();

        isClicked = false;

    }


    // Méthode de mise a jour
    private void Update()
    {
        #region partie chelou
        if (Input.GetMouseButtonUp(0) && toucher)
        {
            if (SENS == 1 || SENS == 3)
            {
                if (volant.transform.position.x <= positionDebutX)
                {
                    volant.transform.position = new Vector3((positionDebutX - epsilon), positionDebutY, positionDebutZ);
                    if (SENS == 1)
                    {
                        Script_Conduite.dimentionTexteConduite(0);
                    }
                    else
                    {
                        Script_Conduite.dimentionTexteConduite(1);
                    }
                        
                }
                else
                {
                    volant.transform.position = new Vector3((positionDebutX + epsilon), positionDebutY, positionDebutZ);
                    if (SENS == 1)
                    {
                        Script_Conduite.dimentionTexteConduite(1);
                    }
                    else
                    {
                        Script_Conduite.dimentionTexteConduite(0);
                    }
                }
            }
            else
            {
                if (volant.transform.position.y <= positionDebutY)
                {
                    volant.transform.position = new Vector3(positionDebutX, (positionDebutY - epsilon), positionDebutZ);
                    if (SENS == 2)
                    {
                        Script_Conduite.dimentionTexteConduite(0);
                    }
                    else
                    {
                        Script_Conduite.dimentionTexteConduite(1);
                    }

                }
                else
                {
                    volant.transform.position = new Vector3(positionDebutX, (positionDebutY + epsilon), positionDebutZ);
                    if (SENS == 2)
                    {
                        Script_Conduite.dimentionTexteConduite(1);
                    }
                    else
                    {
                        Script_Conduite.dimentionTexteConduite(0);
                    }
                }
            }
        }
        #endregion

        if(isClicked)
        {
            Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, volant.transform.position.z);
            SteeringWheelMotion(newPosition);
        }
    }
    #endregion

    #region helping and check functions

    void OnMouseDown()
    {
        isClicked = true;
    }
    private void OnMouseUp()
    {
        isClicked = false;
        if (SENS == 1 || SENS == 3)
        {
            if (Autonomie.transform.position.x - Input.mousePosition.x > Input.mousePosition.x - Attention.transform.position.x)
                position = 1;
            else if (Autonomie.transform.position.x - Input.mousePosition.x < Input.mousePosition.x - Attention.transform.position.x)
                position = 2;
        }
        else
        {
            if (Autonomie.transform.position.y - Input.mousePosition.y > Input.mousePosition.y - Attention.transform.position.y)
                position = 1;
            else if (Autonomie.transform.position.y - Input.mousePosition.y < Input.mousePosition.y - Attention.transform.position.y)
                position = 2;
        } 

        switch (position)
        {
            case 1:
                Attention.rectTransform.sizeDelta = tailleCadreMax;
                Attention.transform.GetChild(0).GetComponent<Text>().fontSize = tailleTxtMax;
                Autonomie.rectTransform.sizeDelta = tailleCadreMin;
                Autonomie.transform.GetChild(0).GetComponent<Text>().fontSize = tailleTxtMin;
                if (SENS == 1 || SENS == 3)
                    volant.transform.position = new Vector3(positionDebutX - epsilon, volant.transform.position.y);
                else
                    volant.transform.position = new Vector3(volant.transform.position.x, positionDebutY - epsilon);
                break;
            case 2:
                Attention.rectTransform.sizeDelta = tailleCadreMin;
                Attention.transform.GetChild(0).GetComponent<Text>().fontSize = tailleTxtMin;
                Autonomie.rectTransform.sizeDelta = tailleCadreMax;
                Autonomie.transform.GetChild(0).GetComponent<Text>().fontSize = tailleTxtMax;
                if (SENS == 1 || SENS == 3)
                    volant.transform.position = new Vector3(positionDebutX + epsilon, volant.transform.position.y);
                else
                    volant.transform.position = new Vector3(volant.transform.position.x, positionDebutY + epsilon);
                break;
        }
    }

    private void SteeringWheelMotion(Vector3 newPosition)
    {
        if (IsSteeringWheelNotAtBorder())
        {
            if (SENS == 1 || SENS == 3)
            {
                newPosition.y = volant.transform.position.y;
                volant.transform.position = newPosition;
            }
            else
            {
                newPosition.x = volant.transform.position.x;
                volant.transform.position = newPosition;
            }
        }
    }

    private bool IsSteeringWheelNotAtBorder()
    {
        return ((SENS == 1 || SENS == 3) && Input.mousePosition.x <= (positionDebutX + epsilon) && Input.mousePosition.x >= (positionDebutX - epsilon)) ||
               ((SENS == 2 || SENS == 4) && Input.mousePosition.y <= (positionDebutY + epsilon) && Input.mousePosition.y >= (positionDebutY - epsilon));
    }


    #endregion
}
