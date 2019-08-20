using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

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
    short startID = 1025;
    string type;
    #endregion

    #region Unity loop
    void Awake()
    {
        //data.AppendLine("id;Nom;Prenom;Sex;Age;Specialite;Etablissement;Remarques");
        JoueurStatic.Client.RegisterHandler(startID, OnStartReceived);

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
        if (type == "expert")
        {
            SceneManager.LoadScene("expert_game_client");
        }
        else
        {
            joueur_infos.SetActive(false);
            joueur_choix_cartes.SetActive(true);
        }

    }

    private void OnStartReceived(NetworkMessage netMsg)
    {
        string type = netMsg.ReadMessage<MyStringMessage>().s;
    }
    #endregion
}
