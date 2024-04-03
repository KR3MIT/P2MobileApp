using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSwipeRotation : MonoBehaviour
{
    public GameObject Ship;

    [SerializeField] private float damp = 1;
    [SerializeField] private float sensitivity = 1;
    private Vector2 startPressPos;
    private Vector2 endPressPos;
    private Vector2 swipe;

    // Update is called once per frame
    void Update()
    {
        //make ship rotate freely based on swipe velocity and direction
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startPressPos = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                endPressPos = touch.position;

                swipe = startPressPos - endPressPos;
            }
        }
        //make the ship rotate with ajusatble dampening and sentivity
        Ship.transform.Rotate(0, swipe.x * sensitivity * Time.deltaTime, 0);
        swipe = Vector2.Lerp(swipe, Vector2.zero, damp * Time.deltaTime);


    }
}
