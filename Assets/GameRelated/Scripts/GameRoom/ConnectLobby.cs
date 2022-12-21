using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.Text;

public class ConnectLobby : MonoBehaviourPunCallbacks
{
    void Awake()
    {
        if(!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.SendRate = 40;
            PhotonNetwork.SerializationRate = 30;
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        print("LobbyJoined");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Room " + PhotonNetwork.CurrentRoom.Name + " Joined!");
    }
}
