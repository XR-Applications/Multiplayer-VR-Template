using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class WorldManager : MonoBehaviourPunCallbacks
{
    #region Photon Callbacks
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("New player "+newPlayer.NickName+" entered "+PhotonNetwork.CurrentRoom.Name +" Total players: "+PhotonNetwork.CurrentRoom.PlayerCount);
    }
    #endregion
}
