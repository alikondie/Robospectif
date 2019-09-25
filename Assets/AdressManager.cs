using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdressManager : MonoBehaviour
{
    public static string ipAdress = "";
    public InputField adressField;

    public void AllocateIPAdress()
    {
        ipAdress = adressField.text;
        SceneManager.LoadScene("LogoScene");
    }

    public void Pass()
    {
        SceneManager.LoadScene("LogoScene");
    }


}
