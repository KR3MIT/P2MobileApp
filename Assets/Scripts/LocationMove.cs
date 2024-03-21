using UnityEngine;

public class LocationMove : MonoBehaviour
{
    private GameObject map;
    private LocationManager locationManager;
    private double userLatitude;
    private double userLongitude;
    private Vector3 previousPosition;
    private float timeSinceLastUpdate = 0f;
    private const float updateInterval = 10f; // Update every 10 seconds
    

    private void Start()
    {
        locationManager = map.GetComponent<LocationManager>();
        map = GameObject.Find("Maps");
        previousPosition = transform.position;

    }

    private void Update()
    {
        timeSinceLastUpdate += Time.deltaTime;

        if (timeSinceLastUpdate >= updateInterval)
        {
            
            userLatitude = locationManager.userLatitude;
            userLongitude = locationManager.userLongitude;

            Vector3 newPosition = ConvertLatLonToUnityCoords(userLatitude, userLongitude);

            // Move the object smoothly from the previous position to the new position
            transform.position = Vector3.Lerp(previousPosition, newPosition, Time.deltaTime);

            previousPosition = newPosition;
            timeSinceLastUpdate = 0f;
        }
    }

    private Vector3 ConvertLatLonToUnityCoords(double latitude, double longitude)
    {
        // Implement this function based on your map projection and scaling
        // For now, let's just return a dummy value
        return new Vector3((float)longitude, 0, (float)latitude);
    }
}