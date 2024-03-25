using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
//this script rotates the object continiously on z axis
 float speed = 0.5f;
    void Update()
    {
        transform.Rotate(0, 0, -speed);
    }
}


