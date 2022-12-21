using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Chatsys : MonoBehaviour
{
    [SerializeField] Text MessageText;
    [SerializeField] InputField InputMessage;
    public List<string> messageList;
    public PlayerData PlayerData;
    private PhotonView _pv;
    string textColor;
    void Start()
    {
        _pv = this.gameObject.GetComponent<PhotonView>();    
    }

    void Update()
    {
        if(InputMessage.text != "")
        {
            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                CallRpcSendMessageToAll(PhotonNetwork.LocalPlayer.NickName + " : " + InputMessage.text);
            }
        }
    }

    public void SendMsgBtnClick()
    {
        if(InputMessage.text != "" )
        {
            CallRpcSendMessageToAll(PhotonNetwork.LocalPlayer.NickName + " : " + InputMessage.text);
        }
    }

    public void CallRpcSendMessageToAll(string message)
    {
        textColor = "<color=" + PlayerData.color +  ">";
        message = textColor + message + "</color>";
        // print(message);
        _pv.RPC("RpcSendMessage", RpcTarget.All, message);
    }

    [PunRPC]
    void RpcSendMessage(string message, PhotonMessageInfo info)
    {
        if(messageList.Count >= 15)
        {
            messageList.RemoveAt(0);
        }
        messageList.Add(message);
        UpdateMessage();
    }

    void UpdateMessage()
    {
        MessageText.text = string.Join("\n",messageList);
        InputMessage.text = "";
    }
}
