//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class CharacterScene : MonoBehaviour
//{
//    [Tooltip("Scenes in which the players visuals will be displayed")]
//    [SerializeField] private List<SceneAsset> SceneAssets = new List<SceneAsset>();
//    private List<string> _scenes = new List<string>();
//    [SerializeField] private GameObject playerVisuals;

    
//    void Start()
//    {
//        SceneManager.activeSceneChanged += ChangedActiveScene;

//        foreach(SceneAsset obj in SceneAssets)
//        {
//            _scenes.Add(obj.name);
//        }
//    }

//    private void ChangedActiveScene(Scene currentScene, Scene nextScene)
//    {
//        Debug.Log($"CurrentScene: {currentScene.name} NextScene: {nextScene.name}");

//        if (_scenes.Contains(nextScene.name))
//        {
//            playerVisuals.SetActive(true);
//        }
//        else { playerVisuals.SetActive(false); }
//    }
//}
