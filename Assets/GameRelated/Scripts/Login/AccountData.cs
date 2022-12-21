using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AccountData", menuName = "Data/AccountData")]

public class AccountData : ScriptableObject
{
    public string id;
    public string School;
    public string Grade;
    public string Class;
    public string Account;
    public string Password;
    public string Sex;
    public string Profession;
}
