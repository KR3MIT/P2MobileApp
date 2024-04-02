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

    public float distianceMultiplier = 1f;

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

            newPosition = CalculatePositionFromGPS((float)locationManager.userLatitude, (float)locationManager.userLongitude, (float)locationManager.originalLatitude, (float)locationManager.originalLongitude); // Convert latitude and longitude to Unity coordinates

            locationText.text = " Current X: " + transform.position.x + " Current Y: " + transform.position.y;


            transform.position = newPosition; // Move the player to the new position

            if (newPosition == previousPosition) // Check if the player is moving
            {

                locationText.text = "Det lader til vi holder stille?";//"It seems like we are not moving?";


            }
            previousPosition = newPosition;// Update the previous position
            timeSinceLastUpdate = 0f; // Reset the timer



        }


    }

    Vector3 CalculatePositionFromGPS(float userLat, float userLon, float origLat, float origLon)
    {
        // Calculate the offset in meters (using Haversine formula or similar)
        float distance = CalculateDistance(origLat, origLon, userLat, userLon);
        // Determine the direction - this is a simplified approach; for more accuracy, you might need a more complex calculation
        Vector3 direction = new Vector3(userLon - origLon, userLat - origLat, 0 ).normalized;

        // Convert distance to Unity units if necessary
        float unityDistance = distance * distianceMultiplier; // Assuming 1 meter = 1 Unity unit; adjust this based on your game's scale

        // Calculate the new position as an offset from the origin based on distance and direction
        Vector3 newPosition = direction * unityDistance;
        newPosition.z = 99.631f; // Assuming the map is flat; adjust this if you have a 3D map
        return newPosition;


        float CalculateDistance(float lat1, float lon1, float lat2, float lon2)
        {
            var R = 6378137; // Earth’s mean radius in meters
            var dLat = Mathf.Deg2Rad * (lat2 - lat1);
            var dLong = Mathf.Deg2Rad * (lon2 - lon1);

            var a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
                    Mathf.Cos(Mathf.Deg2Rad * lat1) * Mathf.Cos(Mathf.Deg2Rad * lat2) *
                    Mathf.Sin(dLong / 2) * Mathf.Sin(dLong / 2);
            var c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
            var distance = R * c;

            return distance; // Distance in meters
        }

    }
}