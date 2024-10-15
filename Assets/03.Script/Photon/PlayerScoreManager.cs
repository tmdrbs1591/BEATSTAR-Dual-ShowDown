using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerScoreManager : MonoBehaviourPunCallbacks
{
    public static PlayerScoreManager instance { get; private set; }

    public List<PlayerScore> playerScores = new List<PlayerScore>();
    public List<ScoreListItem> playerScoreLists = new List<ScoreListItem>();

    public GameObject firstPlacePanel;
    public GameObject secondPlacePanel;
    public GameObject thirdPlacePanel;
    public GameObject fourthPlacePanel;


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
    private void Update()
    {
        DisplayScoreRankings();
    }
    // �� �����Ӹ��� ���� ���Ͽ� �ְ� ���� �÷��̾�� �հ��� Ȱ��ȭ
    public void WinnerLoserPanel()
    {
        // ���� ���� ���� ������ �ش� �÷��̾ ����
        List<PlayerScore> sortedScores = new List<PlayerScore>(playerScores);

        // ������ ������������ ����
        sortedScores.Sort((score1, score2) => score2.currentScore.CompareTo(score1.currentScore));

        // �г� ��Ȱ��ȭ
        firstPlacePanel.SetActive(false);
        secondPlacePanel.SetActive(false);
        thirdPlacePanel.SetActive(false);
        fourthPlacePanel.SetActive(false);

        // 1��, 2��, 3��, 4� �´� �г� Ȱ��ȭ (IsMine�� ���� ���� �÷��̾�Ը� ����)
        if (sortedScores.Count > 0 && sortedScores[0].photonView.IsMine)
        {
            firstPlacePanel.SetActive(true);  // 1�� �г�
            Debug.Log($"1��: {sortedScores[0].photonView.Owner.NickName} - ����: {sortedScores[0].currentScore}");
        }
        if (sortedScores.Count > 1 && sortedScores[1].photonView.IsMine)
        {
            secondPlacePanel.SetActive(true);  // 2�� �г�
            Debug.Log($"2��: {sortedScores[1].photonView.Owner.NickName} - ����: {sortedScores[1].currentScore}");
        }
        if (sortedScores.Count > 2 && sortedScores[2].photonView.IsMine)
        {
            thirdPlacePanel.SetActive(true);  // 3�� �г�
            Debug.Log($"3��: {sortedScores[2].photonView.Owner.NickName} - ����: {sortedScores[2].currentScore}");
        }
        if (sortedScores.Count > 3 && sortedScores[3].photonView.IsMine)
        {
            fourthPlacePanel.SetActive(true);  // 4�� �г�
            Debug.Log($"4��: {sortedScores[3].photonView.Owner.NickName} - ����: {sortedScores[3].currentScore}");
        }
    }
    public void DisplayScoreRankings()
    {
        // ScoreListItem ����Ʈ�� scoreText�� ���� ���� �������� �������� ����
        playerScoreLists.Sort((item1, item2) =>
        {
            // string�� int�� ��ȯ�Ͽ� ��
            int score1 = 0, score2 = 0;
            if (int.TryParse(item1.scoreText.text, out score1) && int.TryParse(item2.scoreText.text, out score2))
            {
                return score2.CompareTo(score1);  // �������� ����
            }
            return 0;  // ��ȯ ���� �� ���� ���� ����
        });

        // ���ĵ� ����Ʈ�� ������� ���� �� �̹��� ����
        for (int i = 0; i < playerScoreLists.Count; i++)
        {
            // ���� ���
            Debug.Log($"Rank {i + 1}: {playerScoreLists[i].scoreText.text} - Score: {playerScoreLists[i].scoreText.text}");

            // 1��, 2��, 3� �´� �̹��� Ȱ��ȭ
            if (i == 0)  // 1��
            {
                playerScoreLists[i].firstImage.SetActive(true);
                playerScoreLists[i].secondImage.SetActive(false);
                playerScoreLists[i].thirdImage.SetActive(false);
            }
            else if (i == 1)  // 2��
            {
                playerScoreLists[i].firstImage.SetActive(false);
                playerScoreLists[i].secondImage.SetActive(true);
                playerScoreLists[i].thirdImage.SetActive(false);
            }
            else if (i == 2)  // 3��
            {
                playerScoreLists[i].firstImage.SetActive(false);
                playerScoreLists[i].secondImage.SetActive(false);
                playerScoreLists[i].thirdImage.SetActive(true);
            }
            else  // 4�� �̻��� �̹��� ��Ȱ��ȭ
            {
                playerScoreLists[i].firstImage.SetActive(false);
                playerScoreLists[i].secondImage.SetActive(false);
                playerScoreLists[i].thirdImage.SetActive(false);
            }
        }
    }

}
