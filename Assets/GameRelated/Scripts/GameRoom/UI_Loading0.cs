using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Cinemachine;
using TMPro;

public class UI_Loading0 : MonoBehaviour
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
        // AsyncOperation operation = SceneManager.LoadSceneAsync(scenename);
        // operation.allowSceneActivation = false;
        //關閉自動切換場景

        //while (!operation.isDone)
        while (progress < 100)
        {
            progress = Mathf.Clamp01(0.9f / 0.9f);
            progress = progress * 100.0f;
            while (displayProgress < progress)
            {
                displayProgress++;
                slider.value = displayProgress / 100.0f;
                progressText.text = displayProgress + "%";
                // while(displayProgress >= 60 && displayProgress < 80)
                // {
                //     displayProgress = 60;
                //     slider.value = displayProgress / 100.0f;
                //     progressText.text = displayProgress + "%";
                //     Loading.SetActive(false);
                //     ConnectingServer.SetActive(true);
                //     yield return new WaitForSeconds(0.05f);
                // }
                // while(displayProgress >= 80)
                // {
                //     displayProgress = 80;
                //     slider.value = displayProgress / 100.0f;
                //     progressText.text = displayProgress + "%";
                //     Loading.SetActive(false);
                //     ConnectingServer.SetActive(false);
                //     ServerConnected.SetActive(true);
                //     yield return new WaitForSeconds(0.001f);
                // }
                yield return new WaitForSeconds(0.02f);
            }
        }
        Loading.SetActive(false);
        ConnectingServer.SetActive(false);
        LoadingisDone.SetActive(true);
        ServerConnected.SetActive(false);
        PressSpace.SetActive(true);
        while (displayProgress >= 100 && PhotonNetwork.CurrentRoom != null)
        {
            if (Input.GetKeyDown(KeyCode.Space)|Input.GetMouseButtonDown(0))
            {
                Loading_System.SetActive(false);
                // operation.allowSceneActivation = true;
                // float spawnPointX = Random.Range(-3,3);
                // float spawnPointY = 2;
                // PhotonNetwork.Instantiate("PhotonPlayer", new Vector3(spawnPointX, spawnPointY, 0), Quaternion.identity);  
            }
            yield return null;
        }
    }
}

