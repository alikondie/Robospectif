using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class Button_ready_next_scene : MonoBehaviour
{
    [SerializeField] GameObject canvas_mains;
    [SerializeField] GameObject canvas_joueurs;
    [SerializeField] Text nb_joueurs;
    [SerializeField] Button[] hands;
    [SerializeField] Sprite[] colors;

    private int[] indices;
    private int[] positions;
    public static int[] envoi;
    short positionsID = 1005;
    private string Ip_serveur = "172.21.232.220";

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < hands.Length; i++)
        {
            PlayerPrefs.SetInt("LaPosition" + (i + 1), 0);
        }
        this.gameObject.SetActive(false);
        positions = new int[6];
        indices = new int[hands.Length];
        for (int i = 0; i < indices.Length; i++)
        {
            indices.SetValue(0, i);
        }
        hands[0].onClick.AddListener(() => onHandClicked(0));
        hands[1].onClick.AddListener(() => onHandClicked(1));
        hands[2].onClick.AddListener(() => onHandClicked(2));
        hands[3].onClick.AddListener(() => onHandClicked(3));
        hands[4].onClick.AddListener(() => onHandClicked(4));
        hands[5].onClick.AddListener(() => onHandClicked(5));
        string ipv4 = IPManager.GetIP(IPManager.ADDRESSFAM.IPv4);
        if (ipv4 != Ip_serveur)
        {
            canvas_mains.SetActive(false);
            canvas_joueurs.SetActive(true);
        }
    }

    private void onHandClicked(int i)
    {
        indices[i] = (indices[i] + 1) % 2;
        //hands[i].gameObject.GetComponent<Image>().sprite = colors[indices[i]];
        MiseAJourText();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void MiseAJourText()
    {
        int nb = 0;
        for (int i = 0; i < indices.Length; i++)
        {
            nb += indices[i];
            if (indices[i] == 1)
                PlayerPrefs.SetInt("LaPosition" + (i+1), nb);
        }
        if (nb >= 1)
            this.gameObject.SetActive(true);
        else
            this.gameObject.SetActive(false);
        
        for (int i = 0; i < indices.Length; i++)
        {
            if (indices[i] == 1)
            {

            }
        }

        nb_joueurs.text = "Il y a " + nb + " joueurs enregistrés";
        PlayerPrefs.SetInt("nbJoueur", nb);    //Envoie le nombre de Joueur
    }

    void OnMouseDown()
    {
        // ----- PARTIE NATHAN -----
        // Envoi des positions
        for (int i = 1; i <= 6; i++)
        {
            PlayerPrefs.SetInt("P" + i, PlayerPrefs.GetInt("LaPosition" + i));
        }
        // Envoies le nombre de joueur
        PlayerPrefs.SetInt("nombreJoueur", PlayerPrefs.GetInt("nbJoueur"));
        int indice = 1;
        for (int i = 0; i <= 5; i++)
        {
            if (PlayerPrefs.GetInt("P" + (i + 1)) == 0)
            {
                positions[i] = 0;
            }
            else
            {
                positions[i] = indice;
                indice++;
            }     // Position des joueurs
        }
        envoi = positions;
        MyPositionsMessage message = new MyPositionsMessage();
        message.position1 = positions[0];
        message.position2 = positions[1];
        message.position3 = positions[2];
        message.position4 = positions[3];
        message.position5 = positions[4];
        message.position6 = positions[5];
        NetworkServer.SendToAll(positionsID, message);

        // --------------------------

        // Debug.Log("Click");
        //SceneManager.LoadScene("Scene_2");
        canvas_mains.SetActive(false);
        canvas_joueurs.SetActive(true);
    }

}
