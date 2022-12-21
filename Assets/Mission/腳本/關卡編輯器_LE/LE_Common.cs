using System;
using UnityEngine;
using UnityEngine.UI;
using UI_ScrollView;

public class LE_Common : MonoBehaviour
{
    [SerializeField] TaskData TaskData;
    public Sprite NoPicture;
    [Header("彈跳視窗")]
    private GameObject 彈跳視窗區塊;
    private GameObject 暫停視窗;
    private GameObject 設定視窗;
    private GameObject 取消編輯視窗;
    private GameObject 完成編輯視窗;

    [Header("面板區塊")]
    private GameObject 編輯面板;
    private GameObject 編輯面板_基本設定;
    private GameObject 編輯面板_NPC設定;
    private GameObject 編輯面板_文本設定;
    private GameObject 編輯面板_問題設定;
    private GameObject 編輯面板_能力設定;
    private GameObject 編輯面板_圖片設定;

    [Header("UI_Scroll")]
    private UI_Scroll NPC列表;
    private UI_Scroll 文本列表;
    private UI_Scroll 問題列表;
    private UI_Scroll 圖片列表;
    private int NPC行數 = 0;
    private int 文本行數 = 1;
    private int 問題行數 = 0;
    public int NPC數量限制 = 8;
    private string 原Task名稱;
    private bool TaskName更動;

    void Awake()
    {
        彈跳視窗區塊 = this.gameObject.transform.Find("彈跳視窗").gameObject;
        暫停視窗 = 彈跳視窗區塊.transform.Find("暫停視窗").gameObject;
        設定視窗 = 彈跳視窗區塊.transform.Find("設定視窗").gameObject;
        取消編輯視窗 = 彈跳視窗區塊.transform.Find("取消編輯視窗").gameObject;
        完成編輯視窗 = 彈跳視窗區塊.transform.Find("完成編輯視窗").gameObject;
        編輯面板 = this.gameObject.transform.Find("編輯面板").gameObject;
        編輯面板_基本設定 = 編輯面板.transform.Find("基本設定").gameObject;
        編輯面板_NPC設定 = 編輯面板.transform.Find("NPC設定").gameObject;
        編輯面板_文本設定 = 編輯面板.transform.Find("文本設定").gameObject;
        編輯面板_問題設定 = 編輯面板.transform.Find("問題設定").gameObject;
        編輯面板_能力設定 = 編輯面板.transform.Find("能力設定").gameObject;
        編輯面板_圖片設定 = 編輯面板.transform.Find("圖片設定").gameObject;
        NPC列表 = 編輯面板_NPC設定.transform.Find("NPC列表/滑動區塊").GetComponent<UI_Scroll>();
        文本列表 = 編輯面板_文本設定.transform.Find("文本列表/滑動區塊").GetComponent<UI_Scroll>();
        問題列表 = 編輯面板_問題設定.transform.Find("問題列表/滑動區塊").GetComponent<UI_Scroll>();
        圖片列表 = 編輯面板_圖片設定.transform.Find("圖片列表/滑動區塊").GetComponent<UI_Scroll>();
    }
    void Start()
    {
        編輯面板.SetActive(true);
        彈跳視窗區塊.SetActive(false);
        NPC列表.初始化(CallBack);
        文本列表.初始化(CallBack);
        問題列表.初始化(CallBack);
        圖片列表.初始化(CallBack);
        //將資料轉移出資料庫();
        //清空TaskData();
        //gameObject.GetComponent<TaskDataToSQL>().Download("Hello World");
        //gameObject.GetComponent<TaskDataToSQL>().ShowTask();
        載入資料();
    }

    void Update()
    {
        //載入資料();
    }
    #region CallBack
    private void CallBack(GameObject cell, int index)
    {
    }
    private void NPC_CallBack(GameObject cell, int index) //已棄用
    {
        cell.transform.Find("InputText").GetComponent<TMPro.TMP_InputField>().text = TaskData.NPCList[index];
        //cell.transform.Find("Picture").GetComponent<Text>().text = index.ToString();
        //cell.transform.Find("Background").GetComponent<Text>().text = index.ToString();
    }
    private void Text_CallBack(GameObject cell, int index) //已棄用
    {
        cell.transform.Find("NPC").GetComponent<TMPro.TMP_Dropdown>().options[index].text = TaskData.NPCList[index];
        cell.transform.Find("InputText").GetComponent<Text>().text = index.ToString();
        cell.transform.Find("Picture").GetComponent<Text>().text = index.ToString();
        cell.transform.Find("Background").GetComponent<Text>().text = index.ToString();
    }
    #endregion

    #region 彈跳視窗
    private void 關閉所有視窗()
    {
        暫停視窗.SetActive(false);
        設定視窗.SetActive(false);
        取消編輯視窗.SetActive(false);
        完成編輯視窗.SetActive(false);
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
    public void 點擊取消編輯()
    {
        彈跳視窗區塊.SetActive(true);
        關閉所有視窗();
        取消編輯視窗.SetActive(true);
    }
    public void 取消編輯_確定()
    {
        //清空TaskData();
        GameObject.Find("Canvas").SendMessage("LoadLevel", "EditHome");
    }
    public void 點擊完成編輯()
    {
        彈跳視窗區塊.SetActive(true);
        關閉所有視窗();
        完成編輯視窗.SetActive(true);
    }
    public void 完成編輯_確定()
    {
        上傳資料();
        GameObject.Find("Canvas").SendMessage("LoadLevelWithCheakConnecting", "EditHome");
    }
    public void 點擊開始測試()
    {
        上傳資料();
        GameObject.Find("Canvas").SendMessage("LoadLevelWithCheakConnecting", "LevelTest");
    }
    #endregion

    #region 編輯面板
    private void 關閉所有區塊()
    {
        編輯面板_基本設定.SetActive(false);
        編輯面板_NPC設定.SetActive(false);
        編輯面板_文本設定.SetActive(false);
        編輯面板_能力設定.SetActive(false);
        編輯面板_問題設定.SetActive(false);
        編輯面板_圖片設定.SetActive(false);
    }
    public void 移至基本設定()
    {
        關閉所有區塊();
        編輯面板_基本設定.SetActive(true);
    }
    public void 移至NPC設定()
    {
        關閉所有區塊();
        編輯面板_NPC設定.SetActive(true);
    }
    public void 移至文本設定()
    {
        關閉所有區塊();
        編輯面板_文本設定.SetActive(true);
    }
    public void 移至問題設定()
    {
        關閉所有區塊();
        編輯面板_問題設定.SetActive(true);
    }
    public void 移至能力設定()
    {
        關閉所有區塊();
        編輯面板_能力設定.SetActive(true);
    }
    public void 移至圖片設定()
    {
        關閉所有區塊();
        編輯面板_圖片設定.SetActive(true);
    }
    #endregion

    #region 任務區塊
    //----------基本設定----------//
    public void 存儲標題名稱()
    {
        if(TaskData.TaskName != 編輯面板_基本設定.transform.Find("滑動區塊遮罩/滑動區塊/標題/輸入框").GetComponent<TMPro.TMP_InputField>().text && TaskName更動 == false)
        {
            原Task名稱 = TaskData.TaskName;
            TaskData.TaskName = 編輯面板_基本設定.transform.Find("滑動區塊遮罩/滑動區塊/標題/輸入框").GetComponent<TMPro.TMP_InputField>().text;
            TaskName更動 = true;
        }
        else
        {
            TaskData.TaskName = 編輯面板_基本設定.transform.Find("滑動區塊遮罩/滑動區塊/標題/輸入框").GetComponent<TMPro.TMP_InputField>().text;
        }
        TaskData.TaskName = 編輯面板_基本設定.transform.Find("滑動區塊遮罩/滑動區塊/標題/輸入框").GetComponent<TMPro.TMP_InputField>().text;
        TaskData.TaskDescription = 編輯面板_基本設定.transform.Find("滑動區塊遮罩/滑動區塊/說明/輸入框").GetComponent<TMPro.TMP_InputField>().text;
        TaskData.EIAW = 編輯面板_基本設定.transform.Find("滑動區塊遮罩/滑動區塊/答錯退出").GetComponent<Toggle>().isOn;
    }
    //----------NPC設定----------//
    public void 新增一個NPC()
    {
        NPC行數++;
        if (NPC行數 > NPC數量限制) NPC行數 = NPC數量限制;
        else
        {
            NPC列表.顯示列表(NPC行數);
            編輯面板_NPC設定.transform.Find("NPC列表/滑動區塊/內容/" + (NPC行數 - 1).ToString() + "/InputText").GetComponent<TMPro.TMP_InputField>().text = "";
            編輯面板_NPC設定.transform.Find("NPC列表/滑動區塊/內容/" + (NPC行數 - 1).ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().ClearOptions();
            TMPro.TMP_Dropdown.OptionData NoPic = new TMPro.TMP_Dropdown.OptionData();
            NoPic.text = "None";
            NoPic.image = NoPicture;
            編輯面板_NPC設定.transform.Find("NPC列表/滑動區塊/內容/" + (NPC行數 - 1).ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().options.Add(NoPic);
            for (int i = 0; i < TaskData.Picture.Count; i++)
            {
                TMPro.TMP_Dropdown.OptionData data = new TMPro.TMP_Dropdown.OptionData();
                data.text = TaskData.PictureFile[i];
                data.image = TaskData.Picture[i];
                編輯面板_NPC設定.transform.Find("NPC列表/滑動區塊/內容/" + (NPC行數 - 1).ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().options.Add(data);
            }
        }
    }
    public void 刪除一個NPC()
    {
        NPC行數--;
        if (NPC行數 < 0) NPC行數 = 0;
        NPC列表.顯示列表(NPC行數);
        更新NPC選項();
    }
    public void 刪除指定NPC(int line)
    {
        for (int i = line; i < NPC行數 - 1; i++)
        {
            編輯面板_NPC設定.transform.Find("NPC列表/滑動區塊/內容/" + i.ToString() + "/InputText").GetComponent<TMPro.TMP_InputField>().text = 編輯面板_NPC設定.transform.Find("NPC列表/滑動區塊/內容/" + (i + 1).ToString() + "/InputText").GetComponent<TMPro.TMP_InputField>().text;
            編輯面板_NPC設定.transform.Find("NPC列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().value = 編輯面板_NPC設定.transform.Find("NPC列表/滑動區塊/內容/" + (i + 1).ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().value;
        }
        刪除一個NPC();
    }
    public void 更新NPC選項()
    {
        TaskData.NPCList.Clear();
        TaskData.NPCList.Add("None");
        for (int i = 0; i < NPC行數; i++)
        {
            if (編輯面板_NPC設定.transform.Find("NPC列表/滑動區塊/內容/" + i.ToString() + "/InputText").GetComponent<TMPro.TMP_InputField>().text == "")
            {
                for (int j = i; j < (NPC行數 - 1); j++)
                {
                    編輯面板_NPC設定.transform.Find("NPC列表/滑動區塊/內容/" + j.ToString() + "/InputText").GetComponent<TMPro.TMP_InputField>().text = 編輯面板_NPC設定.transform.Find("NPC列表/滑動區塊/內容/" + (j + 1).ToString() + "/InputText").GetComponent<TMPro.TMP_InputField>().text;
                    編輯面板_NPC設定.transform.Find("NPC列表/滑動區塊/內容/" + j.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().value = 編輯面板_NPC設定.transform.Find("NPC列表/滑動區塊/內容/" + (j + 1).ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().value;
                }
                NPC行數--;
                if (NPC行數 < 0) NPC行數 = 0;
                NPC列表.顯示列表(NPC行數);
                i--;
            }
            else
            {
                TaskData.NPCList.Add(編輯面板_NPC設定.transform.Find("NPC列表/滑動區塊/內容/" + i.ToString() + "/InputText").GetComponent<TMPro.TMP_InputField>().text);
            }
        }
        TaskData.NPCList.Add("Choice");
        TaskData.NPCList.Add("Question");
        for (int i = 0; i < (文本行數 + 1); i++)
        {
            try
            {
                int x = 編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/NPC").GetComponent<TMPro.TMP_Dropdown>().value;
                編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/NPC").GetComponent<TMPro.TMP_Dropdown>().ClearOptions();
                編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/NPC").GetComponent<TMPro.TMP_Dropdown>().AddOptions(TaskData.NPCList);
                編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/NPC").GetComponent<TMPro.TMP_Dropdown>().RefreshShownValue();
                if (x < TaskData.NPCNumber) 編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/NPC").GetComponent<TMPro.TMP_Dropdown>().value = x;
                else if(x <= TaskData.NPCNumber && NPC行數 >= TaskData.NPCNumber) 編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/NPC").GetComponent<TMPro.TMP_Dropdown>().value = x;
                else if (x == TaskData.NPCNumber + 1) 編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/NPC").GetComponent<TMPro.TMP_Dropdown>().value = NPC行數 + 1;
                else if (x == TaskData.NPCNumber + 2) 編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/NPC").GetComponent<TMPro.TMP_Dropdown>().value = NPC行數 + 2;
                else 編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/NPC").GetComponent<TMPro.TMP_Dropdown>().value = 0;
            }
            catch { }
        }
        TaskData.NPCNumber = NPC行數;
        問題檢查器();
        更新NPC圖片();
    }
    public void 更新NPC圖片()
    {
        TaskData.NPCPicture.Clear();
        for (int i = 0; i < NPC行數; i++)
        {
            TaskData.NPCPicture.Add(編輯面板_NPC設定.transform.Find("NPC列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().value);
        }
    }
    //----------文本設定----------//
    public void 新增一行文本()
    {
        文本行數++;
        文本列表.顯示列表(文本行數);
        編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + (文本行數 - 1).ToString() + "/NPC").GetComponent<TMPro.TMP_Dropdown>().ClearOptions();
        編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + (文本行數 - 1).ToString() + "/NPC").GetComponent<TMPro.TMP_Dropdown>().AddOptions(TaskData.NPCList);
        編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + (文本行數 - 1).ToString() + "/InputText").GetComponent<TMPro.TMP_InputField>().text= "";
        編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + (文本行數 - 1).ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().ClearOptions();
        TMPro.TMP_Dropdown.OptionData NoPic = new TMPro.TMP_Dropdown.OptionData();
        NoPic.text = "None";
        NoPic.image = NoPicture;
        編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + (文本行數 - 1).ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().options.Add(NoPic);
        for (int i = 0; i < TaskData.Picture.Count; i++)
        {
            TMPro.TMP_Dropdown.OptionData data = new TMPro.TMP_Dropdown.OptionData();
            data.text = TaskData.PictureFile[i];
            data.image = TaskData.Picture[i];
            編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + (文本行數 - 1).ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().options.Add(data);
        }
        編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + (文本行數 - 1).ToString() + "/ToQuestion").gameObject.SetActive(false);
    }
    public void 刪除一行文本()
    {
        文本行數--;
        if (文本行數 <= 1) 文本行數 = 1;
        文本列表.顯示列表(文本行數);
    }
    public void 刪除指定文本(int line)
    {
        for (int i = line; i < 文本行數 - 1; i++)
        {
            編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/NPC").GetComponent<TMPro.TMP_Dropdown>().value = 編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + (i + 1).ToString() + "/NPC").GetComponent<TMPro.TMP_Dropdown>().value;
            編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/InputText").GetComponent<TMPro.TMP_InputField>().text = 編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + (i + 1).ToString() + "/InputText").GetComponent<TMPro.TMP_InputField>().text;
            編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().value = 編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + (i + 1).ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().value;
        }
        刪除一行文本();
        問題檢查器();
    }
    public void 更新文本內容()
    {
        int 問題行數Count = 0;
        TaskData.TextNumber = 文本行數;
        TaskData.TextList.Clear();
        for (int i = 0; i < 文本行數; i++)
        {
            if (編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/NPC").GetComponent<TMPro.TMP_Dropdown>().value == 0)
            {
                TaskData.TextList.Add("無");
                TaskData.TextList.Add(編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().value.ToString());
                TaskData.TextList.Add(編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/InputText").GetComponent<TMPro.TMP_InputField>().text);
            }
            else if (編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/NPC").GetComponent<TMPro.TMP_Dropdown>().value == NPC行數 + 1)
            {
                TaskData.TextList.Add("選擇");
                TaskData.TextList.Add(編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + 問題行數Count.ToString() + "/Answer1").GetComponent<TMPro.TMP_InputField>().text);
                TaskData.TextList.Add(編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + 問題行數Count.ToString() + "/Answer2").GetComponent<TMPro.TMP_InputField>().text);
                TaskData.TextList.Add(編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + 問題行數Count.ToString() + "/Answer3").GetComponent<TMPro.TMP_InputField>().text);
                if (編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + 問題行數Count.ToString() + "/Answer1/Toggle").GetComponent<Toggle>().isOn == true) TaskData.TextList.Add("Answer1");
                else if (編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + 問題行數Count.ToString() + "/Answer2/Toggle").GetComponent<Toggle>().isOn == true) TaskData.TextList.Add("Answer2");
                else if (編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + 問題行數Count.ToString() + "/Answer3/Toggle").GetComponent<Toggle>().isOn == true) TaskData.TextList.Add("Answer3");
                else TaskData.TextList.Add("NoAnswer");
                TaskData.TextList.Add(編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + 問題行數Count.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().value.ToString());
                TaskData.TextList.Add(編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + 問題行數Count.ToString() + "/InputText").GetComponent<TMPro.TMP_InputField>().text);
                問題行數Count++;
            }
            else if (編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/NPC").GetComponent<TMPro.TMP_Dropdown>().value == NPC行數 + 2)
            {
                TaskData.TextList.Add("問答");
                TaskData.TextList.Add(編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + 問題行數Count.ToString() + "/AnswerQA/InputField").GetComponent<TMPro.TMP_InputField>().text);
                if (編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + 問題行數Count.ToString() + "/AnswerQA/Toggle").GetComponent<Toggle>().isOn == true) TaskData.TextList.Add("CheckAnswer");
                else TaskData.TextList.Add("NoAnswer");
                TaskData.TextList.Add(編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + 問題行數Count.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().value.ToString());
                TaskData.TextList.Add(編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + 問題行數Count.ToString() + "/InputText").GetComponent<TMPro.TMP_InputField>().text);
                問題行數Count++;
            }
            else
            {
                TaskData.TextList.Add("NPC_" + 編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/NPC").GetComponent<TMPro.TMP_Dropdown>().value.ToString());
                TaskData.TextList.Add(編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().value.ToString());
                TaskData.TextList.Add(編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/InputText").GetComponent<TMPro.TMP_InputField>().text);
            }
        }
        TaskData.TextList.Add("結尾");
        TaskData.TextList.Add("0");
        TaskData.TextList.Add("");
    }
    //----------問題設定----------//
    public void 問題檢查器()
    {
        int 原問題行數 = 問題行數;
        int 問題行數Count = 0;
        問題行數 = 0;
        for (int i = 0; i < 文本行數; i++)
        {
            if (編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/NPC").GetComponent<TMPro.TMP_Dropdown>().value == NPC行數 + 1)
            {
                編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/ToQuestion").gameObject.SetActive(true);
                問題行數++;

            }
            else if(編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/NPC").GetComponent<TMPro.TMP_Dropdown>().value == NPC行數 + 2)
            {
                編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/ToQuestion").gameObject.SetActive(true);
                問題行數++;
            }
            else
            {
                編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/ToQuestion").gameObject.SetActive(false);
            }
        }
        問題列表.顯示列表(問題行數);
        for (int i = 0; i < 文本行數; i++)
        {
            if (編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/NPC").GetComponent<TMPro.TMP_Dropdown>().value == NPC行數 + 1)
            {
                新增一行問題("MC", 問題行數Count);
                問題行數Count++;

            }
            else if (編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/NPC").GetComponent<TMPro.TMP_Dropdown>().value == NPC行數 + 2)
            {
                新增一行問題("QA", 問題行數Count);
                問題行數Count++;
            }
        }
        更新問題列表(原問題行數);
    }
    public void 新增一行問題(string mode,int 問題行數Count)
    {
        if (mode == "MC")
        {
            編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + 問題行數Count.ToString() + "/Title").GetComponent<TMPro.TMP_Text>().text = "Multiple Choice";
            編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + 問題行數Count.ToString() + "/AnswerQA").gameObject.SetActive(false);

        }
        else if (mode == "QA")
        {
            編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + 問題行數Count.ToString() + "/Title").GetComponent<TMPro.TMP_Text>().text = "Questions and Answers";
            編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + 問題行數Count.ToString() + "/AnswerQA").gameObject.SetActive(true);
        }
    }
    public void 更新問題列表(int 原問題行數)
    {
        for (int i = 0; i < 原問題行數; i++)
        {
            int PicVal = 編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().value;
            if (編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Title").GetComponent<TMPro.TMP_Text>().text == "Multiple Choice")
            {
                編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/AnswerQA/InputField").GetComponent<TMPro.TMP_InputField>().text = "";
                編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/AnswerQA/Toggle").GetComponent<Toggle>().isOn = false;
            }
            else if(編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Title").GetComponent<TMPro.TMP_Text>().text == "Questions and Answers")
            {
                編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Answer1").GetComponent<TMPro.TMP_InputField>().text = "";
                編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Answer2").GetComponent<TMPro.TMP_InputField>().text = "";
                編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Answer3").GetComponent<TMPro.TMP_InputField>().text = "";
                編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Answer1/Toggle").GetComponent<Toggle>().isOn = false;
                編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Answer2/Toggle").GetComponent<Toggle>().isOn = false;
                編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Answer3/Toggle").GetComponent<Toggle>().isOn = false;
            }
            編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().ClearOptions();
            TMPro.TMP_Dropdown.OptionData NoPic = new TMPro.TMP_Dropdown.OptionData();
            NoPic.text = "None";
            NoPic.image = NoPicture;
            編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().options.Add(NoPic);
            for (int j = 0; j < TaskData.Picture.Count; j++)
            {
                TMPro.TMP_Dropdown.OptionData data = new TMPro.TMP_Dropdown.OptionData();
                string[] PF = TaskData.PictureFile[j].Split("_PF_");
                data.text = PF[1];
                data.image = TaskData.Picture[j];
                編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().options.Add(data);
            }
            編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().RefreshShownValue();
            編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().value = PicVal;
        }
        for (int i = 原問題行數; i < 問題行數; i++)
        {
            編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/InputText").GetComponent<TMPro.TMP_InputField>().text = "";
            編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Answer1").GetComponent<TMPro.TMP_InputField>().text = "";
            編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Answer2").GetComponent<TMPro.TMP_InputField>().text = "";
            編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Answer3").GetComponent<TMPro.TMP_InputField>().text = "";
            編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Answer1/Toggle").GetComponent<Toggle>().isOn = false;
            編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Answer2/Toggle").GetComponent<Toggle>().isOn = false;
            編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Answer3/Toggle").GetComponent<Toggle>().isOn = false;
            編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/AnswerQA/InputField").GetComponent<TMPro.TMP_InputField>().text = "";
            編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/AnswerQA/Toggle").GetComponent<Toggle>().isOn = false;
            編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().ClearOptions();
            TMPro.TMP_Dropdown.OptionData NoPic = new TMPro.TMP_Dropdown.OptionData();
            NoPic.text = "None";
            NoPic.image = NoPicture;
            編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().options.Add(NoPic);
            for (int j = 0; j < TaskData.Picture.Count; j++)
            {
                TMPro.TMP_Dropdown.OptionData data = new TMPro.TMP_Dropdown.OptionData();
                string[] PF = TaskData.PictureFile[j].Split("_PF_");
                data.text = PF[1];
                data.image = TaskData.Picture[j];
                編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().options.Add(data);
            }
            編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().RefreshShownValue();
        }
    }
    //----------圖片設定----------//
    public void 新增一行圖片()
    {
        圖片列表.顯示列表(TaskData.Picture.Count + 1);
        編輯面板_圖片設定.transform.Find("圖片列表/滑動區塊/內容/" + TaskData.Picture.Count.ToString() + "/Image").GetComponent<Image>().sprite = NoPicture;
        編輯面板_圖片設定.transform.Find("圖片列表/滑動區塊/內容/" + TaskData.Picture.Count.ToString() + "/Picture/Text").GetComponent<TMPro.TMP_Text>().text = "Select a File";
        編輯面板_圖片設定.transform.Find("圖片列表/滑動區塊/內容/" + TaskData.Picture.Count.ToString() + "/UnselectPicture").GetComponent<Button>().interactable = false;
    }
    public void 更新圖片順序()
    {
        for (int i = 0; i < TaskData.Picture.Count; i++)
        {
            if(編輯面板_圖片設定.transform.Find("圖片列表/滑動區塊/內容/" + i.ToString() + "/Picture/Text").GetComponent<TMPro.TMP_Text>().text == "Select a File")
            {
                編輯面板_圖片設定.transform.Find("圖片列表/滑動區塊/內容/" + i.ToString() + "/Image").GetComponent<Image>().sprite = 編輯面板_圖片設定.transform.Find("圖片列表/滑動區塊/內容/" + (i + 1).ToString() + "/Image").GetComponent<Image>().sprite;
                編輯面板_圖片設定.transform.Find("圖片列表/滑動區塊/內容/" + i.ToString() + "/Picture/Text").GetComponent<TMPro.TMP_Text>().text = 編輯面板_圖片設定.transform.Find("圖片列表/滑動區塊/內容/" + (i + 1).ToString() + "/Picture/Text").GetComponent<TMPro.TMP_Text>().text;
                編輯面板_圖片設定.transform.Find("圖片列表/滑動區塊/內容/" + i.ToString() + "/UnselectPicture").GetComponent<Button>().interactable = true;
                編輯面板_圖片設定.transform.Find("圖片列表/滑動區塊/內容/" + (i + 1).ToString() + "/Image").GetComponent<Image>().sprite = NoPicture;
                編輯面板_圖片設定.transform.Find("圖片列表/滑動區塊/內容/" + (i + 1).ToString() + "/Picture/Text").GetComponent<TMPro.TMP_Text>().text = "Select a File";
                編輯面板_圖片設定.transform.Find("圖片列表/滑動區塊/內容/" + (i + 1).ToString() + "/UnselectPicture").GetComponent<Button>().interactable = false;
            }
            if(編輯面板_圖片設定.transform.Find("圖片列表/滑動區塊/內容/" + i.ToString() + "/Picture/Text").GetComponent<TMPro.TMP_Text>().text != "Select a File")
            {
                TaskData.Picture[i] = 編輯面板_圖片設定.transform.Find("圖片列表/滑動區塊/內容/" + i.ToString() + "/Image").GetComponent<Image>().sprite;
                TaskData.PictureFile[i] = i.ToString() + "_PF_" + 編輯面板_圖片設定.transform.Find("圖片列表/滑動區塊/內容/" + i.ToString() + "/Picture/Text").GetComponent<TMPro.TMP_Text>().text;
            }
        }
    }
    public void 更新圖片選項()
    {
        TMPro.TMP_Dropdown.OptionData NoPic = new TMPro.TMP_Dropdown.OptionData();
        NoPic.text = "None";
        NoPic.image = NoPicture;
        for (int i = 0; i < (NPC行數 + 1); i++)
        {
            try
            {
                int PicVal = 編輯面板_NPC設定.transform.Find("NPC列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().value;
                編輯面板_NPC設定.transform.Find("NPC列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().ClearOptions();
                編輯面板_NPC設定.transform.Find("NPC列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().options.Add(NoPic);
                for (int j = 0; j < TaskData.Picture.Count; j++)
                {
                    TMPro.TMP_Dropdown.OptionData data = new TMPro.TMP_Dropdown.OptionData();
                    string[] PF = TaskData.PictureFile[j].Split("_PF_");
                    data.text = PF[1];
                    data.image = TaskData.Picture[j];
                    編輯面板_NPC設定.transform.Find("NPC列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().options.Add(data);
                    編輯面板_NPC設定.transform.Find("NPC列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().RefreshShownValue();
                }
                編輯面板_NPC設定.transform.Find("NPC列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().value = PicVal;
            }
            catch { }
        }
        for (int i = 0; i < (文本行數 + 1); i++)
        {
            try
            {
                int PicVal = 編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().value;
                編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().ClearOptions();
                編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().options.Add(NoPic);
                for (int j = 0; j < TaskData.Picture.Count; j++)
                {
                    TMPro.TMP_Dropdown.OptionData data = new TMPro.TMP_Dropdown.OptionData();
                    string[] PF = TaskData.PictureFile[j].Split("_PF_");
                    data.text = PF[1];
                    data.image = TaskData.Picture[j];
                    編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().options.Add(data);
                    編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().RefreshShownValue();
                }
                編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().value = PicVal;
            }
            catch { }
        }
        for (int i = 0; i < (問題行數 + 1); i++)
        {
            try
            {
                int PicVal = 編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().value;
                編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().ClearOptions();
                編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().options.Add(NoPic);
                for (int j = 0; j < TaskData.Picture.Count; j++)
                {
                    TMPro.TMP_Dropdown.OptionData data = new TMPro.TMP_Dropdown.OptionData();
                    string[] PF = TaskData.PictureFile[j].Split("_PF_");
                    data.text = PF[1];
                    data.image = TaskData.Picture[j];
                    編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().options.Add(data);
                    編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().RefreshShownValue();
                }
                編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().value = PicVal;
            }
            catch { }
        }
    }
    #endregion
    //----------能力設定----------//
    public void 讀取門檻分數()
    {
        TaskData.Ability.Clear();
        TaskData.Ability.Add(Int32.Parse(編輯面板_能力設定.transform.Find("門檻/外向型").GetComponent<TMPro.TMP_InputField>().text));
        TaskData.Ability.Add(Int32.Parse(編輯面板_能力設定.transform.Find("門檻/親和型").GetComponent<TMPro.TMP_InputField>().text));
        TaskData.Ability.Add(Int32.Parse(編輯面板_能力設定.transform.Find("門檻/盡責型").GetComponent<TMPro.TMP_InputField>().text));
        TaskData.Ability.Add(Int32.Parse(編輯面板_能力設定.transform.Find("門檻/沉穩型").GetComponent<TMPro.TMP_InputField>().text));
        TaskData.Ability.Add(Int32.Parse(編輯面板_能力設定.transform.Find("門檻/開放型").GetComponent<TMPro.TMP_InputField>().text));
        TaskData.Ability.Add(Int32.Parse(編輯面板_能力設定.transform.Find("門檻/聽").GetComponent<TMPro.TMP_InputField>().text));
        TaskData.Ability.Add(Int32.Parse(編輯面板_能力設定.transform.Find("門檻/說").GetComponent<TMPro.TMP_InputField>().text));
        TaskData.Ability.Add(Int32.Parse(編輯面板_能力設定.transform.Find("門檻/讀").GetComponent<TMPro.TMP_InputField>().text));
        TaskData.Ability.Add(Int32.Parse(編輯面板_能力設定.transform.Find("門檻/寫").GetComponent<TMPro.TMP_InputField>().text));
        TaskData.Ability.Add(Int32.Parse(編輯面板_能力設定.transform.Find("加分/外向型").GetComponent<TMPro.TMP_InputField>().text));
        TaskData.Ability.Add(Int32.Parse(編輯面板_能力設定.transform.Find("加分/親和型").GetComponent<TMPro.TMP_InputField>().text));
        TaskData.Ability.Add(Int32.Parse(編輯面板_能力設定.transform.Find("加分/盡責型").GetComponent<TMPro.TMP_InputField>().text));
        TaskData.Ability.Add(Int32.Parse(編輯面板_能力設定.transform.Find("加分/沉穩型").GetComponent<TMPro.TMP_InputField>().text));
        TaskData.Ability.Add(Int32.Parse(編輯面板_能力設定.transform.Find("加分/開放型").GetComponent<TMPro.TMP_InputField>().text));
        TaskData.Ability.Add(Int32.Parse(編輯面板_能力設定.transform.Find("加分/聽").GetComponent<TMPro.TMP_InputField>().text));
        TaskData.Ability.Add(Int32.Parse(編輯面板_能力設定.transform.Find("加分/說").GetComponent<TMPro.TMP_InputField>().text));
        TaskData.Ability.Add(Int32.Parse(編輯面板_能力設定.transform.Find("加分/讀").GetComponent<TMPro.TMP_InputField>().text));
        TaskData.Ability.Add(Int32.Parse(編輯面板_能力設定.transform.Find("加分/寫").GetComponent<TMPro.TMP_InputField>().text));
    }

    #region 資料處理

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
    private void 將資料轉移至資料庫()
    {
        GameObject.Find("Canvas").SendMessage("Upload");
        清空TaskData();
    }
    private void 上傳資料()
    {
        存儲標題名稱();
        更新文本內容();
        更新NPC圖片();
        讀取門檻分數();
        將資料轉移至資料庫();
        if(TaskName更動)
        {
            GameObject.Find("Canvas").SendMessage("DeleteTask", 原Task名稱);
        }
    }
    public void 載入資料()
    {
        編輯面板_基本設定.transform.Find("滑動區塊遮罩/滑動區塊/標題/輸入框").GetComponent<TMPro.TMP_InputField>().text = TaskData.TaskName;
        編輯面板_基本設定.transform.Find("滑動區塊遮罩/滑動區塊/說明/輸入框").GetComponent<TMPro.TMP_InputField>().text = TaskData.TaskDescription;
        編輯面板_基本設定.transform.Find("滑動區塊遮罩/滑動區塊/答錯退出").GetComponent<Toggle>().isOn = TaskData.EIAW;
        if (TaskData.Background != null)
        {
            編輯面板_基本設定.transform.Find("滑動區塊遮罩/滑動區塊/背景/Image").GetComponent<Image>().sprite = TaskData.Background;
            編輯面板_基本設定.transform.Find("滑動區塊遮罩/滑動區塊/背景/UnselectPicture").GetComponent<Button>().interactable = true;
        }
        if (TaskData.Appearance != null)
        {
            編輯面板_基本設定.transform.Find("滑動區塊遮罩/滑動區塊/外觀觸發形象/Image").GetComponent<Image>().sprite = TaskData.Appearance;
            編輯面板_基本設定.transform.Find("滑動區塊遮罩/滑動區塊/外觀觸發形象/UnselectPicture").GetComponent<Button>().interactable = true;
        }
        NPC行數 = TaskData.NPCNumber;
        NPC列表.顯示列表(NPC行數);
        文本行數 = TaskData.TextNumber;
        文本列表.顯示列表(文本行數);
        圖片列表.顯示列表(TaskData.Picture.Count + 1);
        問題列表.顯示列表(問題行數);
        if (TaskData.Picture.Count > 0)
        {
            for (int i = 0; i < TaskData.Picture.Count; i++)
            {
                編輯面板_圖片設定.transform.Find("圖片列表/滑動區塊/內容/" + i.ToString() + "/Image").GetComponent<Image>().sprite = TaskData.Picture[i];
                string[] PF =  TaskData.PictureFile[i].Split("_PF_");
                編輯面板_圖片設定.transform.Find("圖片列表/滑動區塊/內容/" + i.ToString() + "/Picture/Text").GetComponent<TMPro.TMP_Text>().text = PF[1];
                編輯面板_圖片設定.transform.Find("圖片列表/滑動區塊/內容/" + i.ToString() + "/UnselectPicture").GetComponent<Button>().interactable = true;
            }
        }
        更新圖片順序();
        更新圖片選項();
        if (NPC行數 > 0)
        {
            for (int i = 0; i < NPC行數; i++)
            {
                編輯面板_NPC設定.transform.Find("NPC列表/滑動區塊/內容/" + i.ToString() + "/InputText").GetComponent<TMPro.TMP_InputField>().text = TaskData.NPCList[i + 1];
                編輯面板_NPC設定.transform.Find("NPC列表/滑動區塊/內容/" + i.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().value = TaskData.NPCPicture[i];
            }
        }
        更新NPC選項();
        if (文本行數 > 0)
        {
            int j = 0;
            for (int i = 0; i < TaskData.TextList.Count; i++)
            {
                if (TaskData.TextList[i] == "結尾") break;
                if (TaskData.TextList[i] == "無")
                {
                    編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + j.ToString() + "/NPC").GetComponent<TMPro.TMP_Dropdown>().value = 0;
                    i++;
                    編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + j.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().value = Int32.Parse(TaskData.TextList[i]);
                    i++;
                    編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + j.ToString() + "/InputText").GetComponent<TMPro.TMP_InputField>().text = TaskData.TextList[i];
                    j++;
                }
                else if (TaskData.TextList[i] == "選擇")
                {
                    問題行數++;
                    問題列表.顯示列表(問題行數);
                    編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + j.ToString() + "/NPC").GetComponent<TMPro.TMP_Dropdown>().value = NPC行數 + 1;
                    編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + j.ToString() + "/ToQuestion").gameObject.SetActive(true);
                    i++;
                    編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + (問題行數 - 1).ToString() + "/Answer1").GetComponent<TMPro.TMP_InputField>().text = TaskData.TextList[i];
                    i++;
                    編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + (問題行數 - 1).ToString() + "/Answer2").GetComponent<TMPro.TMP_InputField>().text = TaskData.TextList[i];
                    i++;
                    編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + (問題行數 - 1).ToString() + "/Answer3").GetComponent<TMPro.TMP_InputField>().text = TaskData.TextList[i];
                    i++;
                    if (TaskData.TextList[i] == "Answer1") 編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + (問題行數 - 1).ToString() + "/Answer1/Toggle").GetComponent<Toggle>().isOn = true;
                    else if (TaskData.TextList[i] == "Answer2") 編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + (問題行數 - 1).ToString() + "/Answer2/Toggle").GetComponent<Toggle>().isOn = true;
                    else if (TaskData.TextList[i] == "Answer3") 編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + (問題行數 - 1).ToString() + "/Answer3/Toggle").GetComponent<Toggle>().isOn = true;
                    i++;
                    編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + (問題行數 - 1).ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().value = Int32.Parse(TaskData.TextList[i]);
                    i++;
                    編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + (問題行數 - 1).ToString() + "/InputText").GetComponent<TMPro.TMP_InputField>().text = TaskData.TextList[i];
                    j++;
                }
                else if (TaskData.TextList[i] == "問答")
                {
                    問題行數++;
                    問題列表.顯示列表(問題行數);
                    編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + j.ToString() + "/NPC").GetComponent<TMPro.TMP_Dropdown>().value = NPC行數 + 2;
                    編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + j.ToString() + "/ToQuestion").gameObject.SetActive(true);
                    i++;
                    編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + (問題行數 - 1).ToString() + "/AnswerQA/InputField").GetComponent<TMPro.TMP_InputField>().text = TaskData.TextList[i];
                    i++;
                    if (TaskData.TextList[i] == "CheckAnswer") 編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + (問題行數 - 1).ToString() + "/AnswerQA/Toggle").GetComponent<Toggle>().isOn = true;
                    i++;
                    編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + (問題行數 - 1).ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().value = Int32.Parse(TaskData.TextList[i]);
                    i++;
                    編輯面板_問題設定.transform.Find("問題列表/滑動區塊/內容/" + (問題行數 - 1).ToString() + "/InputText").GetComponent<TMPro.TMP_InputField>().text = TaskData.TextList[i];
                    j++;
                }
                else
                {
                    編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + j.ToString() + "/NPC").GetComponent<TMPro.TMP_Dropdown>().value = Int32.Parse(TaskData.TextList[i].Split(new string[] { "NPC_" }, StringSplitOptions.RemoveEmptyEntries)[0]);
                    i++;
                    編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + j.ToString() + "/Picture").GetComponent<TMPro.TMP_Dropdown>().value = Int32.Parse(TaskData.TextList[i]);
                    i++;
                    編輯面板_文本設定.transform.Find("文本列表/滑動區塊/內容/" + j.ToString() + "/InputText").GetComponent<TMPro.TMP_InputField>().text = TaskData.TextList[i];
                    j++;
                }
            }
        }
        if (TaskData.Ability.Count == 18)
        {
            編輯面板_能力設定.transform.Find("門檻/外向型").GetComponent<TMPro.TMP_InputField>().text = TaskData.Ability[0].ToString();
            編輯面板_能力設定.transform.Find("門檻/親和型").GetComponent<TMPro.TMP_InputField>().text = TaskData.Ability[1].ToString();
            編輯面板_能力設定.transform.Find("門檻/盡責型").GetComponent<TMPro.TMP_InputField>().text = TaskData.Ability[2].ToString();
            編輯面板_能力設定.transform.Find("門檻/沉穩型").GetComponent<TMPro.TMP_InputField>().text = TaskData.Ability[3].ToString();
            編輯面板_能力設定.transform.Find("門檻/開放型").GetComponent<TMPro.TMP_InputField>().text = TaskData.Ability[4].ToString();
            編輯面板_能力設定.transform.Find("門檻/聽").GetComponent<TMPro.TMP_InputField>().text = TaskData.Ability[5].ToString();
            編輯面板_能力設定.transform.Find("門檻/說").GetComponent<TMPro.TMP_InputField>().text = TaskData.Ability[6].ToString();
            編輯面板_能力設定.transform.Find("門檻/讀").GetComponent<TMPro.TMP_InputField>().text = TaskData.Ability[7].ToString();
            編輯面板_能力設定.transform.Find("門檻/寫").GetComponent<TMPro.TMP_InputField>().text = TaskData.Ability[8].ToString();
            編輯面板_能力設定.transform.Find("加分/外向型").GetComponent<TMPro.TMP_InputField>().text = TaskData.Ability[9].ToString();
            編輯面板_能力設定.transform.Find("加分/親和型").GetComponent<TMPro.TMP_InputField>().text = TaskData.Ability[10].ToString();
            編輯面板_能力設定.transform.Find("加分/盡責型").GetComponent<TMPro.TMP_InputField>().text = TaskData.Ability[11].ToString();
            編輯面板_能力設定.transform.Find("加分/沉穩型").GetComponent<TMPro.TMP_InputField>().text = TaskData.Ability[12].ToString();
            編輯面板_能力設定.transform.Find("加分/開放型").GetComponent<TMPro.TMP_InputField>().text = TaskData.Ability[13].ToString();
            編輯面板_能力設定.transform.Find("加分/聽").GetComponent<TMPro.TMP_InputField>().text = TaskData.Ability[14].ToString();
            編輯面板_能力設定.transform.Find("加分/說").GetComponent<TMPro.TMP_InputField>().text = TaskData.Ability[15].ToString();
            編輯面板_能力設定.transform.Find("加分/讀").GetComponent<TMPro.TMP_InputField>().text = TaskData.Ability[16].ToString();
            編輯面板_能力設定.transform.Find("加分/寫").GetComponent<TMPro.TMP_InputField>().text = TaskData.Ability[17].ToString();
        }
        else
        {
            讀取門檻分數();
        }
    }
    #endregion
}
