﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jeton_pop : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 curScreenPoint;
    private Vector3 positionInit;

    private List<GameObject> joueurs;
    private List<GameObject> cartes;    

    public static int[] nb_bonus;
    public static int[] nb_malus;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        Transform canva = this.transform.parent.parent.parent;
        joueurs = new List<GameObject>();
        cartes = new List<GameObject>();
        for (int i = 3; i < canva.childCount; i++)
        {
            joueurs.Add(canva.GetChild(i).gameObject);
            cartes.Add(canva.GetChild(i).GetChild(0).gameObject);
        }
        positionInit = transform.position;
        nb_bonus = new int[6];
        nb_malus = new int[6];
    }


    // Update is called once per frame
    void Update()
    {
        

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {

        if ( (collision.gameObject.transform != this.transform.parent.parent.GetChild(0)) && (cartes.Contains(collision.gameObject)) )
        {
            this.gameObject.SetActive(false);

            int index = cartes.IndexOf(collision.gameObject);
            
            if (Pile.piles[index] < 8)
            {
                joueurs[index].transform.GetChild(2).GetChild(Pile.piles[index]).gameObject.GetComponent<Image>().sprite = this.gameObject.GetComponent<Image>().sprite;
                joueurs[index].transform.GetChild(2).GetChild(Pile.piles[index]).gameObject.SetActive(true);
                Pile.piles[index]++;
            }
        }

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
