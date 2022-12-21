using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "personalityscore", menuName = "Data/personalityscore")]
public class personalityscore : ScriptableObject
{
    public int Extraversion;
    public int Agreeableness;
    public int Conscientiousness;
    public int EmotionalStability;
    public int OpennesstoExperience;
    public int Listen;
    public int Speak;
    public int Read;
    public int Write;
    //public List<int> radarscore = new List<int>();
    
}
