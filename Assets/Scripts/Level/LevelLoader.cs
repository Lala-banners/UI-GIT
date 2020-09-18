using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Image progressBar;
    public Text progressBarText;

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsyncronously(sceneIndex));
    }

    IEnumerator LoadAsyncronously(int sceneIndex)
    {
        //Async loads out of time, one scene is loading in the background, Unity moves onto the next one.
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            Debug.Log(operation.progress);

            float progress = Mathf.Clamp01(operation.progress) / 0.9f; 
            progressBar.fillAmount = progress;
            progressBarText.text = Mathf.Round(progress) * 100 + "%";

            /*
             * int x -Mathf.Round(10.7f); //Rounds to 11
             * int y = Mathf.RoundToInt(10.7f); //Rounds to 11
             * int z (int)10.7f; //Round to 10
            */

            /*
            Also rounds to 10
            string abc = string.Format("0:0", 10.7f);
            Debug.Log(abc);
            */
            yield return null;
        }
    }
   
}
