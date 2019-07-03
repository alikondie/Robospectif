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
    [SerializeField] Button expert;

    [SerializeField] Button france;
    [SerializeField] Button english;


    // Start is called before the first frame update
    void Start()
    {
        Partie.Initialize();
        standard.onClick.AddListener(() => StandardClicked());
        urbain.onClick.AddListener(() => UrbainClicked());
        france.onClick.AddListener(() => FranceClicked());
        english.onClick.AddListener(() => EnglishClicked());
    }

    private void EnglishClicked()
    {
        Partie.Langue = "EN";
        standard.transform.GetChild(0).GetComponent<Text>().text = "Creative Mode";
        urbain.transform.GetChild(0).GetComponent<Text>().text = "Creative Mode";
        urbain.transform.GetChild(1).GetComponent<Text>().text = "Urban";
        expert.transform.GetChild(0).GetComponent<Text>().text = "Expert Mode";
    }

    private void FranceClicked()
    {
        Partie.Langue = "FR";
        standard.transform.GetChild(0).GetComponent<Text>().text = "Mode Créatif";
        urbain.transform.GetChild(0).GetComponent<Text>().text = "Mode Créatif";
        urbain.transform.GetChild(1).GetComponent<Text>().text = "Urbain";
        expert.transform.GetChild(0).GetComponent<Text>().text = "Mode Expert";
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
