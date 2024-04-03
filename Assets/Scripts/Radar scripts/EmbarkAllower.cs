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
    public OuterRingScript outerRingScript;
    public RadarBehavior radarBehavior;

    public bool DebugMode = false;

   /* private void Start()
    {
        CheckEmbark();
        Debug.Log(PlayerPrefs.GetString("previousDate"));
        Debug.Log(currentDate);
        CheckEmbark();
        //this is proof it works :thumpsupemoji:

    } */
    //if date change, embarkallowed = true
    public void CheckEmbark()
    {
        if(DebugMode)
        {
            embarkAllowed = true;
            GoEmbark();
            return;
        }

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
        // to find where the previous date is stored go to Computer\HKEY_CURRENT_USER\Software\Unity\UnityEditor\Backseat-Sloppy\P2 MobileApp
    }
    void GoEmbark()
    {
        // start the scan from other script
        //load map
        //load player??
        radarBehavior.spawnObjects();
        outerRingScript.StartPulse();
        Debug.Log("Embarking");
    }
   
}
