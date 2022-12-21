using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    // public InputField SchoolInput;
    public Dropdown SchoolInput;
    public Dropdown GradeInput;
    public Dropdown ClassInput;
    public InputField AccountInput;
    public InputField PasswordInput;
    public Button LoginButton;
    [SerializeField] PlayerData PlayerData;
    [SerializeField] personalityscore personalityscore;
    [SerializeField] AccountData AccountData;


    // Start is called before the first frame update
    void Start()
    {
        LoginButton.onClick.AddListener(()=>{
            StartCoroutine(LoginCheck(SchoolInput.options[SchoolInput.value].text, GradeInput.options[GradeInput.value].text, ClassInput.options[ClassInput.value].text, AccountInput.text, PasswordInput.text));
        });
    }

    public class BypassCertificate : CertificateHandler{
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            //Simply return true no matter what
            return true;
        }
    }
    public IEnumerator LoginCheck(string school, string grade, string Class, string username, string password)
    {
        bool loginCheck = false;
        WWWForm form = new WWWForm();
        form.AddField("loginSchool", school);
        form.AddField("loginGrade", grade);
        form.AddField("loginClass", Class);
        form.AddField("loginUser", username);
        form.AddField("loginPassword", password);

        using (UnityWebRequest www = UnityWebRequest.Post("https://140.125.32.129:5000/login", form))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.error);
                Debug.Log("登入失敗");
            }
            else
            {
                var status=www.downloadHandler.text;
                // Debug.Log("status:"+status);
                string[] subs = status.Split(',');
                var listsubs = new List<string>();
                
                foreach (string sub in subs)
                {
                    // Debug.Log($"Substring: {sub}");
                    listsubs.Add(sub);
                }

                if (status !="login error"){
                    AccountData.id = listsubs[0].Trim();
                    AccountData.School = school;
                    AccountData.Grade = grade;
                    AccountData.Class = Class;
                    AccountData.Account = username;
                    AccountData.Password = password;
                    AccountData.Sex = listsubs[1];
                    AccountData.Profession = listsubs[2];
                    
                    Debug.Log("登入id: "+listsubs[0]+" ,School: "+school+" ,Grade: "+grade+" ,Class: "+Class+" ,Account: "+username+" ,Password: "+password+",Sex: "+listsubs[1]);
                    if (AccountData.Profession =="Teacher"){
                        GameObject.Find("Canvas").SendMessage("LoadLevel", "EditHome");
                        loginCheck = false;
                    }else{
                        loginCheck = true;
                    }
                }
            }
        }

        if(loginCheck){

        WWWForm form1 = new WWWForm();
        var id = AccountData.id;
        form1.AddField("id", id);

            using (UnityWebRequest www = UnityWebRequest.Post("https://140.125.32.129:5000/getPlayerData", form1))
            {
                www.certificateHandler = new BypassCertificate();
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    var status=www.downloadHandler.text;
                    // Debug.Log(status);
                    string[] charsRemove = new string[] {"(",")","'"};
                    foreach(var c in charsRemove){
                        status = status.Replace(c,string.Empty);
                    }
                    string[] subs = status.Split(',');
                    var listsubs = new List<string>();
                    // Debug.Log(subs);
                    foreach (string sub in subs)
                    {
                        listsubs.Add(sub);
                    }

                    if (status =="getplayerData error"){
                        Debug.Log("獲取 PlayerData 失敗");
                    }
                    else{
                        AccountData.id = AccountData.id;
                        AccountData.School = school;
                        AccountData.Grade = grade;
                        AccountData.Class = Class;
                        AccountData.Account = username;
                        AccountData.Password = password;
                        AccountData.Sex = AccountData.Sex;
                        AccountData.Profession = AccountData.Profession;

                        PlayerData.playerName = listsubs[1].Trim();
                        PlayerData.sex = listsubs[2].Trim();
                        PlayerData.zodiacSign = listsubs[3].Trim();
                        PlayerData.color = listsubs[4].Trim();
                        PlayerData.grade = listsubs[5].Trim();
                        PlayerData.score = int.Parse(listsubs[6].Trim());

                        personalityscore.Extraversion = int.Parse(listsubs[7].Trim());
                        personalityscore.Agreeableness = int.Parse(listsubs[8].Trim());
                        personalityscore.Conscientiousness = int.Parse(listsubs[9].Trim());
                        personalityscore.EmotionalStability = int.Parse(listsubs[10].Trim());
                        personalityscore.OpennesstoExperience = int.Parse(listsubs[11].Trim());

                        personalityscore.Listen = int.Parse(listsubs[12].Trim());
                        personalityscore.Speak = int.Parse(listsubs[13].Trim());
                        personalityscore.Read = int.Parse(listsubs[14].Trim());
                        personalityscore.Write = int.Parse(listsubs[15].Trim());

                        GameObject.Find("Canvas").GetComponent<OutfitToSQL>().Download(id);

                        //GameObject.Find("Canvas").SendMessage("LoadLevel", "GameLobby");

                        SceneManager.LoadScene("GameLobby");
                    }
                }
            }
        }
    }
}
