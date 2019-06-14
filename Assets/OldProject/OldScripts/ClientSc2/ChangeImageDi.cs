﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ChangeImageDi : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image image;
    private int indice;

    // Start is called before the first frame update
    void Start()

    {
        indice = 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        indice = (indice + 1) % 2;
        image.sprite = JoueurStatic.Dimensions[indice];
    }
}
