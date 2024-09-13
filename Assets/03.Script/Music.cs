using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public PlaayerController Controller;// 플레이어 컨트롤러 참조 변수
    public AudioSource audioSource;// AudioSource 컴포넌트 참조 변수

    void Update()
    {
        if (Controller != null) // Controller가 null이 아닌 경우, 즉 플레이어 컨트롤러가 존재할 때
        {
            Controller = FindObjectOfType<PlaayerController>(); // Scene에서 플레이어 컨트롤러를 찾아서 할당

        }
        if (Controller.Death && audioSource.isPlaying)    // Controller의 Death 상태이고, 현재 AudioSource가 재생 중인 경우
        {
            AudioManager.instance.PlaySound(transform.position, 1, Random.Range(1f, 1f), 1); // 특정 사운드 재생

            audioSource.Stop(); // AudioSource 정지
        }
    }
    public void SetMusicVolume(float volume)    // 음악의 볼륨을 설정하는 메서드
    {
        audioSource.volume = volume;
    }
}
