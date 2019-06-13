using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ValiderVainqueur : MonoBehaviour
{
    [SerializeField] GameObject canvas_choix_vainqueur;
    [SerializeField] GameObject canvas_fin_tour;
    [SerializeField] GameObject canvas_fin;

    private int  vainqueur;

    [SerializeField] GameObject[] couronnes;
    [SerializeField] GameObject[] joueurs;

    [SerializeField] Button button;

    private Sprite[] images;

    // Start is called before the first frame update
    void Start()
    {
        button.gameObject.SetActive(false);
        vainqueur = 0 ;
        button.onClick.AddListener(() => ButtonClicked());
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < joueurs.Length; i++)
        {
            if (joueurs[i].transform.GetChild(2).gameObject.activeSelf)
            {
                vainqueur = i + 1;
                button.gameObject.SetActive(true);
            }
        }
    }

    private void ButtonClicked()
    {
        int nb = Partie.Joueurs.Count;
        for (int i = 0; i < nb; i++)
        {
            if (Partie.Joueurs[i].Position == vainqueur)
            {
                Partie.Joueurs[i].NbCouronnes++;
            }
        }
        canvas_choix_vainqueur.SetActive(false);
        if (nb != Partie.Tour)
        {
            Partie.Tour++;
            Partie.JoueurCourant++;
            if (Partie.JoueurCourant > Partie.Joueurs.Count)
            {
                Partie.JoueurCourant = 1;
            }
            canvas_fin_tour.SetActive(true);
            //SceneManager.LoadScene("Scene_fin_tour");
        }
        else canvas_fin.SetActive(true);
    }

    private void OnEnable()
    {
        foreach (GameObject joueur in joueurs)
        {
            joueur.SetActive(false);
        }

        for (int i = 0; i < joueurs.Length; i++)
        {
            if (Tour.PersosDebat[i] != null)
            {
                joueurs[i].transform.GetChild(0).GetComponent<Image>().sprite = Tour.PersosDebat[i];
                joueurs[i].SetActive(true);
            }
        }

        int pos = Array.IndexOf(Partie.Positions, Partie.JoueurCourant);
        Destroy(joueurs[pos - 1]);
    }
}
