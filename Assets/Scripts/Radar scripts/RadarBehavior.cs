using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class RadarBehavior : MonoBehaviour
{
    public bool debugMode = false;

    public int amount; //Value for how many gameobjects that spawns.

    public Quaternion instantiatedRotation = Quaternion.Euler(new Vector3(0,90,-90));
    public Vector3 instantiatedOffset;
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

    //the touch phase used we use ended since we want the click yeye
    TouchPhase touchPhase = TouchPhase.Ended;

    //ref to ship
    public GameObject ship;
    //radius for ship interaction with poi
    [SerializeField] private float shipInteractRadius = 1f;

    //encounter click cancas
    [SerializeField] private GameObject encounterClickCanvas;
    private bool encounterCanvasActive = false;

    //player
    private Character player;

    //raycasthit
    RaycastHit hit;

    //cnahit
    private bool canAttack = false;

    private void Start()
    {
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Stop("radarSound"); //make me get 10000 errors
        }
        player = GameObject.FindWithTag("Player").GetComponent<Character>();

        encounterClickCanvas = Instantiate(encounterClickCanvas, Vector3.zero, Quaternion.identity);//make an instance of prefab and save in its variable
        encounterClickCanvas.transform.GetComponentInChildren<Button>().onClick.AddListener(AttackButton);//add listener to button
        encounterClickCanvas.SetActive(false);//set it to false
        

        spawnPool.Add(encounterPOI);
        spawnPool.Add(resourcePOI);

        if(GameObject.FindWithTag("Player") != null)//check if player exists if it does set scenestate and if POIs exists set POIs
        {
            sceneStates = GameObject.FindWithTag("Player").GetComponent<SceneStates>();
            sceneStates.isEmbarked = true;
            if (sceneStates.POIdict.Count != 0)
            {
                
                foreach (KeyValuePair<Vector3, GameObject> kvp in sceneStates.POIdict)
                {
                    POIs.Add(Instantiate(kvp.Value, kvp.Key, instantiatedRotation));
                    
                }
                GameObject.FindObjectOfType<OuterRingScript>().StartPulse();//ad
                sceneStates.POIdict.Clear();
            }else 
            {
                spawnObjects();
                GameObject.FindObjectOfType<OuterRingScript>().StartPulse();//ad
            }
        }
        else
        {
            Debug.LogWarning("Player not found");
        }
        
        
    }


    private void Update()
    {
        if (debugMode)
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
                        }
                        else if (POIscript.isResource)
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
        //debug above real below

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == touchPhase)
        {

            
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position); //make a ray from the camera to the mouse position
            if (Physics.Raycast(ray, out hit))//If the ray hits something, and set the hit info
            {
                //If the tag of the hit object is "Prop", then do thing
                if (hit.transform.tag == "Prop")
                {

                    if (hit.transform.GetComponent<POIscript>().isEncounter)
                    {
                        EncounterClick(new Vector3(hit.transform.position.x, hit.transform.position.y, 95));
                    }
                    

                    if (Vector2.Distance(hit.transform.position, ship.transform.position) < shipInteractRadius)//must be close to interact
                    {
                        Debug.Log("In range: " + Vector2.Distance(hit.transform.position, ship.transform.position));

                        

                        

                    }
                    else
                    {
                        Debug.Log("Out of range: " + Vector2.Distance(hit.transform.position, ship.transform.position));
                    }
                    
                    //UnityEngine.SceneManagement.SceneManager.LoadScene("EncounterScene");
                }
            }
        }
    }

    private void AttackButton()
    {
        var button = encounterClickCanvas.transform.GetComponentInChildren<Button>();
        if (!canAttack)
        {
            button.interactable = false;
            button.transform.GetChild(0).GetComponent<TMP_Text>().text = "Too far away";
            return;
        }
        else
        {
            encounterClickCanvas.transform.GetComponentInChildren<Button>().interactable = true;
            button.transform.GetChild(0).GetComponent<TMP_Text>().text = "Attack";
        }
        var poiScript = hit.transform.gameObject.GetComponent<POIscript>();
        

        POIs.Remove(hit.transform.gameObject);
        hit.transform.gameObject.SetActive(false);
        DisableOppositePOI(hit.transform.gameObject);


        sceneStates.SetPOIStats(poiScript.level, poiScript.health, poiScript.attackPower, poiScript.defensePower);
        if (sceneStates != null)
        {
            SaveToPOIs();
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(encounterSceneName);
    }

    private void EncounterClick(Vector3 position)
    {
        encounterClickCanvas.SetActive(!encounterClickCanvas.activeSelf);//toggle canvas
        encounterClickCanvas.transform.position = position;//set position of canvas
        encounterClickCanvas.transform.GetComponentInChildren<TMP_Text>().text = "Enemy lvl: " + hit.transform.gameObject.GetComponent<POIscript>().level;

        encounterCanvasActive = encounterClickCanvas.activeSelf;

        if (Vector2.Distance(hit.transform.position, ship.transform.position) < shipInteractRadius)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }

        if(!encounterCanvasActive)
        {
            hit = new RaycastHit(); //reset hit data if close menu
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
            GameObject poi = Instantiate(toSpawn, (Vector3)randomCirclePosition + instantiatedOffset, instantiatedRotation);
            poi.GetComponent<POIscript>().RandomizeLevel(player.lvl);
            POIs.Add(poi); // we instantiate the chosen prop, and add to list
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
