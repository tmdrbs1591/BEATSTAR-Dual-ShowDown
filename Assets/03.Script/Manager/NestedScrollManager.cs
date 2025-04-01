using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class NestedScrollManager : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Scrollbar scrollbar;// 스크롤바 객체
    const int SIZE = 10; // 항목의 개수
    float[] pos = new float[SIZE];// 각 항목의 위치 배열
    float distance,curPos,targetPos;// 거리, 현재 위치, 목표 위치
    bool isDrag;// 드래그 여부
    int targetIndex;// 목표 인덱스

    [SerializeField]  TMP_Text titlename; // UI에 표시될 제목

    [SerializeField]  TMP_Text tracks; // UI에 표시될 트랙 정보

    [SerializeField] GameObject[] Wave;// 여러 개의 Wave 오브젝트 배열    


    [SerializeField] ButtonManager buttonManager;// 버튼 관리자



    void Start()
    {
        distance = 1f/(SIZE-1);// 각 항목 간의 거리 설정
        for (int i = 0; i < SIZE; i++) pos[i] = distance * i;// 각 항목의 위치 설정

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        curPos = SetPos();// 현재 위치 설정

    }

    public void OnDrag(PointerEventData eventData)
    {
        isDrag = true;// 드래그 중임을 설정
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;// 드래그 종료

        targetPos = SetPos();// 목표 위치 설정
        AudioManager.instance.PlaySound(transform.position, 3, Random.Range(1f, 1f), 1);

        if (curPos == targetPos)  // 사용자가 드래그 한 거리에 따라 목표 위치 조정
        {
            if (eventData.delta.x > 18 && curPos - distance >= 0)
            {

                --targetIndex;
                targetPos = curPos - distance;
            }
            else if(eventData.delta.x < -18 && curPos + distance <= 1.01f)
            {

                ++targetIndex;
                targetPos = curPos + distance;
            }
        }
    }
    float SetPos()
    {
        for (int i = 0; i < SIZE; i++)
            if (scrollbar.value < pos[i] + distance * 0.5f && scrollbar.value > pos[i] - distance * 0.5f)
            {
                targetIndex = i;// 목표 인덱스 설정
                return pos[i];// 해당 위치 반환
            }
        return 0;
    }
    public void NextStage()
    {
        scrollbar.value += 0.25f;

    }
    void Update()
    {
        if (targetIndex < SIZE - 1) // 오른쪽으로 이동 가능 여부 체크
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && !buttonManager.isCharPanel || Input.GetKeyDown(KeyCode.D) && !buttonManager.isCharPanel)  // 오른쪽 화살표 또는 'D' 키 입력 시 이동
            {
                AudioManager.instance.PlaySound(transform.position, 3, Random.Range(1f, 1f), 1); // 사운드 재생

                targetIndex++;
                targetPos = pos[targetIndex];
            }
        }
        if (targetIndex > 0)  // 왼쪽으로 이동 가능 여부 체크
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && !buttonManager.isCharPanel || Input.GetKeyDown(KeyCode.A) && !buttonManager.isCharPanel)
            {
                AudioManager.instance.PlaySound(transform.position, 3, Random.Range(1f, 1f), 1); // 사운드 재생

                targetIndex--;
                targetPos = pos[targetIndex];
            }
        }
        if (!isDrag) scrollbar.value = Mathf.Lerp(scrollbar.value, targetPos, 0.1f);

        //특정 볼륨값에 따라 노래 제목 출력
       
       
     
        if (scrollbar.value <= 0.07f && !buttonManager.isCharPanel && !buttonManager.isTitleSettingPanel) // 벨류값에 따라 노래제목 변경
        {
            SetStage(StagerManager.Stage.FirstStage, "Lian Ai Audio Navigation","3Track",0);
        }
        else if (scrollbar.value >= 0.08f && scrollbar.value <= 0.18f && !buttonManager.isCharPanel && !buttonManager.isTitleSettingPanel)
        {
            SetStage(StagerManager.Stage.SecondStage, "Nitro Fun - Final Boss", "3Track", 1);
        }
        else if (scrollbar.value >= 0.19f && scrollbar.value <= 0.28f && !buttonManager.isCharPanel && !buttonManager.isTitleSettingPanel)
        {
            SetStage(StagerManager.Stage.ThirdStage, "IyaIya", "3Track", 2);
        }
        else if (scrollbar.value >= 0.29f && scrollbar.value <= 0.38f && !buttonManager.isCharPanel && !buttonManager.isTitleSettingPanel)
        {
            SetStage(StagerManager.Stage.fourthStage, "개발중", "", 3);
        }
        else if (scrollbar.value >= 0.39f && scrollbar.value <= 0.5f && !buttonManager.isCharPanel && !buttonManager.isTitleSettingPanel)
        {
            SetStage(StagerManager.Stage.fifthStage, "Gritty Dash", "4Track", 4);
        }
        else if (scrollbar.value >= 0.51f  && !buttonManager.isCharPanel && !buttonManager.isTitleSettingPanel)
        {
            SetStage(StagerManager.Stage.fourthStage, "개발중", "", 3);
        }
       
    }
    void SetStage(StagerManager.Stage stage, string title, string track, int waveIndex) //스테이지 설정
    {
        StagerManager.instance.currentStage = stage;
        titlename.text = title;
        tracks.text = track;

        // 모든 Wave 오브젝트를 비활성화
        for (int i = 0; i < Wave.Length; i++)
        {
            Wave[i].SetActive(i == waveIndex);
        }
    }
    public void TabClick() //노래 오른쪽으로 이동
    {
        if (targetIndex < SIZE - 1) 
        {
            AudioManager.instance.PlaySound(transform.position, 3, Random.Range(1f, 1f), 1);
            targetIndex++;
            targetPos = pos[targetIndex];
        }
    }

    public void TabBackClick()//노래 왼쪽으로 이동
    {
        if (targetIndex > 0) 
        {
            AudioManager.instance.PlaySound(transform.position, 3, Random.Range(1f, 1f), 1);

            targetIndex--;
            targetPos = pos[targetIndex];
        }
    }

}
