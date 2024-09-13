using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FourTrackPlayerController : MonoBehaviour // �ڵ� ���� �����̶� �ּ� ����
{
    TimingManager theTimingManager;

    public GameObject DiePanel; //������ ������ �г�
    public GameObject HitPanel;//������ ��Ʈ ����Ʈ �г�
    public GameObject QPanel; //Q �� ǥ��
    public GameObject WPanel;//W �� ǥ��
    public GameObject EPanel;//E �� ǥ��

    public bool Death = false;//���� �׾��ִ��� Ȯ���ϴ� �Ұ�
    public int MaxHP;//�ִ� HP
    public int CurHP;//���� HP
    public Vector2 appearPosition;//ó���� �����϶� ������ ��ġ��
    private Animator animator;

    [SerializeField]
    Slider HpBar;
    [SerializeField]
    Slider HpBar2;

    public bool invincibility; // ��������
    void Start()
    {
        transform.position = new Vector3(-11f, -2.82f, 2);
        animator = GetComponent<Animator>();
        StartCoroutine(ApplyRootMotion());
        transform.DOMove(appearPosition, 2f);
        theTimingManager = FindObjectOfType<TimingManager>();

    }

    void Update()
    {
        HpBar.value = Mathf.Lerp(HpBar.value, (float)CurHP / (float)MaxHP, Time.deltaTime * 20);
        HpBar2.value = Mathf.Lerp(HpBar2.value, (float)CurHP / (float)MaxHP, Time.deltaTime * 3);

        Die();
        Key();
    }
    public void TakeDamage(int damage) // ������ �Ա�
    {
        if (invincibility)
            return;
        CurHP -= damage;
        StartCoroutine(Hit());
        CameraShake.instance.Shake();
    }
    IEnumerator Hit()//�г� �ѹ� �����̴� �ڷ�ƾ
    {
        HitPanel.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        HitPanel.SetActive(false);

    }
    void Die()
    {
        if (CurHP <= 0)
        {
            Death = true;
            DiePanel.SetActive(true);

        }
    }
    void Key()
    {

        if (!Death)
        {
            bool dPressed = Input.GetKeyDown(KeySetting.keys[KeyAction.D]);
            bool fPressed = Input.GetKeyDown(KeySetting.keys[KeyAction.F]);
            bool jPressed = Input.GetKeyDown(KeySetting.keys[KeyAction.J]);
            bool kPressed = Input.GetKeyDown(KeySetting.keys[KeyAction.K]);

            if ((Input.GetKey(KeySetting.keys[KeyAction.Q]) && fPressed) || (Input.GetKey(KeySetting.keys[KeyAction.W]) && dPressed))
            {
                theTimingManager.CheckTimingWithKey("QW");
                return; // ���� �Է� ó�� �� �Լ� ����
            }
            // W �Ǵ� O�� ���� ���¿��� E �Ǵ� P�� ������
            if ((Input.GetKey(KeySetting.keys[KeyAction.W]) && jPressed) || (Input.GetKey(KeySetting.keys[KeyAction.E]) && fPressed))
            {
                theTimingManager.CheckTimingWithKey("EW");
                return; // ���� �Է� ó�� �� �Լ� ����
            }
            if (dPressed)
            {
                QPanel.SetActive(true);
                theTimingManager.CheckTimingWithKey("Q");

            }
            if (fPressed)
            {
                WPanel.SetActive(true);
                theTimingManager.CheckTimingWithKey("W");

            }
            if (jPressed)
            {
                EPanel.SetActive(true);
                theTimingManager.CheckTimingWithKey("E");

            }
            if (Input.GetKey(KeySetting.keys[KeyAction.D]) && Input.GetKey(KeySetting.keys[KeyAction.F]) && Input.GetKey(KeySetting.keys[KeyAction.J]))
            {
                theTimingManager.CheckTimingWithKey("Space");

            }
        }

        if (Input.GetKeyUp(KeyCode.D) && !Death)
        {
            QPanel.SetActive(false);

        }
        else if (Input.GetKeyUp(KeyCode.F) && !Death)
        {
            WPanel.SetActive(false);

        }
        else if (Input.GetKeyUp(KeyCode.J) && !Death)
        {
            EPanel.SetActive(false);

        }
    }
    IEnumerator ApplyRootMotion()
    {
        animator.applyRootMotion = true;
        yield return new WaitForSeconds(2f);
        animator.applyRootMotion = false;
    }
}
