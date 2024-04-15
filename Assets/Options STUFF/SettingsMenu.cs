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
    [SerializeField] private Button AbandonButton;
    [SerializeField] private GameObject settingsPanel;
    
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
            AbandonButton.gameObject.SetActive(true);
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("EmbarkEndScene");
        settingsPanel.SetActive(false);
        settingsButton.gameObject.SetActive(true);
    }

    private void Start()
    {
        AbandonButton.gameObject.SetActive(false);

        player = GameObject.FindWithTag("Player").GetComponent<SceneStates>();

        settingsButton.onClick.AddListener(ToggleSettings);
        backButton.onClick.AddListener(ToggleSettings);
        AbandonButton.onClick.AddListener(Abandon);
        resetPrefs.onClick.AddListener(ResetPlayerPrefs);
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
