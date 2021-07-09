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

    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
    /// </summary>
    void Awake()
    {


        // #Critical
        // we don't join the lobby. There is no need to join a lobby to get the list of rooms.


        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.LogLevel = logLevel;
    }


    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during initialization phase.
    /// </summary>
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
                btnTxt.text = "Start";
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

    /// <summary>
    /// Start the connection process.
    /// - If already connected, we attempt joining a random room
    /// - if not yet connected, Connect this application instance to Photon Cloud Network
    /// </summary>
    public void Connect()
    {
        // we check if we are connected or not, we join if we are, else we initiate the connection to the server.
        if (PhotonNetwork.IsConnected)
        {
            // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnPhotonRandomJoinFailed() and we'll create one.
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            // #Critical, we must first and foremost connect to Photon Online Server.
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
        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
    }
}