using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreTextMesh : MonoBehaviour
{
    [SerializeField] PlayerData PlayerData;
    private TextMeshProUGUI TextMesh;
    void Start()
    {
        TextMesh = GetComponent<TextMeshProUGUI>();
        TextMesh.text = PlayerData.score.ToString();
    }
}
