using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd; // �ڳ� SDK
using UnityEngine.UIElements;

public class BackendManager : MonoBehaviour
{

    void Awake()
    {
        //������Ʈ �޼ҵ��� backend.AsynPoll(); ȣ���� ���� ������Ʈ�� �ı����� �ʴ´�.
        DontDestroyOnLoad(gameObject);
        BackendSetup();
    }

    void BackendSetup()
    {
        // �ڳ� �ʱ�ȭ
        var bro = Backend.Initialize(true);

        //�ڳ� �ʱ�ȭ�� ���� ���䰪
        if (bro.IsSuccess())
        {
            // �ʱ�ȭ ������ stausCode 204 Success
            Debug.Log($"�ʱ�ȭ ���� : {bro}");
        }
        else
        {
            //�ʱ�ȭ ���н� statusCode 400�� ���� �߻�
            Debug.LogError($"�ʱ�ȭ ���� : {bro}");

        }
    }


    void Update()
    {
        // ������ �񵿱� �޼ҵ� ȣ��(�ݹ� �Լ� Ǯ��)�� ���� �ۼ�
        if (Backend.IsInitialized)
        {
            Backend.AsyncPoll();
        }
    }
}
