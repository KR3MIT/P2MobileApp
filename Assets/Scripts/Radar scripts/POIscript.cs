using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIscript : MonoBehaviour
{
    public bool isEncounter = false;
    public bool isResource = false;
    void Start()
    {
        if(isEncounter == false && isResource == false)
        {
            Debug.LogError("POI is not set to be an encounter or a resource");
        }
    }
}
