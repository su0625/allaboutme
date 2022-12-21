using UnityEngine;
using UnityEngine.UI;
using Microsoft.CognitiveServices.Speech;
using System;
using System.IO;
using System.Collections;
using UnityEngine.Networking;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class AzureSpeechToText : MonoBehaviour
{
    // Hook up the two properties below with a Text and Button object in your UI.
    public Text outputText;
    public Button NextButton;
    public Button BackButton;
    public Button startRecoButton;
    private rasa rasa;
    public GameObject Panel;
    private object threadLocker = new object();
    public bool waitingForReco;
    public string waitingForStop;
    public string message;
    private bool micPermissionGranted = false;
    public AudioClip RecordedClip;
    public AudioSource audioSource; 
    public Text Address; //儲存位址
    private string fileName;//儲存名稱
    private byte[] data;
    [SerializeField] AccountData AccountData;

    

#if PLATFORM_ANDROID
    // Required to manifest microphone permission, cf.
    // https://docs.unity3d.com/Manual/android-manifest.html
    private Microphone mic;
#endif
    public async void ButtonClick()
    {
        // Creates an instance of a speech config with specified subscription key and service region.
        // Replace with your own subscription key and service region (e.g., "westus").
        var config = SpeechConfig.FromSubscription("6635ba6797774336949b389e0de7e8fd", "eastus");
        //Azure Speech API Key
        //ServiceRegion
        StartCoroutine(Begin());
        // Make sure to dispose the recognizer after use!
        using (var recognizer = new SpeechRecognizer(config))
        {
            lock (threadLocker)
            {
                waitingForReco = true;
            }

            // Starts speech recognition, and returns after a single utterance is recognized. The end of a
            // single utterance is determined by listening for silence at the end or until a maximum of 15
            // seconds of audio is processed.  The task returns the recognition text as result.
            // Note: Since RecognizeOnceAsync() returns only a single utterance, it is suitable only for single
            // shot recognition like command or query.
            // For long-running multi-utterance recognition, use StartContinuousRecognitionAsync() instead.
            var result = await recognizer.RecognizeOnceAsync().ConfigureAwait(false);

            // Checks result.
            string newMessage = string.Empty;
            if (result.Reason == ResultReason.RecognizedSpeech)
            {
                newMessage = result.Text;
            }
            else if (result.Reason == ResultReason.NoMatch)
            {
                newMessage = "NOMATCH: Speech could not be recognized.";
            }
            else if (result.Reason == ResultReason.Canceled)
            {
                var cancellation = CancellationDetails.FromResult(result);
                newMessage = $"CANCELED: Reason={cancellation.Reason} ErrorDetails={cancellation.ErrorDetails}";
            }

            lock (threadLocker)
            {
                message = newMessage;
                waitingForReco = false;
                waitingForStop = "stop";

            }
        }
    }
    private void Awake() 
    {
        rasa = GetComponent<rasa>();    
    }
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        // Continue with normal initialization, Text and Button objects are present.

#if PLATFORM_ANDROID
            // Request to use the microphone, cf.
            // https://docs.unity3d.com/Manual/android-RequestingPermissions.html
            message = "Waiting for mic permission";
            if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
            {
                Permission.RequestUserPermission(Permission.Microphone);
            }
#else
            micPermissionGranted = true;
            message = "Click button to answer the question.";
#endif
            startRecoButton.onClick.AddListener(() => {ButtonClick();StartCoroutine(Stop());});

    }

    void Update()
    {
        
            
#if PLATFORM_ANDROID
        if (!micPermissionGranted && Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            micPermissionGranted = true;
            message = "Click button to answer the question.";
        }
#endif

        lock (threadLocker)
        {
            startRecoButton.interactable = !waitingForReco && micPermissionGranted;
            NextButton.interactable = !waitingForReco;
            BackButton.interactable = !waitingForReco;
            Panel.SetActive(waitingForReco);
            outputText.text = message;
        }
    }
 /// <summary>
    /// 开始录音
    /// </summary>
    IEnumerator Begin()
    {
        if (!Microphone.IsRecording(null))
        {
            RecordedClip = Microphone.Start(null, false, 10, 44100);
            Debug.Log("開始錄音");
            yield return null;
        }
    }

    /// <summary>
    /// 停止录音
    /// </summary>
    public IEnumerator Stop()
    {
        yield return new WaitUntil(() => waitingForStop == "stop");
        data = GetRealAudio(ref RecordedClip);
        Microphone.End(null);
        Debug.Log("錄音结束");
        yield return null;

    }

    /// <summary>
    /// 播放录音
    /// </summary>
    // public void Player()
    // {
    //     if (!Microphone.IsRecording(null))
    //     {
    //         audioSource.clip = RecordedClip;
    //         audioSource.Play();
    //         Debug.Log("正在播放录音！");
    //     }
    // }

    /// <summary>
    /// 保存录音
    /// </summary>
    public void Save()
    {
        if (!Microphone.IsRecording(null))
        {   
            var username = AccountData.Account;
            var Qnum = PlayerPrefs.GetString("Qnum");
            fileName = username+"_"+DateTime.Now.ToString("yyyyMMddHHmmss");
            if (!fileName.ToLower().EndsWith(".wav"))
            {//如果不是“.wav”格式的，加上wav
                fileName += "_"+Qnum+".wav";
            }
            // 輸出路徑
            string path = Path.Combine(Application.streamingAssetsPath, fileName);//录音保存路径

            using (FileStream fs = CreateEmpty(path))
            {
                fs.Write(data, 0, data.Length);
                WriteHeader(fs, RecordedClip); //wav文件頭
                waitingForStop = "";
            }
            Byte[] bytes64 = File.ReadAllBytes(path);
            string file64 = Convert.ToBase64String(bytes64);
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(file64);
            StartCoroutine(base64tosql(file64,username,Qnum));
            File.Delete(path);
        }
    }

    /// <summary>
    /// 獲取錄音
    /// </summary>
    /// <param name="recordedClip"></param>
    /// <returns></returns>
    public static byte[] GetRealAudio(ref AudioClip recordedClip)
    {
        int position = Microphone.GetPosition(null);
        if (position <= 0 || position > recordedClip.samples)
        {
            position = recordedClip.samples;
        }
        float[] soundata = new float[position * recordedClip.channels];
        recordedClip.GetData(soundata, 0);
        recordedClip = AudioClip.Create(recordedClip.name, position,
        recordedClip.channels, recordedClip.frequency, false);
        recordedClip.SetData(soundata, 0);
        int rescaleFactor = 32767;
        byte[] outData = new byte[soundata.Length * 2];
        for (int i = 0; i < soundata.Length; i++)
        {
            short temshort = (short)(soundata[i] * rescaleFactor);
            byte[] temdata = BitConverter.GetBytes(temshort);
            outData[i * 2] = temdata[0];
            outData[i * 2 + 1] = temdata[1];
        }
        // Debug.Log("position=" + position + "  outData.leng=" + outData.Length);
        return outData;
    }
    /// <summary>
    /// 寫文件頭
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="clip"></param>
    public static void WriteHeader(FileStream stream, AudioClip clip)
    {
        int hz = clip.frequency;
        int channels = clip.channels;
        int samples = clip.samples;

        float[] fBytes = new float[clip.samples * clip.channels];
        byte[] bytes = new byte[fBytes.Length * 4];
        clip.GetData(fBytes, 0);
        Buffer.BlockCopy(fBytes, 0, bytes, 0, bytes.Length);
        stream.Seek(0, SeekOrigin.Begin);

        Byte[] riff = System.Text.Encoding.UTF8.GetBytes("RIFF");
        stream.Write(riff, 0, 4);

        Byte[] chunkSize = BitConverter.GetBytes(stream.Length - 8);
        stream.Write(chunkSize, 0, 4);

        Byte[] wave = System.Text.Encoding.UTF8.GetBytes("WAVE");
        stream.Write(wave, 0, 4);

        Byte[] fmt = System.Text.Encoding.UTF8.GetBytes("fmt ");
        stream.Write(fmt, 0, 4);

        Byte[] subChunk1 = BitConverter.GetBytes(16);
        stream.Write(subChunk1, 0, 4);

        UInt16 one = 1;

        Byte[] audioFormat = BitConverter.GetBytes(one);
        stream.Write(audioFormat, 0, 2);

        Byte[] numChannels = BitConverter.GetBytes(channels);
        stream.Write(numChannels, 0, 2);

        Byte[] sampleRate = BitConverter.GetBytes(hz);
        stream.Write(sampleRate, 0, 4);

        Byte[] byteRate = BitConverter.GetBytes(hz * channels * 2);
        stream.Write(byteRate, 0, 4);

        UInt16 blockAlign = (ushort)(channels * 2);
        stream.Write(BitConverter.GetBytes(blockAlign), 0, 2);

        UInt16 bps = 16;
        Byte[] bitsPerSample = BitConverter.GetBytes(bps);
        stream.Write(bitsPerSample, 0, 2);

        Byte[] datastring = System.Text.Encoding.UTF8.GetBytes("data");
        stream.Write(datastring, 0, 4);

        Byte[] subChunk2 = BitConverter.GetBytes(samples * channels * 2);
        stream.Write(subChunk2, 0, 4);
    }

    /// <summary>
    /// 創建wav格式文件頭
    /// </summary>
    /// <param name="filepath"></param>
    /// <returns></returns>
    private FileStream CreateEmpty(string filepath)
    {
        FileStream fileStream = new FileStream(filepath, FileMode.Create);
        byte emptyByte = new byte();

        for (int i = 0; i < 44; i++) //為wav文件頭留空間
        {
            fileStream.WriteByte(emptyByte);
        }
        return fileStream;
    }

    public class BypassCertificate : CertificateHandler{
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            //Simply return true no matter what
            return true;
        }
    }

    public IEnumerator base64tosql(string base64,string username,string Qnum)
    {
        WWWForm form = new WWWForm();
        form.AddField("base64", base64);
        form.AddField("username", username);
        form.AddField("Qnum", Qnum);

        using (UnityWebRequest www = UnityWebRequest.Post("https://140.125.32.129:5000/base64decode", form))
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
                Debug.Log("status:"+status);

                if (status =="save_error"){
                    Debug.Log("儲存失敗");
                }
                else{
                    Debug.Log("儲存成功");
                }

            }
        }
    }
}


