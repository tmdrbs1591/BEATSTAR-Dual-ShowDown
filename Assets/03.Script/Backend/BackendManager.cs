using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd; // 뒤끝 SDK
using UnityEngine.UIElements;

public class BackendManager : MonoBehaviour
{

    void Awake()
    {
        //업데ㅣ트 메소드의 backend.AsynPoll(); 호출을 위해 오브젝트를 파괴하지 않는다.
        DontDestroyOnLoad(gameObject);
        BackendSetup();
    }

    void BackendSetup()
    {
        // 뒤끝 초기화
        var bro = Backend.Initialize(true);

        //뒤끝 초기화에 대한 응답값
        if (bro.IsSuccess())
        {
            // 초기화 성공시 stausCode 204 Success
            Debug.Log($"초기화 성공 : {bro}");
        }
        else
        {
            //초기화 실패시 statusCode 400대 에러 발생
            Debug.LogError($"초기화 실패 : {bro}");

        }
    }


    void Update()
    {
        // 서버의 비동기 메소드 호출(콜백 함수 풀링)을 위해 작성
        if (Backend.IsInitialized)
        {
            Backend.AsyncPoll();
        }
    }
}
