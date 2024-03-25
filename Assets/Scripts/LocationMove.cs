using UnityEngine;

public class LocationMove : MonoBehaviour
{
    public GameObject map;
    private LocationManager locationManager;
    private double userLatitude;
    private double userLongitude;
    private Vector3 previousPosition;
    private float timeSinceLastUpdate = 0f;
    private const float updateInterval = 5f; // Update every 

    public TMPro.TextMeshProUGUI locationText;

    Vector3 newPosition = Vector3.zero;


    private void Start()
    {
        locationManager = map.GetComponent<LocationManager>();
        previousPosition = transform.position;
        
    }

    private void Update()
    {
        
        timeSinceLastUpdate += Time.deltaTime;

        if (timeSinceLastUpdate >= updateInterval)
        {
           
            userLatitude = locationManager.userLatitude;
            userLongitude = locationManager.userLongitude;

            newPosition = ConvertLatLonToUnityCoords(userLatitude, userLongitude);

            transform.position = newPosition;

            if (newPosition == previousPosition)
            {

                locationText.text = "Det lader til vi holder stille?";


            }
            locationText.text = newPosition.ToString();
            previousPosition = newPosition;
            timeSinceLastUpdate = 0f;
        }
    
        
    }

    private Vector3 ConvertLatLonToUnityCoords(double latitude, double longitude)
    {
        // Convert latitude and longitude to meters
        double latitudeInMeters = latitude * 111132;
        double longitudeInMeters = longitude * 111132 * Mathf.Cos((float)(Mathf.Deg2Rad * latitude));

        // Convert meters to Unity units using the map scale
        float x = (float)(longitudeInMeters);
        float y = (float)(latitudeInMeters);

        return new Vector3(x, y, 89.44f);
    }

}