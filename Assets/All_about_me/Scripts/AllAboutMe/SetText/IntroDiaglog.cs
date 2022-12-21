using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroDiaglog : MonoBehaviour
{
    private AzureSpeechToText Azure;
    private Scores Scores;
    private rasa rasa;
    public Text Qnum;
    public Text textLable;
    public TextAsset textFile;
    public GameObject CompletePanel;
    public int index;
    public int num;
    List<string> textList = new List<string>();
    public float TextSpeed;
    public bool textFininshed;
    string storeAnswer;
    [SerializeField] AnsData AnsData;
    [SerializeField] ParaData ParaData;
    [SerializeField] PlayerData PlayerData;
    
    void Awake()
    {
        GetTextFromFile(textFile);//讀取文本內容
        QnumSet();
        index = 0;
        rasa = GetComponent<rasa>();
        Scores = GetComponent<Scores>();
        Azure = GetComponent<AzureSpeechToText>();
    }
    private void OnEnable() 
    {  
        StartCoroutine(SetText());
    }

    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var lineData = file.text.Split('\n');//將文本按行切割
        foreach (var line in lineData)
        {
            textList.Add(line);
        }//把每行文字加到文字框

    }
    public IEnumerator SetText()
    {
        textFininshed = false;
        textLable.text = ""; //將文字框清空
        for (int i = 0; i < textList[index].Length; i++)
        {
            textLable.text += textList[index][i];//將文字依序輸出
            yield return new WaitForSeconds(TextSpeed);
        }
        textFininshed = true;
        index++;
        
    }

    public void NextClick()
    {
        if(index == textList.Count && rasa.ansCheck)
        {
            AnsSave();
            // CompletePanel.SetActive(true);
            GameObject.Find("Canvas").SendMessage("LoadLevel", "Paragraph"); 
        }
        else if(textFininshed && rasa.ansCheck && index != textList.Count)
        {
            AnsSave();
            if(index != textList.Count)
            {
                StartCoroutine(SetText());
            }
            QnumSet();
            Azure.message = "Click button to answer the question.";
        }
        
    }

    public void BackButtonClick()
    {
        if(textFininshed && index != 1)
        {
            index = index-2;
            StartCoroutine(SetText());
            QnumSet();
        }
    }

    public void AnsSave()
    {
        storeAnswer = Azure.message;
        AnsData.AnsStorage[index-1] = storeAnswer;
    }

    void QnumSet()
    {
        num = index+1;
        Qnum.text = "Question " + num.ToString() + ":";
        PlayerPrefs.SetString("Qnum",num.ToString());
    }
}