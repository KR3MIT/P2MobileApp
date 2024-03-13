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
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(ActivateContinueButton);
        }
    }
    public void ActivateContinueButton()
    {
        bool isButtonOn = false;
        foreach (Button button in buttons)
        {
            if (button.interactable)
                isButtonOn = true;
        }
        if (!isButtonOn)
        {
            continueButton.interactable = true;
        }
    }
        //on click go to next scene
        /* public void Continue()
         {
             UnityEngine.SceneManagement.SceneManager.LoadScene("");
         }*/

    
}
