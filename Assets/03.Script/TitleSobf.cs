using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSobf : MonoBehaviour
{
    public AudioClip songs;
    public AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>(); // AudioSource ÄÄÆ÷³ÍÆ®¸¦ °¡Á®¿È
    }

    public void SetMusicVolume(float volume)
    {
        audio.volume = volume;
    }

    void Update()
    {
        
    }
}
