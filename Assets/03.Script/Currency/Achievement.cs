using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievement
{
    public string name;
    public string description;
    public int target;
    public int currentValue;
    public bool isCompleted;
    public bool isRealCompleted;
    public int rewardCurrency;
    public bool isRewarded; // 보상을 받았는지 여부
    public void UpdateProgress(int amount)
    {
        if (!isCompleted)
        {
            currentValue += amount;
            if (currentValue >= target)
            {
                currentValue = target;
                CompleteAchievement();
            }
        }
    }


    private void CompleteAchievement()
    {
        isCompleted = true;
        Debug.Log(name + " 도전 과제를 완료했습니다!");
    }

    public void GiveReward()
    {
        CurrencyManager.instance.AddCurrency(rewardCurrency);
        Debug.Log("보상으로 " + rewardCurrency + " 재화를 지급했습니다!");
    }
}
