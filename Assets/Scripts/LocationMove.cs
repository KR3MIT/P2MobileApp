using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LocationMove : MonoBehaviour
{
    public GameObject map;
    private LocationManager locationManager;
    private Statistics statistics;
    private double userLatitude;
    private double userLongitude;
    private Vector3 previousPosition;
    private float timeSinceLastUpdate = 0f;
    private const float updateInterval = 5f; // Update every 
    private float previousLongtitude;
    private float previousLatitude;

    private float rotationSpeed = 1f; // Adjust this value to change the speed of rotation

    private Vector3 velocity = Vector3.zero;
    [HideInInspector]public float totalDistance = 0f;

    [SerializeField] private TMP_Text distanceText;

    public float smoothTime = 1f;

    public float distianceMultiplier = 0.011f;

    public Vector3 newPosition = Vector3.zero;

    public static LocationMove instance;


    private void Start()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        newPosition = transform.position;
        locationManager = map.GetComponent<LocationManager>();
        GameObject statisticsObject = GameObject.Find("Character");
        if (statisticsObject != null)
        {
            statistics = statisticsObject.GetComponent<Statistics>();
        }
        previousPosition = transform.position;
        previousLatitude = (float)locationManager.userLatitude;
        previousLongtitude = (float)locationManager.userLongitude;

    }

    private void Update()
    {
        timeSinceLastUpdate += Time.deltaTime;

        if (timeSinceLastUpdate >= updateInterval)
        {
            Vector3 oldPosition = newPosition;
            newPosition = CalculatePositionFromGPS((float)locationManager.userLatitude, (float)locationManager.userLongitude, (float)locationManager.originalLatitude, (float)locationManager.originalLongitude); // Convert latitude and longitude to Unity coordinates
            //newPosition = CalculatePositionFromGPS((float)locationManager.userLatitude, (float)locationManager.userLongitude, (float)previousLatitude, (float)previousLongtitude);
            previousLatitude = (float)locationManager.userLatitude;
            previousLongtitude = (float)locationManager.userLongitude;
            Vector3 direction = newPosition - oldPosition;
            if (direction != Vector3.zero) // Avoid setting rotation if there's no movement
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(angle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }


            previousPosition = newPosition;// Update the previous position
            timeSinceLastUpdate = 0f; // Reset the timer
        }

        // Use SmoothDamp to gradually change the position of the object
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);

        //this checks if the object is present in a scene named "CombatEncounter" and if it is, it will disable the rendering of the object
        if (SceneManager.GetActiveScene().name == "CombatEncounterTest")
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }

    }

    Vector3 CalculatePositionFromGPS(float userLat, float userLon, float origLat, float origLon)
    {
        // Calculate the offset in meters (using Haversine formula or similar)
        float distance = CalculateDistance(origLat, origLon, userLat, userLon);
        // Calculate the distance from last logged position to new position
        CalculateTravelledDistance(previousLatitude, previousLongtitude, userLat, userLon);
        // Determine the direction - this is a simplified approach; for more accuracy, you might need a more complex calculation
        Vector3 direction = new Vector3(userLon - origLon, userLat - origLat, 0).normalized;

        // Convert distance to Unity units if necessary
        float unityDistance = distance * distianceMultiplier; // Assuming 1 meter = 1 Unity unit; adjust this based on your game's scale

        // Calculate the new position as an offset from the origin based on distance and direction
        Vector3 targetPosition = direction * unityDistance;
        targetPosition.z = 99.631f; // Assuming the map is flat; adjust this if you have a 3D map

        return targetPosition;



    }

    void CalculateTravelledDistance(float currentLat, float currentLon, float prevLat, float prevLon)
    {
        var R = 6378137; // Earth’s mean radius in meters
        var dLat = Mathf.Deg2Rad * (prevLat - currentLat);
        var dLong = Mathf.Deg2Rad * (prevLon - currentLon);

        var a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
                Mathf.Cos(Mathf.Deg2Rad * currentLat) * Mathf.Cos(Mathf.Deg2Rad * prevLat) *
                Mathf.Sin(dLong / 2) * Mathf.Sin(dLong / 2);
        var c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        var distance = R * c;
        if (distance > 200)
        {
         distance = 0;
         }
        distanceText.text = "Distance: " + distance.ToString("F2") + " meters";

        statistics.MetersWalkedPerMonth(distance); // Add the distance to the total distance walked

        totalDistance += distance; // Add the distance to the total distance walked
    }

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