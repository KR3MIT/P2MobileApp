using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        tutorialPanel.SetActive(false);
        switch (Application.loadedLevelName)
        {
            case "TestFest":
               if (TTtestFest == 0)
                {
                    tutorialPanel.SetActive(true);
                    Debug.Log("TestFest");
                    PlayerPrefs.SetInt("TTtestFest", 1);
                }
                break;

            case "Level2":
                if (TWmapCircle == 0)
                {
                    tutorialPanel2.SetActive(true);
                    Debug.Log("Level2");
                    PlayerPrefs.SetInt("TWmapCircle", 1);
                }
                break;

            case "Level3":
                if (THome == 0)
                {
                    tutorialPanel3.SetActive(true);
                    Debug.Log("Level3");
                    PlayerPrefs.SetInt("THome", 1);
                }
                break;

            case "Level4":
                if (TCombatEncounter == 0)
                {
                    tutorialPanel4.SetActive(true);
                    Debug.Log("Level4");
                    PlayerPrefs.SetInt("TCombatEncounter", 1);
                }
                break;

            case "Level5":
                if (TLootIsland == 0)
                {
                    tutorialPanel5.SetActive(true);
                    Debug.Log("Level5");
                    PlayerPrefs.SetInt("TLootIsland", 1);
                }
                break;
        }
    }
}
