using UnityEngine;
using UnityEngine.UI;

public class BtnSelect : MonoBehaviour
{
    public Button[] buttons; // 버튼 배열 선언

    public KeyCode key; // 첫 번째 입력 키
    public KeyCode key2;  // 두 번째 입력 키
    public KeyCode key3;  // 세 번째 입력 키
    public KeyCode key4;  // 네 번째 입력 키
    public string type; // 버튼 선택 타입

    public GameObject MenuBtn; // 메뉴 버튼
    public GameObject SettingBtn;  // 설정 버튼

    public ButtonManager buttonManager; // 버튼 매니저 

    private void OnEnable()
    {  // 현재 선택된 버튼을 해제
        DeselectAllButtons();
        // 타입에 따라 초기 버튼 선택
        if (type == "StageModeBtn")
            SelectFirstButton();
        if (type == "SettingBtn")
            MenuBtn.SetActive(false);

        if (type == "NextSettingBtn")
        {
            SettingBtn.SetActive(false);
            MenuBtn.SetActive(false);
        }
    }

    private void OnDisable() // 비활성화될 때 호출되는 메서드
    {

        // 초기 버튼 선택
        SelectFirstButton();

        // 타입에 따라 버튼 상태 변경
        if (type == "SettingBtn")
            MenuBtn.SetActive(true);
        if (type == "NextSettingBtn")
            SettingBtn.SetActive(true);
    }

    void Update() // 매 프레임마다 호출되는 업데이트 메서드
    {
        // 버튼 네비게이션을 막는 상태가 아니라면 입력 처리
        if (!buttonManager.isNavimpossible)
        {
            if (Input.GetKeyDown(key) || Input.GetKeyDown(key2) || Input.GetKeyDown(key3) || Input.GetKeyDown(key4)) // 설정된 입력 키를 통해 버튼 선택
            {
                if (!IsAnyButtonSelected()) // 아무 버튼도 선택되지 않았다면 첫 번째 버튼 선택
                {
                    SelectFirstButton();
                }
            }
        }
    }

    bool IsAnyButtonSelected() // 현재 선택된 버튼이 있는지 확인하는 메서드
    {
        foreach (Button button in buttons)
        {
            if (button != null && button.gameObject == UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject) // 버튼이 존재하고 현재 선택된 게임 오브젝트인지 확인
            {
                return true;
            }
        }
        return false;
    }

    void SelectFirstButton() // 첫 번째 버튼을 선택하는 메서드
    {
        if (buttons.Length > 0 && buttons[0] != null)
        {
            buttons[0].Select();
        }
    }

    void DeselectAllButtons() // 현재 선택된 버튼을 해제하는 메서드
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
    }
}
