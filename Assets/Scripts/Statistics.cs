using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;

// This script was developed with the help of Github Co-pilot.
public class Statistics : MonoBehaviour
{
    DateTime currentDate;
    DateTime previousDate;
    [HideInInspector] public int embarks;
    private float kilometersWalked;
    private float metersWalked;
    public TextMeshProUGUI embarksText;
    public TextMeshProUGUI kilometersWalkedText;

    // Start is called before the first frame update
    private void Start()
    {
        currentDate = DateTime.Now;
    }

    /// <summary>
    /// Adds an embark to the playerprefs, also resets the embarks if the month has changed
    /// </summary>
    public void AddEmbark()
    {
        currentDate = DateTime.Now;
        previousDate = DateTime.Parse(PlayerPrefs.GetString("previousDate"));
        if (currentDate.Month != previousDate.Month)
        {
            embarks = 1;
            PlayerPrefs.SetInt("embarks", embarks);
        }
        else
        {
            int embark;
            embark = PlayerPrefs.GetInt("embarks");
            embark++;
            PlayerPrefs.SetInt("embarks", embark);
        }

    }

    public void MetersWalkedPerMonth(float _metersWalked)
    {
        currentDate = DateTime.Now;
        previousDate = DateTime.Parse(PlayerPrefs.GetString("previousDate"));

        if (currentDate.Month != previousDate.Month)
        {
            metersWalked = _metersWalked;
            PlayerPrefs.SetFloat("metersWalked", metersWalked);
        }
        else
        {
            metersWalked = PlayerPrefs.GetFloat("metersWalked");
            metersWalked += _metersWalked;
            PlayerPrefs.SetFloat("metersWalked", metersWalked);
        }
    }

    private float ConvertMetersToKilometers(float _metersWalked)
    {
        _metersWalked = PlayerPrefs.GetFloat("metersWalked");
        _metersWalked = _metersWalked / 1000;
        return _metersWalked;
    }

    public void UpdateStatScreen()
    {
        embarks = PlayerPrefs.GetInt("embarks");
        metersWalked = PlayerPrefs.GetFloat("metersWalked");
        kilometersWalked = ConvertMetersToKilometers(metersWalked);

        // Update the UI
        embarksText.text = "Embarks this month: " + embarks;
        kilometersWalkedText.text = "Kilometers walked this month: " + kilometersWalked;

    }
}

