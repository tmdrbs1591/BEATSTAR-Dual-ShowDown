using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour
{
    public static AchievementUI instance;

    public GameObject achievementPrefab; // ���� ���� ������
    public Transform content; // Scroll View�� Content

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);  // �ߺ� ����
    }

    void Start()
    {
        UpdateAchievementList();
    }

    public void UpdateAchievementList()
    {
        // ���� UI ��� ����
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // ���� ���� ��� ����
        foreach (var achievement in AchievementManager.instance.achievements)
        {
            GameObject achievementObj = Instantiate(achievementPrefab, content);
            TMP_Text nameText = achievementObj.transform.Find("AchievementNameText").GetComponent<TMP_Text>();
            TMP_Text descriptionText = achievementObj.transform.Find("AchievementDescriptionText").GetComponent<TMP_Text>();
            TMP_Text progressText = achievementObj.transform.Find("ProgressText").GetComponent<TMP_Text>(); // ���൵ �ؽ�Ʈ
            Button rewardButton = achievementObj.GetComponentInChildren<Button>();
            TMP_Text rewardCurrencyText = rewardButton.transform.Find("RewardCurrencyText").GetComponent<TMP_Text>(); // RewardCurrencyText
            GameObject completeImage = achievementObj.transform.Find("CompleteImage").gameObject; // Complete �̹���

            nameText.text = achievement.name;
            descriptionText.text = achievement.description;
            // ���൵ ǥ��
            progressText.text = achievement.currentValue + "/" + achievement.target;
            rewardCurrencyText.text = achievement.rewardCurrency.ToString();

            // ���� ������ �Ϸ�Ǿ����� ��ư Ȱ��ȭ
            if (achievement.isCompleted)
            {
                rewardButton.interactable = true; // ���� ������ �Ϸ�� ��� ��ư Ȱ��ȭ
                rewardButton.onClick.RemoveAllListeners(); // ���� ������ ����
                rewardButton.onClick.AddListener(() => RewardAchievement(achievement, rewardButton, completeImage)); // Ŭ�� �� ���� �޼��� ȣ��
            }
            else
            {
                rewardButton.interactable = false; // ���� ������ �Ϸ���� ���� ��� ��ư ��Ȱ��ȭ
                completeImage.SetActive(false); // �Ϸ� �̹��� ��Ȱ��ȭ
            }
        }
    }

    private void RewardAchievement(Achievement achievement, Button button, GameObject completeImage)
    {
        if (achievement.isCompleted) // ���� ������ �Ϸ�� ��쿡��
        {
            if (button.interactable) // ��ư�� Ȱ��ȭ�� ��쿡��
            {
                achievement.GiveReward(); // ���� ����
                button.interactable = false; // ��ư ��Ȱ��ȭ
                completeImage.SetActive(true); // �Ϸ� �̹��� Ȱ��ȭ
                // UpdateAchievementList(); // UI ���� ȣ���� ���⼭ ����
            }
        }
    }
}
