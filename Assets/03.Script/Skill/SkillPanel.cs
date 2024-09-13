using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    public Sprite[] sprites;  // Ŭ���� �гο��� ����� ��������Ʈ �迭
    public SpriteRenderer spriteRenderer;// �̹��� ������Ʈ

    void OnEnable()
    {
        spriteRenderer.sprite = sprites[0];// �ʱ� �̹��� ��������Ʈ�� ����

        if (DataManager.instance.currentCharater == Character.White)  // DataManager���� ���� ĳ���� ������ ������ �ش��ϴ� ��������Ʈ�� ����
        {
            spriteRenderer.sprite = sprites[0];// ù��° ĳ���Ϳ� �ش��ϴ� ��������Ʈ 
        }
        else if (DataManager.instance.currentCharater == Character.Red)
        {
            spriteRenderer.sprite = sprites[1]; // �ι�° ĳ���Ϳ� �ش��ϴ� ��������Ʈ 
        }
        else if (DataManager.instance.currentCharater == Character.Blue)
        {
            spriteRenderer.sprite = sprites[2];// ����° ĳ���Ϳ� �ش��ϴ� ��������Ʈ
        }
    }

    void Update()
    {
    }

}
