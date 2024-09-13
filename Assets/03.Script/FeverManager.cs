using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FeverManager : MonoBehaviour
{
    [SerializeField] GameObject Effect;// 피버타임 이펙트를 담당하는 게임 오브젝트

    [SerializeField] public Slider feverSlider = null;// 피버타임 슬라이더 UI
    [SerializeField] public float feverThreshold = 2.0f; // 피버타임이 시작되는 슬라이더 값의 임계치

    [SerializeField] int increaseScore = 10; // 기본 점수 증가량
    int currentScore = 0;// 현재 점수

    [SerializeField] float[] weight = null;// 판정 상태에 따른 가중치 배열
    [SerializeField] int comboBonusScore = 10; // 콤보 보너스 점수


    ComboManager theComboManager; // 콤보 관리자

    public bool feverTime = false; // 피버타임 여부 확인

    // 슬라이더의 이동 속도
    float sliderMoveSpeed = 0.1f;
        
    void Start()
    {
        theComboManager = FindObjectOfType<ComboManager>(); // ComboManager 컴포넌트를 찾아 할당
        currentScore = 0;// 초기 점수 설정
    }

    void Update()
    {
        if (feverSlider.value >= feverThreshold && !feverTime)      // 피버타임이 시작할 조건을 확인하여 피버타임을 시작함
        {
            StartFeverTime();
        }

    
        if (!feverTime) // 피버타임이 아닐 때 슬라이더를 부드럽게 이동시킴
        {
            feverSlider.value = Mathf.MoveTowards(feverSlider.value, (float)currentScore / 1000f, sliderMoveSpeed * Time.deltaTime);
        }
    }

    public void IncreaseFever(int judgementState)    // 판정 결과에 따라 피버 슬라이더를 증가시키는 메서드
    {

        int currentCombo = theComboManager.GetCurrentCombo();// 현재 콤보 수 가져오기
        int bonusComboScore = (currentCombo / 10) * comboBonusScore; // 콤보 보너스 점수 계산

        int scoreIncrease = increaseScore;// 기본 점수 증가량
        scoreIncrease = (int)(scoreIncrease * weight[judgementState]);// 판정 상태에 따른 가중치 적용

        if (feverTime)// 피버타임 중일 경우 점수 증가량 두 배
        {
            scoreIncrease *= 2;
        }

        currentScore += scoreIncrease;// 현재 점수 증가
    }

   public void StartFeverTime() // 피버타임을 시작하는 메서드
    {
       // AudioManager.instance.PlaySound(transform.position, 6, Random.Range(1f, 1f), 1);// 오디오 재생
        feverTime = true; // 피버타임 시작
        feverSlider.value = 1f; // 초기값을 현재 값으로 설정
        StartCoroutine(FeverTime());// 피버타임 코루틴 시작
    }

    IEnumerator FeverTime()  // 피버타임 코루틴
    {
        Effect.SetActive(true); // 피버타임 이펙트 활성화

        float targetValue = 0f; // 목표 슬라이더 값
        float startingValue = feverSlider.value; // 시작 슬라이더 값
        float distance = Mathf.Abs(targetValue - startingValue);// 이동해야 할 거리
        float duration = distance / sliderMoveSpeed; // 거리를 이동 속도로 나누어 이동 시간을 계산
        float elapsedTime = 0f;// 경과 시간 초기화

        while (elapsedTime < duration)
        {
            feverSlider.value = Mathf.Lerp(startingValue, targetValue, elapsedTime / duration);// 슬라이더 값을 부드럽게 이동
            elapsedTime += Time.deltaTime;// 경과 시간 업데이트
            yield return null;  // 다음 프레임까지 대기
        }

        feverSlider.value = targetValue; // 최종 목표값 설정
        currentScore = 0; // 피버타임 종료 후 점수 초기화
        feverSlider.value = 0f; // 슬라이더도 초기화
        feverTime = false; // 피버타임 종료
        Effect.SetActive(false);
    }


}
