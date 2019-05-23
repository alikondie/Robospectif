using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ChangeImageDi : MonoBehaviour, IPointerClickHandler
{
    public Image image;
    public Main.Image[] dim;
    private int indice;
    private Main.Player p;

    // Start is called before the first frame update
    void Start()

    {
        dim = MainScript.dimensions;
        indice = 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        indice = (indice + 1) % 2;
        image.sprite = dim[indice].Sprite;
    }
}
