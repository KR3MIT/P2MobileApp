using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Friends;
using UnityEngine;

public class Friends : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        //await UnityServices.InitializeAsync();
        await FriendsService.Instance.InitializeAsync();

    }


    //var friends = await FriendsService.Instance.Friends;



}
