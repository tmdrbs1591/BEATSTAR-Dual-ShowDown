using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FeverManager : MonoBehaviour
{
    [SerializeField] GameObject Effect;// �ǹ�Ÿ�� ����Ʈ�� ����ϴ� ���� ������Ʈ

    [SerializeField] public Slider feverSlider = null;// �ǹ�Ÿ�� �����̴� UI
    [SerializeField] public float feverThreshold = 2.0f; // �ǹ�Ÿ���� ���۵Ǵ� �����̴� ���� �Ӱ�ġ

    [SerializeField] int increaseScore = 10; // �⺻ ���� ������
    int currentScore = 0;// ���� ����

    [SerializeField] float[] weight = null;// ���� ���¿� ���� ����ġ �迭
    [SerializeField] int comboBonusScore = 10; // �޺� ���ʽ� ����


    ComboManager theComboManager; // �޺� ������

    public bool feverTime = false; // �ǹ�Ÿ�� ���� Ȯ��

    // �����̴��� �̵� �ӵ�
    float sliderMoveSpeed = 0.1f;
        
    void Start()
    {
        theComboManager = FindObjectOfType<ComboManager>(); // ComboManager ������Ʈ�� ã�� �Ҵ�
        currentScore = 0;// �ʱ� ���� ����
    }

    void Update()
    {
        if (feverSlider.value >= feverThreshold && !feverTime)      // �ǹ�Ÿ���� ������ ������ Ȯ���Ͽ� �ǹ�Ÿ���� ������
        {
            StartFeverTime();
        }

    
        if (!feverTime) // �ǹ�Ÿ���� �ƴ� �� �����̴��� �ε巴�� �̵���Ŵ
        {
            feverSlider.value = Mathf.MoveTowards(feverSlider.value, (float)currentScore / 1000f, sliderMoveSpeed * Time.deltaTime);
        }
    }

    public void IncreaseFever(int judgementState)    // ���� ����� ���� �ǹ� �����̴��� ������Ű�� �޼���
    {

        int currentCombo = theComboManager.GetCurrentCombo();// ���� �޺� �� ��������
        int bonusComboScore = (currentCombo / 10) * comboBonusScore; // �޺� ���ʽ� ���� ���

        int scoreIncrease = increaseScore;// �⺻ ���� ������
        scoreIncrease = (int)(scoreIncrease * weight[judgementState]);// ���� ���¿� ���� ����ġ ����

        if (feverTime)// �ǹ�Ÿ�� ���� ��� ���� ������ �� ��
        {
            scoreIncrease *= 2;
        }

        currentScore += scoreIncrease;// ���� ���� ����
    }

   public void StartFeverTime() // �ǹ�Ÿ���� �����ϴ� �޼���
    {
       // AudioManager.instance.PlaySound(transform.position, 6, Random.Range(1f, 1f), 1);// ����� ���
        feverTime = true; // �ǹ�Ÿ�� ����
        feverSlider.value = 1f; // �ʱⰪ�� ���� ������ ����
        StartCoroutine(FeverTime());// �ǹ�Ÿ�� �ڷ�ƾ ����
    }

    IEnumerator FeverTime()  // �ǹ�Ÿ�� �ڷ�ƾ
    {
        Effect.SetActive(true); // �ǹ�Ÿ�� ����Ʈ Ȱ��ȭ

        float targetValue = 0f; // ��ǥ �����̴� ��
        float startingValue = feverSlider.value; // ���� �����̴� ��
        float distance = Mathf.Abs(targetValue - startingValue);// �̵��ؾ� �� �Ÿ�
        float duration = distance / sliderMoveSpeed; // �Ÿ��� �̵� �ӵ��� ������ �̵� �ð��� ���
        float elapsedTime = 0f;// ��� �ð� �ʱ�ȭ

        while (elapsedTime < duration)
        {
            feverSlider.value = Mathf.Lerp(startingValue, targetValue, elapsedTime / duration);// �����̴� ���� �ε巴�� �̵�
            elapsedTime += Time.deltaTime;// ��� �ð� ������Ʈ
            yield return null;  // ���� �����ӱ��� ���
        }

        feverSlider.value = targetValue; // ���� ��ǥ�� ����
        currentScore = 0; // �ǹ�Ÿ�� ���� �� ���� �ʱ�ȭ
        feverSlider.value = 0f; // �����̴��� �ʱ�ȭ
        feverTime = false; // �ǹ�Ÿ�� ����
        Effect.SetActive(false);
    }


}
