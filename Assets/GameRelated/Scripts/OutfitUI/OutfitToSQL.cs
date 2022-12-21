using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class OutfitToSQL : MonoBehaviour
{
    // public void Start(){
        // Debug.Log("start");
        // Download("2");
    // }
    [SerializeField] PlayerOutfitData outfitData;

    public class BypassCertificate : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            //Simply return true no matter what
            return true;
        }
    }

    public void Upload(string id,string OutfitNums)
    {
        StartCoroutine(ToSQL(id,OutfitNums));
    }

    public IEnumerator ToSQL(string id ,string outfit)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("outfit", outfit);

        using (UnityWebRequest www = UnityWebRequest.Post("https://140.125.32.129:5000/OutfitToSQL", form))
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

                if (status == "Outfit Upload fail")
                {
                    Debug.Log("Outfit 上傳失敗");
                    // Status.text = status;
                }
                else
                {
                    Debug.Log("Outfit上傳成功");
                    // Status.text = status;
                }
            }

        }
    }
    public void Download(string id)
    {   
        StartCoroutine(DownloadFromSQL(id));
    }
    public IEnumerator DownloadFromSQL(string id)
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("id", id);

        using (UnityWebRequest www = UnityWebRequest.Post("https://140.125.32.129:5000/getOutfitData", form1))
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

                if (status =="getOutfitData error"){
                    Debug.Log("獲取 OutfitData 失敗");
                }
                else
                {
                    string[] charsRemove = new string[] {"(",")","'",","};
                    foreach(var c in charsRemove){
                        status = status.Replace(c,string.Empty);
                    }
                    string[] subs = status.Split("/");
                    var listsubs = new List<string>();
                    foreach (string sub in subs)
                    {
                        listsubs.Add(sub);
                    }
                    for (int i = 0; i < listsubs.Count ; i++){
                        if(listsubs[i]==""){}
                        else{
                            // Debug.Log(listsubs[i]) ;
                            outfitData.OutfitNums[i]=int.Parse(listsubs[i]);
                        }
                    }
                }
            }
        }
    }

}
