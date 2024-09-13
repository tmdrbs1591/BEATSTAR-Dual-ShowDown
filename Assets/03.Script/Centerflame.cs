using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centerflame : MonoBehaviour
{

    AudioSource myAudio;// AudioSource ������Ʈ�� ������ ����
    bool musicStart = false;    // ������ ���۵Ǿ����� ���θ� ��Ÿ���� �÷���

    private void Start()
    {
        myAudio = GetComponent<AudioSource>(); // �ڽ��� GameObject���� AudioSource ������Ʈ ��������
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ������ ���۵��� ���� ���¿����� ó��
        if (!musicStart)
        {
            if (collision.CompareTag("Note"))// �浹�� ��ü�� �±װ� "Note"�� ���
            {
                myAudio.Play();// AudioSource ���
                musicStart = true;// ���� ���� �÷��׸� true�� �����Ͽ� �ߺ� ��� ����
            }
        }
    }
}
