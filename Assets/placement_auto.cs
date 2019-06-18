using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placement_auto : MonoBehaviour
{
    #region variables
    private GameObject[,] cards_list;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SetCardsList(GameObject[,] cartes)
    {
        cards_list = cartes;
    }
}
