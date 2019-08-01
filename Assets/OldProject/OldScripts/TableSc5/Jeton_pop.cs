using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Jeton_pop : MonoBehaviour
{
    [SerializeField] GameObject button;

    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 curScreenPoint;
    private Vector3 positionInit;

    private List<GameObject> joueurs;
    private List<GameObject> cartes;

    short goID = 1013;

    // Start is called before the first frame update
    void Start()
    {
        Transform canva = this.transform.parent.parent.parent;
        joueurs = new List<GameObject>();
        cartes = new List<GameObject>();
        for (int i = 3; i <= 8; i++)
        {
            /*for (int j = 0; j < canva.GetChild(i).GetChild(2).childCount; j++)
            {
                canva.GetChild(i).GetChild(2).GetChild(j).gameObject.SetActive(false);
            }*/

            joueurs.Add(canva.GetChild(i).gameObject);
            cartes.Add(canva.GetChild(i).GetChild(0).gameObject);


        }
        positionInit = this.transform.position;

        int index = transform.parent.transform.parent.GetSiblingIndex();
        print("index is " + index);
        GameObject jeton = this.gameObject;
        print("Jeton is " + jeton);
        joueurs[index].transform.parent.GetComponent<InitDebat>().AddGivenJeton(index, jeton);
    }

    void OnEnable()
    {
        button.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        Sprite self = this.GetComponent<Image>().sprite;
        Debug.Log(collision.gameObject.name);
        Sprite s = collision.gameObject.GetComponent<Image>().sprite;
        if ( (collision.gameObject.transform != this.transform.parent.parent.GetChild(0)) && (cartes.Contains(collision.gameObject)) && (s.name != "DecideurPublic") && (s.name != "DecideurPrive"))
        {
            int index = cartes.IndexOf(collision.gameObject);
            bool exp = true;
            if (self.name == "Jeton Vert")
                exp = Tour.Piles[index] < 3;
            else if (self.name == "Jeton Rouge")
                exp = Tour.Piles[index] == 3;
            if (exp)
            {
                this.gameObject.SetActive(false);
                MyNetworkMessage msg = new MyNetworkMessage();
                msg.message = Partie.JoueurCourant;
                NetworkServer.SendToAll(goID, msg);
                button.SetActive(true);

                if (Tour.Piles[index] < 8)
                {
                    Debug.Log(Tour.Piles[index]);
                    joueurs[index].transform.GetChild(2).GetChild(Tour.Piles[index]).gameObject.GetComponent<Image>().sprite = this.gameObject.GetComponent<Image>().sprite;
                    joueurs[index].transform.GetChild(2).GetChild(Tour.Piles[index]).gameObject.SetActive(true);
                    // recup donnée envoyer le jeton à partir de la fct AddGivenJeton


                    Tour.Piles[index]++;
                }
                
            }
        }
        this.transform.position = positionInit;

    }

    private void OnMouseDown()
    {
        Vector3 position = Vector3.zero;
        if (Input.touchCount > 0)
        {
            position = new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, 0);
        }
        else
        {
            position = Input.mousePosition;
        }
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        offset = transform.position - Camera.main.ScreenToWorldPoint(
            new Vector3(position.x, position.y, screenPoint.z));
    }

    private void OnMouseDrag()
    {
        // calcul la nouvelle position
        Vector3 position = Vector3.zero;
        if (Input.touchCount > 0)
        {
            position = new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, 0);
        }
        else
        {
            position = Input.mousePosition;
        }
        curScreenPoint = new Vector3(position.x, position.y, screenPoint.z);
        transform.position = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        
    }
}
