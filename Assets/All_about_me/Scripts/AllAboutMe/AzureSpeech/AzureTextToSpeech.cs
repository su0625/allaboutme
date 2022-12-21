using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine.Networking;

public class AzureTextToSpeech : MonoBehaviour
{
    public Text AnswerText;
    public AudioSource audioSource;
    private object threadLocker = new object();
    public bool waitForSpeak;
    private string message;
    private string path;
    private string filename;
    private string audioType;
    public DateTime startTime;
    public DateTime endTime;

    public void ButtonClick()
    {
        startTime = DateTime.Now;

        var config = SpeechConfig.FromSubscription("f01d0e2959f24679a6606c2c4ae2009f","westus");
        using(var synthsizer = new SpeechSynthesizer(config,null))
        {
            lock(threadLocker)
            {
                waitForSpeak = true;
            }
            var result = synthsizer.SpeakTextAsync(AnswerText.text).Result;

            string newMessage = string.Empty;

            if(result.Reason == ResultReason.SynthesizingAudioCompleted)
            {
                var sampleCount = result.AudioData.Length/2;
                var audioData = new float [sampleCount];
                for(var i = 0; i < sampleCount; ++i)
                {
                    audioData[i] = (short)(result.AudioData[i*2 + 1] << 8 | result.AudioData[i*2]) / 32768.0F;
                }

                var audioClip = AudioClip.Create("SynthesizeAudio",sampleCount,1,16000,false);
                audioClip.SetData(audioData,0);
                audioSource.clip = audioClip;
                audioSource.Play();
                endTime = DateTime.Now;
                newMessage = "Speech syntheis success!";

            }
            else if(result.Reason == ResultReason.Canceled)
            {
                var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                newMessage = $"CANCELED\nReason=[{cancellation.Reason}]";
            }

            lock(threadLocker)
            {
                message = newMessage;
                waitForSpeak = false;
            }  
        }
    }
}
