using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Paragraph : MonoBehaviour
{
    [SerializeField] PlayerData PlayerData;
    [SerializeField] ParaData ParaData;
    [SerializeField] ArticleData ArticleData;
    private TextMeshProUGUI TextMesh;
    private Speechlib Speechlib;
    public Text AudioNum;
    public GameObject EditAns;
    public TextAsset textFile;
    public List<string> textList = new List<string>();
    public GameObject AudioPlayBtn;
    public int index = 0;
    public int num;
    int count;
    string keyword;
    string paragraph;
    string colorChange;
    bool audiofininsh = true;
    void Start()
    {
        TextMesh = GetComponent<TextMeshProUGUI>();
        Speechlib = GetComponent<Speechlib>();

        GetTextFromFile(textFile);
        SetParagraph();
        ArticleData.ArticleBefore = paragraph;
        ChangeColor();
        TextMesh.text = paragraph;
        num = index+1;
        AudioNum.text = num.ToString();
    }

    public void BacktoLobby()
    {
        GameObject.Find("Canvas").SendMessage("LoadLevel", "Lobby");
    }
    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        count = 0;

        var lineData = file.text.Split('/');
        foreach (var line in lineData)
        {
            textList.Add(line);
            count = count + 1;
        }
    }

    void SetParagraph()
    {
        for (int i = 0; i < count; i++)
        {
            keyword = "#" + (i+1);
            textList[i] = textList[i].Replace(keyword,ParaData.ParaStorage[i]);
            paragraph = paragraph + textList[i];
        }
    }

    void ChangeColor()
    {
        paragraph = "";
        for (int i = 0; i < count; i++)
        {
            keyword = "<color=yellow>" + ParaData.ParaStorage[i] +  "(" + (i+1) + ")" + "</color>";
            textList[i] = textList[i].Replace(ParaData.ParaStorage[i],keyword);
            paragraph = paragraph + textList[i];
        }
    }

    public void ConfirmParagraph()
    {
        GetTextFromFile(textFile);
        SetParagraph();
        ChangeColor();
        TextMesh.text = paragraph;
    }

    public void OutputParagraph()
    {
        paragraph = "";
        GetTextFromFile(textFile);
        SetParagraph();
        ArticleData.ArticleAfter = paragraph;
        TextMesh.text = paragraph;
        EditAns.SetActive(false);
        Sentence();
        AudioPlayBtn.SetActive(true);
    }

    void Sentence()
    {
        textList.Clear();
        count = 0;

        var lineData = ArticleData.ArticleAfter.Split('.');
        foreach (var line in lineData)
        {
            textList.Add(line);
            count = count + 1;
        }  
        for (int i = 0; i < count-1; i++)
        {
            textList[i] = textList[i] + ".";
        }
    }

    public void AudioPlay()
    {
        if(audiofininsh)
        {
            audiofininsh = false;
            Speechlib.Speech(textList[index]);
            StartCoroutine(SetTextColor());
        }
    }

    public void AudioPlayAll()
    {
        if(audiofininsh)
        {
            audiofininsh = false;
            StartCoroutine(SetAllTextColor());
        }
    }

    public IEnumerator SetTextColor()
    {
        string begin = "<color=blue>";
        string end = "</color>";
        for (int i = 0; i < textList[index].Length; i++)
        {
            colorChange = textList[index];
            colorChange = colorChange.Insert(0,begin);
            colorChange = colorChange.Insert(13+i,end);
            TextMesh.text = paragraph.Replace(textList[index],colorChange);
            if(index==3)
            {
                yield return new WaitForSeconds(0.085f);
            }
            else
            {
                yield return new WaitForSeconds(0.06f);
            }
        }
        audiofininsh = true;
        TextMesh.text = ArticleData.ArticleAfter;
    }

    public IEnumerator SetAllTextColor()
    {
        string paragraph1 = ArticleData.ArticleAfter;
        string begin = "<color=blue>";
        string end = "</color>";
        for (int j = 0; j < count-1; j++)
        {
            Speechlib.Speech(textList[j]);
            for (int i = 0; i < textList[j].Length; i++)
            {
                colorChange = textList[j];
                colorChange = colorChange.Insert(0,begin);
                colorChange = colorChange.Insert(13+i,end);
                TextMesh.text = paragraph1.Replace(textList[j],colorChange);
                if(j==3)
                {
                    yield return new WaitForSeconds(0.085f);
                }
                else
                {
                    yield return new WaitForSeconds(0.06f);
                }
            }
            paragraph1 = TextMesh.text;
            if(j==3)
            {
                yield return new WaitForSeconds(1.2f);
            }
            else
            {
                yield return new WaitForSeconds(0.9f);
            }
        }
        audiofininsh = true;
        TextMesh.text = ArticleData.ArticleAfter;
    }

    public void ArrowUpBtn()
    {
        if(audiofininsh)
        {
            if(index < 17)
        {
            index++;
        }
        else 
        {
            index = 0;
        }
        num = index+1;
        AudioNum.text = num.ToString();
        SetSentenceColor(index);
        }
    }

    public void ArrowDownBtn()
    {
        if(audiofininsh)
        {
            if(index > 0)
            {
                index--;
            }
            else 
            {
                index = 17;
            }
            num = index+1;
            AudioNum.text = num.ToString();
            SetSentenceColor(index);
        }
    }

    void SetSentenceColor(int j)
    {
        TextMesh.text = "";
        paragraph = "";
        List<string> textList1 = new List<string>();
        for (int i = 0; i < count; i++)
        {
            textList1.Add(textList[i]);
        }
        textList1[j] = "<color=yellow>" + textList[j] + "</color>";
        for (int i = 0; i < count-1; i++)
        {
            paragraph = paragraph + textList1[i];
        }
        TextMesh.text = paragraph;
    }

    public void SaveData()
    {
        PlayerData.playerName = ParaData.ParaStorage[0];
        PlayerData.color = ParaData.ParaStorage[16];
    }
}