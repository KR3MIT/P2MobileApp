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
            if(TryGetComponent(out SceneStates sceneStates))
            {
                if(sceneStates.POIdict.Count == 0 && sceneStates.isEmbarked)
                {
                    sceneStates.isEmbarked = false;
                    SceneManager.LoadScene("Home");
                    return;
                }
            }
        }
            

        SceneManager.LoadScene(nextSceneName);
    }
}
