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

            case "TWmapCircle":
                if (TWmapCircle == 0)
                {
                    tutorialPanel2.SetActive(true);
                    Debug.Log("TWmapCircle");
                    PlayerPrefs.SetInt("TWmapCircle", 1);
                }
                break;

            case "THome":
                if (THome == 0)
                {
                    tutorialPanel3.SetActive(true);
                    Debug.Log("THome");
                    PlayerPrefs.SetInt("THome", 1);
                }
                break;

            case "TCombatEncounter":
                if (TCombatEncounter == 0)
                {
                    tutorialPanel4.SetActive(true);
                    Debug.Log("TCombatEncounter");
                    PlayerPrefs.SetInt("TCombatEncounter", 1);
                }
                break;

            case "TLootIsland":
                if (TLootIsland == 0)
                {
                    tutorialPanel5.SetActive(true);
                    Debug.Log("TLootIsland");
                    PlayerPrefs.SetInt("TLootIsland", 1);
                }
                break;
        }
    }
}
