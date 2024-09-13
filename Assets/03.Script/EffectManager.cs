using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    //[SerializeField] Animator noteHitAnimator = null;
    string hit = "Hit";// 애니메이션 트리거 이름
    [SerializeField] Animator judgementAnimator = null;// 판정 이펙트를 담당하는 Animator 컴포넌트
    [SerializeField] UnityEngine.UI.Image judgementImage = null; // 판정 이펙트 이미지를 표시하는 Image 컴포넌트
    [SerializeField] Sprite[] jugementSprite = null; // 판정 이펙트 이미지 스프라이트 배열

    public void judgementEffect(int p_num)
    {
        judgementImage.sprite = jugementSprite[p_num]; // 주어진 인덱스에 해당하는 스프라이트를 설정
        judgementAnimator.SetTrigger(hit); // 판정 애니메이션을 재생
    }
    public void NoteHitEffect()
    {
       // noteHitAnimator.SetTrigger(hit);    
    }
}
