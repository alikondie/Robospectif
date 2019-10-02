using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

////script attaché au canvas de choix des persos (côté client)
public class InitPerso : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Text button;
    [SerializeField] Image[] personnagesGO;
    [SerializeField] Image[] ticks;

    ////à l'activation du canvas, on initialise toutes les éléments d'UI (cartes, textes en fonction de la langue)
    void OnEnable()
    {
        for (int i = 0; i < ticks.Length; i++)
        {
            ticks[i].gameObject.SetActive(false);
        }

        if (JoueurStatic.Langue == "FR")
        {
            text.text = "Joueur " + JoueurStatic.Numero;
            button.text = "Valider";
        }
        else
        {
            text.text = "Player " + JoueurStatic.Numero;
            button.text = "Confirm";
        }

        if (JoueurStatic.Type == "expert")
        {
            for (int i = 0; i < JoueurStatic.Acteurs.Length; i++)
            {
                personnagesGO[i].sprite = JoueurStatic.Acteurs[i];
            }
        } else
        {
            for (int i = 0; i < JoueurStatic.Persos.Length; i++)
            {
                personnagesGO[i].sprite = JoueurStatic.Persos[i];
            }

            for (int i = 0; i < JoueurStatic.PersosChoisis.Length; i++)
            {
                if (JoueurStatic.PersosChoisis[i])
                    personnagesGO[i].gameObject.SetActive(false);
            }
        }        
    }
}
