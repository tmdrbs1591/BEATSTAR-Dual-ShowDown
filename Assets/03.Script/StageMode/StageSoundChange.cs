using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSoundChange : MonoBehaviour
{
    public AudioClip[] songs;
    public AudioSource audio;
    private int currentSongIndex = -1; // ���� ��� ���� ���� �ε���
    private bool isPlaying = false; // ��� ������ ���θ� ��Ÿ���� ����

    void Start()
    {
        audio = GetComponent<AudioSource>(); // AudioSource ������Ʈ�� ������
    }

    void PlaySong(int index)
    {
        // �̹� �ش� ���� ��� ���̸� �Լ��� ����
        if (currentSongIndex == index && isPlaying)
            return;


        audio.clip = songs[index];
        audio.Play();
        currentSongIndex = index; // ���� ��� ���� ���� �ε��� ������Ʈ
        isPlaying = true; // ��� ������ ǥ��
    }

    public void SetMusicVolume(float volume)
    {
        audio.volume = volume;
    }
    void Update()
    {
        // ���� ���������� ���� ���� ���
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


        // ������ �������� Ȯ���ϰ�, ��� ���� ���� ������ isPlaying ������ false�� ����
        if (!audio.isPlaying)
        {
            isPlaying = false;
        }
    }
}
