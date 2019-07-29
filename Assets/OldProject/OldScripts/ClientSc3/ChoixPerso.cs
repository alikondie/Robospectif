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
    [SerializeField] Image[] ticks;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() => ButtonClicked());
    }

    void OnEnable()
    {
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
        canvas_choix_persos.SetActive(false);
        canvas_pres_perso.SetActive(true);
    }
}
