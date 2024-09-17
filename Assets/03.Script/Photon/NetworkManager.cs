using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviourPunCallbacks, IPunObservable
{
    [Header("DisconnectPanel")]
    public TMP_InputField NickNameInput;

    [Header("LobbyPanel")]
    public GameObject LobbyPanel;
    public TMP_Text WelcomeText;

    [Header("RoomPanel")]
    public GameObject RoomPanel;

    [Header("ETC")]
    public TMP_Text StatusText;

    public string[] songPath;
    private string currentSongPath;

    [SerializeField] GameObject playerListItemPrefab;
    [SerializeField] Transform playerLisContent;

    [SerializeField] public Transform scoreListContent;
    [SerializeField] public GameObject scoreListPrefab;

    private Dictionary<string, GameObject> playerObjects = new Dictionary<string, GameObject>(); // 플레이어 오브젝트 관리 딕셔너리

    private PhotonView photonView;

    void Awake()
    {
        PhotonNetwork.Disconnect();

        Screen.SetResolution(960, 540, false);
        photonView = GetComponent<PhotonView>();

        PhotonNetwork.LocalPlayer.NickName = UserInfo.Data.nickname;
        Connect();
    }

    void Start()
    {
        // Awake 이후에 방에 들어있는 모든 플레이어의 스코어 리스트를 초기화합니다.
        SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로딩 후 호출될 메서드 등록
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 메모리 누수 방지를 위해 메서드 등록 해제
    }

    private void InitializePlayerScores()
    {
        // 방에 들어있는 모든 플레이어에 대해 스코어 리스트 아이템을 생성합니다.
        if (PhotonNetwork.InRoom)
        {
            Player[] players = PhotonNetwork.PlayerList;
            foreach (var player in players)
            {
                GameObject playerScoreItem = Instantiate(scoreListPrefab, scoreListContent);
                playerScoreItem.GetComponent<PlayerScore>().Setup(player);
            }
        }
    }

    void Update()
    {
        if (StatusText != null)
            StatusText.text = PhotonNetwork.NetworkClientState.ToString();
    }

    #region 서버연결
    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster() => PhotonNetwork.JoinLobby();

    public override void OnJoinedLobby()
    {
        LobbyPanel.SetActive(true);
        RoomPanel.SetActive(false);
        WelcomeText.text = "=" +  PhotonNetwork.LocalPlayer.NickName + "=" ;
    }

    public void Disconnect() => PhotonNetwork.Disconnect();

    public override void OnDisconnected(DisconnectCause cause)
    {
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(false);
    }
    #endregion

    #region 방
    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();

    public void LeaveRoom() => PhotonNetwork.LeaveRoom();

    public override void OnJoinedRoom()
    {
        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Count(); i++)
        {
            GameObject playerItem = Instantiate(playerListItemPrefab, playerLisContent);
            playerItem.GetComponent<PlayerListItem>().Setup(players[i]);
            playerObjects[players[i].NickName] = playerItem; // 플레이어 오브젝트 저장
        }

        for (int i = 0; i < players.Count(); i++)
        {
            GameObject playerhpItem = Instantiate(scoreListPrefab, scoreListContent);
            playerhpItem.GetComponent<PlayerScore>().Setup(players[i]);
        }
        RoomPanel.SetActive(true);

        // 인원 수가 2명이면 씬 전환
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            photonView.RPC("SetSongPath", RpcTarget.All, songPath[0]);
            PhotonNetwork.LoadLevel("ShowDownStage1");
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    public void CreateRoom()
    {
        string roomName = "Room" + Random.Range(0, 100); // 임의의 이름으로 방을 생성
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 2 }); // 최대 인원 2명으로 설정
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Room creation failed: " + message);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        GameObject playerHpItem = Instantiate(scoreListPrefab, scoreListContent);
        playerHpItem.GetComponent<PlayerScore>().Setup(newPlayer);

        GameObject playerItem = Instantiate(playerListItemPrefab, playerLisContent);
        playerItem.GetComponent<PlayerListItem>().Setup(newPlayer);
        playerObjects[newPlayer.NickName] = playerItem; // 플레이어 오브젝트 저장

        if (songPath != null)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                photonView.RPC("SetSongPath", RpcTarget.All, songPath[0]);
                PhotonNetwork.LoadLevel("ShowDownStage1");
            }
        }
    }

    [PunRPC]
    void SetSongPath(string path)
    {
        currentSongPath = path;
        DataManager.instance.songPath = path;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentSongPath);
        }
        else
        {
            currentSongPath = (string)stream.ReceiveNext();
            DataManager.instance.songPath = currentSongPath;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
            Debug.Log("ShowDownStage1 씬이 로드되었습니다.");
    }
    #endregion
}
