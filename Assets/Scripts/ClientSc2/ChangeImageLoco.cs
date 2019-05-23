using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ChangeImageLoco : MonoBehaviour, IPointerClickHandler
{

    public Image image;
    public Main.Image[] loco;
    private int indice;
    private Main.Player p;

    // Start is called before the first frame update
    void Start()

    {
        loco = MainScript.locomotions;
        indice = 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        indice = (indice + 1) % 2;
        image.sprite = loco[indice].Sprite;
    }
}