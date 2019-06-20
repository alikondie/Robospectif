using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitPerso : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Image[] personnagesGO;
    [SerializeField] Image[] ticks;
    private int nbJoueurs = Init.nbJoueurs;



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

        text.text = "Joueur : " + JoueurStatic.Numero;

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
