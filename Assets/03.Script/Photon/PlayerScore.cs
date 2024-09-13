using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;  // 슬라이더를 사용하기 위한 네임스페이스

public class PlayerScore : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text text;        // 플레이어 이름을 표시할 텍스트
    [SerializeField] TMP_Text socreText;   // 점수를 표시할 텍스트
    Player player;

    public void Setup(Player _player)
    {
        player = _player;
        text.text = _player.NickName;
        UpdateScore(); // 초기 HP 및 레벨 업데이트
    }

    // HP 바와 레벨을 업데이트하는 메서드
    void UpdateScore()
    {
        ScoreManager scoreManager = GetPlayerStatsByNickName(text.text);
        if (scoreManager != null)
        {
            socreText.text = "현재 스코어" + scoreManager.currentScore.ToString();
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
        return null; // 플레이어를 찾지 못한 경우
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