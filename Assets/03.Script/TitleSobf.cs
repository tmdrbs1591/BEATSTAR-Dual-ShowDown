using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSobf : MonoBehaviour
{
    public static TitleSobf instance;

    public AudioSource audio;

    [SerializeField] AudioClip settingSong;
    [SerializeField] AudioClip charSelectSong;
    [SerializeField] AudioClip titleSong;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        audio = GetComponent<AudioSource>(); // AudioSource 컴포넌트를 가져옴
    }

    public void SetMusicVolume(float volume)
    {
        audio.volume = volume;
    }
    public void SettingSongPlay()
    {
        if (audio.isPlaying)
        {
            audio.Stop(); // 현재 재생 중인 곡을 정지합니다.
        }

        audio.clip = settingSong; // 새로운 클립 설정
        audio.Play(); // 클립 재생
    }
    public void titleSongPlay()
    {
        if (audio.isPlaying)
        {
            audio.Stop(); // 현재 재생 중인 곡을 정지합니다.
        }

        audio.clip = titleSong; // 새로운 클립 설정
        audio.Play(); // 클립 재생
    }
}
