using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;


public class CloudSave : MonoBehaviour
{
    private Character character;

    private int dataCount = 0;

    private Dictionary<string, object> saveData = new Dictionary<string, object>();

    [HideInInspector]public bool dataLoaded = false;

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
        character = Character.instance;
        saveData.Clear();

        Debug.Log("resources: " + character.resources[0]);

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
            Debug.Log($"{_keyName}: {keyName.Value.GetAs<string>()}");
        }
    }

    public async void LoadData(List<string> _keyNames)
    {
        HashSet<string> keyNamesHS = new HashSet<string>(_keyNames);
        var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(keyNamesHS);

        if (playerData.TryGetValue(playerData["shipParts"].Key, out var shipParts))
        {
            Debug.Log($"  {shipParts.Key}: {shipParts.Value.GetAs<List<ShipPart>>()}");
            Debug.Log("count " + shipParts.Value.GetAs<List<ShipPart>>().Count);
            character.CreateAndSetShipPart(shipParts.Value.GetAs<List<ShipPart>>());
        }

        if (playerData.TryGetValue(playerData["resources"].Key, out var resources))
        {
            Debug.Log($"  {resources.Key}: {resources.Value.GetAs<Dictionary<Character.ResourceType, int>>()}");
            character.resources = resources.Value.GetAs<Dictionary<Character.ResourceType, int>>();
        
        }

        if (playerData.TryGetValue(playerData["experience"].Key, out var exp))
        {
            Debug.Log($"  {exp.Key}: {exp.Value.GetAs<int>()}");
            character.exp = exp.Value.GetAs<int>();
        }

        if (playerData.TryGetValue(playerData["level"].Key, out var lv))
        {
            Debug.Log($"  {lv.Key}: {lv.Value.GetAs<int>()}");
            character.lvl = lv.Value.GetAs<int>();
        }

        if (playerData.TryGetValue(playerData["coal"].Key, out var coal))
        {
            Debug.Log($"  {coal.Key}: {coal.Value.GetAs<int>()}");
            character.coal = coal.Value.GetAs<int>();
        }

        if (playerData.TryGetValue(playerData["playerName"].Key, out var name))
        {
            Debug.Log($"  {name.Key}: {name.Value.GetAs<string>()}");
            character.playerName = name.Value.GetAs<string>();
        }


        if (!dataLoaded)//on first load go to home idk how else to do this because loadalldata is not async and i need to wait to change scene wjduiahwuidhauiwhduiawhjduiojawiodjoawi
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
            dataLoaded = true;
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
