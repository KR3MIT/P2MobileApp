using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmbarkAllower : MonoBehaviour
{
    private bool embarkAllowed = false;
    DateTime currentDate;
    DateTime previousDate;
 
    //if date change, embarkallowed = true
    public void AllowEmbark()
    {
        currentDate = DateTime.Now.Date;
        if (currentDate != previousDate)
        {
            embarkAllowed = true;
        }   
        
        if (!embarkAllowed)
        {
            Debug.Log("Embark not allowed");
            return;
        }
;       Embark();
        previousDate = currentDate;
        embarkAllowed = false;
    }
    void Embark()
    {
        // start the scan from other script
        //load map
        //load player??
        Debug.Log("Embarking");
    }
   
}
