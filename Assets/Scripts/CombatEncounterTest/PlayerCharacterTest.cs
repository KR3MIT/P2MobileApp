using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


//This script was developed with the help of Github Co-pilot.

public class PlayerCharacterTest : MonoBehaviour
{
    //player ref
    private Character player;

    // Reference to the enemy character instance and health bar instance
    public HealthBar healthBars;

    // Reference to the health bar and its text
    public TMP_Text playerHealthText;
    public Slider playerHealthBar;
    public TMP_Text enemyHealthText;
    public Slider enemyHealthBar;

    // Reference to the player and enemy locations, and the player and enemy bullet prefabs
    public GameObject playerLocation;
    public GameObject playerBulletPrefab;
    public GameObject enemyLocation;
    public GameObject enemyBulletPrefab;
    float bulletDelay = 0.5f;
    
    // Reference to the damage taken text and its location
    public GameObject enemyDamageTakenText;
    public GameObject playerDamageTakenText;
    public GameObject enemyDamageTakenTextLocation;
    public GameObject playerDamageTakenTextLocation;

    // Variables for the player character
    float playerMaxHealth = 100;
    float playerHealth = 100;
    int attackPower = 10;
    int defensePower = 5;

    // Variables for the enemy character
    int enemyAttackPower;
    int enemyDefensePower;
    float enemyMaxHealth;
    float enemyHealth;
    int xpToGive = 25;

    // win or lose bools and gameobjects >:(
   public bool lose = false;
   public bool win = false;
    public GameObject continueButton;
    public SpriteRenderer winImage;
    public SpriteRenderer loseImage;

    //private SoundManager soundManager;

    void Awake()
    {
        //soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        //soundManager.CombatMusicMixerVolume(combatMusicVol);
        //soundManager.StopMusic();
    }


    // Start is called before the first frame update
    void Start()
    {
        continueButton.SetActive(false);
        winImage.enabled = false;
        loseImage.enabled = false;
       
        if (GameObject.FindWithTag("Player").GetComponent<Character>() != null)
        {
            player = GameObject.FindWithTag("Player").GetComponent<Character>();
            playerMaxHealth = player.health;
            playerHealth = player.health;
            attackPower = player.AD;
            defensePower = player.def;
        }


        // Create an enemyCharacter instance through the EnemyCharacterTest class to handle the enemy character
        
        //EnemyCharacterTest enemyCharacter = new EnemyCharacterTest(); //commented out since you should not create a new instance of a monobehaviour class, maybe make it a struct??
        EnemyCharacterTest enemyCharacter = gameObject.AddComponent<EnemyCharacterTest>();

        // The enemy character's health, attack power, and defense are randomly generated within given ranges. enemyMaxHealth is set to the enemy's initial health.
        enemyDefensePower = enemyCharacter.defensePower = Random.Range(4, 6);
        enemyAttackPower = enemyCharacter.attackPower = Random.Range(8, 12);
        enemyHealth = enemyCharacter.health = Random.Range(80, 120);
        enemyMaxHealth = enemyHealth;

        InitiateCombat();
    }

    void InitiateCombat()
    {
        // Create an instance of the HealthBar class to handle the health bar and damage text
        //healthBars = new HealthBar();

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
            int enemyAttack = enemyAttackPower + Random.Range(-2, 2);
            int playerDefense = defensePower + Random.Range(-2, 2);
            int enemyDefense = enemyDefensePower + Random.Range(-2, 2);

            // PLAYER ATTACKS THE ENEMY
            // The enemy's health is reduced by the damage taken, which is the difference between the player's attack power and the enemy's defense.
            int enemyDamageTaken = playerAttack - enemyDefense;
            enemyDamageTaken = Mathf.Max(0, enemyDamageTaken);
            enemyHealth -= enemyDamageTaken;

           // FindObjectOfType<AudioManager>().Play("Canon");
            yield return new WaitForSeconds(0.5f);
            GameObject playerBullet = Instantiate(playerBulletPrefab, playerLocation.transform.position, Quaternion.identity/*, playerLocation.transform*/);
            playerBullet.GetComponent<LaunchProjectile>().Attack(enemyLocation.transform);
            

            yield return new WaitForSeconds(bulletDelay);

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
                player.AddResource(xpToGive + Random.Range(-5, 5));
                ContinueButton(true);
                break;
            }
            
            
            // ENEMY ATTACKS THE PLAYER
            // The player's health is reduced by the damage taken, which is the difference between the enemy's attack power and the player's defense.
            int playerDamageTaken = enemyAttack - playerDefense;
            playerDamageTaken = Mathf.Max(0, playerDamageTaken);
            playerHealth -= playerDamageTaken;

            //FindObjectOfType<AudioManager>().Play("Canon");
            yield return new WaitForSeconds(0.5f);
            GameObject enemyBullet = Instantiate(enemyBulletPrefab, enemyLocation.transform.position, Quaternion.identity/*, enemyLocation.transform*/);
            enemyBullet.GetComponent<LaunchProjectile>().Attack(playerLocation.transform);

            yield return new WaitForSeconds(bulletDelay);

            Debug.Log("Enemy attacked player for: " + enemyAttack);
            Debug.Log("Player defended for: " + playerDefense);
            Debug.Log("Player health: " + playerHealth);

            // The health bar is updated to reflect the player's new health value.
            healthBars.UpdateHealthBar(playerHealth, playerMaxHealth, playerHealthBar, playerHealthText);

            // A damage taken text is displayed above the player's health bar, showing the amount of damage taken. The text then moves upwards and is destroyed after a short time.
            GameObject playerDamageTakenObject = healthBars.DamageTakenText(playerDamageTakenText, playerDamageTakenTextLocation, playerDamageTaken);
            StartCoroutine(healthBars.MoveTextUpwardsAndDestroy(playerDamageTakenObject));
        }
        // If the player's health reaches 0 or below, the player is defeated.
        if (playerHealth < enemyHealth)
        {
            Debug.Log("Player has been defeated");
            ContinueButton(false);
        }
    }
    public void ContinueButton(bool isWin)
    {
        if (isWin)
        {
            winImage.enabled = true;
            FindObjectOfType<AudioManager>().Play("winningSound");
            //soundManager.WinOrLoseSound(isWin);// The Win sound is played through the SoundManager instance.
            continueButton.SetActive(true);
            continueButton.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Loot Island"));

        }
        else
        {
            loseImage.enabled = true;
            FindObjectOfType<AudioManager>().Play("losingSound");
            //soundManager.WinOrLoseSound(isWin); // The Lose sound is played through the SoundManager instance.
            continueButton.SetActive(true);
            continueButton.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("WMapCircle"));
        }
        //soundManager.PlayMusic();
        FindObjectOfType<AudioManager>().Play("music");
    }
}
