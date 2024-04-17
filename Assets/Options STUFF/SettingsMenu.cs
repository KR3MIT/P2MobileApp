using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public static SettingsMenu instance;

    public AudioMixer audioMixer;

    [SerializeField] private Button resetPrefs;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button abandonButton;
    [SerializeField] private Button warningAbandon;
    [SerializeField] private Button warningReturn;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject warningPanel;


    private SceneStates player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void ToggleSettings()
    {
        if (player.isEmbarked)
        {
            abandonButton.gameObject.SetActive(true);
        }
        
        if (settingsPanel.activeSelf)
        {
            Debug.Log("Settings Panel is not active");
            settingsPanel.SetActive(false);
            settingsButton.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Settings Panel is active");
            settingsPanel.SetActive(true);
            settingsButton.gameObject.SetActive(false);
        }
    }

    void Abandon()
    {
        player.ClearData();
        player.SetEmbarked(false);
        abandonButton.gameObject.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("EmbarkEndScene");
        warningPanel.SetActive(false);
        settingsPanel.SetActive(false);
        settingsButton.gameObject.SetActive(true);
    }

    void WarningPopUp()
    {
        warningPanel.SetActive(true);
    }

    void Return()
    {
        warningPanel.SetActive(false);
    }

    private void Start()
    {
        abandonButton.gameObject.SetActive(false);

        player = GameObject.FindWithTag("Player").GetComponent<SceneStates>();

        settingsButton.onClick.AddListener(ToggleSettings);
        backButton.onClick.AddListener(ToggleSettings);
        abandonButton.onClick.AddListener(WarningPopUp);
        resetPrefs.onClick.AddListener(ResetPlayerPrefs);
        warningAbandon.onClick.AddListener(Abandon);
        warningReturn.onClick.AddListener(Return);
    }

    private void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //--------------AUDIO SETTINGS-------------\\

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }



}
