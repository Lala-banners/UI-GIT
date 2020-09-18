using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    #region Public variables
    public string LoadScene = "GameScene";
    public Dropdown qualityDropdown;
    public Toggle fullscreenToggle;
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider SFXSlider;
    
    #endregion
    
    public void Start()
    {
        LoadPlayerPrefs();

        Debug.Log("Starting Game Main Menu");
        if (!PlayerPrefs.HasKey("fullscreen"))
        {
            PlayerPrefs.SetInt("fullscreen", 0);
            Screen.fullScreen = false;
        }
        else
        {
            if (PlayerPrefs.GetInt("fullscreen") == 0)
            {
                Screen.fullScreen = false;
            }
            else
            {
                Screen.fullScreen = true;
            }
        }
        if (!PlayerPrefs.HasKey("quality"))
        {
            PlayerPrefs.SetInt("quality", 5);//dont have magic numbers
            QualitySettings.SetQualityLevel(5);
        }
        else
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("quality"));
        }
        PlayerPrefs.Save();
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    #region Change settings 
    //This changes the screen from fullscreen to windowed
    public void SetFullScreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }
    public void ChangeQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
    public void SetMusicVolume(float value)
    {
        mixer.SetFloat("MusicVol", value);
    }
    public void SetSFXVolume(float value)
    {
        mixer.SetFloat("SFXVol", value);
    }
    #endregion
  
    #region Save and load player prefs
    public void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt("quality", QualitySettings.GetQualityLevel());
        PlayerPrefs.SetInt("quality", qualityDropdown.value);
        if (fullscreenToggle.isOn)
        {
            PlayerPrefs.SetInt("fullscreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("fullscreen", 0);
        }
        
        //save audio sliders
        float musicVol;
        if (mixer.GetFloat("MusicVol", out musicVol))
        {
            PlayerPrefs.SetFloat("MusicVol", musicVol);
        }
        float SFXVol;
        if (mixer.GetFloat("SFXVol", out SFXVol))
        {
            PlayerPrefs.SetFloat("SFXVol", SFXVol);
        }

        PlayerPrefs.Save();

    }
    public void LoadPlayerPrefs()
    {
        //Load Quality
        if (PlayerPrefs.HasKey("quality"))
        {
            int quality = PlayerPrefs.GetInt("quality");
            qualityDropdown.value = quality;
            if (QualitySettings.GetQualityLevel() != quality)
            {
                ChangeQuality(quality);
            }
        }
        //load fullscreen
        if (PlayerPrefs.HasKey("fullscreen"))
        {
            if (PlayerPrefs.GetInt("fullscreen") == 0)
            {
                fullscreenToggle.isOn = false;
            }
            else
            {
                fullscreenToggle.isOn = true;
            }
        }
        //load audio Sliders
        if (PlayerPrefs.HasKey("MusicVol"))
        {
            float musicVol = PlayerPrefs.GetFloat("MusicVol");
            musicSlider.value = musicVol;
            mixer.SetFloat("MusicVol", musicVol);
        }
        if (PlayerPrefs.HasKey("SFXVol"))
        {
            float SFXVol = PlayerPrefs.GetFloat("SFXVol");
            SFXSlider.value = SFXVol;
            mixer.SetFloat("SFXVol", SFXVol);
        }
    }
}

    #endregion







