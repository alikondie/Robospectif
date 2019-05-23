using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class RecevoirJeton : MonoBehaviour
{
    private int[] positions = Button_ready_next_scene.envoi;

    public GameObject jeton1;
	public GameObject jeton2;
	public GameObject jeton3;
	public GameObject jeton4;
	public GameObject jeton5;
	public GameObject jeton6;
	public List<GameObject>[] jetons;
    public int[] index;
    GameObject objet;
	short jeton = 1010;

	// Start is called before the first frame update
	void Start()
    {
        jetons = new List<GameObject>[6];
        jetons[0] = new List<GameObject>();
        jetons[1] = new List<GameObject>();
        jetons[2] = new List<GameObject>();
        jetons[3] = new List<GameObject>();
        jetons[4] = new List<GameObject>();
        jetons[5] = new List<GameObject>();

        for (int i = 0; i < jeton1.transform.GetChildCount() - 1; i++)
        {
            jetons[0].Add(jeton1.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < jeton2.transform.GetChildCount() - 1; i++)
        {
            jetons[1].Add(jeton2.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < jeton3.transform.GetChildCount() - 1; i++)
        {
            jetons[2].Add(jeton3.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < jeton4.transform.GetChildCount() - 1; i++)
        {
            jetons[3].Add(jeton4.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < jeton5.transform.GetChildCount() - 1; i++)
        {
            jetons[4].Add(jeton5.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < jeton6.transform.GetChildCount() - 1; i++)
        {
            jetons[5].Add(jeton6.transform.GetChild(i).gameObject);
        }

        index = new int[6];
        for (int i = 0; i < 6; i++)
        {
            index[i] = 0;
        }
		NetworkServer.RegisterHandler(jeton, onJetonReceived);
	}

	private void onJetonReceived(NetworkMessage netMsg)
	{
		var v = netMsg.ReadMessage<MyJetonMessage>();
		int pos = v.joueur;
		string s = "Jetons/" + v.sprite;
		Sprite jeton_actuel = Resources.Load<Sprite>(s);
        for (int j = 0; j < 6; j++)
        {
            if(positions[j] == pos)
            {
                jetons[j][index[j]].gameObject.GetComponent<SpriteRenderer>().sprite = jeton_actuel;
                Debug.Log(jetons[j][index[j]].gameObject.GetComponent<SpriteRenderer>().sprite.ToString());
                jetons[j][index[j]].gameObject.SetActive(true);
                Debug.Log("active : " + jetons[j][index[j]].active);
                Debug.Log("index[" + (j) + "] = " + index[j]);
                Debug.Log("jetons[" + index[j] + "] = " + jetons[j][index[j]]);
                index[j]++;
            }
        }		
        /*objet = new GameObject("Jeton");
        objet.AddComponent<SpriteRenderer>();
        objet.AddComponent<BoxCollider2D>();
        objet.AddComponent<Jeton_pop>();
        objet.transform.position = jetons[pos - 1].transform.position;
        objet.GetComponent<SpriteRenderer>().enabled = false;
        jetons[pos - 1] = objet;
        Debug.Log(objet.gameObject.GetComponent<SpriteRenderer>().enabled);
        objet.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        Debug.Log(objet.gameObject.GetComponent<SpriteRenderer>().enabled);
        Debug.Log(objet.gameObject.GetComponent<SpriteRenderer>().isVisible);
        jetons[pos - 1].gameObject.GetComponent<SpriteRenderer>().sprite = jeton_actuel;
        Debug.Log("jetons[" + (pos - 1) + "] = " + jetons[pos - 1]);*/
    }

	// Update is called once per frame
	void Update()
    {
        
    }
}
