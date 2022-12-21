using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour
{
    [SerializeField] PlayerData PlayerData;
    private rasa rasa;
    public int score;
    

    private void Start() 
    {
        rasa = GetComponent<rasa>();
        score = 12;
        PlayerData.score = 0;
    }

    public void plus5()
    {
        if(rasa.ansCheck)
            score = score + 4;
            PlayerData.score = score;
    }

    public void plus4()
    {
        if(rasa.ansCheck)
            score = score + 3;
            PlayerData.score = score;
    }

    public void plus3()
    {
        if(rasa.ansCheck)
            score = score + 2;
            PlayerData.score = score;
    }
}
