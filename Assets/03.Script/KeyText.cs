using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyText : MonoBehaviour
{
    public TMP_Text[] txtArray; // TMP_Text 배열
    public float baseFontSize = 24f; // 기본 글자 크기
    public float fontSizePerCharacter = 8f; // 한 글자당 크기 감소량

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
            string keyText = KeySetting.keys[(KeyAction)i].ToString(); // KeySetting에서 키 설정을 가져와 문자열로 변환
            txtArray[i].text = keyText; // TMP_Text에 키 설정 문자열 할당

            // 글자 수에 따라 크기 조정
            int numCharacters = keyText.Length; // 문자열 길이 확인
            float newSize = baseFontSize - fontSizePerCharacter * (numCharacters - 3); // 기본 크기에서 3글자 초과 시 1글자당 크기를 줄이기

            // 최소 크기 설정 (baseFontSize 보다 작아지지 않도록)
            newSize = Mathf.Max(newSize, baseFontSize);

            // TMP_Text의 글자 크기 설정
            txtArray[i].fontSize = newSize;
        }
    }
}
    