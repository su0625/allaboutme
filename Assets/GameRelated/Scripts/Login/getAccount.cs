using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class getAccount : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetUsers());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public class BypassCertificate : CertificateHandler{
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            //Simply return true no matter what
            return true;
        }
    } 

    IEnumerator GetUsers(){
        using (UnityWebRequest www = UnityWebRequest.Get("https://140.125.32.129:5000/")){
            www.certificateHandler = new BypassCertificate();

            yield return www.SendWebRequest();

            // if (www.isNetworkError || www.isHttpError){
            if (www.result == UnityWebRequest.Result.ConnectionError){
                Debug.Log(www.error);
            }else
            {
                Debug.Log("data:"+www.downloadHandler.text);
                byte[] results = www.downloadHandler.data;
            }
        }
    }
}
