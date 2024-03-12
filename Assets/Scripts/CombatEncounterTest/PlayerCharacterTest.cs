using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacterTest : MonoBehaviour
{
    // Reference to the enemy character instance and health bar instance
    public EnemyCharacterTest enemyCharacter;
    public HealthBar healthBars;

    // Reference to the health bar and its text
    public TMP_Text playerHealthText;
    public Slider playerHealthBar;
    public TMP_Text enemyHealthText;
    public Slider enemyHealthBar;

    // Reference to the damage taken text and its location
    public GameObject enemyDamageTakenText;
    public GameObject playerDamageTakenText;
    public GameObject enemyDamageTakenTextLocation;
    public GameObject playerDamageTakenTextLocation;

    // Variables for the player character
    float playerMaxHealth = 100;
    float playerHealth = 100;
    int attackPower = 10;
    int defense = 5;

    // Variables for the enemy character
    float enemyMaxHealth;
    float enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        InitiateCombat();
    }

    void InitiateCombat()
    {
        // Create an instance of the EnemyCharacterTest class, which represents the enemy character
        enemyCharacter = new EnemyCharacterTest();
        // Create an instance of the HealthBar class to handle the health bar and damage text
        healthBars = new HealthBar();

        // The enemy character's health, attack power, and defense are randomly generated within given ranges. enemyMaxHealth is set to the enemy's initial health.
        enemyHealth = enemyCharacter.Health = (float)Random.Range(80, 100);
        enemyMaxHealth = enemyHealth;
        enemyCharacter.attackPower = Random.Range(8, 10);
        enemyCharacter.defense = Random.Range(4, 5);

        // The Coroutine CombatSequence is started, which simulates the combat between the player and the enemy.
        StartCoroutine(CombatSequence());

    }
    IEnumerator CombatSequence()
    {
        // As long as both the player and the enemy are alive, the combat sequence continues.
        while (playerHealth > 0 && enemyHealth > 0)
        {
            // For each attack, the attack power and defense of both the player and the enemy are randomly adjusted within a range of -2 to 2.
            int playerAttack = attackPower + Random.Range(-2, 2);
            int enemyAttack = enemyCharacter.attackPower + Random.Range(-2, 2);
            int playerDefense = defense + Random.Range(-2, 2);
            int enemyDefense = enemyCharacter.defense + Random.Range(-2, 2);

            // PLAYER ATTACKS THE ENEMY
            // The enemy's health is reduced by the damage taken, which is the difference between the player's attack power and the enemy's defense.
            int enemyDamageTaken = playerAttack - enemyDefense;
            enemyHealth -= enemyDamageTaken;

            Debug.Log("Player attacked enemy for: " + playerAttack);
            Debug.Log("Enemy defended for: " + enemyDefense);
            Debug.Log("Enemy health: " + enemyHealth);

            // The health bar is updated to reflect the enemy's new health value.
            healthBars.UpdateHealthBar(enemyHealth, enemyMaxHealth, enemyHealthBar, enemyHealthText);

            // A damage taken text is displayed above the enemy's health bar, showing the amount of damage taken. The text then moves upwards and is destroyed after a short time.
            GameObject enemyDamageTakenObject = healthBars.DamageTakenText(enemyDamageTakenText, enemyDamageTakenTextLocation, enemyDamageTaken);
            StartCoroutine(healthBars.MoveTextUpwardsAndDestroy(enemyDamageTakenObject));

            // If the enemy's health reaches 0 or below, the enemy is defeated and the combat sequence is broken.
            if (enemyHealth <= 0)
            {
                Debug.Log("Enemy has been defeated");
                break;
            }
            // A short delay is added between the player's attack and the enemy's counterattack.
            yield return new WaitForSeconds((float)0.5);
            
            // ENEMY ATTACKS THE PLAYER
            // The player's health is reduced by the damage taken, which is the difference between the enemy's attack power and the player's defense.
            int playerDamageTaken = enemyAttack - playerDefense;
            playerHealth -= playerDamageTaken;

            Debug.Log("Enemy attacked player for: " + enemyAttack);
            Debug.Log("Player defended for: " + playerDefense);
            Debug.Log("Player health: " + playerHealth);

            // The health bar is updated to reflect the player's new health value.
            healthBars.UpdateHealthBar(playerHealth, playerMaxHealth, playerHealthBar, playerHealthText);

            // A damage taken text is displayed above the player's health bar, showing the amount of damage taken. The text then moves upwards and is destroyed after a short time.
            GameObject playerDamageTakenObject = healthBars.DamageTakenText(playerDamageTakenText, playerDamageTakenTextLocation, playerDamageTaken);
            StartCoroutine(healthBars.MoveTextUpwardsAndDestroy(playerDamageTakenObject));

            // A short delay is added between the enemy's attack and the player's counterattack.
            yield return new WaitForSeconds((float)0.5);
        }
        // If the player's health reaches 0 or below, the player is defeated.
        if (playerHealth < enemyHealth)
        {
            Debug.Log("Player has been defeated");
        }
    }
}
