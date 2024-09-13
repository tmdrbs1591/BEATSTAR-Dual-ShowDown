using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;  // �����̴��� ����ϱ� ���� ���ӽ����̽�

public class PlayerScore : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text text;        // �÷��̾� �̸��� ǥ���� �ؽ�Ʈ
    [SerializeField] TMP_Text socreText;   // ������ ǥ���� �ؽ�Ʈ
    Player player;

    public void Setup(Player _player)
    {
        player = _player;
        text.text = _player.NickName;
        UpdateScore(); // �ʱ� HP �� ���� ������Ʈ
    }

    // HP �ٿ� ������ ������Ʈ�ϴ� �޼���
    void UpdateScore()
    {
        ScoreManager scoreManager = GetPlayerStatsByNickName(text.text);
        if (scoreManager != null)
        {
            socreText.text = "���� ���ھ�" + scoreManager.currentScore.ToString();
        }
    }

    ScoreManager GetPlayerStatsByNickName(string nickName)
    {
        foreach (GameObject playerObject in GameObject.FindGameObjectsWithTag("ScoreManager"))
        {
            PhotonView photonView = playerObject.GetComponent<PhotonView>();
            if (photonView != null && photonView.Owner.NickName == nickName)
            {
                return playerObject.GetComponent<ScoreManager>();
            }
        }
        return null; // �÷��̾ ã�� ���� ���
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        UpdateScore();  
    }
}