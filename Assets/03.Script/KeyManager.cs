using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyAction { Q, W, E, D, F, J, K, KEYCOUNT }// 키 액션을 나타내는 열거형

public static class KeySetting
{
    // KeyAction을 키로, KeyCode를 값으로 가지는 사전 초기화
    public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>()
    {
        { KeyAction.Q, KeyCode.Q },
        { KeyAction.W, KeyCode.W },
        { KeyAction.E, KeyCode.E },
        { KeyAction.D, KeyCode.D },
        { KeyAction.F, KeyCode.F },
        { KeyAction.J, KeyCode.J },
        { KeyAction.K, KeyCode.K }
    };
}


public class KeyManager : MonoBehaviour
{
    KeyCode[] defaultKeys = new KeyCode[] { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.D, KeyCode.F, KeyCode.J, KeyCode.K };

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // 게임 오브젝트를 씬 전환 시 파괴되지 않도록 설정

        // 기본 키로 키 사전 초기화
        //for (int i = 0; i < (int)KeyAction.KEYCOUNT; i++)
        //{
        //    KeySetting.keys[(KeyAction)i] = defaultKeys[i];
        //}
    }

    private void OnGUI()
    {
        Event keyEvent = Event.current;
        if (keyEvent.isKey && key != -1 && key < (int)KeyAction.KEYCOUNT)
        {
            KeySetting.keys[(KeyAction)key] = keyEvent.keyCode; // 설정할 키를 입력하면 그에 맞는 KeyCode로 업데이트
            key = -1;// 키 입력 처리 후 초기화
        }
    }

    int key = -1;

    public void ChangeKey(int num) // 외부에서 호출하여 특정 키를 변경할 수 있도록 하는 메서드
    {
        if (num >= 0 && num < (int)KeyAction.KEYCOUNT)
        {
            key = num;// 변경할 키의 인덱스를 저장
        }
        else
        {
            Debug.LogError("유효하지 않은 키 번호입니다!");// 유효하지 않은 키 번호를 입력할 경우 에러 메시지 출력
        }
    }
}
