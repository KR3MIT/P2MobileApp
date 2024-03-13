using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script is used to move the capsule in the scene, up and down sloowly, at a constant speed.
public class MoveCapsule : MonoBehaviour
{
    public float speed = 1.0f;
    public float distance = 1.0f;
    
    void Update()
    {
        float y = Mathf.PingPong(Time.time * speed, distance);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}
