using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    public static BackgroundMusicPlayer instance;
    public AudioSource source;

    public AudioClip clip;
    /// <summary>
    /// A Singleton music player used to play background music using AudioClip
    /// </summary>
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

    public void PlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip);


    }

    public void Start()
    {
        PlaySound(clip);
    }
}
