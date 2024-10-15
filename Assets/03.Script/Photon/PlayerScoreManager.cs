using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerScoreManager : MonoBehaviourPunCallbacks
{
    public static PlayerScoreManager instance { get; private set; }

    public List<PlayerScore> playerScores = new List<PlayerScore>();

    public GameObject WinnerPanel;
    public GameObject LosePanel;

    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // 씬이 변경되어도 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);  // 이미 인스턴스가 있으면 자신을 파괴
        }
    }

    // 매 프레임마다 점수 비교하여 최고 점수 플레이어에게 왕관을 활성화
    public void WinnerLoserPanel()
    {
        // 현재 가장 높은 점수와 해당 플레이어를 추적
        PlayerScore highestScorePlayer = null;
        PlayerScore lowestScorePlayer = null;
        float highestScore = float.MinValue;
        float lowestScore = float.MaxValue;

        // 최고 점수와 최저 점수 찾기
        foreach (var playerScore in playerScores)
        {
            if (playerScore.currentScore > highestScore)
            {
                highestScore = playerScore.currentScore;
                highestScorePlayer = playerScore;
            }

            if (playerScore.currentScore < lowestScore)
            {
                lowestScore = playerScore.currentScore;
                lowestScorePlayer = playerScore;
            }
        }

        // 모든 패널 비활성화
        if (WinnerPanel != null)
        {
            WinnerPanel.SetActive(false);
        }

        if (LosePanel != null)
        {
            LosePanel.SetActive(false);
        }

        // 최고 점수 플레이어에게만 WinnerPanel 활성화 (IsMine을 통해 현재 플레이어에게만 적용)
        if (highestScorePlayer != null && highestScorePlayer.photonView.IsMine && WinnerPanel != null)
        {
            WinnerPanel.SetActive(true);
        }

        // 최저 점수 플레이어에게만 LosePanel 활성화 (IsMine을 통해 현재 플레이어에게만 적용)
        if (lowestScorePlayer != null && lowestScorePlayer.photonView.IsMine && LosePanel != null)
        {
            LosePanel.SetActive(true);
        }
    }

}
