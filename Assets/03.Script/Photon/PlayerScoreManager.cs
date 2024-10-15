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
    private void Update()
    {
        DisplayScoreRankings();
    }
    // 매 프레임마다 점수 비교하여 최고 점수 플레이어에게 왕관을 활성화
    public void WinnerLoserPanel()
    {
        // 현재 가장 높은 점수와 해당 플레이어를 추적
        List<PlayerScore> sortedScores = new List<PlayerScore>(playerScores);

        // 점수를 내림차순으로 정렬
        sortedScores.Sort((score1, score2) => score2.currentScore.CompareTo(score1.currentScore));

        // 패널 비활성화
        firstPlacePanel.SetActive(false);
        secondPlacePanel.SetActive(false);
        thirdPlacePanel.SetActive(false);
        fourthPlacePanel.SetActive(false);

        // 1등, 2등, 3등, 4등에 맞는 패널 활성화 (IsMine을 통해 현재 플레이어에게만 적용)
        if (sortedScores.Count > 0 && sortedScores[0].photonView.IsMine)
        {
            firstPlacePanel.SetActive(true);  // 1등 패널
            Debug.Log($"1등: {sortedScores[0].photonView.Owner.NickName} - 점수: {sortedScores[0].currentScore}");
        }
        if (sortedScores.Count > 1 && sortedScores[1].photonView.IsMine)
        {
            secondPlacePanel.SetActive(true);  // 2등 패널
            Debug.Log($"2등: {sortedScores[1].photonView.Owner.NickName} - 점수: {sortedScores[1].currentScore}");
        }
        if (sortedScores.Count > 2 && sortedScores[2].photonView.IsMine)
        {
            thirdPlacePanel.SetActive(true);  // 3등 패널
            Debug.Log($"3등: {sortedScores[2].photonView.Owner.NickName} - 점수: {sortedScores[2].currentScore}");
        }
        if (sortedScores.Count > 3 && sortedScores[3].photonView.IsMine)
        {
            fourthPlacePanel.SetActive(true);  // 4등 패널
            Debug.Log($"4등: {sortedScores[3].photonView.Owner.NickName} - 점수: {sortedScores[3].currentScore}");
        }
    }
    public void DisplayScoreRankings()
    {
        // ScoreListItem 리스트를 scoreText의 숫자 값을 기준으로 내림차순 정렬
        playerScoreLists.Sort((item1, item2) =>
        {
            // string을 int로 변환하여 비교
            int score1 = 0, score2 = 0;
            if (int.TryParse(item1.scoreText.text, out score1) && int.TryParse(item2.scoreText.text, out score2))
            {
                return score2.CompareTo(score1);  // 내림차순 정렬
            }
            return 0;  // 변환 실패 시 순서 변경 없음
        });

        // 정렬된 리스트를 기반으로 순위 및 이미지 설정
        for (int i = 0; i < playerScoreLists.Count; i++)
        {
            // 순위 출력
            Debug.Log($"Rank {i + 1}: {playerScoreLists[i].scoreText.text} - Score: {playerScoreLists[i].scoreText.text}");

            // 1등, 2등, 3등에 맞는 이미지 활성화
            if (i == 0)  // 1등
            {
                playerScoreLists[i].firstImage.SetActive(true);
                playerScoreLists[i].secondImage.SetActive(false);
                playerScoreLists[i].thirdImage.SetActive(false);
            }
            else if (i == 1)  // 2등
            {
                playerScoreLists[i].firstImage.SetActive(false);
                playerScoreLists[i].secondImage.SetActive(true);
                playerScoreLists[i].thirdImage.SetActive(false);
            }
            else if (i == 2)  // 3등
            {
                playerScoreLists[i].firstImage.SetActive(false);
                playerScoreLists[i].secondImage.SetActive(false);
                playerScoreLists[i].thirdImage.SetActive(true);
            }
            else  // 4등 이상은 이미지 비활성화
            {
                playerScoreLists[i].firstImage.SetActive(false);
                playerScoreLists[i].secondImage.SetActive(false);
                playerScoreLists[i].thirdImage.SetActive(false);
            }
        }
    }

}
