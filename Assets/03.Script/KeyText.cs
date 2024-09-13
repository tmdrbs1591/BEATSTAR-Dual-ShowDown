using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyText : MonoBehaviour
{
    public TMP_Text[] txtArray; // TMP_Text �迭
    public float baseFontSize = 24f; // �⺻ ���� ũ��
    public float fontSizePerCharacter = 8f; // �� ���ڴ� ũ�� ���ҷ�

    void Start()
    {
        UpdateTextSizes();
    }

    void Update()
    {
        UpdateTextSizes();
    }

    void UpdateTextSizes()
    {
        for (int i = 0; i < txtArray.Length; i++)
        {
            string keyText = KeySetting.keys[(KeyAction)i].ToString(); // KeySetting���� Ű ������ ������ ���ڿ��� ��ȯ
            txtArray[i].text = keyText; // TMP_Text�� Ű ���� ���ڿ� �Ҵ�

            // ���� ���� ���� ũ�� ����
            int numCharacters = keyText.Length; // ���ڿ� ���� Ȯ��
            float newSize = baseFontSize - fontSizePerCharacter * (numCharacters - 3); // �⺻ ũ�⿡�� 3���� �ʰ� �� 1���ڴ� ũ�⸦ ���̱�

            // �ּ� ũ�� ���� (baseFontSize ���� �۾����� �ʵ���)
            newSize = Mathf.Max(newSize, baseFontSize);

            // TMP_Text�� ���� ũ�� ����
            txtArray[i].fontSize = newSize;
        }
    }
}
    