using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStage : MonoBehaviour // 코드 삭제 예정이라 주석 없음
{

    public GameObject ClearPanel; // 클리어패널
    public PlaayerController thePlayerController; // 플레이어 컨트롤러
    public int bpm = 120; // bpm
    double currentTime = 0d;
    int noteCount = 0; // 생성된 노트의 수

    enum BeatType // 비트 타입
    {
        Whole = 1,
        Half = 2,
        Quarter = 4,
        Eighth = 8,
        Sixteenth = 16
    }

    public ObjectManager objectManager;

    [SerializeField] Transform tfNoteAppear = null;
    [SerializeField] GameObject go1 = null; // Q노트
    [SerializeField] GameObject go2 = null;//W노트
    [SerializeField] GameObject go3 = null;//E노트
    [SerializeField] GameObject go4 = null;//QW노트

    TimingManager theTimingManager;
    EffectManager theEffectManager;
    ComboManager thecomboManager;

    void Start()
    {

        thecomboManager = FindObjectOfType<ComboManager>();
        theEffectManager = FindObjectOfType<EffectManager>();
        theTimingManager = GetComponent<TimingManager>();
    }

    void FixedUpdate()
    {
        if (thePlayerController != null)
        {
            thePlayerController = FindObjectOfType<PlaayerController>();

        }
        double beatInterval = 60d / bpm;

        currentTime += Time.deltaTime;
        #region beat
        if (noteCount < 16)
        {
            if (currentTime >= beatInterval * 0.79f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 0.79f;
                noteCount++;
            }
        }
        if (noteCount < 30)
        {
            if (currentTime >= beatInterval * 0.79f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 0.79f;
                noteCount++;
            }
        }
        if (noteCount < 31)
        {
            if (currentTime >= beatInterval * 0.79f)
            {
                SpawnQWNote();
                currentTime -= beatInterval * 0.79f;
                noteCount++;
            }
        }
        else if (noteCount < 32)
        {
            if (currentTime >= beatInterval * 1.23f)
            {
                SpawnQNote();
                currentTime -= beatInterval * 1.23f;
                noteCount++;
            }
        }
        else if (noteCount < 33)
        {
            if (currentTime >= beatInterval * 0.5f)
            {
                SpawnQNote();
                currentTime -= beatInterval * 0.5f;
                noteCount++;
            }
        }
        else if (noteCount < 46)
        {
            if (currentTime >= beatInterval * 0.79f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 0.79f;
                noteCount++;
            }
        }
        else if (noteCount < 47)
        {
            if (currentTime >= beatInterval * 0.79f)
            {
                SpawnQWNote();
                currentTime -= beatInterval * 0.79f;
                noteCount++;
            }
        }
        else if (noteCount < 48)
        {
            if (currentTime >= beatInterval * 1.23f)
            {
                SpawnWNote();
                currentTime -= beatInterval * 1.23f;
                noteCount++;
            }
        }
        else if (noteCount < 49)
        {
            if (currentTime >= beatInterval * 0.5f)
            {
                SpawnWNote();
                currentTime -= beatInterval * 0.5f;
                noteCount++;
            }
        }
        else if (noteCount < 62)
        {
            if (currentTime >= beatInterval * 0.79f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 0.79f;
                noteCount++;
            }
        }
        else if (noteCount < 63)
        {
            if (currentTime >= beatInterval * 0.79f)
            {
                SpawnQWNote();
                currentTime -= beatInterval * 0.79f;
                noteCount++;
            }
        }
        else if (noteCount < 64)
        {
            if (currentTime >= beatInterval * 1.23f)
            {
                SpawnENote();
                currentTime -= beatInterval * 1.23f;
                noteCount++;
            }
        }
        else if (noteCount < 65)
        {
            if (currentTime >= beatInterval * 0.5f)
            {
                SpawnENote();
                currentTime -= beatInterval * 0.5f;
                noteCount++;
            }
        }
        else if (noteCount < 78)
        {
            if (currentTime >= beatInterval * 0.79f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 0.79f;
                noteCount++;
            }
        }
        else if (noteCount < 79)
        {
            if (currentTime >= beatInterval * 0.79f)
            {
                SpawnQNote();
                currentTime -= beatInterval * 0.79f;
                noteCount++;
            }
        }
        else if (noteCount < 80)
        {
            if (currentTime >= beatInterval * 0.5f)
            {
                SpawnQNote();

                currentTime -= beatInterval * 0.5f;
                noteCount++;
            }
        }
        else if (noteCount < 81)
        {
            if (currentTime >= beatInterval * 1.23f)
            {
                
                SpawnRandomNote();
                currentTime -= beatInterval * 1.23f;
                noteCount++;
            }
        }
        else if (noteCount < 94)
        {
            if (currentTime >= beatInterval * 0.79f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 0.79f;
                noteCount++;
            }
        }
        else if (noteCount < 95)
        {
            if (currentTime >= beatInterval * 0.78f)
            {
                SpawnWNote();
                currentTime -= beatInterval * 0.78f;
                noteCount++;
            }
        }
        else if (noteCount < 96)
        {
            if (currentTime >= beatInterval * 0.5f)
            {
                SpawnWNote();
                currentTime -= beatInterval * 0.5f;
                noteCount++;
            }
        }
        else if (noteCount < 97)
        {
            if (currentTime >= beatInterval * 1.23f)
            {
                SpawnRandomNote();

                currentTime -= beatInterval * 1.23f;
                noteCount++;
            }
        }
        else if (noteCount < 108)
        {
            if (currentTime >= beatInterval * 0.79f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 0.79f;
                noteCount++;
            }
        }
        else if (noteCount < 109)
        {
            if (currentTime >= beatInterval * 1f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 1f;
                noteCount++;
            }
        }
        else if (noteCount < 110)
        {
            if (currentTime >= beatInterval * 0.5f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 0.5f;
                noteCount++;
            }
        }
        else if (noteCount < 111)
        {
            if (currentTime >= beatInterval * 1f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 1f;
                noteCount++;
            }
        }
        else if (noteCount < 112)
        {
            if (currentTime >= beatInterval * 0.5f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 0.5f;
                noteCount++;
            }
        }
        else if (noteCount < 113)
        {
            if (currentTime >= beatInterval * 1f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 1f;
                noteCount++;
            }
        }
        else if (noteCount < 121)
        {
            if (currentTime >= beatInterval * 0.83f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 0.83f;
                noteCount++;
            }
        }
     
        else if (noteCount < 122)
        {
            if (currentTime >= beatInterval * 1.15f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 1.15f;
                noteCount++;
            }
        }
        else if (noteCount < 123)
        {
            if (currentTime >= beatInterval * 0.45f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 0.45f;
                noteCount++;
            }
        }
        else if (noteCount < 124)
        {
            if (currentTime >= beatInterval * 1.15f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 1.15f;
                noteCount++;
            }
        }
        else if (noteCount < 125)
        {
            if (currentTime >= beatInterval * 0.45f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 0.45f;
                noteCount++;
            }
        }
        else if (noteCount < 126)
        {
            if (currentTime >= beatInterval * 1.15f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 1.15f;
                noteCount++;
            }
        }
        else if (noteCount < 127)
        {
            if (currentTime >= beatInterval * 0.45f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 0.45f;
                noteCount++;
            }
        }
        else if (noteCount < 128)
        {
            if (currentTime >= beatInterval * 1.15f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 1.15f;
                noteCount++;
            }
        }
        else if (noteCount < 129)
        {
            if (currentTime >= beatInterval * 0.45f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 0.45f;
                noteCount++;
            }
        }
        else if (noteCount < 141)
        {
            if (currentTime >= beatInterval * 0.8f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 0.8f;
                noteCount++;
            }
        }
        else if (noteCount < 142)
        {
            if (currentTime >= beatInterval * 0.45f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 0.45f;
                noteCount++;
            }
        }
        else if (noteCount < 143)
        {
            if (currentTime >= beatInterval * 1.15f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 1.15f;
                noteCount++;
            }
        }
        else if (noteCount < 144)
        {
            if (currentTime >= beatInterval * 0.45f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 0.45f;
                noteCount++;
            }
        }
        else if (noteCount < 145)
        {
            if (currentTime >= beatInterval * 1.15f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 1.15f;
                noteCount++;
            }
        }
        else if (noteCount < 153)
        {
            if (currentTime >= beatInterval * 0.76f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 0.76f;
                noteCount++;
            }
        }
        else if (noteCount < 154)
        {
            if (currentTime >= beatInterval * 1.15f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 1.15f;
                noteCount++;
            }
        }
        else if (noteCount < 155)
        {
            if (currentTime >= beatInterval * 0.43f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 0.43f;
                noteCount++;
            }
        }
        else if (noteCount < 156)
        {
            if (currentTime >= beatInterval * 1.15f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 1.15f;
                noteCount++;
            }
        }
        else if (noteCount < 157)
        {
            if (currentTime >= beatInterval * 0.43f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 0.43f;
                noteCount++;
            }
        }
        else if (noteCount < 158)
        {
            if (currentTime >= beatInterval * 0.8f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 0.8f;
                noteCount++;
            }
        }
      
        else if (noteCount < 159)
        {
            if (currentTime >= beatInterval * 0.9f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 0.9f;
                noteCount++;
            }
        }
        else if (noteCount < 160)
        {
            if (currentTime >= beatInterval * 0.42f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 0.42f;
                noteCount++;
            }
        }
        else if (noteCount < 161)
        {
            if (currentTime >= beatInterval * 1.3f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 1.3f;
                noteCount++;
            }
        }
        else if (noteCount < 167)
        {
            if (currentTime >= beatInterval * 3.172f)
            {
                SpawnQWNote();
                currentTime -= beatInterval * 3.172f;
                noteCount++;
            }
        }
        else if (noteCount < 168)
        {
            if (currentTime >= beatInterval * 3.1682f)
            {
                ClearPanel.SetActive(true);
                currentTime -= beatInterval * 3.1682f;
                noteCount++;
            }
        }
        #endregion
        void SpawnRandomNote()
        {
            int randomIndex = Random.Range(1, 4);
            GameObject t_note = null;
            switch (randomIndex)
            {
                case 1:

                    t_note = Instantiate(go1, tfNoteAppear.position, Quaternion.identity);
                    break;
                case 2:
                    t_note = Instantiate(go2, tfNoteAppear.position, Quaternion.identity);
                    break;
                case 3:
                    t_note = Instantiate(go3, tfNoteAppear.position, Quaternion.identity);
                    break;
                default:
                    break;
            }
            if (t_note != null)
            {
                t_note.transform.SetParent(this.transform);
                theTimingManager.boxNoteList.Add(t_note);
            }
        }
    }
    void SpawnQNote()
    {
        GameObject t_note = Instantiate(go1, tfNoteAppear.position, Quaternion.identity);
        if (t_note != null)
        {
            t_note.transform.SetParent(this.transform);
            theTimingManager.boxNoteList.Add(t_note);
        }
    }
    void SpawnWNote()
    {
        GameObject t_note = Instantiate(go2, tfNoteAppear.position, Quaternion.identity);
        if (t_note != null)
        {
            t_note.transform.SetParent(this.transform);
            theTimingManager.boxNoteList.Add(t_note);
        }
    }
    void SpawnENote()
    {
        GameObject t_note = Instantiate(go3, tfNoteAppear.position, Quaternion.identity);
        if (t_note != null)
        {
            t_note.transform.SetParent(this.transform);
            theTimingManager.boxNoteList.Add(t_note);
        }
    }
    void SpawnQWNote()
    {
        GameObject t_note = Instantiate(go4, tfNoteAppear.position, Quaternion.identity);
        if (t_note != null)
        {
            t_note.transform.SetParent(this.transform);
            theTimingManager.boxNoteList.Add(t_note);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {

            if (collision.GetComponent<Note>().GetNoteFlag())
            {
                thePlayerController.CurHP--;
                theEffectManager.judgementEffect(4);
                thecomboManager.ResetCombo();
            }


            theTimingManager.boxNoteList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
        
    }
}
