using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_Autonomie_clean : MonoBehaviour
{
    #region atributs
    [SerializeField] GameObject volant;

    //Déplacement sourie
    [SerializeField] SpriteRenderer spriteRdr;

    //Pour la position du centre des Objets au debut
    private float positionDebutX;  
    private float positionDebutY;
    private float positionDebutZ;
    private int epsilon = 275;


    //Deplacement 
    private bool isClicked;
    private bool toucher = false;
    Vector3 mouseStartPos;
    Vector3 playerStartPos;


    // En fonction du sens
    private int SENS;
    private float[] postionX_Defaut = { 1, 3, -1, -3 };
    private float[] postionY_Defaut = { -3, 1, 3, -1 };

    private int orientation;
    private int[] tabOrien = { 0, 90, 180, 270 };


    #endregion

    #region main functions
    void Start()
    {
        // Position du joueur

        //int pos = Array.IndexOf(Partie.Positions, Partie.JoueurCourant) + 1;
        int pos = 1;
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
        Debug.Log(volant.transform.position);
        // Initialise position et orientation du Volant
        spriteRdr = volant.GetComponent<SpriteRenderer>();
        volant.transform.position = new Vector3(positionDebutX, positionDebutY, positionDebutZ);    //Position du Volant
        volant.transform.Rotate(0, 0, orientation);    // Rotation du Volant

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
