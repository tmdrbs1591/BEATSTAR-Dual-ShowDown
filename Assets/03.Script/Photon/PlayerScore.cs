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

        // ���� �߰�
        if (Input.GetKeyDown(KeyCode.H) && photonView.IsMine)
        {
            Debug.Log("���� �߰�");
            AddScore(1000);
        }
    }

    // ���� �߰� �Լ�
    [PunRPC] // PunRPC ��Ʈ����Ʈ�� ��Ʈ��ũ���� ȣ�� ������ �Լ��� ����
    void AddScoreRPC(float score)
    {
        currentScore += score;
    }

    public void AddScore(float score)
    {
        // ��Ʈ��ũ�� ���� ��� Ŭ���̾�Ʈ�� ���� ������Ʈ�� ����
        photonView.RPC("AddScoreRPC", RpcTarget.All, score);
    }

}
