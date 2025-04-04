using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;  // 슬라이더를 사용하기 위한 네임스페이스

public class ScoreListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text nickNameText;        // 플레이어 이름을 표시할 텍스트
    [SerializeField] public TMP_Text scoreText;   // 레벨을 표시할 텍스트
    Player player;

    [SerializeField] public GameObject firstImage;        // 1등 이미지
    [SerializeField] public GameObject secondImage;       // 2등 이미지
    [SerializeField] public GameObject thirdImage;        // 3등 이미지
    [SerializeField] public GameObject fourthImage;       // 4등 이미지
    private void Start()
    {
        PlayerScoreManager.instance.playerScoreLists.Add(this);
    }
    public void Setup(Player _player)
    {
        player = _player;
        nickNameText.text = _player.NickName;
        UpdateScore(); // 초기 HP 및 레벨 업데이트

    }

    // HP 바와 레벨을 업데이트하는 메서드
    void UpdateScore()
    {
        PlayerScore playerScore = GetPlayerStatsByNickName(nickNameText.text);
        if (playerScore != null)
        {
            scoreText.text = playerScore.currentScore.ToString();
        }
    }

    // 닉네임으로 PlayerStats를 찾는 메서드
    PlayerScore GetPlayerStatsByNickName(string nickName)
    {
        foreach (GameObject playerObject in GameObject.FindGameObjectsWithTag("PlayerScore"))
        {
            PhotonView photonView = playerObject.GetComponent<PhotonView>();
            if (photonView != null && photonView.Owner.NickName == nickName)
            {
                return playerObject.GetComponent<PlayerScore>();
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
        UpdateScore();  // 매 프레임 HP와 레벨을 업데이트 (최신 상태 반영)
    }
}
