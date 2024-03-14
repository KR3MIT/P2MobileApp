using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;


public class CloudSave : MonoBehaviour
{
    async void Start()
    {
        await UnityServices.InitializeAsync();
       
    }

    //Saves player data
    public async void SaveData()
    {
        var saveData = new Dictionary<string, object>
        {
            { "score", 100 },
            { "level", 5 }
        };
        var result = await CloudSaveService.Instance.Data.Player.SaveAsync(saveData);
        Debug.Log($"Saved data {string.Join(',', saveData)}");
    }

    public async void LoadData()
    {
        var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "level" });
        if (playerData.TryGetValue("level", out var keyName))
        {
            Debug.Log($"fuck keyName: {keyName.Value.GetAs<string>()}");
        }
    }




    //Saves player data when the application is closed
    public void OnApplicationQuit()
    {
        SaveData();
    }

}
