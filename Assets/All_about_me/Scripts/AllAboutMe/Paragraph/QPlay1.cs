using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QPlay1 : MonoBehaviour
{
    public Button AudioButton;
    public AudioSource AudioSource1;
    public AudioClip[] audioNumClipArray;
    public Text AudioNum;
    int index = 0;
    int num;
    void Awake()
    {
        AudioSource1 = GetComponent<AudioSource>();
        num = index+1;
        AudioNum.text = num.ToString();
    }

    void Update() 
    {
        if(AudioSource1.isPlaying)
        {
            AudioButton.interactable = false;
        }
        else
        {
            AudioButton.interactable = true;
        }
    }

    public void NumClick()
    {
        AudioSource1.clip = audioNumClipArray[num-1];
        AudioSource1.PlayOneShot(AudioSource1.clip);
    }

    public void ArrowUpBtn()
    {
        if(index < 21)
            index++;
        else 
            index = 0;
        num = index+1;
        AudioNum.text = num.ToString();
    }

    public void ArrowDownBtn()
    {
        if(index > 0)
            index--;
        else 
            index = 21;
        num = index+1;
        AudioNum.text = num.ToString();
    }
}
