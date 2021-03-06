﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerInfo : MonoBehaviour
{
    #region Properties
    public Text nom;
    public Text prenom;
    public Text age;
    public Text spec;
    public Text etab;

    public GameObject joueur_infos;
    public GameObject joueur_choix_cartes;

    #endregion

    #region Inputs
    #endregion

    #region Go or components
    #endregion

    #region Variables
    short playerInfoId = 1050;
    
	#endregion

	#region Unity loop
    void Awake()
    {
        //data.AppendLine("id;Nom;Prenom;Sex;Age;Specialite;Etablissement;Remarques");

    }

    void FixedUpdate()
    {
        
    }
	
    void Update()
    {
        
    }

	#endregion
	
	#region Methods
    public void FillInformation()
    {
        PlayerInfoMessage infos = new PlayerInfoMessage();

        infos.id = JoueurStatic.Numero;
        infos.lastName = nom.text;
        infos.firstName = prenom.text;
        infos.age = age.text;
        infos.specialty = spec.text;
        infos.establishment = etab.text;


        JoueurStatic.Client.Send(playerInfoId, infos);
        joueur_infos.SetActive(false);
        joueur_choix_cartes.SetActive(true);

}
	#endregion
}
