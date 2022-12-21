using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public enum 檔案類型
{
    圖片庫,
    背景圖片,
    外觀圖片
}
public class LE_MountOnCell : MonoBehaviour
{
    [SerializeField] TaskData TaskData;
    public Sprite NoPicture;
    #region 檔案控制
    public 檔案類型 檔案類型 = 檔案類型.圖片庫;
    public void 選擇檔案(string type = "jpg;*.png")
    {
        OpenFileName ofn = new OpenFileName();
        ofn.structSize = Marshal.SizeOf(ofn);
        ofn.filter = "文件(*." + type + ")\0*." + type + "";
        ofn.file = new string(new char[256]);
        ofn.maxFile = ofn.file.Length;
        ofn.fileTitle = new string(new char[64]);
        ofn.maxFileTitle = ofn.fileTitle.Length;
        ofn.initialDir = Application.streamingAssetsPath.Replace('/', '\\');
        ofn.title = "選擇文件";
        ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;

        if (WindowDll.GetOpenFileName(ofn))
        {
            if (檔案類型 == 檔案類型.圖片庫)
            {
                try
                {
                    TaskData.Picture.RemoveAt(TaskData.PictureFile.FindIndex(x => x == this.gameObject.name + "_PF_" + this.gameObject.transform.Find("Picture/Text").GetComponent<TMPro.TMP_Text>().text));
                    TaskData.PictureFile.RemoveAt(TaskData.PictureFile.FindIndex(x => x == this.gameObject.name + "_PF_" + this.gameObject.transform.Find("Picture/Text").GetComponent<TMPro.TMP_Text>().text));
                }
                catch { }
                TaskData.Picture.Add(GetSprite(ofn.file));
                TaskData.PictureFile.Add(this.gameObject.name + "_PF_" + ofn.fileTitle);
                this.gameObject.transform.Find("Image").GetComponent<Image>().sprite = GetSprite(ofn.file);
                GameObject.Find("Canvas").SendMessage("新增一行圖片");
                GameObject.Find("Canvas").SendMessage("更新圖片選項");
            }
            else if (檔案類型 == 檔案類型.背景圖片)
            {
                TaskData.Background = GetSprite(ofn.file);
                this.gameObject.transform.Find("Image").GetComponent<Image>().sprite = GetSprite(ofn.file);
            }
            else if (檔案類型 == 檔案類型.外觀圖片)
            {
                TaskData.Appearance = GetSprite(ofn.file);
                this.gameObject.transform.Find("Image").GetComponent<Image>().sprite = GetSprite(ofn.file);
            }
            this.gameObject.transform.Find("Picture/Text").GetComponent<TMPro.TMP_Text>().text = ofn.fileTitle;
            this.gameObject.transform.Find("UnselectPicture").GetComponent<Button>().interactable = true;
        }
    }
    public void 取消選擇()
    {
        if (檔案類型 == 檔案類型.圖片庫)
        {
            TaskData.Picture.RemoveAt(TaskData.PictureFile.FindIndex(x => x == this.gameObject.name + "_PF_" + this.gameObject.transform.Find("Picture/Text").GetComponent<TMPro.TMP_Text>().text));
            TaskData.PictureFile.RemoveAt(TaskData.PictureFile.FindIndex(x => x == this.gameObject.name + "_PF_" + this.gameObject.transform.Find("Picture/Text").GetComponent<TMPro.TMP_Text>().text));
            this.gameObject.transform.Find("Picture/Text").GetComponent<TMPro.TMP_Text>().text = "Select a File";
            this.gameObject.transform.Find("Image").GetComponent<Image>().sprite = NoPicture;
            GameObject.Find("Canvas").SendMessage("更新圖片順序");
            GameObject.Find("Canvas").SendMessage("新增一行圖片");
            GameObject.Find("Canvas").SendMessage("更新圖片選項");
        }
        else if (檔案類型 == 檔案類型.背景圖片)
        {
            TaskData.Background = null;
            this.gameObject.transform.Find("Image").GetComponent<Image>().sprite = NoPicture;
            this.gameObject.transform.Find("Picture/Text").GetComponent<TMPro.TMP_Text>().text = "No Picture";
            this.gameObject.transform.Find("UnselectPicture").GetComponent<Button>().interactable = false;
        }
        else if (檔案類型 == 檔案類型.外觀圖片)
        {
            TaskData.Appearance = null;
            this.gameObject.transform.Find("Image").GetComponent<Image>().sprite = NoPicture;
            this.gameObject.transform.Find("Picture/Text").GetComponent<TMPro.TMP_Text>().text = "No Picture";
            this.gameObject.transform.Find("UnselectPicture").GetComponent<Button>().interactable = false;
        }
    }
    private Sprite GetSprite(string path)
    {
        //得到圖片的二進制信息
        FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        fileStream.Seek(0, SeekOrigin.Begin);
        byte[] binary = new byte[fileStream.Length];
        fileStream.Read(binary, 0, (int)fileStream.Length);
        fileStream.Close();
        fileStream.Dispose();

        //得到Texture
        Texture2D _texture = new Texture2D(1, 1);
        _texture.LoadImage(binary);
        Sprite _sprite = Sprite.Create(_texture, new Rect(0, 0, _texture.width, _texture.height), new Vector2(0, 0));
        return _sprite;
    }
    #endregion

    #region 選擇面板控制
    GameObject Cell_0;
    GameObject Cell_1;
    GameObject Cell_2;
    GameObject Cell_3;
    TMPro.TMP_Text Cell_0_Text;
    public void 開關選擇面板()
    {
        try
        {
            Cell_1.SetActive(true);
            Cell_2.SetActive(true);
            Cell_3.SetActive(true);
            Cell_0.SetActive(false);
        }
        catch { }
        Cell_1 = GameObject.Find((Int32.Parse(this.gameObject.name) + 1).ToString());
        Cell_2 = GameObject.Find((Int32.Parse(this.gameObject.name) + 2).ToString());
        Cell_3 = GameObject.Find((Int32.Parse(this.gameObject.name) + 3).ToString());
        Cell_0 = this.gameObject.transform.Find("ChoicePanel").gameObject;
        Cell_0_Text = this.gameObject.transform.Find("OpenChoicePanel/Text").GetComponent<TMPro.TMP_Text>();
        if (this.gameObject.transform.Find("OpenChoicePanel/Text").GetComponent<TMPro.TMP_Text>().text == "Open Choice Panel")
        {
            Cell_0.SetActive(true);
            Cell_0_Text.text = "Close Choice Panel";
            try
            {
                Cell_1.SetActive(false);
                Cell_2.SetActive(false);
                Cell_3.SetActive(false);
            }
            catch { }
        }
        else
        {
            Cell_0.SetActive(false);
            Cell_0_Text.text = "Open Choice Panel";
            try
            {
                Cell_1.SetActive(true);
                Cell_2.SetActive(true);
                Cell_3.SetActive(true);
            }
            catch { }
        }
    }
    #endregion

    #region 刪除單一行
    public void 刪除這一行文本()
    {
        GameObject.Find("Canvas").SendMessage("刪除指定文本", Int32.Parse(this.gameObject.name));
    }
    public void 刪除這一行NPC()
    {
        GameObject.Find("Canvas").SendMessage("刪除指定NPC", Int32.Parse(this.gameObject.name));
    }
    #endregion
}
