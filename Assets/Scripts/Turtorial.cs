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
        TTtestFest = PlayerPrefs.GetInt("TTtestFest");
        TWmapCircle = PlayerPrefs.GetInt("TWmapCircle");
        THome = PlayerPrefs.GetInt("THome");
        TCombatEncounter = PlayerPrefs.GetInt("TCombatEncounter");
        TLootIsland = PlayerPrefs.GetInt("TLootIsland");

        tutorialPanel.SetActive(false);
        switch (SceneManager.GetActiveScene().name)
        {
            case "Home":
                if (THome == 0)
                {
                    tutorialPanel.SetActive(true);
                    Debug.Log("Home");
                    PlayerPrefs.SetInt("THome", 1);
                }
                break;

            case "WMapCircle":
                if (TWmapCircle == 0)
                {
                    tutorialPanel2.SetActive(true);
                    Debug.Log("WMapCircle");
                    PlayerPrefs.SetInt("TWmapCircle", 1);
                }
                break;

            case "Loot Island":
                if (TLootIsland == 0)
                {
                    tutorialPanel3.SetActive(true);
                    Debug.Log("Loot island");
                    PlayerPrefs.SetInt("TLootIsland", 1);
                }
                break;

            case "CombatEncounterTest":
                if (TCombatEncounter == 0)
                {
                    tutorialPanel4.SetActive(true);
                    Debug.Log("TCombatEncounter");
                    PlayerPrefs.SetInt("TCombatEncounter", 1);
                    Time.timeScale = 0;
                }
                break;

            case "TTestFest":
                if (TTtestFest == 0)
                {
                    tutorialPanel5.SetActive(true);
                    Debug.Log("Test Fest");
                    PlayerPrefs.SetInt("TTestFest", 1);
                }
                break;
        }
    }
    public void Resume()
    {
        Time.timeScale = 1;
    }
}
