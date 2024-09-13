using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectChar : MonoBehaviour
{
    public Character character;// ���� ĳ���� Ÿ��
    SpriteRenderer spriteRenderer;// ��������Ʈ ������ ������Ʈ
    public SelectChar[] chars;// �ٸ� ĳ���͵� �迭

    public ButtonManager buttonManager;// ��ư �Ŵ��� ����

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();// ��������Ʈ ������ ������Ʈ ��������
        UpdateSelection();// �ʱ� ���� ���� ������Ʈ
    }

    void Update()
    {
        // Ȱ��ȭ�Ǿ� �ִ� ĳ���Ͱ� ����� ������ ���� ������Ʈ
        if (DataManager.instance.currentCharater != character)
        {
            UpdateSelection();
        }
        if (Input.GetKeyDown(KeyCode.Return))  // Return Ű�� ������ ���� ĳ���� ����
        {
            DataManager.instance.currentCharater = character;
            UpdateSelection();
            for (int i = 0; i < chars.Length; i++)  // �ٸ� ĳ���͵��� ���� ���� ������Ʈ
            {
                if (chars[i] != this && chars[i] != null)
                    chars[i].UpdateSelection();
            }
        }
    }
   
    private void OnMouseDown()
    {
        DataManager.instance.currentCharater = character;   // ���콺�� Ŭ���ϸ� ���� ĳ���� ����
        UpdateSelection();
        for (int i = 0; i < chars.Length; i++)  // �ٸ� ĳ���͵��� ���� ���� ������Ʈ
        {
            if (chars[i] != this && chars[i] != null)
                chars[i].UpdateSelection();
        }
    }

    void UpdateSelection() // ���� ���¿� ���� ���� ������Ʈ
    {
        if (DataManager.instance.currentCharater == character)
            OnSelect();// ���õ� ������ �� ȣ��
        else
            OnDeSelect(); // ���õ��� ���� ������ �� ȣ��   
    }

    void OnDeSelect() // ���� ���� ���� ����
    {
        if (spriteRenderer != null)
            spriteRenderer.color = Color.white;// �⺻ �������� ����
    }

    void OnSelect()  // ���õ� ���� ����
    {

        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);   // ȸ������ ���� ����
            AudioManager.instance.PlaySound(transform.position, 10, Random.Range(1.0f, 1.0f), 1); //���� ���

        }


    }
}
