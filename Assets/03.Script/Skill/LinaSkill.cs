using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LinaSkill : MonoBehaviour
{
    [SerializeField] private GameObject AutoBox; // ���� ��ų�� �ڵ� �����̱� ������ �ڵ����� �ϰ� ���ִ� �ڽ� �߰�
    [SerializeField] private GameObject SkillPanel; // ��ų �г� 
    [SerializeField] private GameObject SkillParticl; // ��ų ��ƼŬ
    [SerializeField] private Image skillCooldownImage; // ��Ÿ�� �̹���
    [SerializeField] private TMP_Text skillCooldownText; // ��Ÿ�� �ؽ�Ʈ

    [SerializeField] private float skillCoolTime = 0.5f;
    private float skillCurTime;
    public bool skillpossible = true;

    void Start()
    {
        skillCooldownImage.fillAmount = 0f; // ������ �� ��Ÿ�� �̹����� ��Ȱ��ȭ
        skillCooldownText.text = ""; // ������ �� �ؽ�Ʈ�� �����
    }

    void Update()
    {
        if (skillCurTime <= 0 )
        {

            if (Input.GetKeyDown(KeyCode.Space) && skillpossible)// ���߿� Ű���� �Ŵ������� �޾ƿͼ�
            {
                SkillOn();
                skillCurTime = skillCoolTime;
                skillCooldownImage.fillAmount = 1f; // ��ų ��� �� ��Ÿ�� �̹��� Ȱ��ȭ
                skillCooldownText.text = skillCoolTime.ToString("F1"); // ��Ÿ�� �ؽ�Ʈ �ʱ�ȭ
            }
            else
            {
                skillCooldownText.text = ""; // ���� �ð��� 0�� �� �ؽ�Ʈ�� �������� ����
            }
        }
        else
        {
            skillCurTime -= Time.deltaTime;
            skillCooldownImage.fillAmount = skillCurTime / skillCoolTime; // ��Ÿ�ӿ� ���� fillAmount ����
            skillCooldownText.text = skillCurTime.ToString("F1"); // ���� �ð��� �ؽ�Ʈ�� ǥ��
        }
    }

    public void SkillOn() // ��ų �� ��ư������ �����ؾ��ϱ� ������ public����
    {
       
        AudioManager.instance.PlaySound(transform.position, 13, Random.Range(1f, 1f), 1);// ����� ���
        AudioManager.instance.PlaySound(transform.position, 16, Random.Range(1f, 1f), 1);// ����� ���
        StartCoroutine(SkillCor());
    }

    IEnumerator SkillCor()
    {
        CameraShake.instance.Shake();
        StartCoroutine(SkillPanelCor());
        SkillParticl.SetActive(true);
        AutoBox.SetActive(true);
        yield return new WaitForSeconds(6); // 6�� ���� �ڵ� ���� ����
        AutoBox.SetActive(false);
        SkillParticl.SetActive(false);
    }

    IEnumerator SkillPanelCor()
    {
        SkillPanel.SetActive(true);
        yield return new WaitForSeconds(1.65f);
        SkillPanel.SetActive(false);
    }
}
