using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class TaskDataController : MonoBehaviour
{
    public bool isConnecting = false;
    public bool isConnectFail = false;

    [SerializeField] TaskData TaskData;
    private string background_base64;
    private string appearance_base64;
    private string npclist = "";
    List<int> npcpicture = new List<int>();
    private string textlist = "";
    List<string> picture = new List<string>();

    private string picturefile = "";
    List<int> ability = new List<int>();

    public void ShowTask()
    {
        isConnecting = true;
        StartCoroutine(ShowallTaskName());
    }
    public void DeleteTask(string TaskName)
    {
        isConnecting = true;
        StartCoroutine(DeleteTaskData(TaskName));
    }
    public void Download(string TaskName)
    {
        isConnecting = true;
        TaskName = TaskName.Trim();
        StartCoroutine(DownloadFromSQL(TaskName));
    }
    public void Upload()
    {
        isConnecting = true;
        var taskName = TaskData.TaskName;
        var taskDescription = TaskData.TaskDescription;

        Sprite background = TaskData.Background;
        if (background == null)
        {
            background_base64 = "None";
        }
        else
        {
            Texture2D background_texture = background.texture;
            byte[] backgroundbytes = background_texture.EncodeToJPG();
            background_base64 = Convert.ToBase64String(backgroundbytes);
        }

        Sprite appearance = TaskData.Appearance;
        if (appearance == null)
        {
            appearance_base64 = "None";
        }
        else
        {
            Texture2D appearance_texture = appearance.texture;
            byte[] appearancebytes = appearance_texture.EncodeToJPG();
            appearance_base64 = Convert.ToBase64String(appearancebytes);
        }

        var eiaw = TaskData.EIAW;

        var npcnumber = TaskData.NPCNumber;

        var npclist_num = (TaskData.NPCList).Count;
        if (npclist_num > 0)
        {
            for (int i = 0; i < npclist_num; i++)
            {
                npclist += TaskData.NPCList[i] + "/";
            }
            npclist = npclist.Remove(npclist.LastIndexOf("/"), 1);
        }
        else
        {
            npclist = "None";
        }

        var npcpicture_num = (TaskData.NPCPicture).Count;
        if (npcpicture_num > 0)
        {
            for (int i = 0; i < npcpicture_num; i++)
            {
                npcpicture.Add(TaskData.NPCPicture[i]);
            }
        }

        var textnumber = TaskData.TextNumber;

        var textlist_num = (TaskData.TextList).Count;
        if (textlist_num > 0)
        {
            for (int i = 0; i < textlist_num; i++)
            {
                if (TaskData.TextList[i] == null || TaskData.TextList[i] == "")
                {
                    textlist += "None" + "/";
                }
                else
                {
                    textlist += TaskData.TextList[i] + "/";
                }
            }
            textlist = textlist.Remove(textlist.LastIndexOf("/"), 1);
        }
        else
        {
            textlist = "None";
        }

        var picture_num = (TaskData.Picture).Count;
        if (picture_num > 0)
        {
            for (int i = 0; i < picture_num; i++)
            {
                if (TaskData.Picture[i] == null)
                {
                    picture.Add("None");
                }
                else
                {
                    Sprite picture_temp = TaskData.Picture[i];
                    Texture2D picture_texture = picture_temp.texture;
                    byte[] picture_bytes = picture_texture.EncodeToJPG();
                    string picture_base64 = Convert.ToBase64String(picture_bytes);
                    picture.Add(picture_base64);
                }
            }
        }
        else
        {
            picture.Add("None");
        }

        var picturefile_num = (TaskData.PictureFile).Count;
        if (picturefile_num > 0)
        {
            for (int i = 0; i < picturefile_num; i++)
            {
                if (TaskData.PictureFile[i] == null)
                {
                    picturefile += "None" + "/";
                }
                else
                {
                    picturefile += TaskData.PictureFile[i] + "/";
                }
            }
            picturefile = picturefile.Remove(picturefile.LastIndexOf("/"), 1);
        }
        else
        {
            picturefile = "None";
        }

        var ability_num = (TaskData.Ability).Count;
        if (ability_num > 0)
        {
            for (int i = 0; i < ability_num; i++)
            {
                ability.Add(TaskData.Ability[i]);
            }
        }
        // Debug.Log(taskName);
        // Debug.Log(taskDescription);
        // Debug.Log(background_base64);
        // Debug.Log(appearance_base64);
        // Debug.Log(eiaw);
        // Debug.Log(npcnumber);
        // Debug.Log(npclist);
        // foreach( var x in npcpicture) {
        //     Debug.Log(x);
        // }
        // Debug.Log(textnumber);
        // Debug.Log(textlist);
        // foreach( var x in picture) {
        //     Debug.Log( x.ToString());
        // }
        // Debug.Log(picturefile);

        // foreach( var x in ability) {
        //     Debug.Log(x.ToString());
        // }

        StartCoroutine(DataToSQL(taskName, taskDescription, background_base64, appearance_base64, eiaw, npcnumber, npclist, npcpicture, textnumber, textlist, picture, picturefile, ability));
    }
    public class BypassCertificate : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            //Simply return true no matter what
            return true;
        }
    }

    public IEnumerator ShowallTaskName()
    {
        WWWForm form1 = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Post("https://140.125.32.129:5000/getTaskData_TaskName", form1))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                var status = www.downloadHandler.text;
                // Debug.Log(status);
                string[] charsRemove = new string[] { "(", ",)", "'", "[", "]" };
                foreach (var c in charsRemove)
                {
                    status = status.Replace(c, string.Empty);
                }
                string[] subs = status.Split(",");
                var listsubs = new List<string>();
                foreach (string sub in subs)
                {
                    listsubs.Add(sub);
                }

                if (status == "getTaskData_TaskName error")
                {
                    Debug.Log("獲取 TaskData_TaskName 失敗");
                }
                else
                {
                    //輸出所有TaskName
                    GameObject.Find("Canvas").SendMessage("顯示現有關卡", listsubs);
                    isConnecting = false;
                }
            }
        }
    }
    public IEnumerator DeleteTaskData(string taskName)
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("taskname", taskName);

        using (UnityWebRequest www = UnityWebRequest.Post("https://140.125.32.129:5000/Delete_TaskData", form1))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                var status = www.downloadHandler.text;
                // Debug.Log(status);
                if (status == "Delete TaskData success")
                {
                    Debug.LogFormat("Delete TaskData  {0} 成功", taskName);
                    isConnecting = false;
                }
                else
                {
                    Debug.LogFormat("{0} ", status);
                }
            }
        }
    }
    public IEnumerator DownloadFromSQL(string taskName)
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("taskname", taskName);

        using (UnityWebRequest www = UnityWebRequest.Post("https://140.125.32.129:5000/getTaskData", form1))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                var status = www.downloadHandler.text;
                // Debug.Log(status);
                string[] charsRemove = new string[] { "(", ")", "'" };
                foreach (var c in charsRemove)
                {
                    status = status.Replace(c, string.Empty);
                }
                string[] subs = status.Split(",");
                var listsubs = new List<string>();
                foreach (string sub in subs)
                {
                    listsubs.Add(sub);
                }

                if (status == "getTaskData error  ")
                {
                    Debug.Log("獲取 TaskData 失敗" + status);
                }
                else
                {
                    // Debug.Log(listsubs[0]);
                    TaskData.TaskName = listsubs[0].Trim(); ;
                    // Debug.Log(listsubs[1]);
                    TaskData.TaskDescription = listsubs[1].Trim(); ;

                    // Debug.Log(listsubs[2]);
                    if (listsubs[2] != " None")
                    {
                        byte[] backgroundBytes = Convert.FromBase64String(listsubs[2]);
                        Texture2D background_text = new Texture2D(10, 10);
                        background_text.LoadImage(backgroundBytes);
                        Sprite background = Sprite.Create(background_text, new Rect(0.0f, 0.0f, background_text.width, background_text.height), new Vector2(0.5f, 0.5f), 100.0f);
                        TaskData.Background = background;
                    }

                    if (listsubs[3] != " None")
                    {
                        byte[] appearanceBytes = Convert.FromBase64String(listsubs[3]);
                        Texture2D appearance_text = new Texture2D(10, 10);
                        appearance_text.LoadImage(appearanceBytes);
                        Sprite appearance = Sprite.Create(appearance_text, new Rect(0.0f, 0.0f, appearance_text.width, appearance_text.height), new Vector2(0.5f, 0.5f), 100.0f);
                        TaskData.Appearance = appearance;
                    }

                    TaskData.EIAW = Convert.ToBoolean(listsubs[4]);
                    TaskData.NPCNumber = int.Parse(listsubs[5].Trim());

                    string[] npclist_temp = listsubs[6].Split("/");
                    if (npclist_temp.Length > 0)
                    {
                        for (int i = 0; i < npclist_temp.Length; i++)
                        {
                            var temp = npclist_temp[i].Trim();
                            TaskData.NPCList.Add(temp);
                        }
                    }

                    // Debug.Log(listsubs[7]);
                    if (listsubs[7] == " ")
                    {
                    }
                    else
                    {
                        string[] npcpicture_temp = listsubs[7].Split("---");
                        for (int i = 0; i < npcpicture_temp.Length; i++)
                        {
                            TaskData.NPCPicture.Add(int.Parse(npcpicture_temp[i].Trim()));
                        }
                    }

                    TaskData.TextNumber = int.Parse(listsubs[8].Trim());

                    // Debug.Log(listsubs[10]);
                    string[] textlist_temp = listsubs[9].Split("/");
                    if (textlist_temp.Length > 0)
                    {
                        for (int i = 0; i < textlist_temp.Length; i++)
                        {
                            var temp = textlist_temp[i].Trim();
                            TaskData.TextList.Add(temp);
                        }
                    }

                    if (listsubs[10] != " None")
                    {
                        string[] picture_temp = listsubs[10].Split("---");
                        if (picture_temp.Length > 0)
                        {
                            for (int i = 0; i < picture_temp.Length; i++)
                            {
                                // Debug.Log(npcpicture_temp[i].Trim());
                                byte[] pictureBytes = Convert.FromBase64String(picture_temp[i].Trim());
                                Texture2D picture_text = new Texture2D(10, 10);
                                picture_text.LoadImage(pictureBytes);
                                Sprite picture = Sprite.Create(picture_text, new Rect(0.0f, 0.0f, picture_text.width, picture_text.height), new Vector2(0.5f, 0.5f), 100.0f);
                                TaskData.Picture.Add(picture);
                            }
                        }
                    }

                    string[] picturefile_temp = listsubs[11].Split("/");
                    if (picturefile_temp.Length > 0)
                    {
                        for (int i = 0; i < picturefile_temp.Length; i++)
                        {
                            var temp = picturefile_temp[i].Trim();
                            TaskData.PictureFile.Add(temp);
                        }
                    }

                    string[] ability_temp = listsubs[12].Split("---");
                    if (ability_temp.Length > 0)
                    {
                        for (int i = 0; i < ability_temp.Length; i++)
                        {
                            TaskData.Ability.Add(int.Parse(ability_temp[i].Trim()));
                        }
                    }
                    Debug.LogFormat("下載 TaskData  {0} 成功", taskName);
                    isConnecting = false;
                }
            }
        }
    }
    public IEnumerator DataToSQL(string taskName, string taskDescription, string background, string appearance, bool eiaw, int npcnumber, string npclist, List<int> npcpicture, int textnumber, string textlist, List<String> picture, string picturefile, List<int> ability)
    {
        WWWForm form = new WWWForm();
        form.AddField("taskname", taskName);
        form.AddField("taskDescription", taskDescription);
        form.AddField("background", background);
        form.AddField("appearance", appearance);
        form.AddField("eiaw", eiaw.ToString());
        form.AddField("npcnumber", npcnumber);
        form.AddField("npclist", npclist);

        foreach (var item in npcpicture)
        {
            form.AddField("npcpicture[]", item);
        }

        form.AddField("textnumber", textnumber);
        form.AddField("textlist", textlist);

        foreach (var item in picture)
        {
            form.AddField("picture[]", item);
        }

        form.AddField("picturefile", picturefile);

        foreach (var item in ability)
        {
            form.AddField("ability[]", item);
        }

        using (UnityWebRequest www = UnityWebRequest.Post("https://140.125.32.129:5000/TaskDataToSQL", form))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                var status = www.downloadHandler.text;
                if (status == "TaskData Upload success")
                {
                    // Status.text = status;
                    Debug.Log("TaskData上傳成功");
                    isConnecting = false;
                }
                else
                {
                    Debug.Log("TaskData上傳失敗:" + status);
                }
            }

        }
    }

}
