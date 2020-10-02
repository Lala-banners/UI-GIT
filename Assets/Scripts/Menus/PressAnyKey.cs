using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyKey : MonoBehaviour
{
    public GameObject pressAnyKeyPanel,menuPanel;

    void Start()
    {
        menuPanel.SetActive(false);
    }

    // 
    void Update()
    {
        if (Input.anyKey)
        {
            menuPanel.SetActive(true);
            pressAnyKeyPanel.SetActive(false);
        }
    }
}
