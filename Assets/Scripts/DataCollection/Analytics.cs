using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;



// This class is responsible for initializing the Unity Services SDK and starting data collection.
public class Analytics : MonoBehaviour
{
    [SerializeField] private bool consentGiven = false; // A boolean to track whether the player has given consent to collect data.
    public GameObject consentUIPrefab; // A reference to a UI element that asks for consent.
    public int _consentGiven = 0; 
   

    async void Start()
    {
        await UnityServices.InitializeAsync(); // Initialize the Unity Services SDK.
       PlayerPrefs.GetInt("ConsentGiven", _consentGiven);
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
        consentUIPrefab.SetActive(false);
    }

    private void ConsentDenied()
    {
        Debug.Log("Consent denied. Data collection will not start.");
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
