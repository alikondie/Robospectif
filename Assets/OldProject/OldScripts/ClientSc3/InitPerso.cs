using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitPerso : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Text button;
    [SerializeField] Image[] personnagesGO;
    [SerializeField] Image[] ticks;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

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
