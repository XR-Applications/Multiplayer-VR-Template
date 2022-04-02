using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class LoginManager : MonoBehaviourPunCallbacks
{
    [SerializeField]TMP_InputField playerNameInputField;

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    #endregion

    #region UI Callbacks
    public void ConnectAnonymously()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void SetUpNicknameAndConnectToPhotonServers()
    {
        if(playerNameInputField != null)
        {
            PhotonNetwork.NickName = playerNameInputField.text;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    #endregion

    #region Photon Callbacks
    public override void OnConnected()
    {
        Debug.Log("Internet connection established.");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Photon Server connection established by "+ PhotonNetwork.NickName);
        PhotonNetwork.LoadLevel("HomeScene");

    }
    #endregion
}
