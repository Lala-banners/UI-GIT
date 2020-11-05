using UnityEngine;
using UnityEngine.UI;

public class QuarterHearts : MonoBehaviour
{
    [SerializeField] private Image[] heartSlots;
    [SerializeField] private Sprite[] hearts;

    [SerializeField] public float currentHealth = 12;
    [SerializeField] public float maximumHealth = 12;
    [SerializeField] public float healthPerSection;

    public void UpdateHearts(float curHealth, float maxHealth) 
    {
        int heartSlotIndex = 0;

        healthPerSection = maxHealth / (heartSlots.Length * 4);
        
        foreach (Image image in heartSlots)
        {
            //If current health exceeds current health's value (healthPerSection eg 20) then fill that heart to max and check next one
            if(curHealth >= ((healthPerSection * 4)) + healthPerSection * 4 * heartSlotIndex)
            {
                //Set heart to 4/4 (full filled)
                heartSlots[heartSlotIndex].sprite = hearts[0];
            }
            else if(curHealth >= ((healthPerSection * 3)) + healthPerSection * 4 * heartSlotIndex)
            {
                //Set heart to 3/4
                heartSlots[heartSlotIndex].sprite = hearts[1];
            }
            else if (curHealth >= ((healthPerSection * 2)) + healthPerSection * 4 * heartSlotIndex)
            {
                //Set heart to 2/4 (half filled)
                heartSlots[heartSlotIndex].sprite = hearts[2];
            }
            else if (curHealth >= ((healthPerSection * 1)) + healthPerSection * 4 * heartSlotIndex)
            {
                //Set heart to 1/4
                heartSlots[heartSlotIndex].sprite = hearts[3];
            }
           
            else 
            {
                //heart slots are empty
                heartSlots[heartSlotIndex].sprite = hearts[4];
            }
            //after checking this slots, increase index by 1
            heartSlotIndex++;
        }
    }
}

