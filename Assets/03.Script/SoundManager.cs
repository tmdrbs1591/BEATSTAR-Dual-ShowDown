using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] songs;
    public AudioSource audio;
    private int currentSongIndex = -1; // 현재 재생 중인 곡의 인덱스
    private bool isPlaying = false; // 재생 중인지 여부를 나타내는 변수

    void Start()
    {
        audio = GetComponent<AudioSource>(); // AudioSource 컴포넌트를 가져옴
    }

    void PlaySong(int index)
    {
        // 이미 해당 곡이 재생 중이면 함수를 종료
        if (currentSongIndex == index && isPlaying)
            return;

   
        audio.clip = songs[index];
        audio.Play();
        currentSongIndex = index; // 현재 재생 중인 곡의 인덱스 업데이트
        isPlaying = true; // 재생 중임을 표시
    }

   public void SetMusicVolume(float volume)
    {
        audio.volume = volume;
    }
    void Update()
    {
        // 현재 스테이지에 따라 음악 재생
        if (StagerManager.instance.currentStage == StagerManager.Stage.FirstStage)
        {
            PlaySong(0);
        }
        else if (StagerManager.instance.currentStage == StagerManager.Stage.SecondStage)
        {
            PlaySong(1);
        }
        else if (StagerManager.instance.currentStage == StagerManager.Stage.ThirdStage)
        {
            PlaySong(2);
        }
        else if (StagerManager.instance.currentStage == StagerManager.Stage.fifthStage)
        {
            PlaySong(4);
        }
        else
        {
            PlaySong(3);
        }


        // 음악이 끝났는지 확인하고, 재생 중인 곡이 없으면 isPlaying 변수를 false로 변경
        if (!audio.isPlaying)
        {
            isPlaying = false;
        }
    }
}
