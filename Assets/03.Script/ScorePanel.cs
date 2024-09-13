using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScorePanel : MonoBehaviour
{
    public StageModeStage1 stage1; // �������� 1�� �������� �ޱ����� stage1�� ��Ʈ�ִ� ���� ������ �����ü� �հ� 
    public ScoreManager theScoreManager;
    public TMP_Text Score; // ���ھ� �ؽ�Ʈ
    public TMP_Text Tear; // Ƽ�� �ؽ�Ʈ

    [SerializeField] float S_Score; 
    [SerializeField] float A_Score;
    [SerializeField] float B_Score;
    [SerializeField] float C_Score;
    [SerializeField] float D_Score;

    private int targetScore;
    private float animationDuration = 1.5f; // �ִϸ��̼� ���� �ð�
    private float animationStartTime; // �ִϸ��̼� ���� �ð�
    private float delayBeforeAnimation = 1f; // �ִϸ��̼� ���� �� ���� �ð�

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
    
        // ���� �ð��� �ִϸ��̼� ���� �ð� ���� ��� �ð� ���� ���
        float elapsedTime = Time.time - animationStartTime;

        if (elapsedTime >= 0) // 3�� ���� �� �ִϸ��̼� ����
        {
            float elapsedTimeRatio = elapsedTime / animationDuration;
            int displayScore = Mathf.RoundToInt(Mathf.Lerp(0, targetScore, elapsedTimeRatio)); // ���� ������ ��ǥ �������� ������ ���� ���

            // ���� �ؽ�Ʈ ������Ʈ
            Score.text = "SCORE:" + displayScore.ToString();

            // �ִϸ��̼� ���� �Ŀ��� ���� ������ ǥ��
            if (elapsedTimeRatio >= 1.0f)
            {
                Score.text = "SCORE:" + targetScore.ToString();
            }

           
        }
        // ��� �ؽ�Ʈ ������Ʈ

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
