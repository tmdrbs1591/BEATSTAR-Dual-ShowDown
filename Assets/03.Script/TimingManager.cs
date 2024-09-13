using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
     PlaayerController thePlayerController;
    public List<GameObject> boxNoteList = new List<GameObject>(); // 박스 노트 리스트

    public int[] judgmentRecord = new int[5]; //perfect...기록 저장

    [SerializeField] Transform Center = null; // 타이밍 박스의 중심 위치
    [SerializeField] RectTransform[] timingRect = null;// 타이밍 박스 UI 배열

    Vector2[] timingBoxs = null; // 타이밍 박스들의 x 범위 저장 배열


    EffectManager theEffect;
    ScoreManager theScoreManager;
    ComboManager thecomboManager;
    FeverManager theFeverManager;

    public FeverManager feverManager;
    public GameObject EffectPrefab; // 일반 노트 맞춤 효과 프리팹
    public GameObject childEffectPrefab;
    public GameObject FeverchildEffectPrefab;
    public GameObject laserRotation;
    public SecondStage secondStage;
    public bool camerashake = true;
    public Camera mainCamera;

    private void Update()
    {
        
    }
    void Start()
    {
        // 현재 활성화된 GameObject에서 스크립트를 찾음
        thePlayerController = gameObject.GetComponent<PlaayerController>();
        theFeverManager = FindObjectOfType<FeverManager>();
        theScoreManager = FindObjectOfType<ScoreManager>();
        theEffect = FindObjectOfType<EffectManager>();
        thecomboManager = FindObjectOfType<ComboManager>();

        timingBoxs = new Vector2[timingRect.Length];
        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                              Center.localPosition.x + timingRect[i].rect.width / 2);
        }
    }

    public void CheckTiming()
    {
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;

            for (int x = 0; x < timingBoxs.Length; x++)
            {
                if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
                {
                    SetActiveRecursively(boxNoteList[i], false);
                    boxNoteList.RemoveAt(i);

                    if (x < timingBoxs.Length - 1)
                        theEffect.NoteHitEffect();

                    theEffect.judgementEffect(x);
                    return;
                }
            }
        }
        theEffect.judgementEffect(timingBoxs.Length);
    }

    public void CheckTimingWithKey(string key)    // 타이밍 체크 함수 (키보드 입력에 따른 노트 처리)
    {
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            Note noteComponent = boxNoteList[i].GetComponent<Note>();
            if (noteComponent != null && noteComponent.noteKey == key)
            {
                float t_notePosX = boxNoteList[i].transform.localPosition.x;

                for (int x = 0; x < timingBoxs.Length; x++)   // 노트의 위치가 타이밍 박스 내에 있는지 검사
                {
                    if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
                    {
                        AudioManager.instance.PlaySound(transform.position, 0, Random.Range(1f, 1f), 1);
                        
                        if (camerashake) {
                            CameraShake.instance.Shake();   // 카메라 흔들림 효과
                        }

                       
                        GameObject parentEffect = Instantiate(EffectPrefab, boxNoteList[i].transform.position, Quaternion.identity); // 노트 맞춤 효과 생성 및 파괴
                        Destroy(parentEffect, 2f);
                        foreach (Transform child in boxNoteList[i].transform)
                        {
                            if (!feverManager.feverTime)
                            {
                               // Destroy(Instantiate(childEffectPrefab, child.position, Quaternion.identity, parentEffect.transform),2f);
                                ObjectPool.SpawnFromPool("NoteEffect", child.position, Quaternion.identity);
                                ObjectPool.SpawnFromPool("NoteEffect2", child.position, Quaternion.identity);
                            }
                            else
                            {
                                Destroy(Instantiate(FeverchildEffectPrefab, child.position, Quaternion.identity, parentEffect.transform),2f);
                                ObjectPool.SpawnFromPool("NoteEffect2", child.position, Quaternion.identity);

                            }
                           
                        }
                        // 노트 비활성화 및 리스트에서 제거
                        SetActiveRecursively(boxNoteList[i], false);
                        GameObject destroyedNote = boxNoteList[i];
                        boxNoteList.RemoveAt(i);

                        if (x < timingBoxs.Length - 1)
                            theEffect.NoteHitEffect();

                        // 콤보 증가, 점수 증가, 피버 상태 관리, 판정 기록 등 처리
                        theScoreManager.IncreaseScore(x);//점수증가

                       if ((Input.GetKey(KeySetting.keys[KeyAction.Q]) && Input.GetKey(KeySetting.keys[KeyAction.W]) && Input.GetKey(KeySetting.keys[KeyAction.E])))
                            return;
                            theFeverManager.IncreaseFever(x);//판정연출
                         judgmentRecord[x]++;//판정기록
                        theEffect.judgementEffect(x);//판 등장

                        Destroy(destroyedNote);

                        return;
                    }
                }
            }
        }
       // thePlayerController.CurHP--;
        //thecomboManager.ResetCombo(); //미스
       // theEffect.judgementEffect(timingBoxs.Length);
    }

    void SetActiveRecursively(GameObject obj, bool active)
    {
        obj.SetActive(active);
        foreach (Transform child in obj.transform)
        {
            SetActiveRecursively(child.gameObject, active);
        }
    }
    public int[] GetJudgementRecord()  // 판정 기록 배열 반환
    {
        return judgmentRecord;
    }
}
