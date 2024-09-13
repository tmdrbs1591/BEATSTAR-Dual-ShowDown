using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearPanel : MonoBehaviour
{
    public Sprite[] sprites;  // 클리어 패널에서 사용할 스프라이트 배열
    public Image image;// 이미지 컴포넌트
    public string scenceName = "StageMode";
    void OnEnable()
    {
        StartCoroutine(CameraShakes()); // 카메라 흔들림 효과를 시작하는 코루틴을 실행
        image.sprite = sprites[0];// 초기 이미지 스프라이트를 설정

        if (DataManager.instance.currentCharater == Character.White)  // DataManager에서 현재 캐릭터 정보를 가져와 해당하는 스프라이트를 설정
        {
            image.sprite = sprites[0];// 첫번째 캐릭터에 해당하는 스프라이트 
        }
        else if (DataManager.instance.currentCharater == Character.Red)
        {
            image.sprite = sprites[1]; // 두번째 캐릭터에 해당하는 스프라이트 
        }
        else if (DataManager.instance.currentCharater == Character.Blue)
        {
            image.sprite = sprites[2];// 세번째 캐릭터에 해당하는 스프라이트
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))// Enter 키가 눌리면
        {
            LoadingManager.LoadScene(scenceName);// Title 씬으로 로딩
        }
    }

    IEnumerator CameraShakes()
    {
        yield return new WaitForSeconds(1.45f);// 1.45초 후에
        CameraShake.instance.Shake();// 카메라를 흔들어주는 Shake 메서드를 호출
    }
}
