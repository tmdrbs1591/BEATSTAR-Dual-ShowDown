using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character
{
    White,Red,Blue,Green
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;  // �̱��� ������ ���� ���� �ν��Ͻ� ����

    void Start()
    {
        if (instance == null) instance = this; // �ν��Ͻ��� null�� ���, �ڱ� �ڽ��� instance�� �Ҵ�
        else if (instance != null) return; // �̹� �ٸ� �ν��Ͻ��� �����ϸ�, �� �ν��Ͻ��� �ı��ϰ� �����մϴ�.
        DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� ��ü�� �ı����� �ʵ��� �����մϴ�.
    }



    public Character currentCharater; // ���� ���õ� ĳ���͸� ������ ����
    public string songPath;// ���õ� ���� ��θ� ������ ����

}
