using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_Loading1 : MonoBehaviour
{
    GameObject Loading_System; 
    Slider slider;
    TMPro.TMP_Text progressText;
    GameObject Loading;
    GameObject ConnectingServer;
    GameObject ServerConnected;
    GameObject LoadingisDone;
    GameObject PressSpace;

    float progress = 0;
    float displayProgress = 0;
    private void Awake()
    {
        Loading_System = this.gameObject.transform.Find("Loading_System").gameObject;
        slider = Loading_System.transform.Find("LoadingBar").GetComponent<Slider>();
        progressText = Loading_System.transform.Find("LoadingBar/Text").GetComponent<TMPro.TMP_Text>();
        Loading = Loading_System.transform.Find("LoadingBar/Loading").gameObject;
        ConnectingServer = Loading_System.transform.Find("LoadingBar/ConnectingServer").gameObject;
        ServerConnected = Loading_System.transform.Find("LoadingBar/ServerConnected").gameObject;
        LoadingisDone = Loading_System.transform.Find("LoadingBar/LoadingisDone").gameObject;
        PressSpace = Loading_System.transform.Find("LoadingBar/PressSpace").gameObject;

        Loading.SetActive(true);
        ConnectingServer.SetActive(false);
        ServerConnected.SetActive(false);
        LoadingisDone.SetActive(false);
        PressSpace.SetActive(false);
    }
    public void LoadLevel(string scenename)
    {
        Loading_System.SetActive(true);
        StartCoroutine(LoadAsynchronously(scenename));
    }
    IEnumerator LoadAsynchronously(string scenename)
    {
        //背景載入場景
        AsyncOperation operation = SceneManager.LoadSceneAsync(scenename);
        operation.allowSceneActivation = false;
        //關閉自動切換場景

        //while (!operation.isDone)
        while (progress < 100)
        {
            progress = Mathf.Clamp01(operation.progress / 0.9f);
            progress = progress * 100.0f;
            while (displayProgress < progress )
            {
                displayProgress++;
                slider.value = displayProgress / 100.0f;
                progressText.text = displayProgress + "%";
                yield return new WaitForSeconds(0.01f);
            }
        }
        Loading.SetActive(false);
        LoadingisDone.SetActive(true);
        PressSpace.SetActive(true);
        while (progress >= 100)
        {
            if (Input.GetKeyDown(KeyCode.Space)|Input.GetMouseButtonDown(0))
            { 
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}

