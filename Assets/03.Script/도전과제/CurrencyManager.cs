using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;

    [SerializeField] TMP_Text currencyText;
    [SerializeField]  private int currency = 0;  // 저장된 재화

    private void Update()
    {
        currencyText.text = currency.ToString();
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);  // 중복 방지
    }

    // 재화 추가
    public void AddCurrency(int amount)
    {
        currency += amount;
        Debug.Log("재화를 추가했습니다: " + amount + ". 현재 재화: " + currency);
    }

    // 재화 차감
    public void SubtractCurrency(int amount)
    {
        if (currency >= amount)
        {
            currency -= amount;
            Debug.Log("재화를 사용했습니다: " + amount + ". 남은 재화: " + currency);
        }
        else
        {
            Debug.Log("재화가 부족합니다!");
        }
    }

    // 현재 재화 확인
    public int GetCurrency()
    {
        return currency;
    }
}
