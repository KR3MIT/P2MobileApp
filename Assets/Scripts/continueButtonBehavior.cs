using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        
      // make a method that reloads current scene
      public void ReloadScene()
    {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

    
}
