using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LocationManager : MonoBehaviour
{
    private bool permission = false;
    private bool isLocationEnabled = false;
    private LocationInfo currentLocation;
    private double userLongitude;
    private double userLatitude;
    private double radiusInMeters = 1000; // Adjust for desired zoom level
    private double deltaLatitude;
    private double deltaLongitude;
    public Map map;

    // Default location (Example: Times Square, New York)
    private double defaultLongitude = -73.9851;
    private double defaultLatitude = 40.7580;

    private void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        // Check for Android permission
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.FineLocation))
        {
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.FineLocation);
        }
#endif

        map = GetComponent<Map>();

        StartCoroutine(AskForLocation());
    }

    private IEnumerator AskForLocation()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
    // Android-specific code
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
    }
#else
        // Use default or simulated location for non-Android platforms and Unity Editor
        UseDefaultLocation();
#endif

        // Common code for calculating bounding box, runs after platform-specific location retrieval
        deltaLatitude = radiusInMeters / (111132 * Mathf.Cos((float)(Mathf.Deg2Rad * (float)userLatitude)));
        deltaLongitude = radiusInMeters / (111320 * Mathf.Cos((float)(Mathf.Deg2Rad * (float)userLatitude)));
        map.BoundingBox[0] = userLongitude - deltaLongitude;
        map.BoundingBox[1] = userLatitude - deltaLatitude;
        map.BoundingBox[2] = userLongitude + deltaLongitude;
        map.BoundingBox[3] = userLatitude + deltaLatitude;
        yield break;
    }

    private void UseDefaultLocation()
    {
        Debug.Log("Using default location");
        permission = true;
        isLocationEnabled = true;
        userLongitude = defaultLongitude;
        userLatitude = defaultLatitude;
    }
}
