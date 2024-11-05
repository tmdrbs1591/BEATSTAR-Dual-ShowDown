using System.Collections;
using UnityEngine;

public class StageModeSoundManager : MonoBehaviour
{
    public AudioClip[] songs;
    public AudioSource audio;
    private int currentSongIndex = -1; // ���� ��� ���� ���� �ε���
    private bool isPlaying = false; // ��� ������ ���θ� ��Ÿ���� ����
    private float originalVolume; // ���� ���� �� ����

    void Start()
    {
        audio = GetComponent<AudioSource>(); // AudioSource ������Ʈ�� ������
        originalVolume = audio.volume; // ���� ���� ���� ����
    }

    void PlaySong(int index)
    {
        // �̹� �ش� ���� ��� ���̸� �Լ��� ����
        if (currentSongIndex == index && isPlaying)
            return;

        // �ڷ�ƾ�� ����Ͽ� ���� �ٲٰ� ������ ������ Ű��
        StartCoroutine(FadeInNewSong(index));
    }

    IEnumerator FadeInNewSong(int newIndex)
    {
        // ���ο� ���� ����
        audio.clip = songs[newIndex];
        audio.Play();
        currentSongIndex = newIndex;
        isPlaying = true;

        // ������ 0���� ���� ������ ������ Ű��
        audio.volume = 0;
        for (float v = 0; v <= originalVolume; v += Time.deltaTime * 0.9f)
        {
            audio.volume = v;
            yield return null;
        }

        // ������ ���� ������ ����
        audio.volume = originalVolume;
    }

    public void SetMusicVolume(float volume)
    {
        audio.volume = volume;
        originalVolume = volume;
    }

    void Update()
    {
        // ���� ���������� ���� ���� ���
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


        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FourthTheFirstStage) PlaySong(25);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FourthTheSecondStage) PlaySong(26);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FourthTheThirdStage) PlaySong(27);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FourthThefourthStage) PlaySong(28);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FourthThefifthStage) PlaySong(29);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FourthTheSixthStage) PlaySong(30);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FourthTheSeventhStage) PlaySong(31);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FourthTheEighthStage) PlaySong(32);


        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FifthTheFirstStage) PlaySong(33);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FifthTheSecondStage) PlaySong(34);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FifthTheThirdStage) PlaySong(35);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FifthTheFourthStage) PlaySong(36);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FifthTheFifthStage) PlaySong(37);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FifthTheFSixthtage) PlaySong(38);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FifthTheSeventhStage) PlaySong(39);
        else if (StageModeStageManager.instance.currentStage == StageModeStageManager.Stage.FifthTheEighthStage) PlaySong(40);

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
