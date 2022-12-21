using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class radarToSQL : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] personalityscore personalityscore;
    [SerializeField] AccountData AccountData;

    private void Awake() {
        //Upload();
    }

    // Update is called once per frame
    public class BypassCertificate : CertificateHandler{
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            //Simply return true no matter what
            return true;
        }
    } 
    public void Upload()
    {
        var Id = AccountData.id;
        var Name = playerData.playerName;
        var Sex = playerData.sex;
        var Zodiac = playerData.zodiacSign;
        var Color = playerData.color;
        var Grade = playerData.grade;
        var Score = playerData.score;
 
        //radar
        var extraversion = personalityscore.Extraversion;
        var agreeableness = personalityscore.Agreeableness;
        var conscientiousness = personalityscore.Conscientiousness;
        var emotionalStability = personalityscore.EmotionalStability;
        var opennesstoExperience = personalityscore.OpennesstoExperience;
        // LSRW
        var listen = personalityscore.Listen;
        var speak = personalityscore.Speak;
        var read = personalityscore.Read;
        var write = personalityscore.Write; 
        // print(extraversion);

        StartCoroutine(PlayerDataToSQL(
            Id,Name,Sex,Zodiac,Color,Grade,Score,extraversion,agreeableness,conscientiousness,emotionalStability,opennesstoExperience,listen,speak,read,write
        ));
    }

    public IEnumerator PlayerDataToSQL(string id,string name,string sex,string Zodiac,string color,string grade,int score,int extraversion,int agreeableness,int conscientiousness,int emotionalStability,int opennesstoExperier,int listen,int speak,int read,int write)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("Name", name);
        form.AddField("Sex", sex);
        form.AddField("Zodiac", Zodiac);
        form.AddField("Color", color);
        form.AddField("Grade", grade);
        form.AddField("Score", score);
        //radar
        form.AddField("Extraversion", extraversion);
        form.AddField("Agreeableness", agreeableness);
        form.AddField("Conscientiousness", conscientiousness);
        form.AddField("EmotionalStability", emotionalStability);
        form.AddField("OpennesstoExperier", opennesstoExperier);
        // LSRW
        form.AddField("listen",listen);
        form.AddField("speak",speak);
        form.AddField("read",read);
        form.AddField("write",write);

        using (UnityWebRequest www = UnityWebRequest.Post("https://140.125.32.129:5000/PlayerDataToSQL", form))
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
                // Debug.Log("status:" + status);

                if (status == "PlayerData Upload fail")
                {
                    Debug.Log("PlayerData上傳失敗");
                    // Status.text = status;
                }
                else
                {
                    Debug.Log("PlayerData上傳成功");
                    // Status.text = status;
                    // panel.SetActive(true);
                }
            }

        }
    }

}
