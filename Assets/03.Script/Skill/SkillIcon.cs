using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    public Sprite[] sprites;  // 클리어 패널에서 사용할 스프라이트 배열
    Image image;// 이미지 컴포넌트

    private void Awake()
    {
        image = GetComponent<Image>();
    }
    void OnEnable()
    {
        image.sprite = sprites[0];// 초기 이미지 스프라이트를 설정

        if (DataManager.instance.currentCharater == Character.White)  // DataManager에서 현재 캐릭터 정보를 가져와 해당하는 스프라이트를 설정
        {
            image.sprite = sprites[0];// 첫번째 캐릭터에 해당하는 스프라이트 
        }
        else if (DataManager.instance.currentCharater == Character.Red)
        {
            image.sprite = sprites[1]; // 두번째 캐릭터에 해당하는 스프라이트 
        }
        else if (DataManager.instance.currentCharater == Character.Blue)
        {
            image.sprite = sprites[2];// 세번째 캐릭터에 해당하는 스프라이트
        }
    }

    void Update()
    {
    }

}
