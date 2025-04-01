using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField] GameObject goComboImage = null;// 콤보 이미지를 표시할 GameObject 변수
    [SerializeField] TMPro.TMP_Text txtCombo = null;// 콤보 텍스트를 표시할 TMPro의 TextMeshPro Text 변수

    int currentCombo = 0;// 현재 콤보 수
    int maxCombo = 0;// 최대 콤보 수

    Animator myAnim;// Animator 컴포넌트를 저장할 변수
    string animComboUp = "ComboUp"; // Animator에서 사용할 트리거 이름

    private void Start()
    {
        myAnim = GetComponent<Animator>(); // 자신의 GameObject에서 Animator 컴포넌트 가져오기
        txtCombo.gameObject.SetActive(false); // 시작할 때 콤보 텍스트를 비활성화
        goComboImage.SetActive(false);// 시작할 때 콤보 이미지를 비활성화
    }

    public void IncreaseCombo(int p_num = 1) {
        currentCombo += p_num;// 콤보 수 증가
        txtCombo.text = string.Format("{0:#,##0}", currentCombo);// 현재 콤보 수를 텍스트로 표시

        if (maxCombo < currentCombo)
            maxCombo = currentCombo; // 최대 콤보 수 업데이트

        if (currentCombo > 2)
        {
            txtCombo.gameObject.SetActive(true);// 콤보 텍스트 활성화
            goComboImage.SetActive(true); // 콤보 이미지 활성화

            myAnim.SetTrigger(animComboUp);  // ComboUp 트리거를 통해 Animator 애니메이션 재생
        }
    }
    public int GetCurrentCombo()
    {
        return currentCombo; // 현재 콤보 수 반환
    }
    public void ResetCombo()
    {
        currentCombo = 0;  // 콤보 수 초기화
        txtCombo.text = "0";  // 텍스트 초기화
        txtCombo.gameObject.SetActive(false); // 콤보 텍스트 비활성화
        goComboImage.SetActive(false);// 콤보 이미지 비활성화
    } 
    public int GetMaxCombo()
    {
        return maxCombo; // 최대 콤보 수 반환
    }
}
