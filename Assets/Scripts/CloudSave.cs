using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using System;


public class CloudSave : MonoBehaviour
{
    private Character character;

    private int dataCount = 0;

    private Dictionary<string, object> saveData = new Dictionary<string, object>();

    async void Start()
    {
        character = GetComponent<Character>();

        saveData.Add("playerName", character.playerName);
        saveData.Add("level", character.lvl);
        saveData.Add("experience", character.exp);
        saveData.Add("coal", character.coal);
        saveData.Add("resources", character.resources);
        saveData.Add("shipParts", character.shipParts);

        List<string> list = new List<string>();
        foreach (KeyValuePair<string, object> pair in saveData)
        {
            list.Add(pair.Key);
        }


        await UnityServices.InitializeAsync();

        LoadData(list);
       
    }

    //Saves player data
    public async void SaveData()
    {
        saveData.Clear();

        saveData.Add("playerName", character.playerName);
        saveData.Add("level", character.lvl);
        saveData.Add("experience", character.exp);
        saveData.Add("coal", character.coal);
        saveData.Add("resources", character.resources);
        saveData.Add("shipParts", character.shipParts);

        

        var result = await CloudSaveService.Instance.Data.Player.SaveAsync(saveData);
        Debug.Log($"Saved data {string.Join(',', saveData)}");
    }

    public async void LoadData(string _keyName)
    {
        var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { _keyName });
        if (playerData.TryGetValue(_keyName, out var keyName))
        {
            Debug.Log($"fuck {_keyName}: {keyName.Value.GetAs<string>()}");
        }
    }

    public async void LoadData(List<string> _keyNames)
    {
        HashSet<string> keyNamesHS = new HashSet<string>(_keyNames);
        var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(keyNamesHS);
        foreach(KeyValuePair<string, Unity.Services.CloudSave.Models.Item> kvp in playerData)
        {
            if (playerData.TryGetValue(kvp.Key, out var keyName))
            {
                Debug.Log($"fuck {keyName}: {keyName.Value.GetAs<string>()}");
            }
        }

    }


    private async void LoadAllData()
    {
        foreach (KeyValuePair<string, object> pair in saveData)
        {
            switch (pair.Key)
            {
                case "playerName":
                    LoadData(pair.Key);
                    break;
            }
        }
    }




    //Saves player data when the application is closed
    public void OnApplicationQuit()
    {
        SaveData();
    }

}
