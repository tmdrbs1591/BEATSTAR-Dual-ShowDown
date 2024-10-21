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
    public bool isRewarded; // ������ �޾Ҵ��� ����
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
        Debug.Log(name + " ���� ������ �Ϸ��߽��ϴ�!");
    }

    public void GiveReward()
    {
        CurrencyManager.instance.AddCurrency(rewardCurrency);
        Debug.Log("�������� " + rewardCurrency + " ��ȭ�� �����߽��ϴ�!");
    }
}
