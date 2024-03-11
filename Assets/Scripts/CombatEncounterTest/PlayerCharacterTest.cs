using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacterTest : MonoBehaviour
{
    // Reference to the enemy character instance
    public EnemyCharacterTest enemyCharacter;
    public TMP_Text playerHealthText;
    public Slider playerHealthBar;
    public TMP_Text enemyHealthText;
    public Slider enemyHealthBar;


    float playerMaxHealth = 100;
    float playerHealth = 100;
    float enemyMaxHealth;
    float enemyHealth;
    int attackPower = 10;
    int defense = 5;

    // Start is called before the first frame update
    void Start()
    {
        InitiateCombat();
    }

    void InitiateCombat()
    {
        // Create an instance of the EnemyCharacterTest class
        enemyCharacter = new EnemyCharacterTest();

        // Modify the variables of the enemy character instance
        enemyHealth = enemyCharacter.Health = (float)Random.Range(80, 100);
        enemyMaxHealth = enemyCharacter.Health;
        enemyCharacter.attackPower = Random.Range(8, 10);
        enemyCharacter.defense = Random.Range(4, 5);

        StartCoroutine(CombatSequence());

    }
    IEnumerator CombatSequence()
    {

        while (playerHealth > 0 && enemyHealth > 0)
        {

            int playerAttack = attackPower + Random.Range(-2, 2);
            int enemyAttack = enemyCharacter.attackPower + Random.Range(-2, 2);
            int playerDefense = defense + Random.Range(-2, 2);
            int enemyDefense = enemyCharacter.defense + Random.Range(-2, 2);

            // Player attacks the enemy
            enemyHealth -= playerAttack - enemyDefense;

            Debug.Log("Player attacked enemy for: " + playerAttack);
            Debug.Log("Enemy defended for: " + enemyDefense);
            Debug.Log("Enemy health: " + enemyHealth);

            HealthBar.UpdateHealthBar(enemyHealth, enemyMaxHealth, enemyHealthBar, enemyHealthText);

            if (enemyHealth < 0)
            {
                break;
            }

            // Enemy attacks the player
            playerHealth -= enemyAttack - playerDefense;

            Debug.Log("Enemy attacked player for: " + enemyAttack);
            Debug.Log("Player defended for: " + playerDefense);
            Debug.Log("Player health: " + playerHealth);

            HealthBar.UpdateHealthBar(playerHealth, playerMaxHealth, playerHealthBar, playerHealthText);

            yield return new WaitForSeconds(1);
        }

        if (playerHealth < enemyHealth)
        {
            Debug.Log("Player has been defeated");
        }
        else
        {
            Debug.Log("Enemy has been defeated");
        }

    }
}
