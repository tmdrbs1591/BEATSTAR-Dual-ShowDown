using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 120;
    double currentTime = 0d;
    int noteCount = 0; // 생성된 노트의 수

    enum BeatType
    {
        Whole = 1,
        Half = 2,
        Quarter = 4,
        Eighth = 8,
        Sixteenth = 16
    }

    public ObjectManager objectManager;

    [SerializeField] Transform tfNoteAppear = null;
    [SerializeField] GameObject go1 = null;
    [SerializeField] GameObject go2 = null;
    [SerializeField] GameObject go3 = null;
    TimingManager theTimingManager;
    EffectManager theEffectManager;
    ComboManager thecomboManager;

    void Start()
    {
        thecomboManager = FindObjectOfType<ComboManager>();
        theEffectManager = FindObjectOfType<EffectManager>();
        theTimingManager = GetComponent<TimingManager>();
    }

    void Update()
    {
        double beatInterval = 60d / bpm;

        currentTime += Time.deltaTime;

        if (noteCount < 16) // 처음 16개의 노트는 2박자로 생성
        {
            if (currentTime >= beatInterval * 1.295f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 1.295f;
                noteCount++;
            }
        }
        else if (noteCount < 19) // 16개 이후 4박자로 4개 생성
        {
            if (currentTime >= beatInterval)
            {
                SpawnRandomNote();
                currentTime -= beatInterval;
                noteCount++;
            }
        }
        else if (noteCount < 23) // 16개 이후 4박자로 4개 생성
        {
            if (currentTime >= beatInterval / 1.7f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval / 1.7f;
                noteCount++;
            }
        }
        else if (noteCount < 26)
        {
            if (currentTime >= beatInterval * 0.9f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval * 0.9f;
                noteCount++;
            }
        }
        else if (noteCount < 30)
        {
            if (currentTime >= beatInterval / 1.6f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval / 1.6f;
                noteCount++;
            }
        }
        else if (noteCount < 32)
        {
            if (currentTime >= beatInterval / 1.3f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval / 1.3f;
                noteCount++;
            }
        }
        else if (noteCount < 36)
        {
            if (currentTime >= beatInterval / 2.5f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval / 2.5f;
                noteCount++;
            }
        }
        else if (noteCount < 38)
        {
            if (currentTime >= beatInterval / 1.7f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval / 1.7f;
                noteCount++;
            }
        }
        else if (noteCount < 42)
        {
            if (currentTime >= beatInterval / 2.5f)
            {
                SpawnRandomNote();
                currentTime -= beatInterval / 2.5f;
                noteCount++;
            }
        }
    }

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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            if (collision.GetComponent<Note>().GetNoteFlag())
            {

                theEffectManager.judgementEffect(4);
                thecomboManager.ResetCombo();
            }

            theTimingManager.boxNoteList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
