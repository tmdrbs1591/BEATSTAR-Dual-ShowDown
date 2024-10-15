using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    [SerializeField] private GameObject scoreListPrefab; // ���ھ� ����Ʈ ������
    private Transform scoreListContent; // ���ھ� ����Ʈ�� �� ������

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded; // �̺�Ʈ ��� ����
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("Scene loaded: " + scene.name);

        // NetworkScoreGroup �±׸� ���� ��ü�� ã�� scoreListContent ����
        GameObject scoreListGroup = GameObject.FindGameObjectWithTag("NetWorkScoreGroup");
        if (scoreListGroup != null)
        {
            scoreListContent = scoreListGroup.transform;
            InitializePlayerScores(); // ���� �ε�� �� �÷��̾� ���ھ� �ʱ�ȭ ȣ��
        }
        else
        {
            Debug.LogError("No GameObject with tag 'NetworkScoreGroup' found.");
        }
    }

    void InitializePlayerScores()
    {
        if (PhotonNetwork.InRoom && scoreListContent != null)
        {
            Player[] players = PhotonNetwork.PlayerList;

            // ������ ���ھ� ����Ʈ ������ ���� (������ �����Ͱ� �������� �� �ֱ� ������)
            foreach (Transform child in scoreListContent)
            {
                Destroy(child.gameObject);
            }

            // �� �÷��̾ ���� ���ھ� ����Ʈ ������ ����
            for (int i = 0; i < players.Length; i++)
            {
                GameObject playerScoreItem = Instantiate(scoreListPrefab, scoreListContent);
            }
        }
    }
}
