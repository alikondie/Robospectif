using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Main;

public class Initialisation : MonoBehaviour
{
    public static int premierJoueur;

    private int pos = -1;

    public int position;

    public int[] positions;

    public Button button;

    public static int indice = 0;
    public static Sprite[,] images = new Sprite[6,5];

    public GameObject Plateau;

    public GameObject J1carte1;
    public GameObject J1carte2;
    public GameObject J1carte3;
    public GameObject J1carte4;
    public GameObject J1carte5;

    public GameObject J2carte1;
    public GameObject J2carte2;
    public GameObject J2carte3;
    public GameObject J2carte4;
    public GameObject J2carte5;

    public GameObject J3carte1;
    public GameObject J3carte2;
    public GameObject J3carte3;
    public GameObject J3carte4;
    public GameObject J3carte5;

    public GameObject J4carte1;
    public GameObject J4carte2;
    public GameObject J4carte3;
    public GameObject J4carte4;
    public GameObject J4carte5;

    public GameObject J5carte1;
    public GameObject J5carte2;
    public GameObject J5carte3;
    public GameObject J5carte4;
    public GameObject J5carte5;

    public GameObject J6carte1;
    public GameObject J6carte2;
    public GameObject J6carte3;
    public GameObject J6carte4;
    public GameObject J6carte5;


    // Start is called before the first frame update
    void Start()
    {
        positions = Text_Connexion.positions;

        premierJoueur = Partie.JoueurCourant;

        for (int i = 0; i < 6; i++)
        {
            if (positions[i] == premierJoueur)
            {
                pos = i + 1;
            }
        }

        Debug.Log("pos : " + pos);
       


        if (pos == 3)
        {
            Plateau.transform.Rotate(Vector3.forward * 90);
            button.transform.Rotate(Vector3.forward * 90);
            Vector2 posButton = new Vector2((float)-2.78, 0);
            button.transform.position = posButton;
        }

        if ((pos == 4) || (pos == 5))
        {
            Plateau.transform.Rotate(Vector3.forward * 180);
            button.transform.Rotate(Vector3.forward * 180);
            Vector2 posButton = new Vector2(0, (float)-2.78);
            button.transform.position = posButton;
        }

        if (pos == 6)
        {
            Plateau.transform.Rotate(Vector3.forward * -90);
            button.transform.Rotate(Vector3.forward * -90);
            Vector2 posButton = new Vector2((float)2.78, 0);
            button.transform.position = posButton;
        }

        if (pos != 1)
        { 
            Destroy(J1carte1);
            Destroy(J1carte2);
            Destroy(J1carte3);
            Destroy(J1carte4);
            Destroy(J1carte5);
        }

        if (pos != 2)
        {
            Destroy(J2carte1);
            Destroy(J2carte2);
            Destroy(J2carte3);
            Destroy(J2carte4);
            Destroy(J2carte5);
        }

        if (pos != 3)
        {
            Destroy(J3carte1);
            Destroy(J3carte2);
            Destroy(J3carte3);
            Destroy(J3carte4);
            Destroy(J3carte5);
        }

        if (pos != 4)
        {
            Destroy(J4carte1);
            Destroy(J4carte2);
            Destroy(J4carte3);
            Destroy(J4carte4);
            Destroy(J4carte5);
        }

        if (pos != 5)
        {
            Destroy(J5carte1);
            Destroy(J5carte2);
            Destroy(J5carte3);
            Destroy(J5carte4);
            Destroy(J5carte5);
        }

        if (pos != 6)
        {
            Destroy(J6carte1);
            Destroy(J6carte2);
            Destroy(J6carte3);
            Destroy(J6carte4);
            Destroy(J6carte5);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((positions[0] != 0) && (pos == 1))
        {
            J1carte1.GetComponent<SpriteRenderer>().sprite = images[0, 1];
            J1carte2.GetComponent<SpriteRenderer>().sprite = images[0, 2];
            J1carte3.GetComponent<SpriteRenderer>().sprite = images[0, 3];
            J1carte4.GetComponent<SpriteRenderer>().sprite = images[0, 4];
            J1carte5.GetComponent<SpriteRenderer>().sprite = images[0, 0];
        }

        if ((positions[1] != 0) && (pos == 2))
        {
            J2carte1.GetComponent<SpriteRenderer>().sprite = images[1, 1];
            J2carte2.GetComponent<SpriteRenderer>().sprite = images[1, 2];
            J2carte3.GetComponent<SpriteRenderer>().sprite = images[1, 3];
            J2carte4.GetComponent<SpriteRenderer>().sprite = images[1, 4];
            J2carte5.GetComponent<SpriteRenderer>().sprite = images[1, 0];
        }

        if ((positions[2] != 0) && (pos == 3))
        {
            J3carte1.GetComponent<SpriteRenderer>().sprite = images[2, 1];
            J3carte2.GetComponent<SpriteRenderer>().sprite = images[2, 2];
            J3carte3.GetComponent<SpriteRenderer>().sprite = images[2, 3];
            J3carte4.GetComponent<SpriteRenderer>().sprite = images[2, 4];
            J3carte5.GetComponent<SpriteRenderer>().sprite = images[2, 0];
        }

        if ((positions[3] != 0) && (pos == 4))
        {
            J4carte1.GetComponent<SpriteRenderer>().sprite = images[3, 1];
            J4carte2.GetComponent<SpriteRenderer>().sprite = images[3, 2];
            J4carte3.GetComponent<SpriteRenderer>().sprite = images[3, 3];
            J4carte4.GetComponent<SpriteRenderer>().sprite = images[3, 4];
            J4carte5.GetComponent<SpriteRenderer>().sprite = images[3, 0];
        }

        if ((positions[4] != 0) && (pos == 5))
        {
            J5carte1.GetComponent<SpriteRenderer>().sprite = images[4, 1];
            J5carte2.GetComponent<SpriteRenderer>().sprite = images[4, 2];
            J5carte3.GetComponent<SpriteRenderer>().sprite = images[4, 3];
            J5carte4.GetComponent<SpriteRenderer>().sprite = images[4, 4];
            J5carte5.GetComponent<SpriteRenderer>().sprite = images[4, 0];
        }

        if ((positions[5] != 0) && (pos == 6))
        {
            J6carte1.GetComponent<SpriteRenderer>().sprite = images[5, 1];
            J6carte2.GetComponent<SpriteRenderer>().sprite = images[5, 2];
            J6carte3.GetComponent<SpriteRenderer>().sprite = images[5, 3];
            J6carte4.GetComponent<SpriteRenderer>().sprite = images[5, 4];
            J6carte5.GetComponent<SpriteRenderer>().sprite = images[5, 0];
        }
    }

    public static void get(Sprite[] image, int zone)
    {
        Debug.Log("zone : " + zone);
        for (int i = 0; i < 5; i++)
        {
            images[zone-1, i] = image[i];
        }
    }

}
