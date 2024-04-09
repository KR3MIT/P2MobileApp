using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    DateTime currentDate;
    DateTime previousDate;
    [HideInInspector] public int embarks;
    private float kilometersWalked;

    // Start is called before the first frame update
    private void Start()
    {
        currentDate = DateTime.Now;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    PlayerPrefs.SetString("previousDate", previousDate.ToString());
    //}

    
    public void CountTimePlayed() 
    {
        TimeSpan timePlayed = currentDate - DateTime.Now;
        PlayerPrefs.SetString("timePlayed", timePlayed.ToString());
    }

    /// <summary>
    /// Adds an embark to the playerprefs, also resets the embarks if the month has changed
    /// </summary>
    public void AddEmbark() 
    {
        currentDate = DateTime.Now;
        previousDate = DateTime.Parse(PlayerPrefs.GetString("previousDate"));
        if (currentDate.Month != previousDate.Month)
        {
            embarks = 1;
            PlayerPrefs.SetInt("embarks", embarks);
        }
        else {
            int embark;
            embark = PlayerPrefs.GetInt("embarks");
            embark++;
            PlayerPrefs.SetInt("embarks", embark);
        }
        
    }

    /*
    public void EmbarksPerWeek()
    {
        
        
    }
    */

    public void EmbarksPerMonth()
    {
        embarks = PlayerPrefs.GetInt("embarks");
    }

    /// <summary>
    /// Saves the amount of meters walked to the playerprefs
    /// </summary>
    /// <param name="metersWalked"></param>
    public void SetMetersWalked(float metersWalked)
    {
        PlayerPrefs.SetFloat("kilometersWalked", metersWalked);
    }

    /*
    public void KilometersWalkedPerWeek()
    {
        kilometersWalked = PlayerPrefs.GetFloat("kilometersWalked");
    }
    */
    public void KilometersWalkedPerMonth()
    {

    }

    private void ConvertMetersToKilometers()
    {
        kilometersWalked = PlayerPrefs.GetFloat("kilometersWalked");
    }
    

    //public void CountDown()
    //{
    //    TimeSpan timeLeft = DateTime.Now.Date.AddDays(1) - DateTime.Now;
    //    CountDownText.text = "Time left to next Embark: " + timeLeft.ToString(@"hh\:mm\:ss");

    //}

    //IEnumerator PerSecond()
    //{
    //    currentDate = DateTime.Now.Date;
    //    previousDate = DateTime.Parse(PlayerPrefs.GetString("previousDate"));
    //    if (currentDate != previousDate)
    //    {
    //        embarkAllowed = true;
    //        CountDownText.enabled = false;
    //    }
    //    else
    //    {
    //        embarkAllowed = false;
    //        CountDownText.enabled = true;
    //    }

    //    if (!embarkAllowed)
    //    {
    //        CountDown();

    //    }
    //    yield return new WaitForSeconds(1);
    //    StartCoroutine(PerSecond());
    //}
}
