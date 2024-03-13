using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//This script was developed with the help of Github Co-pilot.

//This script is attached to the projectile prefab. It launches the projectile from the player's location to the enemy's location. 

public class LaunchProjectile : MonoBehaviour
{
    // The attack method is called when the player character attacks the enemy character. The method launches the projectile from the player's location to the enemy's location.
    public void Attack(Transform enemyLocation)
    {
        // The force and duration of the projectile are set.
        int force = 250;
        double duration = 0.5;

        // The projectile is launched from the player's location to the enemy's location by using the projectile's rigidbody component.
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce((enemyLocation.position - transform.position).normalized * force);

        // The projectile is destroyed after a set duration.
        Destroy(gameObject, (float)duration);
    }
}