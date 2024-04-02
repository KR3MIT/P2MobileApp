using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class RadarBehavior : MonoBehaviour
{
    public int amount; //Value for how many gameobjects that spawns.
    public List<GameObject> spawnPool; //A list that contains the gameobjects that we wish to spawn
    public GameObject Sphere; // Sphere is the gameobject that we wish to spawn props ontop of
    int randomItem = 0; // randomItem is set to 0
    GameObject toSpawn; // gameobjected toSpawn is created
    public bool StartScan = false;

    private List<GameObject> POIs = new List<GameObject>();
    [SerializeField] private float minDistance = 10f;
    private int maxRunAmount = 100; //max amount of times we can run the loop if we are too close to another object



    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;//Make a raycasthit to store the information of the object that we hit
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Make a ray from the camera to the mouse position
            if (Physics.Raycast(ray, out hit))//If the ray hits something, and set the hit info
            {
                //If the tag of the hit object is "Prop", then do thing
                if (hit.transform.tag == "Prop")
                {
                    hit.transform.gameObject.SetActive(false);
                    DisableOppositePOI(hit.transform.gameObject);
                }
            }
        }
    }

    private void DisableOppositePOI(GameObject hitPOI)
    {
        if(POIs.Count == 0 || POIs.Count == 1) { return;}

        //find the POI with the longest distance to hitPOI
        GameObject oppositePOI = null;
        float longestDistance = 0;
        foreach (GameObject POI in POIs)
        {
            if (POI == hitPOI) continue;
            float distance = Vector3.Distance(POI.transform.position, hitPOI.transform.position);
            if (distance > longestDistance)
            {
                longestDistance = distance;
                oppositePOI = POI;
            }
        }
        oppositePOI.SetActive(false);
    }

    // this method checks if objects with the tag prop collides, and destroy them
    public void spawnObjects() //we are making a new method called "spawnObjects"
    {
        MeshCollider radar = Sphere.GetComponent<MeshCollider>(); // map is the meshcollider of the sphere gameobject

        int runAmount = 0; // we set runAmount to 0 to ensure no infinite loop
        for (int i = 0; i < amount; i++)
        // i is set to 0 and increases with 1 every time its called and aslong as its true that "i" is smaller than "amountToSpawn", the "for" statement runs
        {
            randomItem = Random.Range(0, spawnPool.Count); // here we chose one random item from the spawnpool list
            toSpawn = spawnPool[randomItem]; // that random chosen gameobject is now set to "toSpawn"
            var randomCirclePosition = Random.insideUnitCircle * radar.bounds.extents; // this is a new vector2 that gets a random position inside the circle
            if(IsTooClose(randomCirclePosition)) // if the distance between the new position and the other positions is less than 1
                                                 // This can cause there to be spawned less than amount of objects since if they cant-
                                                 // -fit within maxRunAmount tries it will discontinue trying
            {
                runAmount++; // we increase runAmount by 1
                
                if (runAmount > maxRunAmount) // if runAmount is greater than 1000
                {
                    Debug.Log("Too close " + runAmount);

                    break; // we break the loop
                }
                i--; // we decrease i by 1
                continue; // and continue to the next iteration
            }
            POIs.Add(Instantiate(toSpawn, randomCirclePosition, Quaternion.Euler (new Vector3 (0,90,-90)))); // we instantiate the chosen prop, and add to list
        }
        
    }

    private bool IsTooClose(Vector3 pos)
    {
        if (POIs.Count == 0) return false; // if the list is empty, return false
        //check if the distance between the new position and the other positions is less than 1
        foreach (GameObject POI in POIs)
        {
            if (Vector3.Distance(pos, POI.transform.position) < minDistance)
            {
                return true;
            }
        }
        return false;
    }
        
        
    
}
