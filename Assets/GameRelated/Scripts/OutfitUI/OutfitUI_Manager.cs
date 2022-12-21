using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitUI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] boxes,texts,buttons,contents;
    [SerializeField] private GameObject[] allObjs;
    private bool currentState = false;
    [HideInInspector] public GameObject myPlayer;
    [SerializeField] private PlayerOutfitData outfitData;

    private void Start()
    {
        Set(0);
        Set_Active(false);
    }

    public void Set(int targetNum)
    {
        Set_OnePositive(boxes,true ,targetNum);
        Set_OnePositive(texts,true,targetNum);
        Set_OnePositive(buttons,true,targetNum);
        Set_OnePositive(contents,true,targetNum);
    }

    private void Set_OnePositive(GameObject[] targetObjs,bool inputBool,int inputNum)
    {
        foreach(GameObject targetObj in targetObjs)
        {
            targetObj.SetActive(!inputBool);
        }

        targetObjs[inputNum].SetActive(inputBool);
    }

    public void Set_Active(bool inputBool)
    {
        foreach (GameObject targetObj in allObjs)
        {
            targetObj.SetActive(inputBool);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CloseOrOpenBag();
        }
    }

    public void UploadOutfitDatatoSQL()
    {
        var outfit = "";
        foreach( var x in outfitData.OutfitNums) 
        {
            outfit += x+"/" ;
        }
        GameObject.Find("Canvas").GetComponent<OutfitToSQL>().Upload(outfitData.id,outfit);    
    }

    public void CloseOrOpenBag()
    {
        Set_Active(!currentState);
        if(currentState)
        {
            UploadOutfitDatatoSQL();
        }
        currentState = !currentState;
    }

    /*
    public void ChangeOutfit(int whichBodypart,int whichOutfitNum)
    {
        //Click Button will trigger this func
        //then change Player's outfit, but how?
        //trigger the func in PhotonPlayer?
        //bodyparts already matched Data
        //don't need the bodypart data in playerOutfitData, delete it!

        //trigger the func in PhotonPlayer, then let the data inside it upload on Photon

        //PhotonPlayer playerController = myPlayer.GetComponent<PhotonPlayer>();

        //NO, PhotonPlayer is finished already, change the OutfitData will automatically change the outfit

        //Button doesn't accept two parameter in one func

        //MOVE THIS TO other scirpt call "OutfitUI_ChangeOutfitButton"

        outfitData.OutfitNum[whichBodypart] = whichOutfitNum;
    }
    */

}
