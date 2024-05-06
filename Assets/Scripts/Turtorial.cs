using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Turtorial : MonoBehaviour
{
    public GameObject tutorialPanel;
    public GameObject tutorialPanel2;
    public GameObject tutorialPanel3;
    public GameObject tutorialPanel4;
    public GameObject tutorialPanel5;

    private int TTtestFest = 0;
    private int TWmapCircle = 0;
    private int THome = 0;
    private int TCombatEncounter = 0;
    private int TLootIsland = 0;

    void Start()
    {
        // here we set playerprefs to 0 if they havent already been opened and set them to 1 if they have been opened
        TTtestFest = PlayerPrefs.GetInt("TTtestFest");
        TWmapCircle = PlayerPrefs.GetInt("TWmapCircle");
        THome = PlayerPrefs.GetInt("THome");
        TCombatEncounter = PlayerPrefs.GetInt("TCombatEncounter");
        TLootIsland = PlayerPrefs.GetInt("TLootIsland");

        tutorialPanel.SetActive(false);
        // this switch statement checks which scene is currently active and opens the tutorial panel for that scene,
        // but only if the tutorial for that scene hasnt been opened yet / is set to 0
        switch (SceneManager.GetActiveScene().name)
        {
            case "Home":
                if (THome == 0)
                {
                    tutorialPanel.SetActive(true);
                    Debug.Log("Home");
                    // set the playerprefs to 1 so the tutorial panel doesnt open again
                    PlayerPrefs.SetInt("THome", 1);
                }
                break;

            case "WMapCircle":
                if (TWmapCircle == 0)
                {
                    tutorialPanel2.SetActive(true);
                    Debug.Log("WMapCircle");
                    // set the playerprefs to 1 so the tutorial panel doesnt open again
                    PlayerPrefs.SetInt("TWmapCircle", 1);
                }
                break;

            case "Loot Island":
                if (TLootIsland == 0)
                {
                    tutorialPanel3.SetActive(true);
                    Debug.Log("Loot island");
                    // set the playerprefs to 1 so the tutorial panel doesnt open again
                    PlayerPrefs.SetInt("TLootIsland", 1);
                }
                break;

            case "CombatEncounterTest":
                if (TCombatEncounter == 0)
                {
                    tutorialPanel4.SetActive(true);
                    Debug.Log("TCombatEncounter");
                    // set the playerprefs to 1 so the tutorial panel doesnt open again
                    PlayerPrefs.SetInt("TCombatEncounter", 1);
                    // timescale is set to 0 to pause the automatic combat scene. the player can then read the tutorial
                    // and press the continue button to resume the game with the Resume() method
                    Time.timeScale = 0;
                }
                break;

            case "TTestFest":
                if (TTtestFest == 0)
                {
                    tutorialPanel5.SetActive(true);
                    Debug.Log("Test Fest");
                    // set the playerprefs to 1 so the tutorial panel doesnt open again
                    PlayerPrefs.SetInt("TTestFest", 1);
                }
                break;
        }
    }
    // this method is called by the continue button in the tutorial panel to resume the game in combat encounter
    public void Resume()
    {
        Time.timeScale = 1;
    }
}
