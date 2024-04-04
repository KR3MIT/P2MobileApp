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
    public TextMeshProUGUI CountDownText;
    public bool DebugMode = false;
    public string SceneName;

   private void Start()
    {
        
        StartCoroutine(PerSecond());
        
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
     
        outerRingScript.StartReversePulse();
        StartCoroutine(ChangeScene());
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
 

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
    }
}
