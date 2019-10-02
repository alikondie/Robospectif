using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

////script de la scène du menu, composé uniquement de bouton, qui détermine une partie des 
//// variables globales de la partie (langue, type) 
public class Menu : MonoBehaviour
{
    [SerializeField] Button standard;
    [SerializeField] Button urbain;
    [SerializeField] Button expert;

    [SerializeField] Button france;
    [SerializeField] Button english;


    void Start()
    {
        Partie.Initialize();
        Partie.Langue = "FR";
        standard.onClick.AddListener(() => StandardClicked());
        urbain.onClick.AddListener(() => UrbainClicked());
        expert.onClick.AddListener(() => ExpertClicked());
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

    private void ExpertClicked()
    {
        Partie.Type = "expert";
        SceneManager.LoadScene("expert_game_server");
    }
}
