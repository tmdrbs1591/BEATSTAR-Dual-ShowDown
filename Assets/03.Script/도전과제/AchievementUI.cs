using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour
{
    public static AchievementUI instance;

    public GameObject achievementPrefab; // 도전 과제 프리팹
    public Transform content; // Scroll View의 Content

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);  // 중복 방지
    }

    void Start()
    {
        UpdateAchievementList();
    }

    public void UpdateAchievementList()
    {
        // 기존 UI 요소 제거
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // 도전 과제 목록 생성
        foreach (var achievement in AchievementManager.instance.achievements)
        {
            GameObject achievementObj = Instantiate(achievementPrefab, content);
            TMP_Text nameText = achievementObj.transform.Find("AchievementNameText").GetComponent<TMP_Text>();
            TMP_Text descriptionText = achievementObj.transform.Find("AchievementDescriptionText").GetComponent<TMP_Text>();
            Slider progressSlider = achievementObj.transform.Find("ProgressSlider").GetComponent<Slider>(); // 진행도 슬라이더
            TMP_Text progressText = achievementObj.transform.Find("ProgressText").GetComponent<TMP_Text>(); // 진행도 텍스트
            Button rewardButton = achievementObj.GetComponentInChildren<Button>();
            TMP_Text rewardCurrencyText = rewardButton.transform.Find("RewardCurrencyText").GetComponent<TMP_Text>(); // RewardCurrencyText
            GameObject realCompleteImage = achievementObj.transform.Find("RealCompleteImage").gameObject; // Complete 이미지
            GameObject completeImage = achievementObj.transform.Find("CompleteImage").gameObject; // Complete 이미지

            nameText.text = achievement.name;
            descriptionText.text = achievement.description;

            // 슬라이더의 최대값과 현재값 설정
            progressSlider.maxValue = achievement.target;
            progressSlider.value = achievement.currentValue;

            // 진행도 텍스트 업데이트 (현재 값 / 목표 값 형식)
            progressText.text = $"{achievement.currentValue} / {achievement.target}";

            rewardCurrencyText.text = achievement.rewardCurrency.ToString();

            if (!achievement.isRealCompleted)
            {
                // 도전 과제가 완료되었으면 버튼 활성화
                if (achievement.isCompleted)
                {
                    rewardButton.interactable = true; // 도전 과제가 완료된 경우 버튼 활성화
                    rewardButton.onClick.RemoveAllListeners(); // 기존 리스너 제거
                    rewardButton.onClick.AddListener(() => RewardAchievement(achievement, rewardButton, realCompleteImage)); // 클릭 시 보상 메서드 호출
                    completeImage.SetActive(true);
                }
                else
                {
                    rewardButton.interactable = false; // 도전 과제가 완료되지 않은 경우 버튼 비활성화
                    realCompleteImage.SetActive(false); // 완료 이미지 비활성화
                }
            }
            else
            {
                realCompleteImage.SetActive(true); // 완료 이미지 활성화
            }
        }
    }

    private void RewardAchievement(Achievement achievement, Button button, GameObject completeImage)
    {
        if (achievement.isCompleted) // 도전 과제가 완료된 경우에만
        {
            if (button.interactable) // 버튼이 활성화된 경우에만
            {
                achievement.GiveReward(); // 보상 지급
                button.interactable = false; // 버튼 비활성화
                completeImage.SetActive(true); // 완료 이미지 활성화
                achievement.isRealCompleted = true;
            }
        }
    }
}
