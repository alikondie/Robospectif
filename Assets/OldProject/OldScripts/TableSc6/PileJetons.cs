using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileJetons : MonoBehaviour
{
    public GameObject pile1;
    public GameObject pile2;
    public GameObject pile3;
    public GameObject pile4;
    public GameObject pile5;
    public GameObject pile6;

    public Sprite[,] sprites;
    public bool[,] actives;


    // Start is called before the first frame update
    void Start()
    {
        sprites = InitDebat.envoiSprites;
        actives = InitDebat.envoiActives;

        for (int j = 0; j < 6; j++)
        {
            for (int i = 0; i < pile1.transform.childCount; i++)
            {
                switch (j)
                {
                    case 0:
                        pile1.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = sprites[j, i];
                        pile1.transform.GetChild(i).gameObject.SetActive(actives[j, i]);
                        break;
                    case 1:
                        pile2.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = sprites[j, i];
                        pile2.transform.GetChild(i).gameObject.SetActive(actives[j, i]);
                        break;
                    case 2:
                        pile3.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = sprites[j, i];
                        pile3.transform.GetChild(i).gameObject.SetActive(actives[j, i]);
                        break;
                    case 3:
                        pile4.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = sprites[j, i];
                        pile4.transform.GetChild(i).gameObject.SetActive(actives[j, i]);
                        break;
                    case 4:
                        pile5.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = sprites[j, i];
                        pile5.transform.GetChild(i).gameObject.SetActive(actives[j, i]);
                        break;
                    case 5:
                        pile6.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = sprites[j, i];
                        pile6.transform.GetChild(i).gameObject.SetActive(actives[j, i]);
                        break;
                }
            }
        }
    }
      

    // Update is called once per frame
    void Update()
    {

    }
}
