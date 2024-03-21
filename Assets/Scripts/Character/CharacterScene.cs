using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterScene : MonoBehaviour
{
    [TextArea]
    [Tooltip("Scenes in which the players visuals will be displayed ONLY UNITY SCENE ASSETS!!!!!!!!!!!!!!!!!!!!!!!!!!!!")]
    [SerializeField] private string important_note = "ONLY PUT UNITY SCENES IN LIST";
    [SerializeField] private List<Object> Scenes = new List<Object>();
    private List<string> _scenes = new List<string>();
    [SerializeField] private GameObject playerVisuals;

    void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;

        foreach(Object obj in Scenes)
        {
            _scenes.Add(obj.name);
        }
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

        if (_scenes.Contains(nextScene.name))
        {
            playerVisuals.SetActive(true);
        }else { playerVisuals.SetActive(false);}
    }
}
