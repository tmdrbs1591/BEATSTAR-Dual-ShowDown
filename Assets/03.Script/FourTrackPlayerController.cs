using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FourTrackPlayerController : MonoBehaviour // 코드 삭제 예정이라 주석 없음
{
    TimingManager theTimingManager;

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
    Slider HpBar;
    [SerializeField]
    Slider HpBar2;

    public bool invincibility; // 무적인지
    void Start()
    {
        transform.position = new Vector3(-11f, -2.82f, 2);
        animator = GetComponent<Animator>();
        StartCoroutine(ApplyRootMotion());
        transform.DOMove(appearPosition, 2f);
        theTimingManager = FindObjectOfType<TimingManager>();

    }

    void Update()
    {
        HpBar.value = Mathf.Lerp(HpBar.value, (float)CurHP / (float)MaxHP, Time.deltaTime * 20);
        HpBar2.value = Mathf.Lerp(HpBar2.value, (float)CurHP / (float)MaxHP, Time.deltaTime * 3);

        Die();
        Key();
    }
    public void TakeDamage(int damage) // 데미지 입기
    {
        if (invincibility)
            return;
        CurHP -= damage;
        StartCoroutine(Hit());
        CameraShake.instance.Shake();
    }
    IEnumerator Hit()//패널 한번 깜빡이는 코루틴
    {
        HitPanel.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        HitPanel.SetActive(false);

    }
    void Die()
    {
        if (CurHP <= 0)
        {
            Death = true;
            DiePanel.SetActive(true);

        }
    }
    void Key()
    {

        if (!Death)
        {
            bool dPressed = Input.GetKeyDown(KeySetting.keys[KeyAction.D]);
            bool fPressed = Input.GetKeyDown(KeySetting.keys[KeyAction.F]);
            bool jPressed = Input.GetKeyDown(KeySetting.keys[KeyAction.J]);
            bool kPressed = Input.GetKeyDown(KeySetting.keys[KeyAction.K]);

            if ((Input.GetKey(KeySetting.keys[KeyAction.Q]) && fPressed) || (Input.GetKey(KeySetting.keys[KeyAction.W]) && dPressed))
            {
                theTimingManager.CheckTimingWithKey("QW");
                return; // 동시 입력 처리 후 함수 종료
            }
            // W 또는 O가 눌린 상태에서 E 또는 P가 눌리면
            if ((Input.GetKey(KeySetting.keys[KeyAction.W]) && jPressed) || (Input.GetKey(KeySetting.keys[KeyAction.E]) && fPressed))
            {
                theTimingManager.CheckTimingWithKey("EW");
                return; // 동시 입력 처리 후 함수 종료
            }
            if (dPressed)
            {
                QPanel.SetActive(true);
                theTimingManager.CheckTimingWithKey("Q");

            }
            if (fPressed)
            {
                WPanel.SetActive(true);
                theTimingManager.CheckTimingWithKey("W");

            }
            if (jPressed)
            {
                EPanel.SetActive(true);
                theTimingManager.CheckTimingWithKey("E");

            }
            if (Input.GetKey(KeySetting.keys[KeyAction.D]) && Input.GetKey(KeySetting.keys[KeyAction.F]) && Input.GetKey(KeySetting.keys[KeyAction.J]))
            {
                theTimingManager.CheckTimingWithKey("Space");

            }
        }

        if (Input.GetKeyUp(KeyCode.D) && !Death)
        {
            QPanel.SetActive(false);

        }
        else if (Input.GetKeyUp(KeyCode.F) && !Death)
        {
            WPanel.SetActive(false);

        }
        else if (Input.GetKeyUp(KeyCode.J) && !Death)
        {
            EPanel.SetActive(false);

        }
    }
    IEnumerator ApplyRootMotion()
    {
        animator.applyRootMotion = true;
        yield return new WaitForSeconds(2f);
        animator.applyRootMotion = false;
    }
}
