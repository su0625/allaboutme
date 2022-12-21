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

public class AzureTextToSpeech1 : MonoBehaviour
{
    public AudioSource audioSource;
    private object threadLocker = new object();
    public bool waitingForSpeak;
    private string message;
    private string path;
    private string filename;
    private string audioType;
    public DateTime startTime;
    public DateTime endTime;

    public void AudioPlay(string sentense)
    {
        startTime = DateTime.Now;
        // Creates an instance of a speech config with specified subscription key and service region.
        // Replace with your own subscription key and service region (e.g., "westus").
        var config = SpeechConfig.FromSubscription("f01d0e2959f24679a6606c2c4ae2009f", "eastus");
        // Creates a speech synthesizer.
        // Make sure to dispose the synthesizer after use!
        using (var synthsizer = new SpeechSynthesizer(config, null))
        {
            lock (threadLocker)
            {
                waitingForSpeak = true;
            }
            // Starts speech synthesis, and returns after a single utterance is synthesized.
            var result = synthsizer.SpeakTextAsync(sentense).Result;
            //print("after   " + DateTime.Now);
 
            // Checks result.
            string newMessage = string.Empty;
 
            if (result.Reason == ResultReason.SynthesizingAudioCompleted)
            {
                // Since native playback is not yet supported on Unity yet (currently only supported on Windows/Linux Desktop),
                // use the Unity API to play audio here as a short term solution.
                // Native playback support will be added in the future release.
                var sampleCount = result.AudioData.Length / 2;
                var audioData = new float[sampleCount];
                for (var i = 0; i < sampleCount; ++i)
                {
                    audioData[i] = (short)(result.AudioData[i * 2 + 1] << 8 | result.AudioData[i * 2]) / 32768.0F;
 
                }
                // The default output audio format is 16K 16bit mono
                var audioClip = AudioClip.Create("SynthesizedAudio", sampleCount, 1, 16000, false);
                audioClip.SetData(audioData, 0);
                audioSource.clip = audioClip;
                audioSource.Play();
                endTime = DateTime.Now;
                // timeText.text = "合成前时间：" + startTime + "     合成成功时间：" + endTime;
 
                newMessage = "Speech synthesis succeeded!";
            }
 
            else if (result.Reason == ResultReason.Canceled)
            {
                var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                newMessage = $"CANCELED:\nReason=[{cancellation.Reason}]\nErrorDetails=[{cancellation.ErrorDetails}]\nDid you update the subscription info?";
            }
 
            lock (threadLocker)
            {
                message = newMessage;
                print(message);
                waitingForSpeak = false;
            }
        }
    }
}
