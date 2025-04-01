using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    //[SerializeField] Animator noteHitAnimator = null;
    string hit = "Hit";// �ִϸ��̼� Ʈ���� �̸�
    [SerializeField] Animator judgementAnimator = null;// ���� ����Ʈ�� ����ϴ� Animator ������Ʈ
    [SerializeField] UnityEngine.UI.Image judgementImage = null; // ���� ����Ʈ �̹����� ǥ���ϴ� Image ������Ʈ
    [SerializeField] Sprite[] jugementSprite = null; // ���� ����Ʈ �̹��� ��������Ʈ �迭

    public void judgementEffect(int p_num)
    {
        judgementImage.sprite = jugementSprite[p_num]; // �־��� �ε����� �ش��ϴ� ��������Ʈ�� ����
        judgementAnimator.SetTrigger(hit); // ���� �ִϸ��̼��� ���
    }
    public void NoteHitEffect()
    {
       // noteHitAnimator.SetTrigger(hit);    
    }
}
