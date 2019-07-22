using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ScriptTimer : MonoBehaviour
{
    // ---------- ATTRIBUETS ----------
    [SerializeField] Transform cercleMinuteur;
    [SerializeField] GameObject imgMinuteur;
    public NetworkClient client;
    public static bool doitLancer;
    public static bool done;

    private float debut;
    [SerializeField] private float vitesse;


    // ---------- METHODES ----------

    // Methode d'inisialisation
    void Start()
    {
        doitLancer = false;
        done = false;
        imgMinuteur.SetActive(false);
        debut = 0;
        cercleMinuteur.GetComponent<Image>().fillAmount = debut / 100;

    }

    // Méthode de Mise A Jour
    void Update()
    {

        if (doitLancer) {
            imgMinuteur.SetActive(true);
            debut = lancerMinuteur(debut, vitesse, cercleMinuteur);
            Debug.Log("debut = " + debut);
        }

        if (debut >= 100)
        {
            done = true;
        }
    }

        // Methode activation de minuteur
        public float lancerMinuteur(float nbDebut, float nbvitesse, Transform cercle)
    {
        if (nbDebut < 100)
        {
            nbDebut += nbvitesse * Time.deltaTime;
        }

        cercle.GetComponent<Image>().fillAmount = nbDebut / 100;

        return nbDebut;
    }

    internal static void debutChrono()
    {
        doitLancer = true;
    }
}
