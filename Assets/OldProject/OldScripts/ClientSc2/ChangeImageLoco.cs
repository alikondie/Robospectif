using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

////Script permettant de changer la position des cartes lorsque les joueurs appuie sur une carte lors du canvas de sélection de carte
public class ChangeImageLoco : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] Image image;
    private int indice;

    void Start()

    {
        indice = 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        indice = (indice + 1) % 2;
        image.sprite = JoueurStatic.Locomotions[indice];
    }
}