using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AttenteLogo : StateMachineBehaviour
{
    private string Ip_serveur = "172.21.232.220";
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        string hostName = Dns.GetHostName();
        IPHostEntry adresses = Dns.GetHostEntry(hostName);
        string Ip_serveur = adresses.AddressList[1].ToString();
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
