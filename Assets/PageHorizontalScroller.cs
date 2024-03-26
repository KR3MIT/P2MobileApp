using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PageHorizontalScroller : MonoBehaviour
{
    private Vector3 canvasStats, canvasHome, canvasUpgrades;

    [SerializeField] private UnityEngine.UI.Button stats;
    [SerializeField] private UnityEngine.UI.Button home;
    [SerializeField] private UnityEngine.UI.Button upgrades;

    private Vector3 position;
    private float width;
    private float height;

    public float scrollSpeed = 1;
    public float dragSnapSpeed = 10f;

    private bool buttonPressed = false;


    // Start is called before the first frame update
    void Start()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;

        // Position used for the cube.
        position = new Vector3(0.0f, 0.0f, 0.0f);

        canvasStats = transform.GetChild(0).position;
        canvasHome = transform.GetChild(1).position;
        canvasUpgrades = transform.GetChild(2).position;

        stats.onClick.AddListener(() => ChangeToPage(canvasUpgrades));
        home.onClick.AddListener(() => ChangeToPage(canvasHome));
        upgrades.onClick.AddListener(() => ChangeToPage(canvasStats));

    }

    private void ChangeToPage(Vector3 pageLocation)
    {
        StopAllCoroutines();

        buttonPressed = true;

        var x = new Vector3(pageLocation.x, transform.position.y, 0);

        //make smooth transition between pages with lerp
        StartCoroutine(LerpLocation(x));
        
    }

    IEnumerator LerpLocation(Vector3 pageLocation)
    {
        while (transform.position != pageLocation)
        {
            transform.position = Vector3.MoveTowards(transform.position, pageLocation, dragSnapSpeed);
            yield return null;

            buttonPressed = false;
        }
    }

    private void ClosestPage()
    {
        StopAllCoroutines();

        var closestPage = Vector3.zero;

        var homeDistance = Vector3.Distance(transform.position, canvasHome);
        var statsDistance = Vector3.Distance(transform.position, canvasStats);
        var upgradesDistance = Vector3.Distance(transform.position, canvasUpgrades);

        if (homeDistance <= statsDistance && homeDistance <= upgradesDistance)
        {
            closestPage = canvasHome;
        }
        else if (statsDistance < homeDistance && statsDistance < upgradesDistance)
        {
            closestPage = canvasStats;
        }
        else
        {
            closestPage = canvasUpgrades;
        }

        StartCoroutine(LerpLocation(closestPage));
    }


    void Update()
    {
        
        if (buttonPressed)
        {
            return;
        }
        //Debug.Log(Vector3.Distance(transform.position, canvasUpgrades));
        if (Vector3.Distance(transform.position, canvasStats) < 1f)//need opposite canvas position since we move the entire thing
        {
            return;
        }

        if (Input.touchCount > 0)
        {
            UnityEngine.Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Moved)
            {
                var x = touch.deltaPosition.x / (width * scrollSpeed);
                transform.position = new Vector3(transform.position.x + x, 0, 0);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                ClosestPage();
            }
        }
    }
}
