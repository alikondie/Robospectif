using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public class Player
    {
        private int id;
        private int isSelect;
        private int place;
        private int idcarte1;
        private int idcarte2;
        private int idcarte3;
        private int isOk;

        public Player(int id, int isSelect, int place, int idcarte1, int idcarte2, int idcarte3, int isOk)
        {
            this.id = id;
            this.isSelect = isSelect;
            this.place = place;
            this.idcarte1 = idcarte1;
            this.idcarte2 = idcarte2;
            this.idcarte3 = idcarte3;
            this.isOk = isOk;
        }

        public Player(int id)
        {
            this.id = id;
            this.isSelect = 1;
            this.idcarte1 = 0;
            this.idcarte2 = 0;
            this.idcarte3 = 0;
            this.isOk = 0;
        }

        public Player()
        {
            this.id = 0;
            this.isSelect = 0;
            this.idcarte1 = 0;
            this.idcarte2 = 0;
            this.idcarte3 = 0;
            this.isOk = 0;
        }

        public int Id
        {
            get => id;
            set => id = value;
        }

        public int IsSelect
        {
            get => isSelect;
            set => isSelect = value;
        }

        public int Place
        {
            get => place;
            set => place = value;
        }

        public int Idcarte1
        {
            get => idcarte1;
            set => idcarte1 = value;
        }

        public int Idcarte2
        {
            get => idcarte2;
            set => idcarte2 = value;
        }

        public int Idcarte3
        {
            get => idcarte3;
            set => idcarte3 = value;
        }

        public int IsOk
        {
            get => isOk;
            set => isOk = value;
        }

        public String ToString()
        {
            return "" + this.id.ToString() + this.isSelect.ToString() + this.place.ToString() +
                   this.idcarte1.ToString() + this.idcarte2.ToString() + this.idcarte3.ToString() +
                   this.isOk.ToString();
        }
    }

    public class Image
    {
        private int id;
        private Sprite sprite;


        public Image(int id, Sprite sprite)
        {
            this.id = id;
            this.sprite = sprite;
        }

        public int Id
        {
            get => id;
            set => id = value;
        }

        public Sprite Sprite
        {
            get => sprite;
            set => sprite = value;
        }
    }

    public class TabImage
    {
        private int taille;
        private List<Image> tabsprite;


        public TabImage()
        {
            tabsprite = new List<Image>();
            this.taille = 0;
        }

        public void addImage(Image image)
        {
            tabsprite.Add(image);
            taille++;
        }

        public void removeImage(Image image)
        {
            tabsprite.Remove(image);
            taille--;
        }

        public int Taille
        {
            get => taille;
            set => taille = value;
        }

        public List<Image> Tabsprite
        {
            get => tabsprite;
            set => tabsprite = value;
        }

        public Image getImageind(int i)
        {
            return this.tabsprite[i];
        }

        public void toString()
        {
            for (int i = 0; i < this.Taille; i++)
            {
                Debug.Log(getImageind(i));
            }
        }
    }

    public class Global
    {
        private static Player player;
        private static TabImage tabL;
        private static TabImage tabD;
        private static TabImage tabE;
        private static TabImage tabP;
        public static Player[] tabPlayer;
        private static int nbjoueur;

        public Global()
        {
            player = new Player();
            tabL = new TabImage();
            tabD = new TabImage();
            tabE = new TabImage();
            tabPlayer = new Main.Player[7];
            nbjoueur = 0;
        }
        
        public static Player[] TabPlayer
        {
            get => tabPlayer;
            set => tabPlayer = value;
        }

        public static int Nbjoueur
        {
            get => nbjoueur;
            set => nbjoueur = value;
        }

        public void SetPlayer(Player[] p)
        {
            tabPlayer = p;
        }

        public static void Setnbjoueur(int i)
        {
            nbjoueur = i;
        }
        public static void addPlayer(Player p)
        {
            tabPlayer[nbjoueur] = p;
            nbjoueur++;
        }
        public static string ToString()
        {
            string res = "";
            for (int i = 1; i < 7; i++)
            {
                res = res + TabPlayer[i].ToString() + " ; ";
            }

            return res;
        }

        public static Player Player
        {
            get => player;
            set => player = value;
        }

        public static TabImage TabL
        {
            get => tabL;
            set => tabL = value;
        }

        public static TabImage TabD
        {
            get => tabD;
            set => tabD = value;
        }

        public static TabImage TabE
        {
            get => tabE;
            set => tabE = value;
        }

        public static TabImage TabP
        {
            get => tabP;
            set => tabP = value;
        }
    }

    private TabImage tab;

    // Start is called before the first frame update
    void Start()
    {

        Global g = new Global();
        initializeLoco();
        initializeDi();
        initializeEqui();
        initializePerso();

    }

    private void initializePerso()
    {
        tab = new TabImage();
        for (int i = 0; i < 2; i++)
        {
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Agathe")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Antonio")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Aurelie")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Bob")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Camille")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Christine")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Corinne")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/David")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Denis")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Dimitri")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Dominik")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Eleonore")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Elise")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Frederic")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Gaetan")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Jacques")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Johanna")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/John")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Judith")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Julio")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Laura")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Luc")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Marie")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Mathilde")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Mohammed")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Nadege")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Nathalie")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Nicolas")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Patrick")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Philippe")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Rose")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Sarah")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Thierry")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Tom")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Veronique")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Personnages/Yvette")));
        }
        Global.TabP = tab;
    }

    private void initializeLoco()
    {
        tab = new TabImage();
        for (int i = 0; i < 2; i++)
        {
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Locomotion/Ailes")));
            /*string s = "image/Locomotion/Ailes";
            Sprite sp = Resources.Load<Sprite>(s);
            Debug.Log(sp);*/
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Locomotion/Ballon")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Locomotion/Chenilles")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Locomotion/Flottaison")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Locomotion/Helices")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Locomotion/Pattes")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Locomotion/Rampant")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Locomotion/Rebondir")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Locomotion/Roues")));
        }
        Global.TabL = tab;

    }


    private void initializeDi()
    {
        tab = new TabImage();
        for (int i = 0; i < 2; i++)
        {
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Dimension/GrandeCapacite")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Dimension/GrandVehiculeLourd")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Dimension/GrandVehiculeIntermediaire")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Dimension/Monoplace")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Dimension/PetitRobot")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Dimension/TresGrandeCapacite")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Dimension/VehiculeIntermediaire")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Dimension/VehiculeLeger")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Dimension/VehiculeLourd")));
        }
        Global.TabD = tab;

    }
    public void initializeEqui()
    {
        tab = new TabImage();
        for (int i = 0; i < 2; i++)
        {
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/AppelUrgence")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/BrasArticules")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/CameraThermique")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/CanonPeinture")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/CarrosserieReflechissante")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/CentraleRecyclage")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/CoffreBlinde")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/CommandeVocale")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/ConnexionVPN")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/Couchettes")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/Cuisine")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/DetecteurFumeeGaz")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/Eoliennes")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/EquipementsFitness")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/FlotteDecentralisee")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/FuseeDetresse")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/Imprimante3D")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/KitReparation")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/KitSoinsUrgence")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/LaboratoireAnalyses")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/Minibar")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/OutillageChantier")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/PanneauxSolaires")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/PanneauxVideos")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/ProjectionHolographique")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/RampeAcces")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/SiegeEjectable")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/Sonar")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/SondesMesures")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/StationJeu")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/StudioEnregistrement")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/SystemeArrosage")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/SystemeEcoute")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/Toboggan")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/TVSatellite")));
            tab.addImage(new Image(tab.Taille, Resources.Load<Sprite>("image/Equipements/Ventouses")));
        }
        Global.TabE = tab;

    }
}
