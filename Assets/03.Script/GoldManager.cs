using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance; // 골드 매니저의 인스턴스를 정적으로 저장하는 변수

    public int allGold; // 전체 골드 금액

    [SerializeField] private int branch; // 분기 변수
    [SerializeField] private string tier; // 티어 변수
    [SerializeField] private DataBase database; // 데이터베이스 변수

    [SerializeField] TMP_Text goldText; // UI에 골드 금액을 표시할 TMP_Text

    private void Awake()
    {
        instance = this; // 인스턴스 초기화
        DontDestroyOnLoad(gameObject); // 씬 전환 시 삭제되지 않도록 설정
    }

    private void Update()
    {
        goldText.text = database.Entities[6].gold.ToString(); // UI에 데이터베이스에서 가져온 골드 금액 표시

        // B 키를 누르면 S 티어의 골드를 클리어
        if (Input.GetKeyDown(KeyCode.B))
        {
            CrearGold("S");
        }

        // P 키를 누르면 데이터베이스의 6번째 엔티티의 골드를 초기화
        if (Input.GetKeyDown(KeyCode.P))
        {
            database.Entities[6].gold = 0;
        }
    }

    // 지정된 티어의 골드를 클리어하는 메서드
    public void CrearGold(string clearTier)
    {
        for (int i = 0; i < database.Entities.Count; ++i)
        {
            // 데이터베이스에서 해당 티어를 찾아서 골드를 추가
            if (database.Entities[i].tier == clearTier)
            {
                database.Entities[6].gold += database.Entities[i].gold; // 데이터베이스의 6번째 엔티티의 골드 추가
                Debug.Log(database.Entities[6].gold); // 디버그 로그로 현재 골드 출력
            }
        }
    }
}
