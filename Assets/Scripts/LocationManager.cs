using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


public class LocationManager : MonoBehaviour
{
  
    private bool canGetLocation = false;

    public LocationInfo currentLocation;
    public double userLongitude;
    public double userLatitude;

    public double originalLongitude;
    public double originalLatitude;

    public Map map;

    // Default location (Example: Times Square, New York)
    public double defaultLongitude = -73.9851;
    public double defaultLatitude = 40.7580;


    private void Start()
    {

        // Check for Android permission
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.FineLocation))
        {
           
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.FineLocation);
            canGetLocation = false;

        }
        else
        {
            canGetLocation = !Application.isEditor;
        }
    
        map = GetComponent<Map>();

        StartCoroutine(AskForLocation());
    }

    private IEnumerator AskForLocation()
    {


        if (!Input.location.isEnabledByUser)
        {

            UseDefaultLocation();
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            UseDefaultLocation();
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            UseDefaultLocation();
            yield break;
        }
        else
        {
           
            currentLocation = Input.location.lastData;
            userLongitude = currentLocation.longitude;
            userLatitude = currentLocation.latitude;

            originalLatitude = userLatitude;
            originalLongitude = userLongitude;

            map.UpdateBoundingBox(userLongitude, userLatitude);
        }


        yield break;
    }

    private void UseDefaultLocation()
    {
        Debug.Log("Using default location");
      
        userLongitude = defaultLongitude;
        userLatitude = defaultLatitude;
        originalLatitude = userLatitude;
        originalLongitude = userLongitude;
        map.UpdateBoundingBox(userLongitude, userLatitude);
    }

    void Update()
    {
        if (canGetLocation)
        {
            currentLocation = Input.location.lastData;
            userLongitude = currentLocation.longitude;
            userLatitude = currentLocation.latitude;
        }
        else
        {
            userLongitude = defaultLongitude;
            userLatitude = defaultLatitude;

        }

    }


}
