using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlaayerController : MonoBehaviour
{
    TimingManager theTimingManager;// Ÿ�̹� ������ ��ü
    public ButtonManager buttonManager;// ��ư ������ ��ü

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
    Slider HpBar; // ü�¹� UI
    [SerializeField]
    Slider HpBar2;// ü�¹� 2��° UI

    public bool invincibility; // ��������
    void Start()
    {
        transform.position = new Vector3(-11f, -2.82f,2);// ���� ��ġ ����
        animator = GetComponent<Animator>(); // Animator ������Ʈ ��������
        StartCoroutine(ApplyRootMotion()); // Root ��� ����

        // appearPosition���� 2�� ���� �ε巴�� �̵�
        transform.DOMove(appearPosition, 2f);
        theTimingManager = FindObjectOfType<TimingManager>(); // TimingManager ã�Ƽ� �����ϱ�

    }

    void Update()
    {
        if (!buttonManager.isCountDown) {
            // HP�� �� ���� (�ε巴��)
            HpBar.value = Mathf.Lerp(HpBar.value, (float)CurHP / (float)MaxHP, Time.deltaTime * 20);
        HpBar2.value = Mathf.Lerp(HpBar2.value, (float)CurHP / (float)MaxHP, Time.deltaTime * 3);

        Die();// ��� üũ
            Key(); // Ű �Է� ó��
            FourTrackKey();// 4Ʈ�� Ű �Է� ó��
        }
    }
    public void TakeDamage(int damage) // ������ �Ա�
    {
        if (invincibility)
            return;
        CurHP -= damage;// ���� HP���� ��������ŭ ����
        StartCoroutine(Hit());// �ǰ� ����Ʈ ���
        CameraShake.instance.Shake();// ī�޶� ��鸲 ȿ��
    }
    IEnumerator Hit()//�г� �ѹ� �����̴� �ڷ�ƾ
    {
        HitPanel.SetActive(true); // �ǰ� ����Ʈ �г� Ȱ��ȭ
        yield return new WaitForSeconds(0.25f);// 0.25�� ���
        HitPanel.SetActive(false); // �ǰ� ����Ʈ �г� ��Ȱ��ȭ

    }
    void Die() // ��� ���� üũ
    {
        if (CurHP <= 0)
        {
            Death = true;// ��� ���� ����
            DiePanel.SetActive(true); // ��� �г� Ȱ��ȭ

        }
    }
    void Key()
    {

        if (!Death)
        {
            bool qPressed = Input.GetKeyDown(KeySetting.keys[KeyAction.Q]);
            bool wPressed = Input.GetKeyDown(KeySetting.keys[KeyAction.W]);
            bool ePressed = Input.GetKeyDown(KeySetting.keys[KeyAction.E]);

            // Q�� W�� ���ÿ� ���� ���
            if ((Input.GetKey(KeySetting.keys[KeyAction.Q]) && wPressed) || (Input.GetKey(KeySetting.keys[KeyAction.W]) && qPressed))
            {
                theTimingManager.CheckTimingWithKey("QW");
                return; // ���� �Է� ó�� �� �Լ� ����
            }
            // W �Ǵ� O�� ���� ���¿��� E �Ǵ� P�� ������
            if ((Input.GetKey(KeySetting.keys[KeyAction.W]) && ePressed) || (Input.GetKey(KeySetting.keys[KeyAction.E]) && wPressed))
            {
                theTimingManager.CheckTimingWithKey("EW");
                return; // ���� �Է� ó�� �� �Լ� ����
            }
            if (qPressed)
            {
                QPanel.SetActive(true);
                theTimingManager.CheckTimingWithKey("Q");

            }
            if (wPressed)
            {
                WPanel.SetActive(true);
                theTimingManager.CheckTimingWithKey("W");

            }
            if (ePressed)
            {
                EPanel.SetActive(true);
                theTimingManager.CheckTimingWithKey("E");

            }
            if (Input.GetKey(KeySetting.keys[KeyAction.Q]) && Input.GetKey(KeySetting.keys[KeyAction.W]) && Input.GetKey(KeySetting.keys[KeyAction.E]))
            {
                theTimingManager.CheckTimingWithKey("Space");

            }
        }

        if (Input.GetKeyUp(KeyCode.Q) && !Death)
        {
            QPanel.SetActive(false);

        }
        else if (Input.GetKeyUp(KeyCode.W) && !Death)
        {
            WPanel.SetActive(false);

        }
        else if (Input.GetKeyUp(KeyCode.E) && !Death)
        {
            EPanel.SetActive(false);

        }
    }
    IEnumerator ApplyRootMotion()
    {
        animator.applyRootMotion = true; // Root ��� Ȱ��ȭ
        yield return new WaitForSeconds(2f);// 2�� ���
        animator.applyRootMotion = false; // Root ��� ��Ȱ��ȭ
    }



    void FourTrackKey()
    {

        bool dPressed = Input.GetKeyDown(KeySetting.keys[KeyAction.D]);
        bool fPressed = Input.GetKeyDown(KeySetting.keys[KeyAction.F]);
        bool jPressed = Input.GetKeyDown(KeySetting.keys[KeyAction.J]);
        bool kPressed = Input.GetKeyDown(KeySetting.keys[KeyAction.K]);

        if ((Input.GetKey(KeySetting.keys[KeyAction.D]) && fPressed) || (Input.GetKey(KeySetting.keys[KeyAction.F]) && dPressed))
        {
            theTimingManager.CheckTimingWithKey("DF");
            return; // ���� �Է� ó�� �� �Լ� ����
        }
        // W �Ǵ� O�� ���� ���¿��� E �Ǵ� P�� ������
        if ((Input.GetKey(KeySetting.keys[KeyAction.J]) && kPressed) || (Input.GetKey(KeySetting.keys[KeyAction.K]) && jPressed))
        {
            theTimingManager.CheckTimingWithKey("JK");
            return; // ���� �Է� ó�� �� �Լ� ����
        }

        if (dPressed)
        {
            theTimingManager.CheckTimingWithKey("D");

        }
        if (fPressed)
        {
            theTimingManager.CheckTimingWithKey("F");

        }
        if (jPressed)
        {
            theTimingManager.CheckTimingWithKey("J");

        }
        if (kPressed)
        {
            theTimingManager.CheckTimingWithKey("K");

        }
        if (Input.GetKey(KeySetting.keys[KeyAction.D]) && Input.GetKey(KeySetting.keys[KeyAction.F]) && Input.GetKey(KeySetting.keys[KeyAction.J]) && Input.GetKey(KeySetting.keys[KeyAction.K]))
        {
            theTimingManager.CheckTimingWithKey("DFJK");

        }
    }

}
