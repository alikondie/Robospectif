using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button standard;


    // Start is called before the first frame update
    void Start()
    {
        standard.onClick.AddListener(() => ButtonClicked());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ButtonClicked()
    {
        SceneManager.LoadScene("1ere scene jeu");
    }
}
