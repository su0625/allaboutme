using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskData", menuName = "Data/TaskData")]
public class TaskData : ScriptableObject
{
    public string TaskName;
    [TextArea(2, 5)]
    public string TaskDescription;
    public Sprite Background;
    public Sprite Appearance; //新增的東西 - 目前無用處
    public bool EIAW;//目前無用處
    public int NPCNumber = 0;
    public List<string> NPCList = new List<string>();
    public List<int> NPCPicture = new List<int>(); //更換資料形式
    public int TextNumber = 0;
    public List<string> TextList = new List<string>();
    public List<Sprite> Picture = new List<Sprite>(); //新增的東西
    public List<string> PictureFile = new List<string>(); //新增的東西
    public List<int> Ability = new List<int>(); //新增的東西 - 還在想要怎麼處理 - 跟雷達圖相關
    /*
    public string BackgroundFile; //待刪除
    public List<string> NPCPictureFile = new List<string>(); //待刪除
    public List<Sprite> TextPicture = new List<Sprite>(); //待刪除
    public List<string> TextPictureFile = new List<string>(); //待刪除
    */
}

