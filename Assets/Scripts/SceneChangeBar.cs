using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangeBar : MonoBehaviour
{
    private Button statButton;
    private Button upgradeButton;
    private Button homeButton;

    string statSceneName = "Stats";
    string homeSceneName = "Home";
    string upgradeSceneName = "Upgrade";

    private void Start()
    {
        statButton = transform.GetChild(0).GetChild(0).GetComponent<Button>();
        homeButton = transform.GetChild(0).GetChild(1).GetComponent<Button>();
        upgradeButton = transform.GetChild(0).GetChild(2).GetComponent<Button>();

        statButton.onClick.AddListener(() => ChangeScene(statSceneName));
        homeButton.onClick.AddListener(() => ChangeScene(homeSceneName));
        upgradeButton.onClick.AddListener(() => ChangeScene(upgradeSceneName));
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void OnDestroy()
    {
        statButton.onClick.RemoveAllListeners();
        homeButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.RemoveAllListeners();
    }
}
