using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public PlaayerController controller;
    public ButtonManager buttonManager;

    public Transform Q; // Q ���� ������
    public Transform W;  // W ���� ������
    public Transform E;   // E ���� ������
    public GameObject laserRotation;    //�Ѿ˹߻� ������ ���Ⱚ �����ϱ� ���� �����̼� ������ ������Ʈ ��������
    public GameObject QWEEffect; // QWE ������ ���� ����Ʈ ������Ʈ
    public List<GameObject> lasers; // ����Ʈ�� laser GameObject���� ����

    public Animator animator; // �ִϸ�����

    private void Start()
    {
        animator = GetComponent<Animator>(); // �ʱ�ȭ
    }

    void Update()
    {
        if (!controller.Death && !buttonManager.isCountDown) // ���� �ʰų� ī��Ʈ ���� �ƴҶ���
        {
            if ((Input.GetKey(KeySetting.keys[KeyAction.Q]) && Input.GetKey(KeySetting.keys[KeyAction.W]) && Input.GetKey(KeySetting.keys[KeyAction.E])))// QWE���ÿ� ������ 
            {
                QWEEffect.SetActive(true); //����Ʈ Ű��
                TripleAttack(); // Ʈ���� ���� �Լ� ȣ�� (�Ѿ˹߻� ����Ʈ)
            }
            else if (Input.GetKeyDown(KeySetting.keys[KeyAction.Q])) // Q������
            {
                animator.SetTrigger("QAttack"); // Q �ִϸ��̼� Ʈ���� Ű��
                RotateLaser(Q); // �������� Transform �Ű����� Q �������� Rotate
                StartCoroutine(laserSetActive()); // �Ѿ� �߻� ����Ʈ
            }
            else if (Input.GetKeyDown(KeySetting.keys[KeyAction.W]))
            {
                animator.SetTrigger("WAttack"); // W�ִϸ��̤ż� Ʈ���� �ѱ� 
                RotateLaser(W); // �������� Transform �Ű����� E�������� Rotate
                StartCoroutine(laserSetActive()); // �Ѿ� �߻� ����Ʈ
            }
            else if (Input.GetKeyDown(KeySetting.keys[KeyAction.E]))
            {
                animator.SetTrigger("EAttack"); // E�ִϸ��̼� Ʈ���� �ѱ�
                RotateLaser(E); // �������� Transform �Ű����� E�������� Rotate
                StartCoroutine(laserSetActive());// �Ѿ� �߻� ����Ʈ
            }
            else
            {
                QWEEffect.SetActive(false); //�ƹ��͵� ���ϰ� �ִ� ���¿��� QWE ����Ʈ ������
            }

        }
    }


    public void TripleAttack() // QWE ���ÿ� ������ �� ����Ʈ 
    {
        int randomValue = Random.Range(0, 3); // 0, 1, 2 �߿��� ������ �� ����

        switch (randomValue) // Q W E ��ġ�� ���������� �����̼� ����
        {
            case 0:
                RotateLaser(Q);
                StartCoroutine(laserSetActive());
                break;
            case 1:
                RotateLaser(W);
                StartCoroutine(laserSetActive());
                break;
            case 2:
                animator.SetTrigger("EAttack");
                RotateLaser(E);
                StartCoroutine(laserSetActive());
                break;
        }
    }

    public void RotateLaser(Transform targetTransform) // ������ �����̼� ����
    {
        // ���������� Ÿ�ٱ����� ���� ���� ���
        Vector3 direction = targetTransform.position - laserRotation.transform.position;

        // Ÿ���� ���� �������� ȸ����Ű�� ���� �ʿ��� ������ ���ȿ��� �� ������ ��ȯ�Ͽ� ���
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // z ���� �߽����� �ϴ� ȸ���� ��Ÿ���� ���ʹϾ� ����
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        // ���� ȸ���� �������� ����
        laserRotation.transform.rotation = rotation;
    }

    public IEnumerator laserSetActive()
    {
        // ��Ȱ��ȭ�� ������ GameObject�� ã��
        GameObject inactiveLaser = lasers.Find(laser => !laser.activeSelf);

        // ��Ȱ��ȭ�� �������� �����ϸ�
        if (inactiveLaser != null)
        {
            // �������� Ȱ��ȭ
            inactiveLaser.SetActive(true);

            // 0.2�� ���� ���
            yield return new WaitForSeconds(0.2f);

            // �������� �ٽ� ��Ȱ��ȭ
            inactiveLaser.SetActive(false);
        }
        else
        {
            // ��Ȱ��ȭ�� �������� ������ ��� �޽����� ���
            Debug.LogWarning("No inactive laser available!");
        }
    }

}
