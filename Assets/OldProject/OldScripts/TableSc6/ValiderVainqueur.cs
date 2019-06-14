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

    [SerializeField] GameObject couronne1;
    [SerializeField] GameObject couronne2;
    [SerializeField] GameObject couronne3;
    [SerializeField] GameObject couronne4;
    [SerializeField] GameObject couronne5;
    [SerializeField] GameObject couronne6;

    [SerializeField] Button button;

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
        if (couronne1.activeSelf)
        {
            vainqueur = 1;
            button.gameObject.SetActive(true);
        }
        if (couronne2.activeSelf)
        {
            vainqueur = 2;
            button.gameObject.SetActive(true);
        }
        if (couronne3.activeSelf)
        {
            vainqueur = 3;
            button.gameObject.SetActive(true);
        }
        if (couronne4.activeSelf)
        {
            vainqueur = 4;
            button.gameObject.SetActive(true);
        }
        if (couronne5.activeSelf)
        {
            vainqueur = 5;
            button.gameObject.SetActive(true);
        }
        if (couronne6.activeSelf)
        {
            vainqueur = 6;
            button.gameObject.SetActive(true);
        }
    }

    private void ButtonClicked()
    {
        int nb = Partie.Joueurs.Count;
        Debug.Log("capacité joueurs : " + nb);
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
}
