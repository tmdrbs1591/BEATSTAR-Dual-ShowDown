using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectChar : MonoBehaviour
{
    public Character character;// 현재 캐릭터 타입
    SpriteRenderer spriteRenderer;// 스프라이트 렌더러 컴포넌트
    public SelectChar[] chars;// 다른 캐릭터들 배열

    public ButtonManager buttonManager;// 버튼 매니저 참조

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();// 스프라이트 렌더러 컴포넌트 가져오기
        UpdateSelection();// 초기 선택 상태 업데이트
    }

    void Update()
    {
        // 활성화되어 있는 캐릭터가 변경될 때마다 색상 업데이트
        if (DataManager.instance.currentCharater != character)
        {
            UpdateSelection();
        }
        if (Input.GetKeyDown(KeyCode.Return))  // Return 키를 누르면 현재 캐릭터 선택
        {
            DataManager.instance.currentCharater = character;
            UpdateSelection();
            for (int i = 0; i < chars.Length; i++)  // 다른 캐릭터들의 선택 상태 업데이트
            {
                if (chars[i] != this && chars[i] != null)
                    chars[i].UpdateSelection();
            }
        }
    }
   
    private void OnMouseDown()
    {
        DataManager.instance.currentCharater = character;   // 마우스로 클릭하면 현재 캐릭터 선택
        UpdateSelection();
        for (int i = 0; i < chars.Length; i++)  // 다른 캐릭터들의 선택 상태 업데이트
        {
            if (chars[i] != this && chars[i] != null)
                chars[i].UpdateSelection();
        }
    }

    void UpdateSelection() // 선택 상태에 따라 색상 업데이트
    {
        if (DataManager.instance.currentCharater == character)
            OnSelect();// 선택된 상태일 때 호출
        else
            OnDeSelect(); // 선택되지 않은 상태일 때 호출   
    }

    void OnDeSelect() // 선택 해제 상태 설정
    {
        if (spriteRenderer != null)
            spriteRenderer.color = Color.white;// 기본 색상으로 설정
    }

    void OnSelect()  // 선택된 상태 설정
    {

        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);   // 회색으로 색상 변경
            AudioManager.instance.PlaySound(transform.position, 10, Random.Range(1.0f, 1.0f), 1); //사운드 재생

        }


    }
}
