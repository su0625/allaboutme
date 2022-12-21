using UnityEngine;
using UI_ScrollView;
using System.Collections.Generic;
using System.Collections;

public class EH_Common : MonoBehaviour
{
    TaskDataController TaskDataController;
    [SerializeField] TaskData TaskData;
    [Header("彈跳視窗")]
    private GameObject 彈跳視窗區塊;
    private GameObject 暫停視窗;
    private GameObject 退出視窗;
    private GameObject 設定視窗;
    private GameObject 新關卡視窗;
    private GameObject 關卡清單視窗;
    private GameObject 刪除確認視窗;
    private UI_Scroll 關卡清單;
    private GameObject 警告視窗;

    [Header("創建新關卡")]
    public TMPro.TMP_InputField 關卡名輸入框;//還沒實際作用

    private int 關卡數量;
    void Awake()
    {
        TaskDataController = GameObject.Find("Canvas").GetComponent<TaskDataController>();
        彈跳視窗區塊 = this.gameObject.transform.Find("彈跳視窗").gameObject;
        暫停視窗 = 彈跳視窗區塊.transform.Find("暫停視窗").gameObject;
        退出視窗 = 彈跳視窗區塊.transform.Find("退出視窗").gameObject;
        設定視窗 = 彈跳視窗區塊.transform.Find("設定視窗").gameObject;
        新關卡視窗 = 彈跳視窗區塊.transform.Find("新關卡視窗").gameObject;
        關卡清單視窗 = 彈跳視窗區塊.transform.Find("關卡清單視窗").gameObject;
        刪除確認視窗 = 彈跳視窗區塊.transform.Find("刪除確認視窗").gameObject;
        關卡清單 = 關卡清單視窗.transform.Find("關卡清單/滑動區塊").GetComponent<UI_Scroll>();
        警告視窗 = this.gameObject.transform.Find("警告視窗").gameObject;
    }
    void Start()
    {
        彈跳視窗區塊.SetActive(false);
        關卡清單.初始化(CallBack);
        關卡清單.顯示列表(關卡數量);
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape)) & (彈跳視窗區塊.activeSelf == false)) 啟用暫停();
        else if ((Input.GetKeyDown(KeyCode.Escape)) & (暫停視窗.activeSelf == true)) 關閉彈跳視窗();
        else if ((Input.GetKeyDown(KeyCode.Escape)) & (退出視窗.activeSelf == true)) 關閉彈跳視窗();
        else if ((Input.GetKeyDown(KeyCode.Escape)) & (設定視窗.activeSelf == true)) 啟用暫停();
        else if ((Input.GetKeyDown(KeyCode.Escape)) & (新關卡視窗.activeSelf == true)) 關閉彈跳視窗();
        else if ((Input.GetKeyDown(KeyCode.Escape)) & (關卡清單視窗.activeSelf == true)) 關閉彈跳視窗();
    }

    #region CallBack
    private void CallBack(GameObject cell, int index)
    {
    }
    #endregion

    #region 彈跳視窗
    private void 關閉所有視窗()
    {
        暫停視窗.SetActive(false);
        退出視窗.SetActive(false);
        設定視窗.SetActive(false);
        新關卡視窗.SetActive(false);
        關卡清單視窗.SetActive(false);
        刪除確認視窗.SetActive(false);
    }
    public void 關閉彈跳視窗()
    {
        彈跳視窗區塊.SetActive(false);
    }
    public void 啟用暫停()
    {
        彈跳視窗區塊.SetActive(true);
        關閉所有視窗();
        暫停視窗.SetActive(true);
    }
    public void 開啟設定()
    {
        關閉所有視窗();
        設定視窗.SetActive(true);
    }
    public void 更改密碼()
    {
        //連接到更改密碼系統那邊
    }
    public void 點擊退出()
    {
        彈跳視窗區塊.SetActive(true);
        關閉所有視窗();
        退出視窗.SetActive(true);
    }
    public void 退出_確定()
    {
        Application.Quit();
    }
    #endregion

    #region 主畫面功能
    public void 創建新關卡()
    {
        彈跳視窗區塊.SetActive(true);
        關閉所有視窗();
        新關卡視窗.SetActive(true);
    }
    public void 創建新關卡_確定()
    {
        //這裡要有儲存關卡名稱等
        if(關卡名輸入框.text != "")
        {
            清空TaskData();
            TaskData.TaskName = 關卡名輸入框.text;
            GameObject.Find("Canvas").SendMessage("LoadLevel", "LevelEdit");
        }
        else
        {
            警告視窗.SetActive(true);
            警告視窗.transform.Find("WARNING/Text").GetComponent<TMPro.TMP_Text>().text = "Task name cannot be empty.";
        }
    }
    public void 編輯現有關卡()
    {
        彈跳視窗區塊.SetActive(true);
        關閉所有視窗();
        關卡清單視窗.SetActive(true);
        //從資料庫裡面找到此使用者的檔案並列出來
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
            關卡清單視窗.transform.Find("關卡清單/滑動區塊/內容/" + i.ToString() + "/TaskName").GetComponent<TMPro.TMP_Text>().text = TaskName[i].ToString();
        }
    }
    public void 刪除關卡()
    {
        刪除確認視窗.SetActive(true);
    }
    public void 刪除關卡_確認()
    {
        //刪除關卡
        GameObject.Find("Canvas").SendMessage("DeleteTask", TaskData.TaskName);
        編輯現有關卡();
    }
    public void 刪除關卡_取消()
    {
        刪除確認視窗.SetActive(false);
    }
    public void 測試關卡()
    {
        GameObject.Find("Canvas").SendMessage("LoadLevel", "LevelTest");
    }
    public void 創建房間()
    {
        //如果老師不想把自己做的關卡放在公共大廳，就可以自行開私人房間
        //透過隨機產生的五位字符的房間代碼
        //讓學生透過房間代碼加入房間
    }
    public void 編輯形象()
    {
        //編輯老師自身的形象
        //連接到變裝系統那邊
    }
    #endregion

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
