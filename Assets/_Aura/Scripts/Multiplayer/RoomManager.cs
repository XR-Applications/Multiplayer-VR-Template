using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomManager: MonoBehaviourPunCallbacks
{
    private string mapType;

    public TextMeshProUGUI OccupancyRateText_Outdoor;
    public TextMeshProUGUI OccupancyRateText_School;

    #region Unity Methods
    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        //check if connected to servers
        if (PhotonNetwork.IsConnectedAndReady)
        {
            //join default lobby for purposes of accessing OnRoomListUpdate()
            PhotonNetwork.JoinLobby();
        }
    }
    #endregion

    #region UI Callbacks
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnEnterButtonClicked_Outdoor()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR;

        //create a new HashTable of expected room properties
        ExitGames.Client.Photon.Hashtable expectedProperties = new ExitGames.Client.Photon.Hashtable() 
        {
            { MultiplayerVRConstants.MAP_TYPE_KEY,mapType }
        };

        //join a room with these expected properties.
        PhotonNetwork.JoinRandomRoom(expectedProperties,0);
    }

    public void OnEnterButtonClicked_School()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_SCHOOL;

        //create a new HashTable of expected room properties
        ExitGames.Client.Photon.Hashtable expectedProperties = new ExitGames.Client.Photon.Hashtable()
        {
            { MultiplayerVRConstants.MAP_TYPE_KEY,mapType }
        };

        //join a room with these expected properties.
        PhotonNetwork.JoinRandomRoom(expectedProperties, 0);
    }

    #endregion

    #region Photon Callbacks
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        CreateAndJoinRoom();
    }
    public override void OnJoinedRoom()
    {
        Debug.Log($"{PhotonNetwork.NickName} Found a room called {PhotonNetwork.CurrentRoom.Name} with {PhotonNetwork.CurrentRoom.MaxPlayers} players! ");

        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("map"))
        {
            object mapType;
            if(PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MultiplayerVRConstants.MAP_TYPE_KEY,out mapType))
            {
                Debug.Log("Joined room with Custom property of "+ (string)mapType);
                if((string)mapType == MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR)
                {
                    //load outdoor scene
                    PhotonNetwork.LoadLevel("World_Outdoor");
                }else if((string)mapType == MultiplayerVRConstants.MAP_TYPE_VALUE_SCHOOL)
                {
                    //load school scene
                    PhotonNetwork.LoadLevel("World_School");
                }
            }
        }
    }
    public override void OnCreatedRoom()
    {
        Debug.Log($"Room Created. {PhotonNetwork.CurrentRoom.Name}");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
       if(roomList.Count == 0)
        {
            OccupancyRateText_School.text = 0 + "/" + 20;
            OccupancyRateText_Outdoor.text = 0 + "/" + 20;
        }

        foreach (var roomInfo in roomList)
        {
            if (roomInfo.Name.Contains(MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR))
            {
                //Update outdoor player count text
                OccupancyRateText_Outdoor.text = roomInfo.PlayerCount + "/" + 20;
            }
            else if (roomInfo.Name.Contains(MultiplayerVRConstants.MAP_TYPE_VALUE_SCHOOL))
            {
                //Update school player count text
                OccupancyRateText_School.text = roomInfo + "/" + 20;
            }
        }

    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined default Lobby");
    }
    #endregion

    #region Private Methods
    private void CreateAndJoinRoom()
    {
        string randomRoom = "Aura "+ mapType;
        RoomOptions roomOptions = new RoomOptions()
        {
            MaxPlayers = 3
        };

        ExitGames.Client.Photon.Hashtable roomProps = new ExitGames.Client.Photon.Hashtable()
        {
            {MultiplayerVRConstants.MAP_TYPE_KEY,mapType}
        };

        string[] roomPropertiesInLobby = { "map" };
        roomOptions.CustomRoomProperties = roomProps;
        roomOptions.CustomRoomPropertiesForLobby = roomPropertiesInLobby;
        PhotonNetwork.CreateRoom(randomRoom, roomOptions);
    }
    #endregion
}
