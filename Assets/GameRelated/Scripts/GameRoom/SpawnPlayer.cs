using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Cinemachine;

public class SpawnPlayer : MonoBehaviourPunCallbacks
{
    private PhotonView pv;
    public GameObject Loading_System;
    public PlayerData PlayerData;
    private CinemachineVirtualCamera cinemachine;
    
    private void Start() 
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        pv = this.GetComponent<PhotonView>();
        cinemachine = FindObjectOfType<CinemachineVirtualCamera>();
        
        PhotonNetwork.JoinOrCreateRoom("grade",null,null);
       
    }

    void InitGame()
    {
        float spawnPointX = Random.Range(-3,3);
        float spawnPointY = 2;
        GameObject _player = PhotonNetwork.Instantiate("PhotonPlayer", new Vector3(spawnPointX, spawnPointY, 0), Quaternion.identity);
        PhotonPlayer playerController = GetComponent<PhotonPlayer>();

        cinemachine.Follow = _player.transform;

        FindObjectOfType<OutfitUI_Manager>().myPlayer = _player;
    }

    public void ExitGame()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnJoinedRoom()
    {
        InitGame();
        Debug.Log("Room " + PhotonNetwork.CurrentRoom.Name + " Joined!");
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("GameLobby");
        Debug.Log("LeaveRoom!");
    }
}
