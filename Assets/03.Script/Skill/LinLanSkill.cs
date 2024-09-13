using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LinLanSkill : MonoBehaviour
{
    [SerializeField] private GameObject SkillPanel; // 스킬 패널 
    [SerializeField] private GameObject SkillParticl; // 스킬 파티클
    [SerializeField] private GameObject ShieldPtc; // 쉴드 파티클
    [SerializeField] private Image skillCooldownImage; // 쿨타임 이미지
    [SerializeField] private TMP_Text skillCooldownText; // 쿨타임 텍스트

    [SerializeField] private PlaayerController plaayerController; // 3트렉 플레이어 컨트롤러
    [SerializeField] private FourTrackPlayerController FourPlaayerController; // 4트렉 플레이어 컨트롤러

    [SerializeField] private float skillCoolTime = 0.5f;
    private float skillCurTime;

    void Start()
    {
        plaayerController = GetComponent<PlaayerController>();
        FourPlaayerController = GetComponent<FourTrackPlayerController>();

        skillCooldownImage.fillAmount = 0f; // 시작할 때 쿨타임 이미지를 비활성화
        skillCooldownText.text = ""; // 시작할 때 텍스트를 비워둠
    }

    void Update()
    {
        if (skillCurTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space)) // 나중에 키세팅 매니저에서 받아와서
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
        AudioManager.instance.PlaySound(transform.position, 14, Random.Range(1f, 1f), 1);// 오디오 재생
        AudioManager.instance.PlaySound(transform.position, 16, Random.Range(1f, 1f), 1);// 오디오 재생
        StartCoroutine(SkillCor());
    }

    IEnumerator SkillCor()
    {
        CameraShake.instance.Shake();
        StartCoroutine(SkillPanelCor());
        if (plaayerController != null) plaayerController.invincibility = true;
        if (FourPlaayerController != null) FourPlaayerController.invincibility = true;
        SkillParticl.SetActive(true);
        ShieldPtc.SetActive(true);
        yield return new WaitForSeconds(6); //6초동안 무적
        if (plaayerController != null) plaayerController.invincibility = false;
        if (FourPlaayerController != null) FourPlaayerController.invincibility = false;
        SkillParticl.SetActive(false);
        ShieldPtc.SetActive(false);

    }

    IEnumerator SkillPanelCor()
    {
        SkillPanel.SetActive(true);
        yield return new WaitForSeconds(1.65f);
        SkillPanel.SetActive(false);
    }
}
