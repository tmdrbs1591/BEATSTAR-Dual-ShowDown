using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSoundChange : MonoBehaviour
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
        if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FirstTheFirstStage)
        {
            audio.clip = songs[1];
        }
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FirstTheSecondStage)
        {
            audio.clip = songs[2];
        }
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FirstTheThirdStage)
        {
            audio.clip = songs[3];
        }
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FirstThefourthStage)
        {
            audio.clip = songs[4];
        }
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FirstThefifthStage)
        {
            audio.clip = songs[5];
        }
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FirstTheSixthStage)
        {
            audio.clip = songs[6];
        }
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FirstTheSeventhStage)
        {
            audio.clip = songs[7];  
        }
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FirstTheEighthStage)
        {
            audio.clip = songs[8];
        }


        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.SecondTheFirstStage)
        {
            audio.clip = songs[1];
        }
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.SecondTheSecondStage)
        {
            audio.clip = songs[2];
        }
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.SecondTheThirdStage)
        {
            audio.clip = songs[3];
        }
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.SecondThefourthStage)
        {
            audio.clip = songs[4];

        }
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.SecondThefifthStage)
        {
            audio.clip = songs[5];
        }
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.SecondTheSixthStage)
        {
            audio.clip = songs[6];
        }
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.SecondTheSeventhStage)
        {
            audio.clip = songs[7];
        }
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.SecondTheEighthStage)
        {
            audio.clip = songs[8];
        }

        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.ThirdTheFirstStage)
        {
            audio.clip = songs[1];
        }
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.ThirdTheSecondStage)
        {
            audio.clip = songs[2];
        }
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.ThirdTheThirdStage)
        {
            audio.clip = songs[3];
        }
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.ThirdThefourthStage)
        {
            audio.clip = songs[4];

        }
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.ThirdThefifthStage)
        {
            audio.clip = songs[5];
        }
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.ThirdTheSixthStage)
        {
            audio.clip = songs[6];
        }
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.ThirdTheSeventhStage)
        {
            audio.clip = songs[7];
        }
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.ThirdTheEighthStage)
        {
            audio.clip = songs[8];
        }
        else
        {
            PlaySong(0);
        }


        // 음악이 끝났는지 확인하고, 재생 중인 곡이 없으면 isPlaying 변수를 false로 변경
        if (!audio.isPlaying)
        {
            isPlaying = false;
        }
    }
}
