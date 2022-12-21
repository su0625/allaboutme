using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArticlelData", menuName = "Data/ArticleData")]
public class ArticleData : ScriptableObject
{
    [TextArea(5, 10)]
    public string ArticleBefore;

    [TextArea(5, 10)]
    public string ArticleAfter;
}
