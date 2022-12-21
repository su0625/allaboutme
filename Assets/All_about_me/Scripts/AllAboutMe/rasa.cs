using UnityEngine;
using UnityEngine.UI;
using Microsoft.CognitiveServices.Speech;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Reflection;

// A class to help in creating the Json object to be sent to the rasa server
public class PostMessageJson {
    public string message;
    public string sender;
    
}

[Serializable]
// A class to extract multiple json objects nested inside a value
public class RootReceiveMessageJson {
    public ReceiveMessageJson[] messages;
}

[Serializable]
// A class to extract a single message returned from the bot
public class ReceiveMessageJson {
    public string recipient_id;
    public string text;
    public string image;
    public string attachemnt;
    public string button;
    public string element;
    public string quick_replie;
}

public class rasa : MonoBehaviour
{
    [SerializeField] ParaData ParaData;
    public Text outputText;
    public Button NextButton;
    public Button BackButton;
    public Button RecordButton;
    public GameObject CheckPanel;
    public GameObject panel;
    public GameObject panel1;
    private AzureSpeechToText Azure;
    private IntroDiaglog Intro;
    private HintDiaglog Hint;
    private Scores Scores;
    public int answertime = 0;
    public bool ansCheck = false;
    private const string rasa_url = "http://140.125.32.129:5005/webhooks/rest/webhook";
    private void Awake() 
    {
        Azure = GetComponent<AzureSpeechToText>();
        Intro = GetComponent<IntroDiaglog>();
        Hint = GetComponent<HintDiaglog>();
        Scores = GetComponent<Scores>();
    }

    public void CheckRecord()
    {
        if(Azure.message == "NOMATCH: Speech could not be recognized." || Azure.message == "Click button to answer the question.")
        {
            ansCheck = false;
            panel.SetActive(true);
            Debug.Log("incorrect");
        }
        else
        {
            SendMessageToRasa ();
        }
    }

    public void SelectNext()
    {
        ansCheck = true;
        Scores.plus3();
        answertime = 0;
        Intro.NextClick();
        Hint.NextButtonClick();
    }

    public void SendMessageToRasa () {
        ansCheck = false;
        answertime++;
        CheckPanel.SetActive(true);
        NextButton.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);
        RecordButton.gameObject.SetActive(false);
        
        //Debug.Log("input:"+ outputText.text);
        Debug.Log("input:"+ outputText.text);
        // Create a json object from user message
        PostMessageJson postMessage = new PostMessageJson {
            sender = "user",
            message =  outputText.text
             //message = "My name is David"
        };

        string jsonBody = JsonUtility.ToJson(postMessage);
        print("User json : " + jsonBody);

        // Create a post request with the data to send to Rasa server
        StartCoroutine(PostRequest(rasa_url, jsonBody));

    // }
    }

    private IEnumerator PostRequest (string url, string jsonBody) {
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] rawBody = new System.Text.UTF8Encoding().GetBytes(jsonBody);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(rawBody);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        RecieveResponse(request.downloadHandler.text);    }
    
     // Parse the response received from the bot
    public void RecieveResponse (string response) {
        // Deserialize response recieved from the bot
        RootReceiveMessageJson receiveMessages =
            JsonUtility.FromJson<RootReceiveMessageJson>("{\"messages\":" + response + "}");
        // Debug.Log(receiveMessages);

        // show message based on message type on UI
        foreach (ReceiveMessageJson message in receiveMessages.messages) {
            FieldInfo[] fields = typeof(ReceiveMessageJson).GetFields();
            foreach (FieldInfo field in fields) {
                string data = null;

                // extract data from response in try-catch for handling null exceptions
                try {
                    data = field.GetValue(message).ToString();
                } catch (NullReferenceException) { }

                // print data
                if (data != null && field.Name != "recipient_id") {
                    // Debug.Log("Bot said: \"" + data + "\"");
                response = data;
                if(Intro.index == 5)
                {
                    string word = "";
                    string word1 = "'";
                    string word2 = ",";
                    string word3 = "(";
                    string word4 = ")";
                    response = response.Replace(word1,word);
                    response = response.Replace(word3,word);
                    response = response.Replace(word2,word);
                    response = response.Replace(word4,word);
                }
                Debug.Log(response);
                    
                    //Answer check
                    if(response != "None")
                    {
                        string answer = outputText.text;
                        answer = answer.Replace(",","");
                        if(answer.Contains(response))
                        {
                            ansCheck = true;

                            if(answertime == 1)
                            {
                                Scores.plus5();
                            }
                            else if(answertime == 2)
                            {
                                Scores.plus4();
                            }
                            else
                            {
                                Scores.plus3();
                            }

                            Intro.NextClick();
                            Hint.NextButtonClick();
                            
                            ParaData.ParaStorage[Intro.index-1] = response;
                            Debug.Log("correct");
                            response = " ";
                            answertime = 0;
                        }
                        else
                        {
                            ansCheck = false;
                            panel.SetActive(true);
                            Debug.Log("incorrect");
                            response = " ";
                        }
                    }
                    else
                    {
                        ansCheck = false;
                        panel.SetActive(true);
                        Debug.Log("incorrect");
                        response = " ";
                    }

                    CheckPanel.SetActive(false);   
                    NextButton.gameObject.SetActive(true);
                    BackButton.gameObject.SetActive(true);
                    RecordButton.gameObject.SetActive(true);
                }
            }
        }
    }
}

