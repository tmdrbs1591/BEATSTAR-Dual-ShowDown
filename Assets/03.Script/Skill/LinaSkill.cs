using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LinaSkill : MonoBehaviour
{
    [SerializeField] private GameObject AutoBox; // 리나 스킬은 자동 공격이기 때문에 자동으로 하게 해주는 박스 추가
    [SerializeField] private GameObject SkillPanel; // 스킬 패널 
    [SerializeField] private GameObject SkillParticl; // 스킬 파티클
    [SerializeField] private Image skillCooldownImage; // 쿨타임 이미지
    [SerializeField] private TMP_Text skillCooldownText; // 쿨타임 텍스트

    [SerializeField] private float skillCoolTime = 0.5f;
    private float skillCurTime;
    public bool skillpossible = true;

    void Start()
    {
        skillCooldownImage.fillAmount = 0f; // 시작할 때 쿨타임 이미지를 비활성화
        skillCooldownText.text = ""; // 시작할 때 텍스트를 비워둠
    }

    void Update()
    {
        if (skillCurTime <= 0 )
        {

            if (Input.GetKeyDown(KeyCode.Space) && skillpossible)// 나중에 키세팅 매니저에서 받아와서
            {
                SkillOn();
                skillCurTime = skillCoolTime;
                skillCooldownImage.fillAmount = 1f; // 스킬 사용 시 쿨타임 이미지 활성화
                skillCooldownText.text = skillCoolTime.ToString("F1"); // 쿨타임 텍스트 초기화
            }
            else
            {
                skillCooldownText.text = ""; // 남은 시간이 0일 때 텍스트를 공백으로 설정
            }
        }
        else
        {
            skillCurTime -= Time.deltaTime;
            skillCooldownImage.fillAmount = skillCurTime / skillCoolTime; // 쿨타임에 따라 fillAmount 변경
            skillCooldownText.text = skillCurTime.ToString("F1"); // 남은 시간을 텍스트로 표시
        }
    }

    public void SkillOn() // 스킬 온 버튼에서도 실행해야하기 때문에 public으로
    {
       
        AudioManager.instance.PlaySound(transform.position, 13, Random.Range(1f, 1f), 1);// 오디오 재생
        AudioManager.instance.PlaySound(transform.position, 16, Random.Range(1f, 1f), 1);// 오디오 재생
        StartCoroutine(SkillCor());
    }

    IEnumerator SkillCor()
    {
        CameraShake.instance.Shake();
        StartCoroutine(SkillPanelCor());
        SkillParticl.SetActive(true);
        AutoBox.SetActive(true);
        yield return new WaitForSeconds(6); // 6초 동안 자동 공격 시작
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
