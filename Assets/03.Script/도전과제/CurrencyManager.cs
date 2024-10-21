using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;

    [SerializeField] TMP_Text currencyText;
    [SerializeField]  private int currency = 0;  // ����� ��ȭ

    private void Update()
    {
        currencyText.text = currency.ToString();
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);  // �ߺ� ����
    }

    // ��ȭ �߰�
    public void AddCurrency(int amount)
    {
        currency += amount;
        Debug.Log("��ȭ�� �߰��߽��ϴ�: " + amount + ". ���� ��ȭ: " + currency);
    }

    // ��ȭ ����
    public void SubtractCurrency(int amount)
    {
        if (currency >= amount)
        {
            currency -= amount;
            Debug.Log("��ȭ�� ����߽��ϴ�: " + amount + ". ���� ��ȭ: " + currency);
        }
        else
        {
            Debug.Log("��ȭ�� �����մϴ�!");
        }
    }

    // ���� ��ȭ Ȯ��
    public int GetCurrency()
    {
        return currency;
    }
}
