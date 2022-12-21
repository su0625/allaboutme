using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_WarningAutoClose : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);
    }
}
