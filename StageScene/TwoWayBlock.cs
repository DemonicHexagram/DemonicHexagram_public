using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoWayBlock : MonoBehaviour
{
    public GameObject ThisGameObject;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(KeyWordManager.str_TagPlayer))
        {
            ThisGameObject.SetActive(false);

        }
    }
}
