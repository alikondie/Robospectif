using System;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ChangeImageEqui : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    private bool[] selection;
    [SerializeField] Image image;
    private int indice;

    // Start is called before the first frame update
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
