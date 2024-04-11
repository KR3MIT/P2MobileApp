using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Account : MonoBehaviour
{
    [SerializeField] private GameObject signInDisplay;
    [SerializeField] private GameObject playerNameDisplay;
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private TMP_InputField playerNameInput;
    [SerializeField] private Button signUpButton;
    [SerializeField] private Button signInButton;
    [SerializeField] private TMP_Text passwordError;

    private bool signInActive = true;
    private string pass;
    private string userName;
    private bool passError = false;

    async void Start()
    {
        signUpButton.interactable = signInActive;
        signInButton.interactable = signInActive;

        passwordError.text = " ";

        pass = passwordInput.text;

        await UnityServices.InitializeAsync();
        //checkSignIn();
    }

    //New player SIGN UP with username and password
    public async void signUp()
    {
        pass = passwordInput.text;
        userName = usernameInput.text;

        if (SignUpCheck())
        {
            checkSignIn();

            string username = usernameInput.text;
            string password = passwordInput.text;
            await SignUpWithUsernamePassword(username, password);
            //await SignInWithUsernamePassword(username, password);

            playerNameDisplay.SetActive(true);
            signInDisplay.SetActive(false);
        }
        else if(!passError)
        {
            passwordError.text = "username or password error, try restarting the app";
        }
        else
        {
            passwordError.text = "password error, try restarting the app";
        }
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

    private bool SignUpCheck()
    {
        if(userName.Length < 8)
        {
            passError = true;
            return false; }

        if(pass.Length > 30 )
        {
            passError = true;
            return false; }

        bool hasUpper = CheckUpper(pass);
        if( !hasUpper)
        {
            passError = true;
            return false; }

        bool hasSymbol = CheckSymbol(pass);
        if( !hasSymbol)
        {
            passError = true;
            return false; }

        bool hasNumber = CheckNumber(pass);
        if( !hasNumber)
        {
            passError = true;
            return false; }

        bool checkSpaces = CheckSpaces(pass);
        if( !checkSpaces)
        {
            passError = true;
            return false; }

        bool checkSpacesName = CheckSpaces(userName);
        if( !checkSpaces)
        {
            passError = true;
            return false; }

        return true;
    }

    bool CheckSpaces(string pass)
    {
        foreach (char character in pass)
        {
            if (char.IsWhiteSpace(character))
            {
                return true; // Return true if a space is found
            }
        }

        return false; // Return false if no space is found
    }

    bool CheckUpper(string pass)
    {
        string[] words = pass.Split(' ');

        foreach (string word in words)
        {
            // Check if the word contains an uppercase letter
            foreach (char letter in word)
            {
                if (char.IsUpper(letter))
                {
                    return true; // Return true if an uppercase letter is found
                }
            }
        }

        return false; // Return false if no uppercase letter is found in any word
    }

    bool CheckSymbol(string pass)
    {
        string symbols = "!@#$%^&*()-_+=[]{}|;:',.<>?"; // Set of symbols

        foreach (char character in pass)
        {
            if (symbols.Contains(character.ToString()))
            {
                return true; // Return true if a symbol is found
            }
        }
        return false; // Return false if no symbol is found
    }

    bool CheckNumber(string pass)
    {
        string numbers = "1234567890"; // Set of symbols

        foreach (char character in pass)
        {
            if (numbers.Contains(character.ToString()))
            {
                return true; // Return true if a symbol is found
            }
        }
        return false; // Return false if no symbol is found
    }

    //Existing player SIGN IN with username and password
    public async void signIn()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;
        await SignInWithUsernamePassword(username, password);
        if (checkSignIn())
        {
            Character.instance.gameObject.GetComponent<CloudSave>().LoadAllData();//loads player data after login need to do it here and not onclick since we need to await the login
            //GetComponent<ChangeScene>().ChangeSceneToNext("Home");//go to home scene
        }
    }

    public async void signInDebug()
    {
        await SignInWithUsernamePassword("Matt", "Matt123!");
        //checkSignIn();
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
  public bool checkSignIn()
    {
        return AuthenticationService.Instance.IsSignedIn;
        //if (isSignedIn)
        //{
        //    signInDisplay.SetActive(false);
        //    playerNameDisplay.SetActive(true);
        //}
    }

    //Set player name
    public async void setPlayerName()
    {
        string playerName = playerNameInput.text;
        //await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName);
        //playerNameDisplay.SetActive(false);
        Debug.Log("Player name is set to " + playerName);
        //Access chached player name
        //string cachedPlayerName = AuthenticationService.Instance.PlayerName;
        //Debug.Log("Cached player name is " + cachedPlayerName);
        Character.instance.playerName = playerName;

    }
}
