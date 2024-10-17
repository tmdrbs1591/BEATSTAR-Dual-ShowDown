using UnityEngine;
using UnityEngine.UI;

public class VolumeBasedImageScaler : MonoBehaviour
{
    public AudioSource audioSource;  // 오디오 소스
    public Image targetImage;        // 크기를 조절할 이미지
    public float scaleMultiplier = 2.0f; // 볼륨에 곱할 값 (이미지 크기 조정)
    public float smoothSpeed = 0.1f; // 이미지 크기 변화 속도 조정

    private Vector3 originalScale;   // 원래 이미지 크기

    void Start()
    {
        // 이미지의 원래 크기 저장
        originalScale = targetImage.transform.localScale;
    }

    void Update()
    {
        // 실제 음악 볼륨 값 (RMS 값)을 계산
        float volume = GetAudioVolume();

        // 이미지 크기를 음악의 볼륨에 맞춰 조정 (부드럽게 변화)
        Vector3 newScale = originalScale * (1 + volume * scaleMultiplier);
        targetImage.transform.localScale = Vector3.Lerp(targetImage.transform.localScale, newScale, smoothSpeed);
    }

    // 실제 음악의 볼륨 (출력 레벨)을 계산하는 함수
    float GetAudioVolume()
    {
        // 오디오 소스의 출력 데이터를 가져옴
        float[] samples = new float[256];
        audioSource.GetOutputData(samples, 0);

        // RMS (Root Mean Square) 계산
        float sum = 0f;
        foreach (float sample in samples)
        {
            sum += sample * sample;
        }
        return Mathf.Sqrt(sum / samples.Length);  // RMS 값 반환
    }
}
