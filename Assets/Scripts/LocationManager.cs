using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocationManeger : MonoBehaviour
    // this script is used to get the location of the user and update the bounding box of the map so that the map we generate is centered around the user
{
 // Firstly we must ask permission to use the androids location services
    private bool permission = false;
    private bool isLocationEnabled = false;
    private LocationInfo currentLocation;
    private double userLongitude;
    private double userLatitude;
    private double radiusInMeters = 1000; // Adjust for desired zoom level
    private double deltaLatitude;
    private double deltaLongitude;
    private double[] boundingBox = new double[] {0,0,0,0}; //[lon(min), lat(min), lon(max), lat(max)]
    public Map map;
    private void Start()
    {
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.FineLocation))
        {
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.FineLocation);
        }//this code is used to ask the user for permission to use the location services

        //the folowing code is used to get the location of the user and update the bounding box of the map so that the map we generate is centered around the user
        map = GetComponent<Map>();
        StartCoroutine(AskForLocation());// this is a coroutine that asks for the location of the user
    }
    private IEnumerator AskForLocation()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location services are not enabled");
            yield break;
        }
        // Start service before querying location
        Input.location.Start();
        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            yield break;
        }
        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            permission = true;
            isLocationEnabled = true;
            currentLocation = Input.location.lastData;
            userLongitude = currentLocation.longitude;
            userLatitude = currentLocation.latitude;
            deltaLatitude = radiusInMeters / (111132 * Mathf.Cos((float)(Mathf.Deg2Rad * (float)userLatitude)));
            deltaLongitude = radiusInMeters / (111132 * Mathf.Cos((float)(Mathf.Deg2Rad * (float)userLatitude)));
            boundingBox[0] = userLongitude - deltaLongitude;
            boundingBox[1] = userLatitude - deltaLatitude;
            boundingBox[2] = userLongitude + deltaLongitude;
            boundingBox[3] = userLatitude + deltaLatitude;
        }
    }

}
