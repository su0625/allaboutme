using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class E_BuildingScript : MonoBehaviour
{
    public string targetSceneName;
    [SerializeField] private GameObject lightEffect;
    [SerializeField] private GameObject E_effect;

    private void Start()
    {
        lightEffect.SetActive(false);
        E_effect.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
            return;

        if (other.gameObject.GetComponent<PhotonView>() == null)
            return;

        if (!other.gameObject.GetComponent<PhotonView>().IsMine)
            return;

        lightEffect.SetActive(true);
        E_effect.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
            return;

        if (other.gameObject.GetComponent<PhotonView>() == null)
            return;

        if (!other.gameObject.GetComponent<PhotonView>().IsMine)
            return;

        lightEffect.SetActive(false);
        E_effect.SetActive(false);
    }
}
