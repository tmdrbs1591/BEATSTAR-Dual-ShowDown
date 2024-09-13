using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearPanel : MonoBehaviour
{
    public Sprite[] sprites;  // Ŭ���� �гο��� ����� ��������Ʈ �迭
    public Image image;// �̹��� ������Ʈ
    public string scenceName = "StageMode";
    void OnEnable()
    {
        StartCoroutine(CameraShakes()); // ī�޶� ��鸲 ȿ���� �����ϴ� �ڷ�ƾ�� ����
        image.sprite = sprites[0];// �ʱ� �̹��� ��������Ʈ�� ����

        if (DataManager.instance.currentCharater == Character.White)  // DataManager���� ���� ĳ���� ������ ������ �ش��ϴ� ��������Ʈ�� ����
        {
            image.sprite = sprites[0];// ù��° ĳ���Ϳ� �ش��ϴ� ��������Ʈ 
        }
        else if (DataManager.instance.currentCharater == Character.Red)
        {
            image.sprite = sprites[1]; // �ι�° ĳ���Ϳ� �ش��ϴ� ��������Ʈ 
        }
        else if (DataManager.instance.currentCharater == Character.Blue)
        {
            image.sprite = sprites[2];// ����° ĳ���Ϳ� �ش��ϴ� ��������Ʈ
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))// Enter Ű�� ������
        {
            LoadingManager.LoadScene(scenceName);// Title ������ �ε�
        }
    }

    IEnumerator CameraShakes()
    {
        yield return new WaitForSeconds(1.45f);// 1.45�� �Ŀ�
        CameraShake.instance.Shake();// ī�޶� �����ִ� Shake �޼��带 ȣ��
    }
}
