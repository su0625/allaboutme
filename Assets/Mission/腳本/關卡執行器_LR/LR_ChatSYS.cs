using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;
using System.Reflection;

public class LR_ChatSYS : MonoBehaviour
{

    [SerializeField] TaskData TaskData;

    [Header("文本設定")]
    public float 文字速度 = 0.05f;

    [Header("UI組件")]
    private GameObject 對話框系統;
    private GameObject 對話框;
    private TMP_Text 對話框文字;
    private GameObject 對話人物;
    private GameObject 對話圖片;
    private GameObject 選擇區塊;
    private GameObject 填空區塊;
    private GameObject 人物名稱;
    private GameObject 結算面板;

    [Header("選擇框1")]
    private GameObject 選項一;
    private GameObject 選項二;
    private GameObject 選項三;
    private TMP_Text 選項一文字;
    private TMP_Text 選項二文字;
    private TMP_Text 選項三文字;

    [Header("對話框圖片")]
    public Sprite 左框;
    public Sprite 無框;

    int index;
    List<string> textList = new List<string>();
    bool 正在選擇中;
    bool 正在填寫中;
    bool 答案判定中;
    enum 選擇題狀態
    {
        未選擇,
        選擇一,
        選擇二,
        選擇三
    }
    private 選擇題狀態 選擇狀態 = 選擇題狀態.未選擇;
    private string 正確答案 = "";
    private string 填空答案 = "";
    bool textFinished;
    bool cancelTyping;
    bool click;
    bool WrongAnswer = false;

    void Awake()
    {
        textList = TaskData.TextList.ToList();
        對話框系統 = this.gameObject;
        對話框 = GameObject.Find("對話框");
        對話框文字 = GameObject.Find("對話文字").GetComponent<TMPro.TMP_Text>();
        對話人物 = GameObject.Find("對話人物");
        對話圖片 = GameObject.Find("對話圖片");
        選擇區塊 = GameObject.Find("選擇區塊");
        選項一 = GameObject.Find("選項一");
        選項二 = GameObject.Find("選項二");
        選項三 = GameObject.Find("選項三");
        選項一文字 = GameObject.Find("選項一").transform.Find("Text").GetComponent<TMPro.TMP_Text>();
        選項二文字 = GameObject.Find("選項二").transform.Find("Text").GetComponent<TMPro.TMP_Text>();
        選項三文字 = GameObject.Find("選項三").transform.Find("Text").GetComponent<TMPro.TMP_Text>();
        填空區塊 = GameObject.Find("填空區塊");
        人物名稱 = GameObject.Find("人物名稱");
        結算面板 = GameObject.Find("結算面板");
        對話圖片.SetActive(false);
        選擇區塊.SetActive(false);
        填空區塊.SetActive(false);
        結算面板.SetActive(false);
    }
    private void OnEnable()
    {
        textFinished = true;
        StartCoroutine(setTextUI());
    }
    void Start()
    {
        if (TaskData.Background != null)
        {
            對話框系統.GetComponent<Image>().sprite = TaskData.Background;
        }
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || click) && WrongAnswer == true)
        {
            gameObject.SetActive(false);
            index = 0;
            開啟結算面板("Fail");
            return;
        }
        else if ((Input.GetKeyDown(KeyCode.Space) || click) && index == textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        else if (Input.GetKeyDown(KeyCode.Space) || click)
        {
            if (textFinished && !cancelTyping)
            {
                StartCoroutine(setTextUI());
            }
            else if (!textFinished && !cancelTyping)
            {
                cancelTyping = true;
            }
        }
        click = false;
    }
    IEnumerator setTextUI()
    {
        textFinished = false;
        對話框文字.text = "";
        選擇區塊.SetActive(false);
        填空區塊.SetActive(false);
        //判斷答案
        檢查正確答案();
        //判斷NPC、選擇題與結尾
        switch (textList[index])
        {
            case "無":
                正在選擇中 = false;
                對話人物.SetActive(false);
                對話框.GetComponent<Image>().sprite = 無框;
                人物名稱.GetComponent<TMP_Text>().text = "";
                index++;
                break;
            case "NPC_1":
                正在選擇中 = false;
                if (TaskData.NPCPicture[0] != 0)
                {
                    對話人物.GetComponent<Image>().sprite = TaskData.Picture[TaskData.NPCPicture[0] - 1];
                    對話人物.SetActive(true);
                }
                else
                {
                    對話人物.SetActive(false);
                }
                對話框.GetComponent<Image>().sprite = 左框;
                人物名稱.GetComponent<TMP_Text>().text = TaskData.NPCList[1];
                index++;
                break;
            case "NPC_2":
                正在選擇中 = false;
                if (TaskData.NPCPicture[1] != 0)
                {
                    對話人物.GetComponent<Image>().sprite = TaskData.Picture[TaskData.NPCPicture[1] - 1];
                    對話人物.SetActive(true);
                }
                else
                {
                    對話人物.SetActive(false);
                }
                對話框.GetComponent<Image>().sprite = 左框;
                人物名稱.GetComponent<TMP_Text>().text = TaskData.NPCList[2];
                index++;
                break;
            case "NPC_3":
                正在選擇中 = false;
                if (TaskData.NPCPicture[2] != 0)
                {
                    對話人物.GetComponent<Image>().sprite = TaskData.Picture[TaskData.NPCPicture[2] - 1];
                    對話人物.SetActive(true);
                }
                else
                {
                    對話人物.SetActive(false);
                }
                對話框.GetComponent<Image>().sprite = 左框;
                人物名稱.GetComponent<TMP_Text>().text = TaskData.NPCList[3];
                index++;
                break;
            case "NPC_4":
                正在選擇中 = false;
                if (TaskData.NPCPicture[3] != 0)
                {
                    對話人物.GetComponent<Image>().sprite = TaskData.Picture[TaskData.NPCPicture[3] - 1];
                    對話人物.SetActive(true);
                }
                else
                {
                    對話人物.SetActive(false);
                }
                對話框.GetComponent<Image>().sprite = 左框;
                人物名稱.GetComponent<TMP_Text>().text = TaskData.NPCList[4];
                index++;
                break;
            case "NPC_5":
                正在選擇中 = false;
                if (TaskData.NPCPicture[4] != 0)
                {
                    對話人物.GetComponent<Image>().sprite = TaskData.Picture[TaskData.NPCPicture[4] - 1];
                    對話人物.SetActive(true);
                }
                else
                {
                    對話人物.SetActive(false);
                }
                對話框.GetComponent<Image>().sprite = 左框;
                人物名稱.GetComponent<TMP_Text>().text = TaskData.NPCList[5];
                index++;
                break;
            case "NPC_6":
                正在選擇中 = false;
                if (TaskData.NPCPicture[5] != 0)
                {
                    對話人物.GetComponent<Image>().sprite = TaskData.Picture[TaskData.NPCPicture[5] - 1];
                    對話人物.SetActive(true);
                }
                else
                {
                    對話人物.SetActive(false);
                }
                對話框.GetComponent<Image>().sprite = 左框;
                人物名稱.GetComponent<TMP_Text>().text = TaskData.NPCList[6];
                index++;
                break;
            case "NPC_7":
                正在選擇中 = false;
                if (TaskData.NPCPicture[6] != 0)
                {
                    對話人物.GetComponent<Image>().sprite = TaskData.Picture[TaskData.NPCPicture[6] - 1];
                    對話人物.SetActive(true);
                }
                else
                {
                    對話人物.SetActive(false);
                }
                對話框.GetComponent<Image>().sprite = 左框;
                人物名稱.GetComponent<TMP_Text>().text = TaskData.NPCList[7];
                index++;
                break;
            case "NPC_8":
                正在選擇中 = false;
                if (TaskData.NPCPicture[7] != 0)
                {
                    對話人物.GetComponent<Image>().sprite = TaskData.Picture[TaskData.NPCPicture[7] - 1];
                    對話人物.SetActive(true);
                }
                else
                {
                    對話人物.SetActive(false);
                }
                對話框.GetComponent<Image>().sprite = 左框;
                人物名稱.GetComponent<TMP_Text>().text = TaskData.NPCList[8];
                index++;
                break;
            case "選擇":
                選擇區塊.SetActive(true);
                正在選擇中 = true;
                選擇狀態 = 選擇題狀態.未選擇;
                對話框.GetComponent<Image>().sprite = 無框;
                人物名稱.GetComponent<TMP_Text>().text = "";
                index++;
                if (textList[index] != "")選項一文字.text = textList[index];
                選項一.SetActive(true);
                index++;
                if (textList[index] != "")選項二文字.text = textList[index];
                選項二.SetActive(true);
                index++;
                if (textList[index] != "")
                {
                    選項三.SetActive(true);
                    選項三文字.text = textList[index];
                }
                else 選項三.SetActive(false);
                index++;
                正確答案 = textList[index];
                index++;
                break;
            case "問答":
                填空區塊.SetActive(true);
                正在填寫中 = true;
                選擇狀態 = 選擇題狀態.未選擇;
                對話框.GetComponent<Image>().sprite = 無框;
                人物名稱.GetComponent<TMP_Text>().text = "";
                index++;
                填空答案 = textList[index];
                index++;
                正確答案 = textList[index];
                index++;
                break;
            case "結尾":
                index++;
                開啟結算面板("Quit");
                break;
        }
        //判斷圖片
        if(textList[index] != "0")
        {
            對話圖片.GetComponent<Image>().sprite = TaskData.Picture[Int32.Parse(textList[index]) - 1];
            對話圖片.SetActive(true);
            index++;
        }
        else
        {
            對話圖片.SetActive(false);
            index++;
        }
        //顯示文字
        int letter = 0;
        while (!cancelTyping && letter < textList[index].Length - 1)
        {
            對話框文字.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(文字速度);
        }
        對話框文字.text = textList[index];
        cancelTyping = false;
        textFinished = true;
        index++;
    }
    public void ClickDown()
    {
        if (正在選擇中)
        {
            if (選擇狀態 != 選擇題狀態.未選擇)
            {
                click = true;
                正在選擇中 = false;
                答案判定中 = true;
            }
        }
        else if (正在填寫中)
        {
            if (填空區塊.transform.Find("InputField").GetComponent<TMPro.TMP_InputField>().text != "")
            {
                click = true;
                正在填寫中 = false;
                答案判定中 = true;
            }
        }
        else click = true;
    }
    public void 選擇題按鈕(int count)
    {
        switch (count)
        {
            case 1:
                選擇狀態 = 選擇題狀態.選擇一;
                break;
            case 2:
                選擇狀態 = 選擇題狀態.選擇二;
                break;
            case 3:
                選擇狀態 = 選擇題狀態.選擇三;
                break;
            default:
                選擇狀態 = 選擇題狀態.未選擇;
                break;
        }
    }
    private void 檢查正確答案()
    {
        if (答案判定中)
        {
            對話框.GetComponent<Image>().sprite = 無框;
            人物名稱.GetComponent<TMP_Text>().text = "";
            if (正確答案 == "Answer1" && 選擇狀態 == 選擇題狀態.選擇一)
            {
                textList.Insert(index, "(The answer was correct.)");
                textList.Insert(index, "0");
                WrongAnswer = false;
            }
            else if (正確答案 == "Answer2" && 選擇狀態 == 選擇題狀態.選擇二)
            {
                textList.Insert(index, "(The answer was correct.)");
                textList.Insert(index, "0");
                WrongAnswer = false;
            }
            else if (正確答案 == "Answer3" && 選擇狀態 == 選擇題狀態.選擇三)
            {
                textList.Insert(index, "(The answer was correct.)");
                textList.Insert(index, "0");
                WrongAnswer = false;
            }
            else if (正確答案 == "CheckAnswer")
            {
                if (檢查填空答案())
                {
                    textList.Insert(index, "(The answer was correct.)");
                    textList.Insert(index, "0");
                    WrongAnswer = false;
                }
                else
                {
                    textList.Insert(index, "(Wrong answer, you will quit the Task.)");
                    textList.Insert(index, "0");
                    WrongAnswer = true;
                }
            }
            else if (正確答案 == "NoAnswer")
            {
                textList.Insert(index, "(Great answer.)");
                textList.Insert(index, "0");
                WrongAnswer = false;
            }
            else
            {
                textList.Insert(index, "(Wrong answer, you will quit the Task.)");
                textList.Insert(index, "0");
                WrongAnswer = true;
            }
            答案判定中 = false;
        }
    }
    private bool 檢查填空答案()
    {
        var lineData = 填空答案.Split('\n');
        foreach (var line in lineData)
        {
            if(填空區塊.transform.Find("InputField").GetComponent<TMPro.TMP_InputField>().text == line)
            {
                return true;
            }
        }
        return false;
    }
    private void 開啟結算面板(string mode)
    {
        結算面板.SetActive(true);
        結算面板.transform.Find("面板/TaskName").GetComponent<TMPro.TMP_Text>().text = TaskData.TaskName;
        if (mode == "Fail")
        {
            結算面板.transform.Find("面板/標題").GetComponent<TMPro.TMP_Text>().text = "You failed, you can try again later.";
            結算面板.transform.Find("面板/AbilityInformation").GetComponent<TMPro.TMP_Text>().color = new Color(255,0,0);
        }
        if (mode == "Quit")
        {
            結算面板.transform.Find("面板/標題").GetComponent<TMPro.TMP_Text>().text = "You complete the Task. Well done.";
            結算面板.transform.Find("面板/AbilityInformation").GetComponent<TMPro.TMP_Text>().color = new Color(0, 150, 0);
        }
    }
    public void 退出任務()
    {
        GameObject.Find("Canvas").SendMessage("LoadLevel", "LevelTest");
    }
}
