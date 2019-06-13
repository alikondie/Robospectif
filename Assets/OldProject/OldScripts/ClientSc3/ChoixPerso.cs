using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoixPerso : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject canvas_choix_persos;
    [SerializeField] GameObject canvas_pres_perso;
    [SerializeField] Image image;
    [SerializeField] Image tickCurrent;
    [SerializeField] Button button;
    public static Sprite perso;
    [SerializeField] Image[] ticks;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() => ButtonClicked());
        button.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        tickCurrent.gameObject.SetActive(true);
        button.gameObject.SetActive(true);
        for (int i = 0; i < ticks.Length; i++)
        {
            ticks[i].gameObject.SetActive(false);
        }
    }
    private void ButtonClicked()
    {
        if (tickCurrent.gameObject.activeSelf)
        {
            Debug.Log(image.sprite);
            if (JoueurStatic.Perso1 == image.sprite)
                JoueurStatic.Perso1Choisi = true;

            if (JoueurStatic.Perso2 == image.sprite)
                JoueurStatic.Perso2Choisi = true;

            if (JoueurStatic.Perso3 == image.sprite)
                JoueurStatic.Perso3Choisi = true;

            if (JoueurStatic.Perso4 == image.sprite)
                JoueurStatic.Perso4Choisi = true;

            if (JoueurStatic.Perso5 == image.sprite)
                JoueurStatic.Perso5Choisi = true;

            if (JoueurStatic.Perso6 == image.sprite)
                JoueurStatic.Perso6Choisi = true;
            perso = image.sprite;
        }
        canvas_choix_persos.SetActive(false);
        canvas_pres_perso.SetActive(true);
        //SceneManager.LoadScene("scene4");
    }
}
