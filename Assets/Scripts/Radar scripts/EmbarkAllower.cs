using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EmbarkAllower : MonoBehaviour
{   
    private bool embarkAllowed = false;
    DateTime currentDate;
    DateTime previousDate;
    public RadarBehavior radarBehavior;

   /* private void Start()
    {
        AllowEmbark();
        Debug.Log(PlayerPrefs.GetString("previousDate"));
        Debug.Log(currentDate);
        AllowEmbark();
        //this is proof it works :thumpsupemoji:

    } */
    //if date change, embarkallowed = true
    public void CheckEmbark()
    {
        currentDate = DateTime.Now.Date;
        previousDate = DateTime.Parse(PlayerPrefs.GetString("previousDate"));
        if (currentDate != previousDate)
        {
            embarkAllowed = true;
        }   
        
        if (!embarkAllowed)
        {
            Debug.Log("Embark not allowed");
            return;
        }
;       GoEmbark();
        previousDate = currentDate;
        embarkAllowed = false;
        PlayerPrefs.SetString("previousDate", previousDate.ToString());
     
    }
    void GoEmbark()
    {
        // start the scan from other script
        //load map
        //load player??
        radarBehavior.spawnObjects();
        
        Debug.Log("Embarking");
    }
   
}
