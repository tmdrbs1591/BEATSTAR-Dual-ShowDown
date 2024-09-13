using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingImage : MonoBehaviour
{
    [SerializeField]
    Image[] backGroundImage; // �̹��� �迭

    void Start()
    {
        // �迭�� �̹����� �Ҵ�Ǿ� �ִ��� Ȯ��
        if (backGroundImage.Length > 0)
        {
            // �迭 ���� ������ ������ �ε��� ���� 
            int randomIndex = Random.Range(0, backGroundImage.Length);

            // ���õ� �̹��� Ȱ��ȭ
            backGroundImage[randomIndex].gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No images assigned to backGroundImage array.");// �迭�� �̹����� �Ҵ���� �ʾ��� ��� ��� ���
        }
    }

    void Update()
    {
        // You can add update logic here if needed
    }
}
