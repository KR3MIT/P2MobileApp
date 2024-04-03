using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PageHorizontalScroller : MonoBehaviour
{
    public GameObject Ship;
    private Vector3 canvasStats, canvasHome, canvasUpgrades;
    private Vector3 pivotHome, pivotUpgrades, pivotStats;

    [SerializeField] private GameObject HomePivot, UpgradesPivot, StatsPivot;
    [SerializeField] private UnityEngine.UI.Button stats, home, upgrades;

    private Vector3 position;
    private float width;
    private float height;

    //public float scrollSpeed = 1;
    public float dragSnapSpeed = 1f;
    public float shipSnapSpeed = 1f;

    private bool buttonPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;

        canvasStats = transform.GetChild(0).position;
        canvasHome = transform.GetChild(1).position;
        canvasUpgrades = transform.GetChild(2).position;

        Ship.transform.position = HomePivot.transform.position;

        pivotHome = HomePivot.transform.position;
        pivotUpgrades = UpgradesPivot.transform.position;
        pivotStats = StatsPivot.transform.position;

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
        
        //when page is home, move ship to home pivot etc. but when page is stats, move ship to home pivot and make it smooth
        if (pageLocation == canvasHome)
        {
            StartCoroutine(LerpShipToPivot(pivotHome));
        }
        else if (pageLocation == canvasStats)
        {
            StartCoroutine(LerpShipToPivot(pivotUpgrades));
        }
        else
        {
            StartCoroutine(LerpShipToPivot(pivotStats));
        }

    }

    //smoothdamp ship to pivot

    private IEnumerator LerpShipToPivot(Vector3 pivotLocation)
    {
        while (Ship.transform.position != pivotLocation)
        {
            Ship.transform.position = Vector3.MoveTowards(Ship.transform.position, pivotLocation, shipSnapSpeed);
            yield return null;
        }
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

    //NO DRAGGING GRR!!!

    //private void ClosestPage()
    //{
    //    StopAllCoroutines();

    //    var closestPage = Vector3.zero;

    //    var homeDistance = Vector3.Distance(transform.position, canvasHome);
    //    var statsDistance = Vector3.Distance(transform.position, canvasStats);
    //    var upgradesDistance = Vector3.Distance(transform.position, canvasUpgrades);

    //    if (homeDistance <= statsDistance && homeDistance <= upgradesDistance)
    //    {
    //        closestPage = canvasHome;
    //    }
    //    else if (statsDistance < homeDistance && statsDistance < upgradesDistance)
    //    {
    //        closestPage = canvasStats;
    //    }
    //    else
    //    {
    //        closestPage = canvasUpgrades;
    //    }

    //    StartCoroutine(LerpLocation(closestPage));
    //}


    //void Update()
    //{
        
    //    if (buttonPressed)
    //    {
    //        return;
    //    }
    //    //Debug.Log(Vector3.Distance(transform.position, canvasUpgrades));
    //    if (Vector3.Distance(transform.position, canvasStats) < 1f)//need opposite canvas position since we move the entire thing
    //    {
    //        return;
    //    }

    //    if (Input.touchCount > 0)
    //    {
    //        UnityEngine.Touch touch = Input.touches[0];
    //        if (touch.phase == TouchPhase.Moved)
    //        {
    //            var x = touch.deltaPosition.x / (width * scrollSpeed);
    //            transform.position = new Vector3(transform.position.x + x, 0, 0);
    //        }
    //        else if (touch.phase == TouchPhase.Ended)
    //        {
    //            ClosestPage();
    //        }
    //    }
    //}
}
