using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlaayerController : MonoBehaviour
{
    TimingManager theTimingManager;// 타이밍 관리자 객체
    public ButtonManager buttonManager;// 버튼 관리자 객체

    public GameObject DiePanel; //죽으면 나오는 패널
    public GameObject HitPanel;//빨간색 히트 이펙트 패널
    public GameObject QPanel; //Q 줄 표시
    public GameObject WPanel;//W 줄 표시
    public GameObject EPanel;//E 줄 표시

    public bool Death = false;//현재 죽어있는지 확인하는 불값
    public int MaxHP;//최대 HP
    public int CurHP;//현재 HP
    public Vector2 appearPosition;//처음에 움직일때 도착할 위치값
    private Animator animator;

    [SerializeField]
    Slider HpBar; // 체력바 UI
    [SerializeField]
    Slider HpBar2;// 체력바 2번째 UI

    public bool invincibility; // 무적인지
    void Start()
    {
        transform.position = new Vector3(-11f, -2.82f,2);// 시작 위치 설정
        animator = GetComponent<Animator>(); // Animator 컴포넌트 가져오기
        StartCoroutine(ApplyRootMotion()); // Root 모션 적용

        // appearPosition으로 2초 동안 부드럽게 이동
        transform.DOMove(appearPosition, 2f);
        theTimingManager = FindObjectOfType<TimingManager>(); // TimingManager 찾아서 설정하기

    }

    void Update()
    {
        if (!buttonManager.isCountDown) {
            // HP바 값 조정 (부드럽게)
            HpBar.value = Mathf.Lerp(HpBar.value, (float)CurHP / (float)MaxHP, Time.deltaTime * 20);
        HpBar2.value = Mathf.Lerp(HpBar2.value, (float)CurHP / (float)MaxHP, Time.deltaTime * 3);

        Die();// 사망 체크
            Key(); // 키 입력 처리
            FourTrackKey();// 4트랙 키 입력 처리
        }
    }
    public void TakeDamage(int damage) // 데미지 입기
    {
        if (invincibility)
            return;
        CurHP -= damage;// 현재 HP에서 데미지만큼 감소
        StartCoroutine(Hit());// 피격 이펙트 재생
        CameraShake.instance.Shake();// 카메라 흔들림 효과
    }
    IEnumerator Hit()//패널 한번 깜빡이는 코루틴
    {
        HitPanel.SetActive(true); // 피격 이펙트 패널 활성화
        yield return new WaitForSeconds(0.25f);// 0.25초 대기
        HitPanel.SetActive(false); // 피격 이펙트 패널 비활성화

    }
    void Die() // 사망 상태 체크
    {
        if (CurHP <= 0)
        {
            Death = true;// 사망 상태 설정
            DiePanel.SetActive(true); // 사망 패널 활성화

        }
    }
    void Key()
    {

        if (!Death)
        {
            bool qPressed = Input.GetKeyDown(KeySetting.keys[KeyAction.Q]);
            bool wPressed = Input.GetKeyDown(KeySetting.keys[KeyAction.W]);
            bool ePressed = Input.GetKeyDown(KeySetting.keys[KeyAction.E]);

            // Q와 W가 동시에 눌린 경우
            if ((Input.GetKey(KeySetting.keys[KeyAction.Q]) && wPressed) || (Input.GetKey(KeySetting.keys[KeyAction.W]) && qPressed))
            {
                theTimingManager.CheckTimingWithKey("QW");
                return; // 동시 입력 처리 후 함수 종료
            }
            // W 또는 O가 눌린 상태에서 E 또는 P가 눌리면
            if ((Input.GetKey(KeySetting.keys[KeyAction.W]) && ePressed) || (Input.GetKey(KeySetting.keys[KeyAction.E]) && wPressed))
            {
                theTimingManager.CheckTimingWithKey("EW");
                return; // 동시 입력 처리 후 함수 종료
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
        animator.applyRootMotion = true; // Root 모션 활성화
        yield return new WaitForSeconds(2f);// 2초 대기
        animator.applyRootMotion = false; // Root 모션 비활성화
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
            return; // 동시 입력 처리 후 함수 종료
        }
        // W 또는 O가 눌린 상태에서 E 또는 P가 눌리면
        if ((Input.GetKey(KeySetting.keys[KeyAction.J]) && kPressed) || (Input.GetKey(KeySetting.keys[KeyAction.K]) && jPressed))
        {
            theTimingManager.CheckTimingWithKey("JK");
            return; // 동시 입력 처리 후 함수 종료
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
