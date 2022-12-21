using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EditAns : MonoBehaviour
{
    [SerializeField] ParaData paraData;
    public InputField A1,A2,A3,A4,A5,A6,A7,A8,A9,A10,A11,A12,A13,A14,A15,A16,A17,A18,A19,A20,A21,A22;
    void Start()
    {
        SetA();
    }

    void SetA()
    {
        A1.text = paraData.ParaStorage[0];
        A2.text = paraData.ParaStorage[1];
        A3.text = paraData.ParaStorage[2];
        A4.text = paraData.ParaStorage[3];
        A5.text = paraData.ParaStorage[4];
        A6.text = paraData.ParaStorage[5];
        A7.text = paraData.ParaStorage[6];
        A8.text = paraData.ParaStorage[7];
        A9.text = paraData.ParaStorage[8];
        A10.text = paraData.ParaStorage[9];
        A11.text = paraData.ParaStorage[10];
        A12.text = paraData.ParaStorage[11];
        A13.text = paraData.ParaStorage[12];
        A14.text = paraData.ParaStorage[13];
        A15.text = paraData.ParaStorage[14];
        A16.text = paraData.ParaStorage[15];
        A17.text = paraData.ParaStorage[16];
        A18.text = paraData.ParaStorage[17];
        A19.text = paraData.ParaStorage[18];
        A20.text = paraData.ParaStorage[19];
        A21.text = paraData.ParaStorage[20];
        A22.text = paraData.ParaStorage[21];
    }

    public void EditAnswer()
    {
        paraData.ParaStorage[0] = A1.text;
        paraData.ParaStorage[1] = A2.text;
        paraData.ParaStorage[2] = A3.text;
        paraData.ParaStorage[3] = A4.text;
        paraData.ParaStorage[4] = A5.text;
        paraData.ParaStorage[5] = A6.text;
        paraData.ParaStorage[6] = A7.text;
        paraData.ParaStorage[7] = A8.text;
        paraData.ParaStorage[8] = A9.text;
        paraData.ParaStorage[9] = A10.text;
        paraData.ParaStorage[10] = A11.text;
        paraData.ParaStorage[11] = A12.text;
        paraData.ParaStorage[12] = A13.text;
        paraData.ParaStorage[13] = A14.text;
        paraData.ParaStorage[14] = A15.text;
        paraData.ParaStorage[15] = A16.text;
        paraData.ParaStorage[16] = A17.text;
        paraData.ParaStorage[17] = A18.text;
        paraData.ParaStorage[18] = A19.text;
        paraData.ParaStorage[19] = A20.text;
        paraData.ParaStorage[20] = A21.text;
        paraData.ParaStorage[21] = A22.text;
    }

    public void BacktoLobby()
    {
        GameObject.Find("Canvas").SendMessage("LoadLevel", "GameLobby");
    }
}
