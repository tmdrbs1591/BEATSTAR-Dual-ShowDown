using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TapManager : MonoBehaviour
{
    public GameObject[] Tap;// 탭 UI 배열
    private int currentIndex = 0;// 현재 탭 인덱스
    public ButtonManager buttonManager;// 버튼 매니저 참조
    public Image[] CharImage;// 캐릭터 이미지 배열
    public Image[] CharShadowImage;// 캐릭터 그림자 이미지 배열

    void Start()
    {
        TapClick(0);// 초기화면 첫 번째 탭으로 설정
    }
    
    public void TapClick(int n)
    {
        AudioManager.instance.PlaySound(transform.position, 9, Random.Range(1.0f, 1.0f), 1);

        for (int i = 0; i < Tap.Length; i++)// 모든 탭 비활성화 후 선택된 탭 활성화
        {
            Tap[i].SetActive(i == n);
        }
        currentIndex = n;// 현재 인덱스 업데이트
    }
    public void TapClickRight()
    {
        if (currentIndex < Tap.Length - 1)
        {
            RightMove(); // 오른쪽으로 이동 애니메이션 실행

            TapClick(currentIndex + 1);// 다음 탭으로 이동
        }
    }
    public void TapClickLeft()
    {
        if (currentIndex > 0)
        {
            LeftMove();// 왼쪽으로 이동 애니메이션 실행
            TapClick(currentIndex - 1); // 이전 탭으로 이동
        }
    }
    private void Update()
    {
        // 오른쪽 화살표 또는 D 키를 눌렀을 때 다음 탭으로 이동 (캐릭터 패널이 열려 있을 때)
        if (Input.GetKeyDown(KeyCode.RightArrow) && buttonManager.isCharPanel || Input.GetKeyDown(KeyCode.D) && buttonManager.isCharPanel)
        {
            if (currentIndex < Tap.Length - 1)
            {

                RightMove();// 오른쪽으로 이동 애니메이션 실행
                TapClick(currentIndex + 1);// 다음 탭으로 이동
            }
        }
        // 왼쪽 화살표 또는 A 키를 눌렀을 때 이전 탭으로 이동 (캐릭터 패널이 열려 있을 때)
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && buttonManager.isCharPanel || Input.GetKeyDown(KeyCode.A) && buttonManager.isCharPanel)
        {
            if (currentIndex > 0)
            {
                LeftMove(); // 왼쪽으로 이동 애니메이션 실행

                TapClick(currentIndex - 1); // 이전 탭으로 이동
            }
        }
    }
    void RightMove()
    {
        for (int i = 0; i < CharImage.Length; i++)
        {
            // 초기 위치로 이동 후, 이동 애니메이션 실행
            CharImage[i].rectTransform.anchoredPosition = new Vector2(800, -563);
            CharImage[i].rectTransform.DOAnchorPos(new Vector2(-360, -568), 0.25f);
        }
        for (int i = 0; i < CharShadowImage.Length; i++)
        {
            // 초기 위치로 이동 후, 이동 애니메이션 실행
            CharShadowImage[i].rectTransform.anchoredPosition = new Vector2(800, -563);
            CharShadowImage[i].rectTransform.DOAnchorPos(new Vector2(-360, -568), 0.3f);
        }
    }

    void LeftMove()
    {
        for (int i = 0; i < CharImage.Length; i++)
        {
            // 초기 위치로 이동 후, 이동 애니메이션 실행
            CharImage[i].rectTransform.anchoredPosition = new Vector2(-800, -563);
            CharImage[i].rectTransform.DOAnchorPos(new Vector2(-360, -568), 0.25f);
        }
        for (int i = 0; i < CharShadowImage.Length; i++)
        {
            // 초기 위치로 이동 후, 이동 애니메이션 실행
            CharShadowImage[i].rectTransform.anchoredPosition = new Vector2(-800, -563);
            CharShadowImage[i].rectTransform.DOAnchorPos(new Vector2(-360, -568), 0.3f);
        }
    }

}
