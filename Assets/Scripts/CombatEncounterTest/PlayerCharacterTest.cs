using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterTest : MonoBehaviour
{
    // Reference to the enemy character instance
    private EnemyCharacterTest enemyCharacter;

    int health = 100;
    int attackPower = 10;
    int defense = 5;

    // Start is called before the first frame update
    void Start()
    {
        InitiateCombat();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitiateCombat()
    {
        // Create an instance of the EnemyCharacterTest class
        enemyCharacter = new EnemyCharacterTest();

        // Modify the variables of the enemy character instance
        enemyCharacter.health = Random.Range(50, 80);
        enemyCharacter.attackPower = Random.Range(6, 8);
        enemyCharacter.defense = Random.Range(3, 5);

        while (health > 0 && enemyCharacter.health > 0)
        {

            int playerAttack = attackPower + Random.Range(-2, 2);
            int enemyAttack = enemyCharacter.attackPower + Random.Range(-2, 2);
            int playerDefense = defense + Random.Range(-2, 2);
            int enemyDefense = enemyCharacter.defense + Random.Range(-2, 2);

            // Player attacks the enemy
            enemyCharacter.health -= playerAttack - enemyDefense;

            Debug.Log("Player attacked enemy for: " + playerAttack);
            Debug.Log("Enemy defended for: " + enemyDefense);
            Debug.Log("Enemy health: " + enemyCharacter.health);

            if (enemyCharacter.health <= 0)
            {
                Debug.Log("Player wins!");
                break;
            }

            // Enemy attacks the player
            health -= enemyAttack - playerDefense;

            Debug.Log("Enemy attacked player for: " + enemyAttack);
            Debug.Log("Player defended for: " + playerDefense);
            Debug.Log("Player health: " + health);

            if (health <= 0)
            {
                Debug.Log("Enemy wins!");
                break;
            }
        }

    }
}
