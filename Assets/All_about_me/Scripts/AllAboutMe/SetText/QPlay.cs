using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QPlay : MonoBehaviour
{
    public GameObject VoicePrefab;
    public Button AudioButton;
    public AudioSource AudioSource1;
    public AudioClip[] audioNumClipArray;
    public bool playfinished = false;
    public int index = 0;
    void Awake()
    {
        AudioSource1 = GetComponent<AudioSource>();
    }

    void Update() 
    {
        if(AudioSource1.isPlaying)
        {
            AudioButton.interactable = false;
            AudioButton.gameObject.SetActive(false);
            VoicePrefab.SetActive(true);

        }
        else
        {
            AudioButton.interactable = true;
            AudioButton.gameObject.SetActive(true);
            VoicePrefab.SetActive(false);
        }
    }

    public void BtnClick()
    {
        AudioSource1.clip = audioNumClipArray[index];
        AudioSource1.PlayOneShot(AudioSource1.clip);
        playfinished= true;
    }

    public void BacktoLobby()
    {
        GameObject.Find("Canvas").SendMessage("LoadLevel", "GameLobby");
    }
}
