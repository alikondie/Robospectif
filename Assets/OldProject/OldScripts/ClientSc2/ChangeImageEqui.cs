using System;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

////Script permettant de changer la position des cartes lorsque les joueurs appuie sur une carte lors du canvas de sélection de carte
public class ChangeImageEqui : MonoBehaviour, IPointerClickHandler
{
    private bool[] selection;
    [SerializeField] Image image;
    private int indice;

    void Start()
    {
        selection = Equipement.selection;
        if (image.name == "ImageEquipement1")
        {
            indice = 0;
        }
        else if (image.name == "ImageEquipement2")
        {
            indice = 1;
        }
        else if (image.name == "ImageEquipement3")
        {
            indice = 2;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        indice = Equipement.TraiteIndice(indice);
        image.sprite = JoueurStatic.Equipements[indice];
    }
}
