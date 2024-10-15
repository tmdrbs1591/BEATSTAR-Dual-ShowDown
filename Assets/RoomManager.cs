using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    [SerializeField] private GameObject scoreListPrefab; // 스코어 리스트 프리팹
    private Transform scoreListContent; // 스코어 리스트가 들어갈 콘텐츠

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
        SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트 등록 해제
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("Scene loaded: " + scene.name);

        // NetworkScoreGroup 태그를 가진 객체를 찾아 scoreListContent 설정
        GameObject scoreListGroup = GameObject.FindGameObjectWithTag("NetWorkScoreGroup");
        if (scoreListGroup != null)
        {
            scoreListContent = scoreListGroup.transform;
            InitializePlayerScores(); // 씬이 로드될 때 플레이어 스코어 초기화 호출
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

            // 기존의 스코어 리스트 아이템 제거 (이전의 데이터가 남아있을 수 있기 때문에)
            foreach (Transform child in scoreListContent)
            {
                Destroy(child.gameObject);
            }

            // 각 플레이어에 대해 스코어 리스트 아이템 생성
            for (int i = 0; i < players.Length; i++)
            {
                GameObject playerScoreItem = Instantiate(scoreListPrefab, scoreListContent);
            }
        }
    }
}
