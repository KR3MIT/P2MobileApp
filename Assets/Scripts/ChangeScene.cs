using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public TMP_InputField inputField;

    public void ChangeSceneToNext(string nextSceneName)
    {
        if(Character.instance != null)
        {
            if(Character.instance.TryGetComponent(out SceneStates sceneStates))//does not work right??
            {
                if(sceneStates.POIdict.Count == 0 && sceneStates.isEmbarked)
                {
                    sceneStates.SetEmbarked(false);
                    SceneManager.LoadScene("EmbarkEndScene");
                    return;
                }
            }
        }
            


        SceneManager.LoadScene(nextSceneName);
    }

    public void IsNotEmptyChangeScene(string nextSceneName)//for auth login
    {
        if (inputField.text != "")
        {
            Character.instance.GetComponent<CloudSave>().SaveData();
            ChangeSceneToNext(nextSceneName);
        }
    }
}
