using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Properties

    public List<Canvas> phases;
    #endregion

    #region Inputs
    #endregion

    #region Go or components
    #endregion

    #region Variables
    private int currentPhase = 0;
    [HideInInspector]
    public int playersNumber = 0;
    #endregion

    #region Unity loop
    void Awake()
    {
        
    }

	void FixedUpdate()
    {
        
    }
	
    void Update()
    {
        
    }
    #endregion

    #region Methods
    public void AddPlayer()
    {
        playersNumber++;
        GameObject clickedHand = EventSystem.current.currentSelectedGameObject.gameObject;
        // get the hand's animator and image color.
        clickedHand.GetComponent<Animator>().SetTrigger("Shake");
        clickedHand.GetComponent<Image>().color = Color.green;
        //print("player's number : " + playersNumber);

    }

    public void LoadNextPhase()
    {
        phases[currentPhase].gameObject.SetActive(false);
        phases[++currentPhase].gameObject.SetActive(true);
    }

    public void LoadScene(int index)
    {

    }
    #endregion
}
