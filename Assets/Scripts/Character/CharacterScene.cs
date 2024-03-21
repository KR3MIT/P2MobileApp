using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterScene : MonoBehaviour
{
    [Tooltip("Scenes in which the players visuals will be displayed")]
    public List<Scene> Scenes = new List<Scene>();
    public GameObject playerVisuals;

    void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    // Update is called once per frame
    private void ChangedActiveScene(Scene currentScene, Scene nextScene) 
    {
        string currentName = currentScene.name;

        if (currentName == null)
        {
            // Scene1 has been removed
            currentName = "Replaced";
        }

        Debug.Log($"CurrentScene: {currentName} NextScene: {nextScene.name}");

        if (Scenes.Contains(nextScene))
        {
            playerVisuals.SetActive(true);
        }else { playerVisuals.SetActive(false);}
    }
}
