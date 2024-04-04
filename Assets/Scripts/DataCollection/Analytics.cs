using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;



// This class is responsible for initializing the Unity Services SDK and starting data collection.
public class Analytics : MonoBehaviour
{
    public GameObject consentUIPrefab; // A reference to a UI element that asks for consent.
    public int _consentGiven; 
   

    async void Start()
    {
        await UnityServices.InitializeAsync(); // Initialize the Unity Services SDK.
        _consentGiven = PlayerPrefs.GetInt("ConsentGiven");
        Debug.Log("Consentgiven on start: " + _consentGiven);
        if (_consentGiven == 0)
        {
            AskForConsent(); // Ask the player for consent to collect data.
        }
        else
        {
            ConsentGiven(); // Start data collection.
        }
      
        
    }
    
    // This method asks the player for consent to collect data.
    void AskForConsent()
    {
        consentUIPrefab.SetActive(true);
    }
    private void ConsentGiven()
    {
        AnalyticsService.Instance.StartDataCollection();
        _consentGiven = 1;
        PlayerPrefs.SetInt("ConsentGiven", _consentGiven);
        Debug.Log("Consentgiven after click: " + _consentGiven);
        consentUIPrefab.SetActive(false);
    }

    private void ConsentDenied()
    {
        Debug.Log("Consent denied. Data collection will not start.");
        _consentGiven = 0;
        PlayerPrefs.SetInt("ConsentGiven", _consentGiven);
        consentUIPrefab.SetActive(false);
    }
    private void RemoveConsent()
    {
        AnalyticsService.Instance.StopDataCollection();
        _consentGiven = 0;
        PlayerPrefs.SetInt("ConsentGiven", _consentGiven);
    }

    private void DeleteData()
    {
        AnalyticsService.Instance.RequestDataDeletion();
    }

    public void OnClickButtonOptIn()
    {
        ConsentGiven();
    }

    public void OnClickButtonYes()
    {
        ConsentGiven();
    }

    public void OnClickButtonNo()
    {
        ConsentDenied();
    }


}
