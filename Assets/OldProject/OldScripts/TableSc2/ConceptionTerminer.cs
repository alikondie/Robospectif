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

        //rejectedCardsContent = new StringBuilder();
        /*SansHUD.data.AppendLine("Cartes rejetees");
        SansHUD.data.AppendLine("Joueur;Dimension;Locomotion;Equipement1;Equipement2;Equipement3");*/


        // Initialise le compteur
        nbJoueurConceptionTerminer = 1;

        // Recuperation du Nombre de joueur:
        nbJoueur = PlayerPrefs.GetInt("nombreJoueur");     // Nombre de Joueurs

    }

    // Update is called once per frame
    void Update()
    {

        if (nbJoueurConceptionTerminer == nbJoueur)
        {
            #region recup_données
            rejectedCardsContent = new StringBuilder();
            //for(int i)
            //SceneManager.LoadScene(nomSceneDemander);

            //rejectedCardsContent.AppendLine("Joueur;Dimension;Locomotion;Equipement1;Equipement2;Equipement3");
            //string filePath = "donnees\\cartes_rejetees_le_" + DateTime.Now.ToString("dd-MM-yyyy") + "_a_" + DateTime.Now.ToString("hh") + "h" + DateTime.Now.ToString("mm") + "m" + DateTime.Now.ToString("ss") + "s" + ".csv";


            foreach (Joueur j in Partie.Joueurs)
            {
                int numero = j.Numero;
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
                string[] equipements = { j.Equipements[0].name, j.Equipements[1].name, j.Equipements[2].name, j.Equipements[3].name, j.Equipements[4].name, j.Equipements[5].name };

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

                CarteRejetee carte = new CarteRejetee
                {
                    Joueur = numero,
                    Dimension = dimension,
                    Locomotion = locomotion,
                    Equipement1 = equi1,
                    Equipement2 = equi2,
                    Equipement3 = equi3,

                };

                string json = JsonUtility.ToJson(carte);
                print("saved");
                File.WriteAllText(Application.dataPath + "/carte, +" + DateTime.Now.ToString("dd-MM-yyyy") + "_a_" + DateTime.Now.ToString("hh") + "h" + DateTime.Now.ToString("mm") + "m" + DateTime.Now.ToString("ss") + "s" + ".json", json);

                //SansHUD.data.AppendLine(line);

            }
            //}
            //File.AppendAllText(filePath, rejectedCardsContent.ToString());

            #endregion
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



