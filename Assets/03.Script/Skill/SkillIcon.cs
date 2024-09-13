using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    public Sprite[] sprites;  // Ŭ���� �гο��� ����� ��������Ʈ �迭
    Image image;// �̹��� ������Ʈ

    private void Awake()
    {
        image = GetComponent<Image>();
    }
    void OnEnable()
    {
        image.sprite = sprites[0];// �ʱ� �̹��� ��������Ʈ�� ����

        if (DataManager.instance.currentCharater == Character.White)  // DataManager���� ���� ĳ���� ������ ������ �ش��ϴ� ��������Ʈ�� ����
        {
            image.sprite = sprites[0];// ù��° ĳ���Ϳ� �ش��ϴ� ��������Ʈ 
        }
        else if (DataManager.instance.currentCharater == Character.Red)
        {
            image.sprite = sprites[1]; // �ι�° ĳ���Ϳ� �ش��ϴ� ��������Ʈ 
        }
        else if (DataManager.instance.currentCharater == Character.Blue)
        {
            image.sprite = sprites[2];// ����° ĳ���Ϳ� �ش��ϴ� ��������Ʈ
        }
    }

    void Update()
    {
    }

}
