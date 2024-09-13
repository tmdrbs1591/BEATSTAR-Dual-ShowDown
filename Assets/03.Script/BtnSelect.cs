using UnityEngine;
using UnityEngine.UI;

public class BtnSelect : MonoBehaviour
{
    public Button[] buttons; // ��ư �迭 ����

    public KeyCode key; // ù ��° �Է� Ű
    public KeyCode key2;  // �� ��° �Է� Ű
    public KeyCode key3;  // �� ��° �Է� Ű
    public KeyCode key4;  // �� ��° �Է� Ű
    public string type; // ��ư ���� Ÿ��

    public GameObject MenuBtn; // �޴� ��ư
    public GameObject SettingBtn;  // ���� ��ư

    public ButtonManager buttonManager; // ��ư �Ŵ��� 

    private void OnEnable()
    {  // ���� ���õ� ��ư�� ����
        DeselectAllButtons();
        // Ÿ�Կ� ���� �ʱ� ��ư ����
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

    private void OnDisable() // ��Ȱ��ȭ�� �� ȣ��Ǵ� �޼���
    {

        // �ʱ� ��ư ����
        SelectFirstButton();

        // Ÿ�Կ� ���� ��ư ���� ����
        if (type == "SettingBtn")
            MenuBtn.SetActive(true);
        if (type == "NextSettingBtn")
            SettingBtn.SetActive(true);
    }

    void Update() // �� �����Ӹ��� ȣ��Ǵ� ������Ʈ �޼���
    {
        // ��ư �׺���̼��� ���� ���°� �ƴ϶�� �Է� ó��
        if (!buttonManager.isNavimpossible)
        {
            if (Input.GetKeyDown(key) || Input.GetKeyDown(key2) || Input.GetKeyDown(key3) || Input.GetKeyDown(key4)) // ������ �Է� Ű�� ���� ��ư ����
            {
                if (!IsAnyButtonSelected()) // �ƹ� ��ư�� ���õ��� �ʾҴٸ� ù ��° ��ư ����
                {
                    SelectFirstButton();
                }
            }
        }
    }

    bool IsAnyButtonSelected() // ���� ���õ� ��ư�� �ִ��� Ȯ���ϴ� �޼���
    {
        foreach (Button button in buttons)
        {
            if (button != null && button.gameObject == UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject) // ��ư�� �����ϰ� ���� ���õ� ���� ������Ʈ���� Ȯ��
            {
                return true;
            }
        }
        return false;
    }

    void SelectFirstButton() // ù ��° ��ư�� �����ϴ� �޼���
    {
        if (buttons.Length > 0 && buttons[0] != null)
        {
            buttons[0].Select();
        }
    }

    void DeselectAllButtons() // ���� ���õ� ��ư�� �����ϴ� �޼���
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
    }
}
