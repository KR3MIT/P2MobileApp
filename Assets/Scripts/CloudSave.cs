using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.VisualScripting;


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
        //saveData.Add("shipParts", character.shipParts);
        saveData.Add("shipParts", character.shipPartList);



        await UnityServices.InitializeAsync();

        
       
    }



    public void LoadAllData()
    {
        List<string> list = new List<string>();
        foreach (KeyValuePair<string, object> pair in saveData)
        {
            list.Add(pair.Key);
        }

        LoadData(list);
    }

    //Saves player data
    public async void SaveData()
    {
        character = GetComponent<Character>();//maybe remove this line
        saveData.Clear();

        Debug.Log("fuck resources: " + character.resources[0]);

        saveData.Add("playerName", character.playerName);
        saveData.Add("level", character.lvl);
        saveData.Add("experience", character.exp);
        saveData.Add("coal", character.coal);
        saveData.Add("resources", character.resources);
        //saveData.Add("shipParts", character.shipParts);
        saveData.Add("shipParts", character.shipPartList);
        

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

        //if (playerData.TryGetValue(playerData["shipParts"].Key, out var playerName))
        //{
        //    Debug.Log($"fuck {playerName.Key}: {playerName.Value.GetAs<List<ShipPartObject>>()}");
        //    Debug.Log("count " + playerName.Value.GetAs<List<ShipPartObject>>().Count);
        //    character.SetShipParts(playerName.Value.GetAs<List<ShipPartObject>>());
        //}

        if (playerData.TryGetValue(playerData["shipParts"].Key, out var shipParts))
        {
            Debug.Log($"fuck {shipParts.Key}: {shipParts.Value.GetAs<List<ShipPart>>()}");
            Debug.Log("count " + shipParts.Value.GetAs<List<ShipPart>>().Count);
            character.CreateAndSetShipPart(shipParts.Value.GetAs<List<ShipPart>>());
        }

        if (playerData.TryGetValue(playerData["resources"].Key, out var resources))
        {
            Debug.Log($"fuck {resources.Key}: {resources.Value.GetAs<Dictionary<Character.ResourceType, int>>()}");
            character.resources = resources.Value.GetAs<Dictionary<Character.ResourceType, int>>();
        
        }

        if (playerData.TryGetValue(playerData["experience"].Key, out var exp))
        {
            Debug.Log($"fuck {exp.Key}: {exp.Value.GetAs<int>()}");
            character.exp = exp.Value.GetAs<int>();
        }

        if (playerData.TryGetValue(playerData["level"].Key, out var lv))
        {
            Debug.Log($"fuck {lv.Key}: {lv.Value.GetAs<int>()}");
            character.lvl = lv.Value.GetAs<int>();
        }

        if (playerData.TryGetValue(playerData["coal"].Key, out var coal))
        {
            Debug.Log($"fuck {coal.Key}: {coal.Value.GetAs<int>()}");
            character.coal = coal.Value.GetAs<int>();
        }

        if (playerData.TryGetValue(playerData["playerName"].Key, out var name))
        {
            Debug.Log($"fuck {name.Key}: {name.Value.GetAs<string>()}");
            character.playerName = name.Value.GetAs<string>();
        }

    }


    //Saves player data when the application is closed
    public void OnApplicationQuit()
    {
        SaveData();
    }

    public void OnDisable()
    {
        SaveData();
    }

}
