using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class EH_MountOnCell : MonoBehaviour
{
    [SerializeField] TaskData TaskData;
    public void 編輯特定關卡()
    {
        清空TaskData();
        print(this.gameObject.transform.Find("TaskName").GetComponent<TMPro.TMP_Text>().text);
        GameObject.Find("Canvas").SendMessage("Download", this.gameObject.transform.Find("TaskName").GetComponent<TMPro.TMP_Text>().text);
        GameObject.Find("Canvas").SendMessage("LoadLevelWithCheakConnecting", "LevelEdit");
    }
    public void 刪除特定關卡()
    {
        清空TaskData();
        print("準備刪除" + this.gameObject.transform.Find("TaskName").GetComponent<TMPro.TMP_Text>().text);
        GameObject.Find("Canvas").SendMessage("Download", this.gameObject.transform.Find("TaskName").GetComponent<TMPro.TMP_Text>().text);
        GameObject.Find("Canvas").SendMessage("刪除關卡");
    }
    private void 清空TaskData()
    {
        TaskData.TaskName = "";
        TaskData.TaskDescription = "";
        TaskData.Background = null;
        TaskData.Appearance = null;
        TaskData.EIAW = false;
        TaskData.NPCNumber = 0;
        TaskData.NPCList.Clear();
        TaskData.NPCPicture.Clear();
        TaskData.TextNumber = 0;
        TaskData.TextList.Clear();
        TaskData.Picture.Clear();
        TaskData.PictureFile.Clear();
        TaskData.Ability.Clear();
    }
}
