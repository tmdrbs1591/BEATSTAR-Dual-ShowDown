using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Tutorial : MonoBehaviour  // �ڵ� ���� �����̶� �ּ� ����
{
    public ParallaxScroll map;
    public GameObject SpeedEffect;
    public GameObject ClearPanel;
    public GameObject Warning;
    public AudioSource Song;
    public PlaayerController thePlayerController;
    public Animator CameraAnim;
    public int bpm = 120;
    double currentTime = 0d;
    int noteCount = 0; // ������ ��Ʈ�� ��
    public GameObject GlitchVolume;
    public GameObject DigitalGlitchVolume;

    public TMP_Text messageText;
    public GameObject messageBox;
    public GameObject autoBox;


    public GameObject AutoPanel; //�ڵ� �ÿ��� ���� �г� 
    public Animator AutoPanelAnim; //�ڵ� �ÿ��� ������ �ִϸ��̼�
    enum BeatType
    {
        Whole = 1,
        Half = 2,
        Quarter = 4,
        Eighth = 8,
        Sixteenth = 16
    }

    public ObjectManager objectManager;
    public GameObject PowerEffect;

    [SerializeField] Transform tfNoteAppear = null;
    [SerializeField] GameObject go1 = null;
    [SerializeField] GameObject go2 = null;
    [SerializeField] GameObject go3 = null;
    [SerializeField] GameObject go4 = null;
    [SerializeField] GameObject go5 = null;
    [SerializeField] GameObject go6 = null;
    [SerializeField] GameObject go7 = null;


    [SerializeField] LinaSkill linaSkill;


    TimingManager theTimingManager;
    EffectManager theEffectManager;
    ComboManager thecomboManager;

    private void Awake()
    {
        DataManager.instance.currentCharater = Character.White;
    }
    void Start()
    {
        
        Song.Stop();
        thecomboManager = FindObjectOfType<ComboManager>();
        theEffectManager = FindObjectOfType<EffectManager>();
        theTimingManager = GetComponent<TimingManager>();
        linaSkill.skillpossible = false;
    }

    void FixedUpdate()
    {
        if (linaSkill != null) // Controller�� null�� �ƴ� ���, �� �÷��̾� ��Ʈ�ѷ��� ������ ��
        {
            linaSkill = FindObjectOfType<LinaSkill>(); // Scene���� �÷��̾� ��Ʈ�ѷ��� ã�Ƽ� �Ҵ�

        }
        if (thePlayerController != null)
        {
            thePlayerController = FindObjectOfType<PlaayerController>();

        }

        double beatInterval = 60d / bpm;

        currentTime += Time.deltaTime;
        #region beat

        if (noteCount < 1)
        {
            if (currentTime >= beatInterval * 5.4f)
            {
                Song.Play();
                TextUpdate($"�ݰ���,{UserInfo.Data.nickname}!") ;
                currentTime -= beatInterval * 5.4f;
                noteCount++;
            }
        }
        else if (noteCount < 2)
        {
            if (currentTime >= beatInterval * 8f)
            {
                TextUpdate("����, �⺻���� �������� �������!");
                currentTime -= beatInterval * 15f;
                noteCount++;

            }
        }
        else if (noteCount < 3)
        {
            if (currentTime >= beatInterval * 1.99f)
            {
                AutoPanel.SetActive(true);
                AutoPanelAnim.SetBool("isAutoPanel", true);
                SpawnQNote();
                autoBox.SetActive(true); // �ڵ��ÿ��� ���� autobox Ȱ��ȭ
                TextUpdate("Q�� ���� ����� ���� ������ �� �־�!");
                currentTime -= beatInterval * 1.99f;
                noteCount++;
            }
        }
        else if (noteCount < 9)
        {
            if (currentTime >= beatInterval * 1.99f)
            {
                SpawnQNote();
                currentTime -= beatInterval * 1.99f;
                noteCount++;
            }
        }
        else if (noteCount < 18)
        {
            if (currentTime >= beatInterval * 4f)
            {
                AutoPanelAnim.SetBool("isAutoPanel", false);
                autoBox.SetActive(false); 
                messageBox.SetActive(false);
                SpawnQNote();
                currentTime -= beatInterval * 1.99f;
                noteCount++;
            }
        }
        else if (noteCount < 19)
        {
            if (currentTime >= beatInterval * 1.99f)
            {
                TextUpdate("���Ҿ�!");
                currentTime -= beatInterval * 1.99f;
                noteCount++;
            }
        }
        else if (noteCount < 20)
        {
            if (currentTime >= beatInterval * 4f)
            {
                TextUpdate("����� �ִ°�?");
                currentTime -= beatInterval * 4f;
                noteCount++;
            }
        }
        else if (noteCount < 21)
        {
            if (currentTime >= beatInterval * 4f)
            {
                TextUpdate("���� �ܰ�� ������?");
                currentTime -= beatInterval * 10f;
                noteCount++;
            }
        }
        else if (noteCount < 22)
        {
            if (currentTime >= beatInterval * 1.99f)
            {
                AutoPanelAnim.SetBool("isAutoPanel", true);
                SpawnWNote();
                autoBox.SetActive(true); // �ڵ��ÿ��� ���� autobox Ȱ��ȭ
                TextUpdate("W�� ���� �ߴ��� ���� ������ �� �־�!");
                currentTime -= beatInterval * 1.99f;
                noteCount++;
            }
        }
        else if (noteCount < 30)
        {
            if (currentTime >= beatInterval * 2f)
            {
                SpawnWNote();
                currentTime -= beatInterval * 2f;
                noteCount++;
            }
        }
        else if (noteCount < 37)
        {
            if (currentTime >= beatInterval * 4f)
            {
                AutoPanelAnim.SetBool("isAutoPanel", false);
                messageBox.SetActive(false);
                autoBox.SetActive(false);
                SpawnWNote();
                currentTime -= beatInterval * 2f;
                noteCount++;
            }
        }
        else if (noteCount < 38)
        {
            if (currentTime >= beatInterval * 4f)
            {
                AutoPanelAnim.SetBool("isAutoPanel", true);
                SpawnENote();
                autoBox.SetActive(true); // �ڵ��ÿ��� ���� autobox Ȱ��ȭ
                TextUpdate("E�� ���� �ϴ��� ���� ������ �� �־�!");
                currentTime -= beatInterval * 4f;
                noteCount++;
            }
        }
        else if (noteCount < 45)
        {
            if (currentTime >= beatInterval * 2f)
            {
                SpawnENote();
                currentTime -= beatInterval * 2f;
                noteCount++;
            }
        }
        else if (noteCount < 52)
        {
            if (currentTime >= beatInterval * 4f)
            {
                AutoPanelAnim.SetBool("isAutoPanel", false);
                messageBox.SetActive(false);
                autoBox.SetActive(false);
                SpawnENote();
                currentTime -= beatInterval * 2f;
                noteCount++;
            }
        }
        else if (noteCount < 53)
        {
            if (currentTime >= beatInterval * 1.99f)
            {
                TextUpdate("���Ҿ�!");
                currentTime -= beatInterval * 4f;
                noteCount++;
            }
        }
        else if (noteCount < 54)
        {
            if (currentTime >= beatInterval * 4f)
            {
                TextUpdate("�� ���� �ȶ��ϱ���!");
                currentTime -= beatInterval * 4f;
                noteCount++;
            }
        }
        else if (noteCount < 55)
        {
            if (currentTime >= beatInterval * 8f)
            {
                TextUpdate("���� �ܰ�� ������?");
                currentTime -= beatInterval * 8f;
                noteCount++;
            }
        }
        else if (noteCount < 56)
        {
            if (currentTime >= beatInterval * 3f)
            {
                AutoPanelAnim.SetBool("isAutoPanel", true);
                SpawnQWNote();
                autoBox.SetActive(true); // �ڵ��ÿ��� ���� autobox Ȱ��ȭ
                TextUpdate("QW�� ���� ���ߴ��� ���ÿ� ���� ������ ������ �� �־�!");
                currentTime -= beatInterval * 3f;
                noteCount++;
            }
        }
        else if (noteCount < 62)
        {
            if (currentTime >= beatInterval * 2f)
            {
                SpawnQWNote();
                currentTime -= beatInterval * 2f;
                noteCount++;
            }
        }
        else if (noteCount < 70)
        {
            if (currentTime >= beatInterval * 4f)
            {
                AutoPanelAnim.SetBool("isAutoPanel", false);
                messageBox.SetActive(false);
                autoBox.SetActive(false);
                SpawnQWNote();
                currentTime -= beatInterval * 2f;
                noteCount++;
            }
        }
        else if (noteCount < 71)
        {
            if (currentTime >= beatInterval * 4f)
            {
                AutoPanelAnim.SetBool("isAutoPanel", true);
                SpawnEWNote();
                autoBox.SetActive(true); // �ڵ��ÿ��� ���� autobox Ȱ��ȭ
                TextUpdate("WE�� ���� ���ϴ��� ���ÿ� ���� ������ ������ �� �־�!");
                currentTime -= beatInterval * 3f;
                noteCount++;
            }
        }
        else if (noteCount < 78)
        {
            if (currentTime >= beatInterval * 2f)
            {
                SpawnEWNote();
                currentTime -= beatInterval * 2f;
                noteCount++;
            }
        }
        else if (noteCount < 86)
        {
            if (currentTime >= beatInterval * 4f)
            {
                AutoPanelAnim.SetBool("isAutoPanel", false);
                messageBox.SetActive(false);
                autoBox.SetActive(false);
                SpawnEWNote();
                currentTime -= beatInterval * 2f;
                noteCount++;
            }
        }
        else if (noteCount < 87)
        {
            if (currentTime >= beatInterval * 7f)
            {
                AutoPanelAnim.SetBool("isAutoPanel", true);
                SpawnSpaceNote();
                autoBox.SetActive(true); // �ڵ��ÿ��� ���� autobox Ȱ��ȭ
                TextUpdate("QWE�� ��� �� ���� �����ϴ��� �������� ���� ������ ������ �� �־�!");
                currentTime -= beatInterval * 7f;
                noteCount++;
            }
        }
        else if (noteCount < 110)
        {
            if (currentTime >= beatInterval * 0.1f)
            {
                SpawnSpaceNote();
                currentTime -= beatInterval * 0.1f;
                noteCount++;
            }
        }
        else if (noteCount < 137)
        {
            if (currentTime >= beatInterval * 5.5f)
            {
                AutoPanelAnim.SetBool("isAutoPanel", false);
                messageBox.SetActive(false);
                autoBox.SetActive(false);
                SpawnSpaceNote();
                currentTime -= beatInterval * 0.1f;
                noteCount++;
            }
        }
        else if (noteCount < 138)
        {
            if (currentTime >= beatInterval * 15f)
            {
                AutoPanelAnim.SetBool("isAutoPanel", true);
                linaSkill.skillpossible = true;
                linaSkill.SkillOn();
                linaSkill.skillpossible = false;
                TextUpdate("Space�� ���� ��ų�� ����� �� �־�!");
                currentTime -= beatInterval * 15f;
                noteCount++;
            }
        }
      
        else if (noteCount < 160)
        {
            if (currentTime >= beatInterval * 4f)
            {
                SpawnRandomNote();
                StartCoroutine(SkilltrueCor());
                currentTime -= beatInterval * 0.2f;
                noteCount++;
            }
        }
        else if (noteCount < 161)
        {
            if (currentTime >= beatInterval * 2f)
            {

                currentTime -= beatInterval * 2f;
                noteCount++;
            }
        }
        else if (noteCount < 190)
        {
            if (currentTime >= beatInterval * 10f)
            {
                AutoPanelAnim.SetBool("isAutoPanel", false);
                messageBox.SetActive(false);
                SpawnRandomNote();
                currentTime -= beatInterval * 0.2f;
                noteCount++;
            }
        }
        else if (noteCount < 191)
        {
            if (currentTime >= beatInterval * 18f)
            {
                TextUpdate("�����߾�!!");
                currentTime -= beatInterval * 18f;
                noteCount++;
            }
        }
        else if (noteCount < 192)
        {
            if (currentTime >= beatInterval * 8f)
            {
                TextUpdate("���� �����Ӱ� �÷��� �غ�~!");
                currentTime -= beatInterval * 8f;
                noteCount++;
            }
        }
        else if (noteCount < 193)
        {
            if (currentTime >= beatInterval * 12f)
            {
                currentTime -= beatInterval * 12f;
                ClearPanel.SetActive(true);
                noteCount++;
            }
        }
    }

    IEnumerator ClearPanelCor()
    {

        yield return new WaitForSeconds(4f);
        ClearPanel.SetActive(true);
    }
    #endregion
    IEnumerator Effect()
    {
        yield return new WaitForSeconds(1.4f);



        map.MapSpeed *= 1000;

        PowerEffect.SetActive(true);
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
    void SpawnDoubleRandomNote()
    {
        int randomIndex = Random.Range(1, 3);
        GameObject t_note = null;
        switch (randomIndex)
        {
            case 1:

                t_note = Instantiate(go4, tfNoteAppear.position, Quaternion.identity);
                break;
            case 2:
                t_note = Instantiate(go5, tfNoteAppear.position, Quaternion.identity);
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
    void SpawnSpaceNote()
    {
        GameObject t_note = Instantiate(go6, tfNoteAppear.position, Quaternion.identity);
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
    void SpawnRNote()
    {
        GameObject t_note = Instantiate(go7, tfNoteAppear.position, Quaternion.identity);
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
    void SpawnEWNote()
    {
        GameObject t_note = Instantiate(go5, tfNoteAppear.position, Quaternion.identity);
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
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {

            if (collision.GetComponent<Note>().GetNoteFlag())
            {
                thePlayerController.TakeDamage(10);
                theEffectManager.judgementEffect(4);
                thecomboManager.ResetCombo();
            }


            theTimingManager.boxNoteList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }

    }

    IEnumerator GlitchOn(float time)
    {
        yield return new WaitForSeconds(time);
        GlitchVolume.SetActive(true);
    }
    IEnumerator DigitalGlitchOn(float time)
    {
        yield return new WaitForSeconds(time);
        DigitalGlitchVolume.SetActive(true);
    }

    void TextUpdate(string message)
    {
        messageBox.SetActive(false);
        messageText.text = message;
        messageBox.SetActive(true);
    }
    IEnumerator SkilltrueCor()
    {
        yield return new WaitForSeconds(6f);
        linaSkill.skillpossible = true;


    }
}
