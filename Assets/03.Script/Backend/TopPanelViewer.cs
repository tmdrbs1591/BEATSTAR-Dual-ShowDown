using UnityEngine;
using TMPro;

public class TopPanelViewer : MonoBehaviour
{
	[SerializeField]
	private	TextMeshProUGUI	textNickname;

	[SerializeField]
	GameObject NickPanel;

    [SerializeField]
    ButtonManager button;
	//UserInfo.Data.gamerId 
	public void UpdateNickname()
	{
		// �г����� ������ gamer_id�� ����ϰ�, �г����� ������ �г��� ���
		textNickname.text = UserInfo.Data.nickname == null ?
						" "	: UserInfo.Data.nickname;
		if (UserInfo.Data.nickname == null)
		{
            button.isNavimpossible = true;

            NickPanel.SetActive(true);
		}
		else
		{
            NickPanel.SetActive(false);
        }

	}
}

