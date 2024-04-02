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

    private void Start()
    {
        AllowEmbark();
        Debug.Log(PlayerPrefs.GetString("previousDate"));
        Debug.Log(currentDate);
        AllowEmbark();

    }
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
        PlayerPrefs.SetString("previousDate", previousDate.ToString());
    }
    void Embark()
    {
        // start the scan from other script
        //load map
        //load player??
        
        Debug.Log("Embarking");
    }
   
}
