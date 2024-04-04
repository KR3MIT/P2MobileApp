using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //[SerializeField] private string nextSceneName;

    public void ChangeSceneToNext(string nextSceneName)
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<SceneStates>();
            if(player.POIdict.Count == 0 && player.isEmbarked)
            {
                player.isEmbarked = false;
                SceneManager.LoadScene("Home");
                
                return;
            }
        }
            

        SceneManager.LoadScene(nextSceneName);
    }
}
