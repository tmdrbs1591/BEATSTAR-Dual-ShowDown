using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;

    public List<Achievement> achievements = new List<Achievement>(); // 도전 과제 목록

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);  // 중복 방지
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            AchievementManager.instance.AddProgress("리듬의 기초", 1);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            AchievementManager.instance.AddProgress("첫 발자국", 1);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            AchievementManager.instance.AddProgress("리듬의 조율", 1);
        }
    }
    private void Start()
    {
        // 초기 도전 과제 설정 (예시)
        // 더 많은 도전 과제 추가...
    }

    public void AddProgress(string achievementName, int amount)
    {
        Achievement achievement = achievements.Find(a => a.name == achievementName);
        if (achievement != null)
        {
            achievement.UpdateProgress(amount); // 진행도 업데이트
            AchievementUI.instance.UpdateAchievementList(); // 즉시 UI 갱신
        }
    }



}
