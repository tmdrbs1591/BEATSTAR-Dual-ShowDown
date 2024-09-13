using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text txtScore = null;// ���� �ؽ�Ʈ UI

    [SerializeField] int increaseScore = 10; // �⺻ ���� ������
    public int currentScore = 0; // ���� ����

    [SerializeField] float[] weight = null; // ���� ����ġ �迭
    [SerializeField] int comboBonusScore = 10; // �޺� ���ʽ� ����

    Animator myAnim; // ���� �� �ִϸ�����
    string animationScoreUp = "ScoreUp"; // ���� �� �ִϸ��̼� �̸�

    ComboManager thecomboManager; // �޺� �Ŵ���

    void Start()
    {
        thecomboManager = FindObjectOfType<ComboManager>(); // �޺� �Ŵ��� ã��
        myAnim = GetComponent<Animator>();// �ִϸ����� ������Ʈ ��������
        currentScore = 0;// ���� ���� �ʱ�ȭ
        txtScore.text = "0";// ���� ���� �ʱ�ȭ
    }

  
   public void IncreaseScore(int p_JudgementState)
    {//�޺�����
        thecomboManager.IncreaseCombo();// �޺� ����

        //�޺� ���ʽ� ���� ���
        int t_currentCombo = thecomboManager.GetCurrentCombo(); // ���� �޺��� �޺� ���ʽ� ���� ���
        int t_bonusComboScore = (t_currentCombo / 10) * comboBonusScore;

        int t_increateScore = increaseScore;
        //����ġ ���
        int t_increaseScore = increaseScore + t_bonusComboScore;    
        t_increateScore = (int)(t_increateScore * weight[p_JudgementState]);

        //���� �ݿ�
        currentScore += t_increateScore;
        txtScore.text = string.Format("{0:#,##0}", currentScore); // ������ �ؽ�Ʈ�� �ݿ�
        //�ִϸ��̼�
        myAnim.SetTrigger(animationScoreUp);

    }
}
