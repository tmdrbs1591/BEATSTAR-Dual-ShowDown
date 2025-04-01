using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField] GameObject goComboImage = null;// �޺� �̹����� ǥ���� GameObject ����
    [SerializeField] TMPro.TMP_Text txtCombo = null;// �޺� �ؽ�Ʈ�� ǥ���� TMPro�� TextMeshPro Text ����

    int currentCombo = 0;// ���� �޺� ��
    int maxCombo = 0;// �ִ� �޺� ��

    Animator myAnim;// Animator ������Ʈ�� ������ ����
    string animComboUp = "ComboUp"; // Animator���� ����� Ʈ���� �̸�

    private void Start()
    {
        myAnim = GetComponent<Animator>(); // �ڽ��� GameObject���� Animator ������Ʈ ��������
        txtCombo.gameObject.SetActive(false); // ������ �� �޺� �ؽ�Ʈ�� ��Ȱ��ȭ
        goComboImage.SetActive(false);// ������ �� �޺� �̹����� ��Ȱ��ȭ
    }

    public void IncreaseCombo(int p_num = 1) {
        currentCombo += p_num;// �޺� �� ����
        txtCombo.text = string.Format("{0:#,##0}", currentCombo);// ���� �޺� ���� �ؽ�Ʈ�� ǥ��

        if (maxCombo < currentCombo)
            maxCombo = currentCombo; // �ִ� �޺� �� ������Ʈ

        if (currentCombo > 2)
        {
            txtCombo.gameObject.SetActive(true);// �޺� �ؽ�Ʈ Ȱ��ȭ
            goComboImage.SetActive(true); // �޺� �̹��� Ȱ��ȭ

            myAnim.SetTrigger(animComboUp);  // ComboUp Ʈ���Ÿ� ���� Animator �ִϸ��̼� ���
        }
    }
    public int GetCurrentCombo()
    {
        return currentCombo; // ���� �޺� �� ��ȯ
    }
    public void ResetCombo()
    {
        currentCombo = 0;  // �޺� �� �ʱ�ȭ
        txtCombo.text = "0";  // �ؽ�Ʈ �ʱ�ȭ
        txtCombo.gameObject.SetActive(false); // �޺� �ؽ�Ʈ ��Ȱ��ȭ
        goComboImage.SetActive(false);// �޺� �̹��� ��Ȱ��ȭ
    } 
    public int GetMaxCombo()
    {
        return maxCombo; // �ִ� �޺� �� ��ȯ
    }
}
