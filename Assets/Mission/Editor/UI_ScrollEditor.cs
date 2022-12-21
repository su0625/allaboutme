using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace UI_ScrollView
{
    [CustomEditor(typeof(UI_ScrollView.UI_Scroll))]
    public class UI_ScrollEditor : Editor
    {

        UI_Scroll 清單;
        public override void OnInspectorGUI()
        {
            清單 = (UI_ScrollView.UI_Scroll)target;

            清單.卷軸方向 = (方向)EditorGUILayout.EnumPopup("方向: ", 清單.卷軸方向);

            清單.排數 = EditorGUILayout.IntField("排數: ", 清單.排數);
            清單.間距 = EditorGUILayout.FloatField("間距: ", 清單.間距);
            清單.Cell單位 = (GameObject)EditorGUILayout.ObjectField("Cell: ", 清單.Cell單位, typeof(GameObject), true);

        }
    }
}
