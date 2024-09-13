using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public PlaayerController Controller;// �÷��̾� ��Ʈ�ѷ� ���� ����
    public AudioSource audioSource;// AudioSource ������Ʈ ���� ����

    void Update()
    {
        if (Controller != null) // Controller�� null�� �ƴ� ���, �� �÷��̾� ��Ʈ�ѷ��� ������ ��
        {
            Controller = FindObjectOfType<PlaayerController>(); // Scene���� �÷��̾� ��Ʈ�ѷ��� ã�Ƽ� �Ҵ�

        }
        if (Controller.Death && audioSource.isPlaying)    // Controller�� Death �����̰�, ���� AudioSource�� ��� ���� ���
        {
            AudioManager.instance.PlaySound(transform.position, 1, Random.Range(1f, 1f), 1); // Ư�� ���� ���

            audioSource.Stop(); // AudioSource ����
        }
    }
    public void SetMusicVolume(float volume)    // ������ ������ �����ϴ� �޼���
    {
        audioSource.volume = volume;
    }
}
