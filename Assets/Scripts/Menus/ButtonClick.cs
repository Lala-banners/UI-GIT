using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public AudioSource woofButtonClick;

    public void playSoundEffect()
    {
        woofButtonClick.Play();
        Debug.Log("Woof!");
    }
}
