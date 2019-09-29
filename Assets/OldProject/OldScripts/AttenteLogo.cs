using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AttenteLogo : StateMachineBehaviour
{
    private string Ip_serveur = "192.168.43.40";
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Ip_serveur = AdressManager.ipAdress;

        string ipv4 = IPManager.GetIP(IPManager.ADDRESSFAM.IPv4);
        if (AdressManager.isServer)
        {
            SceneManager.LoadScene("menu");
        }
        
        else
        {
            SceneManager.LoadScene("standard_game_client");
        }
    }
}
