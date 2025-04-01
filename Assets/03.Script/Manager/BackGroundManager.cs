using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField] GameObject[] backGrounds; // 배열명을 소문자로 변경하고, 이름 수정

    void Update()
    {
        // 현재 스테이지에 따라 배경 활성화 여부를 설정
        for (int i = 0; i < backGrounds.Length; i++)
        {
            bool isActive = (i == (int)StagerManager.instance.currentStage);
            backGrounds[i].SetActive(isActive);
        }
    }
}
