using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class FourTrackStageBase : MonoBehaviour
{
    public ParallaxScroll map;// 배경 스크롤 관리 클래스
    public GameObject SpeedEffect;// 속도 효과 오브젝트
    public GameObject ClearPanel; // 클리어 패널
    public GameObject Warning; // 경고 메시지
    public AudioSource Song;// 음악 재생을 담당하는 오디오 소스
    public PlaayerController thePlayerController; // 플레이어 컨트롤러 스크립트
    public Animator CameraAnim; // 카메라 애니메이터
    public int bpm = 120; // 음악의 BPM
    public double currentTime = 0d;// 현재 시간
    public int noteCount = 0; // 생성된 노트의 수

    [SerializeField] public GameObject Glitch;//글리치 이펙트
    [SerializeField] public GameObject Flash;//플래시 이펙트


    enum BeatType// 리듬의 종류 열거형
    {
        Whole = 1,
        Half = 2,
        Quarter = 4,
        Eighth = 8,
        Sixteenth = 16
    }

    protected ObjectManager objectManager; // 오브젝트 매니저 클래스
    protected GameObject PowerEffect;// 파워 효과 오브젝트

    [SerializeField] public Transform tfNoteAppear = null;// 노트가 생성될 위치
    [SerializeField] public GameObject D_Note = null;// 노트 오브젝트 1
    [SerializeField] public GameObject F_Note = null;// 노트 오브젝트 2
    [SerializeField] public GameObject J_Note = null;// 노트 오브젝트 3
    [SerializeField] public GameObject K_Note = null;// 노트 오브젝트 4
    [SerializeField] public GameObject DF_Note = null;// 노트 오브젝트 5
    [SerializeField] public GameObject JK_Note = null;// 노트 오브젝트 6
    [SerializeField] public GameObject QWE_Note = null;// 노트 오브젝트 6


    public TimingManager theTimingManager;// 타이밍 매니저    
    public EffectManager theEffectManager;// 이펙트 매니저
    public ComboManager thecomboManager;// 콤보 매니저

    [SerializeField] public List<NoteInfo> notemap = new List<NoteInfo>(); // 노트 정보 리스트


    public bool firstNote;// 첫 번째 노트 플래그

    public int allNotes; // 전체 노트 수
    public int maxNotes;// 최대 노트 수

    void Start()
    {
        Song.Stop(); // 음악 정지
        thecomboManager = FindObjectOfType<ComboManager>();// 콤보 매니저 찾기
        theEffectManager = FindObjectOfType<EffectManager>(); // 이펙트 매니저 찾기
        theTimingManager = GetComponent<TimingManager>();// 타이밍 매니저 찾기

        // JSON 파일에서 노트 맵 정보 읽어오기
        string path = Application.dataPath + "/Songs/" + DataManager.instance.songPath + ".json";
        using (StreamReader reader = new StreamReader(path))
        {
            notemap = JsonUtility.FromJson<FourTrackEditorManager.SerializableList<NoteInfo>>(reader.ReadToEnd()).list;
        }

        // 모든 노트 정보에 대해 처리
        foreach (NoteInfo e in notemap)
        {
            allNotes++;
            maxNotes++;
            StartCoroutine(QueueToSpawn(e)); // 생성할 노트 큐에 추가
        }
    }

    void FixedUpdate()
    {
        if (thePlayerController != null)
            thePlayerController = FindObjectOfType<PlaayerController>();// 플레이어 컨트롤러 찾기

        if (allNotes <= 0)
            StartCoroutine(Clear(3f)); // 모든 노트가 생성되면 클리어 처리

        if (firstNote)
            StartCoroutine(EffectTrue(0.4f, Glitch));// 첫 번째 노트가 생성되면 글리치 효과 발동
        if (allNotes <= maxNotes - 3)
        {
            StartCoroutine(EffectTrue(0.4f, Flash)); // 일정 수 이상의 노트가 생성되면 플래시 효과 발동
            StartCoroutine(EffectFalse(0.4f, Glitch)); // 글리치 효과 해제
        }

        currentTime += Time.deltaTime;// 현재 시간 증가

    }

    protected IEnumerator QueueToSpawn(NoteInfo e) // 노트 생성을 위한 큐
    {
        yield return new WaitForSeconds(e.timing); // 설정된 시간만큼 대기 후 생성
        if (!firstNote)
            Song.Play();// 첫 번째 노트가 생성되면 음악 재생 시작
        firstNote = true;
        switch (e.note) //노트의 이름에 따라 생성
        {
            case "D": SpawnDNote(); break;
            case "F": SpawnFNote(); break;
            case "J": SpawnJNote(); break;
            case "K": SpawnKNote(); break;
            case "DF": SpawnDFNote(); break;
            case "JK": SpawnJKNote(); break;
            case "QWE": SpawnSpaceNote(); break;
        }
        allNotes--;
        // 생성된 노트 수 감소
    }
    public IEnumerator Clear(float time)
    {
        yield return new WaitForSeconds(time);// 설정된 시간만큼 대기 후 클리어 패널 활성화
        ClearPanel.SetActive(true);
        PlayerScoreManager.instance.WinnerLoserPanel();
    }
    public IEnumerator EffectTrue(float time, GameObject Effect)   // 특정 시간 후 이펙트 활성화
    {
        yield return new WaitForSeconds(time);
        Effect.SetActive(true);
    }
    public IEnumerator EffectFalse(float time, GameObject Effect)    // 특정 시간 후 이펙트 비활성화
    {
        yield return new WaitForSeconds(time);
        Effect.SetActive(false);
    }
    IEnumerator NoteMinas()    // 노트 수 감소 코루틴 (사용하지 않는 것으로 보임)
    {
        yield return new WaitForSeconds(5f);
        allNotes--;
    }
    IEnumerator Effect()    // 효과 발동
    {
        yield return new WaitForSeconds(1.4f);



        map.MapSpeed *= 1000;

        PowerEffect.SetActive(true);
    }
    void SpawnRandomNote() // 랜덤 노트 생성
    {
        int randomIndex = Random.Range(1, 4); ; // 랜덤 인덱스 선택
        GameObject t_note = null;
        switch (randomIndex)
        {
            case 1:

                t_note = Instantiate(D_Note, tfNoteAppear.position, Quaternion.identity);
                break;
            case 2:
                t_note = Instantiate(F_Note, tfNoteAppear.position, Quaternion.identity);
                break;
            case 3:
                t_note = Instantiate(J_Note, tfNoteAppear.position, Quaternion.identity);
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
    void SpawnDoubleRandomNote()  // 더블 랜덤 노트 생성
    {
        int randomIndex = Random.Range(1, 3);// 랜덤 인덱스 선택
        GameObject t_note = null;
        switch (randomIndex)
        {
            case 1:

                t_note = Instantiate(DF_Note, tfNoteAppear.position, Quaternion.identity);
                break;
            case 2:
                t_note = Instantiate(JK_Note, tfNoteAppear.position, Quaternion.identity);
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
    void SpawnDNote()// D 노트 생성
    {
        GameObject t_note = Instantiate(D_Note, tfNoteAppear.position, Quaternion.identity);
        if (t_note != null)
        {
            t_note.transform.SetParent(this.transform);
            theTimingManager.boxNoteList.Add(t_note);
        }
    }
    void SpawnFNote()  // F노트 생성
    {
        GameObject t_note = Instantiate(F_Note, tfNoteAppear.position, Quaternion.identity);
        if (t_note != null)
        {
            t_note.transform.SetParent(this.transform);
            theTimingManager.boxNoteList.Add(t_note);
        }
    }
    void SpawnSpaceNote()  // QWE 노트 생성
    {
        GameObject t_note = Instantiate(QWE_Note, tfNoteAppear.position, Quaternion.identity);
        if (t_note != null)
        {
            t_note.transform.SetParent(this.transform);
            theTimingManager.boxNoteList.Add(t_note);
        }
    }
    void SpawnJNote()
    {
        GameObject t_note = Instantiate(J_Note, tfNoteAppear.position, Quaternion.identity);
        if (t_note != null)
        {
            t_note.transform.SetParent(this.transform);
            theTimingManager.boxNoteList.Add(t_note);
        }
    }

    void SpawnKNote()
    {
        GameObject t_note = Instantiate(K_Note, tfNoteAppear.position, Quaternion.identity);
        if (t_note != null)
        {
            t_note.transform.SetParent(this.transform);
            theTimingManager.boxNoteList.Add(t_note);
        }
    }
    void SpawnDFNote()
    {
        GameObject t_note = Instantiate(DF_Note, tfNoteAppear.position, Quaternion.identity);
        if (t_note != null)
        {
            t_note.transform.SetParent(this.transform);
            theTimingManager.boxNoteList.Add(t_note);
        }
    }
    void SpawnJKNote()
    {
        GameObject t_note = Instantiate(JK_Note, tfNoteAppear.position, Quaternion.identity);
        if (t_note != null)
        {
            t_note.transform.SetParent(this.transform);
            theTimingManager.boxNoteList.Add(t_note);
        }
    }
    IEnumerator CameraBounce(float time) //카메라 바운스
    {
        yield return new WaitForSeconds(time);
        CameraAnim.SetTrigger("Bounce");

    }
    private void OnTriggerExit2D(Collider2D collision)// 노트가 충돌 영역을 벗어날 때 호출
    {
        if (collision.CompareTag("Note"))// 충돌한 오브젝트가 노트인 경우
        {

            if (collision.GetComponent<Note>().GetNoteFlag())// 노트가 올바르게 처리된 경우
            {
                thePlayerController.TakeDamage(10);// 플레이어에게 데미지를 입힘
                theEffectManager.judgementEffect(4);// 판정 이펙트 표시
                thecomboManager.ResetCombo(); ; // 콤보 초기화
            }


            theTimingManager.boxNoteList.Remove(collision.gameObject); // 노트 리스트에서 제거
            Destroy(collision.gameObject);// 노트 오브젝트 파괴
        }

    }
    IEnumerator StartEffect()
    {
        Glitch.SetActive(true);// 글리치 효과 활성화
        yield return new WaitForSeconds(1.3f); // 일정 시간 대기 후
        Glitch.SetActive(false); // 글리치 효과 비활성화
        Flash.SetActive(true);// 플래시 효과 활성화

    }
}