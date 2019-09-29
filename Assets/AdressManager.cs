using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdressManager : MonoBehaviour
{
    public static string ipAdress = "";
    public InputField adressField;
    public static bool isServer = false;

    public void AllocateIPAdress()
    {
        isServer = false;
        ipAdress = adressField.text;
        SceneManager.LoadScene("LogoScene");
    }

    public void Pass()
    {
        isServer = true;
        SceneManager.LoadScene("LogoScene");
    }


}
