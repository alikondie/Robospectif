using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileJetons : MonoBehaviour
{
    [SerializeField] GameObject pile1;
    [SerializeField] GameObject pile2;
    [SerializeField] GameObject pile3;
    [SerializeField] GameObject pile4;
    [SerializeField] GameObject pile5;
    [SerializeField] GameObject pile6;

    private Sprite[,] sprites;
    private bool[,] actives;


    // Start is called before the first frame update
    void Start()
    {
        sprites = Tour.JetonsDebat;
        actives = Tour.ActivesDebat;

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
