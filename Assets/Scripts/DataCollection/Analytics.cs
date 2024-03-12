using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;



// This class is responsible for initializing the Unity Services SDK and starting data collection.
public class Analytics : MonoBehaviour
{
    [SerializeField] private bool consentGiven = false; // A boolean to track whether the player has given consent to collect data.


    async void Start()
    {
        await UnityServices.InitializeAsync(); // Initialize the Unity Services SDK.

        AskForConsent(); // Ask the player for consent to collect data.
    }

    void AskForConsent()
    {
        //show the player a UI element that asks for consent

       // Instantiate(consentUIPrefab); // Instantiate a UI element that asks for consent.

    }
    private void ConsentGiven()
    {
        AnalyticsService.Instance.StartDataCollection();
    }

    private void ConsentDenied()
    {
        Debug.Log("Consent denied. Data collection will not start.");
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
