using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBox : MonoBehaviour //�ڵ����� �Ҷ� �ʿ���  Auto Box
{
    // Ÿ�̹� �Ŵ��� ��ü
    TimingManager theTimingManager;
    // 3Ʈ�� ���� ��ũ��Ʈ
    public Attack playerAttack;
    // 4Ʈ�� ����    ��ũ��Ʈ
    public FourTrackAttack fourTrackPlayerAttack; 
    void Start()
    {
        // Ÿ�̹� �Ŵ����� Scene���� ã�Ƽ� �Ҵ�
        theTimingManager = FindObjectOfType<TimingManager>();

    }


    void Update()
    {
        // �÷��̾� 3Ʈ�� ���� ��ũ��Ʈ�� �Ҵ���� �ʾ����� ã�Ƽ� �Ҵ�
        if (playerAttack != null)
        {
            playerAttack = FindObjectOfType<Attack>();
        }
        if (fourTrackPlayerAttack != null)      // �÷��̾�4Ʈ�� ���� ��ũ��Ʈ�� �Ҵ���� �ʾ����� ã�Ƽ� �Ҵ�
        {
            fourTrackPlayerAttack = FindObjectOfType<FourTrackAttack>();

        }
    }
    // �浹 �߻� �� ȣ��Ǵ� �޼���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ��ü���� Note ������Ʈ�� ������
        Note note = collision.GetComponent<Note>();


        // ��Ʈ�� noteKey�� ���� ó��
        if (note.noteKey == "Q")
        {
            theTimingManager.CheckTimingWithKey("Q");   // Ÿ�̹� üũ �� �÷��̾� �ִϸ��̼� �� ������ ȸ�� ����
            playerAttack.animator.SetTrigger("QAttack");
            playerAttack.RotateLaser(playerAttack.Q);
            playerAttack.StartCoroutine(playerAttack.laserSetActive());

        }
        if (note.noteKey == "W")
        {
            theTimingManager.CheckTimingWithKey("W");   // Ÿ�̹� üũ �� �÷��̾� �ִϸ��̼� �� ������ ȸ�� ����
            playerAttack.animator.SetTrigger("WAttack");
            playerAttack.RotateLaser(playerAttack.W);
            playerAttack.StartCoroutine(playerAttack.laserSetActive());
        }
        if (note.noteKey == "E")
        {
            theTimingManager.CheckTimingWithKey("E");   // Ÿ�̹� üũ �� �÷��̾� �ִϸ��̼� �� ������ ȸ�� ����
            playerAttack.animator.SetTrigger("EAttack");
            playerAttack.RotateLaser(playerAttack.E);
            playerAttack.StartCoroutine(playerAttack.laserSetActive());
        }
        if (note.noteKey == "QW")
        {
            theTimingManager.CheckTimingWithKey("QW");   // Ÿ�̹� üũ �� �÷��̾� �ִϸ��̼� �� ������ ȸ�� ����
            playerAttack.animator.SetTrigger("QAttack");
            playerAttack.RotateLaser(playerAttack.W);
            playerAttack.StartCoroutine(playerAttack.laserSetActive());
        }
        if (note.noteKey == "EW")
        {
            theTimingManager.CheckTimingWithKey("EW");   // Ÿ�̹� üũ �� �÷��̾� �ִϸ��̼� �� ������ ȸ�� ����
            playerAttack.animator.SetTrigger("QAttack");
            playerAttack.RotateLaser(playerAttack.W);
            playerAttack.StartCoroutine(playerAttack.laserSetActive());
        }
        if (note.noteKey == "Space")
        {
            theTimingManager.CheckTimingWithKey("Space");   // Ÿ�̹� üũ �� �÷��̾� �ִϸ��̼� �� ������ ȸ�� ����
            playerAttack.QWEEffect.SetActive(true);
            playerAttack.TripleAttack();
        }
        if (note.noteKey == "D")
        {
            theTimingManager.CheckTimingWithKey("D");   // Ÿ�̹� üũ �� �÷��̾� �ִϸ��̼� �� ������ ȸ�� ����
            fourTrackPlayerAttack.animator.SetTrigger("QAttack");
            fourTrackPlayerAttack.RotateLaser(fourTrackPlayerAttack.Q);
            fourTrackPlayerAttack.StartCoroutine(fourTrackPlayerAttack.laserSetActive());
        }
        if (note.noteKey == "F")
        {
            theTimingManager.CheckTimingWithKey("F");   // Ÿ�̹� üũ �� �÷��̾� �ִϸ��̼� �� ������ ȸ�� ����
            fourTrackPlayerAttack.animator.SetTrigger("QAttack");
            fourTrackPlayerAttack.RotateLaser(fourTrackPlayerAttack.W);
            fourTrackPlayerAttack.StartCoroutine(fourTrackPlayerAttack.laserSetActive());
        }
        if (note.noteKey == "J")
        {
            theTimingManager.CheckTimingWithKey("J");   // Ÿ�̹� üũ �� �÷��̾� �ִϸ��̼� �� ������ ȸ�� ����
            fourTrackPlayerAttack.animator.SetTrigger("WAttack");
            fourTrackPlayerAttack.RotateLaser(fourTrackPlayerAttack.E);
            fourTrackPlayerAttack.StartCoroutine(fourTrackPlayerAttack.laserSetActive());
        }
        if (note.noteKey == "K")
        {
            theTimingManager.CheckTimingWithKey("K");   // Ÿ�̹� üũ �� �÷��̾� �ִϸ��̼� �� ������ ȸ�� ����
            fourTrackPlayerAttack.animator.SetTrigger("EAttack");
            fourTrackPlayerAttack.RotateLaser(fourTrackPlayerAttack.R);
            fourTrackPlayerAttack.StartCoroutine(fourTrackPlayerAttack.laserSetActive());
        }
        if (note.noteKey == "DF")
        {
            theTimingManager.CheckTimingWithKey("DF");   // Ÿ�̹� üũ �� �÷��̾� �ִϸ��̼� �� ������ ȸ�� ����
            fourTrackPlayerAttack.animator.SetTrigger("WAttack");
            fourTrackPlayerAttack.RotateLaser(fourTrackPlayerAttack.W);
            fourTrackPlayerAttack.StartCoroutine(fourTrackPlayerAttack.laserSetActive());
        }
        if (note.noteKey == "JK")
        {
            theTimingManager.CheckTimingWithKey("JK");   // Ÿ�̹� üũ �� �÷��̾� �ִϸ��̼� �� ������ ȸ�� ����
            fourTrackPlayerAttack.animator.SetTrigger("EAttack");
            fourTrackPlayerAttack.RotateLaser(fourTrackPlayerAttack.E);
            fourTrackPlayerAttack.StartCoroutine(fourTrackPlayerAttack.laserSetActive());
        }
        if (note.noteKey == "DFJK")
        {
            theTimingManager.CheckTimingWithKey("DFJK");   // Ÿ�̹� üũ �� �÷��̾� �ִϸ��̼� �� ������ ȸ�� ����
            fourTrackPlayerAttack.QWEEffect.SetActive(true);
            fourTrackPlayerAttack.TripleAttack();
        }
     
    }
}
