using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitreConnextion : MonoBehaviour
{
    // ---------- ATRIBUES ----------
    public GameObject titreScene;


    private int estConnecter;
    // ---------- METHODES ----------

    // Methode d'inisialisation
    void Start()
    {
        Text textTitre = titreScene.GetComponent<Text>();
        textTitre.color = Color.white; //Couleur de tout le texte 
        textTitre.text = "Connectez-vous à " +  "\n" + " la table ! ";
    }

    // Méthode de Mise A Jour
    void Update()
    {
        estConnecter = PlayerPrefs.GetInt("TousConnecter");

        //Boucle IF qui change le texte si tout les joueurs sont connectés
        if (estConnecter == 1)
        {
            Text textTitre = titreScene.GetComponent<Text>();
            textTitre.color = Color.white; //Couleur de tout le texte 
            textTitre.text = "Tous les joueurs sont " + "\n" + " connectés à la table ! ";
        }
    }

    // Méthode de clik sourie pour faire touner le Titre
    private void OnMouseUp()
    {
        transform.Rotate(Vector3.back, -90);
    }

}
