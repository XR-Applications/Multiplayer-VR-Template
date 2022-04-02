using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager: MonoBehaviourPunCallbacks
{
    #region UI Callbacks
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
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
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"Room Created. {PhotonNetwork.CurrentRoom.Name}");
    }
    #endregion

    #region Private Methods
    private void CreateAndJoinRoom()
    {
        string randomRoom = "Aura";
        RoomOptions roomOptions = new RoomOptions()
        {
            MaxPlayers = 3
        };

        PhotonNetwork.CreateRoom(randomRoom, roomOptions);
    }
    #endregion
}
