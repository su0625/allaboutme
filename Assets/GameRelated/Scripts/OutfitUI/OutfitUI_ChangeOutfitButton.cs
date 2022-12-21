using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitUI_ChangeOutfitButton : MonoBehaviour
{
    [SerializeField] private PlayerOutfitData outfitData;

    [SerializeField] private int whichBodypart;
    //Hat(0) HairFront(1) HairBack(2) Face(3) SholderUp(4) BodyUp(5) BodyDown(6) SholderDown(7) LegUp(8) LegDown(9)

    private PhotonPlayer playerScript;

    public void ChangeOutfit(int whichOutfitNum)
    {

        foreach (PhotonPlayer target in FindObjectsOfType<PhotonPlayer>())
        {
            if (target.photonView.IsMine)
            {
                playerScript = target;
            }
        }

        playerScript.outfitNums[whichBodypart] = whichOutfitNum;

        if(whichBodypart == 1)
        {
            playerScript.outfitNums[2] = whichOutfitNum;
        }
        else if (whichBodypart == 2)
        {
            playerScript.outfitNums[1] = whichOutfitNum;
        }

        if (whichBodypart == 9)
        {
            playerScript.outfitNums[8] = whichOutfitNum;
        }
        else if (whichBodypart == 8)
        {
            playerScript.outfitNums[9] = whichOutfitNum;
        }

        if (whichBodypart == 5)
        {
            playerScript.outfitNums[4]= whichOutfitNum;
            playerScript.outfitNums[7]= whichOutfitNum;
        }

        //print("test:"+whichBodypart+"+"+whichOutfitNum);
    }
}
