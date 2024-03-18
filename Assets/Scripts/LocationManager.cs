using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


public class LocationManager : MonoBehaviour
{
    private bool permission = false;
    private bool isLocationEnabled = false;
    private LocationInfo currentLocation;
    private double userLongitude;
    private double userLatitude;

    public Map map;

    // Default location (Example: Times Square, New York)
    private double defaultLongitude = -73.9851;
    private double defaultLatitude = 40.7580;

    public TMPro.TextMeshProUGUI locationText;

    private void Start()
    {

        // Check for Android permission
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.FineLocation))
        {
            locationText.text = "Location permission is not granted";
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.FineLocation);

        }


        map = GetComponent<Map>();

        StartCoroutine(AskForLocation());
    }

    private IEnumerator AskForLocation()
    {

       
        if (!Input.location.isEnabledByUser)
        {
           
            Debug.Log("Location services are not enabled");
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
            permission = true;
            isLocationEnabled = true;
            currentLocation = Input.location.lastData;
            userLongitude = currentLocation.longitude;
            userLatitude = currentLocation.latitude;
            map.UpdateBoundingBox(userLongitude, userLatitude);
        }

      
        yield break;
    }

    private void UseDefaultLocation()
    {
        Debug.Log("Using default location");
        permission = true;
        isLocationEnabled = true;
        userLongitude = defaultLongitude;
        userLatitude = defaultLatitude;
        map.UpdateBoundingBox(userLongitude, userLatitude);
    }
}
