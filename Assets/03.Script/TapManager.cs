using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TapManager : MonoBehaviour
{
    public GameObject[] Tap;// �� UI �迭
    private int currentIndex = 0;// ���� �� �ε���
    public ButtonManager buttonManager;// ��ư �Ŵ��� ����
    public Image[] CharImage;// ĳ���� �̹��� �迭
    public Image[] CharShadowImage;// ĳ���� �׸��� �̹��� �迭

    void Start()
    {
        TapClick(0);// �ʱ�ȭ�� ù ��° ������ ����
    }
    
    public void TapClick(int n)
    {
        AudioManager.instance.PlaySound(transform.position, 9, Random.Range(1.0f, 1.0f), 1);

        for (int i = 0; i < Tap.Length; i++)// ��� �� ��Ȱ��ȭ �� ���õ� �� Ȱ��ȭ
        {
            Tap[i].SetActive(i == n);
        }
        currentIndex = n;// ���� �ε��� ������Ʈ
    }
    public void TapClickRight()
    {
        if (currentIndex < Tap.Length - 1)
        {
            RightMove(); // ���������� �̵� �ִϸ��̼� ����

            TapClick(currentIndex + 1);// ���� ������ �̵�
        }
    }
    public void TapClickLeft()
    {
        if (currentIndex > 0)
        {
            LeftMove();// �������� �̵� �ִϸ��̼� ����
            TapClick(currentIndex - 1); // ���� ������ �̵�
        }
    }
    private void Update()
    {
        // ������ ȭ��ǥ �Ǵ� D Ű�� ������ �� ���� ������ �̵� (ĳ���� �г��� ���� ���� ��)
        if (Input.GetKeyDown(KeyCode.RightArrow) && buttonManager.isCharPanel || Input.GetKeyDown(KeyCode.D) && buttonManager.isCharPanel)
        {
            if (currentIndex < Tap.Length - 1)
            {

                RightMove();// ���������� �̵� �ִϸ��̼� ����
                TapClick(currentIndex + 1);// ���� ������ �̵�
            }
        }
        // ���� ȭ��ǥ �Ǵ� A Ű�� ������ �� ���� ������ �̵� (ĳ���� �г��� ���� ���� ��)
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && buttonManager.isCharPanel || Input.GetKeyDown(KeyCode.A) && buttonManager.isCharPanel)
        {
            if (currentIndex > 0)
            {
                LeftMove(); // �������� �̵� �ִϸ��̼� ����

                TapClick(currentIndex - 1); // ���� ������ �̵�
            }
        }
    }
    void RightMove()
    {
        for (int i = 0; i < CharImage.Length; i++)
        {
            // �ʱ� ��ġ�� �̵� ��, �̵� �ִϸ��̼� ����
            CharImage[i].rectTransform.anchoredPosition = new Vector2(800, -563);
            CharImage[i].rectTransform.DOAnchorPos(new Vector2(-360, -568), 0.25f);
        }
        for (int i = 0; i < CharShadowImage.Length; i++)
        {
            // �ʱ� ��ġ�� �̵� ��, �̵� �ִϸ��̼� ����
            CharShadowImage[i].rectTransform.anchoredPosition = new Vector2(800, -563);
            CharShadowImage[i].rectTransform.DOAnchorPos(new Vector2(-360, -568), 0.3f);
        }
    }

    void LeftMove()
    {
        for (int i = 0; i < CharImage.Length; i++)
        {
            // �ʱ� ��ġ�� �̵� ��, �̵� �ִϸ��̼� ����
            CharImage[i].rectTransform.anchoredPosition = new Vector2(-800, -563);
            CharImage[i].rectTransform.DOAnchorPos(new Vector2(-360, -568), 0.25f);
        }
        for (int i = 0; i < CharShadowImage.Length; i++)
        {
            // �ʱ� ��ġ�� �̵� ��, �̵� �ִϸ��̼� ����
            CharShadowImage[i].rectTransform.anchoredPosition = new Vector2(-800, -563);
            CharShadowImage[i].rectTransform.DOAnchorPos(new Vector2(-360, -568), 0.3f);
        }
    }

}
