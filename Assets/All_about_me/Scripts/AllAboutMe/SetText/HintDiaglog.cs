using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintDiaglog : MonoBehaviour
{
    private AzureSpeechToText Azure;
    private AzureTextToSpeech Azure1;
    private Speechlib Speechlib;
    private IntroDiaglog Intro;
    private QPlay QPlay;
    private rasa rasa;
    private Dropdown0 dropdown0;
    private Dropdown1 dropdown1;
    private Dropdown2 dropdown2;
    public GameObject panel;
    public GameObject SelectAns;
    public Text textLable;
    public Text AnswerText;
    public TextAsset textFile;
    public int index;
    List<string> textList = new List<string>();
    public float TextSpeed;
    public bool textFininshed;
    public int warningTimes;
    void Awake()
    {
        GetTextFromFile(textFile);//讀取文本內容
        index = 0;
        warningTimes = 0;
        rasa = GetComponent<rasa>();
        Azure = GetComponent<AzureSpeechToText>();
        Azure1 = GetComponent<AzureTextToSpeech>();
        Speechlib = GetComponent<Speechlib>();
        Intro = GetComponent<IntroDiaglog>();
        dropdown0 = GetComponent<Dropdown0>();
        dropdown1 = GetComponent<Dropdown1>();
        dropdown2 = GetComponent<Dropdown2>();
        QPlay = GetComponent<QPlay>();
        QPlay.BtnClick();
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

    public void NextButtonClick()
    {
        if(textFininshed && rasa.ansCheck && index != textList.Count)
        {
            StartCoroutine(SetText());
            QPlay.index++;
            QPlay.BtnClick();
            warningTimes = 0;
            SelectAns.SetActive(false);
            dropdown0.DropdowmClear();
            dropdown1.DropdowmClear();
            dropdown2.DropdowmClear();
        }
    }

    public void BackButtonClick()
    {
        if(textFininshed && index != 1 && !QPlay.AudioSource1.isPlaying)
        {
            index = index-2;
            Intro.BackButtonClick();
            StartCoroutine(SetText());
            QPlay.index--;
            QPlay.BtnClick();
            warningTimes = 0;
            dropdown0.DropdowmClear();
            dropdown1.DropdowmClear();
            dropdown2.DropdowmClear();
        }
    }

    public void AnswerPlay()
    {
        Speechlib.Speech(AnswerText.text);
    }

    public void OkButton()
    {
        panel.SetActive(false);
        warningTimes++;
        if(warningTimes >= 2 )
        {
            SelectAns.SetActive(true);
        }
        if(warningTimes == 2)
        {
            dropdown0.AddOptions();
            dropdown1.AddOptions();
            dropdown1.SetAct();
            dropdown2.AddOptions();
            dropdown2.SetAct();
        }
    }
}
