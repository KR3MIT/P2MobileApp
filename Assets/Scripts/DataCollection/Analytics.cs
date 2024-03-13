using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;



// This class is responsible for initializing the Unity Services SDK and starting data collection.
public class Analytics : MonoBehaviour
{
    [SerializeField] private bool consentGiven = false; // A boolean to track whether the player has given consent to collect data.
    public GameObject consentUIPrefab; // A reference to a UI element that asks for consent.
    public bool _consentGiven = false; // A boolean to track whether the player has given consent to collect data.


    async void Start()
    {
        await UnityServices.InitializeAsync(); // Initialize the Unity Services SDK.
       
        if (_consentGiven == false)
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
       // Instantiate(consentUIPrefab); // Instantiate a UI element that asks for consent.

    }
    private void ConsentGiven()
    {
        AnalyticsService.Instance.StartDataCollection();
        _consentGiven = true;
        if (consentUIPrefab != null)
        {
            Destroy(consentUIPrefab);
        }
    }

    private void ConsentDenied()
    {
        Debug.Log("Consent denied. Data collection will not start.");
        if (consentUIPrefab != null)
        {
            Destroy(consentUIPrefab);
        }
    }
    private void RemoveConsent()
    {
        AnalyticsService.Instance.StopDataCollection();
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
