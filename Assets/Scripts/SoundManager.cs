using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource winSound;
    [SerializeField] AudioSource loseSound;
    public static SoundManager instance;
    public AudioMixer mainMixer;

    void Awake()
    {
        // Ensure there is only one instance of SoundManager
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

    public void CombatMusicMixerVolume(float volume)
    {


        // Set the volume of the music mixer
        mainMixer.SetFloat("MusicVolume", volume);
    }

    public void WinOrLoseSound(bool isWin)
    {
        if (isWin)
        {
            // Play win sound
            winSound.Play();
        }
        else
        {
            // Play lose sound
            loseSound.Play();
        }
    }

}
