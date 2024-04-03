using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class RadarBehavior : MonoBehaviour
{
    public int amount; //Value for how many gameobjects that spawns.

    public Quaternion instantiatedRotation = Quaternion.Euler(new Vector3(0,90,-90));
    public GameObject encounterPOI;
    public GameObject resourcePOI;
    private List<GameObject> spawnPool = new List<GameObject>(); //A list that contains the gameobjects that we wish to spawn
    public GameObject Sphere; // Sphere is the gameobject that we wish to spawn props ontop of
    int randomItem = 0; // randomItem is set to 0
    GameObject toSpawn; // gameobjected toSpawn is created
    public bool StartScan = false;

    private List<GameObject> POIs = new List<GameObject>();
    [SerializeField] private float minDistance = .5f;
    private int maxRunAmount = 100; //max amount of times we can run the loop if we are too close to another object

    public string encounterSceneName = "EncounterScene";
    public string resourceSceneName = "ResourceScene";

    private SceneStates sceneStates;

    private void Start()
    {
        spawnPool.Add(encounterPOI);
        spawnPool.Add(resourcePOI);

        if(GameObject.FindWithTag("Player") != null)//check if player exists if it does set scenestate and if POIs exists set POIs
        {
            sceneStates = GameObject.FindWithTag("Player").GetComponent<SceneStates>();
            if (sceneStates.POIdict.Count != 0)
            {
                
                foreach (KeyValuePair<Vector3, GameObject> kvp in sceneStates.POIdict)
                {
                    POIs.Add(Instantiate(kvp.Value, kvp.Key, instantiatedRotation));
                    
                }
                GameObject.FindObjectOfType<OuterRingScript>().StartPulse();//ad
                sceneStates.POIdict.Clear();
            }
        }
        
        
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))//just to test :):)):):)))
        {
            RaycastHit hit;//Make a raycasthit to store the information of the object that we hit
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Make a ray from the camera to the mouse position
            if (Physics.Raycast(ray, out hit))//If the ray hits something, and set the hit info
            {
                //If the tag of the hit object is "Prop", then do thing
                if (hit.transform.tag == "Prop")
                {
                    POIs.Remove(hit.transform.gameObject);
                    hit.transform.gameObject.SetActive(false);
                    DisableOppositePOI(hit.transform.gameObject);
                    var POIscript = hit.transform.gameObject.GetComponent<POIscript>();
                    if (POIscript.isEncounter)
                    {
                        if (sceneStates != null)
                        {
                            SaveToPOIs();
                        }
                        
                        UnityEngine.SceneManagement.SceneManager.LoadScene(encounterSceneName);
                    }else if (POIscript.isResource)
                    {
                        if (sceneStates != null)
                        {
                            SaveToPOIs();
                        }

                        UnityEngine.SceneManagement.SceneManager.LoadScene(resourceSceneName);
                    }
                }
            }
        }
    }

    private void SaveToPOIs()
    {
        foreach (GameObject POI in POIs)
        {
            if (POI.GetComponent<POIscript>().isEncounter)
            {
                sceneStates.POIdict.Add(POI.transform.position, encounterPOI);
            }
            else
            {
                sceneStates.POIdict.Add(POI.transform.position, resourcePOI);
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
        POIs.Remove(oppositePOI);
        oppositePOI.SetActive(false);
        //Debug.Log("Removed 1 opp " + POIs.Count);
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
            POIs.Add(Instantiate(toSpawn, randomCirclePosition, instantiatedRotation)); // we instantiate the chosen prop, and add to list
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
