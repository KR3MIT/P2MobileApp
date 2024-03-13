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
}
