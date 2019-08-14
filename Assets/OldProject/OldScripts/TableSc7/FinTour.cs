using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinTour : MonoBehaviour
{
    [SerializeField] GameObject canvas_fin_tour;
    [SerializeField] GameObject canvas_plateau_vehicule;
    [SerializeField] GameObject canvas_fin;
    [SerializeField] GameObject cartes;

    [SerializeField] Button prochain;
    [SerializeField] Button fin;

    short nextID = 1015;
    short nextexpertID = 1024;

    // Start is called before the first frame update
    void Start()
    {
        if (Partie.Langue == "FR")
        {
            prochain.transform.GetChild(0).GetComponent<Text>().text = "Continuer la partie";
            fin.transform.GetChild(0).GetComponent<Text>().text = "Terminer la partie";
        }
        else
        {

            prochain.transform.GetChild(0).GetComponent<Text>().text = "Continue the game";
            fin.transform.GetChild(0).GetComponent<Text>().text = "End the game";
        }
        prochain.onClick.AddListener(() => ProchainClicked());
        fin.onClick.AddListener(() => FinClicked());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ProchainClicked()
    {
        for (int k = 0; k < Tour.Listiscartesposees.Length; k++)
        {
            Tour.Listiscartesposees[k] = false;
        }
        for (int k = 0; k < 10; k++)
        {
            cartes.transform.GetChild(k).gameObject.SetActive(true);
        }
        MyStringMessage next = new MyStringMessage();
        if (Partie.Type == "expert")
        {
            next.s = "next";
            NetworkServer.SendToAll(nextexpertID, next);
            canvas_fin_tour.SetActive(false);
            canvas_plateau_vehicule.SetActive(true);
        }
        else
        {
            next.s = "next";
            NetworkServer.SendToAll(nextID, next);
            canvas_fin_tour.SetActive(false);
            canvas_plateau_vehicule.SetActive(true);
        }
    }

    private void FinClicked()
    {
        MyStringMessage end = new MyStringMessage();
        if (Partie.Type == " expert")
        {
            end.s = "end";
            NetworkServer.SendToAll(nextexpertID, end);
            canvas_fin_tour.SetActive(false);
            canvas_fin.SetActive(true);
        }
        else
        {
            end.s = "end";
            NetworkServer.SendToAll(nextID, end);
            canvas_fin_tour.SetActive(false);
            canvas_fin.SetActive(true);
        }
    }
}
