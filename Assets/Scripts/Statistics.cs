using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;

public class Statistics : MonoBehaviour
{
    DateTime currentDate;
    DateTime previousDate;
    [HideInInspector] public int embarks;
    private float kilometersWalked;
    private float metersWalked;
    public TextMeshProUGUI embarksText;
    public TextMeshProUGUI kilometersWalkedText;

    private void Start()
    {
        currentDate = DateTime.Now;
    }

    public void AddEmbark()
    {
        currentDate = DateTime.Now;
        string previousDateString = PlayerPrefs.GetString("previousDate");
        if (!string.IsNullOrEmpty(previousDateString))
        {
            previousDate = DateTime.Parse(previousDateString);
        }
        else
        {
            previousDate = DateTime.Now;
        }
        if (currentDate.Month != previousDate.Month)
        {
            embarks = 1;
            PlayerPrefs.SetInt("embarks", embarks);
        }
        else
        {
            int embark = PlayerPrefs.GetInt("embarks");
            embark++;
            PlayerPrefs.SetInt("embarks", embark);
        }
    }

    public void MetersWalkedPerMonth(float _metersWalked)
    {
        currentDate = DateTime.Now;
        string previousDateString = PlayerPrefs.GetString("previousDate");
        if (!string.IsNullOrEmpty(previousDateString))
        {
            previousDate = DateTime.Parse(previousDateString);
        }
        else
        {
            previousDate = DateTime.Now;
        }
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

        embarksText.text = embarks.ToString();
        kilometersWalkedText.text = kilometersWalked.ToString();
    }
}
