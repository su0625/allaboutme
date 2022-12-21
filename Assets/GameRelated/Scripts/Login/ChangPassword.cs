using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class ChangPassword : MonoBehaviour
{
    public Text SchoolText;
    public Text GradeText;
    public Text ClassText;
    public Text AccountText;
    public InputField OldPasswordInput;
    public InputField NewPasswordInput;
    public InputField checkNewPasswordInput;
    public Button ChangPasswordButton;
    public Text Status;
    public GameObject panel;
    [SerializeField] AccountData AccountData;

    // Start is called before the first frame update
    void Start()
    {
        var Id = AccountData.id;
        var School = AccountData.School;
        var grade = AccountData.Grade;
        var Class = AccountData.Class;;
        var Account = AccountData.Account;
        var OldPassword = AccountData.Password;
        
        SchoolText.text = School;
        GradeText.text = grade;
        ClassText.text = Class;
        AccountText.text = Account;

        ChangPasswordButton.onClick.AddListener(()=>{
            StartCoroutine(ChangPasswordCheck(Id,Account,OldPasswordInput.text,NewPasswordInput.text,Class,checkNewPasswordInput.text));
        });
    }
    public class BypassCertificate : CertificateHandler{
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            //Simply return true no matter what
            return true;
        }
    } 

   public IEnumerator ChangPasswordCheck(string id,string account,string oldpassword,string newpassword,string Class,string checkNewpassword)
    {
        if (oldpassword == string.Empty || newpassword == string.Empty || checkNewpassword == string.Empty)
        {
            Status.text = "有空格沒填滿";
            Debug.Log("Empty Inputfield Exist");
            yield return null;
        }
        else if(newpassword != checkNewpassword)
        {
            Status.text = "確認密碼錯誤";
            Debug.Log("確認密碼錯誤");
            yield return null;
        }
        else
        {
            WWWForm form = new WWWForm();
            form.AddField("id", id);
            form.AddField("account", account);
            form.AddField("oldPassword", oldpassword);
            form.AddField("NewPassword", newpassword);
            form.AddField("Class", Class);

            using (UnityWebRequest www = UnityWebRequest.Post("https://140.125.32.129:5000/ChangePassword", form))
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
                    Debug.Log("status:" + status);

                    if (status == "oldPassword error")
                    {
                        Debug.Log("oldPassword error");
                        Status.text = status;
                    }
                    else
                    {
                        Debug.Log("change success");
                        AccountData.Password = newpassword;
                        Status.text = status;
                        panel.SetActive(true);
                    }
                }

            }
        }
    }

    
}

