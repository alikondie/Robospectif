using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml.Linq;
using System.IO;
using System.Text;
using System;

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
            /*   foreach(string line in EnAttenteCT.rejectedCards)
               {
                   rejectedCardsContent.AppendLine(line);
               }*/
            rejectedCardsContent.AppendLine("Joueur;Dimension;Locomotion;Equipement1;Equipement2;Equipement3");
            string filePath = "rejected_cards.csv";
             

            foreach (Joueur j in Partie.Joueurs)
            {
                    string numero = "J " + j.Numero;
                    string locomotion = "";
                    string dimension = "";
                    string equi1 = "";
                    string equi2 = "";
                    string equi3 = "";

                    foreach (Sprite loc in j.Locomotions)
                    {
                        if (loc != j.Loco)
                        {
                            locomotion = loc.name;
                            break;
                        }
                    }

                    foreach (Sprite dim in j.Dimensions)
                    {
                        if (dim != j.Dim)
                        {
                            dimension = dim.name;
                            break;
                        }
                    }

                    string[] chosenEquipements = { j.Equi1.name, j.Equi2.name, j.Equi3.name };
                    string[] equipements = { j.Equipements[0].name, JoueurStatic.Equipements[1].name, JoueurStatic.Equipements[2].name, JoueurStatic.Equipements[3].name, JoueurStatic.Equipements[4].name, JoueurStatic.Equipements[5].name };

                    for (int i = 0; i < 6; i++)
                    {
                        // equipements doesn't exist in chosenEquipements, take it to rejected cards
                        if (Array.IndexOf(chosenEquipements, equipements[i]) <= -1)
                        {
                            if (string.IsNullOrEmpty(equi1))
                            {
                                equi1 = equipements[i];
                                continue;
                            }
                            else if (string.IsNullOrEmpty(equi2))
                            {
                                equi2 = equipements[i];
                                continue;
                            }
                            else
                            {
                                equi3 = equipements[i];
                                break;
                            }
                        }
                    }

                    string line = numero + ";" + dimension + ";" + locomotion + ";" + equi1 + ";" + equi2 + ";" + equi3;
                    rejectedCardsContent.AppendLine(line);

            }
            //}
            File.AppendAllText(filePath, rejectedCardsContent.ToString());
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
