using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScorePanel : MonoBehaviour
{
    public StageModeStage1 stage1; // 스테이지 1의 점수값을 받기위해 stage1의 노트최대 생성 개수를 가져올수 잇게 
    public ScoreManager theScoreManager;
    public TMP_Text Score; // 스코어 텍스트
    public TMP_Text Tear; // 티어 텍스트

    [SerializeField] float S_Score; 
    [SerializeField] float A_Score;
    [SerializeField] float B_Score;
    [SerializeField] float C_Score;
    [SerializeField] float D_Score;

    private int targetScore;
    private float animationDuration = 1.5f; // 애니메이션 지속 시간
    private float animationStartTime; // 애니메이션 시작 시간
    private float delayBeforeAnimation = 1f; // 애니메이션 시작 전 지연 시간

    void Start()
    {
        animationStartTime = Time.time + delayBeforeAnimation;
        targetScore = theScoreManager.currentScore;
    }

    void Update()
    {
        if (stage1 != null)
        {
            int maxNotes = stage1.maxNotes;

            S_Score = maxNotes * 320;
            A_Score = maxNotes * 270;
            B_Score = maxNotes * 227;
            C_Score = maxNotes * 180;
            D_Score = maxNotes * 140;
        }
    
        // 현재 시간과 애니메이션 시작 시간 간의 경과 시간 비율 계산
        float elapsedTime = Time.time - animationStartTime;

        if (elapsedTime >= 0) // 3초 지연 후 애니메이션 시작
        {
            float elapsedTimeRatio = elapsedTime / animationDuration;
            int displayScore = Mathf.RoundToInt(Mathf.Lerp(0, targetScore, elapsedTimeRatio)); // 시작 값부터 목표 값까지의 보간된 값을 계산

            // 점수 텍스트 업데이트
            Score.text = "SCORE:" + displayScore.ToString();

            // 애니메이션 종료 후에는 실제 점수를 표시
            if (elapsedTimeRatio >= 1.0f)
            {
                Score.text = "SCORE:" + targetScore.ToString();
            }

           
        }
        // 등급 텍스트 업데이트

        if (targetScore >= S_Score)
        {
            Tear.text = "S";
            //GoldManager.instance.CrearGold("S");
        }
        else if (targetScore >= A_Score)
            Tear.text = "A";
        else if (targetScore >= B_Score)
            Tear.text = "B";
        else if (targetScore >= C_Score)
            Tear.text = "C";
        else if (targetScore >= D_Score)
            Tear.text = "D";
        else
            Tear.text = "E";
    }
        
    public void Retry()
    {
        LoadingManager.LoadScene("Title");
    }
}
