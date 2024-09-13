using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class DiePanel : MonoBehaviour
{
    public VideoPlayer videoPlayer;// ���� �÷��̾ ������ ����
    public VideoClip[] videoClips;// �پ��� ĳ���Ϳ� ���� ���� Ŭ���� ������ �迭

    void OnEnable()
    {
        StartCoroutine(CameraShakes());  // ī�޶� ��鸲 ȿ���� ���� �ڷ�ƾ ����
        UpdateVideoClip(); // ���� Ŭ���� ������Ʈ�մϴ�.
    }

    void UpdateVideoClip()
    {
        if (DataManager.instance.currentCharater == Character.White)  // DataManager���� ���� ���õ� ĳ���Ϳ� ���� ���� Ŭ���� �����մϴ�.
        {
            videoPlayer.clip = videoClips[0];
        }
        else if (DataManager.instance.currentCharater == Character.Red)
        {
            videoPlayer.clip = videoClips[1];
        }
        else if (DataManager.instance.currentCharater == Character.Blue)
        {
            videoPlayer.clip = videoClips[2];
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            LoadingManager.LoadScene("Menu");// Enter Ű�� ������ Ÿ��Ʋ ������ �ε�
        }
    }

    IEnumerator CameraShakes()
    {
        yield return new WaitForSeconds(1.45f);// ��� ��
        CameraShake.instance.Shake();// ī�޶� ��鸲 ȿ���� ����
    }
}
