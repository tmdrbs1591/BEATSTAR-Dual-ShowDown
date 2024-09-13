using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBox : MonoBehaviour //자동공격 할때 필요한  Auto Box
{
    // 타이밍 매니저 객체
    TimingManager theTimingManager;
    // 3트랙 공격 스크립트
    public Attack playerAttack;
    // 4트랙 공격    스크립트
    public FourTrackAttack fourTrackPlayerAttack; 
    void Start()
    {
        // 타이밍 매니저를 Scene에서 찾아서 할당
        theTimingManager = FindObjectOfType<TimingManager>();

    }


    void Update()
    {
        // 플레이어 3트랙 공격 스크립트가 할당되지 않았으면 찾아서 할당
        if (playerAttack != null)
        {
            playerAttack = FindObjectOfType<Attack>();
        }
        if (fourTrackPlayerAttack != null)      // 플레이어4트랙 공격 스크립트가 할당되지 않았으면 찾아서 할당
        {
            fourTrackPlayerAttack = FindObjectOfType<FourTrackAttack>();

        }
    }
    // 충돌 발생 시 호출되는 메서드
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 객체에서 Note 컴포넌트를 가져옴
        Note note = collision.GetComponent<Note>();


        // 노트의 noteKey에 따라 처리
        if (note.noteKey == "Q")
        {
            theTimingManager.CheckTimingWithKey("Q");   // 타이밍 체크 및 플레이어 애니메이션 및 레이저 회전 설정
            playerAttack.animator.SetTrigger("QAttack");
            playerAttack.RotateLaser(playerAttack.Q);
            playerAttack.StartCoroutine(playerAttack.laserSetActive());

        }
        if (note.noteKey == "W")
        {
            theTimingManager.CheckTimingWithKey("W");   // 타이밍 체크 및 플레이어 애니메이션 및 레이저 회전 설정
            playerAttack.animator.SetTrigger("WAttack");
            playerAttack.RotateLaser(playerAttack.W);
            playerAttack.StartCoroutine(playerAttack.laserSetActive());
        }
        if (note.noteKey == "E")
        {
            theTimingManager.CheckTimingWithKey("E");   // 타이밍 체크 및 플레이어 애니메이션 및 레이저 회전 설정
            playerAttack.animator.SetTrigger("EAttack");
            playerAttack.RotateLaser(playerAttack.E);
            playerAttack.StartCoroutine(playerAttack.laserSetActive());
        }
        if (note.noteKey == "QW")
        {
            theTimingManager.CheckTimingWithKey("QW");   // 타이밍 체크 및 플레이어 애니메이션 및 레이저 회전 설정
            playerAttack.animator.SetTrigger("QAttack");
            playerAttack.RotateLaser(playerAttack.W);
            playerAttack.StartCoroutine(playerAttack.laserSetActive());
        }
        if (note.noteKey == "EW")
        {
            theTimingManager.CheckTimingWithKey("EW");   // 타이밍 체크 및 플레이어 애니메이션 및 레이저 회전 설정
            playerAttack.animator.SetTrigger("QAttack");
            playerAttack.RotateLaser(playerAttack.W);
            playerAttack.StartCoroutine(playerAttack.laserSetActive());
        }
        if (note.noteKey == "Space")
        {
            theTimingManager.CheckTimingWithKey("Space");   // 타이밍 체크 및 플레이어 애니메이션 및 레이저 회전 설정
            playerAttack.QWEEffect.SetActive(true);
            playerAttack.TripleAttack();
        }
        if (note.noteKey == "D")
        {
            theTimingManager.CheckTimingWithKey("D");   // 타이밍 체크 및 플레이어 애니메이션 및 레이저 회전 설정
            fourTrackPlayerAttack.animator.SetTrigger("QAttack");
            fourTrackPlayerAttack.RotateLaser(fourTrackPlayerAttack.Q);
            fourTrackPlayerAttack.StartCoroutine(fourTrackPlayerAttack.laserSetActive());
        }
        if (note.noteKey == "F")
        {
            theTimingManager.CheckTimingWithKey("F");   // 타이밍 체크 및 플레이어 애니메이션 및 레이저 회전 설정
            fourTrackPlayerAttack.animator.SetTrigger("QAttack");
            fourTrackPlayerAttack.RotateLaser(fourTrackPlayerAttack.W);
            fourTrackPlayerAttack.StartCoroutine(fourTrackPlayerAttack.laserSetActive());
        }
        if (note.noteKey == "J")
        {
            theTimingManager.CheckTimingWithKey("J");   // 타이밍 체크 및 플레이어 애니메이션 및 레이저 회전 설정
            fourTrackPlayerAttack.animator.SetTrigger("WAttack");
            fourTrackPlayerAttack.RotateLaser(fourTrackPlayerAttack.E);
            fourTrackPlayerAttack.StartCoroutine(fourTrackPlayerAttack.laserSetActive());
        }
        if (note.noteKey == "K")
        {
            theTimingManager.CheckTimingWithKey("K");   // 타이밍 체크 및 플레이어 애니메이션 및 레이저 회전 설정
            fourTrackPlayerAttack.animator.SetTrigger("EAttack");
            fourTrackPlayerAttack.RotateLaser(fourTrackPlayerAttack.R);
            fourTrackPlayerAttack.StartCoroutine(fourTrackPlayerAttack.laserSetActive());
        }
        if (note.noteKey == "DF")
        {
            theTimingManager.CheckTimingWithKey("DF");   // 타이밍 체크 및 플레이어 애니메이션 및 레이저 회전 설정
            fourTrackPlayerAttack.animator.SetTrigger("WAttack");
            fourTrackPlayerAttack.RotateLaser(fourTrackPlayerAttack.W);
            fourTrackPlayerAttack.StartCoroutine(fourTrackPlayerAttack.laserSetActive());
        }
        if (note.noteKey == "JK")
        {
            theTimingManager.CheckTimingWithKey("JK");   // 타이밍 체크 및 플레이어 애니메이션 및 레이저 회전 설정
            fourTrackPlayerAttack.animator.SetTrigger("EAttack");
            fourTrackPlayerAttack.RotateLaser(fourTrackPlayerAttack.E);
            fourTrackPlayerAttack.StartCoroutine(fourTrackPlayerAttack.laserSetActive());
        }
        if (note.noteKey == "DFJK")
        {
            theTimingManager.CheckTimingWithKey("DFJK");   // 타이밍 체크 및 플레이어 애니메이션 및 레이저 회전 설정
            fourTrackPlayerAttack.QWEEffect.SetActive(true);
            fourTrackPlayerAttack.TripleAttack();
        }
     
    }
}
