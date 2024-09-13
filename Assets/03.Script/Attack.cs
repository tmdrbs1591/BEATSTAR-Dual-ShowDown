using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public PlaayerController controller;
    public ButtonManager buttonManager;

    public Transform Q; // Q 공격 포지션
    public Transform W;  // W 공격 포지션
    public Transform E;   // E 공격 포지션
    public GameObject laserRotation;    //총알발사 레이저 방향값 조절하기 위해 로테이션 변경할 오브젝트 가져오기
    public GameObject QWEEffect; // QWE 누를때 나올 이펙트 오브젝트
    public List<GameObject> lasers; // 리스트로 laser GameObject들을 관리

    public Animator animator; // 애니메이터

    private void Start()
    {
        animator = GetComponent<Animator>(); // 초기화
    }

    void Update()
    {
        if (!controller.Death && !buttonManager.isCountDown) // 죽지 않거나 카운트 중이 아닐때만
        {
            if ((Input.GetKey(KeySetting.keys[KeyAction.Q]) && Input.GetKey(KeySetting.keys[KeyAction.W]) && Input.GetKey(KeySetting.keys[KeyAction.E])))// QWE동시에 누르면 
            {
                QWEEffect.SetActive(true); //이펙트 키기
                TripleAttack(); // 트리플 어택 함수 호출 (총알발사 이펙트)
            }
            else if (Input.GetKeyDown(KeySetting.keys[KeyAction.Q])) // Q누르기
            {
                animator.SetTrigger("QAttack"); // Q 애니메이션 트리거 키기
                RotateLaser(Q); // 레이저를 Transform 매개변수 Q 방향으로 Rotate
                StartCoroutine(laserSetActive()); // 총알 발사 이펙트
            }
            else if (Input.GetKeyDown(KeySetting.keys[KeyAction.W]))
            {
                animator.SetTrigger("WAttack"); // W애니메이ㅕ션 트리거 켜기 
                RotateLaser(W); // 레이저를 Transform 매개변수 E방향으로 Rotate
                StartCoroutine(laserSetActive()); // 총알 발사 이펙트
            }
            else if (Input.GetKeyDown(KeySetting.keys[KeyAction.E]))
            {
                animator.SetTrigger("EAttack"); // E애니메이션 트리거 켜기
                RotateLaser(E); // 레이저를 Transform 매개변수 E방향으로 Rotate
                StartCoroutine(laserSetActive());// 총알 발사 이펙트
            }
            else
            {
                QWEEffect.SetActive(false); //아무것도 안하고 있는 상태에선 QWE 이펙트 꺼놓기
            }

        }
    }


    public void TripleAttack() // QWE 동시에 누를때 할 이펙트 
    {
        int randomValue = Random.Range(0, 3); // 0, 1, 2 중에서 랜덤한 값 생성

        switch (randomValue) // Q W E 위치의 랜덤값으로 로테이션 변경
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

    public void RotateLaser(Transform targetTransform) // 레이저 로테이션 변경
    {
        // 레이저에서 타겟까지의 방향 벡터 계산
        Vector3 direction = targetTransform.position - laserRotation.transform.position;

        // 타겟을 향해 레이저를 회전시키기 위해 필요한 각도를 라디안에서 도 단위로 변환하여 계산
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // z 축을 중심으로 하는 회전을 나타내는 쿼터니언 생성
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        // 계산된 회전을 레이저에 적용
        laserRotation.transform.rotation = rotation;
    }

    public IEnumerator laserSetActive()
    {
        // 비활성화된 레이저 GameObject를 찾음
        GameObject inactiveLaser = lasers.Find(laser => !laser.activeSelf);

        // 비활성화된 레이저가 존재하면
        if (inactiveLaser != null)
        {
            // 레이저를 활성화
            inactiveLaser.SetActive(true);

            // 0.2초 동안 대기
            yield return new WaitForSeconds(0.2f);

            // 레이저를 다시 비활성화
            inactiveLaser.SetActive(false);
        }
        else
        {
            // 비활성화된 레이저가 없으면 경고 메시지를 출력
            Debug.LogWarning("No inactive laser available!");
        }
    }

}
