using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmbarkBehavior : MonoBehaviour
{
    public string SceneName;
 // this method changes into another scene after 1.5 secounds
 public void LoadEmbark()
    {
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
    }
}
