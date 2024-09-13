using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using BackEnd;
using System.Collections;

public class Nickname : LoginBase
{
	[System.Serializable]
	public class NicknameEvent : UnityEngine.Events.UnityEvent { }
	public NicknameEvent		onNicknameEvent = new NicknameEvent();

	[SerializeField]
	private	Image				imageNickname;		// �г��� �ʵ� ���� ����
	[SerializeField]
	private	TMP_InputField		inputFieldNickname;	// �г��� �ʵ� �ؽ�Ʈ ���� ����

	[SerializeField]
	private	Button				btnUpdateNickname;  // "�г��� ����" ��ư (��ȣ�ۿ� ����/�Ұ���)
	[SerializeField]
	GameObject NikPanel;

	public ButtonManager buttonManager;




    private void OnEnable()
	{
        // �г��� ���濡 ������ ���� �޽����� ����� ���¿���
        // �г��� ���� �˾��� �ݾҴٰ� �� �� �ֱ� ������ ���¸� �ʱ�ȭ
        ResetUI(imageNickname);
		SetMessage("�г����� �����ּ���!");
	}

	public void OnClickUpdateNickname()
	{
		// �Ű������� �Է��� InputField UI�� ����� Message ���� �ʱ�ȭ
		ResetUI(imageNickname);

		// �ʵ� ���� ����ִ��� üũ
		if ( IsFieldDataEmpty(imageNickname, inputFieldNickname.text, "Nickname") )	return;

		// "�г��� ����" ��ư�� ��ȣ�ۿ� ��Ȱ��ȭ
		btnUpdateNickname.interactable = false;
		SetMessage("�г��� �������Դϴ�..");

		// �ڳ� ���� �г��� ���� �õ�
		UpdateNickname();
	}
    public void StartOnClickUpdateNickname()// Ʃ�丮���� ���� ����
    {
        // �Ű������� �Է��� InputField UI�� ����� Message ���� �ʱ�ȭ
        ResetUI(imageNickname);

        // �ʵ� ���� ����ִ��� üũ
        if (IsFieldDataEmpty(imageNickname, inputFieldNickname.text, "Nickname")) return;

        // "�г��� ����" ��ư�� ��ȣ�ۿ� ��Ȱ��ȭ
        btnUpdateNickname.interactable = false;
        SetMessage("�г��� �������Դϴ�..");

        // �ڳ� ���� �г��� ���� �õ� �ϰ� Ʃ�丮�� �̵�
        StartUpdateNickname();
    }
    IEnumerator PanelOff()
	{

		yield return new WaitForSeconds(1.5f);
        buttonManager.isNavimpossible = false;
        NikPanel.SetActive(false);

    }
	public void NavOn()
	{
       buttonManager.isNavimpossible = false;
    }
    private void StartUpdateNickname()
    {
        // �г��� ����
        Backend.BMember.UpdateNickname(inputFieldNickname.text, callback =>
        {
            // "�г��� ����" ��ư�� ��ȣ�ۿ� Ȱ��ȭ
            btnUpdateNickname.interactable = true;

            // �г��� ���� ����
            if (callback.IsSuccess())
            {
                SetMessage($"{inputFieldNickname.text}(��)�� �г����� ����Ǿ����ϴ�.");

                StartCoroutine(PanelOff());
                buttonManager.StageScenceLoad("Tutorial");
                // �г��� ���濡 �������� �� onNicknameEvent�� ��ϵǾ� �ִ� �̺�Ʈ �޼ҵ� ȣ��
                onNicknameEvent?.Invoke();
            }
            // �г��� ���� ����
            else
            {
                string message = string.Empty;

                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 400:   // �� �г��� Ȥ�� string.Empty, 20�� �̻��� �г���, �г��� ��/�ڿ� ������ �ִ� ���
                        message = "�г����� ����ְų� | 20�� �̻� �̰ų� | ��/�ڿ� ������ �ֽ��ϴ�.";
                        break;
                    case 409:   // �̹� �ߺ��� �г����� �ִ� ���
                        message = "�̹� �����ϴ� �г����Դϴ�.";
                        break;
                    default:
                        message = callback.GetMessage();
                        break;
                }

                GudieForIncorrectlyEnteredData(imageNickname, message);
            }
        });
    }
    private void UpdateNickname()
	{
		// �г��� ����
		Backend.BMember.UpdateNickname(inputFieldNickname.text, callback =>
		{
			// "�г��� ����" ��ư�� ��ȣ�ۿ� Ȱ��ȭ
			btnUpdateNickname.interactable = true;

			// �г��� ���� ����
			if ( callback.IsSuccess() )
			{
				SetMessage($"{inputFieldNickname.text}(��)�� �г����� ����Ǿ����ϴ�.");

				StartCoroutine(PanelOff());
                // �г��� ���濡 �������� �� onNicknameEvent�� ��ϵǾ� �ִ� �̺�Ʈ �޼ҵ� ȣ��
                onNicknameEvent?.Invoke();
			}
			// �г��� ���� ����
			else
			{
				string message = string.Empty;

				switch ( int.Parse(callback.GetStatusCode()) )
				{
					case 400:	// �� �г��� Ȥ�� string.Empty, 20�� �̻��� �г���, �г��� ��/�ڿ� ������ �ִ� ���
						message = "�г����� ����ְų� | 20�� �̻� �̰ų� | ��/�ڿ� ������ �ֽ��ϴ�.";
						break;
					case 409:	// �̹� �ߺ��� �г����� �ִ� ���
						message = "�̹� �����ϴ� �г����Դϴ�.";
						break;
					default:
						message = callback.GetMessage();
						break;
				}

                GudieForIncorrectlyEnteredData(imageNickname, message);
			}
		});
	}
}

