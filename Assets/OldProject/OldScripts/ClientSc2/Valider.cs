using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

////Script attaché au bouton valider du canvas de choix des cartes
public class Valider : MonoBehaviour
{
    public static Main.Image[] personnages;
    [SerializeField] GameObject canvas_choix_cartes;
    [SerializeField] GameObject canvas_pres_robot;

    [SerializeField] Button button;
    [SerializeField] Image loco;
    [SerializeField] Image dim;
    [SerializeField] Image equi0;
    [SerializeField] Image equi1;
    [SerializeField] Image equi2;
    short idMessage = 1001;
    short conceptionID = 1002;
    short chronoID = 1003;

    void Start()
    {
        ////On initialise les variables stockées pour le reste de la partie (cartes choisies)
        JoueurStatic.Persos = new Sprite[6];
        JoueurStatic.PersosChoisis = new bool[] { false, false, false, false, false, false };
        button.onClick.AddListener(() => ButtonClicked());
        JoueurStatic.Client.RegisterHandler(chronoID, onChronoReceived);
    }

    ////Quand un premier joueur termine de choisir les cartes de son robot, les autres joueurs reçoivent 
    //// un message pour activer un chronomètre de 30 secondes 
    private void onChronoReceived(NetworkMessage netMsg)
    {
        int joueurFini = netMsg.ReadMessage<MyNetworkMessage>().message;
        if (joueurFini != JoueurStatic.Numero)
        {
            ScriptTimer.debutChrono();
        }
    }

    ////lorsqu'un joueur appuie sur valider, on stocke les choix du joueur, met à jour les caractéristique du robot
    //// et on passe au canvas suivant
    private void ButtonClicked()
    {
        JoueurStatic.Dim = dim.sprite;
        JoueurStatic.Loco = loco.sprite;
        JoueurStatic.Equi1 = equi0.sprite;
        JoueurStatic.Equi2 = equi1.sprite;
        JoueurStatic.Equi3 = equi2.sprite;
        MyNetworkMessage conception = new MyNetworkMessage();
        conception.message = JoueurStatic.Numero;
        JoueurStatic.Client.Send(conceptionID, conception);
        MyImageMessage robot = new MyImageMessage();
        robot.loco = loco.sprite.name;
        robot.dim = dim.sprite.name;
        robot.equi1 = equi0.sprite.name;
        robot.equi2 = equi1.sprite.name;
        robot.equi3 = equi2.sprite.name;
        robot.num = JoueurStatic.Numero;
        robot.zone = JoueurStatic.Position;
        JoueurStatic.Client.Send(idMessage, robot);
        
        canvas_choix_cartes.SetActive(false);
        canvas_pres_robot.SetActive(true);
    }

    //// est utilisé pour le chronomètre
    void Update()
    {
        ////lorsque le chronomètre est terminé, on sélectionne automatiquement les cartes devant le joueur
        if (ScriptTimer.done)
            ButtonClicked();
    }
}
