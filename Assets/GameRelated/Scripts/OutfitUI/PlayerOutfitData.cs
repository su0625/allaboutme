using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

[CreateAssetMenu(fileName = "OutfitData", menuName = "Data/OutfitData")]
public class PlayerOutfitData : ScriptableObject
{
    //total 10 parts (no heads
    //Hat(0) HairFront(1) HairBack(2) Face(3) SholderUp(4) BodyUp(5) BodyDown(6) SholderDown(7) LegUp(8) LegDown(9)

    [System.Serializable]
    public class OutfitSprites
    {
        public Sprite[] sprites;            // [] should be input "which Outfit Num"
    }

    public OutfitSprites[] outfitSprites;   // [] should be input "which Bodypart Num"

    public int[] OutfitNums = new int[10];

    public PlayerData playerData;

    public bool loginFirstTime = true;

    public string id = "2";


    private void Awake()
    {
        //loginFirstTime = OnlineDatabase.player[playerID].loginFirstTime;   <---database code here
        if (loginFirstTime)
        {
            if (playerData.sex == "boy")
            {
                for (int i = 0; i < OutfitNums.Length; i++)
                {
                    OutfitNums[i] = 1;

                    //�ui�vmeans which bodypart
                    
                    //OnlineDatabase.player[playerID].outfitNum[i] = 1;   <---database code here
                    // GameObject.Find("Canvas").GetComponent<OutfitToSQL>().Download(id);

                }

            }
            else if (playerData.sex == "girl")
            {
                for (int i = 0; i < OutfitNums.Length; i++)
                {
                    OutfitNums[i] = 0;

                    //�ui�vmeans which bodypart

                    //OnlineDatabase.player[playerID].outfitNum[i] = 0;    <---database code here
                    // GameObject.Find("Canvas").GetComponent<OutfitToSQL>().Download(id);

                }
            }
        }
        else if (!loginFirstTime)
        {
            for (int i = 0; i < OutfitNums.Length; i++)
            {
                //OutfitNums[i] = OnlineDatabase.player[playerID].outfitNum[i];   <---database code here

                //�ui�vmeans which bodypart
                // GameObject.Find("Canvas").GetComponent<OutfitToSQL>().Download(id);

            }
        }
    }

    public void UpdateOutfitData(int whichBodypart,int targetNum)
    {
        OutfitNums[whichBodypart] = targetNum;
        //OnlineDatabase.player[playerID].outfitNum[whichBodypart] = targetNum;   <---database code here

        //Note�Gthis func will be triggered when MyPlayer change outfit , see the function "DatabaseUpdate_Outfit()" , it is inside PhotonPlayer Script
        
        // var outfit = "";

        // foreach( var x in OutfitNums) {
        //     outfit += x+"/" ;
        // }
        // // Debug.Log("outfit = "+ outfit);
        // GameObject.Find("Canvas").GetComponent<OutfitToSQL>().Upload(id,outfit);
    }
}

