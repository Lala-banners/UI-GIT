using UnityEngine;

public class PressAnyKey : MonoBehaviour
{
    public GameObject pressAnyKeyPanel,menuPanel;

    void Start()
    {
        menuPanel.SetActive(false);
    }

    //Function to call press any key panel
    void Update()
    {
        if (Input.anyKey)
        {
            menuPanel.SetActive(true);
            pressAnyKeyPanel.SetActive(false);
        }
    }
}
