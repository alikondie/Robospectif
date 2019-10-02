using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

////script attaché au personnages (standard)et aux acteurs (expert) qui permet d'afficher une marque
//// sur la carte sélectionnée
public class ChoixPerso : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject canvas_choix_persos;
    [SerializeField] GameObject canvas_pres_perso;
    [SerializeField] Image image;
    [SerializeField] Image tickCurrent;
    [SerializeField] Button button;
    [SerializeField] Image[] ticks;

    void Start()
    {
        button.onClick.AddListener(() => ButtonClicked());
    }

    ////Lors de l'activation du canvas, le joueur ne peut pas valider sans avoir sélectionné de carte
    void OnEnable()
    {
        button.gameObject.SetActive(false);
    }

    ////En cliquant sur la carte, on affiche la marque de sélection sur cette carte, et on active 
    ////le bouton pour valider
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        tickCurrent.gameObject.SetActive(true);
        button.gameObject.SetActive(true);
        for (int i = 0; i < ticks.Length; i++)
        {
            ticks[i].gameObject.SetActive(false);
        }
    }

    ////méthode d'appuie sur le bouton valider
    private void ButtonClicked()
    {
        if (tickCurrent.gameObject.activeSelf)
        {
            ////On stocke le personnage/acteur choisi pour l'utiliser sur le serveur 
            if (JoueurStatic.Type == "expert")
            {
                JoueurStatic.Actif = image.sprite;
            }
            else
            {
                for (int i = 0; i < JoueurStatic.Persos.Length; i++)
                {
                    if (JoueurStatic.Persos[i] == image.sprite)
                        JoueurStatic.PersosChoisis[i] = true;
                }
                JoueurStatic.Actif = image.sprite;
            }
        }
        ////On passe au canvas suivant
        canvas_choix_persos.SetActive(false);
        canvas_pres_perso.SetActive(true);
    }
}
