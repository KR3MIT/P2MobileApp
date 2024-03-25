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
        var x = new Vector3(pageLocation.x, transform.position.y, 0);

        //make smooth transition between pages with lerp
        StartCoroutine(LerpLocation(x));
        
    }

    IEnumerator LerpLocation(Vector3 pageLocation)
    {
        while (transform.position != pageLocation)
        {
            transform.position = Vector3.Lerp(transform.position, pageLocation, 0.1f);
            yield return null;
        }
    }

    private void ClosestPage()
    {
        StopAllCoroutines();
        var dist = 100000f;//arbitrary large  number
        Vector3 closestPage = Vector3.zero;
        foreach (Transform child in transform)
        {
            if (Vector3.Distance(child.position, transform.position) < dist)
            {
                dist = Vector3.Distance(child.position, transform.position);
                closestPage = child.position;
            }
        }
       StartCoroutine(LerpLocation(closestPage));
    }


    void Update()
    {
        if (Input.touchCount > 0)
        {
            UnityEngine.Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log("touch moved");
                var x = touch.deltaPosition.x / (width * scrollSpeed);
                transform.position = new Vector3(transform.position.x + x, transform.position.y, 0);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("touch ended");
                StartCoroutine(LerpLocation(canvasHome));
            }
        }
    }
}
