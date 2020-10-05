using UnityEngine;
using UnityEngine.UI;

public class QuarterHearts : MonoBehaviour
{
    
    [SerializeField] private Image[] heartSlots;
    [SerializeField] private Sprite[] hearts;

    private float currentHealth;
    private float maximumHealth;
    private float healthPerSection;

    public void Start()
    {
        //UpdateHearts();
    }


    #region Update Hearts
    public void UpdateHearts(float curHealth, float maxHealth)
    {
        currentHealth = curHealth;
        maximumHealth = maxHealth;

        healthPerSection = maximumHealth / (heartSlots.Length * 4);
    }
    #endregion

    private void OnGUI() //Previously private void Update()
    {
        int heartSlotIndex = 0;

        foreach(Image image in heartSlots)
        {
            //If current health exceeds current health's value (healthPerSection eg 20) then fill that heart to max and check next one
            if(currentHealth >= ((healthPerSection * 4)) + healthPerSection * 4 * heartSlotIndex)
            {
                //Set heart to 4/4 (full filled)
                heartSlots[heartSlotIndex].sprite = hearts[0];
            }
            else if(currentHealth >= ((healthPerSection * 3)) + healthPerSection * 4 * heartSlotIndex)
            {
                //Set heart to 3/4
                heartSlots[heartSlotIndex].sprite = hearts[1];
            }
            else if (currentHealth >= ((healthPerSection * 2)) + healthPerSection * 4 * heartSlotIndex)
            {
                //Set heart to 2/4 (half filled)
                heartSlots[heartSlotIndex].sprite = hearts[2];
            }
            else if (currentHealth >= ((healthPerSection * 1)) + healthPerSection * 4 * heartSlotIndex)
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
