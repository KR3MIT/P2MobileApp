using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        var x = new Vector3(pageLocation.x, transform.position.y, transform.position.z);

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


    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Move the cube if the screen has the finger moving.
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 pos = touch.position;
                pos.x = (pos.x - width) / width;
                //pos.y = (pos.y - height) / height;
                position = new Vector3(-pos.x, 0.0f, 0.0f);

                // Position the cube.
                transform.position = position;
            }

        }
    }
}
