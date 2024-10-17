using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; // UnityEditor 네임스페이스 추가
using System;
using System.IO;


public class FourTrackEditorManager : MonoBehaviour
{
    [SerializeField] List<NoteInfo> map = new List<NoteInfo>(); // 노트 정보를 담는 리스트
    public float time;// 현재 시간 
    public FourTrackEditNote editDNote;// D 노트에 대한 프리팹
    public FourTrackEditNote editFNote;// F 노트에 대한 프리팹
    public FourTrackEditNote editJNote;// J 노트에 대한 프리팹
    public FourTrackEditNote editKNote;// K 노트에 대한 프리팹

    public FourTrackEditNote editDFNote;// DF 노트에 대한 프리팹
    public FourTrackEditNote editJKNote;// EW 노트에 대한 프리팹
    public FourTrackEditNote editSpaceNote;// Space 노트에 대한 프리팹
    public AudioSource audioSource;// 음악 재생을 위한 AudioSource

    void Add(string noteType)
    {
        GameObject prefab = null;
        switch (noteType)
        {
            case "D": prefab = editDNote.gameObject; break;
            case "F": prefab = editFNote.gameObject; break;
            case "J": prefab = editJNote.gameObject; break;
            case "K": prefab = editKNote.gameObject; break;
            case "DF": prefab = editDFNote.gameObject; break;
            case "JK": prefab = editJKNote.gameObject; break;
            case "QWE": prefab = editSpaceNote.gameObject; break;
        }

        if (prefab != null)
        {
            GameObject noteObject = Instantiate(prefab); // 프리팹 복제
            FourTrackEditNote editNoteComponent = noteObject.GetComponent<FourTrackEditNote>();
            editNoteComponent.Sart(noteType, time); // 노트 초기화

            NoteInfo newNote = new NoteInfo(noteType, time, noteObject);
            map.Add(newNote);// 리스트에 새로운 노트 정보 추가
        }
        else
        {
            Debug.LogError("Prefab not found for note type: " + noteType);
        }
    }

    void Save()
    {
#if UNITY_EDITOR
        SerializableList<NoteInfo> r = new SerializableList<NoteInfo>();  // 시리얼라이즈 가능한 리스트 생성
        r.list = map;  // 노트 정보 리스트 저장
        var path = EditorUtility.SaveFilePanel("Save your map", Application.dataPath, DateTime.Now.ToString("yyyyMMddHHmmss") + ".json", "json"); // 저장하는 창 열기
        using (StreamWriter sw = new StreamWriter(path)) // StreamWriter를 사용해 파일에 쓰기
        {
            sw.WriteLine(JsonUtility.ToJson(r)); // Json으로 변환하여 파일에 씀
        }
#else
        Debug.LogError("Save can only be called from within the Unity Editor."); // 에디터에서만 저장 가능하도록 경고 출력
#endif
    }

    void Start()
    {
        audioSource.Play();// 게임 시작 시 오디오 재생
    }

    void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            float scrollAmount = Mathf.Sign(Input.mouseScrollDelta.y) / 2;
            time += scrollAmount; // 마우스 스크롤로 시간 조정
            audioSource.time = Mathf.Clamp(audioSource.time + scrollAmount, 0, audioSource.clip.length); // 오디오 시간 조정
        }

        // 각 키 입력에 따라 노트 추가
        if (Input.GetKeyDown(KeyCode.D)) Add("D");
        if (Input.GetKeyDown(KeyCode.F)) Add("F");
        if (Input.GetKeyDown(KeyCode.J)) Add("J");
        if (Input.GetKeyDown(KeyCode.K)) Add("K");
        if (Input.GetKeyDown(KeyCode.S)) Add("DF");
        if (Input.GetKeyDown(KeyCode.L)) Add("JK");

        // "QWE" 입력 시 0.1초 간격으로 계속 노트 추가
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Time.time >= nextNoteTime)
            {
                Add("QWE");
                nextNoteTime = Time.time + 0.07f; // 다음 노트 시간 갱신
            }
        }

        // 백스페이스 입력 시 가장 최근에 추가된 노트 삭제
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (map.Count > 0)
            {
                // 가장 최근에 추가된 노트 삭제
                NoteInfo removedNote = map[map.Count - 1];
                map.RemoveAt(map.Count - 1);

                // 삭제된 노트의 게임 오브젝트도 삭제
                Destroy(removedNote.gameObject);

                Debug.Log("Removed note: " + removedNote.note + " at timing: " + removedNote.timing);
            }
            else
            {
                Debug.Log("Map is empty, nothing to remove.");
            }
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S)) Save();

        time += Time.deltaTime;// 시간 업데이트
        // 스페이스 입력 시 음악 정지 후 재생
        if (Input.GetKeyDown(KeyCode.Space)) { audioSource.Stop(); audioSource.Play(); time = 0; }
    }

    float nextNoteTime = 0f; // 다음 노트 추가 시간을 저장하는 변수

    [System.Serializable] // 시리얼라이즈 가능한 리스트 클래스 정의
    public class SerializableList<T>
    {
        public List<T> list;
    }
}
