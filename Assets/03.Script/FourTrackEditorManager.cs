using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; // UnityEditor ���ӽ����̽� �߰�
using System;
using System.IO;


public class FourTrackEditorManager : MonoBehaviour
{
    [SerializeField] List<NoteInfo> map = new List<NoteInfo>(); // ��Ʈ ������ ��� ����Ʈ
    public float time;// ���� �ð� 
    public FourTrackEditNote editDNote;// D ��Ʈ�� ���� ������
    public FourTrackEditNote editFNote;// F ��Ʈ�� ���� ������
    public FourTrackEditNote editJNote;// J ��Ʈ�� ���� ������
    public FourTrackEditNote editKNote;// K ��Ʈ�� ���� ������

    public FourTrackEditNote editDFNote;// DF ��Ʈ�� ���� ������
    public FourTrackEditNote editJKNote;// EW ��Ʈ�� ���� ������
    public FourTrackEditNote editSpaceNote;// Space ��Ʈ�� ���� ������
    public AudioSource audioSource;// ���� ����� ���� AudioSource

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
            GameObject noteObject = Instantiate(prefab); // ������ ����
            FourTrackEditNote editNoteComponent = noteObject.GetComponent<FourTrackEditNote>();
            editNoteComponent.Sart(noteType, time); // ��Ʈ �ʱ�ȭ

            NoteInfo newNote = new NoteInfo(noteType, time, noteObject);
            map.Add(newNote);// ����Ʈ�� ���ο� ��Ʈ ���� �߰�
        }
        else
        {
            Debug.LogError("Prefab not found for note type: " + noteType);
        }
    }

    void Save()
    {
#if UNITY_EDITOR
        SerializableList<NoteInfo> r = new SerializableList<NoteInfo>();  // �ø�������� ������ ����Ʈ ����
        r.list = map;  // ��Ʈ ���� ����Ʈ ����
        var path = EditorUtility.SaveFilePanel("Save your map", Application.dataPath, DateTime.Now.ToString("yyyyMMddHHmmss") + ".json", "json"); // �����ϴ� â ����
        using (StreamWriter sw = new StreamWriter(path)) // StreamWriter�� ����� ���Ͽ� ����
        {
            sw.WriteLine(JsonUtility.ToJson(r)); // Json���� ��ȯ�Ͽ� ���Ͽ� ��
        }
#else
        Debug.LogError("Save can only be called from within the Unity Editor."); // �����Ϳ����� ���� �����ϵ��� ��� ���
#endif
    }

    void Start()
    {
        audioSource.Play();// ���� ���� �� ����� ���
    }

    void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            float scrollAmount = Mathf.Sign(Input.mouseScrollDelta.y) / 2;
            time += scrollAmount; // ���콺 ��ũ�ѷ� �ð� ����
            audioSource.time = Mathf.Clamp(audioSource.time + scrollAmount, 0, audioSource.clip.length); // ����� �ð� ����
        }

        // �� Ű �Է¿� ���� ��Ʈ �߰�
        if (Input.GetKeyDown(KeyCode.D)) Add("D");
        if (Input.GetKeyDown(KeyCode.F)) Add("F");
        if (Input.GetKeyDown(KeyCode.J)) Add("J");
        if (Input.GetKeyDown(KeyCode.K)) Add("K");
        if (Input.GetKeyDown(KeyCode.S)) Add("DF");
        if (Input.GetKeyDown(KeyCode.L)) Add("JK");

        // "QWE" �Է� �� 0.1�� �������� ��� ��Ʈ �߰�
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Time.time >= nextNoteTime)
            {
                Add("QWE");
                nextNoteTime = Time.time + 0.07f; // ���� ��Ʈ �ð� ����
            }
        }

        // �齺���̽� �Է� �� ���� �ֱٿ� �߰��� ��Ʈ ����
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (map.Count > 0)
            {
                // ���� �ֱٿ� �߰��� ��Ʈ ����
                NoteInfo removedNote = map[map.Count - 1];
                map.RemoveAt(map.Count - 1);

                // ������ ��Ʈ�� ���� ������Ʈ�� ����
                Destroy(removedNote.gameObject);

                Debug.Log("Removed note: " + removedNote.note + " at timing: " + removedNote.timing);
            }
            else
            {
                Debug.Log("Map is empty, nothing to remove.");
            }
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S)) Save();

        time += Time.deltaTime;// �ð� ������Ʈ
        // �����̽� �Է� �� ���� ���� �� ���
        if (Input.GetKeyDown(KeyCode.Space)) { audioSource.Stop(); audioSource.Play(); time = 0; }
    }

    float nextNoteTime = 0f; // ���� ��Ʈ �߰� �ð��� �����ϴ� ����

    [System.Serializable] // �ø�������� ������ ����Ʈ Ŭ���� ����
    public class SerializableList<T>
    {
        public List<T> list;
    }
}
