using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class DiePanel : MonoBehaviour
{
    public VideoPlayer videoPlayer;// 비디오 플레이어를 연결할 변수
    public VideoClip[] videoClips;// 다양한 캐릭터에 따른 비디오 클립을 저장할 배열

    void OnEnable()
    {
        StartCoroutine(CameraShakes());  // 카메라 흔들림 효과를 위한 코루틴 시작
        UpdateVideoClip(); // 비디오 클립을 업데이트합니다.
    }

    void UpdateVideoClip()
    {
        if (DataManager.instance.currentCharater == Character.White)  // DataManager에서 현재 선택된 캐릭터에 따라 비디오 클립을 설정합니다.
        {
            videoPlayer.clip = videoClips[0];
        }
        else if (DataManager.instance.currentCharater == Character.Red)
        {
            videoPlayer.clip = videoClips[1];
        }
        else if (DataManager.instance.currentCharater == Character.Blue)
        {
            videoPlayer.clip = videoClips[2];
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            LoadingManager.LoadScene("Menu");// Enter 키를 누르면 타이틀 씬으로 로드
        }
    }

    IEnumerator CameraShakes()
    {
        yield return new WaitForSeconds(1.45f);// 대기 후
        CameraShake.instance.Shake();// 카메라 흔들림 효과를 실행
    }
}
