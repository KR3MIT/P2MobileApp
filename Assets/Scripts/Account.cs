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
    [SerializeField] private GameObject playerNameDisplay;
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private TMP_InputField playerNameInput;


    async void Start()
    {
        await UnityServices.InitializeAsync();
        checkSignIn();
    }

    //New player SIGN UP with username and password
    public async void signUp()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;
        await SignUpWithUsernamePassword(username, password);
        //await SignInWithUsernamePassword(username, password);
        checkSignIn();
        playerNameDisplay.SetActive(true);
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


    //Existing player SIGN IN with username and password
    public async void signIn()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;
        await SignInWithUsernamePassword(username, password);
        checkSignIn();
    }

    async Task SignInWithUsernamePassword(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            Debug.Log("SignIn is successful.");
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

    //Sign out
    public async void signOut()
    {
        //await AuthenticationService.Instance.SignOutAsync();
        signInDisplay.SetActive(true);
    }

  //Check if the player is signed in
  public void checkSignIn()
    {
        bool isSignedIn = AuthenticationService.Instance.IsSignedIn;
        if (isSignedIn)
        {
            signInDisplay.SetActive(false);
        }
    }

    //Set player name
    public async void setPlayerName()
    {
        string playerName = playerNameInput.text;
        await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName);
        playerNameDisplay.SetActive(false);
        Debug.Log("Player name is set to " + playerName);
        //Access chached player name
        string cachedPlayerName = AuthenticationService.Instance.PlayerName;
        Debug.Log("Cached player name is " + cachedPlayerName);

    }




}
