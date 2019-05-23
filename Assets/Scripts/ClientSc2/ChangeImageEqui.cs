using System;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ChangeImageEqui : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    public bool[] selection;
    public Image image;
    public Main.Image[] equi;
    private int indice;
    private Main.Player p;

    // Start is called before the first frame update
    void Start()

    {
        equi = MainScript.equipements;
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
        image.sprite = equi[indice].Sprite;
        Debug.Log(image.sprite.ToString());
    }
}
