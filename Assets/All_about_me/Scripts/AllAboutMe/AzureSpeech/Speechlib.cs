using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechLib;

public class Speechlib : MonoBehaviour
{
    SpVoice voice;
    void Start()
    {
        voice = new SpVoice();

        //Item(0)中文语音 Item(1)英文语音 
        voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(1);
        
        //範圍-10~10
        voice.Rate = -1;
        
        //範圍0~100
        voice.Volume = 100;

        // voice.Speak(string.Empty, SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.W))
        // {
        //     //暂停使用该对象的所有朗读进程，同步朗读下无法使用该方法暂停
        //     voice.Pause();
        // }

        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     //恢复该对象所对应的被暂停的朗读进程
        //     voice.Resume();
        // }
    }

    public void Speech(string message)
    {
        voice.Speak(string.Empty, SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
        voice.Speak(message, SpeechVoiceSpeakFlags.SVSFlagsAsync);
    }

    public void CancelSpeech()
    {
        voice.Speak(string.Empty, SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
    }

}
