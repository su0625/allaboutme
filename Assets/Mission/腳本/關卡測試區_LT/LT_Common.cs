using System.Collections;
using System.Collections.Generic;
using UI_ScrollView;
using UnityEngine;

public class LT_Common : MonoBehaviour
{
    TaskDataController TaskDataController;
    [SerializeField] TaskData TaskData;
    private GameObject 任務資訊面板;
    private UI_Scroll 關卡清單;
    private int 關卡數量;
    private void Awake()
    {
        TaskDataController = GameObject.Find("Canvas").GetComponent<TaskDataController>();
    }
    void Start()
    {
        任務資訊面板 = this.gameObject.transform.Find("任務資訊面板").gameObject;
        關卡清單 = this.gameObject.transform.Find("滑動區塊").GetComponent<UI_Scroll>();
        GameObject.Find("Canvas").SendMessage("ShowTask");
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
    }
    void Update()
    {
        
    }
    public void 觸發任務()
    {
        任務資訊面板.SetActive(true);
        任務資訊面板.transform.Find("面板/TaskName").GetComponent<TMPro.TMP_Text>().text = TaskData.TaskName;
        任務資訊面板.transform.Find("面板/Task_Description").GetComponent<TMPro.TMP_Text>().text = TaskData.TaskDescription;
    }
    public void 接受任務()
    {
        GameObject.Find("Canvas").SendMessage("LoadLevelWithCheakConnecting", "LevelRunner");
    }
    public void 返回任務()
    {
        任務資訊面板.SetActive(false);
    }
    public void 結束測試()
    {
        GameObject.Find("Canvas").SendMessage("LoadLevel", "EditHome");
    }
    public void 顯示現有關卡(List<string> TaskName)
    {
        關卡清單.初始化(CallBack);
        關卡數量 = 0;
        foreach (var x in TaskName)
        {
            關卡數量++;
        }
        關卡清單.顯示列表(關卡數量);
        for (int i = 0; i < 關卡數量; i++)
        {
            this.gameObject.transform.Find("滑動區塊/內容/" + i.ToString() + "/任務名稱").GetComponent<TMPro.TMP_Text>().text = TaskName[i].ToString();
        }
    }
    private void CallBack(GameObject cell, int index)
    {
    }
}
