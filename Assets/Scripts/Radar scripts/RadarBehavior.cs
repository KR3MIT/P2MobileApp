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

    //resourcuacwawacaw
    [SerializeField] private GameObject resourceClickCanvas;
    private bool resourceCanvasActive = false;

    //player
    private Character player;

    //raycasthit
    RaycastHit hit;

    //cnahit
    private float maxXPos = .85f;

    private void Start()
    {
        if (debugMode)
        {
            shipInteractRadius = 1000f;
        }

        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("radarSound");
        }
    }
        player = GameObject.FindWithTag("Player").GetComponent<Character>();

        encounterClickCanvas = Instantiate(encounterClickCanvas, Vector3.zero, Quaternion.identity);//make an instance of prefab and save in its variable
        encounterClickCanvas.transform.GetComponentInChildren<Button>().onClick.AddListener(AttackButton);//add listener to button
        encounterClickCanvas.SetActive(false);//set it to false

        resourceClickCanvas = Instantiate(resourceClickCanvas, Vector3.zero, Quaternion.identity);//make an instance of prefab and save in its variable
        resourceClickCanvas.transform.GetComponentInChildren<Button>().onClick.AddListener(AttackButton);//add listener to button
        resourceClickCanvas.SetActive(false);//set it to false

        

        spawnPool.Add(encounterPOI);
        spawnPool.Add(resourcePOI);

        if(GameObject.FindWithTag("Player") != null)//check if player exists if it does set scenestate and if POIs exists set POIs
        {
            sceneStates = GameObject.FindWithTag("Player").GetComponent<SceneStates>();
            sceneStates.isEmbarked = true;
            if (sceneStates.POIdict.Count != 0)
            {
                
                foreach (KeyValuePair<stats, GameObject> kvp in sceneStates.POIdict)
                {
                    var poi = Instantiate(kvp.Value, kvp.Key.pos, instantiatedRotation);
                    poi.GetComponent<POIscript>().SetStats(kvp.Key.health, kvp.Key.ad, kvp.Key.def, kvp.Key.level);
                    POIs.Add(poi);

                    //if(kvp.Value.GetComponent<POIscript>().isEncounter)
                    //{
                    //    Debug.Log("javla from stats " + kvp.Key.level);
                    //    Debug.Log("javla when instantiated " + poi.GetComponent<POIscript>().level);
                    //}
                    
                    
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
        //if (debugMode)
        //{
        //    if (Input.GetMouseButtonDown(0))//just to test :):)):):)))
        //    {
        //        RaycastHit hit;//Make a raycasthit to store the information of the object that we hit
        //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Make a ray from the camera to the mouse position
        //        if (Physics.Raycast(ray, out hit))//If the ray hits something, and set the hit info
        //        {
        //            //If the tag of the hit object is "Prop", then do thing
        //            if (hit.transform.tag == "Prop")
        //            {
        //                POIs.Remove(hit.transform.gameObject);
        //                hit.transform.gameObject.SetActive(false);
        //                DisableOppositePOI(hit.transform.gameObject);
        //                var POIscript = hit.transform.gameObject.GetComponent<POIscript>();
        //                if (POIscript.isEncounter)
        //                {
        //                    if (sceneStates != null)
        //                    {
        //                        SaveToPOIs();
        //                    }

        //                    UnityEngine.SceneManagement.SceneManager.LoadScene(encounterSceneName);
        //                }
        //                else if (POIscript.isResource)
        //                {
        //                    if (sceneStates != null)
        //                    {
        //                        SaveToPOIs();
        //                    }

        //                    UnityEngine.SceneManagement.SceneManager.LoadScene(resourceSceneName);
        //                }
        //            }
        //        }
        //    }
        //}
        ////debug above real below

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == touchPhase)
        {
            if(encounterCanvasActive || resourceCanvasActive)
            {
                encounterCanvasActive = false;
                resourceCanvasActive = false;
                encounterClickCanvas.SetActive(false);
                resourceClickCanvas.SetActive(false);
            }
            
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position); //make a ray from the camera to the mouse position
            if (Physics.Raycast(ray, out hit))//If the ray hits something, and set the hit info
            {
                //If the tag of the hit object is "Prop", then do thing
                if (hit.transform.tag == "Prop")
                {

                    if (hit.transform.GetComponent<POIscript>().isEncounter)
                    {
                        EncounterClick(encounterClickCanvas, true);
                    }else if (hit.transform.GetComponent<POIscript>().isResource)
                    {
                        EncounterClick(resourceClickCanvas, false);
                    }
                }
            }
        }
    }

    private void AttackButton()//used for attack and resource
    {
        var poiScript = hit.transform.gameObject.GetComponent<POIscript>();

        POIs.Remove(hit.transform.gameObject);
        hit.transform.gameObject.SetActive(false);
        DisableOppositePOI(hit.transform.gameObject);


        sceneStates.SetPOIStats(poiScript.level, poiScript.health, poiScript.attackPower, poiScript.defensePower);
        if (sceneStates != null)
        {
            SaveToPOIs();
        }

        if (poiScript.isEncounter)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(encounterSceneName);
        }
        else if (poiScript.isResource)
        {
            Debug.Log("javla isresoucr attackbutton");
            UnityEngine.SceneManagement.SceneManager.LoadScene(resourceSceneName);
        }
    }

    private void EncounterClick(GameObject canvas, bool isEnounter)
    {
        canvas.SetActive(!canvas.activeSelf);//toggle canvas
        canvas.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y, 95);//set position of canvas
        if (canvas.transform.position.x > maxXPos)//no out of bounds canvas
        {
            canvas.transform.position = new Vector3(maxXPos, canvas.transform.position.y, 95);
        }
        else if (canvas.transform.position.x < -maxXPos)
        {
            canvas.transform.position = new Vector3(-maxXPos, canvas.transform.position.y, 95);
        }


        

        if (isEnounter)
        {
            encounterCanvasActive = canvas.activeSelf;
            canvas.transform.GetComponentInChildren<TMP_Text>().text = "Enemy lvl: " + hit.transform.gameObject.GetComponent<POIscript>().level;
        }
        else
        {
            resourceCanvasActive = canvas.activeSelf;
            canvas.transform.GetComponentInChildren<TMP_Text>().text = "Resource!!!";
        }
        

        var button = canvas.transform.GetComponentInChildren<Button>();
        if (Vector2.Distance(hit.transform.position, ship.transform.position) < shipInteractRadius && isEnounter)
        {
            canvas.transform.GetComponentInChildren<Button>().interactable = true;
            button.transform.GetChild(0).GetComponent<TMP_Text>().text = "Attack";
        }else if (Vector2.Distance(hit.transform.position, ship.transform.position) < shipInteractRadius && !isEnounter)
        {
            canvas.transform.GetComponentInChildren<Button>().interactable = true;
            button.transform.GetChild(0).GetComponent<TMP_Text>().text = "Go";
        }
        else
        {
            button.interactable = false;
            button.transform.GetChild(0).GetComponent<TMP_Text>().text = "Too far";

        }

        //if (!encounterCanvasActive)//maybe redundant  
        //{
        //    hit = new RaycastHit(); //reset hit data if close menu
        //}else if (!resourceCanvasActive)
        //{
        //    hit = new RaycastHit(); //reset hit data if close menu
        //}
    }

    //private void EncounterClick()
    //{
    //    encounterClickCanvas.SetActive(!encounterClickCanvas.activeSelf);//toggle canvas
    //    encounterClickCanvas.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y, 95);//set position of canvas
    //    if(encounterClickCanvas.transform.position.x > maxXPos)//no out of bounds canvas
    //    {
    //        encounterClickCanvas.transform.position = new Vector3(maxXPos, encounterClickCanvas.transform.position.y, 95);
    //    }else if(encounterClickCanvas.transform.position.x < -maxXPos)
    //    {
    //        encounterClickCanvas.transform.position = new Vector3(-maxXPos, encounterClickCanvas.transform.position.y, 95);
    //    }

    //    encounterClickCanvas.transform.GetComponentInChildren<TMP_Text>().text = "Enemy lvl: " + hit.transform.gameObject.GetComponent<POIscript>().level;

    //    encounterCanvasActive = encounterClickCanvas.activeSelf;

    //    var button = encounterClickCanvas.transform.GetComponentInChildren<Button>();
    //    if (Vector2.Distance(hit.transform.position, ship.transform.position) < shipInteractRadius)
    //    {
    //        encounterClickCanvas.transform.GetComponentInChildren<Button>().interactable = true;
    //        button.transform.GetChild(0).GetComponent<TMP_Text>().text = "Attack";
    //    }
    //    else
    //    {
    //        button.interactable = false;
    //        button.transform.GetChild(0).GetComponent<TMP_Text>().text = "Too far";

    //    }

    //    if(!encounterCanvasActive)
    //    {
    //        hit = new RaycastHit(); //reset hit data if close menu
    //    }
    //}

    private void SaveToPOIs()
    {
        foreach (GameObject POI in POIs)
        {
            var poiScript = POI.GetComponent<POIscript>();
            if (POI.GetComponent<POIscript>().isEncounter)
            {
                sceneStates.POIdict.Add(new stats(POI.transform.position, poiScript.level, poiScript.health, poiScript.attackPower, poiScript.defensePower), encounterPOI);
            }
            else
            {
                sceneStates.POIdict.Add(new stats(POI.transform.position, poiScript.level, poiScript.health, poiScript.attackPower, poiScript.defensePower), resourcePOI);
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
            var poiScript = poi.GetComponent<POIscript>();
            if (poiScript.isEncounter)
            {
                poiScript.RandomizeLevel(player);
            }
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
