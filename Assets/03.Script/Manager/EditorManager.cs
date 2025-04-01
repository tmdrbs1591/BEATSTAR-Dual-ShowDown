using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; // UnityEditor ���ӽ����̽� �߰�
using System;
using System.IO;

[System.Serializable]
public class NoteInfo
{
    public string note; // ��Ʈ Ÿ���� �����ϴ� ���ڿ�
    public float timing;// ��Ʈ�� Ÿ�̹�(�ð�) ����
    public GameObject gameObject; // ��Ʈ�� �����ϴ� ���� ������Ʈ

    // ������
    public NoteInfo(string noteType, float time, GameObject obj)
    {
        note = noteType;
        timing = time;
        gameObject = obj;
    }
}

public class EditorManager : MonoBehaviour
{
    [SerializeField] List<NoteInfo> map = new List<NoteInfo>(); // ��Ʈ ������ ��� ����Ʈ
    public float time;// ���� �ð� 
    public EditNote editQNote;// Q ��Ʈ�� ���� ������
    public EditNote editWNote;// W ��Ʈ�� ���� ������
    public EditNote editENote;// E ��Ʈ�� ���� ������
    public EditNote editQWNote;// QW ��Ʈ�� ���� ������
    public EditNote editEWNote;// EW ��Ʈ�� ���� ������
    public EditNote editSpaceNote;// Space ��Ʈ�� ���� ������
    public AudioSource audioSource;// ���� ����� ���� AudioSource

    void Add(string noteType)
    {
        GameObject prefab = null;
        switch (noteType)
        {
            case "Q": prefab = editQNote.gameObject; break;
            case "W": prefab = editWNote.gameObject; break;
            case "E": prefab = editENote.gameObject; break;
            case "QW": prefab = editQWNote.gameObject; break;
            case "EW": prefab = editEWNote.gameObject; break;
            case "QWE": prefab = editSpaceNote.gameObject; break;
        }

        if (prefab != null)
        {
            GameObject noteObject = Instantiate(prefab); // ������ ����
            EditNote editNoteComponent = noteObject.GetComponent<EditNote>();
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
        if (Input.GetKeyDown(KeyCode.Q)) Add("Q");
        if (Input.GetKeyDown(KeyCode.W)) Add("W");
        if (Input.GetKeyDown(KeyCode.E)) Add("E");
        if (Input.GetKeyDown(KeyCode.Alpha1)) Add("QW");
        if (Input.GetKeyDown(KeyCode.Alpha2)) Add("EW");

        // "QWE" �Է� �� 0.1�� �������� ��� ��Ʈ �߰�
        if (Input.GetKey(KeyCode.Alpha3))
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
