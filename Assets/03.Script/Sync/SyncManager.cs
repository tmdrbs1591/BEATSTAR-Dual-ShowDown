using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncManager : MonoBehaviour
{
    public static SyncManager instance;

    [SerializeField] AudioSource audioSource;
    [SerializeField] float BPM = 120f; // 노래의 BPM 설정 (예: 120 BPM)

    private List<float> timings = new List<float>(); // 타이밍을 저장할 리스트
    private int spacePressCount = 0; // 스페이스 바 입력 횟수 카운트
    public float averageInterval = 0.58f; // 최종 평균 간격을 저장할 변수

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
        // 스페이스 바를 누르면 현재 노래의 재생 시간을 타이밍 리스트에 추가
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (spacePressCount < 20) // 20번까지만 입력 가능
            {
                float currentTime = audioSource.time; // 현재 노래의 재생 시간
                timings.Add(currentTime); // 타이밍 리스트에 추가
                Debug.Log("Timing saved: " + currentTime);
                spacePressCount++;

                if (timings.Count > 1)
                {
                    // 이전 타이밍과 현재 타이밍 사이의 간격을 구합니다.
                    float lastTiming = timings[timings.Count - 2]; // 이전 타이밍
                    float interval = currentTime - lastTiming; // 현재 타이밍과 이전 타이밍 사이의 간격
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

    // 저장된 타이밍에 맞춰 노래 재생
    public void SyncStart()
    {
        audioSource.Play();
    }

    // 타이밍 간격들의 평균을 구하는 메서드
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

    // 평균 간격을 반환하는 메서드
    public float GetAverageInterval()
    {
        return averageInterval;
    }
}
