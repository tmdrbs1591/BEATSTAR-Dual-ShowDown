using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
     PlaayerController thePlayerController;
    public List<GameObject> boxNoteList = new List<GameObject>(); // �ڽ� ��Ʈ ����Ʈ

    public int[] judgmentRecord = new int[5]; //perfect...��� ����

    [SerializeField] Transform Center = null; // Ÿ�̹� �ڽ��� �߽� ��ġ
    [SerializeField] RectTransform[] timingRect = null;// Ÿ�̹� �ڽ� UI �迭

    Vector2[] timingBoxs = null; // Ÿ�̹� �ڽ����� x ���� ���� �迭


    EffectManager theEffect;
    ScoreManager theScoreManager;
    ComboManager thecomboManager;
    FeverManager theFeverManager;

    public FeverManager feverManager;
    public GameObject EffectPrefab; // �Ϲ� ��Ʈ ���� ȿ�� ������
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
        // ���� Ȱ��ȭ�� GameObject���� ��ũ��Ʈ�� ã��
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

    public void CheckTimingWithKey(string key)    // Ÿ�̹� üũ �Լ� (Ű���� �Է¿� ���� ��Ʈ ó��)
    {
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            Note noteComponent = boxNoteList[i].GetComponent<Note>();
            if (noteComponent != null && noteComponent.noteKey == key)
            {
                float t_notePosX = boxNoteList[i].transform.localPosition.x;

                for (int x = 0; x < timingBoxs.Length; x++)   // ��Ʈ�� ��ġ�� Ÿ�̹� �ڽ� ���� �ִ��� �˻�
                {
                    if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
                    {
                        AudioManager.instance.PlaySound(transform.position, 0, Random.Range(1f, 1f), 1);
                        
                        if (camerashake) {
                            CameraShake.instance.Shake();   // ī�޶� ��鸲 ȿ��
                        }

                       
                        GameObject parentEffect = Instantiate(EffectPrefab, boxNoteList[i].transform.position, Quaternion.identity); // ��Ʈ ���� ȿ�� ���� �� �ı�
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
                        // ��Ʈ ��Ȱ��ȭ �� ����Ʈ���� ����
                        SetActiveRecursively(boxNoteList[i], false);
                        GameObject destroyedNote = boxNoteList[i];
                        boxNoteList.RemoveAt(i);

                        if (x < timingBoxs.Length - 1)
                            theEffect.NoteHitEffect();

                        // �޺� ����, ���� ����, �ǹ� ���� ����, ���� ��� �� ó��
                        theScoreManager.IncreaseScore(x);//��������

                       if ((Input.GetKey(KeySetting.keys[KeyAction.Q]) && Input.GetKey(KeySetting.keys[KeyAction.W]) && Input.GetKey(KeySetting.keys[KeyAction.E])))
                            return;
                            theFeverManager.IncreaseFever(x);//��������
                         judgmentRecord[x]++;//�������
                        theEffect.judgementEffect(x);//�� ����

                        Destroy(destroyedNote);

                        return;
                    }
                }
            }
        }
       // thePlayerController.CurHP--;
        //thecomboManager.ResetCombo(); //�̽�
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
    public int[] GetJudgementRecord()  // ���� ��� �迭 ��ȯ
    {
        return judgmentRecord;
    }
}
