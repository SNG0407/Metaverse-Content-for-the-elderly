using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
// using Unity.XR.Interaction.Toolkit;
//using Unity.XR.CoreUtils;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;



[System.Serializable]
public class DefaultRoom
{
    public string Name;
    public string SceneName;
    public int maxPlayer;
}

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Instance;
    public List<DefaultRoom> defaultRooms;
    public string PlayerName;
    public int PlayerNum = 0;
    public Text ServerStatus;
    //public GameObject RoomUI;

    private GameObject spawnedPlayerPrefab;

      private void Awake()
    {
        var objs = FindObjectsOfType<NetworkManager>();
        if(objs.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        //ConnectToServer();

        
    }

   

    public void ConnectToServer(){
        Debug.Log("Try to connect to server...");
        ServerStatus.text = "Try to connect to server...";
        PhotonNetwork.ConnectUsingSettings();
    }
    public void TestBtn()
    {
        Debug.Log("Testing...");
        ServerStatus.text = "Testing...";

    }

    public void InitializeRoom(string sPlayerName){
        DefaultRoom roomSettings = defaultRooms[0];
        PlayerName = sPlayerName;
        Debug.Log(NetworkManager.Instance.PlayerName+" : Name");

        //Load Scene
        PhotonNetwork.LoadLevel(roomSettings.SceneName);
        //Create the room
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)roomSettings.maxPlayer;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;

        PhotonNetwork.JoinOrCreateRoom(roomSettings.Name, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedLobby(){
        Debug.Log("We Joined The Lobby");
        ServerStatus.text = "We Joined The Lobby";

        base.OnJoinedLobby();
      //RoomUI.SetActive(true);
    }

    public override void OnConnectedToMaster(){
        Debug.Log("Connected to Server");
        ServerStatus.text = "Connected to Server";

        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
       
    }

    public override void OnJoinedRoom(){
        Debug.Log("Joined a Room");
        //ServerStatus.text = "Joined a Room";

        base.OnJoinedRoom();

        Debug.Log(NetworkManager.Instance.PlayerName+" Joined");
        if(NetworkManager.Instance.PlayerName == "VRNetworkPlayer(1)")
        {
            spawnedPlayerPrefab = PhotonNetwork.Instantiate(NetworkManager.Instance.PlayerName, new Vector3(0, 0, 0), transform.rotation);
        }
        else if(NetworkManager.Instance.PlayerName == "VRNetworkPlayer(2)"){
            spawnedPlayerPrefab = PhotonNetwork.Instantiate(NetworkManager.Instance.PlayerName, new Vector3(1, 0, 1), Quaternion.Euler(0, 180, 0));
        }
        else{
            spawnedPlayerPrefab = PhotonNetwork.Instantiate(NetworkManager.Instance.PlayerName, new Vector3(0, 0, 1), Quaternion.Euler(0, 180, 0));

        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) { //NetworkPlayer
        Debug.Log("A new player Joined the room");
        base.OnPlayerEnteredRoom(newPlayer);
    }
    public override void OnLeftRoom(){
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}
