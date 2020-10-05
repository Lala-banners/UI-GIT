using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    public static bool isPaused;
    public GameObject pauseMenu; 
    public GameObject optionsMenu;
    public GameObject keybinds;
    public GameObject HUD;

    void Paused()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        HUD.SetActive(false);
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
