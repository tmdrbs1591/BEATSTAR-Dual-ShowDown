using UnityEngine;

public class BeatStartTiming : MonoBehaviour
{
    public AudioSource audioSource; // ���� ����� AudioSource
    private bool isTiming = false; // ��ư�� ������ �����ϴ��� ����
    private float[] beatTimes = new float[20]; // 20���� ��ư ���� �ð� ����
    private int pressCount = 0; // ���� Ƚ��
    private float startTime; // ù ��° ��ư ���� �ð�
    private bool songStarted = false; // �뷡�� ���۵Ǿ����� ����

    void Update()
    {
        // 'Space' Ű�� ��Ʈ�� �����ٰ� ����
        if (Input.GetKeyDown(KeyCode.Space) && !songStarted)
        {
            if (!isTiming)
            {
                // ù ��° ��ư�� ������ Ÿ�̸� ����
                isTiming = true;
                startTime = Time.time;
                Debug.Log("First beat pressed.");
            }

            // ��ư ���� �ð��� ����
            beatTimes[pressCount] = Time.time - startTime;
            pressCount++;
            Debug.Log("Beat " + pressCount + " at: " + beatTimes[pressCount - 1] + " seconds.");

            // 20�� ������ ��� ���� ����
            if (pressCount == 20)
            {
                // ��� Ÿ�̹� ���
                float averageTime = CalculateAverageTime();
                Debug.Log("Average time between beats: " + averageTime + " seconds.");

                // ���� �ð� �Ŀ� ������ ����
                Invoke("StartSong", averageTime);
                songStarted = true;
            }
        }
    }

    // ��� Ÿ�̹��� ����ϴ� �Լ�
    float CalculateAverageTime()
    {
        float totalTime = 0f;
        for (int i = 1; i < pressCount; i++) // ù ��° ��Ʈ�� �����̹Ƿ� ����
        {
            totalTime += beatTimes[i] - beatTimes[i - 1];
        }
        return totalTime / (pressCount - 1); // ��Ʈ ������ ��հ� ��ȯ
    }

    void StartSong()
    {
        // ���� ����
        audioSource.Play();
        Debug.Log("Song started!");
    }
}
