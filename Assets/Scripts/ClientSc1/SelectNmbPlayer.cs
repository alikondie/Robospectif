using System;
using System.Collections;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SelectNmbPlayer : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    public Button button6;

    public static Main.Player p1 ;
    public static Main.Player p2 ;
    public static Main.Player p3 ;
    public static Main.Player p4 ;
    public static Main.Player p5 ;
    public static Main.Player p6 ;

    static Main.Player[] tabPlayer;

    // Start is called before the first frame update

    void Start()
    {
        InvokeRepeating("requette", 0f, 1f);
        p1 = new Main.Player();
        p2 = new Main.Player();
        p3 = new Main.Player();
        p4 = new Main.Player();
        p5 = new Main.Player();
        p6 = new Main.Player();
        tabPlayer = new Main.Player[7]{null,p1,p2,p3,p4,p5,p6};
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        button1.interactable = tabPlayer[1].IsSelect != 1;
        button2.interactable = tabPlayer[2].IsSelect != 1;
        button3.interactable = tabPlayer[3].IsSelect != 1;
        button4.interactable = tabPlayer[4].IsSelect != 1;
        button5.interactable = tabPlayer[5].IsSelect != 1;
        button6.interactable = tabPlayer[6].IsSelect != 1;
    }

    private static IEnumerator userInfo()
    {
        

    WWWForm logform = new WWWForm();
        var www = new WWW("https://primsie-spears.000webhostapp.com/select.php", logform);
        yield return www;

        string[] data = www.text.Split(new string[] {","}, System.StringSplitOptions.None);
        int x = 0;
        
        for (int i = 1; i < 7; i++)
        {
            tabPlayer[i] = new Main.Player(int.Parse(data[x++]), int.Parse(data[x++]), int.Parse(data[x++]), int.Parse(data[x++]),
                int.Parse(data[x++]), int.Parse(data[x++]), int.Parse(data[x++]));
    
        }

    }

    private void requette()
    {
        StartCoroutine(userInfo());
    }

    public static Main.Player  getPlayer(int i)
    {
        return tabPlayer[i]; 
    }
}