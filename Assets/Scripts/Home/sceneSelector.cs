using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class sceneSelector : MonoBehaviour
{
    [SerializeField] private Button _toggleScenes;
    [SerializeField] private GameObject _panel;

    public GameObject sceneButton;

    public List<string> sceneNames;

    // Start is called before the first frame update
    void Start()
    {
        _toggleScenes.onClick.AddListener(toggleScenes);
        _panel.SetActive(false);

        foreach (string scene in sceneNames)
        {
            GameObject _sceneButton = Instantiate(sceneButton, _panel.transform);
            _sceneButton.GetComponentInChildren<TMP_Text>().text = scene;
            _sceneButton.GetComponent<Button>().onClick.AddListener(() => LoadScene(scene));
            
        }
    }

    private void LoadScene(string scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }

    private void toggleScenes()
    {
        
        _panel.SetActive(!_panel.activeSelf);

    }

    //make a list of scenes that can be selected from the panel
    //when a scene is selected, load the scene
    


}
