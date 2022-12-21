using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class PlayerObjPositionFix : MonoBehaviour
{
    private Vector3 targetPosition;
    [SerializeField] private GameObject playerObj;

    private void Start()
    {
        targetPosition = this.transform.localPosition;
        
        if(this.GetComponent<PhotonView>() != null)
        {
            Debug.LogError("this Obj doesn't need any Photon Componment¡I¡I¡I");
            return;
        }
    }
    private void Update()
    {
        if (playerObj.transform.eulerAngles.y == 0)
        {
            this.transform.localPosition = targetPosition;
            this.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (playerObj.transform.eulerAngles.y == 180)
        {
            this.transform.localPosition = new Vector3(-targetPosition.x, targetPosition.y, 0);
            this.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
