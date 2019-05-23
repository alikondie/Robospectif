using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JetonsHandler : MonoBehaviour
{
    public Button usageVert;
    public Button usageRouge;
    public Button societeVert;
    public Button societeRouge;
    public Button planeteVert;
    public Button planeteRouge;

    private int usageCompteur;
    private int societeCompteur;
    private int planeteCompteur;

    // Start is called before the first frame update
    void Start()
    {
        usageCompteur = 0;
        societeCompteur = 0;
        planeteCompteur = 0;
        usageVert.onClick.AddListener(() => OnUsageClicked());       
        usageRouge.onClick.AddListener(() => OnUsageClicked());       
        societeVert.onClick.AddListener(() => OnSocieteClicked());       
        societeRouge.onClick.AddListener(() => OnSocieteClicked());       
        planeteVert.onClick.AddListener(() => OnPlaneteClicked());       
        planeteRouge.onClick.AddListener(() => OnPlaneteClicked());       
    }

    private void OnPlaneteClicked()
    {
        planeteCompteur++;
    }

    private void OnSocieteClicked()
    {
        societeCompteur++;
    }

    private void OnUsageClicked()
    {
        usageCompteur++;
    }

    // Update is called once per frame
    void Update()
    {
        if (usageCompteur >= 2)
        {
            usageVert.gameObject.SetActive(false);
            usageRouge.gameObject.SetActive(false);
        }

        if (societeCompteur >= 2)
        {
            societeVert.gameObject.SetActive(false);
            societeRouge.gameObject.SetActive(false);
        }

        if (planeteCompteur >= 2)
        {
            planeteVert.gameObject.SetActive(false);
            planeteRouge.gameObject.SetActive(false);
        }
    }
}
