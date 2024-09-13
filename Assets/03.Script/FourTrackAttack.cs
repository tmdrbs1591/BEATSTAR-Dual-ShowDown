using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourTrackAttack: MonoBehaviour
{
    public PlaayerController controller;// 플레이어 컨트롤러를 참조하는 변수
    public ButtonManager buttonManager;// 버튼 매니저를 참조하는 변수

    public Transform Q;// Q 키에 해당하는 Transform
    public Transform W;// W 키에 해당하는 Transform
    public Transform E; // E 키에 해당하는 Transform
    public Transform R;// R 키에 해당하는 Transform
    public GameObject laserRotation;// 레이저 회전을 조절하는 게임 오브젝트
    public GameObject QWEEffect;// QWEEfffect를 활성화하는 게임 오브젝트
    public List<GameObject> lasers; // 리스트로 laser GameObject들을 관리

    public Animator animator;// 애니메이터 컴포넌트

    private void Start()
    {
        animator = GetComponent<Animator>();// 해당 게임 오브젝트에 있는 애니메이터 컴포넌트 가져오기   
    }

    void Update()
    {
        if (!controller.Death && !buttonManager.isCountDown) // 플레이어가 사망하지 않았고, 카운트다운이 아닌 경우에만 실행
        {
            // 모든 키(D, F, J, K)를 동시에 눌렀을 때
            if ((Input.GetKey(KeySetting.keys[KeyAction.D]) && Input.GetKey(KeySetting.keys[KeyAction.F]) && Input.GetKey(KeySetting.keys[KeyAction.J]) && Input.GetKey(KeySetting.keys[KeyAction.K])) )
            {
                QWEEffect.SetActive(true); ; // QWEEfffect 활성화
                TripleAttack();// 랜덤한 트리플 공격 실행
            }
            else if (Input.GetKeyDown(KeySetting.keys[KeyAction.D]))  // 개별 공격 키 입력 시
            {
                animator.SetTrigger("QAttack");// Q 공격 애니메이션 실행
                RotateLaser(Q); // Q 레이저 회전
                StartCoroutine(laserSetActive());// 레이저 활성화 코루틴 실행
            }
            else if (Input.GetKeyDown(KeySetting.keys[KeyAction.F]))
            {
                animator.SetTrigger("WAttack");// W 공격 애니메이션 실행
                RotateLaser(W);// W 레이저 회전
                StartCoroutine(laserSetActive()); // 레이저 활성화 코루틴 실행
            }
            else if (Input.GetKeyDown(KeySetting.keys[KeyAction.J]))
            {
                animator.SetTrigger("EAttack"); // E 공격 애니메이션 실행
                RotateLaser(E);// E 레이저 회전
                StartCoroutine(laserSetActive());// 레이저 활성화 코루틴 실행
            }
            else if (Input.GetKeyDown(KeySetting.keys[KeyAction.K]))
            {
                animator.SetTrigger("EAttack");// E 공격 애니메이션 실행
                RotateLaser(R);// R 레이저 회전
                StartCoroutine(laserSetActive()); // 레이저 활성화 코루틴 실행
            }
            else
            {
                QWEEffect.SetActive(false);// QWEEfffect 비활성화
            }
            
        }
    }


    public void TripleAttack()
    {
        int randomValue = Random.Range(0, 3); // 0, 1, 2 중에서 랜덤한 값 생성

        switch (randomValue)
        {
            case 0:
                RotateLaser(Q);
                StartCoroutine(laserSetActive()); // 레이저 활성화 코루틴 실행
                break;
            case 1:
                RotateLaser(W);
                StartCoroutine(laserSetActive());// 레이저 활성화 코루틴 실행
                break;
            case 2:
                animator.SetTrigger("EAttack"); // E 공격 애니메이션 실행
                RotateLaser(E);
                StartCoroutine(laserSetActive());// 레이저 활성화 코루틴 실행
                break;
        }
    }

    public void RotateLaser(Transform targetTransform)// 레이저를 targetTransform 방향으로 회전시키는 메서드
    {
        // 회전 방향 계산
        Vector3 direction = targetTransform.position - laserRotation.transform.position;

        // z축을 기준으로 회전 각도 계산
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        // 레이저에 회전 적용
        laserRotation.transform.rotation = rotation;
    }

    public IEnumerator laserSetActive() // 레이저를 활성화하고 일정 시간 후에 비활성화하는 코루틴
    {
        // 비활성화된 레이저 GameObject 찾기
        GameObject inactiveLaser = lasers.Find(laser => !laser.activeSelf);

        if (inactiveLaser != null)
        {
            inactiveLaser.SetActive(true); // 레이저 활성화
            yield return new WaitForSeconds(0.2f);// 0.2초 후에
            inactiveLaser.SetActive(false);// 레이저 비활성화
        }
        else
        {
            Debug.LogWarning("No inactive laser available!"); // 비활성화된 레이저가 없을 경우 경고 출력
        }
    }
}
