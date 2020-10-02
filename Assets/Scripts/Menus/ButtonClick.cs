using System.Collections;
using System.Collections.Generic;
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
