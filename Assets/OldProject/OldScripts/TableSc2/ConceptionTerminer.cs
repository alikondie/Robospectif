using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml.Linq;
using System.IO;
using System.Text;

public class ConceptionTerminer : MonoBehaviour
{
    [SerializeField] GameObject canvas_sablier;
    [SerializeField] GameObject canvas_plateau_vehicule;
    private int nbJoueur; //Nb Joueurs
    private static int nbJoueurConceptionTerminer; //Conteur du nombre de joueurs a avoir Terminer leur conception
    
    StringBuilder rejectedCardsContent;

    // ---------- METHODES ----------

    // Methode d'inisialisation
    void Start()
    {
        
        /*rejectedCardsContent = new StringBuilder();
        rejectedCardsContent.AppendLine("Joueur;Dimension;Locomotion;Equipement1;Equipement2;Equipement3");*/
        
        
        // Initialise le compteur
        nbJoueurConceptionTerminer = 1;

        // Recuperation du Nombre de joueur:
        nbJoueur = PlayerPrefs.GetInt("nombreJoueur");     // Nombre de Joueurs

    }

    // Update is called once per frame
    void Update()
    {
        if(nbJoueurConceptionTerminer == nbJoueur)
        {
            //for(int i)
            //SceneManager.LoadScene(nomSceneDemander);
           /* foreach(string line in EnAttenteCT.rejectedCards)
            {
                rejectedCardsContent.AppendLine(line);
            }
            string filePath = "C:\\Users\\taki.yamani\\Desktop\\rejected_cards.csv";
            File.AppendAllText(filePath, rejectedCardsContent.ToString());*/
            canvas_sablier.SetActive(false);
            canvas_plateau_vehicule.SetActive(true);
        }
    }

    // Méthode qui dit quand le joueur a une conception terminer (Android)
    public static void finiMaConception()
    {
        nbJoueurConceptionTerminer++;
    }
}
