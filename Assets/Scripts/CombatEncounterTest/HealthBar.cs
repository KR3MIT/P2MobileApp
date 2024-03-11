using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public static void UpdateHealthBar (float health, float maxHealth, Slider healthBar, TMP_Text healthText)
    {
        if (health < 0)
        {
            health = 0;
        }
        healthText.text = "Health: " + health;
        healthBar.value = health / maxHealth;
        if (healthBar.value < 0.25f)
        {
            healthBar.fillRect.GetComponent<Image>().color = Color.red;
        }
        else if (healthBar.value < 0.5f)
        {
            healthBar.fillRect.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            healthBar.fillRect.GetComponent<Image>().color = Color.green;
        }
    }

}
