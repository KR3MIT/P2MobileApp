using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

//This script was developed with the help of Github Co-pilot.

//This script is attached to the projectile prefab. It launches the projectile from the player's location to the enemy's location. 

public class LaunchProjectile : MonoBehaviour
{
    public AudioSource audioPlayer;
    // The attack method is called when the player character attacks the enemy character. The method launches the projectile from the player's location to the enemy's location.
    public void Start()
    {
        audioPlayer.pitch = audioPlayer.pitch + Random.Range(-0.3f, 0.3f);
    }
    public void Attack(Transform enemyLocation)
    {
        // The force and duration of the projectile are set.
        int force = 250;
        float duration = 0.5f;

        // The projectile is launched from the player's location to the enemy's location by using the projectile's rigidbody component.
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce((enemyLocation.position - transform.position).normalized * force);
        // The projectile is destroyed after a set duration.
        audioPlayer.Play();
        //Destroy(gameObject, (float)duration);
        Invoke("DisableVisuals", duration);
    }
    
        

    
    private void DisableVisuals()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }


   
    


}
