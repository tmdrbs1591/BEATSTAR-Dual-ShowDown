using UnityEngine;

public class BeatStartTiming : MonoBehaviour
{
    public AudioSource audioSource; // 음악 재생할 AudioSource
    private bool isTiming = false; // 버튼이 눌리기 시작하는지 여부
    private float[] beatTimes = new float[20]; // 20번의 버튼 누른 시간 저장
    private int pressCount = 0; // 누른 횟수
    private float startTime; // 첫 번째 버튼 누른 시간
    private bool songStarted = false; // 노래가 시작되었는지 여부

    void Update()
    {
        // 'Space' 키로 비트를 눌렀다고 가정
        if (Input.GetKeyDown(KeyCode.Space) && !songStarted)
        {
            if (!isTiming)
            {
                // 첫 번째 버튼을 누르면 타이머 시작
                isTiming = true;
                startTime = Time.time;
                Debug.Log("First beat pressed.");
            }

            // 버튼 누른 시간을 저장
            beatTimes[pressCount] = Time.time - startTime;
            pressCount++;
            Debug.Log("Beat " + pressCount + " at: " + beatTimes[pressCount - 1] + " seconds.");

            // 20번 눌렀을 경우 음악 시작
            if (pressCount == 20)
            {
                // 평균 타이밍 계산
                float averageTime = CalculateAverageTime();
                Debug.Log("Average time between beats: " + averageTime + " seconds.");

                // 일정 시간 후에 음악을 시작
                Invoke("StartSong", averageTime);
                songStarted = true;
            }
        }
    }

    // 평균 타이밍을 계산하는 함수
    float CalculateAverageTime()
    {
        float totalTime = 0f;
        for (int i = 1; i < pressCount; i++) // 첫 번째 비트는 기준이므로 제외
        {
            totalTime += beatTimes[i] - beatTimes[i - 1];
        }
        return totalTime / (pressCount - 1); // 비트 간격의 평균값 반환
    }

    void StartSong()
    {
        // 음악 시작
        audioSource.Play();
        Debug.Log("Song started!");
    }
}
