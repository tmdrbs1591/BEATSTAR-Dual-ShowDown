using System.Collections;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public static CountDown instance;
    [SerializeField] TMP_Text countDownText;

    IEnumerator StartCountDown()
    {
        int count = 3;
        while (count > 0)
        {
            countDownText.text = count.ToString();
            yield return new WaitForSecondsRealtime(0.4f); // 실제 시간 기준으로 0.4초 대기
            count--;
        }

        countDownText.text = "Go!";
        yield return new WaitForSecondsRealtime(0.41f); // 실제 시간 기준으로 0.4초 대기
        countDownText.text = "";
    }

    public void CountDowns()
    {
        StartCoroutine(StartCountDown());
    }
    private void Start()
    {
    }
    private void OnEnable()
    {

        instance = this;
        StartCoroutine(StartCountDown());
    }
}
