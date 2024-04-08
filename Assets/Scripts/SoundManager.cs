using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource winSound;
    [SerializeField] AudioSource loseSound;
    public static SoundManager instance;

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
