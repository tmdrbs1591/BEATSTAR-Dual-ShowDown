using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField] GameObject[] backGrounds; // �迭���� �ҹ��ڷ� �����ϰ�, �̸� ����

    void Update()
    {
        // ���� ���������� ���� ��� Ȱ��ȭ ���θ� ����
        for (int i = 0; i < backGrounds.Length; i++)
        {
            bool isActive = (i == (int)StagerManager.instance.currentStage);
            backGrounds[i].SetActive(isActive);
        }
    }
}
