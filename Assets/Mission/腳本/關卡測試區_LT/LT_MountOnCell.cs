using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class LT_MountOnCell : MonoBehaviour
{
    TaskDataController TaskDataController;
    [SerializeField] TaskData TaskData;
    private void Awake()
    {
        TaskDataController = GameObject.Find("Canvas").GetComponent<TaskDataController>();
    }
    public void 開啟特定關卡()
    {
        清空TaskData();
        print(this.gameObject.transform.Find("任務名稱").GetComponent<TMPro.TMP_Text>().text);
        GameObject.Find("Canvas").SendMessage("Download", this.gameObject.transform.Find("任務名稱").GetComponent<TMPro.TMP_Text>().text);
        StartCoroutine(CheakDownload());
    }
    IEnumerator CheakDownload()
    {
        while (TaskDataController.isConnecting)
        {
            GameObject.Find("Canvas").GetComponent<UI_Loading>().CallLoadingPanel(0, true);
            yield return new WaitForSeconds(1);
        }
        GameObject.Find("Canvas").GetComponent<UI_Loading>().CallLoadingPanel(0, false);
        GameObject.Find("Canvas").SendMessage("觸發任務");
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
