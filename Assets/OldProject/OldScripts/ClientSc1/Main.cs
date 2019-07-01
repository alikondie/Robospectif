using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    [SerializeField] Sprite[] personnages;
    [SerializeField] Sprite[] locomotions;
    [SerializeField] Sprite[] dimensions;
    [SerializeField] Sprite[] equipements;

    [SerializeField] Sprite[] dimensionsUrbain;
    [SerializeField] Sprite[] locomotionsUrbain;

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

        override public string ToString()
        {
            return "" + this.id.ToString() + this.isSelect.ToString() + this.place.ToString() +
                   this.idcarte1.ToString() + this.idcarte2.ToString() + this.idcarte3.ToString() +
                   this.isOk.ToString();
        }
    }

    public class Image
    {
        private int id;
        public Sprite sprite;


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
        private static Player[] tabPlayer;
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
        override public string ToString()
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
        for (int i = 0; i < personnages.Length; i++)
        {
            tab.addImage(new Image(tab.Taille, personnages[i]));
           
        }
        for (int i = 0; i < personnages.Length; i++)
        {
            tab.addImage(new Image(tab.Taille, personnages[i]));

        }

        Global.TabP = tab;
    }

    private void initializeLoco()
    {
        tab = new TabImage();

        if (Partie.Type == "standard")
        {
            for (int i = 1; i <= 2; i++)
            {
                foreach (Sprite s in locomotions)
                {
                    tab.addImage(new Image(tab.Taille, s));

                }
            }
        }
        else /* mode urbain */
        {
            for (int i = 1; i <= 3; i++)
            {
                foreach (Sprite s in locomotionsUrbain)
                {
                    tab.addImage(new Image(tab.Taille, s));
                }
            }
        }
        Global.TabL = tab;

    }


    private void initializeDi()
    {
        tab = new TabImage();
        if (Partie.Type == "standard")
        {
            for (int i = 1; i <= 2; i++)
            {
                foreach (Sprite s in dimensions)
                {
                    tab.addImage(new Image(tab.Taille, s));

                }
            }
        }
        else /* mode urbain */
        {
            for (int i = 1; i <= 3; i++)
            {
                foreach (Sprite s in dimensionsUrbain)
                {
                    tab.addImage(new Image(tab.Taille, s));
                }
            }
        }
        Global.TabD = tab;

    }

    private void initializeEqui()
    {
        tab = new TabImage();
        for (int i = 0; i < equipements.Length; i++)
        {
            tab.addImage(new Image(tab.Taille, equipements[i]));

        }
        for (int i = 0; i < equipements.Length; i++)
        {
            tab.addImage(new Image(tab.Taille, equipements[i]));

        }
        Global.TabE = tab;

    }
}
