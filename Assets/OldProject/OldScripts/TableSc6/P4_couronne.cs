﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P4_couronne : MonoBehaviour
{
    public GameObject personnage4;
    public GameObject couronne;
    public static int couronne_active;

    // Start is called before the first frame update
    void Start()
    {
        couronne_active = 0;
        couronne.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (couronne_active == 0)
        {
            couronne.SetActive(false);
        }
    }

    void OnMouseDown()
    {
        if (this.gameObject == personnage4)
        {
            couronne.SetActive(true);
            couronne_active = 1;
        }
        if (P1_couronne.couronne_active == 1)
        {
            P1_couronne.couronne_active = 0;
        }
        if (P2_couronne.couronne_active == 1)
        {
            P2_couronne.couronne_active = 0;
        }
        if (P3_couronne.couronne_active == 1)
        {
            P3_couronne.couronne_active = 0;
        }
        if (P5_couronne.couronne_active == 1)
        {
            P5_couronne.couronne_active = 0;
        }
        if (P6_couronne.couronne_active == 1)
        {
            P6_couronne.couronne_active = 0;
        }
    }
}