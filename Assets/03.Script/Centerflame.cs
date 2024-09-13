using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centerflame : MonoBehaviour
{

    AudioSource myAudio;// AudioSource 컴포넌트를 저장할 변수
    bool musicStart = false;    // 음악이 시작되었는지 여부를 나타내는 플래그

    private void Start()
    {
        myAudio = GetComponent<AudioSource>(); // 자신의 GameObject에서 AudioSource 컴포넌트 가져오기
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 음악이 시작되지 않은 상태에서만 처리
        if (!musicStart)
        {
            if (collision.CompareTag("Note"))// 충돌한 객체의 태그가 "Note"인 경우
            {
                myAudio.Play();// AudioSource 재생
                musicStart = true;// 음악 시작 플래그를 true로 설정하여 중복 재생 방지
            }
        }
    }
}
