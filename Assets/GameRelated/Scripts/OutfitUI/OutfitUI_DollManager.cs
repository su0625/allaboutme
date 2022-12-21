using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutfitUI_DollManager : MonoBehaviour
{
    [SerializeField] private Image[] bodyparts;
    //Hat(0) HairFront(1) HairBack(2) Face(3) SholderUp(4) BodyUp(5) BodyDown(6) SholderDown(7) LegUp(8) LegDown(9)
    private void Start()
    {
        DollUpdate();
    }

    private void Update()
    {
        DollUpdate();
    }

    private void DollUpdate()
    {
        PhotonPlayer[] allPhotonPlayer = FindObjectsOfType<PhotonPlayer>();

        foreach (PhotonPlayer target in allPhotonPlayer)
        {
            if (target.photonView.IsMine)
            {
                for (int i = 0; i < target.bodyparts.Length; i++)
                {
                    bodyparts[i].sprite = target.bodyparts[i].sprite;
                }
            }
        }
    }
}
