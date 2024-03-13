using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class continueButtonBehavior : MonoBehaviour
{
    //this script activates the continue button after the player has deactiaved all buttons
    public Button continueButton;
    public Button[] buttons;


    private void Start()
    {
        continueButton.interactable = false;
    }
    private void ActivateContinueButton()
    {
        if (buttons[0].interactable == false && buttons[1].interactable == false && buttons[2].interactable == false )
        {
           continueButton.interactable = true;
        }
    }
        void Update()
    {
        ActivateContinueButton();
    }

    //on click go to next scene
   /* public void Continue()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("");
    }*/


}
