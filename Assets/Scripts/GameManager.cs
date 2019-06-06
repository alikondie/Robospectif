using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        print("player's number : " + playersNumber);
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
