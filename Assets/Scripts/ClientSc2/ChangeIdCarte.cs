using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeIdCarte : MonoBehaviour
{
    public Button button;
    public Scene scene;
    // Start is called before the first frame update
    private Main.Player p;
    void Start(){
        p =  Main.Global.Player;
        button.onClick.AddListener(() => ButtonClicked());


    }
    void ButtonClicked()
    {
        //Debug.Log(p.ToString());
        StartCoroutine(changeId());
        //SceneManager.LoadScene(scene.handle);
        

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator changeId()
    {
        WWWForm form = new WWWForm();
        form.AddField("idPlayer",p.Id);
        form.AddField("idCarte1",p.Idcarte1);
        form.AddField("idCarte2",p.Idcarte2);
        form.AddField("idCarte3",p.Idcarte3);


        WWW www = new WWW("https://primsie-spears.000webhostapp.com/changeIdCarte.php",form);
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("ok");
        }
        else
        {
            Debug.Log("erroer"+www.text);
        }
    }
}
