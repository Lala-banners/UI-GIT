using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    public static bool isPaused;
    public GameObject pauseMenu; 
    public GameObject optionsMenu;
    public GameObject keybinds;
    public GameObject HUD;
    public GameObject inventory;
    public GameObject dialoguePanel;

    public void Paused(GameObject panel)
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(false);
        inventory.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        panel.SetActive(true);
    }

    public void UnPaused()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        keybinds.SetActive(false);
        optionsMenu.SetActive(false);
        HUD.SetActive(true);
        inventory.SetActive(false);
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
                    Paused(pauseMenu);
                }
                else
                {
                    UnPaused();
                }
            }
        }

        if (DialogueNPC.showDialogue)
        {
            dialoguePanel.SetActive(true);
        }
        else if(dialoguePanel.activeSelf)
        {
            dialoguePanel.SetActive(false);
        }

    }
}
