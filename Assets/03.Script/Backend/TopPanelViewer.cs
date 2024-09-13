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
		// 닉네임이 없으면 gamer_id를 출력하고, 닉네임이 있으면 닉네임 출력
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

