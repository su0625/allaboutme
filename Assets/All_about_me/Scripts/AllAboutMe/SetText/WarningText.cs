using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WarningText : MonoBehaviour
{
    private TextMeshProUGUI TextMesh;
    private AzureSpeechToText Azure;
    public Text AnswerText;
    void Start()
    {
        TextMesh = GetComponent<TextMeshProUGUI>();
        Azure = GetComponent<AzureSpeechToText>();
    }

    void Update() 
    {
        if(AnswerText.text == "Click button to answer the question.")
        {
            TextMesh.text = "Please click the <color=red>record button</color> to answer the question.";
        }
        else if(AnswerText.text == "NOMATCH: Speech could not be recognized.")
        {
            TextMesh.text = "Please record your answer after <color=red>clicking the record button</color>.";
        }
        else
        {
            TextMesh.text = "Please answer the question according to the  <color=red>Hint</color>.";
        }
    }
}
