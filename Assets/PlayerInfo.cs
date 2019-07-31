using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    #region Properties
    public Text nom;
    public Text prenom;
    public Text age;
    public Text spec;
    public TMP_Dropdown sex;
    public Text etab;

    #endregion

    #region Inputs
    #endregion

    #region Go or components
    #endregion

    #region Variables
    private StringBuilder data;
	#endregion

	#region Unity loop
    void Awake()
    {
        data.AppendLine("id;Nom;Prenom;Sex;Age;Specialite;Etablissement;Remarques");

    }

    void FixedUpdate()
    {
        
    }
	
    void Update()
    {
        
    }

	#endregion
	
	#region Methods
    void fillInformation()
    {
        string infos = JoueurStatic.Numero + ";" + nom + ";" + prenom + ";" + sex.value + ";" + age + ";" + spec + ";" + etab + ";";
        
    }
	#endregion
}
