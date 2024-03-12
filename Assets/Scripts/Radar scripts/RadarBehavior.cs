using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarBehavior : MonoBehaviour
{
    public int amount; //Value for how many gameobjects that spawns.
    public List<GameObject> spawnPool; //A list that contains the gameobjects that we wish to spawn
    public GameObject Sphere; // Quad is the gameobject that we wish to spawn props ontop of
    int randomItem = 0; // randomItem is set to 0
    GameObject toSpawn; // gameobjected toSpawn is created
    float screenX;
    float screenY;
    float screenZ;
    // 3 floats to hold the different axis values for our vector3 
    Vector3 pos; // a vector3 called pos
    void Start()
    {
        spawnObjects(); //this method is called once, when the game is started.
    }
    public void spawnObjects() //we are making a new method called "spawnObjects"
    {


        MeshCollider radar = Sphere.GetComponent<MeshCollider>(); // map is the meshcollider of the sphere gameobject

        for (int i = 0; i < amount; i++)
        // i is set to 0 and increases with 1 every time its called and aslong as its true that "i" is smaller than "amountToSpawn", the "for" statement runs
        {
            randomItem = Random.Range(0, spawnPool.Count); // here we chose one random item from the spawnpool list
            toSpawn = spawnPool[randomItem]; // that random chosen gameobject is now set to "toSpawn"
            screenX = Random.Range(radar.bounds.min.x, radar.bounds.max.x);// limits the area where the gameobjects can spawn on the x axis to be inside the bounds of the sphere 
            screenY = Random.Range(radar.bounds.min.y, radar.bounds.max.y);// limits the area where the gameobjects can spawn on the y axis to be inside the bounds of the  sphere
            screenZ = Random.Range(radar.bounds.min.z, radar.bounds.max.z);// limits the area where the gameobjects can spawn on the z axis to be inside the bounds of the sphere
            pos = new Vector3(screenX, screenY, screenZ); // pos now gets values, which is a new vector3, that contains the xyz map bounds 



            Instantiate(toSpawn, pos, toSpawn.transform.rotation); // we instantiate the chosen prop, with the new vector3 positition with a default rotation
        }

    } //important to credit the same guy from the p1 project for helping me with this script
}
