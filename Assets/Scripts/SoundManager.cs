using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource winSound;
    [SerializeField] AudioSource loseSound;
    [SerializeField] AudioSource music;
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

    public void StopMusic()
    {
        // Stop the music
        music.Stop();
    }

    public void PlayMusic()
    {
        // Play the music
        music.Play();
    }

    public void CombatMusicMixerVolume(float volume)
    {
        // Set the volume of the music mixer for combat music
        mainMixer.SetFloat("MusicVolume", volume);
    }

    public void ResetMusicMixerVolume(float volume)
    {
        // Reset the volume of the music mixer back to player set volume
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
