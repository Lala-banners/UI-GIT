using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customisation : MonoBehaviour
{
    public int currentHeight;

    void testMethod()
    {
        Debug.Log("Button is working");
    }

    //Updates specifically when GUI elements are called
    private void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 100, 210), "Visuals");

        currentHeight = 40;

        string[] names = { "Skin", "Hair", "Eyes", "Mouth", "Clothes", "Armour" };

        for (int i = 0; i < names.Length; i++)
        {
            if (GUI.Button(new Rect(20, currentHeight + i * 30, 20, 20), "<"))
            {
                testMethod();
            }

            GUI.Label(new Rect(45, currentHeight + i * 30, 60, 20), names[i]);

            if (GUI.Button(new Rect(80, currentHeight + i * 30, 20, 20), ">"))
            {
                testMethod();
            }
        }
    }

}
