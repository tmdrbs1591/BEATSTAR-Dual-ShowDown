using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("DisconnectPanel")]
    public TMP_InputField NickNameInput;
    public GameObject DisconnectPanel;

    [Header("LobbyPanel")]
    public GameObject LobbyPanel;
    public TMP_InputField RoomInput;
    public TMP_Text WelcomeText;
    public TMP_Text LobbyInfoText;
    public Button[] CellBtn;
    public Button PreviousBtn;
    public Button NextBtn;

    [Header("RoomPanel")]
    public GameObject RoomPanel;
    public TMP_Text ListText;
    public TMP_Text RoomInfoText;
    public TMP_Text[] ChatText;
    public TMP_InputField ChatInput;

    [Header("ETC")]
    public TMP_Text StatusText;
    public PhotonView PV;

    [Header("Room Code")]
    public TMP_InputField RoomCodeInput; // �� �ڵ带 �Է¹��� �ʵ� �߰�
    public TMP_Text RoomCodeText; // ������ �� �ڵ带 ǥ���� �ؽ�Ʈ

    List<RoomInfo> myList = new List<RoomInfo>();
    int currentPage = 1, maxPage, multiple;

    [Header("LobbyPanel")]
    public GameObject StartButton;

    [SerializeField] Transform playerScoreLisContent;
    [SerializeField] Transform playerLisContent;
    [SerializeField] GameObject playerListItemPrefab;
    [SerializeField] GameObject playerScoreListItemPrefab;
    private Dictionary<string, GameObject> playerObjects = new Dictionary<string, GameObject>(); // �÷��̾� ������Ʈ ���� ��ųʸ�

    string roomCode; // �� �ڵ带 ������ ����

    [Header("SongPath")]
    public string[] songPath;
    private string currentSongPath;
    void Awake()
    {
        Screen.SetResolution(960, 540, false);
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            StartButton.SetActive(false);
        }
    }

    void Update()
    {
        StatusText.text = PhotonNetwork.NetworkClientState.ToString();
        LobbyInfoText.text = (PhotonNetwork.CountOfPlayers - PhotonNetwork.CountOfPlayersInRooms) + "�κ� / " + PhotonNetwork.CountOfPlayers + "����";

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Send();
        }
    }

    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (songPath != null)
            {
                    photonView.RPC("SetSongPath", RpcTarget.All, songPath[0]);
                    PhotonNetwork.LoadLevel("MultiStage1");
            }
        }
    }

    [PunRPC]
    void SetSongPath(string path)
    {
        currentSongPath = path;
        DataManager.instance.songPath = path;
    }
    #region �渮��Ʈ ����
    public void MyListClick(int num)
    {
        if (num == -2) --currentPage;
        else if (num == -1) ++currentPage;
        else PhotonNetwork.JoinRoom(myList[multiple + num].Name);
        MyListRenewal();
    }

    void MyListRenewal()
    {
        maxPage = (myList.Count % CellBtn.Length == 0) ? myList.Count / CellBtn.Length : myList.Count / CellBtn.Length + 1;

        PreviousBtn.interactable = (currentPage <= 1) ? false : true;
        NextBtn.interactable = (currentPage >= maxPage) ? false : true;

        multiple = (currentPage - 1) * CellBtn.Length;
        for (int i = 0; i < CellBtn.Length; i++)
        {
            CellBtn[i].interactable = (multiple + i < myList.Count) ? true : false;
            CellBtn[i].transform.GetChild(0).GetComponent<TMP_Text>().text = (multiple + i < myList.Count) ? myList[multiple + i].Name : "";
            CellBtn[i].transform.GetChild(1).GetComponent<TMP_Text>().text = (multiple + i < myList.Count) ? myList[multiple + i].PlayerCount + "/" + myList[multiple + i].MaxPlayers : "";
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int roomCount = roomList.Count;
        for (int i = 0; i < roomCount; i++)
        {
            if (!roomList[i].RemovedFromList)
            {
                if (!myList.Contains(roomList[i])) myList.Add(roomList[i]);
                else myList[myList.IndexOf(roomList[i])] = roomList[i];
            }
            else if (myList.IndexOf(roomList[i]) != -1) myList.RemoveAt(myList.IndexOf(roomList[i]));
        }
        MyListRenewal();
    }
    #endregion

    #region ��������

    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster() => PhotonNetwork.JoinLobby();

    public override void OnJoinedLobby()
    {
        LobbyPanel.SetActive(true);
        RoomPanel.SetActive(false);
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        WelcomeText.text = PhotonNetwork.LocalPlayer.NickName + "�� ȯ���մϴ�";
        myList.Clear();
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        LobbyPanel.SetActive(true);
        RoomPanel.SetActive(false);
        DisconnectPanel.SetActive(true);
    }
    #endregion

    #region �� ���� �� ����

    // �� �̸��� �� �ڵ带 �����ϴ� ������� ����
    public void CreateRoom()
    {
        roomCode = Random.Range(1000, 9999).ToString(); // 4�ڸ� �� �ڵ� ����
        string roomName = (RoomInput.text == "" ? "Room" + Random.Range(0, 100) : RoomInput.text) + "_" + roomCode; // �� �̸��� �� �ڵ� �߰�
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(roomName, roomOptions);
        RoomCodeText.text = "Room Code: " + roomCode; // UI�� �� �ڵ� ǥ��
    }

    // ����ڰ� �� �ڵ� �Է� �� ����
    public void JoinRoomWithCode()
    {
        string inputCode = RoomCodeInput.text;

        // �� ��Ͽ��� �� �ڵ�� ���͸��Ͽ� ����
        foreach (RoomInfo roomInfo in myList)
        {
            if (roomInfo.Name.EndsWith("_" + inputCode))
            {
                PhotonNetwork.JoinRoom(roomInfo.Name);
                return;
            }
        }

        // ���� ã�� ���ϸ� ���� �޽����� ��� �� ����
        Debug.LogError("Room with code " + inputCode + " not found.");
    }

    // ���� �� ���� ���
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        // ������ ������ ����Ʈ �����۵��� ��� ����
        foreach (Transform child in playerLisContent)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in playerScoreLisContent)
        {
            Destroy(child.gameObject);
        }

        // �÷��̾� ����Ʈ�� ���� ����
        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Count(); i++)
        {
            GameObject playerItem = Instantiate(playerListItemPrefab, playerLisContent);
            playerItem.GetComponent<PlayerListItem>().Setup(players[i]);
            playerObjects[players[i].NickName] = playerItem; // �÷��̾� ������Ʈ ����
        }

        for (int i = 0; i < players.Count(); i++)
        {
            GameObject playerScoreItem = Instantiate(playerScoreListItemPrefab, playerScoreLisContent);
            playerScoreItem.GetComponent<ScoreListItem>().Setup(players[i]);
            playerObjects[players[i].NickName] = playerScoreItem; // �÷��̾� ������Ʈ ����
        }

        RoomPanel.SetActive(true);
        RoomRenewal();
        ChatInput.text = "";
        for (int i = 0; i < ChatText.Length; i++) ChatText[i].text = "";

        StartButton.SetActive(PhotonNetwork.IsMasterClient);

        // �� �ڵ� ǥ��
        string roomName = PhotonNetwork.CurrentRoom.Name;
        string roomCode = roomName.Split('_')[^1];  // �� �̸����� ������ �κ��� �� �ڵ�
        RoomCodeText.text = "Room Code: " + roomCode;

        LobbyPanel.SetActive(false);
        DisconnectPanel.SetActive(false);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        RoomInput.text = "";
        CreateRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomInput.text = "";
        CreateRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + newPlayer.NickName + "���� �����ϼ̽��ϴ�</color>");

        GameObject playerItem = Instantiate(playerListItemPrefab, playerLisContent);
        playerItem.GetComponent<PlayerListItem>().Setup(newPlayer);
        playerObjects[newPlayer.NickName] = playerItem; // �÷��̾� ������Ʈ ����

        GameObject playerScoreListItem = Instantiate(playerScoreListItemPrefab, playerScoreLisContent);
        playerScoreListItem.GetComponent<ScoreListItem>().Setup(newPlayer);
        playerObjects[newPlayer.NickName] = playerItem; // �÷��̾� ������Ʈ ����



    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + otherPlayer.NickName + "���� �����ϼ̽��ϴ�</color>");

        if (PhotonNetwork.IsMasterClient)
        {
            StartButton.SetActive(true);
        }
    }

    void RoomRenewal()
    {
        ListText.text = "";
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            ListText.text += PhotonNetwork.PlayerList[i].NickName + ((i + 1 == PhotonNetwork.PlayerList.Length) ? "" : ", ");
        RoomInfoText.text = PhotonNetwork.CurrentRoom.Name + " / " + PhotonNetwork.CurrentRoom.PlayerCount + "�� / " + PhotonNetwork.CurrentRoom.MaxPlayers + "�ִ�";
    }
    #endregion

    #region ä��
    public void Send()
    {
        PV.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName + " : " + ChatInput.text);
        ChatInput.text = "";
    }

    [PunRPC]
    void ChatRPC(string msg)
    {
        bool isInput = false;
        for (int i = 0; i < ChatText.Length; i++)
            if (ChatText[i].text == "")
            {
                isInput = true;
                ChatText[i].text = msg;
                break;
            }
        if (!isInput)
        {
            for (int i = 1; i < ChatText.Length; i++) ChatText[i - 1].text = ChatText[i].text;
            ChatText[ChatText.Length - 1].text = msg;
        }
    }
    #endregion
}
