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
        // �̱��� �ν��Ͻ� ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // ���� ����Ǿ �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject);  // �̹� �ν��Ͻ��� ������ �ڽ��� �ı�
        }
    }

    // �� �����Ӹ��� ���� ���Ͽ� �ְ� ���� �÷��̾�� �հ��� Ȱ��ȭ
    public void WinnerLoserPanel()
    {
        // ���� ���� ���� ������ �ش� �÷��̾ ����
        PlayerScore highestScorePlayer = null;
        PlayerScore lowestScorePlayer = null;
        float highestScore = float.MinValue;
        float lowestScore = float.MaxValue;

        // �ְ� ������ ���� ���� ã��
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

        // ��� �г� ��Ȱ��ȭ
        if (WinnerPanel != null)
        {
            WinnerPanel.SetActive(false);
        }

        if (LosePanel != null)
        {
            LosePanel.SetActive(false);
        }

        // �ְ� ���� �÷��̾�Ը� WinnerPanel Ȱ��ȭ (IsMine�� ���� ���� �÷��̾�Ը� ����)
        if (highestScorePlayer != null && highestScorePlayer.photonView.IsMine && WinnerPanel != null)
        {
            WinnerPanel.SetActive(true);
        }

        // ���� ���� �÷��̾�Ը� LosePanel Ȱ��ȭ (IsMine�� ���� ���� �÷��̾�Ը� ����)
        if (lowestScorePlayer != null && lowestScorePlayer.photonView.IsMine && LosePanel != null)
        {
            LosePanel.SetActive(true);
        }
    }

}
