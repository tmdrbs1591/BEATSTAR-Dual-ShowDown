using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance; // ��� �Ŵ����� �ν��Ͻ��� �������� �����ϴ� ����

    public int allGold; // ��ü ��� �ݾ�

    [SerializeField] private int branch; // �б� ����
    [SerializeField] private string tier; // Ƽ�� ����
    [SerializeField] private DataBase database; // �����ͺ��̽� ����

    [SerializeField] TMP_Text goldText; // UI�� ��� �ݾ��� ǥ���� TMP_Text

    private void Awake()
    {
        instance = this; // �ν��Ͻ� �ʱ�ȭ
        DontDestroyOnLoad(gameObject); // �� ��ȯ �� �������� �ʵ��� ����
    }

    private void Update()
    {
        goldText.text = database.Entities[6].gold.ToString(); // UI�� �����ͺ��̽����� ������ ��� �ݾ� ǥ��

        // B Ű�� ������ S Ƽ���� ��带 Ŭ����
        if (Input.GetKeyDown(KeyCode.B))
        {
            CrearGold("S");
        }

        // P Ű�� ������ �����ͺ��̽��� 6��° ��ƼƼ�� ��带 �ʱ�ȭ
        if (Input.GetKeyDown(KeyCode.P))
        {
            database.Entities[6].gold = 0;
        }
    }

    // ������ Ƽ���� ��带 Ŭ�����ϴ� �޼���
    public void CrearGold(string clearTier)
    {
        for (int i = 0; i < database.Entities.Count; ++i)
        {
            // �����ͺ��̽����� �ش� Ƽ� ã�Ƽ� ��带 �߰�
            if (database.Entities[i].tier == clearTier)
            {
                database.Entities[6].gold += database.Entities[i].gold; // �����ͺ��̽��� 6��° ��ƼƼ�� ��� �߰�
                Debug.Log(database.Entities[6].gold); // ����� �α׷� ���� ��� ���
            }
        }
    }
}
