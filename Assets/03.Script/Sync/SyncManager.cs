using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncManager : MonoBehaviour
{
    public static SyncManager instance;

    [SerializeField] AudioSource audioSource;
    [SerializeField] float BPM = 120f; // �뷡�� BPM ���� (��: 120 BPM)

    private List<float> timings = new List<float>(); // Ÿ�̹��� ������ ����Ʈ
    private int spacePressCount = 0; // �����̽� �� �Է� Ƚ�� ī��Ʈ
    public float averageInterval = 0.58f; // ���� ��� ������ ������ ����

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        audioSource.Stop();
    }

    void Update()
    {
        // �����̽� �ٸ� ������ ���� �뷡�� ��� �ð��� Ÿ�̹� ����Ʈ�� �߰�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (spacePressCount < 20) // 20�������� �Է� ����
            {
                float currentTime = audioSource.time; // ���� �뷡�� ��� �ð�
                timings.Add(currentTime); // Ÿ�̹� ����Ʈ�� �߰�
                Debug.Log("Timing saved: " + currentTime);
                spacePressCount++;

                if (timings.Count > 1)
                {
                    // ���� Ÿ�ְ̹� ���� Ÿ�̹� ������ ������ ���մϴ�.
                    float lastTiming = timings[timings.Count - 2]; // ���� Ÿ�̹�
                    float interval = currentTime - lastTiming; // ���� Ÿ�ְ̹� ���� Ÿ�̹� ������ ����
                    Debug.Log("Interval: " + interval);
                }

                if (spacePressCount == 20)
                {
                    averageInterval = CalculateAverageInterval();
                    Debug.Log("Maximum space presses reached. Stopping song.");
                    audioSource.Stop();
                    Debug.Log("Average Interval: " + averageInterval);
                }
            }
        }
    }

    // ����� Ÿ�ֿ̹� ���� �뷡 ���
    public void SyncStart()
    {
        audioSource.Play();
    }

    // Ÿ�̹� ���ݵ��� ����� ���ϴ� �޼���
    float CalculateAverageInterval()
    {
        float sum = 0f;
        for (int i = 1; i < timings.Count; i++)
        {
            float interval = timings[i] - timings[i - 1];
            sum += interval;
        }
        if (timings.Count > 1)
        {
            float averageInterval = sum / (timings.Count - 1);
            return averageInterval;
        }
        else
        {
            Debug.LogWarning("Not enough timings recorded to calculate average interval.");
            return 0f;
        }
    }

    // ��� ������ ��ȯ�ϴ� �޼���
    public float GetAverageInterval()
    {
        return averageInterval;
    }
}
