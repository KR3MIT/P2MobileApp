using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EmbarkAllower : MonoBehaviour
{   
    private bool embarkAllowed;
    DateTime currentDate;
    DateTime previousDate;
    public OuterRingScript outerRingScript;
    public RadarBehavior radarBehavior;
    public TextMeshProUGUI CountDownText;
    public bool DebugMode = false;

   private void Start()
    {
        
        StartCoroutine(PerSecond());
    } 
    //if date change, embarkallowed = true
    private void Update()
    {
        //if (!embarkAllowed)
        //{
        //    CountDown();

        //}
    }
    public void CheckEmbark()
    {
        if(DebugMode)
        {
            embarkAllowed = true;
            GoEmbark();
            return;
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
   // this method counts down untill 00:00
       public void CountDown()
    {
        TimeSpan timeLeft = DateTime.Now.Date.AddDays(1) - DateTime.Now;
        CountDownText.text = "Time left to next Embark: " + timeLeft.ToString(@"hh\:mm\:ss");
        
    }

    IEnumerator PerSecond()
    {
        currentDate = DateTime.Now.Date;
        previousDate = DateTime.Parse(PlayerPrefs.GetString("previousDate"));
        if (currentDate != previousDate)
        {
            embarkAllowed = true;
            CountDownText.enabled = false;
        }
        else
        {
            embarkAllowed = false;
            CountDownText.enabled = true;
        }

        if (!embarkAllowed)
        {
            CountDown();

        }
        yield return new WaitForSeconds(1);
        StartCoroutine(PerSecond());
    }
}
