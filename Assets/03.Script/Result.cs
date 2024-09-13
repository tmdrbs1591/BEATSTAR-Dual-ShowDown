using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Result : MonoBehaviour
{
    [SerializeField] GameObject goUi = null;

    [SerializeField] TMP_Text[] txtCount = null;
    [SerializeField] TMP_Text txtMaxCombo = null;

    [SerializeField] AudioClip soundClip; // 플레이할 오디오 클립
    private AudioSource audioSource; // 오디오 소스

    [SerializeField] ComboManager theCombo;
    [SerializeField] TimingManager theTiming;

    void Start()
    {
      
    }

    public void ShowResult()
    {
        goUi.SetActive(true);
    }
    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = soundClip;
        audioSource.Play();  // 오디오 재생

        Invoke("Sound", 1.2f);
        int[] t_judgement = theTiming.GetJudgementRecord();
        int t_MaxCombo = theCombo.GetMaxCombo();

        for (int i = 0; i < txtCount.Length; i++)
        {
            txtCount[i].text =string.Format("{0:#,##0}",t_judgement[i]);
        }
        txtMaxCombo.text = string.Format("{0:#,##0}",t_MaxCombo);
    }
    void Sound()
    {
        AudioManager.instance.PlaySound(transform.position, 8, Random.Range(1f, 1f), 1);
    }

}
