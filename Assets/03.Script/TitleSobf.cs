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
        audio = GetComponent<AudioSource>(); // AudioSource ������Ʈ�� ������
    }

    public void SetMusicVolume(float volume)
    {
        audio.volume = volume;
    }
    public void SettingSongPlay()
    {
        if (audio.isPlaying)
        {
            audio.Stop(); // ���� ��� ���� ���� �����մϴ�.
        }

        audio.clip = settingSong; // ���ο� Ŭ�� ����
        audio.Play(); // Ŭ�� ���
    }
    public void titleSongPlay()
    {
        if (audio.isPlaying)
        {
            audio.Stop(); // ���� ��� ���� ���� �����մϴ�.
        }

        audio.clip = titleSong; // ���ο� Ŭ�� ����
        audio.Play(); // Ŭ�� ���
    }
}
