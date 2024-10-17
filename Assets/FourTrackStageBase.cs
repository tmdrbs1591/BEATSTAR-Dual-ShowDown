using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class FourTrackStageBase : MonoBehaviour
{
    public ParallaxScroll map;// ��� ��ũ�� ���� Ŭ����
    public GameObject SpeedEffect;// �ӵ� ȿ�� ������Ʈ
    public GameObject ClearPanel; // Ŭ���� �г�
    public GameObject Warning; // ��� �޽���
    public AudioSource Song;// ���� ����� ����ϴ� ����� �ҽ�
    public PlaayerController thePlayerController; // �÷��̾� ��Ʈ�ѷ� ��ũ��Ʈ
    public Animator CameraAnim; // ī�޶� �ִϸ�����
    public int bpm = 120; // ������ BPM
    public double currentTime = 0d;// ���� �ð�
    public int noteCount = 0; // ������ ��Ʈ�� ��

    [SerializeField] public GameObject Glitch;//�۸�ġ ����Ʈ
    [SerializeField] public GameObject Flash;//�÷��� ����Ʈ


    enum BeatType// ������ ���� ������
    {
        Whole = 1,
        Half = 2,
        Quarter = 4,
        Eighth = 8,
        Sixteenth = 16
    }

    protected ObjectManager objectManager; // ������Ʈ �Ŵ��� Ŭ����
    protected GameObject PowerEffect;// �Ŀ� ȿ�� ������Ʈ

    [SerializeField] public Transform tfNoteAppear = null;// ��Ʈ�� ������ ��ġ
    [SerializeField] public GameObject D_Note = null;// ��Ʈ ������Ʈ 1
    [SerializeField] public GameObject F_Note = null;// ��Ʈ ������Ʈ 2
    [SerializeField] public GameObject J_Note = null;// ��Ʈ ������Ʈ 3
    [SerializeField] public GameObject K_Note = null;// ��Ʈ ������Ʈ 4
    [SerializeField] public GameObject DF_Note = null;// ��Ʈ ������Ʈ 5
    [SerializeField] public GameObject JK_Note = null;// ��Ʈ ������Ʈ 6
    [SerializeField] public GameObject QWE_Note = null;// ��Ʈ ������Ʈ 6


    public TimingManager theTimingManager;// Ÿ�̹� �Ŵ���    
    public EffectManager theEffectManager;// ����Ʈ �Ŵ���
    public ComboManager thecomboManager;// �޺� �Ŵ���

    [SerializeField] public List<NoteInfo> notemap = new List<NoteInfo>(); // ��Ʈ ���� ����Ʈ


    public bool firstNote;// ù ��° ��Ʈ �÷���

    public int allNotes; // ��ü ��Ʈ ��
    public int maxNotes;// �ִ� ��Ʈ ��

    void Start()
    {
        Song.Stop(); // ���� ����
        thecomboManager = FindObjectOfType<ComboManager>();// �޺� �Ŵ��� ã��
        theEffectManager = FindObjectOfType<EffectManager>(); // ����Ʈ �Ŵ��� ã��
        theTimingManager = GetComponent<TimingManager>();// Ÿ�̹� �Ŵ��� ã��

        // JSON ���Ͽ��� ��Ʈ �� ���� �о����
        string path = Application.dataPath + "/Songs/" + DataManager.instance.songPath + ".json";
        using (StreamReader reader = new StreamReader(path))
        {
            notemap = JsonUtility.FromJson<FourTrackEditorManager.SerializableList<NoteInfo>>(reader.ReadToEnd()).list;
        }

        // ��� ��Ʈ ������ ���� ó��
        foreach (NoteInfo e in notemap)
        {
            allNotes++;
            maxNotes++;
            StartCoroutine(QueueToSpawn(e)); // ������ ��Ʈ ť�� �߰�
        }
    }

    void FixedUpdate()
    {
        if (thePlayerController != null)
            thePlayerController = FindObjectOfType<PlaayerController>();// �÷��̾� ��Ʈ�ѷ� ã��

        if (allNotes <= 0)
            StartCoroutine(Clear(3f)); // ��� ��Ʈ�� �����Ǹ� Ŭ���� ó��

        if (firstNote)
            StartCoroutine(EffectTrue(0.4f, Glitch));// ù ��° ��Ʈ�� �����Ǹ� �۸�ġ ȿ�� �ߵ�
        if (allNotes <= maxNotes - 3)
        {
            StartCoroutine(EffectTrue(0.4f, Flash)); // ���� �� �̻��� ��Ʈ�� �����Ǹ� �÷��� ȿ�� �ߵ�
            StartCoroutine(EffectFalse(0.4f, Glitch)); // �۸�ġ ȿ�� ����
        }

        currentTime += Time.deltaTime;// ���� �ð� ����

    }

    protected IEnumerator QueueToSpawn(NoteInfo e) // ��Ʈ ������ ���� ť
    {
        yield return new WaitForSeconds(e.timing); // ������ �ð���ŭ ��� �� ����
        if (!firstNote)
            Song.Play();// ù ��° ��Ʈ�� �����Ǹ� ���� ��� ����
        firstNote = true;
        switch (e.note) //��Ʈ�� �̸��� ���� ����
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
        // ������ ��Ʈ �� ����
    }
    public IEnumerator Clear(float time)
    {
        yield return new WaitForSeconds(time);// ������ �ð���ŭ ��� �� Ŭ���� �г� Ȱ��ȭ
        ClearPanel.SetActive(true);
        PlayerScoreManager.instance.WinnerLoserPanel();
    }
    public IEnumerator EffectTrue(float time, GameObject Effect)   // Ư�� �ð� �� ����Ʈ Ȱ��ȭ
    {
        yield return new WaitForSeconds(time);
        Effect.SetActive(true);
    }
    public IEnumerator EffectFalse(float time, GameObject Effect)    // Ư�� �ð� �� ����Ʈ ��Ȱ��ȭ
    {
        yield return new WaitForSeconds(time);
        Effect.SetActive(false);
    }
    IEnumerator NoteMinas()    // ��Ʈ �� ���� �ڷ�ƾ (������� �ʴ� ������ ����)
    {
        yield return new WaitForSeconds(5f);
        allNotes--;
    }
    IEnumerator Effect()    // ȿ�� �ߵ�
    {
        yield return new WaitForSeconds(1.4f);



        map.MapSpeed *= 1000;

        PowerEffect.SetActive(true);
    }
    void SpawnRandomNote() // ���� ��Ʈ ����
    {
        int randomIndex = Random.Range(1, 4); ; // ���� �ε��� ����
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
    void SpawnDoubleRandomNote()  // ���� ���� ��Ʈ ����
    {
        int randomIndex = Random.Range(1, 3);// ���� �ε��� ����
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
    void SpawnDNote()// D ��Ʈ ����
    {
        GameObject t_note = Instantiate(D_Note, tfNoteAppear.position, Quaternion.identity);
        if (t_note != null)
        {
            t_note.transform.SetParent(this.transform);
            theTimingManager.boxNoteList.Add(t_note);
        }
    }
    void SpawnFNote()  // F��Ʈ ����
    {
        GameObject t_note = Instantiate(F_Note, tfNoteAppear.position, Quaternion.identity);
        if (t_note != null)
        {
            t_note.transform.SetParent(this.transform);
            theTimingManager.boxNoteList.Add(t_note);
        }
    }
    void SpawnSpaceNote()  // QWE ��Ʈ ����
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
    IEnumerator CameraBounce(float time) //ī�޶� �ٿ
    {
        yield return new WaitForSeconds(time);
        CameraAnim.SetTrigger("Bounce");

    }
    private void OnTriggerExit2D(Collider2D collision)// ��Ʈ�� �浹 ������ ��� �� ȣ��
    {
        if (collision.CompareTag("Note"))// �浹�� ������Ʈ�� ��Ʈ�� ���
        {

            if (collision.GetComponent<Note>().GetNoteFlag())// ��Ʈ�� �ùٸ��� ó���� ���
            {
                thePlayerController.TakeDamage(10);// �÷��̾�� �������� ����
                theEffectManager.judgementEffect(4);// ���� ����Ʈ ǥ��
                thecomboManager.ResetCombo(); ; // �޺� �ʱ�ȭ
            }


            theTimingManager.boxNoteList.Remove(collision.gameObject); // ��Ʈ ����Ʈ���� ����
            Destroy(collision.gameObject);// ��Ʈ ������Ʈ �ı�
        }

    }
    IEnumerator StartEffect()
    {
        Glitch.SetActive(true);// �۸�ġ ȿ�� Ȱ��ȭ
        yield return new WaitForSeconds(1.3f); // ���� �ð� ��� ��
        Glitch.SetActive(false); // �۸�ġ ȿ�� ��Ȱ��ȭ
        Flash.SetActive(true);// �÷��� ȿ�� Ȱ��ȭ

    }
}