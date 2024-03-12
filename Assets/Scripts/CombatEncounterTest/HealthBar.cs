using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// This script was developed with the help of Github Co-pilot.

// This script handles the health bar and damage text for the player and enemy characters.
// All methods are called through the PlayerCharacterTest class.

public class HealthBar : MonoBehaviour
{

    public GameObject DamageTakenText(GameObject damageTakenText, GameObject damageTakenTextLocation, float damageTaken)
    {
        GameObject damageText = Instantiate(damageTakenText, damageTakenTextLocation.transform.position, Quaternion.identity, damageTakenTextLocation.transform);
        damageText.GetComponent<TMP_Text>().text = "-" + damageTaken.ToString();
        return damageText;
    }

    public IEnumerator MoveTextUpwardsAndDestroy(GameObject damageText)
    {
        float elapsedTime = 0f;
        float duration = 0.5f;
        float speed = 0.5f;
        Vector3 startPosition = damageText.transform.position;
        Vector3 targetPosition = startPosition + Vector3.up * speed; // Adjust the upward movement distance as needed

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            damageText.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return new WaitForEndOfFrame();
        }
            
        Destroy(damageText);
    }

    public void UpdateHealthBar(float health, float maxHealth, Slider healthBar, TMP_Text healthText)
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
