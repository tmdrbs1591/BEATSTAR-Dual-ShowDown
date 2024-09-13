using System.Collections;
using UnityEngine;

public class StageModeSoundManager : MonoBehaviour
{
    public AudioClip[] songs;
    public AudioSource audio;
    private int currentSongIndex = -1; // 현재 재생 중인 곡의 인덱스
    private bool isPlaying = false; // 재생 중인지 여부를 나타내는 변수
    private float originalVolume; // 원래 볼륨 값 저장

    void Start()
    {
        audio = GetComponent<AudioSource>(); // AudioSource 컴포넌트를 가져옴
        originalVolume = audio.volume; // 원래 볼륨 값을 저장
    }

    void PlaySong(int index)
    {
        // 이미 해당 곡이 재생 중이면 함수를 종료
        if (currentSongIndex == index && isPlaying)
            return;

        // 코루틴을 사용하여 곡을 바꾸고 볼륨을 서서히 키움
        StartCoroutine(FadeInNewSong(index));
    }

    IEnumerator FadeInNewSong(int newIndex)
    {
        // 새로운 곡을 설정
        audio.clip = songs[newIndex];
        audio.Play();
        currentSongIndex = newIndex;
        isPlaying = true;

        // 볼륨을 0에서 원래 값으로 서서히 키움
        audio.volume = 0;
        for (float v = 0; v <= originalVolume; v += Time.deltaTime * 0.9f)
        {
            audio.volume = v;
            yield return null;
        }

        // 볼륨을 원래 값으로 설정
        audio.volume = originalVolume;
    }

    public void SetMusicVolume(float volume)
    {
        audio.volume = volume;
        originalVolume = volume;
    }

    void Update()
    {
        // 현재 스테이지에 따라 음악 재생
        if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FirstTheFirstStage) PlaySong(1);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FirstTheSecondStage) PlaySong(2);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FirstTheThirdStage) PlaySong(3);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FirstThefourthStage) PlaySong(4);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FirstThefifthStage) PlaySong(5);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FirstTheSixthStage) PlaySong(6);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FirstTheSeventhStage) PlaySong(7);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FirstTheEighthStage) PlaySong(8);


        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.SecondTheFirstStage) PlaySong(9);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.SecondTheSecondStage) PlaySong(10);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.SecondTheThirdStage) PlaySong(11);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.SecondThefourthStage) PlaySong(12);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.SecondThefifthStage) PlaySong(13);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.SecondTheSixthStage) PlaySong(14);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.SecondTheSeventhStage) PlaySong(15);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.SecondTheEighthStage) PlaySong(16);


        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.ThirdTheFirstStage) PlaySong(17);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.ThirdTheSecondStage) PlaySong(18);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.ThirdTheThirdStage) PlaySong(19);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.ThirdThefourthStage) PlaySong(20);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.ThirdThefifthStage) PlaySong(21);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.ThirdTheSixthStage) PlaySong(22);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.ThirdTheSeventhStage) PlaySong(23);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.ThirdTheEighthStage) PlaySong(24);
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
