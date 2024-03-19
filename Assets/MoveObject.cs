using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float moveSpeed = 1.0f; // Adjust movement speed

    void Update()
    {
        // Get horizontal input (A/D or left/right arrow keys)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Get vertical input (W/S or up/down arrow keys)
        float verticalInput = Input.GetAxis("Vertical");


        Debug.Log("Horizontal: " + horizontalInput + " Vertical: " + verticalInput);
        // Combine inputs into a movement vector
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0.0f);

        // Move the object by the movement vector scaled by speed and time
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
