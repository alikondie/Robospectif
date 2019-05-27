using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_Autonomie : MonoBehaviour
{
    // ---------- ATTRIBUTS ----------
    public int positionJoueur;

    public int[] positions;

    public GameObject volant;

    private bool autonomie; // ou  public int autonomie; (avec 0 ou 1)

    //Déplacement sourie
    public SpriteRenderer spriteRdr;
    private float positionSouris;
    private Vector3 positionVollant;

    //Pour la position du centre des Objets au debut
    private float positionDebutX;  
    private float positionDebutY;
    private float positionDebutZ = -1;
    private int ecart = 2;


    //Deplacement 
    private bool est_cliquer;
    private bool toucher = false;
    Vector3 mouseStartPos;
    Vector3 playerStartPos;


    // En fonction du sens
    private int SENS;
    private float[] postionX_Defaut = { 1, 3, -1, -3 };
    private float[] postionY_Defaut = { -3, 1, 3, -1 };

    private int orientation;
    private int[] tabOrien = { 0, 90, 180, 270 };


    // ---------- METHODES ----------

    // Methode d'inisialisation
    void Start()
    {
        // Position du joueur
        positionJoueur = Partie.JoueurCourant;
        
        positions = Text_Connexion.positions;

        int pos = -1;

        for (int i = 0; i < 6; i++)
        {
            if (positions[i] == positionJoueur)
            {
                pos = i + 1;
            }
        }

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

        positionDebutX = postionX_Defaut[SENS - 1];
        positionDebutY = postionY_Defaut[SENS - 1];
        orientation = tabOrien[SENS - 1];

        // Initialise position et orientation du Volant
        spriteRdr = volant.GetComponent<SpriteRenderer>();
        volant.transform.position = new Vector3(positionDebutX, positionDebutY, positionDebutZ);    //Position du Volant
        volant.transform.Rotate(0, 0, orientation);    // Rotation du Volant

        est_cliquer = false;

    }


    // Méthode de mise a jour
    private void Update()
    {
        // Si le vollant a dépasser sa limite:
        if (toucher) { 
            if(SENS == 1 || SENS == 3)
            {
                if (volant.transform.position.x > (positionDebutX + ecart))
                {
                    volant.transform.position = new Vector3((positionDebutX + ecart), positionDebutY, positionDebutZ);
                }
                if (volant.transform.position.x < (positionDebutX - ecart))
                {
                    volant.transform.position = new Vector3((positionDebutX - ecart), positionDebutY, positionDebutZ);
                }
            }
            else
            {
                if (volant.transform.position.y > (positionDebutY + ecart))
                {
                    volant.transform.position = new Vector3(positionDebutX, (positionDebutY + ecart), positionDebutZ);
                }
                if (volant.transform.position.y < (positionDebutY - ecart))
                {
                    volant.transform.position = new Vector3(positionDebutX, (positionDebutY - ecart), positionDebutZ);
                }
            }

        }

        // Déplacement Vollant
        if (Input.GetMouseButtonDown(0) && est_cliquer)
        {
            toucher = true;
            if (SENS == 1 || SENS == 3)
            {
                mouseStartPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, positionDebutY, positionDebutZ));
            }
            else
            {
                mouseStartPos = Camera.main.ScreenToWorldPoint(new Vector3(positionDebutX, Input.mousePosition.y, positionDebutZ));
            }               
            playerStartPos = volant.transform.position;
        }

        if (toucher && est_cliquer)
        {
            Vector3 mousePos;
            if (SENS == 1 || SENS == 3)
            {
                mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, positionDebutY, positionDebutZ));
            }
            else
            {
                mousePos = Camera.main.ScreenToWorldPoint(new Vector3(positionDebutX, Input.mousePosition.y, positionDebutZ));
            }

            Vector3 move = mousePos - mouseStartPos;
            positionVollant = playerStartPos + move;

            if (SENS == 1 || SENS == 3)
            {
                if (positionVollant.x <= (positionDebutX + ecart) && positionVollant.x >= (positionDebutX - ecart))
                {
                    volant.transform.position = positionVollant;
                }
            }
            else
            {
                if (positionVollant.y <= (positionDebutY + ecart) && positionVollant.y >= (positionDebutY - ecart))
                {
                    volant.transform.position = positionVollant;
                }
            }
                


        }

        // --
        if (Input.GetMouseButtonUp(0) && toucher)
        {
            if (SENS == 1 || SENS == 3)
            {
                if (volant.transform.position.x <= positionDebutX)
                {
                    volant.transform.position = new Vector3((positionDebutX - ecart), positionDebutY, positionDebutZ);
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
                    volant.transform.position = new Vector3((positionDebutX + ecart), positionDebutY, positionDebutZ);
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
                    volant.transform.position = new Vector3(positionDebutX, (positionDebutY - ecart), positionDebutZ);
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
                    volant.transform.position = new Vector3(positionDebutX, (positionDebutY + ecart), positionDebutZ);
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
    }

    void OnMouseDown()
    {
        est_cliquer = true;
    }
    private void OnMouseUp()
    {
        est_cliquer = false;
    }
}
