using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class Account : MonoBehaviour
{
    [SerializeField] private GameObject signInDisplay;
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;


    async void Start()
    {
        await UnityServices.InitializeAsync();
        bool isSignedIn = AuthenticationService.Instance.IsSignedIn;
        if (isSignedIn)
        {
            signInDisplay.SetActive(false);
        }

    }


    public async void create()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

    }

    async Task SignUpWithUsernamePassword(string username, string password)
    {   
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            Debug.Log("SignUp is successful.");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }



}
