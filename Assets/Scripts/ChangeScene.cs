using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //[SerializeField] private string nextSceneName;

    public void ChangeSceneToNext(string nextSceneName)
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
