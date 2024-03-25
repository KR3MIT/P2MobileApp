using UnityEngine;

public class LocationMove : MonoBehaviour
{
    public GameObject map;
    private LocationManager locationManager;
    private double userLatitude;
    private double userLongitude;
    private Vector3 previousPosition;
    private float timeSinceLastUpdate = 0f;
    private const float updateInterval = 5f; // Update every 10 seconds

    public TMPro.TextMeshProUGUI locationText;

    private Map radiusGetter;

   

    private void Start()
    {
        locationManager = map.GetComponent<LocationManager>();
        radiusGetter = map.GetComponent<Map>();
        previousPosition = transform.position;
        
    }

    private void Update()
    {
        Vector3 newPosition = Vector3.zero;
        timeSinceLastUpdate += Time.deltaTime;

        if (timeSinceLastUpdate >= updateInterval)
        {
            if(userLatitude == 0 || userLongitude == 0)
            {
                return;
            }
            userLatitude = locationManager.userLatitude;
            userLongitude = locationManager.userLongitude;

            newPosition = ConvertLatLonToUnityCoords(userLatitude, userLongitude);

            transform.position = newPosition;

            if (newPosition == previousPosition)
            {

                locationText.text = "Det lader til vi holder stille?";


            }
            locationText.text = "Latitude: " + userLatitude + " Longitude: " + userLongitude;
            previousPosition = newPosition;
            timeSinceLastUpdate = 0f;
        }
        // Move the object smoothly from the previous position to the new position
        
    }

    private Vector3 ConvertLatLonToUnityCoords(double latitude, double longitude)
    {
        // Convert latitude and longitude to meters
        double latitudeInMeters = latitude * 111132;
        double longitudeInMeters = longitude * 111132 * Mathf.Cos((float)(Mathf.Deg2Rad * latitude));

        // Convert meters to Unity units using the map scale
        float x = (float)(longitudeInMeters / (100000 * 2));
        float y = (float)(latitudeInMeters / (100000 * 2));

        return new Vector3(x, y, 0);
    }

}