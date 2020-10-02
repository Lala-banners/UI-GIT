using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PauseHandler : MonoBehaviour
{
    public static bool isPaused;
    public GameObject pauseMenu; 
    public GameObject optionsMenu;
    public GameObject keybinds;

    void Paused()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void UnPaused()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        keybinds.SetActive(false);
        optionsMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        UnPaused();

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsMenu.activeSelf)
            {
                optionsMenu.SetActive(false);
                pauseMenu.SetActive(true);
            }
            else
            {
                isPaused = !isPaused;
                if(isPaused)
                {
                    Paused();
                }
                else
                {
                    UnPaused();
                }
            }
        }
    }
}
