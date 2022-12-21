using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class StageRed_Fix : MonoBehaviour
{
    private PhotonPlayer myPlayer;
    private bool playerChecked = false;
    [SerializeField] private GameObject stageObj;
    [SerializeField] private Image[] targetObjs;

    private void Update()
    {
        if(PhotonNetwork.CurrentRoom != null)
        {
            if (playerChecked == false)
            {
                PhotonPlayer[] allPlayers = FindObjectsOfType<PhotonPlayer>();

                foreach (PhotonPlayer target in allPlayers)
                {
                    if (target.photonView.IsMine)
                    {
                        myPlayer = target;
                        playerChecked = true;
                    }
                }
            }
            else
            {
                float dist = Vector3.Distance(stageObj.transform.position,myPlayer.transform.position);

                float max = 8;

                float targetTransparent = -dist/max + 1;

                // print(targetTransparent);

                if(targetTransparent >=0)
                {
                    foreach (Image targetImg in targetObjs)
                    {
                        targetImg.color = new Color(1, 1, 1, targetTransparent);
                    }
                }
                else
                {
                    foreach (Image targetImg in targetObjs)
                    {
                        targetImg.color = new Color(1, 1, 1, 0);
                    }
                }
            }
        }  
    }
}
