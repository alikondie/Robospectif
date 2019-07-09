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
        Debug.Log(Ip_serveur);

        string ipv4 = IPManager.GetIP(IPManager.ADDRESSFAM.IPv4);
        if (ipv4 == Ip_serveur)
        {
            SceneManager.LoadScene("menu");
        }
        else
        {
            SceneManager.LoadScene("standard_game_client");
        }
    }
}
