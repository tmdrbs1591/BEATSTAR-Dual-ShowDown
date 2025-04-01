using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyAction { Q, W, E, D, F, J, K, KEYCOUNT }// Ű �׼��� ��Ÿ���� ������

public static class KeySetting
{
    // KeyAction�� Ű��, KeyCode�� ������ ������ ���� �ʱ�ȭ
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
        DontDestroyOnLoad(gameObject); // ���� ������Ʈ�� �� ��ȯ �� �ı����� �ʵ��� ����

        // �⺻ Ű�� Ű ���� �ʱ�ȭ
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
            KeySetting.keys[(KeyAction)key] = keyEvent.keyCode; // ������ Ű�� �Է��ϸ� �׿� �´� KeyCode�� ������Ʈ
            key = -1;// Ű �Է� ó�� �� �ʱ�ȭ
        }
    }

    int key = -1;

    public void ChangeKey(int num) // �ܺο��� ȣ���Ͽ� Ư�� Ű�� ������ �� �ֵ��� �ϴ� �޼���
    {
        if (num >= 0 && num < (int)KeyAction.KEYCOUNT)
        {
            key = num;// ������ Ű�� �ε����� ����
        }
        else
        {
            Debug.LogError("��ȿ���� ���� Ű ��ȣ�Դϴ�!");// ��ȿ���� ���� Ű ��ȣ�� �Է��� ��� ���� �޽��� ���
        }
    }
}
