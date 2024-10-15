using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;

public class PlayerScore : MonoBehaviourPunCallbacks
{
    public float currentScore;

    [SerializeField]  TMP_Text scoreText;

    private void Start()
    {
        PlayerScoreManager.instance.playerScores.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = currentScore.ToString();

        // 점수 추가
        if (Input.GetKeyDown(KeyCode.H) && photonView.IsMine)
        {
            Debug.Log("점수 추가");
            AddScore(1000);
        }
    }

    // 점수 추가 함수
    [PunRPC] // PunRPC 어트리뷰트로 네트워크에서 호출 가능한 함수로 만듬
    void AddScoreRPC(float score)
    {
        currentScore += score;
    }

    public void AddScore(float score)
    {
        // 네트워크를 통해 모든 클라이언트에 점수 업데이트를 전달
        photonView.RPC("AddScoreRPC", RpcTarget.All, score);
    }

}
