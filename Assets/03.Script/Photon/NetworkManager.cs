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

    private Dictionary<string, GameObject> playerObjects = new Dictionary<string, GameObject>(); // �÷��̾� ������Ʈ ���� ��ųʸ�

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
        // Awake ���Ŀ� �濡 ����ִ� ��� �÷��̾��� ���ھ� ����Ʈ�� �ʱ�ȭ�մϴ�.
        SceneManager.sceneLoaded += OnSceneLoaded; // �� �ε� �� ȣ��� �޼��� ���
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // �޸� ���� ������ ���� �޼��� ��� ����
    }

    private void InitializePlayerScores()
    {
        // �濡 ����ִ� ��� �÷��̾ ���� ���ھ� ����Ʈ �������� �����մϴ�.
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

    #region ��������
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

    #region ��
    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();

    public void LeaveRoom() => PhotonNetwork.LeaveRoom();

    public override void OnJoinedRoom()
    {
        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Count(); i++)
        {
            GameObject playerItem = Instantiate(playerListItemPrefab, playerLisContent);
            playerItem.GetComponent<PlayerListItem>().Setup(players[i]);
            playerObjects[players[i].NickName] = playerItem; // �÷��̾� ������Ʈ ����
        }

        for (int i = 0; i < players.Count(); i++)
        {
            GameObject playerhpItem = Instantiate(scoreListPrefab, scoreListContent);
            playerhpItem.GetComponent<PlayerScore>().Setup(players[i]);
        }
        RoomPanel.SetActive(true);

        // �ο� ���� 2���̸� �� ��ȯ
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
        string roomName = "Room" + Random.Range(0, 100); // ������ �̸����� ���� ����
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 2 }); // �ִ� �ο� 2������ ����
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
        playerObjects[newPlayer.NickName] = playerItem; // �÷��̾� ������Ʈ ����

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
            Debug.Log("ShowDownStage1 ���� �ε�Ǿ����ϴ�.");
    }
    #endregion
}
