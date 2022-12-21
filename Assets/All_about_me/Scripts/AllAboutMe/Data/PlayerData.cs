using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    public string playerName;
    public string sex;
    public string zodiacSign;
    public string color;
    public string grade;
    public int score;
}