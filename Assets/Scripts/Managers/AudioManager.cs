using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    
    private AudioSource musicSource;
    private AudioSource soundSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
            AudioSource[] asources = GetComponents<AudioSource>();
            musicSource = asources[0];
            musicSource.loop = true;
            soundSource = asources[1];
        }
    }

    public void PlayMusic(AudioClip musicClip)
    {
        musicSource.clip = musicClip;
        musicSource.Play();
    }

    public void PlaySound(AudioClip soundClip)
    {
        soundSource.clip = soundClip;
        soundSource.Play();
    }
}
