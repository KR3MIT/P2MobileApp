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

    private List<Vector3> POIPositions = new List<Vector3>();
    [SerializeField] private float minDistance = 10f;
    private int maxRunAmount = 100; //max amount of times we can run the loop if we are too close to another object



    //void Update()
    //{

    //    if (StartScan == true)
    //    {
    //        spawnObjects();
    //        StartScan = false;
    //    }

    //}
    //public void StartScanButton()
    //{
    //    StartScan = true;
    //}

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
            POIPositions.Add(Instantiate(toSpawn, randomCirclePosition, Quaternion.Euler (new Vector3 (0,90,-90))).transform.position); // we instantiate the chosen prop, and add to list
        }
        
    }

    private bool IsTooClose(Vector3 pos)
    {
        if (POIPositions.Count == 0) return false; // if the list is empty, return false
        //check if the distance between the new position and the other positions is less than 1
        foreach (Vector3 position in POIPositions)
        {
            if (Vector3.Distance(pos, position) < minDistance)
            {
                return true;
            }
        }
        return false;
    }
        
        
    
}
