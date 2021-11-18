using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PhotonController : MonoBehaviourPunCallbacks
{



    /// <summary>
    /// This client's version number. Users are separated from each other by gameversion (which allows you to make breaking changes).
    /// </summary>
    string _gameVersion = "1";

    public PunLogLevel logLevel = PunLogLevel.Informational;

    public byte MaxPlayersPerRoom = 4;

    public Text Players;

    public Text Status;

    public Text btnTxt;

    public Button btn;



    void Awake()
    {

        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.LogLevel = logLevel;
    }


    void Start()
    {
        Connect();
    }


    private void Update()
    {
        if (!PhotonNetwork.InRoom)
        {
            Status.text = "Not Connected";
            btnTxt.text = "Connecting";
            Players.text = "Players in Room:" + PhotonNetwork.CountOfPlayers;
            PhotonNetwork.JoinOrCreateRoom("Test", new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
        }
        else
        {
            Status.text = "Connected";
            if(PhotonNetwork.CurrentRoom.Players.Count == 1)
            {
                btnTxt.text = "Waiting For Opponent";
            }
            else if(PhotonNetwork.CurrentRoom.Players.Count == 2)
            {
                btnTxt.text = "Start Mode";
                btn.interactable = true;
            }
            if (PhotonNetwork.IsMasterClient)
            {
                Players.text = "Players in Room: " + PhotonNetwork.CurrentRoom.Players.Count + "\n You are the Master client";
            }
            else
            {
                PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                Players.text = "Players in Room: " + PhotonNetwork.CurrentRoom.Players.Count + "\n you are not the master client";
            }
        }
    }

    public void StartGame()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LoadLevel("GameScene");
        }
    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = _gameVersion;
        }
    }

    public override void OnConnected()
    {
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN");
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("DemoAnimator/Launcher:OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
    }
}