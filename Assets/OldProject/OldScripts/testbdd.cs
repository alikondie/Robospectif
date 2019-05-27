using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testbdd : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button;
    public int i;
    void Start()
    {
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        button.onClick.AddListener(() => ButtonClicked());
    }    
    
    void ButtonClicked()
    {
        //Output this to console when the Button3 is clicked
        StartCoroutine(selectPlayer());
       
    }

    IEnumerator selectPlayer()
    {
        WWWForm form = new WWWForm();
        form.AddField("id",1);
        WWW www = new WWW("https://primsie-spears.000webhostapp.com/estSelect.php",form);
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("Il n y a pas de probleme");

        }
        else
        {
            Debug.Log("error"+www.text);
        }
    }
 }
