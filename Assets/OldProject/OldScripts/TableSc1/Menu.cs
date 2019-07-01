using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] Button standard;
    [SerializeField] Button urbain;


    // Start is called before the first frame update
    void Start()
    {
        Partie.Initialize();
        standard.onClick.AddListener(() => StandardClicked());
        urbain.onClick.AddListener(() => UrbainClicked());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StandardClicked()
    {
        Partie.Type = "standard";
        SceneManager.LoadScene("standard_game_server");
    }

    private void UrbainClicked()
    {
        Partie.Type = "urbain";
        SceneManager.LoadScene("standard_game_server");
    }
}
