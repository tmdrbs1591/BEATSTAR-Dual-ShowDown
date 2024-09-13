using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourTrackAttack: MonoBehaviour
{
    public PlaayerController controller;// �÷��̾� ��Ʈ�ѷ��� �����ϴ� ����
    public ButtonManager buttonManager;// ��ư �Ŵ����� �����ϴ� ����

    public Transform Q;// Q Ű�� �ش��ϴ� Transform
    public Transform W;// W Ű�� �ش��ϴ� Transform
    public Transform E; // E Ű�� �ش��ϴ� Transform
    public Transform R;// R Ű�� �ش��ϴ� Transform
    public GameObject laserRotation;// ������ ȸ���� �����ϴ� ���� ������Ʈ
    public GameObject QWEEffect;// QWEEfffect�� Ȱ��ȭ�ϴ� ���� ������Ʈ
    public List<GameObject> lasers; // ����Ʈ�� laser GameObject���� ����

    public Animator animator;// �ִϸ����� ������Ʈ

    private void Start()
    {
        animator = GetComponent<Animator>();// �ش� ���� ������Ʈ�� �ִ� �ִϸ����� ������Ʈ ��������   
    }

    void Update()
    {
        if (!controller.Death && !buttonManager.isCountDown) // �÷��̾ ������� �ʾҰ�, ī��Ʈ�ٿ��� �ƴ� ��쿡�� ����
        {
            // ��� Ű(D, F, J, K)�� ���ÿ� ������ ��
            if ((Input.GetKey(KeySetting.keys[KeyAction.D]) && Input.GetKey(KeySetting.keys[KeyAction.F]) && Input.GetKey(KeySetting.keys[KeyAction.J]) && Input.GetKey(KeySetting.keys[KeyAction.K])) )
            {
                QWEEffect.SetActive(true); ; // QWEEfffect Ȱ��ȭ
                TripleAttack();// ������ Ʈ���� ���� ����
            }
            else if (Input.GetKeyDown(KeySetting.keys[KeyAction.D]))  // ���� ���� Ű �Է� ��
            {
                animator.SetTrigger("QAttack");// Q ���� �ִϸ��̼� ����
                RotateLaser(Q); // Q ������ ȸ��
                StartCoroutine(laserSetActive());// ������ Ȱ��ȭ �ڷ�ƾ ����
            }
            else if (Input.GetKeyDown(KeySetting.keys[KeyAction.F]))
            {
                animator.SetTrigger("WAttack");// W ���� �ִϸ��̼� ����
                RotateLaser(W);// W ������ ȸ��
                StartCoroutine(laserSetActive()); // ������ Ȱ��ȭ �ڷ�ƾ ����
            }
            else if (Input.GetKeyDown(KeySetting.keys[KeyAction.J]))
            {
                animator.SetTrigger("EAttack"); // E ���� �ִϸ��̼� ����
                RotateLaser(E);// E ������ ȸ��
                StartCoroutine(laserSetActive());// ������ Ȱ��ȭ �ڷ�ƾ ����
            }
            else if (Input.GetKeyDown(KeySetting.keys[KeyAction.K]))
            {
                animator.SetTrigger("EAttack");// E ���� �ִϸ��̼� ����
                RotateLaser(R);// R ������ ȸ��
                StartCoroutine(laserSetActive()); // ������ Ȱ��ȭ �ڷ�ƾ ����
            }
            else
            {
                QWEEffect.SetActive(false);// QWEEfffect ��Ȱ��ȭ
            }
            
        }
    }


    public void TripleAttack()
    {
        int randomValue = Random.Range(0, 3); // 0, 1, 2 �߿��� ������ �� ����

        switch (randomValue)
        {
            case 0:
                RotateLaser(Q);
                StartCoroutine(laserSetActive()); // ������ Ȱ��ȭ �ڷ�ƾ ����
                break;
            case 1:
                RotateLaser(W);
                StartCoroutine(laserSetActive());// ������ Ȱ��ȭ �ڷ�ƾ ����
                break;
            case 2:
                animator.SetTrigger("EAttack"); // E ���� �ִϸ��̼� ����
                RotateLaser(E);
                StartCoroutine(laserSetActive());// ������ Ȱ��ȭ �ڷ�ƾ ����
                break;
        }
    }

    public void RotateLaser(Transform targetTransform)// �������� targetTransform �������� ȸ����Ű�� �޼���
    {
        // ȸ�� ���� ���
        Vector3 direction = targetTransform.position - laserRotation.transform.position;

        // z���� �������� ȸ�� ���� ���
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        // �������� ȸ�� ����
        laserRotation.transform.rotation = rotation;
    }

    public IEnumerator laserSetActive() // �������� Ȱ��ȭ�ϰ� ���� �ð� �Ŀ� ��Ȱ��ȭ�ϴ� �ڷ�ƾ
    {
        // ��Ȱ��ȭ�� ������ GameObject ã��
        GameObject inactiveLaser = lasers.Find(laser => !laser.activeSelf);

        if (inactiveLaser != null)
        {
            inactiveLaser.SetActive(true); // ������ Ȱ��ȭ
            yield return new WaitForSeconds(0.2f);// 0.2�� �Ŀ�
            inactiveLaser.SetActive(false);// ������ ��Ȱ��ȭ
        }
        else
        {
            Debug.LogWarning("No inactive laser available!"); // ��Ȱ��ȭ�� �������� ���� ��� ��� ���
        }
    }
}
