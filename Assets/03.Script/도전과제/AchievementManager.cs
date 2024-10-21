using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;

    public List<Achievement> achievements = new List<Achievement>(); // ���� ���� ���

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);  // �ߺ� ����
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            AchievementManager.instance.AddProgress("������ ����", 1);
        }
    }
    private void Start()
    {
        // �ʱ� ���� ���� ���� (����)
        // �� ���� ���� ���� �߰�...
    }

    public void AddProgress(string achievementName, int amount)
    {
        Achievement achievement = achievements.Find(a => a.name == achievementName);
        if (achievement != null)
        {
            achievement.UpdateProgress(amount);
            // ���� ������ �Ϸ�Ǹ� UI ���� ȣ��
            if (achievement.isCompleted)
            {
                AchievementUI.instance.UpdateAchievementList(); // UI ����
            }
        }
    }

}
