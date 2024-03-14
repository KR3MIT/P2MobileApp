using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Friends;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using Unity.Services.Friends.Exceptions;
using Unity.Services.Friends.Models;

public class Friends : MonoBehaviour
{


    [SerializeField] private TMP_InputField addFriendNameInput;





    async void Start()
    {
        //await UnityServices.InitializeAsync();
        await FriendsService.Instance.InitializeAsync();

    }


    //var friends = await FriendsService.Instance.Friends; mega diller

    async void GetFriends()
    {
        //var friends = await FriendsService.Instance.Friends;
        foreach (var friend in friends)
        {
            Debug.Log(friend);
        }
    }



    async Task<bool> SendFriendRequest(string addFriendNameInput)
    {
        try
        {
            //We add the friend by name in this sample but you can also add a friend by ID using AddFriendAsync
            var relationship = await FriendsService.Instance.AddFriendByNameAsync(addFriendNameInput);
            Debug.Log($"Friend request sent to {addFriendNameInput}.");
            //If both players send friend request to each other, their relationship is changed to Friend.
            return relationship.Type is RelationshipType.FriendRequest or RelationshipType.Friend;
        }
        catch (FriendsServiceException e)
        {
            Debug.Log($"Failed to Request {addFriendNameInput} - {e}.");
            return false;
        }
    }




}
