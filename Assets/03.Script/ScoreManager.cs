using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text txtScore = null;// 점수 텍스트 UI

    [SerializeField] int increaseScore = 10; // 기본 점수 증가량
    public int currentScore = 0; // 현재 점수

    [SerializeField] float[] weight = null; // 판정 가중치 배열
    [SerializeField] int comboBonusScore = 10; // 콤보 보너스 점수

    Animator myAnim; // 점수 업 애니메이터
    string animationScoreUp = "ScoreUp"; // 점수 업 애니메이션 이름

    ComboManager thecomboManager; // 콤보 매니저

    void Start()
    {
        thecomboManager = FindObjectOfType<ComboManager>(); // 콤보 매니저 찾기
        myAnim = GetComponent<Animator>();// 애니메이터 컴포넌트 가져오기
        currentScore = 0;// 현재 점수 초기화
        txtScore.text = "0";// 현재 점수 초기화
    }

  
   public void IncreaseScore(int p_JudgementState)
    {//콤보증가
        thecomboManager.IncreaseCombo();// 콤보 증가

        //콤보 보너스 점수 계산
        int t_currentCombo = thecomboManager.GetCurrentCombo(); // 현재 콤보와 콤보 보너스 점수 계산
        int t_bonusComboScore = (t_currentCombo / 10) * comboBonusScore;

        int t_increateScore = increaseScore;
        //가중치 계산
        int t_increaseScore = increaseScore + t_bonusComboScore;    
        t_increateScore = (int)(t_increateScore * weight[p_JudgementState]);

        //점수 반영
        currentScore += t_increateScore;
        txtScore.text = string.Format("{0:#,##0}", currentScore); // 점수를 텍스트에 반영
        //애니메이션
        myAnim.SetTrigger(animationScoreUp);

    }
}
