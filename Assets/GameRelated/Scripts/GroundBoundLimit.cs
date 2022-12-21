using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBoundLimit : MonoBehaviour
{
    [SerializeField] private GameObject blockObj;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            

        }
    }
}
