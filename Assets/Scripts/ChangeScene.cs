using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public Button buttonuser;
    public Scene scene;

    // Start is called before the first frame update
    void Start()
    {
      buttonuser.onClick.AddListener(() =>SceneManager.LoadScene(scene.handle));      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
