using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Loading : MonoBehaviour
{
    TaskDataController TaskDataController;
    GameObject Loading系統;
    Slider slider;
    TMPro.TMP_Text progressText;
    GameObject Loading;
    GameObject LoadingisDone;
    GameObject PressSpace;
    GameObject Loading_圓點;
    GameObject Loading_線條;
    float progress = 0;
    float displayProgress = 0;
    private void Awake()
    {
        TaskDataController = GetComponent<TaskDataController>();
        Loading系統 = this.gameObject.transform.Find("Loading系統").gameObject;
        slider = Loading系統.transform.Find("LoadingBar").GetComponent<Slider>();
        progressText = Loading系統.transform.Find("LoadingBar/Text").GetComponent<TMPro.TMP_Text>();
        Loading = Loading系統.transform.Find("LoadingBar/Loading").gameObject;
        LoadingisDone = Loading系統.transform.Find("LoadingBar/LoadingisDone").gameObject;
        PressSpace = Loading系統.transform.Find("LoadingBar/PressSpace").gameObject;
        try
        {
            Loading_圓點 = this.gameObject.transform.Find("Loading_圓點").gameObject;
            Loading_線條 = this.gameObject.transform.Find("Loading_線條").gameObject;
        }
        catch { }
        Loading.SetActive(true);
        LoadingisDone.SetActive(false);
        PressSpace.SetActive(false);
    }
    public void LoadLevel(string scenename)
    {
        Loading系統.SetActive(true);
        StartCoroutine(LoadAsynchronously(scenename));
    }
    public void LoadLevelWithCheakConnecting(string scenename)
    {
        Loading系統.SetActive(true);
        StartCoroutine(LoadAsynchronouslyCheakConnecting(scenename));
    }
    public void CallLoadingPanel(int Loading方式,bool 狀態)
    {
        if(Loading方式 == 0)
        {
            if (狀態 == true) Loading_圓點.SetActive(true);
            else Loading_圓點.SetActive(false);
        }
        else if(Loading方式 == 1)
        {
            if (狀態 == true) Loading_線條.SetActive(true);
            else Loading_線條.SetActive(false);
        }
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
            while (displayProgress< progress)
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
    IEnumerator LoadAsynchronouslyCheakConnecting(string scenename)
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
            while (displayProgress < progress)
            {
                displayProgress++;
                slider.value = displayProgress / 100.0f;
                progressText.text = displayProgress + "%";
                while (TaskDataController.isConnecting && displayProgress >= 65)
                {
                    displayProgress = 65;
                    slider.value = displayProgress / 100.0f;
                    progressText.text = displayProgress + "%";
                    yield return new WaitForSeconds(0.001f);
                }
                yield return new WaitForSeconds(0.01f);
            }
        }
        Loading.SetActive(false);
        LoadingisDone.SetActive(true);
        PressSpace.SetActive(true);
        while (displayProgress >= 100)
        {
            if (Input.GetKeyDown(KeyCode.Space) | Input.GetMouseButtonDown(0))
            {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
